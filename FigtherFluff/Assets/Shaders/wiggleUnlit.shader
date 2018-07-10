// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Unlit alpha-cutout shader.
// - no lighting
// - no lightmap support
// - no per-material color

Shader "Custom/Wiggle Transparent Cutout" {
	Properties{
		_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
		_Cutoff("Alpha cutoff", Range(0, 1)) = 0.5
		_Color("Color", Color) = (1, 1, 1, 1)
	}
		SubShader{
		Tags{ "Queue" = "AlphaTest" "IgnoreProjector" = "True" "RenderType" = "TransparentCutout" }
		LOD 100

		Lighting Off

		Pass{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

		struct appdata_t {
		float4 vertex : POSITION;
		float2 texcoord : TEXCOORD0;
	};

	struct v2f {
		float4 vertex : SV_POSITION;
		half2 texcoord : TEXCOORD0;
		float4 screenPos : TEXCOORD1;
	};

	sampler2D _MainTex;
	float4 _MainTex_ST;
	fixed _Cutoff;
	fixed4 _Color;

	v2f vert(appdata_t v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.screenPos = ComputeScreenPos(o.vertex);
		o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
		return o;
	}

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

	fixed4 frag(v2f i) : SV_Target
	{
		float2 uv = float2(i.texcoord);
		uv.x += (noise(uv.y + round(i.screenPos.y*5) * round(_Time * 2))) * 0.02;
		uv.y += (noise(uv.x + round(i.screenPos.x*10) * round(_Time * 2))) * 0.02;

		fixed4 col = tex2D(_MainTex, uv)  * _Color;
		clip(col.a - _Cutoff);
		return col;
	}
		ENDCG
	}
	}

}