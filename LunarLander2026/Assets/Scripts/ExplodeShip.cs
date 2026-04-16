using UnityEngine;
using UnityEngine.VFX;


public class ExplodeShip : MonoBehaviour
{

    public bool explode = false;

    //vfx for explosion goes here (Bia)
    public VisualEffect ExplosionVFX;
    public ParticleSystem particleSystem;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        ExplosionVFX.pause = true;
        particleSystem.Stop();
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

                //in addition to above call on event to handle Vfx explosion (Bia)
                ExplosionEffects();
            }

        }

    }

    void ExplosionEffects()
    {
        //reseting the visual effect and unpause it (Bia)
        ExplosionVFX.Reinit();
        ExplosionVFX.pause = false;

        particleSystem.Play();

    }
}
