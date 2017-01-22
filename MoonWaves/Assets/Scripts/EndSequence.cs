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
	public PauseMenu menu;

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

			if (Input.GetKeyDown(KeyCode.B))
			{
				GameManager.SetState(GameManager.LevelStates.End);
				RunEndSequence(Moon.Players.PlayerTwo);
			}
			return;
		}

		float angle = Mathf.Sin(Time.time * rotateSpeed) * angleDelta;
		victoryText.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
	}

	public void RunEndSequence(Moon.Players loser)
	{
		victoryText.gameObject.SetActive(true);
		StartCoroutine(EndRoutine(loser));
	}

	private IEnumerator EndRoutine(Moon.Players loser)
	{
		switch (loser)
		{
			case Moon.Players.PlayerOne:
				{
					playerText.text = "Red";
					playerText.color = Color.red;
					
					if (p2Fireworks != null)
					{
						p2Fireworks.Play();
					}
				}
				break;
			case Moon.Players.PlayerTwo:
				{
					playerText.text = "Blue";
					playerText.color = Color.blue;

					if (p1Fireworks != null)
					{
						p1Fireworks.Play();
					}
				}
				break;
		}

		victoryText.transform.localScale = Vector3.zero;
		HOTween.To(victoryText.transform, 1.5f, "localScale", Vector3.one, false, EaseType.EaseInOutQuad, 0f);
		

		yield return new WaitForSeconds(5);

		menu.SetPauseState(true);
	}
}
