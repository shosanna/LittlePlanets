Shader "Hidden/DestShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_DestMapa("Texture", 2D) = "black" {}
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
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _DestMapa;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 destoBarva = tex2D(_DestMapa, i.uv + 10 *_Time.xx);
				fixed4 col = tex2D(_MainTex, i.uv);

				float time = _Time.y;
				int rounded_time = floor(time);
				float diff = time - rounded_time;

				if (destoBarva.r == 1 ) {
					col = col + fixed4(0.3, 0.3, 0.3, 0);
				}


				if (diff < 0.002) {
					col = fixed4(1, 1, 1, 1);
				}

				return col;
			}
			ENDCG
		}
	}
}
