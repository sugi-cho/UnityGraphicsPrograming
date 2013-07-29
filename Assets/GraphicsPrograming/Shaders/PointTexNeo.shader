Shader "Custom/PointTex" {

    Properties {
		_Color ("color", Color) = (0,0,0,0)
        _MainTex ("Color Tex", 2D) = "white" {}
        _MainTex2 ("Color 2", 2D) = "white" {}
        _LogoTexReplace ("Logo", 2D) = "white" {}
        _LogoTexReplace2 ("Logo2", 2D) = "white" {}
        _Second ("SecondFactor", Range(0,1)) = 0
        _T ("Tex", Range(0,1)) = 0
        _Break ("break", float) = 0
    }

    SubShader {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		AlphaTest Greater .01
		ColorMask RGB
		Cull Off Lighting Off ZWrite Off
        
        Pass {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #pragma glsl
            #pragma target 3.0

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _MainTex2;
            sampler2D _LogoTexReplace;
            sampler2D _LogoTexReplace2;
            fixed4 _Color;
            fixed _Second;
            fixed _T;
            float _Break;
            
            struct appdata_t{
            	float4 vertex : POSITION;
            	fixed4 color : COLOR;
            	float2 texcoord : TEXCOORD0;
            };

            struct v2f {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 uv : TEXCOORD0;
            };

            float4 _MainTex_ST;

            v2f vert (appdata_t v)
            {
				v2f o;
				
                float4 tex1 = tex2Dlod (_LogoTexReplace, float4(v.texcoord.xy,0,0));
                float4 tex2 = tex2Dlod (_LogoTexReplace2, float4(v.texcoord.xy,0,0));
                
                float4 tex = (1-_Second)*tex1 + _Second*tex2;
                
                float4 pos1 = tex;
				pos1.x = tex.r/10 + tex.g - (v.vertex.x-0.5)/512;
				pos1.y = tex.b/10 + tex.a - (v.vertex.y-0.5)/512;
				
				pos1.xy -= 0.5;
				pos1.z = 0;
				pos1.w = 1;
				
				v.vertex = _T*pos1 + (1-_T)*v.vertex;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				
				o.color = v.color;
				o.uv = TRANSFORM_TEX(v.texcoord,_MainTex);
				o.uv = pos1.xy + 0.5;
				return o;
            }

            half4 frag (v2f i) : COLOR

            {
                half4 c = tex2D (_MainTex, i.uv);
                half4 c2 = tex2D (_MainTex2, i.uv);
                return (1 - _Second) * c + _Second * c2;
            }

            ENDCG

        }

    }

}