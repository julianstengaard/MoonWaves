using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour {
	public enum Players { PlayerOne, PlayerTwo }

    public string InputAxisName;
    public string InputSuckName;
	public Animator anim;

    public float InitialPIAngle;

    public GameObject Anchor;
    public float Distance;

	public float speed;
	public float suckSpeed;
    public AnimationCurve SpeedCurve;
    public float BrakeFactor;
    
    public AnimationCurve BounceCurve;

    private float _curveDuration;

    private float _currentInertia;
    private float _currentBounceInertia;
    private float _currentAngle;
    
    private bool _bouncing;

    private bool _sucking;

	private bool _canSuck
	{
		get
		{
			return anim.GetCurrentAnimatorStateInfo(0).IsName("moonFaceSuck") &&
				GameManager.currentState == GameManager.LevelStates.Battle;
		}
	}

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
			anim.SetBool("suckFromEarth", true);
	    } else {
			anim.SetBool("suckFromEarth", false);
		}

		_sucking = _canSuck;

	    if (_bouncing) {
            UpdateInertia(_currentBounceInertia, false);
            //_currentAngle += BounceCurve.Evaluate(_bounceTimer) * _bounceDirection * Time.deltaTime;
	        _bouncing = false;
        } else {
	        float horizontal = Input.GetAxis(InputAxisName);
	        if (Mathf.Abs(horizontal) > 0.1f) {
	            UpdateInertia(horizontal, true);
	        } else {
	            UpdateInertia(horizontal, false);
	        }
        }
        UpdateAngle();
        SetPosition(_currentAngle);
    }

    private void UpdateInertia(float direction, bool moving) {
        float previousBounceDir = Mathf.Sign(_currentBounceInertia);
        _currentBounceInertia += -Mathf.Sign(_currentBounceInertia) * Time.deltaTime;
        if (previousBounceDir != Mathf.Sign(_currentBounceInertia)) {
            _currentBounceInertia = 0f;
        }

        if (moving) {
            float accelerationFactor = (Mathf.Sign(_currentInertia) != Mathf.Sign(direction)) ? GetCurrentBrakeFactor() : 1f;
            _currentInertia = Mathf.Min(_curveDuration, Mathf.Max(-_curveDuration, _currentInertia + direction * accelerationFactor * Time.deltaTime));
        } else {
            float previousDirection = Mathf.Sign(_currentInertia);
            _currentInertia += -Mathf.Sign(_currentInertia) * Time.deltaTime * GetCurrentBrakeFactor();
            if (previousDirection != Mathf.Sign(_currentInertia)) {
                _currentInertia = 0f;
            }
        }
    }

    private void UpdateAngle() {
        _currentAngle += Mathf.Sign(_currentInertia) * SpeedCurve.Evaluate(_currentInertia) * (_sucking ? suckSpeed : speed) * Time.deltaTime;
        _currentAngle += Mathf.Sign(_currentBounceInertia) * BounceCurve.Evaluate(_currentBounceInertia) * Time.deltaTime;

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
            _bouncing = false;
            return;
        }
        float dir = 0f;
        if (otherMoon.CurrentAngle > _currentAngle) {
            dir = -1f;
            //Crossing wrap point?
            if (Mathf.Abs(otherMoon.CurrentAngle - _currentAngle) > Mathf.PI) {
                //Other moon is "under"
                if (otherMoon.CurrentAngle > Mathf.PI) {
                    dir = 1f;
                }
            }
        } else {
            dir = 1f;
        }
        float otherInertia = otherMoon._currentInertia * otherMoon.GetCurrentUsedSpeed();
        float myInertia = _currentInertia * GetCurrentUsedSpeed();
        SetBounceDirectionAndStart(dir, otherInertia);
        otherMoon.SetBounceDirectionAndStart(-dir, myInertia);
    }

    public float GetCurrentUsedSpeed() {
        return (_sucking ? suckSpeed : speed) / 2f;
    }
    public float GetCurrentBrakeFactor() {
        return (_sucking ? BrakeFactor/5f : BrakeFactor);
    }

    public void SetBounceDirectionAndStart(float dir, float otherInertia) {
        _currentAngle += dir * 0.02f;
        _bouncing = true;
        _currentInertia = otherInertia;
        _currentBounceInertia = otherInertia;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!_bouncing) {
			Moon moon = collision.gameObject.GetComponent<Moon>();
			if (moon == null) return;

			AudioManager.MoonCollision();
            StartBounce(collision.gameObject.GetComponent<Moon>());
        }
    }
}
