using UnityEngine;
using System.Collections;

public class dof : MonoBehaviour
{
    public Material Material;
    public Transform target;

    void Awake()
    {
        GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //Material.SetTexture("_MainTex", GetComponent<Camera>().targetTexture);
        Graphics.Blit(source, destination, Material,0);
    }
}
