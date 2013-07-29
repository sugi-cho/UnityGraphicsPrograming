Shader "Custom/Water" {
	  Properties {
		_Color1 ("color1", Color) = (0,0,1,1)
		_Color2 ("color2", Color) = (0,1,1,1)
		_To ("target position", Vector) = (1,0,0,0)
		_G ("gravity", Vector) = (0,-9,0,0)
		_DT ("timeFromSceneLoaded", Float) = 0
		_S ("speed", Float) = 1
		_DW("delta water", Float) = 3
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		Lighting Off
		
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Lambert vertex:vert
		#include "Libs/Noise.cginc"
		#include "Libs/Random.cginc"
 
		fixed4 _Color1,_Color2;
		float4 _To,_G;
		float _DT,_S,_DW,_DX,_CS,_CP;
 
		struct Input {
			fixed4 color : COLOR;
		};
		
		void vert (inout appdata_full v){
			fixed4 c = lerp(_Color1,_Color2,abs(sin(v.texcoord.x*3.1415)));
			c.rgb += v.normal.xyz/8;
			
			float t = _DT;
			float time2Hit = length(_To)/_S;
			float3 v0 = _To/time2Hit - _G*time2Hit/2;
			v0 += snoise(v.vertex.xyz*3)/5*normalize(v0) + v.vertex.xyz/10;
			
			t = max((t-snoise(v.vertex.xyz*3)-1)*_DW,0);
			c += 1-saturate(t);
			
			float3 pos = lerp(float3(0,0,0),v.vertex.xyz,sqrt(t));
			pos += v.normal.xyz*saturate(1-t)*snoise(v.vertex*3)*pos*3;
			
			pos += _G/2*t*t+v0*t;
			if(pos.y < _To.y){
				float delta = sqrt(_To.y - pos.y);
				pos.y = max(pos.y,_To.y);
				pos.xz += (_To.xz-pos.xz)*saturate(delta/10);
				
				float y = length(v0)/2*(1+sin(delta*2))/(1+delta);
				pos.y += y;
				c += y;
				c.a = 5-delta;
			}
			
			v.vertex.xyz = pos;
			v.color = c;
		}
 
		void surf (Input IN, inout SurfaceOutput o) {
			clip(IN.color.a-0.5);
			o.Emission = IN.color;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}