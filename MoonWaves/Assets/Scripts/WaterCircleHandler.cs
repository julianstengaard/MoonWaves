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
    public Moon[] ForceTargetMoon;

    private Rigidbody2D[] Circles;

    private int count; 

	// Use this for initialization
	void Start () {
        SpawnCircles(CirclesToSpawn);
	}

    private void SpawnCircles(int count) {
        Circles = new Rigidbody2D[CirclesToSpawn];

        for (int i = 0; i < count; i++) {
            float randomAngle = Random.value > 0.5f ? Random.Range(1f, Mathf.PI - 1f) : Random.Range(Mathf.PI + 1f, Mathf.PI * 2f - 1f);
            Rigidbody2D g = Instantiate(CirclePrefab, new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)) * 2.6f, Quaternion.identity, transform).GetComponent<Rigidbody2D>();
            Circles[i] = g;
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
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
            if (!ForceTargetMoon[i].Sucking) {
                continue;
            }
            Vector2 moonDir = ForceTargetMoon[i].transform.position - target.transform.position;
            moonForces += moonDir.normalized * MoonForceFalloff.Evaluate(Mathf.InverseLerp(0f, MoonForceRange, moonDir.magnitude)) * PullForceMoon;
        }

        bool outOfBounds = dirPlanet.magnitude > outOfBoundsDist;
        target.drag = outOfBounds ? 0 : oobParticleDrag;

        Vector2 planetForce = dirPlanet.normalized*PullForcePlanet*(outOfBounds ? oobPlanetForceMod : 1);

        target.AddForce(planetForce + (outOfBounds ? Vector2.zero : moonForces));
    }

	void OnDrawGizmos()
	{
#if UNITY_EDITOR
		if (UnityEditor.Selection.activeGameObject != gameObject) return;
#endif
		Gizmos.color = new Color(0, 1, 0, .25f);
		Gizmos.DrawSphere(ForceTargetPlanet.transform.position, outOfBoundsDist);
		foreach (var moon in ForceTargetMoon)
		{
			Gizmos.DrawSphere(moon.transform.position, MoonForceRange);
		}
	}
}
