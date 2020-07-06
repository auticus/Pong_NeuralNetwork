using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace NeuralNetwork.Brain
{
    public abstract class Brain : IBrain
    {
        protected NeuralNetwork network;
        protected double sumSquareError = 0; //statistics - how closely your model fits the data you fed into it

        public Brain(NeuralNetwork network)
        {
            this.network = network;
        }

        public abstract List<double> Think(List<double> inputs, List<double> outputs);
        public abstract List<double> Train(List<double> inputs, List<double> outputs, int epochs);
    }
}
