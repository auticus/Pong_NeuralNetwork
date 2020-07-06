using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork.Brain
{
    public interface IBrain
    {
        List<double> Think(List<double> inputs, List<double> outputs);
        List<double> Train(List<double> inputs, List<double> outputs, int epochs);
    }
}
