using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	public TextMesh p1ReadyText;
	public TextMesh p2ReadyText;
	public SpriteRenderer p1AButton;
	public SpriteRenderer p2AButton;
	public int nextSceneIndex;

	private bool p1Ready;
	private bool p2Ready;

	void Start()
	{
		p1Ready = false;
		p2Ready = false;

		p1ReadyText.gameObject.SetActive(false);
		p2ReadyText.gameObject.SetActive(false);

		p1AButton.enabled = true;
		p2AButton.enabled = true;
	}

	void Update () {
		if (Input.GetAxis("Fire1") > 0)
		{
			p1Ready = true;
			p1ReadyText.gameObject.SetActive(true);
			p1AButton.enabled = false;
		}
		if (Input.GetAxis("Fire2") > 0)
		{
			p2Ready = true;
			p2ReadyText.gameObject.SetActive(true);
			p2AButton.enabled = false;
		}

		if (p1Ready && p2Ready) UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneIndex);
	}
}
