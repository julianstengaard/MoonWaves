using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonMover : MonoBehaviour {
    public GameObject Anchor;
    public float Distance;

    public AnimationCurve SpeedCurve;
    public float BrakeFactor;

    private float _curveDuration;

    private float _currentInertia;
    private float _currentAngle;

	// Use this for initialization
	void Start () {
	    _curveDuration = SpeedCurve[SpeedCurve.length - 1].time;
	}
	
	// Update is called once per frame
	void Update () {
	    float horizontal = Input.GetAxis("Horizontal");
	    if (Mathf.Abs(horizontal) > 0.1f) {
            UpdateInertia(horizontal, true);
        } else {
            UpdateInertia(horizontal, false);
        }
        UpdateMovement();
        SetPosition(_currentAngle);
    }

    private void UpdateInertia(float direction, bool moving) {
        if (moving) {
            float accelerationFactor = (Mathf.Sign(_currentInertia) != Mathf.Sign(direction)) ? BrakeFactor : 1f;
            _currentInertia = Mathf.Min(_curveDuration, Mathf.Max(-_curveDuration, _currentInertia + direction * accelerationFactor * Time.deltaTime));
        } else {
            float previousDirection = Mathf.Sign(_currentInertia);
            _currentInertia += -Mathf.Sign(_currentInertia) * Time.deltaTime * BrakeFactor;
            if (previousDirection != Mathf.Sign(_currentInertia)) {
                _currentInertia = 0f;
            }
        }
    }

    private void UpdateMovement() {
        _currentAngle += Mathf.Sign(_currentInertia) * SpeedCurve.Evaluate(_currentInertia) * Time.deltaTime;
    }

    private void SetPosition(float angle) {
        float xPos = Mathf.Cos(angle);
        float yPos = Mathf.Sin(angle);
        transform.position = new Vector3(xPos, yPos) * Distance;
    }
}
