using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour {
    public GameObject MeteorPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.P)) {
	        CreateMeteor();
	    }
	}

    private void CreateMeteor() {
        float randomAngle = Random.value > 0.5f ? Random.Range(1f, Mathf.PI - 1f) : Random.Range(Mathf.PI + 1f, Mathf.PI * 2f - 1f);
        Instantiate(MeteorPrefab, new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)) * 10f, Quaternion.identity, transform);
    }
}
