using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCircleHandler : MonoBehaviour {
    public int CirclesToSpawn;
    public GameObject CirclePrefab;

    public GameObject ForceTarget;

    public float PullForce;

    private Rigidbody2D[] Circles;

    private int count; 

	// Use this for initialization
	void Start () {
        SpawnCircles(CirclesToSpawn);
	}
	
	// Update is called once per frame
	void Update () {
	    count++;
	    if (count%1 == 0) {
	        for (int i = 0; i < CirclesToSpawn; i++) {
	            ApplyForces(Circles[i]);
	        }
	    }
	}

    private void ApplyForces(Rigidbody2D target) {
        Vector2 dir = ForceTarget.transform.position - target.transform.position;
        target.AddForce(dir * PullForce);
    }

    private void SpawnCircles(int count) {
        Circles = new Rigidbody2D[CirclesToSpawn];

        for (int i = 0; i < count; i++) {
            Rigidbody2D g = Instantiate(CirclePrefab, new Vector2(Random.Range(-10f, 10f), Random.Range(-5f, 5f)), Quaternion.identity).GetComponent<Rigidbody2D>();
            Circles[i] = g;
        }
    }
}
