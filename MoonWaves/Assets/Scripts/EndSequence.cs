using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Holoville.HOTween;

public class EndSequence : MonoBehaviour {

	public float angleDelta;
	public float rotateSpeed;
	public Transform victoryText;
	public ParticleSystem p1Fireworks;
	public ParticleSystem p2Fireworks;
	public TextMesh playerText;

	void Awake()
	{
		victoryText.gameObject.SetActive(false);
	}

	void Update()
	{
		if (GameManager.currentState != GameManager.LevelStates.End)
		{
			if (Input.GetKeyDown(KeyCode.V))
			{
				GameManager.SetState(GameManager.LevelStates.End);
				RunEndSequence(Moon.Players.PlayerOne);
			}
			return;
		}

		float angle = Mathf.Sin(Time.time * rotateSpeed) * angleDelta;
		Debug.Log(angle);
		victoryText.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
	}

	public void RunEndSequence(Moon.Players victor)
	{
		victoryText.gameObject.SetActive(true);
		StartCoroutine(EndRoutine(victor));
	}

	private IEnumerator EndRoutine(Moon.Players victor)
	{
		switch (victor)
		{
			case Moon.Players.PlayerOne:
				{
					if (p1Fireworks != null)
					{
						p1Fireworks.Play();
						playerText.text = "Blue";
						playerText.color = Color.blue;
					}
				}
				break;
			case Moon.Players.PlayerTwo:
				{
					if (p2Fireworks != null)
					{
						p2Fireworks.Play();
						playerText.text = "Red";
						playerText.color = Color.red;
					}
				}
				break;
		}

		victoryText.transform.localScale = Vector3.zero;
		HOTween.To(victoryText.transform, 1.5f, "localScale", Vector3.one, false, EaseType.EaseInOutQuad, 0f);
		

		yield return new WaitForSeconds(5);

		GameManager.SetState(GameManager.LevelStates.Menu);
	}
}
