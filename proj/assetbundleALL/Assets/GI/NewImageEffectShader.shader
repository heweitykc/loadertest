Shader "Custom/NewImageEffectShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_MaskTex("Mask Texture", 2D) = "white" {}
	}

	CGINCLUDE
		#include "UnityCG.cginc"

		sampler2D _MainTex;
		sampler2D _MaskTex;

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

		v2f vert(appdata v)
		{
			v2f o;
			o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
			o.uv = v.uv;
			return o;
		}		

		fixed4 frag(v2f i) : SV_Target
		{
			fixed4 col = tex2D(_MainTex, i.uv);			
			col.rgb += half3(0.1, 0.1, 0.1);
			return col;
		}

		fixed4 frag2(v2f i) : SV_Target
		{
			fixed4 col1 = tex2D(_MainTex, i.uv);
			fixed4 col2 = tex2D(_MaskTex, i.uv);
			col1.rgb = lerp(col1, col2, 0.5);
			return col1;
		}
	ENDCG

	SubShader
	{			
		Cull Off
		ZWrite Off
		ZTest Always

		//0
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			ENDCG
		}

		//1
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag2
			ENDCG
		}
	}
}
