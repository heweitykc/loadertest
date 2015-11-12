Shader "Unlit/ball"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_HitAngle("HitAngle", float) = 1
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
			#pragma target 3.0

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
				float4 color : COLOR;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _HitAngle;
			
			v2f vert (appdata v)
			{
				v2f o;				
				float3 first = float3(0, 1, 0);
				float angle = acos(dot(normalize(first), normalize(v.vertex)));
				float diff = angle - radians(_HitAngle);
				v.vertex.xy = v.vertex.xy * (1+sin(_Time.y*3 + diff)*0.1);
				//v.vertex.xy += (v.vertex.xy*cos(diff));
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.color = float4(angle,0,0,0);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);	
				//col = fixed4(i.color.x, i.color.x, i.color.x,1);
				return col;			
			}
			ENDCG
		}
	}
}
