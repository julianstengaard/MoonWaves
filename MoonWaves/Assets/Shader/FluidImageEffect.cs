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

        mat.SetColor("_WaterColor", waterColor);
        mat.SetColor("_FoamColor", foamColor);
        mat.SetFloat("_Cutoff", cutoff);
        mat.SetFloat("_FoamCutoff", foamCutoff);
        mat.SetTexture("_FluidTex", rt);

        Graphics.Blit(src, dest, mat);
    }
}
