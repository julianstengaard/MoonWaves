using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckBallMover : MonoBehaviour {
    private Dictionary<GameObject, float> _ballsFadeTime = new Dictionary<GameObject, float>();
	public float fizzleTime;

	public void RemoveBall(GameObject ball)
	{
		if (_ballsFadeTime.ContainsKey(ball)) return;
		_ballsFadeTime.Add(ball, Time.realtimeSinceStartup + fizzleTime);
		StartCoroutine(BallFader(ball));
	}
    private IEnumerator BallFader(GameObject ball) {
        Vector3 baseScale = new Vector3(0.03f, 0.03f, 0.03f);
        float endTime = Time.realtimeSinceStartup + 1f;
        while (Time.realtimeSinceStartup < endTime) {
            ball.transform.localScale *= 0.9f;
            yield return new WaitForEndOfFrame();
        }
        ball.gameObject.transform.position = (new Vector2(0f, 10f));
        float randomAngle = Random.value > 0.5f ? Random.Range(1f, Mathf.PI - 1f) : Random.Range(Mathf.PI + 1f, Mathf.PI * 2f - 1f);
        ball.gameObject.transform.position = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)) * 2.6f;

        _ballsFadeTime.Remove(ball);

        ball.transform.localScale = baseScale;
    }
}
