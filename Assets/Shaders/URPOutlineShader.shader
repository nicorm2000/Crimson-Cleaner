Shader "Custom/URPOutlineShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Color("Main Color", Color) = (1,1,1,1)
        _OutlineColor("Outline Color", Color) = (0,0,0,1)
        _OutlineThickness("Outline Thickness", Range(.002, 0.03)) = .005
    }
        SubShader
        {
            Tags { "RenderPipeline" = "UniversalRenderPipeline" }
            Pass
            {
                Name "OUTLINE"
                Tags { "LightMode" = "UniversalForward" }
                Cull Front

                HLSLPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

                struct Attributes
                {
                    float4 positionOS : POSITION;
                    float3 normalOS : NORMAL;
                };

                struct Varyings
                {
                    float4 positionHCS : SV_POSITION;
                    float4 color : COLOR;
                };

                float _OutlineThickness;
                float4 _OutlineColor;

                Varyings vert(Attributes input)
                {
                    Varyings output;
                    float3 normalWS = TransformObjectToWorldNormal(input.normalOS);
                    float4 offset = float4(normalWS * _OutlineThickness, 0);
                    output.positionHCS = TransformObjectToHClip(input.positionOS + offset);
                    output.color = _OutlineColor;
                    return output;
                }

                half4 frag(Varyings input) : SV_Target
                {
                    return input.color;
                }

                ENDHLSL
            }
        }
}