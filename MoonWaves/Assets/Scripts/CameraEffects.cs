using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class CameraEffects : MonoBehaviour {
    public Camera Camera2;

    private VignetteAndChromaticAberration vignetteAndChromaticAberration;
    private Tweener chromaTweener = null;
    private Tweener shakeTweener = null;
    private Tweener zoomTweener1 = null;
    private Tweener zoomTweener2 = null;

    // Use this for initialization
    void Start () {
	    vignetteAndChromaticAberration = GetComponent<VignetteAndChromaticAberration>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Q)) {
	        FireChromaEffect();
	    }
        if (Input.GetKeyDown(KeyCode.W)) {
            FireShakeEffect();
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            FireZoomEffect();
        }
    }

    public void FireChromaEffect() {
        if (chromaTweener != null) {
            chromaTweener.Kill();
            ResetChroma();
        }
        TweenParms parms = new TweenParms();
        parms.Prop("chromaticAberration", 0f); // Position tween
        parms.Ease(EaseType.EaseOutBounce); // Easing type
        chromaTweener = HOTween.Shake(vignetteAndChromaticAberration, 1f, parms, 10f);
    }

    private void ResetChroma() {
        vignetteAndChromaticAberration.chromaticAberration = 0f;
    }

    public void FireShakeEffect() {
        if (shakeTweener != null) {
            shakeTweener.Kill();
            ResetShake();
        }
        float angle = Random.Range(0f, 2f*Mathf.PI);
        Vector3 shakeDir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), -10f) * 0.5f;

        TweenParms parms = new TweenParms();
        parms.Prop("position", shakeDir); // Position tween
        parms.Ease(EaseType.EaseOutBounce); // Easing type
        shakeTweener = HOTween.Shake(transform, 1, parms);
    }

    private void ResetShake() {
        transform.position = new Vector3(0f, 0f, -10f);
    }

    public void FireZoomEffect() {
        if (zoomTweener1 != null) {
            zoomTweener1.Kill();
            ResetZoom();
        }
        if (zoomTweener2 != null) {
            zoomTweener2.Kill();
            ResetZoom();
        }

        TweenParms parms1 = new TweenParms();
        parms1.Prop("orthographicSize", 5f); // Position tween
        parms1.Ease(EaseType.EaseOutBounce); // Easing type
        zoomTweener1 = HOTween.Shake(Camera.main, 1, parms1, 0.5f);

        TweenParms parms2 = new TweenParms();
        parms2.Prop("orthographicSize", 5f); // Position tween
        parms2.Ease(EaseType.EaseOutBounce); // Easing type
        zoomTweener2 = HOTween.Shake(Camera2, 1, parms2, 0.5f);
    }

    private void ResetZoom() {
        Camera.main.orthographicSize = 5f;
        Camera2.orthographicSize = 5f;
    }
}
