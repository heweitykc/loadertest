using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
public class NewImageEffect : PostEffectsBase
{
    public Shader fastShader = null;
    public Material fastMaterial = null;
    public Texture mask;

    public override bool CheckResources()
    {
        CheckSupport(false);

        fastMaterial = CheckShaderAndCreateMaterial(fastShader, fastMaterial);

        if (!isSupported)
            ReportAutoDisable();

        fastMaterial.SetTexture("_MaskTex", mask);

        return isSupported;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (CheckResources() == false)
        {
            Graphics.Blit(source, destination);
            return;
        }
        
        int divider = 2;
        var rtW = source.width / divider;
        var rtH = source.height / divider;

        RenderTexture rt = RenderTexture.GetTemporary(rtW, rtH, 0, source.format);
        Graphics.Blit(source, destination, fastMaterial, 1);
        RenderTexture.ReleaseTemporary(rt);
    }
}
