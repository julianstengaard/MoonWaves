using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	[Header("Ambience Settings")]
	public float fadeTime;
	public float wavesTwoSwitchCount;
	public float wavesThreeSwitchCount;

	[Header("Ambience")]
	public AudioSource wavesOne;
	public AudioSource wavesTwo;
	public AudioSource wavesThree;

	void Start()
	{
		wavesOne.volume = 0;
		wavesOne.Play();
		wavesTwo.volume = 0;
		wavesTwo.Play();
		wavesThree.volume = 0;
		wavesThree.Play();

	}

	void Update()
	{
		UpdateWaves(WaterParticle.waveCollisionCountAvg);
	}

	private void UpdateWaves(int count)
	{
		if (count > 0 && count < wavesTwoSwitchCount)
		{
			wavesOne.volume += Time.deltaTime / fadeTime;
			wavesTwo.volume -= Time.deltaTime / fadeTime;
			wavesThree.volume -= Time.deltaTime / fadeTime;
		}
		else if (count > wavesTwoSwitchCount && count < wavesThreeSwitchCount)
		{
			wavesOne.volume -= Time.deltaTime / fadeTime;
			wavesTwo.volume += Time.deltaTime / fadeTime;
			wavesThree.volume -= Time.deltaTime / fadeTime;
		}
		else if (count > wavesThreeSwitchCount)
		{
			wavesOne.volume -= Time.deltaTime / fadeTime;
			wavesTwo.volume -= Time.deltaTime / fadeTime;
			wavesThree.volume += Time.deltaTime / fadeTime;
		}
	}
}
