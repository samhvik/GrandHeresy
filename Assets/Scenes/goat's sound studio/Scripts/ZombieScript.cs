using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    private string eventpath = "event:/ZombieWalkRun";
    private FMOD.Studio.EventInstance e;

    // step info
    public float m_StepDistance = 2.5f;
    float m_DistanceTravelled;
    float m_StepRand;
    Vector3 m_PrevPos;

    // Start is called before the first frame update
    void Start()
    {
        //Initialise member variables
        m_StepRand = Random.Range(0.0f, 1.5f);
        m_PrevPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        e.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        m_DistanceTravelled += (transform.position - m_PrevPos).magnitude;

        if (m_DistanceTravelled >= m_StepDistance + m_StepRand)
        {
            PlayFootstepSound();
            m_StepRand = Random.Range(0.0f, 1.5f);      //Adding subtle random variation to the distance required before a step is taken - Re-randomise after each step.
            m_DistanceTravelled = 0.0f;
        }
        m_PrevPos = transform.position;
    }

    void PlayFootstepSound()
    {
        e = FMODUnity.RuntimeManager.CreateInstance(eventpath);
        e.start();
        e.release();//Release each event instance immediately, there are fire and forget, one-shot instances. 
    }
}
