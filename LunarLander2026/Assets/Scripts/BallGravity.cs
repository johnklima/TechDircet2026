using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallGravity : MonoBehaviour
{

    //gravity in meters per second per second
    public float GRAVITY_CONSTANT = -9.8f;                      // -- for earth,  -1.6 for moon 

    public Vector3 velocity = new Vector3(0, 0, 0);             //current direction and speed of movement
    public Vector3 acceleration = new Vector3(0, 0, 0);         //movement controlled by player movement force and gravity

    public Vector3 thrust = new Vector3(0, 0, 0);               //player applied thrust vector
    public Vector3 finalForce = new Vector3(0, 0, 0);           //final force to be applied this frame

    public float mass = 1.0f;

    public float SurfaceHeight = 0;
    public float CurrentHeight;
    public bool onSurface = false;

    public Vector3 impulse = new Vector3(0, 0, 0);

    public float timeScalar = 1.0f;

    public Transform Geometry;   //if there is a geometry to react to landing    

    public bool InertialDampnerXZ = true;
    public float DampnFactor = 0.5f;

    public Vector3 angAcceleration;
    public Vector3 AngularVelocity;
   

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        handleMovement();
    }

    void handleMovement()
    {
        if (onSurface) { return; }
         
         //I generally do rotation first, translation second
         AngularVelocity += angAcceleration * Time.deltaTime * Time.deltaTime;   
         transform.rotation *= Quaternion.Euler(AngularVelocity);


        //set the rate of integration, which is (almost) equivalent to
        //explosion by mass for impulse calc. problem is, gravity
        //is no longer a constant. but for gameplay, maybe not an issue?
        float forceDeltaTime = Time.deltaTime * timeScalar; 

        Vector3 curPos = transform.position;  //begin position

        //reset final force to the initial force of gravity
        finalForce.Set(0, GRAVITY_CONSTANT * mass, 0);
        finalForce += thrust;


        acceleration = finalForce / mass;
        velocity += acceleration * forceDeltaTime;
        velocity += impulse;

        //move the object
        transform.position += velocity * forceDeltaTime;

        //dampner
        if (InertialDampnerXZ)
        {
            
            float y = velocity.y;
            velocity = Vector3.Lerp(velocity,new Vector3(0,y,0), Time.deltaTime * DampnFactor);
        }

        RaycastHit hit;
        string hitname = "";
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 10000.0f))
        {
            CurrentHeight = hit.distance;
            hitname = hit.transform.name;

          
           
        }
            
        //TODO: rethink
        if (Mathf.Abs(CurrentHeight - SurfaceHeight) < 0.1f && Geometry)
        {
           
            //TODO: generalize this, use OOP.           
                          
                float mag = velocity.magnitude;
                if (mag > 2.0f)
                {
                    //do something game over, reload entire scene
                    Debug.Log("landed on " + hitname);
                    Debug.Log("BOOOOM!! " + mag);
                    transform.GetComponent<ExplodeShip>().explode = true;
                    onSurface = true;

            }                    
                else
                {
                    //reset lander on ground
                    
                    if (hit.transform.tag == "Platform" && onSurface == false)
                    {
                        Debug.Log("on platform");

                        //once untagged after landing it is no longer a valid surface to land on. You can still
                        //touch down, but no extra points, no extra fuel.

                        hit.transform.tag = "Untagged";
                        
                        onSurface = true;
                        //refuel ship

                        ShipController shipcontrol = transform.GetComponent<ShipController>();
                        shipcontrol.FuelCapacity += shipcontrol.FuelRefill;
                        shipcontrol.Consumption = 0.0f;
                        transform.GetComponent<LandShip>().landed = true;
                        
                    }
                    else 
                    {
                        //Debug.Log("on surface");
                    }

                }
            
            transform.position = curPos;       //hard reset to the surface
            acceleration *= 0;
            velocity *= 0;

        }

        

        //reset impulse
        impulse *= 0;


    }
   
    public void reset()
    {
        Debug.Log("BallGravity Reset");

        velocity *= 0;
        acceleration *= 0;
        impulse *= 0;
        thrust *= 0;
        
       
    }

    public void OnRespawn()
    {
        Debug.Log("BallGravity Reset on spawn");

        velocity *= 0;
        acceleration *= 0;
        impulse *= 0;
        thrust *= 0;
        onSurface = false;  

    }


}
