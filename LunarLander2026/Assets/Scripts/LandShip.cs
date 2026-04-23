using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LandShip : MonoBehaviour
{
    
    public bool landed = false;
    private ShipController shipController;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shipController = GetComponent<ShipController>();    
    }

    // Update is called once per frame
    void Update()
    {
       if(landed)
        {
            landed = false;
            StartCoroutine("LoadNextScene", 2);  // wait 2 seconds and reload the same scene.

        }
    }


    IEnumerator LoadNextScene(float seconds)
    {
        //dont do nuthin until seconds have expired
        yield return new WaitForSeconds(seconds);


        //MAKE SURE IT RUNS ONCE
        StopAllCoroutines();

        //add one to it and go!
        int scenenumber = shipController.sceneNumber + 1;
        //load it
        Debug.Log("Reset done Scene: " + scenenumber);
        SceneManager.LoadScene(scenenumber);
       
    }
}
