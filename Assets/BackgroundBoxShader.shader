Shader "Unlit/BackgroundBoxShader"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_BumpMap("Normalmap", 2D) = "bump" {}
		_GlossMap("Gloss Map", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_MetallicMap("Metallic Map", 2D) = "white" {}
		_Metallic("Metallic", Range(0,1)) = 0.0
	}
	SubShader
	{
		Stencil
		{
			Ref[_COLORMASK]
			Comp equal
			Pass keep
		}

		Cull Front
		ZWrite Off
			CGPROGRAM
			#pragma surface surf Standard fullforwardshadows vertex:vert
			#pragma target 3.0

			float3 _PortalNormal;
			float3 _PortalPoint;

			sampler2D _MainTex;
			sampler2D _BumpMap;
			sampler2D _GlossMap;
			sampler2D _MetallicMap;

			struct Input
			{
				float2 uv_MainTex;
				float2 uv_BumpMap;
				float distToPlane;
			};

			half _Glossiness;
			half _Metallic;
			fixed4 _Color;

			UNITY_INSTANCING_BUFFER_START(Props)
			UNITY_INSTANCING_BUFFER_END(Props)

			void vert(inout appdata_full v, out Input o)
			{
				UNITY_INITIALIZE_OUTPUT(Input, o);

				float3 worldPos = mul(unity_ObjectToWorld, v.vertex);
				float theDot = dot(_PortalNormal, worldPos);
				float planeDot = dot(_PortalNormal, _PortalPoint);
				o.distToPlane = theDot - planeDot;
			}

			void surf(Input IN, inout SurfaceOutputStandard o)
			{
				clip(-IN.distToPlane);
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
				o.Albedo = c;
				o.Metallic = tex2D(_MetallicMap, IN.uv_MainTex) * _Metallic;
				o.Smoothness = tex2D(_GlossMap, IN.uv_MainTex) * _Glossiness;
				o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
				o.Alpha = c.a;
			}
			ENDCG
	}
}
