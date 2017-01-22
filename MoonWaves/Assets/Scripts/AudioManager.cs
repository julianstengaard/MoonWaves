using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	private static AudioManager _instance;

	[Header("Moon Collision")]
	public AudioSource moonCollision;

	[Header("Castle References")]
	public AudioSource minorDmg;
	public AudioSource destroyTower;
	public AudioSource destroyCastle;

	[Header("Ambience Settings")]
	public float fadeTime;
	public float wavesVolume;
	public float wavesTwoSwitchCount;
	public float wavesThreeSwitchCount;

	[Header("Ambience References")]
	public AudioSource wavesOne;
	public AudioSource wavesTwo;
	public AudioSource wavesThree;

	[Header("Events Settings")]
	public float waveCrashVolume;
	public float waveCrashCooldown;

	private Dictionary<Moon.Players, float> waveCrashPlayerDelay;

	[Header("Events References")]
	public AudioSource waveCrash;
	public AudioClip[] waveCrashes;

	void Awake()
	{
		_instance = this;
		waveCrashPlayerDelay = new Dictionary<Moon.Players, float>();
	}

	void Start()
	{
		//wavesOne.volume = 0;
		//wavesOne.Play();
		//wavesTwo.volume = 0;
		//wavesTwo.Play();
		//wavesThree.volume = 0;
		//wavesThree.Play();
	}

	void Update()
	{
		//UpdateWaves(WaterParticle.waveCollisionCountAvg);
		UpdateWaveCrashDelays();

	}

	private void UpdateWaves(int count)
	{
		if (count > 0 && count < wavesTwoSwitchCount)
		{
			wavesOne.volume = Mathf.Clamp(wavesOne.volume + Time.deltaTime / fadeTime, 0, wavesVolume);
			wavesTwo.volume = Mathf.Clamp(wavesTwo.volume - Time.deltaTime / fadeTime, 0, wavesVolume);
			wavesThree.volume = Mathf.Clamp(wavesThree.volume - Time.deltaTime / fadeTime, 0, wavesVolume);
		}
		else if (count > wavesTwoSwitchCount && count < wavesThreeSwitchCount)
		{
			wavesOne.volume = Mathf.Clamp(wavesOne.volume - Time.deltaTime / fadeTime, 0, wavesVolume);
			wavesTwo.volume = Mathf.Clamp(wavesTwo.volume + Time.deltaTime / fadeTime, 0, wavesVolume);
			wavesThree.volume = Mathf.Clamp(wavesThree.volume - Time.deltaTime / fadeTime, 0, wavesVolume);
		}
		else if (count > wavesThreeSwitchCount)
		{
			wavesOne.volume = Mathf.Clamp(wavesOne.volume - Time.deltaTime / fadeTime, 0, wavesVolume);
			wavesTwo.volume = Mathf.Clamp(wavesTwo.volume - Time.deltaTime / fadeTime, 0, wavesVolume);
			wavesThree.volume = Mathf.Clamp(wavesThree.volume + Time.deltaTime / fadeTime, 0, wavesVolume);
		}
	}

	private void UpdateWaveCrashDelays()
	{
		List<Moon.Players> players = new List<Moon.Players>(waveCrashPlayerDelay.Keys);
		foreach (Moon.Players player in players)
		{
			if (waveCrashPlayerDelay[player] <= 0) continue;
			waveCrashPlayerDelay[player] -= Time.deltaTime;
		}
	}

	public static void PlayWaveCrash(Moon.Players player)
	{
		if (_instance.waveCrashes.Length == 0) return;
		if (!_instance.waveCrashPlayerDelay.ContainsKey(player)) _instance.waveCrashPlayerDelay.Add(player, 0);
		else if (_instance.waveCrashPlayerDelay[player] > 0) return;
		
		AudioClip clip = _instance.waveCrashes[Random.Range(0, _instance.waveCrashes.Length)];
		_instance.waveCrash.PlayOneShot(clip, _instance.waveCrashVolume);
		_instance.waveCrashPlayerDelay[player] = _instance.waveCrashCooldown;
    }

	public enum CrashSounds { Minor, TowerCollapse, CastleCollapse }
	public static void StateChangeAudio(CrashSounds soundType)
	{
		switch (soundType)
		{
			case CrashSounds.Minor: _instance.minorDmg.Play(); break;
			case CrashSounds.TowerCollapse: _instance.destroyTower.Play(); break;
			case CrashSounds.CastleCollapse: _instance.destroyCastle.Play(); break;
		}
		
	}

	public static void MoonCollision()
	{
		_instance.moonCollision.Play();
	}
}
