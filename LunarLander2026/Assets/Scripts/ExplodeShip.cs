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

    StudioEventEmitter emitter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        ExplosionVFX.pause = true;
        particles.Stop();
        fullscreen = GetComponent<Fullscreen>();

        emitter = GetComponent<StudioEventEmitter>();

    }

    // Update is called once per frame
    void Update()
    {
        if (explode)
        {
            explode = false;

            foreach (Transform shippart in transform)
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

            //nope
            //Invoke("ExplosionResetScene", 2 );

            //Also stop Coroutine for ship controls to keep them still (Bia)
            //StopCoroutine("");
            //nope
            //Ok StopCoroutine is for the Explosion Reset (Bia)

            shipController.enabled = (false);
        }

    }


    IEnumerator ExplosionResetScene(float seconds)
    {
        //dont do nuthin until seconds have expired
        yield return new WaitForSeconds(seconds);

        fullscreen.Fade(0);
        //now reload the same scene because we crashed
        Debug.Log("Reset done");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    void ExplosionEffects()
    {
        //reseting the visual effect and unpause it (Bia)
        ExplosionVFX.Reinit();
        ExplosionVFX.pause = false;

        particles.Play();

    }
}
