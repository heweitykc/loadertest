Shader "Unlit/newball"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_AnimPos("_AnimPos", Vector) = (0,0,0,0)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			Cull Off
			CGPROGRAM
			#pragma target 3.0
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

			sampler2D _MainTex;
			float4 _MainTex_ST;
			Vector _AnimPos;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex) + _AnimPos;
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float2 p = i.uv ;
				float2 px = ddx(p);
				float2 py = ddy(p);

				float fx = (2*p.x*px.x) - px.y;
				float fy = (2*p.x*py.x) - py.y;

				float sd = ((p.x*p.x)-p.y) / sqrt(fx*fx+fy*fy);
				float alpha = 0.5-sd;
				clip(alpha);

				alpha = saturate(alpha);

				return fixed4(1,1,1,alpha);
			}
			ENDCG
		}
	}
}
