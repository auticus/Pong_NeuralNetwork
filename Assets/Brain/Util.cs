using System;

namespace NeuralNetwork
{
    public static class Util
    {
        public static double RandomRange(double minValue, double maxValue)
        {
            var rnd = new Random();
            return rnd.NextDouble() * (maxValue - minValue) + minValue;
        }
    }
}
