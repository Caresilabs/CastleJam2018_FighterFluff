﻿Shader "Custom/xwiggle" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		//Tags { "RenderType"="Opaque" }
		Tags{ "Queue" = "Transparent" "RenderType" = "Cutoff" "IgnoreProjector" = "True" }
		//LOD 200
		LOD 200

		Blend SrcAlpha OneMinusSrcAlpha

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard alphatest:_Cutoff addshadow fullforwardshadows 

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)
		
		float hash(float n)
		{
			return frac(sin(n)*43758.5453);
		}

		float noise(float3 x)
		{
			// The noise function returns a value in the range -1.0f -> 1.0f

			float3 p = floor(x);
			float3 f = frac(x);

			f = f * f*(3.0 - 2.0*f);
			float n = p.x + p.y*57.0 + 113.0*p.z;

			return lerp(lerp(lerp(hash(n + 0.0), hash(n + 1.0), f.x),
				lerp(hash(n + 57.0), hash(n + 58.0), f.x), f.y),
				lerp(lerp(hash(n + 113.0), hash(n + 114.0), f.x),
					lerp(hash(n + 170.0), hash(n + 171.0), f.x), f.y), f.z);
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			float2 uv = float2(IN.uv_MainTex);
			uv.x += (noise(uv.y + 5 * round(_Time * 2))) * 0.02;
			uv.y += (noise(uv.x + 8 * round(_Time * 2))) * 0.02;
			//uv.x += 0.1;
			fixed4 c = tex2D (_MainTex, uv) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
