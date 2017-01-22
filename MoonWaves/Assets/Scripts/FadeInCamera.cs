using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class FadeInCamera : MonoBehaviour {

	private VignetteAndChromaticAberration script;

	// Use this for initialization
	void Awake() {
	    script = GetComponent<VignetteAndChromaticAberration>();
    }
	
	public void Fade(bool fadeIn, float duration)
	{
		script.intensity = fadeIn ? 1 : 0;
		script.blur = fadeIn ? 1 : 0;

		//Fade in/out
		HOTween.To(script, duration, "intensity", fadeIn ? 0 : 1, false, EaseType.EaseInCubic, 0f);
		HOTween.To(script, duration, "blur", fadeIn ? 0 : 1, false, EaseType.EaseInCubic, 0f);
	}
}