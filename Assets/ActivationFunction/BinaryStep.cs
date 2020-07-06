namespace NeuralNetwork.ActivationFunction
{
    /// <summary>
    /// Activation step with a range of {0,1}
    /// </summary>
    public class BinaryStep : IActivationFunction
    {
        public double Execute(double value)
        {
            if (value < 0) return 0;

            return 1;
        }
    }
}
