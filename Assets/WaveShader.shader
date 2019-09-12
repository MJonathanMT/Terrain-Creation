//UNITY_SHADER_NO_UPGRADE

Shader "Unlit/WaveShader"
{
	Properties
	{
		_Color("Color", Color) = (0,0,0,1)
	}
	SubShader
	{
		Tags { "RenderType" = "Transparent" }
		Pass
		{

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct vertIn
			{
				float4 vertex : POSITION;
			};

			struct vertOut
			{
				float4 vertex : SV_POSITION;
			};

			// Implementation of the vertex shader
			vertOut vert(vertIn v)
			{
				// Displace the original vertex in model space
				float4 displacement = float4(0.0f, sin((v.vertex.x + _Time.y) * 2.0f) * 0.05f, 0.0f, 0.0f); // Q4

				v.vertex += displacement * 0.5f;

				vertOut o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				return o;
			}

			fixed4 _Color;
			
			// Implementation of the fragment shader
			fixed4 frag(vertOut v) : SV_Target
			{
				return _Color;
			}
			ENDCG
		}
	}
}