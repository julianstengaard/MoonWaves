using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterParticle : MonoBehaviour {

	//Static stuff
	private static bool newFrame;
	private static int[] waveCollisionCountBuffer = new int[50];
	private static int waveBufferIndex = 0;

	public static int waveCollisionCount
	{
		get
		{
			return waveCollisionCountBuffer[waveBufferIndex];
		}
	}
	public static int waveCollisionCountAvg
	{
		get
		{
			int sum = 0;
			for(int i = 0; i < waveCollisionCountBuffer.Length; i++)
			{
				sum += waveCollisionCountBuffer[i];
			}
			return sum / waveCollisionCountBuffer.Length;
		}
	}
	
	//non-static stuff
	public float waveHeightThreshold;

	private int frameCollisionCount;

	void Update()
	{
		newFrame = true;
    }

	void LateUpdate()
	{
		if (newFrame)
		{
			waveBufferIndex++;
			if (waveBufferIndex == waveCollisionCountBuffer.Length - 1) waveBufferIndex = 0;

			waveCollisionCountBuffer[waveBufferIndex] = 0;
			newFrame = false;
		}

		if (transform.position.sqrMagnitude > waveHeightThreshold) waveCollisionCountBuffer[waveBufferIndex]++;
		//waveCollisionCountBuffer[waveBufferIndex] += frameCollisionCount;
		frameCollisionCount = 0;
	}

	//private void OnCollisionStay2D(Collision2D collision)
	//{
	//	if (transform.position.sqrMagnitude > waveHeightThreshold) frameCollisionCount++;
	//}
}
