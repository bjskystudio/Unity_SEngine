Shader "UI/ParticleUVMoveADD"
{
	Properties
	{
		_MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		_SpeedU ("SpeedU", Float) = 10
		_SpeedV ("SpeedV", Float) = 10
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest [unity_GUIZTestMode]
		Fog { Mode Off }
		Blend One One

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#include "UnityUI.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color	: COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				half2 texcoord  : TEXCOORD0;
				fixed4 color	: COLOR;
			};
			
			fixed4 _Color;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _SpeedU;
			float _SpeedV;

			v2f vert(appdata_t IN)
			{
				v2f OUT;

				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = TRANSFORM_TEX(IN.texcoord, _MainTex);

				float timeinteger = floor(_Time.x);
                float timedelta = frac(_Time.x);
                float offsetU = (timeinteger - 2.0 * floor(timeinteger / 2.0) + timedelta) * _SpeedU;
				float offsetV = (timeinteger - 2.0 * floor(timeinteger / 2.0) + timedelta) * _SpeedV;

				OUT.texcoord.x = OUT.texcoord.x - offsetU;
                OUT.texcoord.y = OUT.texcoord.y - offsetV;

				OUT.color = IN.color * _Color;

				return OUT;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 finalColor = tex2D(_MainTex, IN.texcoord) * IN.color;
				finalColor.rgb *= finalColor.a;
				return finalColor;
			}
		ENDCG
		}
	}
}
