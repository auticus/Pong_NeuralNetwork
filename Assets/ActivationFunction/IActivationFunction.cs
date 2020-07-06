using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork.ActivationFunction
{
    //for full list of activation functions see en.wikipedia.org/wiki/Activation_function
    //reference:  https://medium.com/the-theory-of-everything/understanding-activation-functions-in-neural-networks-9491262884e0

    //what you want to train the app to do is important, and there is trial and error to get what you are after
    //and figure out what activation functions work for you
    //for example xor works really well with step, but won't get trained well with ReLu
    public interface IActivationFunction
    {
        double Execute(double value);
    }
}
