using NeuralNetwork.ActivationFunction;

namespace NeuralNetwork
{
    public class BrainFactoryInput
    {
        /// <summary>
        /// The activation function that will be used by the input and output layers
        /// </summary>
        public IActivationFunction ActivationFunctionInputOutput { get; set; }

        /// <summary>
        /// The activation function that will be used by the hidden layers
        /// </summary>
        public IActivationFunction ActivationFunctionHiddenLayers { get; set; }
        public int Inputs { get; set; }
        public int Outputs { get; set; }
        public int HiddenLayers { get; set; } 
        public int NeuronsPerHiddenLayer { get; set; }
        public double Alpha { get; set; }
    }
}
