using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCircleHandler : MonoBehaviour {
    
    [Header("Settings")]
    public int CirclesToSpawn;
    public float PullForcePlanet;
    public float PullForceMoon;
    public float outOfBoundsDist;

    public float MoonForceRange;
    public AnimationCurve MoonForceFalloff; 

    [Header("OutOfBounds")]
    public float oobPlanetForceMod;
    public float oobParticleDrag;

    [Header("References")]
    public GameObject CirclePrefab;
    public GameObject ForceTargetPlanet;
    public GameObject[] ForceTargetMoon;

    private Rigidbody2D[] Circles;

    private int count; 

	// Use this for initialization
	void Start () {
        SpawnCircles(CirclesToSpawn);
	}

    private void SpawnCircles(int count) {
        Circles = new Rigidbody2D[CirclesToSpawn];

        for (int i = 0; i < count; i++) {
            Rigidbody2D g = Instantiate(CirclePrefab, new Vector2(Random.Range(-10f, 10f), Random.Range(-5f, 5f)), Quaternion.identity, transform).GetComponent<Rigidbody2D>();
            Circles[i] = g;
        }
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
        Vector2 dirPlanet = ForceTargetPlanet.transform.position - target.transform.position;
        Vector2 moonForces = Vector2.zero;
        for (int i = 0; i < ForceTargetMoon.Length; i++) {
            Vector2 moonDir = ForceTargetMoon[i].transform.position - target.transform.position;
            moonForces += moonDir.normalized * MoonForceFalloff.Evaluate(Mathf.InverseLerp(0f, MoonForceRange, moonDir.magnitude)) * PullForceMoon;
        }

        bool outOfBounds = dirPlanet.magnitude > outOfBoundsDist;
        target.drag = outOfBounds ? 0 : oobParticleDrag;
        


        target.AddForce(
            dirPlanet.normalized * PullForcePlanet * (outOfBounds ? oobPlanetForceMod : 1) +        //Planet force
            moonForces                  //Moon force
            );
    }

    void OnDrawGizmos() {
        Gizmos.color = new Color(0, 1, 0, .25f);
        Gizmos.DrawSphere(ForceTargetPlanet.transform.position, outOfBoundsDist);
        foreach (var moon in ForceTargetMoon) {
            Gizmos.DrawSphere(moon.transform.position, MoonForceRange);
        }
    }
}
