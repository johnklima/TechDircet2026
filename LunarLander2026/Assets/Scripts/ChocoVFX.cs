using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.VFX;

public class ChocoVFX : MonoBehaviour
{
    public VisualEffect choco;
    public VisualEffect gas;
    public VisualEffect disolve; 

    private void Start()
    {
       choco.pause = true;
       gas.pause = true;
       disolve.pause = true;
    }

    public void PlayChoco()
    {
        choco.Reinit();
        gas.Reinit();
        disolve.Reinit();
        choco.pause = false;
        gas.pause = false;
        disolve.pause= false;   
       


        StartCoroutine(stopVfx(choco, 1));
        StartCoroutine(stopVfx(gas, 1.5f));

        //StartCoroutine(stopVfx(disolve, 1.5f));

    }
    IEnumerator stopVfx(VisualEffect effect, float time)
    {       

        yield return new WaitForSeconds(time);
        effect.pause = true;
    

    }

}
