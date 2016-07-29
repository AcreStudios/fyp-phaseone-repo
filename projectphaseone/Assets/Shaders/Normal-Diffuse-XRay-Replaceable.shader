Shader "EVision/Diffuse-XRay-Replaceable"
{
	Properties
	{
		[Header(Normal Unity Texture Settings)]
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		[Space]

		[Header(EVision Settings)]
		_EdgeColor("EVision Color", Color) = (0,0,0,0)
	}

	SubShader
	{
		Tags
		{
			//////////////////////////////////////////////////////////////////////////////////////////////////////
			// In some cases, it's necessary to force XRay objects to render before the rest of the geometry	//
			// This is so their depth info is already in the ZBuffer, and Occluding objects won't mistakenly	//
			// write to the Stencil buffer when they shouldn't.													//
			//																									//
			// This is what "Queue" = "Geometry-1" is for.														//
			// I didn't bring this up in the video because I'm an idiot.										//
			//																									//
			// Cheers,																							//
			// Dan																								//
			//////////////////////////////////////////////////////////////////////////////////////////////////////
			"Queue" = "Geometry-1"
			"RenderType" = "Opaque"
			"EVision" = "ColoredOutline"
		}
		LOD 200

		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex2;
		fixed4 _Color;

		struct Input 
		{
			float2 uv_MainTex2;
		};

		void surf(Input IN, inout SurfaceOutput o)
		{
			fixed4 c = tex2D(_MainTex2, IN.uv_MainTex2) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG

		// Pass 2 Settings
		ZTest LEqual // Do not draw when occluded
		Zwrite On // write to ZBuffer
		Tags
		{
			"RenderType" = "Opaque" "Queue" = "Geometry"
		}
			// Pass 2, Render Object as needed (Standard Surface shader...)
		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows
		#pragma target 3.0
		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
			float3 viewDir;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG // End Pass 2
	}
	
	Fallback "Legacy Shaders/VertexLit"
}
