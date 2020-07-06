using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace NeuralNetwork.ActivationFunction
{
    public class LeakyReLu : IActivationFunction
    {
        /// <summary>
        /// Leaky rectified linear unit that returns value (- infinity , infinity)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public double Execute(double value)
        {
            if (value < 0) return 0.01 * value; //this is what puts the tiny downward slope on the back end of the graph to give us something to work with in terms of learning
            return value;
        }
    }
}
