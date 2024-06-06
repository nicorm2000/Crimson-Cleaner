Shader "Custom/OutlineShader"
{
    Properties
    {
        _OutlineColor("Outline Color", Color) = (1,0,0,1)
        _OutlineWidth("Outline Width", Range(0.0, 0.1)) = 0.05
    }
        SubShader
    {
        Tags {"Queue" = "Overlay" "RenderType" = "Overlay"}
        ZWrite On
        ZTest LEqual
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            Name "OUTLINE"
            Cull Front

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : POSITION;
            };

            float _OutlineWidth;
            float4 _OutlineColor;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex) + _OutlineWidth * _OutlineWidth * float4(0,0,0,1);
                return o;
            }

            half4 frag(v2f i) : COLOR
            {
                return _OutlineColor;
            }
            ENDCG
        }

        Pass
        {
            Name "BASE"
            Tags { "LightMode" = "ForwardBase" }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            half4 frag(v2f i) : COLOR
            {
                return fixed4(0,0,0,0); // Transparent
            }
            ENDCG
        }
    }
}