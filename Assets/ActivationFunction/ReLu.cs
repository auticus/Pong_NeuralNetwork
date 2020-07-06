using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork.ActivationFunction
{
    public class ReLu : IActivationFunction
    {
        /// <summary>
        /// Rectified Linear Unit (0, Infinity)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public double Execute(double value)
        {
            if (value > 0) return value;

            return 0;
        }
    }
}
