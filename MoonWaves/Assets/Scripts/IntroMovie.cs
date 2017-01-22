using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroMovie : MonoBehaviour {

	public int nextSceneIndex;
	public float holdToSkipTime;
	public float textRemainTime;
	public GameObject skipText;
	public MovieTexture movie;

	private float skipTimer;
	private float disappearTimer;
	private AudioSource source;

	void Awake()
	{
		source = GetComponent<AudioSource>();
	}

	void Start()
	{
		source.Play();
		movie.Play();
	}

	void Update()
	{
		if (!source.isPlaying) Continue();

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
				Continue();
			}
		}
	}

	private void Continue()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneIndex);
	}
}
