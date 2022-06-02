using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenuMusic : MonoBehaviour
{
    [Header("FMOD Info")]
    private string music_path = "event:/Main Menu Music";
    public FMOD.Studio.EventInstance music_event;
    //private Slider slider;

    // Start is called before the first frame update
    void Awake()
    {
        //slider = GetComponent<Slider>();
        music_event = FMODUnity.RuntimeManager.CreateInstance(music_path);
        music_event.start();
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetVolume(float volume)
    {
        music_event.setVolume(volume);
    }
    
}
