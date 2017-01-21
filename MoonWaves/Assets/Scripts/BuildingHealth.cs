using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHealth : MonoBehaviour {

	//public float currentHealth { get; private set; }

	//[Header("Settings")]
	//[SerializeField]
	//private float health;
	//public Sprite[] states;
	//public PolygonCollider2D col;

	//[Header("Debug")]
	//public int HitCount;
	//public int HitCountOverThreshold1;
	//public int HitCountOverThreshold2;
	//public int HitCountOverThreshold3;
	//public int HitThreshold1;
	//public int HitThreshold2;
	//public int HitThreshold3;

	//void Start()
	//{
	//	_hitThreshold1Sqr = HitThreshold1 * HitThreshold1;
	//	_hitThreshold2Sqr = HitThreshold2 * HitThreshold2;
	//	_hitThreshold3Sqr = HitThreshold3 * HitThreshold3;
	//}

	//void Update()
	//{
	//	if (Input.GetKeyDown(KeyCode.Space))
	//	{
	//		HitCount = 0;
	//		HitCountOverThreshold1 = 0;
	//		HitCountOverThreshold2 = 0;
	//		HitCountOverThreshold3 = 0;
	//		currentHealth = 100f;
	//	}
	//}

	//private void OnTriggerEnter2D(Collider2D collision)
	//{
	//	HitCount++;
	//	if (collision.attachedRigidbody.velocity.sqrMagnitude > _hitThreshold1Sqr)
	//	{
	//		HitCountOverThreshold1++;
	//		Health -= 0.05f;
	//		AudioManager.PlayWaveCrash(player);
	//	}
	//	if (collision.attachedRigidbody.velocity.sqrMagnitude > _hitThreshold2Sqr)
	//	{
	//		HitCountOverThreshold2++;
	//		Health -= 0.2f;
	//	}
	//	if (collision.attachedRigidbody.velocity.sqrMagnitude > _hitThreshold3Sqr)
	//	{
	//		HitCountOverThreshold3++;
	//		Health -= 1f;
	//	}
	//}
}
