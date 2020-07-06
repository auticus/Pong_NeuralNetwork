using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;

namespace NeuralNetwork.Brain
{
    public class PongBrain : Brain
    {
        public PongBrain(NeuralNetwork network) : base(network)
        {

        }

        public override List<double> Think(List<double> inputs, List<double> outputs)
        {
            return network.Execute(inputs, outputs);
        }

        public override List<double> Train (List<double> inputs, List<double> outputs, int epochs)
        {
            List<double> result = null;

            for (int i = 0; i < epochs; i++)
            {
                result = Think(inputs, outputs);
                network.UpdateWeights(result, outputs); //result is the actual, outputs are what we expected
            }

            return result;
        }
    }
}
