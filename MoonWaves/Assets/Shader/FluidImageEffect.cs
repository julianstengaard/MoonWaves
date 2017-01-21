using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FluidImageEffect : MonoBehaviour {

    [Header("Settings")]
    public Color waterColor;
    public Color foamColor;
    public float cutoff;
    public float foamCutoff;
	[Range(0, 1)]
	public float foamFadeInDist;
	public float foamFadeDist;

    [Header("References")]
    public Shader fluidShader;
    public Camera fluidCamera;
    
    private Material mat;
    private RenderTexture rt;
    
    void Awake()
    {
        mat = new Material(fluidShader);
        rt = new RenderTexture(Screen.width, Screen.height, 0);
    }

    void Start()
    {
        fluidCamera.targetTexture = rt;
        fluidCamera.enabled = false;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        fluidCamera.Render();

		mat.SetTexture("_FluidTex", rt);
		mat.SetColor("_WaterColor", waterColor);
        mat.SetColor("_FoamColor", foamColor);
        mat.SetFloat("_Cutoff", cutoff);
        mat.SetFloat("_FoamCutoff", foamCutoff);
		mat.SetFloat("_FoamFadeInDist", foamFadeInDist);
		mat.SetFloat("_FoamMaxDist", foamFadeInDist + foamFadeDist);

		Graphics.Blit(src, dest, mat);
    }
}
