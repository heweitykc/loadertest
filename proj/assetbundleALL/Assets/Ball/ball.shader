Shader "Unlit/ball"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Num("Num", float) = 1
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
// Upgrade NOTE: excluded shader from DX11 and Xbox360; has structs without semantics (struct v2f members angle)
#pragma exclude_renderers d3d11 xbox360
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
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
			float _Num;
			
			v2f vert (appdata v)
			{
				v2f o;
				float tv = 3.1415926 / _Num;
				float3 first = float3(0, 1, 0);
				float angle;
				if (v.vertex.x == 0 && v.vertex.y == 0)
					angle = 0;
				else {
					angle = acos(dot(normalize(v.vertex.xyz), first)/(length(first)*length(v.vertex)));
				}				

				float ret = sin(_Time.y + angle)*0.01;
				if ((round(angle / tv) % 2) == 0) {
					o.color = float4(0, 0, 0, 0);
					v.vertex.z += ret;
					//v.vertex.x += sin(ret)*0.01;
					//v.vertex.y += cos(ret)*0.01;
				}
				else {
					o.color = float4(1, 0, 0, 0);
					v.vertex.z -= ret;
					//v.vertex.x -= sin(ret)*0.01;
					//v.vertex.y -= cos(ret)*0.01;
				}

				
				
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);						
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
