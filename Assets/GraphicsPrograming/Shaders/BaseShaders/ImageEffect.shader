Shader "ImageEffect/Base" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader {
		pass{
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }  
			ColorMask RGB	
			CGPROGRAM
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma vertex vert_img
			#pragma fragment frag
			#include "UnityCG.cginc"
      
			sampler2D _MainTex;
			
			half4 _MainTex_TexelSize;
			
			fixed4 frag(v2f_img i) : COLOR{
				return tex2D(_MainTex, i.uv);
			}
			ENDCG
		}
	} 
}