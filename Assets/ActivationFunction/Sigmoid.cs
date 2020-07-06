namespace NeuralNetwork.ActivationFunction
{
    public class Sigmoid : IActivationFunction
    {
        /// <summary>
        /// Logistic Softstep with a range of (0,1)
        /// Works well as a classifier
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public double Execute (double value)
        {
            double k = (double)System.Math.Exp(value);
            return k / (1.0f + k);
        }
    }
}
