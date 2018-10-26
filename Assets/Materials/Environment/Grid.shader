Shader "Unlit/Grid"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_AlphaFalloff ("Alpha", 2D) = "white" {}
		_Mult("Mult", Float) = 1.0
		_Color("Color", Color) = (1,1,1,1)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Tags { "Queue" = "Transparent +100" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		AlphaTest Greater .01
		ColorMask RGB
		Cull Off Lighting Off ZWrite Off Fog { Color(0,0,0,0) }
		BindChannels {
			Bind "Color", color
			Bind "Vertex", vertex
			Bind "TexCoord", texcoord
		}

		Pass
		{
			CGPROGRAM
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
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			sampler2D _AlphaFalloff;
			float4 _AlphaFalloff_ST;
			float _Mult;
			float4 _MainTex_ST;
			float4 _Color;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 a = tex2D(_AlphaFalloff, i.uv * _AlphaFalloff_ST.xy);
				fixed4 mcol = col;
				mcol.rgb *= _Mult * _Color.rgb;
				mcol.a *= a.r;
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, mcol);
				return mcol;
			}
			ENDCG
		}
	}
}
