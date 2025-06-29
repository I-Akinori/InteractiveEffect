Shader "Unlit/Julia"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Resolution("Resolution", Vector) = (1920, 1080, 0, 0)
    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
        }
        Pass
        {
            ZTest Always Cull Off ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Utility/Noise1D.hlsl"

            sampler2D _MainTex;
            float4 _Resolution; // (width, height, _, _)
            float _CustomTime;
            float _InputTimes[16];

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            float GetRandomNumber(float2 texCoord, int Seed)
            {
                return frac(sin(dot(texCoord.xy, float2(12.9898, 78.233)) + Seed) * 43758.5453);
            }

            v2f vert(appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.vertex.xy; // transform to [0,1]
                return o;
            }

            float2 GetShortSideNormalizedCoord(float2 uv, float2 resolution)
            {
                float2 coord = uv * 2.0 - 1.0;
                float aspect = resolution.x / resolution.y;

                if (aspect >= 1.0)
                {
                    coord.x *= aspect;
                }
                else
                {
                    coord.y /= aspect;
                }

                return coord;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float4 col = float4(0, 0, 0, 1);
                float2 p = GetShortSideNormalizedCoord(i.uv, _Resolution.xy);

                int j = 0;
                float2 x = float2(-0.345, 0.654);
                float2 y = float2(_CustomTime * 0.005, 0.0);
                float2 z = p;
                for (uint k = 0; k < 360; k++)
                {
                    j++;
                    if (length(z) > 2.0) { break; }
                    z = float2(z.x * z.x - z.y * z.y, 2.0 * z.x * z.y) + x + y;
                }
                float h = abs(fmod(_CustomTime * 15.0 - float(j), 360.0) / 360.0);
                h = saturate(h) * 0.25;
                col = float4(h, h, h, 1.0);
                return col;
            }
            ENDCG
        }
    }
}