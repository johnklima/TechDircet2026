using UnityEngine;

public class SnapObject : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(Transform child in transform)
        {
            RaycastHit hit;
            if(Physics.Raycast(child.position, Vector3.down, out hit))
            {
                
                child.position = hit.point;
            }

        }

    }


}
