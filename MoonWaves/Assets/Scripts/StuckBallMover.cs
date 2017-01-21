using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckBallMover : MonoBehaviour {
    private Dictionary<GameObject, float> _ballsFadeTime = new Dictionary<GameObject, float>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!_ballsFadeTime.ContainsKey(collision.gameObject)) {
            _ballsFadeTime.Add(collision.gameObject, Time.realtimeSinceStartup + Random.Range(2f, 6f));
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (!_ballsFadeTime.ContainsKey(collision.gameObject)) {
            return;
        }
        if (Time.realtimeSinceStartup > _ballsFadeTime[collision.gameObject]) {
            StartCoroutine(BallFader(collision.gameObject));
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (!_ballsFadeTime.ContainsKey(collision.gameObject)) {
            return;
        }
        _ballsFadeTime.Remove(collision.gameObject);
    }

    private IEnumerator BallFader(GameObject ball) {
        Vector3 baseScale = ball.transform.localScale;
        _ballsFadeTime.Remove(ball);
        float endTime = Time.realtimeSinceStartup + 1f;
        while (Time.realtimeSinceStartup < endTime) {
            ball.transform.localScale *= 0.9f;
            yield return new WaitForEndOfFrame();
        }
        ball.gameObject.transform.position = (new Vector2(0f, 10f));
        float randomAngle = Random.value > 0.5f ? Random.Range(1f, Mathf.PI - 1f) : Random.Range(Mathf.PI + 1f, Mathf.PI * 2f - 1f);
        ball.gameObject.transform.position = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)) * 2.6f;


        ball.transform.localScale = baseScale;
    }
}
