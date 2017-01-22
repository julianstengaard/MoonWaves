using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moonControl : MonoBehaviour {

    public ParticleSystem moonSucktionParticles;

	public AudioClip[] suckSounds;
	public float[] volumes;

	public AudioSource suckSource;

	public void playSuckSounds(int index){

		suckSource.volume = volumes[index];
		suckSource.clip = suckSounds[index];
		suckSource.Play();

	}

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
