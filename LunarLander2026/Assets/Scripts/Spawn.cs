using UnityEngine;

public class Spawn : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //set up the scene
        GameObject ship = GameObject.Find("Ship");
        ship.transform.position = transform.position;

        transform.gameObject.SetActive(false);
        
    }


}
