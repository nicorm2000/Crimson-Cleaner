Shader "Custom/TextureBlendURP"
{
    Properties
    {
        _MainTexA("Texture A", 2D) = "white" {}
        _MainTexB("Texture B", 2D) = "white" {}
        _Blend("Blend", Range(0,1)) = 0.0
    }

        SubShader
        {
            Tags { "RenderType" = "Opaque" }

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

                sampler2D _MainTexA;
                sampler2D _MainTexB;
                float _Blend;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 colA = tex2D(_MainTexA, i.uv);
                    fixed4 colB = tex2D(_MainTexB, i.uv);
                    fixed4 finalColor = lerp(colA, colB, _Blend);
                    return finalColor;
                }
                ENDCG
            }
        }
}