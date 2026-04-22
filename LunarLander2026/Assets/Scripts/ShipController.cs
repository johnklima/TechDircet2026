using FMODUnity;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class ShipController : MonoBehaviour
{

    public Transform TheShip;
    public BallGravity ShipPhysics;
    public float ThrustForce = 2.0f;

    //rocket effect
    public VisualEffect rocket;
    //rocket sound
    public StudioEventEmitter emitter;

    public float Consumption;
    public float FuelCapacity = 3.0f;
    public float FuelPercent;
    public Scrollbar Fuelscroll;

    public Camera shipCam;
    public Transform ShipRotator;

    public int sceneNumber = 0;
    public Transform nukethis;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shipCam = Camera.main;
        Consumption = 0;        
        //rocket is disabled when in edit mode
        rocket.gameObject.SetActive(true);
        //setting spawn rate seems to work best
        rocket.SetFloat("Spawn Rate", 0);
        
    }

    // Update is called once per frame
    void Update()
    {
                

        FuelPercent = ( Consumption / FuelCapacity);


        if(Fuelscroll)
        {
            
            Fuelscroll.size = FuelPercent;

        }

        
        float TL = 0;
        float TR = 0;
        float TU = 0;
        float TF = 0;
        float TB = 0;

        bool keyIsPressed = false;

        if (Consumption > FuelCapacity)
        {
            //out of gas
            ShipPhysics.thrust = Vector3.zero;
            rocket.SetFloat("Spawn Rate", 0);
            return;
        }
            
        //up
        if (Input.GetKey(KeyCode.Space))
        {
            TU = ThrustForce;
            keyIsPressed = true;
            Consumption += Time.deltaTime;

        }
        
        //left
        if (Input.GetKey(KeyCode.A))
        {
            TL = ThrustForce;
            keyIsPressed = true;
            Consumption += Time.deltaTime;
        }
        //right
        if (Input.GetKey(KeyCode.D))
        {

            TR = ThrustForce;
            keyIsPressed = true;
            Consumption += Time.deltaTime;
        }
        

        //(Bia) adding if statements for forward and backwards thrust
        // up arrow and down arrow used since wasd is for vertical movement
        //GetKey is better than GetKeyDown for thruster
        //(John) changed to space bar for up thrust so W/S forward/back
        
        //forward
        if (Input.GetKey(KeyCode.W))
        { 
            TF = ThrustForce;
            keyIsPressed = true;
            Consumption += Time.deltaTime;
        }
        //backward
        if (Input.GetKey(KeyCode.S))
        {
            TB = ThrustForce;
            keyIsPressed = true;
            Consumption += Time.deltaTime;
        }


        //TODO add camera forward for l/r/f/b, need to think...
        //add the thrust
        Vector3 dir = shipCam.transform.localPosition ;
        dir.y = 0;
        dir.Normalize();
        ShipPhysics.thrust = -dir * TF + dir * TB  + Vector3.up * TU ;

        dir = Vector3.Cross(dir, Vector3.up);
        dir.Normalize ();   
        ShipPhysics.thrust += -dir * TL + dir * TR;

        //found that spawn rate best for rocket vfx
        if (keyIsPressed)
        {
            //play a sound here
            if(emitter.IsPlaying() == false)
            {
                emitter.Play();
            }
                 


            rocket.SetFloat("Spawn Rate", 1024 );
        }
        else
        {
            //stop sound here
            emitter.Stop();
            rocket.SetFloat("Spawn Rate", 0);
        }


        //ship rotations      
        Quaternion normalRotation = Quaternion.identity;
        Quaternion rightRotation = Quaternion.Euler(0, 0, -40.0f); 
        Quaternion leftRotation = Quaternion.Euler(0, 0, 40.0f) ;
        Quaternion forwardRotation = Quaternion.Euler(40.0f, 0, 0) ;
        Quaternion backwardRotation = Quaternion.Euler(-40.0f, 0, 0) ;


        // ship rotation based on thrust?
        // maybe better to use velocity?
        // do something with camera?

        //first rotate the ship by camera - sort of ok for now
        Vector3 eulers = shipCam.transform.localRotation.eulerAngles;
        Quaternion rot = Quaternion.Euler(0, eulers.y, 0);
        
        
        //ShipRotator.localRotation = rot;
        ShipRotator.localRotation = Quaternion.Lerp(ShipRotator.localRotation,rot,Time.deltaTime);

        //now rotate locally
        if (TL > 0.0f)
        {
            TheShip.localRotation = Quaternion.Lerp(TheShip.localRotation, leftRotation, Time.deltaTime );
        }
        

        if (TR > 0.0f)
        {
            TheShip.localRotation = Quaternion.Lerp(TheShip.localRotation, rightRotation, Time.deltaTime );
        }       

        //(BIA) forward and backwarrds thrust
        if (TF > 0.0f)
        {
            TheShip.localRotation = Quaternion.Lerp(TheShip.localRotation, forwardRotation, Time.deltaTime);
        }        

        if (TB > 0.0f)
        {
            TheShip.localRotation = Quaternion.Lerp(TheShip.localRotation, backwardRotation, Time.deltaTime);
        }
        

        //restitution
        if (keyIsPressed == false)
        {
            TheShip.localRotation = Quaternion.Lerp(TheShip.localRotation, normalRotation, Time.deltaTime);
        }
        
    }
}
