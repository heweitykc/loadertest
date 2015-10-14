Shader "PostEffect/dof"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}		
	}
	SubShader
	{		
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D_float _CameraDepthTexture;
			uniform float _Near;
			uniform float _Far;
			uniform float _FocusRange;
			uniform float _FocusDistance;
			uniform float _BlurOffset;

			fixed4 frag (v2f i) : SV_Target
			{
				float blurFactor;
				fixed4 cOrig = tex2D(_MainTex, i.uv);
				fixed4 cBlurred = tex2D(_MainTex, i.uv);

				cBlurred += tex2D(_MainTex, half2(i.uv.x + _BlurOffset, i.uv.y + _BlurOffset));
				cBlurred += tex2D(_MainTex, half2(i.uv.x - _BlurOffset, i.uv.y - _BlurOffset));
				cBlurred += tex2D(_MainTex, half2(i.uv.x + _BlurOffset, i.uv.y - _BlurOffset));
				cBlurred += tex2D(_MainTex, half2(i.uv.x - _BlurOffset, i.uv.y + _BlurOffset));
				cBlurred /= 4;

				float2 depth_uv = float2(i.uv.x, i.uv.y);
				float d = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, depth_uv);
				d = 1 - Linear01Depth(d);
				float sceneZ = _Near / (d - _Far);
				blurFactor = saturate(abs(sceneZ-_FocusDistance)/ _FocusRange);
				return lerp(cBlurred, cOrig, blurFactor);
				//fixed4 col = tex2D(_MainTex, i.uv);
				//fixed4 col = fixed4(d, d, d, 1);
				//return col;
			}
			ENDCG
		}
	}
}
