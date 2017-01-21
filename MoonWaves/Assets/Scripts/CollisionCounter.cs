using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCounter : MonoBehaviour {
    public TextMesh Text;

    public float HeightThreshold;
    private float _heightThresholdSqr;

    public static CollisionCounter Singleton;

    private int _collisionCounter;

    public void Start() {
        Singleton = this;
        _heightThresholdSqr = HeightThreshold*HeightThreshold;
    }

    public void AddCollision(float heightSqr) {
        if (heightSqr >= _heightThresholdSqr) {
            _collisionCounter++;
        }
    }

    private void LateUpdate() {
        Text.text = _collisionCounter.ToString();
        _collisionCounter = 0;
    }


}
