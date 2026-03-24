
using UnityEngine;

public class ExplodeShip : MonoBehaviour
{

    public bool explode = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(explode)
        {
            explode = false;

            foreach(Transform shippart in transform)
            {
                float x = Random.Range(-10.0f, 10.0f);
                float y = Random.Range(-10.0f, 10.0f);
                float z = Random.Range(-10.0f, 10.0f);
                Vector3 impulse = Vector3.up * y + Vector3.right * x + Vector3.forward * z;

                shippart.GetComponent<BallGravity>().enabled = true;
                shippart.GetComponent<BallGravity>().impulse = impulse;
            }

        }
        
    }
}
