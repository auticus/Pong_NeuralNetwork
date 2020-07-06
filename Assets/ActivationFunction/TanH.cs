using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork.ActivationFunction
{
    public class TanH : IActivationFunction
    {
        /// <summary>
        /// An extension of the Sigmoid activation function that returns values (-1, 1)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public double Execute(double value)
        {
            //sigmoid only returns values between 0 and 1, the TanH will return values up to -1 as well
            var sigmoid = new Sigmoid();

            return (2 * (sigmoid.Execute(2 * value)) - 1);
        }
    }
}
