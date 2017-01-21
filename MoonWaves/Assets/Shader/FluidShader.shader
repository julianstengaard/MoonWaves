Shader "Hidden/FluidShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_FluidTex ("Texture", 2D) = "white" {}
		_WaterColor("Water Color", COLOR) = (1,1,1,1)
		_FoamColor("Foam Color", COLOR) = (1,1,1,1)
		_Cutoff("Cutoff", float) = 0.5
		_FoamCutoff("FoamCutoff", float) = 0.5
		_FoamFadeInDist("FoamFadeInDist", float) = 0.5
		_FoamMaxDist("FoamMaxDist", float) = 0.5
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

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

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _FluidTex;
			float4 _WaterColor;
			float4 _FoamColor;
			float _Cutoff;
			float _FoamCutoff;
			float _FoamFadeInDist;
			float _FoamMaxDist;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);

				//potentially flip uv.y
				#if UNITY_UV_STARTS_AT_TOP
				i.uv.y = 1 - i.uv.y;
				#endif

				fixed4 fluid = tex2D(_FluidTex, i.uv);
				
				//color the water
				if (fluid.b > _Cutoff) {
					if (fluid.b < _FoamCutoff) {
						float format = 16.0f / 9.0f;
						float xDiff = (i.uv.x - .5f) * format;
						float yDiff = i.uv.y - .5f;
						float dist = sqrt(pow(xDiff, 2) + pow(yDiff, 2));
						float lerpVal = (dist - _FoamFadeInDist) / (_FoamMaxDist - _FoamFadeInDist);

						col = lerp(_WaterColor, _FoamColor, lerpVal);
					}
					else {
						col = _WaterColor;
					}
				}
				
				return col;
			}
			ENDCG
		}
	}
}
