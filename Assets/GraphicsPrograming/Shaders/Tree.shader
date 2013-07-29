Shader "Custom/Tree" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_C1 ("miki no iro", Color) = (0.5,0.2,0,1)
		_C2 ("ki no iro1", Color) = (0.1,0.8,0.1,1)
		_C3 ("ki no iro2", Color) = (0.3,0.9,0.2,1)
		_T ("trantiton", Float) = 0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert
		#pragma target 3.0
		#include "Libs/Random.cginc"
		#include "Libs/Noise.cginc"

		sampler2D _MainTex;
		fixed4 _C1,_C2,_C3;
		float _T;
		
		struct appdata {
			float4 vertex : POSITION;
			float4 tangent : TANGENT;
			float3 normal : NORMAL;
			float2 texcoord : TEXCOORD0;
			float2 texcoord1 : TEXCOORD1;
			float4 color : COLOR;
		};

		struct Input {
			float2 uv_MainTex;
			float4 color : COLOR;
		};
		
		void vert (inout appdata v){
			float2 uv = v.texcoord.xy;
			float3 pos = v.vertex.xyz;
			if(uv.y < 0.2){
				pos.xz *= 2;
				pos.y += 10 * uv.y *uv.y;
				v.color = _C1;
			}
			else{
				pos.y += 2;
				if(uv.y > 0.2){
					pos.xyz *= 5 * uv.y;
					float noise = (snoise(pos.xyz/2+_Time.x)/2+0.5);
					pos.xyz += v.normal * noise * 2;
					v.color = lerp(_C2,_C3,uv.y)*noise;
				}
				else{
					v.color = _C1/2;
				}
			}
			v.vertex.xyz = lerp(v.vertex.xyz, pos,_T);
		}

		void surf (Input IN, inout SurfaceOutput o) {
			float2 uv = IN.uv_MainTex;
			half4 c = IN.color;
			if(IN.uv_MainTex.y <= 0.2){
				uv.x = floor(uv.x*40);
				uv.y = floor((uv.y+sin(uv.x))*20);
				c -= rand(uv)/2;
			}
			o.Albedo = c.rgb;
			o.Emission = IN.color/5;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
