Shader "Unlit/Dummy"
{
    Properties
    {
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

            sampler2D _BaseTex;
            sampler2D _OverTex;
            float4 _Resolution; // (width, height, _, _)
            float _CustomTime;
            float _InputTimes[16];

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.vertex.xy;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = fixed4(0, 0, 0, 0);
                return col;
            }
            ENDCG
        }
    }
}