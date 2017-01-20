using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonMover : MonoBehaviour {
    public string InputAxisName;
    public float InitialPIAngle;

    public GameObject Anchor;
    public float Distance;

    public AnimationCurve SpeedCurve;
    public float BrakeFactor;
    public AnimationCurve BounceCurve;

    private float _curveDuration;

    private float _currentInertia;
    private float _currentAngle;

    private float _bouncing;
    private float _bounceTimer;

	// Use this for initialization
	void Start () {
	    _curveDuration = SpeedCurve[SpeedCurve.length - 1].time;
	    _currentAngle = InitialPIAngle * Mathf.PI;
	}
	
	// Update is called once per frame
	void Update () {
	    float horizontal = Input.GetAxis(InputAxisName);
	    if (Mathf.Abs(horizontal) > 0.1f) {
            UpdateInertia(horizontal, true);
        } else {
            UpdateInertia(horizontal, false);
        }
        UpdateAngle();
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

    private void UpdateAngle() {
        _currentAngle += Mathf.Sign(_currentInertia) * SpeedCurve.Evaluate(_currentInertia) * Time.deltaTime;
        if (_currentAngle >= Mathf.PI * 2f) {
            _currentAngle -= Mathf.PI * 2f;
        } else if (_currentAngle <= -Mathf.PI * 2f) {
            _currentAngle += Mathf.PI * 2f;
        }
    }

    private void SetPosition(float angle) {
        float xPos = Mathf.Cos(angle);
        float yPos = Mathf.Sin(angle);
        transform.position = new Vector3(xPos, yPos) * Distance;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.isTrigger) {
            return;
        }
        print("lol");
    }
}
