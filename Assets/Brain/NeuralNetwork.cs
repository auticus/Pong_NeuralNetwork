using UnityEngine;
using NeuralNetwork.ActivationFunction;
using System;
using System.Collections.Generic;

namespace NeuralNetwork.Brain
{
    public class NeuralNetwork
    {
        int _numberInputs; //number of inputs coming into the network right at the start
        int _numberOutputs; //number of outputs leaving the neural network
        int _numberHiddenLayers; //the number of layers between the input layer and the output layer
        int _neuronsPerHidden; //number of neurons in each hidden layer (simple implementation where each layer has same number of neurons)
        double _alpha; //a value that determines how fast the neural network is going to learn, how each training run will impact the overall weights (1 being fully)
        List<NeuronLayer> _layers = new List<NeuronLayer>();
        IActivationFunction _activationFunctionInputOutput;
        IActivationFunction _activationFunctionHiddenLayers;

        public NeuralNetwork(IActivationFunction activationFunctionInputOutput, IActivationFunction activationFunctionHiddenLayers,
            int inputs, int outputs, int hiddenLayers, int nPerHidden, double alpha)
        {
            _numberInputs = inputs;
            _numberOutputs = outputs;
            _numberHiddenLayers = hiddenLayers;
            _neuronsPerHidden = nPerHidden;
            _alpha = alpha;
            
            if (_numberHiddenLayers == 0) //no hidden layers, just have one layer - the output layer
            {
                //output layer
                _layers.Add(new NeuronLayer(NeuronLayer.eLayerType.OutputLayer,_numberOutputs, _numberInputs));
                return;
            }

            //there are hidden layers
            _layers.Add(new NeuronLayer(NeuronLayer.eLayerType.InputLayer,_neuronsPerHidden, _numberInputs)); //input layer

            for (int i = 0; i < _numberHiddenLayers - 1; i++)
            {
                _layers.Add(new NeuronLayer(NeuronLayer.eLayerType.HiddenLayer,_neuronsPerHidden, _neuronsPerHidden)); //hidden layers
            }

            //output layer
            _layers.Add(new NeuronLayer(NeuronLayer.eLayerType.OutputLayer,_numberOutputs, _neuronsPerHidden));

            _activationFunctionInputOutput = activationFunctionInputOutput;
            _activationFunctionHiddenLayers = activationFunctionHiddenLayers;
        }

        public List<double> Execute(List<double> inputValues, List<double> desiredOutputs)
        {
            var outputs = new List<double>();

            if (inputValues.Count != _numberInputs)
            {
                Debug.Log("ERROR: Number of Inputs must be " + _numberInputs);
                return outputs;
            }

            var inputs = new List<double>(inputValues);
            bool firstRun = true;
            for (int i = 0; i < _numberHiddenLayers + 1; i++) 
            {
                //init the first run with the input values passed (this is the input layer), after use whats in outputs (the hidden layers or the output layer)
                if (firstRun) 
                    firstRun = false;
                else 
                    inputs = new List<double>(outputs);

                var layer = _layers[i];
                
                outputs.Clear();

                for (int j = 0; j < layer.Neurons.Count; j++)
                {
                    var neuron = layer.Neurons[j];
                    double dotProduct = 0;
                    neuron.Inputs.Clear();

                    for (int k = 0; k < neuron.NumberInputs; k++)
                    {
                        neuron.Inputs.Add(inputs[k]);
                        dotProduct += neuron.Weights[k] * inputs[k]; //this is essentially our dot product that the perceptron does
                    }

                    dotProduct -= neuron.Bias;

                    if (layer.LayerType == NeuronLayer.eLayerType.HiddenLayer)
                        neuron.Output = _activationFunctionHiddenLayers.Execute(dotProduct);
                    else
                        neuron.Output = _activationFunctionInputOutput.Execute(dotProduct);

                    outputs.Add(neuron.Output);
                }
            }

            return outputs; //the answer from the inputs
        }

        public void UpdateWeights(List<double> outputs, List<double> desiredOutput)
        {
            double error;
            for (int i = _numberHiddenLayers; i >=0; i--) //loop through the layers in reverse because we need to take the error we get at the end and feed it back through the layers
            {
                for (int j = 0; j < _layers[i].Neurons.Count; j++) //loop through the layer neurons
                {
                    if (i == _numberHiddenLayers) //the last layer (the output layer) - the only time we can calculate the actual error
                    {
                        //errorGradient calculated with the delta rule: en.wikipedia.org/wiki/Delta_rule
                        error = desiredOutput[j] - outputs[j];
                        _layers[i].Neurons[j].ErrorGradient = outputs[j] * (1 - outputs[j]) * error;
                        //the error gradient works out how responsible that particular neuron is to the overall error (the percentage)
                        //if you add all the error gradients up it should add up to the total error
                    }
                    else //the other layers
                    {
                        _layers[i].Neurons[j].ErrorGradient = _layers[i].Neurons[j].Output * (1 - _layers[i].Neurons[j].Output);
                        double errorGradSum = 0; //the errors in the layer above 
                        foreach(var neuron in _layers[i+1].Neurons)
                        {
                            errorGradSum += neuron.ErrorGradient * neuron.Weights[j];
                        }
                        _layers[i].Neurons[j].ErrorGradient *= errorGradSum;
                    }
                    for (int k = 0; k < _layers[i].Neurons[j].NumberInputs; k++) //foreach input for that neuron
                    {
                        if (i == _numberHiddenLayers) //the last layer (the output layer)
                        {
                            error = desiredOutput[j] - outputs[j];
                            _layers[i].Neurons[j].Weights[k] += _alpha * _layers[i].Neurons[j].Inputs[k] * error; //update the weight of that layer 
                            //_alpha is the learning rate
                        }
                        else //the other layers
                        {
                            _layers[i].Neurons[j].Weights[k] += _alpha * _layers[i].Neurons[j].Inputs[k] * _layers[i].Neurons[j].ErrorGradient;
                        }
                    }
                    _layers[i].Neurons[j].Bias += _alpha * -1 * _layers[i].Neurons[j].ErrorGradient;
                }
            }
        }
    }
}
