using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork.Training
{
    public interface ITrainingSet
    {
        List<TrainingSet> TrainingSets { get; }
        double[] InitialWeights { get; }
    }
}
