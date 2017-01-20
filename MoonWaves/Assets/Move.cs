using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    private Rigidbody2D _rigidbody2D;

    void Start() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    } 
	// Update is called once per frame
	void Update () {
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	    targetPosition.z = 0f;
        _rigidbody2D.MovePosition(targetPosition);

    }
}
