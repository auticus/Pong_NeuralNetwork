using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork.Brain
{
    public class Neuron
    {
        /* 
         * A neuron is a type of perceptron that sits in a layer.  A neuron will be connected to every other neuron in the
         * layer preceding it so it can have a great number of inputs
         */

        //The reason we have NumberInputs and Inputs separate (as opposed to just querying the list for its count) is because NumberInputs is used to populate
        //the inputs List in an external for loop
        public List<double> Inputs = new List<double>(); 
        public int NumberInputs = 0;
        public List<double> Weights = new List<double>();
        public double Bias; //used as an extra weight
        public double Output;
        public double ErrorGradient;
        
        public Neuron(int numberInputs) 
        {
            Bias = Util.RandomRange(-1.0f, 1.0f);
            NumberInputs = numberInputs;  

            for (int i = 0; i < numberInputs; i++)
                Weights.Add(Util.RandomRange(-1.0f, 1.0f));
        }
    }
}
