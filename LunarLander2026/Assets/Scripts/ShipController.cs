using UnityEngine;

public class ShipController : MonoBehaviour
{

    public BallGravity ShipPhysics;
    public float ThrustForce = 2.0f;

    public Transform VertThruster;
    public Transform RightThruster;
    public Transform LeftThruster;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float TL = 0;
        float TR = 0;
        float TU = 0;
        float TF = 0;
        float TB = 0;

        bool keyIsPressed = false;

        if (Input.GetKey(KeyCode.W))
        {
            TU = ThrustForce;
            keyIsPressed = true;


        }
        if (Input.GetKey(KeyCode.A))
        {
            TL = ThrustForce;
            keyIsPressed = true;
        }
        
        if (Input.GetKey(KeyCode.D))
        {

            TR = ThrustForce;
            keyIsPressed = true;
        }

        //(Bia) adding if statements for forward and backwards thrust
        // up arrow and down arrow used since wasd is for vertical movement
        if (Input.GetKeyDown(KeyCode.UpArrow))
            { 
                TF = ThrustForce;
                keyIsPressed = true;
            }

        if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                TB = ThrustForce;
                keyIsPressed = true;
            }

        ShipPhysics.thrust = Vector3.left * TL + Vector3.right * TR + Vector3.up * TU + Vector3.forward * TF + Vector3.back * TB;

        float normalY = 1.0f;
        Quaternion normalRotation = Quaternion.identity;
        Quaternion rightRotation = Quaternion.Euler(0, 0, -10.0f);
        Quaternion leftRotation = Quaternion.Euler(0, 0, 10.0f);

        if (TU > 0.0f)
        {
            //thrust vfx here
            //play a sound here

            VertThruster.localScale = new Vector3(VertThruster.localScale.x, 2.0f, VertThruster.localScale.z);

        }
        else if (keyIsPressed == false)
        {
            VertThruster.localScale = new Vector3(VertThruster.localScale.x, normalY, VertThruster.localScale.z);
        }

        if (TL > 0.0f)
        {
            //thrust vfx here
            //play a sound here
            Debug.Log("HELLOOOOOO LEFt");
            LeftThruster.localScale = new Vector3(LeftThruster.localScale.x, 2.0f, LeftThruster.localScale.z);
            transform.rotation = leftRotation;

        }
        else if (keyIsPressed == false)
        {
            LeftThruster.localScale = new Vector3(LeftThruster.localScale.x, normalY, LeftThruster.localScale.z);
            transform.rotation = normalRotation;
        }

        if (TR > 0.0f)
        {
            //thrust vfx here
            //play a sound here

            RightThruster.localScale = new Vector3(RightThruster.localScale.x, 2.0f, RightThruster.localScale.z);
            transform.rotation = rightRotation;

        }
        else if (keyIsPressed == false)
        {
            RightThruster.localScale = new Vector3(RightThruster.localScale.x, normalY, RightThruster.localScale.z);
            transform.rotation = normalRotation;
        }

        //(BIA) forward and backwarrds thrust
        if (TF > 0.0f)
        {
            Debug.Log("forward movement");
        }
        else if (keyIsPressed == false)
        {

        }

        if (TB > 0.0f) 
        {
            Debug.Log("Backwards movement");
        }
        else if (keyIsPressed == false)
        {

        }
    }
}
