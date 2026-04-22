using UnityEngine;

public class Spawn : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //set up the scene
        
        //get ship
        GameObject ship = GameObject.Find("Ship");
        ship.transform.position = transform.position;
        
        //get this scene additive's thinks to nuke
        GameObject nukeThis = GameObject.Find("ThingsToNuke");
        ship.GetComponent<LandShip>().nukethis = nukeThis.transform;

        //hide spawn
        transform.gameObject.SetActive(false);
        
    }


}
