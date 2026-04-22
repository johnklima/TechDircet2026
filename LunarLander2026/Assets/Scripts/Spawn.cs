using UnityEngine;

public class Spawn : MonoBehaviour
{
    public float sceneGravity = -2.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //set up the scene
        
        //get ship
        GameObject ship = GameObject.Find("Ship");
        
        //put it at spawn point
        ship.transform.position = transform.position;

        //reset physics and state
        ship.GetComponent<BallGravity>().OnRespawn();
        ship.GetComponent<BallGravity>().GRAVITY_CONSTANT = sceneGravity; // TODO: param this to OnRespawn

        //get this scene additive's things to nuke
        GameObject nukeThis = GameObject.Find("ThingsToNuke");
        ship.GetComponent<ShipController>().nukethis = nukeThis.transform;

        //hide spawn
        transform.gameObject.SetActive(false);
        
    }


}
