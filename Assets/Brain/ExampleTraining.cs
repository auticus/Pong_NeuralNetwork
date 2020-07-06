using System.Collections;
using System.Collections.Generic;
using NeuralNetwork;
using NeuralNetwork.ActivationFunction;
using NeuralNetwork.Training;
using NeuralNetwork.Brain;
using UnityEngine;

public class ExampleTraining : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TrainNeuralNetwork();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private static void TrainNeuralNetwork()
    {
        /*
        var input = new BrainFactoryInput()
        {
            ActivationFunctionInputOutput = new Sigmoid(),
            ActivationFunctionHiddenLayers = new Sigmoid(), //do not use TanH for the xorbrain it is counter productive.  The xor needs 0 or 1, TanH brings in negative values as well
            Inputs = 2,
            Outputs = 1,
            HiddenLayers = 1,
            NeuronsPerHiddenLayer = 2,
            Alpha = 0.8 //how much impact the training has, sometimes you'll see NaN come back and this dials back the calculations a bit
        };
        var brain = BrainFactory.CreateBrain<XorBrain>(input);
        brain.Think(trainingIterations: 1000);
        */
    }
}
