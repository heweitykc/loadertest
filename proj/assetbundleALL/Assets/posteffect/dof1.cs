﻿using UnityEngine;
using System.Collections;
using System.Net.Sockets;

public class dof1 : MonoBehaviour {
    public float BlurOffset = 0.0027f;
    public float FocusRange = 120f;
    public GameObject TargetPlayer;    
    public Material mat;

    public Camera camera;

    public TcpClient client;

    void Start()
    {         
        camera.depthTextureMode = DepthTextureMode.Depth;
        client = new TcpClient();        
    }

    IEnumerator load()
    {
        WWW www = new WWW("www.baidu.com");
        yield return null;
        Debug.Log(www.text);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        float farM = camera.farClipPlane / (camera.farClipPlane - camera.nearClipPlane);
        float nearM = -camera.nearClipPlane * farM;

        mat.SetFloat("_Near", nearM);
        mat.SetFloat("_Far", farM);
        mat.SetFloat("_FocusRange", FocusRange);
        mat.SetFloat("_BlurOffset", BlurOffset);
        float focusDist = camera.WorldToViewportPoint(TargetPlayer.transform.position).z;
        mat.SetFloat("_FocusDistance", focusDist);

        Graphics.Blit(src, dst, mat);
    }
}
