using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour
{

    public BallGravity ShipPhysics;
    public float ThrustForce = 2.0f;

    public Transform VertThruster;
    public Transform RightThruster;
    public Transform LeftThruster;
    public Transform ForwardThruster;
    public Transform BackThruster;

    public float Consumption;
    public float FuelCapacity = 3.0f;
    public Text FuelPercentText;
    public float FuelPercent;
    public Scrollbar Fuelscroll;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Consumption = 0;   
    }

    // Update is called once per frame
    void Update()
    {

        FuelPercent = ( Consumption / FuelCapacity);

        FuelPercentText.text = " Percent: " + FuelPercent.ToString() ;
        Fuelscroll.size = FuelPercent;

        float TL = 0;
        float TR = 0;
        float TU = 0;
        float TF = 0;
        float TB = 0;

        bool keyIsPressed = false;

        if (Consumption > FuelCapacity)
        {
            ShipPhysics.thrust = Vector3.zero;
            return;
        }
            

        if (Input.GetKey(KeyCode.Space))
        {
            TU = ThrustForce;
            keyIsPressed = true;
            Consumption += Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.A))
        {
            TL = ThrustForce;
            keyIsPressed = true;
            Consumption += Time.deltaTime;
        }
        
        if (Input.GetKey(KeyCode.D))
        {

            TR = ThrustForce;
            keyIsPressed = true;
            Consumption += Time.deltaTime;
        }

        //(Bia) adding if statements for forward and backwards thrust
        // up arrow and down arrow used since wasd is for vertical movement
        //GetKey is better than GetKeyDown for thruster
        if (Input.GetKey(KeyCode.W))
        { 
            TF = ThrustForce;
            keyIsPressed = true;
            Consumption += Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            TB = ThrustForce;
            keyIsPressed = true;
            Consumption += Time.deltaTime;
        }

        ShipPhysics.thrust = Vector3.left * TL + Vector3.right * TR + Vector3.up * TU + Vector3.forward * TF + Vector3.back * TB;

        float normalY = 1.0f;
        Quaternion normalRotation = Quaternion.identity;
        Quaternion rightRotation = Quaternion.identity; //Quaternion.Euler(0, 0, -10.0f);
        Quaternion leftRotation = Quaternion.identity; //Quaternion.Euler(0, 0, 10.0f);

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
            ForwardThruster.localScale = new Vector3(ForwardThruster.localScale.x, 2.0f, ForwardThruster.localScale.z);
        }
        else if (keyIsPressed == false)
        {
            ForwardThruster.localScale = new Vector3(ForwardThruster.localScale.x, normalY, ForwardThruster.localScale.z);
        }

        if (TB > 0.0f) 
        {
            Debug.Log("Backwards movement");
            BackThruster.localScale = new Vector3(BackThruster.localScale.x, 2.0f, BackThruster.localScale.z);
        }
        else if (keyIsPressed == false)
        {
            BackThruster.localScale = new Vector3(BackThruster.localScale.x, normalY, BackThruster.localScale.z);
        }
    }
}
