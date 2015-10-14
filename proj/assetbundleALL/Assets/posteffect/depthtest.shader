Shader "Unlit/depthtest"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

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

			struct fragOut
			{
				half4 color : SV_Target;
				//float depth : SV_Depth;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				//o.vertex.z = 0;
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fragOut frag (v2f i)
			{
				fragOut outdata;
				outdata.color = tex2D(_MainTex, i.uv);				
				return outdata;
			}
			ENDCG			
		}
	}
	FallBack "Diffuse"
}
