using UnityEngine;

public class Fullscreen : MonoBehaviour
{
    public Material fullscreen;
    public bool fade = false;
    private float fadeVal = 0;

    private void Start()
    {
        fullscreen.SetFloat("_VignetteIntensity", 0);
        fade = false;
        fadeVal = 0;
    }
    private void Update()
    {
        if(fade)
        {
            fadeVal += Time.deltaTime * 4.0f;
            
            if(fadeVal > 20 )
            {
                fadeVal = 20;
                fade = false;
            }
            Fade(fadeVal);
        }
    }

    public void Fade(float amount)
    {
        fullscreen.SetFloat("_VignetteIntensity", amount);
    }

}
