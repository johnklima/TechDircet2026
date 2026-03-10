using UnityEngine;

public class ShipController : MonoBehaviour
{

    public BallGravity ShipPhysics;
    public float ThrustForce = 2.0f;  
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


        if (Input.GetKey(KeyCode.W))
        {
            TU = ThrustForce; 
           
        }
        if (Input.GetKey(KeyCode.A))
        {
            TL = ThrustForce;
        }
        
        if (Input.GetKey(KeyCode.D))
        {

            TR = ThrustForce;
        }

        ShipPhysics.thrust = Vector3.left * TL + Vector3.right * TR + Vector3.up * TU;
    }
}
