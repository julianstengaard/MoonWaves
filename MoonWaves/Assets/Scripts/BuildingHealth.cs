using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHealth : MonoBehaviour {

	public delegate void DamageHandler(GameObject collider, int stateIndex);
	public event DamageHandler OnDamage;

	public Moon.Players player;
	public float currentHealth { get; private set; }

	[Header("Settings")]
	public float maxHealth;
	public SpriteRenderer[] states;
	public SpriteRenderer destroyed;
	public AnimationCurve collapseCurve;
	public int HitThreshold1;
	public int HitThreshold2;
	public int HitThreshold3;
	public StuckBallMover stuckBallMover;
	public ParticleSystem collapseParticles;
	public ParticleSystem destroyParticles;

	private bool isDead;
	private int spriteIndex;
	private float nextChange { get { return maxHealth - (maxHealth * (float)(spriteIndex + 1) / (float)(states.Length)); } }

	[Header("Debug")]
	public float healthDebug;
	public int HitCount;
	public int HitCountOverThreshold1;
	public int HitCountOverThreshold2;
	public int HitCountOverThreshold3;

	private float _hitThreshold1Sqr;
	private float _hitThreshold2Sqr;
	private float _hitThreshold3Sqr;

	void Start()
	{
		_hitThreshold1Sqr = HitThreshold1 * HitThreshold1;
		_hitThreshold2Sqr = HitThreshold2 * HitThreshold2;
		_hitThreshold3Sqr = HitThreshold3 * HitThreshold3;
		currentHealth = maxHealth;
		spriteIndex = 0;
		isDead = false;
	}

	void Update()
	{
		healthDebug = currentHealth;

		//if (Input.GetKeyDown(KeyCode.Space))
		//{
		//	HitCount = 0;
		//	HitCountOverThreshold1 = 0;
		//	HitCountOverThreshold2 = 0;
		//	HitCountOverThreshold3 = 0;
		//	currentHealth = maxHealth;
		//}
	}

	private void UpdateSprite()
	{
		if (spriteIndex >= states.Length) return;

		if (currentHealth < nextChange)
		{
			states[spriteIndex].gameObject.SetActive(false);
			spriteIndex++;

			AudioManager.StateChangeAudio(AudioManager.CrashSounds.Minor);
			collapseParticles.Play();

			if (spriteIndex < states.Length)
			{
				states[spriteIndex].gameObject.SetActive(true);
			}
			else if (!isDead)
			{
				isDead = true;
				if (GetComponent<Castle>() != null) AudioManager.StateChangeAudio(AudioManager.CrashSounds.CastleCollapse);
				else AudioManager.StateChangeAudio(AudioManager.CrashSounds.TowerCollapse);
				destroyParticles.Play();
				StartCoroutine(DestroyRoutine());
			}
		}
	}

	private IEnumerator DestroyRoutine()
	{
		destroyed.gameObject.SetActive(true);

		float startTimer = Time.time;
		float endTimer = startTimer + 3.5f;
		float progress = 0;
		while (progress < 1)
		{
			progress = Mathf.InverseLerp(startTimer, endTimer, Time.time);
			destroyed.transform.localScale = Vector3.one * collapseCurve.Evaluate(progress);
			yield return null;
		}
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		HitCount++;
		if (collider.attachedRigidbody.velocity.sqrMagnitude > _hitThreshold1Sqr)
		{
			HitCountOverThreshold1++;
			currentHealth -= 0.05f;
		}
		if (collider.attachedRigidbody.velocity.sqrMagnitude > _hitThreshold2Sqr)
		{
			HitCountOverThreshold2++;
			currentHealth -= 0.2f;

			AudioManager.PlayWaveCrash(player);
		}
		if (collider.attachedRigidbody.velocity.sqrMagnitude > _hitThreshold3Sqr)
		{
			HitCountOverThreshold3++;
			currentHealth -= 1f;
		}

		UpdateSprite();
		if (OnDamage != null) OnDamage(collider.gameObject, spriteIndex);
		stuckBallMover.RemoveBall(collider.gameObject);
	}
}
