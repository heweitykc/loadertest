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
				//fixed4 col = tex2D(_MainTex, i.uv);	
				//col = fixed4(i.color.x, i.color.x, i.color.x,1);
				//return col;

				float2 uv = i.uv / _ScreenParams.xy;
				float time = _Time.y*1.25;

				float blob1x = (-0.5 + sin(time*0.8) / 3.0);
				float blob1y = (-0.5 - cos(time*2.2) / 3.0);
				float blob1 = sqrt(pow(uv.x + blob1x, 2.0)*2.5 + pow(uv.y + blob1y, 2.0));

				float blob2x = (-0.5 + sin(time*0.7 + 0.3) / 3.0);
				float blob2y = (-0.5 - cos(time*1.0 - 0.2) / 3.0);
				float blob2 = sqrt(pow(uv.x + blob2x, 2.0)*2.5 + pow(uv.y + blob2y, 2.0));

				float blob3x = (-0.5 + sin(time*1.5 + 0.6) / 3.0);
				float blob3y = (-0.5 - cos(time*0.4 - 0.85) / 3.0);
				float blob3 = sqrt(pow(uv.x + blob3x, 2.0)*2.5 + pow(uv.y + blob3y, 2.0));

				float final = (1.0 - (blob1*blob2*blob3)*16.0 + 1.0) / 2.0;

				float3 gum = float3((final)*1.0 + final*0.8*abs(-final + 0.1), -(final*-1.0*abs(final)), (final*1.0));

				return float4(clamp(-gum.brr / 16.0*2.0, 0.0, 1.0) + clamp(gum.rgb, 0.0, 1.0), 1.0);				
			}
			ENDCG
		}
	}
}
