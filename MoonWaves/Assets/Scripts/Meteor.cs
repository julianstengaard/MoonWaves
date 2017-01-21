using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour {
    private Rigidbody2D _rigidbody;

    private Vector2 travelDir;

    void Start() {
        _rigidbody = GetComponent<Rigidbody2D>();
        travelDir = (Vector3.zero - transform.position).normalized;
    }

	void FixedUpdate () {
        _rigidbody.MovePosition(_rigidbody.position + travelDir * Time.deltaTime * 4f);
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        float explosionRadius = 3f;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(_rigidbody.transform.position, explosionRadius);
        //print(hitColliders.Length);
        for (int i = 0; i < hitColliders.Length; i++) {
            if (hitColliders[i].attachedRigidbody == null) {
                continue;
            }
            Vector2 dist = hitColliders[i].attachedRigidbody.position - (Vector2) _rigidbody.transform.position;
            float explosionStrength = Mathf.InverseLerp(explosionRadius, 0f, dist.magnitude);
            hitColliders[i].attachedRigidbody.AddForce(explosionStrength * dist.normalized * 300f);
        }
        Destroy(gameObject);
    }
}
