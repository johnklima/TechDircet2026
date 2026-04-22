using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;
using FMODUnity;

public class ExplodeShip : MonoBehaviour
{

    public bool explode = false;

    //vfx for explosion goes here (Bia)
    public VisualEffect ExplosionVFX;
    public ParticleSystem particles;

    //the quick and dirty way, I like quick and dirty...
    public ShipController shipController;

    private Fullscreen fullscreen;

    public StudioEventEmitter emitter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        ExplosionVFX.pause = true;
        particles.Stop();
        fullscreen = GetComponent<Fullscreen>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (explode)
        {
            explode = false;
            Transform ship  = shipController.TheShip;
            foreach (Transform shippart in ship)
            {
                float x = Random.Range(-10.0f, 10.0f);
                float y = Random.Range(-10.0f, 10.0f);
                float z = Random.Range(-10.0f, 10.0f);
                Vector3 impulse = Vector3.up * y + Vector3.right * x + Vector3.forward * z;

                shippart.GetComponent<BallGravity>().enabled = true;
                shippart.GetComponent<BallGravity>().impulse = impulse;
                shippart.GetComponent<BallGravity>().angAcceleration = impulse * 50.0f;
               
            }

            //disable controller
            shipController.enabled = false;

            //fade in death fullscreen
            fullscreen.fade = true;

            //play sound
            emitter.Play(); 

            //in addition to above call on event to handle Vfx explosion (Bia)
            ExplosionEffects();
            
            //then reload the scene after a bit of time after the explosion(Bia)            
            Debug.Log("timer start");

            StartCoroutine("ExplosionResetScene", 5);  // wait 5 seconds and reload the same scene.

             shipController.enabled = (false);
        }

    }


    IEnumerator ExplosionResetScene(float seconds)
    {
        //dont do nuthin until seconds have expired
        yield return new WaitForSeconds(seconds);

        //reset fullscreen shader
        fullscreen.Fade(0);
        //stop sound
        emitter.Stop();
        //now reload the same scene because we crashed      
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Reset done");

    }

    void ExplosionEffects()
    {
        //reseting the visual effect and unpause it (Bia)
        ExplosionVFX.Reinit();
        ExplosionVFX.pause = false;

        particles.Play();

    }
}
