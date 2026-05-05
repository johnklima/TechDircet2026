using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Spawn : MonoBehaviour
{
    public float sceneGravity = -2.5f;
    public int sceneNumber = 0;
    public int requiredLandings = 0;


    //lets use tex mex pro
    public TMP_Text PadsText;
    public TMP_Text PlanetsText;

     

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //set up the scene
        PadsText.text = "Pads Needed: " + requiredLandings;
        PlanetsText.text = "Planets Landed: " + sceneNumber;

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
