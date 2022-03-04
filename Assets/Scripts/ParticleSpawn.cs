using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawn : MonoBehaviour
{
    private ParticleSystem particles;
    private float timer = 0;
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        particles.Play();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 5)
            Destroy(this.gameObject);
    }
}
