using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FluidImageEffect : MonoBehaviour {

    public Shader fluidShader;
    public Color col;

    private Camera cam;
    private Material mat;
    
    void Awake()
    {
        cam = GetComponent<Camera>();
        mat = new Material(fluidShader);
    }

    void Start()
    {
        cam.enabled = true;
        mat.SetColor("_Color", col);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, mat);
    }
}
