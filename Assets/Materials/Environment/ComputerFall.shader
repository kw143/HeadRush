Shader "Custom/ComputerFall" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Speed("Speed", Float) = 1.0
		_Mult("Mult", Float) = 1.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _OffsetTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		float _Speed;
		float _Mult;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		float rand(float3 co)
		{
			return frac(sin(dot(co.xyz, float3(12.9898, 78.233, 45.5432))) * 43758.5453);
		}


		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed2 uvs = IN.uv_MainTex;
			uvs.y += _Time.x * _Speed;
			uvs.y += 0.1 * cos(sin(40 * uvs.x + _Time.y)) + 0.08 * sin(40 * uvs.x + 1.3*_Time.x);
			fixed4 c = tex2D (_MainTex, uvs) * _Color;
			//o.Albedo = rand(c.rgb) * _Mult;
			o.Albedo = c.rgb * _Mult;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			//o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
