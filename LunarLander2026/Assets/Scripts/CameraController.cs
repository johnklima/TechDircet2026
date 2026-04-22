using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform Target;

    [Header("Camera Settings")]
    [Range(0f, 20f)]
    public float mouseSensitivity = 10;
    public float dstFromTarget = 2;
    public Vector2 pitchMinMax = new Vector2(-40, 85);
    public float rotationSmoothTime = .12f;

    [Header("Cursor Check")]
    public bool lockCursor;

    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;


    float yaw;
    float pitch;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }

    // Update is called once per frame
    void Update()
    {

        // Camera.main.transform.RotateAround(Target.position, Camera.main.transform.right, Input.GetAxis("Mouse Y"));
        // Camera.main.transform.RotateAround(Target.position, Vector3.up, -Input.GetAxis("Mouse X"));

        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;

        transform.position = Target.transform.position - transform.forward * dstFromTarget;
    }


}
