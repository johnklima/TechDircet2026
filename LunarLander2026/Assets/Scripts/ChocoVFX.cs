using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class ChocoVFX : MonoBehaviour
{
    public VisualEffect choco;
    public VisualEffect gas;

    private void Start()
    {
       choco.pause = true;
       gas.pause = true;
    }

    public void PlayChoco()
    {
        choco.Reinit();
        gas.Reinit();
        choco.pause = false;
        gas.pause = false;
        StartCoroutine(stopVfx());

    }
    IEnumerator stopVfx()
    {       

        yield return new WaitForSeconds(1);
        choco.pause = true;
        gas.pause = true;

    }

}
