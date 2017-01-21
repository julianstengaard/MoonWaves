using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moonControl : MonoBehaviour {

    public ParticleSystem moonSucktionParticles;

    void Start()
    {
        moonSucktionParticles.Stop();
    }

    public void turnOffSucktionParticles()
    {
        moonSucktionParticles.Stop();
    }

    public void turnOnSucktionParticles()
    {
        moonSucktionParticles.Play();
    }
}
