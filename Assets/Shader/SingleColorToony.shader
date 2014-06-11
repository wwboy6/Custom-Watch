Shader "Custom/SingleColorToony" {
	Properties {
		_Color ("Main Color", Color) = (.5,.5,.5,1)
//		_MainTex ("Base (RGB)", 2D) = "white" {}
		_ToonShade ("ToonShader Cubemap(RGB)", CUBE) = "" { Texgen CubeNormal }
	}
	
	SubShader {
		Tags { "RenderType"="Opaque" }
		
		Pass {
			Name "BASE"
			Cull Off
			
			CGPROGRAM
// Upgrade NOTE: excluded shader from DX11 and Xbox360; has structs without semantics (struct v2f members cubenormal)
#pragma exclude_renderers d3d11 xbox360
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest 

			#include "UnityCG.cginc"

//			sampler2D _MainTex;
			samplerCUBE _ToonShade;
//			float4 _MainTex_ST;
			float4 _Color;

			struct appdata {
				float4 vertex : POSITION;
//				float2 texcoord : TEXCOORD0;
				float3 normal : NORMAL;
			};
			
			struct v2f {
				float4 pos : POSITION;
//				float2 texcoord : TEXCOORD0;
				float3 cubenormal;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
//				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.cubenormal = mul (UNITY_MATRIX_MV, float4(v.normal,0));
				return o;
			}

			float4 frag (v2f i) : COLOR
			{
//				float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 col = _Color;
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
				return float4(2.0f * cube.rgb * col.rgb, col.a);
			}
			ENDCG			
		}
	} 

	SubShader {
		Tags { "RenderType"="Opaque" }
		Pass {
			Name "BASE"
			Cull Off
//			SetTexture [_MainTex] {
//				constantColor [_Color]
//				Combine texture * constant
//			} 
			SetTexture [_ToonShade] {
				constantColor [_Color]
				combine texture * constant DOUBLE, constant
			}
		}
	}
	
//	FallBack "FX/Flare"
}
