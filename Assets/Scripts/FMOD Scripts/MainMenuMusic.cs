using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    [Header("FMOD Info")]
    private string music_path = "event:/Main Menu Music";
    public FMOD.Studio.EventInstance music_event;

    // Start is called before the first frame update
    void Start()
    {
        music_event = FMODUnity.RuntimeManager.CreateInstance(music_path);
        music_event.start();
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
