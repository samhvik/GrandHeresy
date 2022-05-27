using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleAttraction : MonoBehaviour
{
    public bool targetExtractionPoint;
    public Transform target;
    public float speed;
    private ParticleSystem.Particle[] particles;
    private ParticleSystem particleSystem;
    private Transform beaconTransform;
 
    void Start()
    {
        if (particleSystem == null) particleSystem = GetComponent<ParticleSystem>();
        if(targetExtractionPoint)
        {
            beaconTransform = GameObject.FindWithTag("Beacon").transform;
            target = beaconTransform;
        } 
    }
 
    void Update()
    {
        particles = new ParticleSystem.Particle[particleSystem.particleCount];
        particleSystem.GetParticles(particles);

        for (int i = 0; i < particles.Length; i++)
        {
            float distance = Vector3.Distance(target.position, particles[i].position);
            if (distance > 0.1f)
            {
                particles[i].position = Vector3.MoveTowards(particles[i].position, target.position, Time.deltaTime * speed);
            }
        }
        particleSystem.SetParticles(particles, particles.Length);
    }
}