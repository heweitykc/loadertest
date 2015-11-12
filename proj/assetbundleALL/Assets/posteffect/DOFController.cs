using UnityEngine;
using System.Collections;

public class DOFController : MonoBehaviour
{
    public float BlurOffset = 0.0027f;
    public float FocusRange = 120f;
    public GameObject TargetPlayer;

    public Material _material;

    public Shader _dofshader;
    
    public GameObject _depthCamera = null;
    public Shader _depthshader = null;

    public RenderTexture _depthTexture = null;

    public RenderTexture depthTexture
    {
        get {
            if (_depthTexture == null) {
                _depthTexture = new RenderTexture(Screen.width, Screen.height, 16);                
                _depthTexture.hideFlags = HideFlags.HideAndDontSave;
                _depthTexture.isPowerOfTwo = false;
            }
            return _depthTexture;
        }
    }

    public Material material
    {
        get {
            if (_material == null) {
                _material = new Material(_dofshader);
                _material.hideFlags = HideFlags.HideAndDontSave;
            }
            return _material;
        }
    }

    void OnEnable()
    {

    }

    void OnDisable()
    {
        
    }

    void OnPreRender()
    {
        var cam = _depthCamera.GetComponent<Camera>();
        
        cam.CopyFrom(GetComponent<Camera>());
        cam.backgroundColor = Color.black;
        cam.hideFlags = HideFlags.HideAndDontSave;
        cam.enabled = false;
        cam.targetTexture = depthTexture;
        cam.RenderWithShader(_depthshader, "RenderType");
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        var camera = GetComponent<Camera>();

        float clipRange = (camera.farClipPlane - camera.nearClipPlane);
        float normalizeFar = Mathf.Clamp01(camera.farClipPlane/clipRange);        

        material.SetFloat("_ClipRange", clipRange);
        material.SetFloat("_Far", normalizeFar);
        material.SetFloat("_FocusRange", FocusRange);
        material.SetFloat("_BlurOffset", BlurOffset);
        material.SetTexture("_DepthTexture", depthTexture);
        float focusDist = camera.WorldToViewportPoint(TargetPlayer.transform.position).z;
        material.SetFloat("_FocusDistance", focusDist);

        Graphics.Blit(src, dst, _material);
    }
}
