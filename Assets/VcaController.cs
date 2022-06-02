using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VcaController : MonoBehaviour
{
    public string VcaName;
    public float currVolume;
    private FMOD.Studio.VCA vcaControl;
    private FMOD.Studio.Bus bus;
    private Slider slider;    

    // Start is called before the first frame update
    void Start()
    {
        bus = FMODUnity.RuntimeManager.GetBus("bus:/");
        //vcaControl = FMODUnity.RuntimeManager.GetVCA("vca:/" + VcaName);
        slider = GetComponent<Slider>();

    }

    public void SetVolume(float volume)
    {
        bus.setVolume(volume);
        bus.getVolume(out currVolume);
    }


}
