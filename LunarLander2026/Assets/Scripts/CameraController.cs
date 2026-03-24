using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform Target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Camera.main.transform.RotateAround(Target.position, Camera.main.transform.right, Input.GetAxis("Mouse Y"));
            Camera.main.transform.RotateAround(Target.position, Vector3.up, -Input.GetAxis("Mouse X"));
        }
        return;
    }

}
