Shader "CMPM163/Outline"
{
    Properties
    {
        _Color ("Color", Color) = (0,0,0,1)
		_MainTex("Texture", 2D) = "white" {}
		

		_Depth ("Depth", Range(0.0,1.0)) = 1.0
		_LineColor("LineColor", Color) = (0,0,0,1)
    }
    SubShader
    {
		// Outline pass 1, render textured object normally
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			fixed4 _Color;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;

			fixed4 frag(v2f i) : SV_TARGET
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				col *= _Color;
				return col;
			}
			ENDCG
		}

		// Outline pass 2, render colored outline + get bright pixels
		Pass
		{
			Cull Front
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			fixed4 _LineColor;
			float _Depth;

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
			};

			v2f vert(appdata v)
			{
				v2f o;
				float3 normal = normalize(v.normal);
				float3 position = v.vertex + (normal * _Depth);
				//convert the vertex positions from object space to clip space so they can be rendered
				o.vertex = UnityObjectToClipPos(position);
				return o;
			}

			sampler2D _MainTex;

			fixed4 frag(v2f i) : SV_TARGET
			{
				return _LineColor;
			}
			ENDCG
		}
    }
}
