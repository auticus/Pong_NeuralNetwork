using NeuralNetwork.Brain;
using System;

namespace NeuralNetwork
{
    /// <summary>
    /// A creepy brain factory where we wire up a neural network
    /// </summary>
    public class BrainFactory
    {
        public static T CreateBrain<T>(BrainFactoryInput input) where T: Brain.Brain
        {
            var nNetwork = new Brain.NeuralNetwork(input.ActivationFunctionInputOutput, input.ActivationFunctionHiddenLayers,
                input.Inputs, input.Outputs, input.HiddenLayers, input.NeuronsPerHiddenLayer, input.Alpha);
            return (T)Activator.CreateInstance(typeof(T), nNetwork);
        }
    }
}
