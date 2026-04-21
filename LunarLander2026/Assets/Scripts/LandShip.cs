using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LandShip : MonoBehaviour
{
    public Transform nukethis;
    public bool landed = false;

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

        //now reload the same scene because we crashed
        Debug.Log("Reset done");

        Destroy(nukethis.gameObject);

        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }
}
