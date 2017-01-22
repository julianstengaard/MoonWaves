using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroMovie : MonoBehaviour {

	public int nextSceneIndex;
	public float holdToSkipTime;
	public float textRemainTime;
	public GameObject skipText;

	private float skipTimer;
	private float disappearTimer;

	void Update()
	{
		if (Input.GetAxis("Fire1") <= 0 && Input.GetAxis("Fire2") <= 0)
		{
			skipTimer = Time.time;

			if (Time.time > disappearTimer) skipText.SetActive(false);
		}
		else
		{
			skipText.SetActive(true);
			disappearTimer = Time.time + textRemainTime;

			if (Time.time > skipTimer + holdToSkipTime)
			{
				UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneIndex);
			}
		}
	}
}
