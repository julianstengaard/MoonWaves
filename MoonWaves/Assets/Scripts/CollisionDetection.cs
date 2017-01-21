using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour {
    private void OnCollisionStay2D(Collision2D collision) {
        CollisionCounter.Singleton.AddCollision(transform.position.sqrMagnitude);
    }
}
