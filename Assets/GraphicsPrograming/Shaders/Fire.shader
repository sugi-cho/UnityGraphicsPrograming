Shader "Custom/Fire" {
	  Properties {
		_C1 ("color1", Color) = (0,0,0,1)
		_C2 ("color2", Color) = (1,1,1,1)
		_CD ("curl delta pos", Float) = 0.01
		_CS ("curl scale", Float) = 1
		_CSpeed ("curl speed", Float) = 1
		_DT ("Time.timeFromSceneLoaded", Float) = 0
		_Speed ("fire speed", Float) = 1
		_Size ("fire size(0-1)", Range(0,10)) = 0
		_GradScale ("gradation scale", Float) = 1
		_GradOffset ("gradation offset", Float) = 0
		_FireSpeed ("fire speed moeru",Float) = 1
		_Layers ("num layers", Float) = 10
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		Lighting Off
		
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert noforwardadd
		#pragma target 3.0
		#include "Libs/Noise.cginc"
		
		uniform float _Taiyo;
		fixed4 _C1,_C2;
		float _CD,_CS,_CSpeed,_DT,_Speed,_Size,_GradScale,_GradOffset,_FireSpeed,_Layers;
 
		struct Input {
			float2 uv_MainTex;
			fixed4 color : COLOR;
		};
		
		void vert (inout appdata_full v){
			float fire = saturate(_DT);
			
			float3 pos = v.vertex.xyz;
			float3 wPos = mul(_Object2World, v.vertex).xyz;
			float y = pos.y + 0.5;
			pos.y += y*y;
			float3 tmp = pos;
			float delta = snoise(2*float3(wPos.x,wPos.y-_Time.t*_FireSpeed-_DT,wPos.z)) * y;
			pos += v.normal * delta;
			pos.y += snoise(2*pos+_Time.y-_DT)*y*y;
			pos += float3(curlX(pos*_CS+100,_CD),curlY(pos*_CS+100,_CD),curlZ(pos*_CS+100,_CD)) * _CSpeed;
			
			pos.y += 0.5;
			float size = (saturate(_Size/(pow(pos.y/2,2)*2+1)));
			pos.xyz *= 0.5 + size*size/2;
			//pos.y -= 0.5;
			
			v.vertex.xyz = lerp(v.vertex.xyz, pos, fire);
			
			delta = saturate(delta*y);
			delta += y*_GradScale-_GradOffset;
			delta = floor(delta*_Layers)/_Layers;
			v.color = lerp(_C1,_C2,delta);
		}
 
		void surf (Input IN, inout SurfaceOutput o) {
			o.Emission = IN.color;
			
		}
		ENDCG
	} 
	FallBack "Diffuse"
}