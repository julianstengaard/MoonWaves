using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour {
	public Moon.Players player;
	public TextMesh Text;
    public float Health = 100f;

    public int HitCount;
    public int HitCountOverThreshold1;
    public int HitCountOverThreshold2;
    public int HitCountOverThreshold3;
    public int HitThreshold1;
    public int HitThreshold2;
    public int HitThreshold3;

    private float _hitThreshold1Sqr;
    private float _hitThreshold2Sqr;
    private float _hitThreshold3Sqr;

    void Start() {
        _hitThreshold1Sqr = HitThreshold1*HitThreshold1;
        _hitThreshold2Sqr = HitThreshold2*HitThreshold2;
        _hitThreshold3Sqr = HitThreshold3*HitThreshold3;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            HitCount = 0;
            HitCountOverThreshold1 = 0;
            HitCountOverThreshold2 = 0;
            HitCountOverThreshold3 = 0;
            Health = 100f;
        }
        Text.text = Health.ToString("F1");
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        HitCount++;
        if (collision.attachedRigidbody.velocity.sqrMagnitude > _hitThreshold1Sqr) {
            HitCountOverThreshold1++;
            Health -= 0.05f;
			AudioManager.PlayWaveCrash(player);
		}
        if (collision.attachedRigidbody.velocity.sqrMagnitude > _hitThreshold2Sqr) {
            HitCountOverThreshold2++;
            Health -= 0.2f;
		}
        if (collision.attachedRigidbody.velocity.sqrMagnitude > _hitThreshold3Sqr) {
            HitCountOverThreshold3++;
            Health -= 1f;
		}
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        HitCount++;
        if (collision.relativeVelocity.sqrMagnitude > _hitThreshold1Sqr) {
            HitCountOverThreshold1++;
        }
        if (collision.relativeVelocity.sqrMagnitude > _hitThreshold2Sqr) {
            HitCountOverThreshold2++;
        }
        if (collision.relativeVelocity.sqrMagnitude > _hitThreshold3Sqr) {
            HitCountOverThreshold3++;
        }
        //AverageHitSpeed = (AverageHitSpeed * HitCount + collision.relativeVelocity.magnitude)/ (float) (HitCount + 1);
    }
}
