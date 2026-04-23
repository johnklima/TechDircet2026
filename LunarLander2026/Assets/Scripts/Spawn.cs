using UnityEngine;

public class Spawn : MonoBehaviour
{
    public float sceneGravity = -2.5f;
    public int sceneNumber = 0;
    public int requiredLandings = 0;

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
        ship.GetComponent<ShipController>().sceneNumber = sceneNumber;
        ship.GetComponent<LandShip>().requiredLandings = requiredLandings;

        //hide spawn (me)
        transform.gameObject.SetActive(false);
        
    }


}
