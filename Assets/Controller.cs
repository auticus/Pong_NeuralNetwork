using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NeuralNetwork;
using NeuralNetwork.ActivationFunction;
using NeuralNetwork.Brain;

public class Controller : MonoBehaviour
{
    //the primary controller for the application that will house the brain and neural network
    public GameObject Paddle;
    public GameObject Ball;
    public float numSaved = 0;
    public float numMissed = 0;

    Rigidbody2D ball_rigidBody;
    float y_velocity;
    float paddleMinY = 8.8f; //paddle boundaries
    float paddleMaxY = 17.4f; //paddle boundaries
    float paddleMaxSpeed = 15;
    Brain pongBrain;

    void Start()
    {
        ball_rigidBody = Ball.GetComponent<Rigidbody2D>();

        var input = new BrainFactoryInput()
        {
            ActivationFunctionInputOutput = new TanH(),
            ActivationFunctionHiddenLayers = new TanH(), //do not use TanH for the xorbrain it is counter productive.  The xor needs 0 or 1, TanH brings in negative values as well
            Inputs = 6,
            Outputs = 1,
            HiddenLayers = 1,
            NeuronsPerHiddenLayer = 4,
            Alpha = 0.11 //how much impact the training has, sometimes you'll see NaN come back and this dials back the calculations a bit
        };
        pongBrain = BrainFactory.CreateBrain<PongBrain>(input);
    }

    
    void Update()
    {
        //the controller is responsible for moving the paddle on behalf of the machine.  
        //pos_y where we are going to move based on the velocity, but clamping so we don't bust out of the boundaries
        //y_velocity is neural network output, which has a value between -1 and 1 so we need to multiply that by paddleMaxSpeed
        //its kind of a percentage ... 0.5 would be 50% positive direction 

        //try and keep the values as normalized as possible (between 0 and 1 or -1 and 1) to prevent "blowouts" which is where you start overflowing numbers
        float pos_y = Mathf.Clamp(Paddle.transform.position.y + (y_velocity * Time.deltaTime * paddleMaxSpeed), 
                                    paddleMinY,
                                    paddleMaxY);
        Paddle.transform.position = new Vector3(Paddle.transform.position.x, pos_y, Paddle.transform.position.z);  //only moving up and down the y axis

        var output = new List<double>();

        //check to see if the ball has struck a "backwall" and if so adjust velocity accordingly
        //why raycast?  it tells us where the ball is going to hit.  Using physics we know where the ball will hit at all times
        //restrict it to layer (1 shift 8) picking out the backwall of the court.  Layer exists as Backwall.  In unity you should see Backwall is Layer 8
        int layerMask = 1 << 8;
        var hit = Physics2D.Raycast(Ball.transform.position, ball_rigidBody.velocity, 1000, layerMask); //origin, direction, distance, layer mask (only raycast to that layer)

        if (hit.collider != null)
        {
            //this first if statement will make the pong brain move even when the ball is bouncing off top or bottom instead of waiting to learn where it should have hit
            //when it strikes the back wall

            if (hit.collider.gameObject.tag == "tops") //reflecting off of the top or bottom
            {
                var x1 = hit.point;
                var reflection = Vector3.Reflect(ball_rigidBody.velocity, hit.normal);
                
                hit = Physics2D.Raycast(hit.point, reflection, 1000, layerMask); //origin = where it connected at top / bottom, reflection is now the direction so its going to find
                                                                                 //where on the backwall its going to strike
                if (hit.collider != null)
                {
                    Debug.DrawLine(x1, hit.point, Color.red);
                }
            }

            if (hit.collider != null && hit.collider.gameObject.tag == "backwall") //redundant call since backwall layer only contains backwall but this is here for demonstration in case you have other things on the layer
            {
                float delta_y = (hit.point.y - Paddle.transform.position.y); //the change in y, the error of where it should be compared to where it is now

                output = Run(Ball.transform.position.x,
                            Ball.transform.position.y,
                            ball_rigidBody.velocity.x,
                            ball_rigidBody.velocity.y,
                            Paddle.transform.position.x,
                            Paddle.transform.position.y,
                            delta_y,
                            train: true); //this currently trains as it goes.  So its always going to be learning
                y_velocity = (float)output[0];
            }
        }
        else y_velocity = 0;
    }

    List<double> Run(double ball_x, double ball_y, double ball_xVelocity, double ball_yVelocity, double paddleX, double paddleY, double paddleVelocity, bool train)
    {
        //inevitably the inputs we are looking at are where the ball is, how fast its moving, where my paddle is, and how fast my paddle is currently moving
        //the machine will then figure out what to make its paddle velocity need to be to match where the ball is going (the only output)
        var inputs = new List<double>() { ball_x, ball_y, ball_xVelocity, ball_yVelocity, paddleX, paddleY};
        var outputs = new List<double>() { paddleVelocity };

        return train ? pongBrain.Train(inputs, outputs, 1) : pongBrain.Think(inputs, outputs);
    }
}
