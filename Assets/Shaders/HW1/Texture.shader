﻿Shader "CMPM163/Texture"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1, 1, 1, 1)
		_Shininess("Shininess", Float) = 10 //Shininess
		_SpecColor("Specular Color", Color) = (1, 1, 1, 1) //Specular highlights color
		_Reactivity("Reactivity", Float) = 1
	}
		SubShader
		{
			Tags { "LightMode" = "ForwardAdd" }
			//Blend One One
			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"

				uniform float _Reactivity;
				uniform float4 _LightColor0; //From UnityCG
				uniform float4 _Color;
				uniform float4 _SpecColor;
				uniform float _Shininess;

            struct appdata
            {
                float4 vertex : POSITION;
				float3 normal: NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
				float3 normal: NORMAL;
				float2 uv : TEXCOORD0;
				float3 vertexInWorldCoords : TEXCOORD1;
            };

            sampler2D _MainTex;

            v2f vert (appdata v)
            {
                v2f o;
				float4 tx = tex2Dlod(_MainTex, float4(v.uv.xy, 0.0, 0.0));
				float displacement = dot(float3(0.21, 0.72, 0.07), tx.rgb) * _Reactivity;
				float4 xyz = v.vertex + float4(v.normal * displacement, 0.0);

				o.vertexInWorldCoords = mul(unity_ObjectToWorld, xyz); //Vertex position in WORLD coords 
				o.vertex = UnityObjectToClipPos(xyz);
				o.normal = v.normal; //Normal
                o.uv = v.uv; 
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
				float3 P = i.vertexInWorldCoords.xyz;
				float3 N = normalize(i.normal);
				float3 V = normalize(_WorldSpaceCameraPos - P);
				float3 L = normalize(_WorldSpaceLightPos0.xyz - P);
				float3 H = normalize(L + V);

				float3 Kd = _Color.rgb; //Color of object
				float3 Ka = UNITY_LIGHTMODEL_AMBIENT.rgb; //Ambient light
				//float3 Ka = float3(0,0,0); //UNITY_LIGHTMODEL_AMBIENT.rgb; //Ambient light
				float3 Ks = _SpecColor.rgb; //Color of specular highlighting
				float3 Kl = _LightColor0.rgb; //Color of light


				//AMBIENT LIGHT 
				float3 ambient = Ka;


				//DIFFUSE LIGHT
				float diffuseVal = max(dot(N, L), 0);
				float3 diffuse = Kd * Kl * diffuseVal;


				//SPECULAR LIGHT
				float specularVal = pow(max(dot(N,H), 0), _Shininess);

				if (diffuseVal <= 0) {
					specularVal = 0;
				}

				float3 specular = Ks * Kl * specularVal;

				float4 tx = tex2D(_MainTex, i.uv);

				//FINAL COLOR OF FRAGMENT
				return float4(ambient + diffuse + specular, 1.0) + tx;
            }
            ENDCG
        }
    }
}
