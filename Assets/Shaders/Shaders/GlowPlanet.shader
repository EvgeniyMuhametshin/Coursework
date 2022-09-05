// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/GlowPlanet"
{
    Properties
	{
		_AtmosphereColor ("Atmosphere Color", Color) = (1,1,1,1)
		_AtmosphereSize ("Atmosphere Size", float) = 1.1
		_Ambient ("Ambient", float) = 0
		_Intensive ("Intensive", float) = 1
	}

	SubShader {

		Tags {"RenderType"="Opaque" "Queue" = "Transparent"}
		LOD 200

		blend SrcAlpha OneMinusSrcAlpha

		pass
		{
			CGPROGRAM
   			#pragma vertex vert
   			#pragma fragment frag
   			#include "UnityCG.cginc"
   			#include "UnityLightingCommon.cginc"

   			half4 _AtmosphereColor;
   			float _AtmosphereSize;
   			float _Ambient;
   			float _Intensive;

			struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                fixed4 diff : COLOR0;
            };

			v2f vert (appdata v)
    		{
   				v2f o;
   				v.vertex.xyz *= _AtmosphereSize;
   				o.vertex = UnityObjectToClipPos(v.vertex);
   				half3 worldNormal = UnityObjectToWorldNormal(v.normal);
   				half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
   				o.diff = nl * _LightColor0;
   				o.diff.rgb += ShadeSH9(half4(worldNormal,1));
   				return o;
   			}

   			fixed4 frag (v2f i) : SV_Target
   			{
   				return i.diff * _AtmosphereColor * _Intensive + _Ambient * _AtmosphereColor;
   			}
   			ENDCG
		}
	}
	Fallback "Diffuse"
}
