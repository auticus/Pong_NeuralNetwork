using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork.Brain
{
    public class NeuronLayer
    {
        public enum eLayerType
        {
            InputLayer = 0,
            HiddenLayer,
            OutputLayer
        }

        public List<Neuron> Neurons = new List<Neuron>();
        public eLayerType LayerType { get; }
        
        public NeuronLayer(eLayerType layerType, int numberNeurons, int numNeuronInputs)
        {
            for (int i = 0; i < numberNeurons; i++)
            {
                Neurons.Add(new Neuron(numNeuronInputs));
            }

            LayerType = layerType;
        }
    }
}
