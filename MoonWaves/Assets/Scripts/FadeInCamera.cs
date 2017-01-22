using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class FadeInCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    VignetteAndChromaticAberration script = GetComponent<VignetteAndChromaticAberration>();
	    HOTween.To(script, 1.5f, "intensity", 0f, false, EaseType.EaseInCubic, 0f);
        HOTween.To(script, 1.5f, "blur", 0f, false, EaseType.EaseInCubic, 0f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
