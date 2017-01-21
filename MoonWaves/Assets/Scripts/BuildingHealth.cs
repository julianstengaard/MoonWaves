using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHealth : MonoBehaviour {

	public Moon.Players player;
	public float currentHealth { get; private set; }

	[Header("Settings")]
	[SerializeField]
	private float health;
	public SpriteRenderer[] states;
	public SpriteRenderer destroyed;
	public AnimationCurve collapseCurve;
	public int HitThreshold1;
	public int HitThreshold2;
	public int HitThreshold3;

	private bool isDead;
	private int spriteIndex;
	private float nextChange { get { return health - (health * (float)(spriteIndex + 1) / (float)(states.Length)); } }

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
		currentHealth = health;
		spriteIndex = 0;
		isDead = false;
	}

	void Update()
	{
		healthDebug = currentHealth;

		if (Input.GetKeyDown(KeyCode.Space))
		{
			HitCount = 0;
			HitCountOverThreshold1 = 0;
			HitCountOverThreshold2 = 0;
			HitCountOverThreshold3 = 0;
			currentHealth = health;
		}
		UpdateSprite();
	}

	private void UpdateSprite()
	{
		if (currentHealth < nextChange)
		{
			
			states[spriteIndex].gameObject.SetActive(false);
			spriteIndex++;

			if (spriteIndex < states.Length)
			{
				states[spriteIndex].gameObject.SetActive(true);
			}
			else if (!isDead)
			{
				isDead = true;
				StartCoroutine(DestroyRoutine());
			}
		}
	}

	private IEnumerator DestroyRoutine()
	{
		destroyed.gameObject.SetActive(true);

		float startTimer = Time.time;
		float endTimer = startTimer + 2;
		float progress = 0;
		while (progress < 1)
		{
			progress = Mathf.InverseLerp(startTimer, endTimer, Time.time);
			destroyed.transform.localScale = Vector3.one * (1 - progress);
			yield return null;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		HitCount++;
		if (collision.attachedRigidbody.velocity.sqrMagnitude > _hitThreshold1Sqr)
		{
			HitCountOverThreshold1++;
			currentHealth -= 0.05f;
			AudioManager.PlayWaveCrash(player);
		}
		if (collision.attachedRigidbody.velocity.sqrMagnitude > _hitThreshold2Sqr)
		{
			HitCountOverThreshold2++;
			currentHealth -= 0.2f;
		}
		if (collision.attachedRigidbody.velocity.sqrMagnitude > _hitThreshold3Sqr)
		{
			HitCountOverThreshold3++;
			currentHealth -= 1f;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		HitCount++;
		if (collision.relativeVelocity.sqrMagnitude > _hitThreshold1Sqr)
		{
			HitCountOverThreshold1++;
		}
		if (collision.relativeVelocity.sqrMagnitude > _hitThreshold2Sqr)
		{
			HitCountOverThreshold2++;
		}
		if (collision.relativeVelocity.sqrMagnitude > _hitThreshold3Sqr)
		{
			HitCountOverThreshold3++;
		}
		//AverageHitSpeed = (AverageHitSpeed * HitCount + collision.relativeVelocity.magnitude)/ (float) (HitCount + 1);
	}
}
