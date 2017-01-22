using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour {

	[Header("Settings")]
	public float ouchDuration;
	public float faceDuration;
	public float facePositionDuration;

	[Header("References")]
	public BuildingHealth health;
	public SpriteRenderer face;
	
	public List<FaceState> faces;
	private int currentFaceIndex;

	[System.Serializable]
	public class FaceState
	{
		public float yPos;
		public Sprite[] happyFaces;
		public Sprite[] ouchFaces;
	}

	private float faceOuchTimer;
	private float faceTimer;
	private float facePositionTimer;

	void Start()
	{
		health.OnDamage += OnDamage;
	}

	void Update()
	{
		if (health.currentHealth <= 0) face.gameObject.SetActive(false);

		UpdateFace();
		UpdateFacePosition();
	}

	private void UpdateFace()
	{
		if (Time.time < faceTimer || Time.time < faceOuchTimer) return;
		faceTimer = Time.time + faceDuration + Random.Range(-.5f, .5f);

		Vector3 newSca = new Vector3(Random.Range(0, 1) == 1 ? 1 : -1, 1, 1);
		face.transform.localScale = newSca;

		Sprite[] options = faces[currentFaceIndex].happyFaces;
		face.sprite = options[Random.Range(0, options.Length)];
	}

	private void UpdateFacePosition()
	{
		if (Time.time < facePositionTimer) return;
		facePositionTimer = Time.time + facePositionDuration;

		Vector3 newRot = new Vector3(0, 0, Random.Range(-6f, 6f));
		face.transform.localRotation = Quaternion.Euler(newRot);

		Vector3 newPos = new Vector3(
			Random.Range(-.02f, .02f),
			faces[currentFaceIndex].yPos + Random.Range(-.02f, .02f),
			0);
		face.transform.localPosition = newPos;
	}

	private void OnDamage(GameObject collider, int stateIndex)
	{
		if (Time.time < faceOuchTimer) return;
		faceOuchTimer = Time.time + ouchDuration;

		currentFaceIndex = stateIndex;
		if (transform.InverseTransformPoint(collider.transform.position).x > 0)
		{
			face.transform.localScale = new Vector3(-1, 1, 1);
		}
		else
		{
			face.transform.localScale = Vector3.one;
		}

		Sprite[] options = faces[currentFaceIndex].ouchFaces;
		face.sprite = options[Random.Range(0, options.Length)];
	}
}
