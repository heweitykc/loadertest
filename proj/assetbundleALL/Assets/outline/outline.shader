Shader "Unlit/outline"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Outline("Outline", range(0,0.1)) = 0.02
		_Factor ("Factor", range(0,1)) = 0.1
		_ToonEffect("Toon Effect", range(0,1)) = 0.5	//卡通化程度
		_Steps("Steps of toon",range(0,9)) = 3			//色阶层数
	}
	SubShader
	{
		
		LOD 100
		
		Pass
		{
			Tags{ "RenderType" = "Opaque" "LightMode" = "Always" }
			Cull Front
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float4 normal : NORMAL;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 color : Color;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _Outline;
			float _Factor;			
			
			v2f vert (appdata v)
			{
				v2f o;
				
				float3 dir = normalize(v.vertex.xyz);
				/**/
				float3 dir2 = v.normal;
				float D = dot(dir,dir2); //反夹角				
				dir = dir*sign(D);
				dir = dir*_Factor + dir2*(1 - _Factor);
				
				v.vertex.xyz += dir*_Outline;

				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);		
				o.color = float4(_Outline, _Outline, _Outline, _Outline);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				return 0;
			}
			ENDCG
		}
		/**/	
		Pass
		{
			Tags{ "RenderType" = "Opaque" "LightMode" = "ForwardBase" }
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float4 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float3 normal: TEXCOORD1;
				float3 lightdir:TEXCOORD2;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _LightColor0;
			float _ToonEffect;
			float _Steps;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.normal = normalize(v.normal);
				o.lightdir = ObjSpaceLightDir(v.vertex);
				o.lightdir = normalize(o.lightdir);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{				
				fixed4 col = tex2D(_MainTex, i.uv);	
				float diff = max(0, dot(i.normal, i.lightdir));
				diff = (diff+1) / 2;					//亮化处理
				diff = smoothstep(0,1,diff);			//使颜色平滑在[0, 1]
				float toon = round(diff*_Steps)/_Steps;	//简化颜色
				diff = lerp(diff, toon, _ToonEffect);	//调节卡通程度
				
				return col * _LightColor0 * diff;
				//return fixed4(i.normal,0);
			}
			ENDCG
		}
	}
}
