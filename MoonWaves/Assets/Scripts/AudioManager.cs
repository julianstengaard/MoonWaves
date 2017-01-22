using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

	private static AudioManager _instance;

	[Header("General Settings")]
	public AudioMixerSnapshot defaultSnapshot;
	public AudioMixerSnapshot menuSnapshot;

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
	public AudioClip[] waveCrashes;
	public float waveCrashVolume;
	public float waveCrashCooldown;
	public AudioClip[] castleScreams;
	public float castleScreamVolume;
	public float castleScreamCooldown;

	private Dictionary<Moon.Players, float> castleScreamPlayerDelay;
	private Dictionary<Moon.Players, float> waveCrashPlayerDelay;
	private int previousCastleScreamIndex;

	[Header("Events References")]
	public AudioSource waveCrash;
	public AudioSource moonCollision;
	public AudioSource minorDmg;
	public AudioSource destroyTower;
	public AudioSource destroyCastle;
	public AudioSource castleScream;

	void Awake()
	{
		_instance = this;
		castleScreamPlayerDelay = new Dictionary<Moon.Players, float>();
		waveCrashPlayerDelay = new Dictionary<Moon.Players, float>();
	}

	void Start()
	{
		GameManager.OnStateChange += OnStateChange;

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
		UpdateDelays();

	}

	private void OnStateChange(GameManager.LevelStates oldState, GameManager.LevelStates newState)
	{
		if (newState == GameManager.LevelStates.Menu) menuSnapshot.TransitionTo(.5f);
		else defaultSnapshot.TransitionTo(.5f);
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

	private void UpdateDelays()
	{
		List<Moon.Players> players = new List<Moon.Players>(castleScreamPlayerDelay.Keys);
		foreach (Moon.Players player in players)
		{
			if (castleScreamPlayerDelay[player] <= 0) continue;
			castleScreamPlayerDelay[player] -= Time.deltaTime;
		}

		players.Clear();
		players.AddRange(waveCrashPlayerDelay.Keys);
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

	public static void PlayCastleScream(Moon.Players player)
	{
		if (_instance.castleScreams.Length == 0) return;
		if (!_instance.castleScreamPlayerDelay.ContainsKey(player)) _instance.castleScreamPlayerDelay.Add(player, 0);
		else if (_instance.castleScreamPlayerDelay[player] > 0) return;

		int newClipIndex = Random.Range(0, _instance.castleScreams.Length);
		while (newClipIndex == _instance.previousCastleScreamIndex)
		{
			newClipIndex = Random.Range(0, _instance.castleScreams.Length);
		}
		_instance.previousCastleScreamIndex = newClipIndex;

		AudioClip clip = _instance.castleScreams[newClipIndex];
		_instance.castleScream.PlayOneShot(clip, _instance.castleScreamVolume);
		_instance.castleScreamPlayerDelay[player] = _instance.castleScreamCooldown;
	}
}
