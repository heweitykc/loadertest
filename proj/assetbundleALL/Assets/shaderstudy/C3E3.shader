Shader "Custom/C3E3"
{
	Properties
	{
		decal("Base (RGB)", 2D) = "white" {}
	}
	//Reference  : http://unity3d.com/support/documentation/Components/SL-SubShader.html
	SubShader
	{
		//Reference  : http://unity3d.com/support/documentation/Components/SL-Pass.html
		Pass
		{
			//Reference  : http://unity3d.com/support/documentation/Components/SL-ShaderPrograms.html
			CGPROGRAM			
			#pragma vertex C3E2v_varying			
			#pragma fragment C3E3f_texture
			#include "UnityCG.cginc"

			struct v2f
			{
				float4 position : SV_POSITION;
				float4 color    : COLOR;
				float4 texCoord  : TEXCOORD0;
			};

			struct a2v
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float4 texcoord : TEXCOORD0;
			};
			float4 decal_ST;
			v2f  C3E2v_varying(a2v In, sampler2D decal)
			{
				v2f  OUT;
				OUT.position = mul(UNITY_MATRIX_MVP, float4(In.vertex.xyz, 1));				
				OUT.color = In.color;				
				OUT.texCoord = float4(TRANSFORM_TEX(In.texcoord.xy, decal),0,0);
				return OUT;
			}

			struct C3E3f_Output
			{
				float4 color : COLOR;
			};

			C3E3f_Output C3E3f_texture(float4 texcoord : TEXCOORD0, sampler2D decal)
			{
				C3E3f_Output OUT;
				OUT.color = tex2D(decal, texcoord.xy);
				return OUT;
			}

			ENDCG
		}
	}
}