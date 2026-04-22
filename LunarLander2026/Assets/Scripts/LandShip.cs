using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LandShip : MonoBehaviour
{
    public Transform nukethis;
    public bool landed = false;
    public int SceneNumber = 0;  //first scene is already loaded

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(landed)
        {
            landed = false;
            StartCoroutine("LoadNextScene", 2);  // wait 5 seconds and reload the same scene.

        }
    }


    IEnumerator LoadNextScene(float seconds)
    {
        //dont do nuthin until seconds have expired
        yield return new WaitForSeconds(seconds);

        //nuke what we dont want to carry to the next scene
        Destroy(nukethis.gameObject);

        //increment scene number
        SceneNumber++;

        //MAKE SURE IT RUNS ONCE
        StopAllCoroutines();
        
        //load it
        Debug.Log("Reset done Scene: " + SceneNumber);
        SceneManager.LoadScene(SceneNumber, LoadSceneMode.Additive);
       
    }
}
