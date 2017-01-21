using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
    public GameObject LookTarget;

	// Update is called once per frame
	void Update () {
        transform.LookAt(transform.position + Vector3.forward, transform.position - LookTarget.transform.position);
	}
}