using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour {
	public enum Players { PlayerOne, PlayerTwo }

    public string InputAxisName;
    public string InputSuckName;

    public float InitialPIAngle;

    public GameObject Anchor;
    public float Distance;

    public AnimationCurve SpeedCurve;
    public float BrakeFactor;
    public AnimationCurve BounceCurve;

    private float _curveDuration;

    private float _currentInertia;
    private float _currentAngle;

    private bool _bouncing;
    private float _bounceDirection;
    private float _bounceTimer;

    private bool _sucking;

    public float CurrentAngle {
        get { return _currentAngle; }
    }

    public bool Sucking {
        get { return _sucking; }
    }

    // Use this for initialization
	void Start () {
	    _curveDuration = SpeedCurve[SpeedCurve.length - 1].time;
	    _currentAngle = InitialPIAngle * Mathf.PI;
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetAxis(InputSuckName) > 0f) {
	        _sucking = true;
	    } else {
	        _sucking = false;
	    }

	    if (_bouncing) {
	        _currentAngle += BounceCurve.Evaluate(_bounceTimer) * _bounceDirection * Time.deltaTime;
	        _bounceTimer += Time.deltaTime;
	        if (_bounceTimer >= BounceCurve[BounceCurve.length - 1].time) {
	            _bouncing = false;
	        }
	    } else {
	        float horizontal = Input.GetAxis(InputAxisName);
	        if (Mathf.Abs(horizontal) > 0.1f) {
	            UpdateInertia(horizontal, true);
	        } else {
	            UpdateInertia(horizontal, false);
	        }
	        UpdateAngle();
	    }
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
        } else if (_currentAngle <= 0f) {
            _currentAngle += Mathf.PI * 2f;
        }
    }

    private void SetPosition(float angle) {
        float xPos = Mathf.Cos(angle);
        float yPos = Mathf.Sin(angle);
        transform.position = new Vector3(xPos, yPos) * Distance + Anchor.transform.position;
    }

    public void StartBounce(Moon otherMoon) {
        if (_bouncing) {
            return;
        }
        float dir = 0f;
        if (otherMoon.CurrentAngle > _currentAngle) {
            dir = -1f;
            if (Mathf.Abs(otherMoon.CurrentAngle - _currentAngle) > Mathf.PI) {
                dir = -dir;
            }
        } else {
            dir = 1f;
        }
        SetBounceDirectionAndStart(dir);
        otherMoon.SetBounceDirectionAndStart(-dir);
    }

    public void SetBounceDirectionAndStart(float dir) {
        _bounceDirection = dir;
        _bouncing = true;
        _bounceTimer = 0f;
        _currentInertia = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!_bouncing) {
			Moon moon = collision.gameObject.GetComponent<Moon>();
			if (moon == null) return;

            StartBounce(collision.gameObject.GetComponent<Moon>());
        }
    }
}
