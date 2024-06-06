Shader "Custom/TextureBlendURP"
{
    Properties
    {
        _MainTexA("Texture A (BaseMap)", 2D) = "white" {}
        _MetallicMapA("Texture A (MetallicMap)", 2D) = "white" {}
        _NormalMapA("Texture A (NormalMap)", 2D) = "bump" {}
        _HeightMapA("Texture A (HeightMap)", 2D) = "white" {}
        _OcclusionMapA("Texture A (OcclusionMap)", 2D) = "white" {}

        _MainTexB("Texture B (BaseMap)", 2D) = "white" {}
        _MetallicMapB("Texture B (MetallicMap)", 2D) = "white" {}
        _NormalMapB("Texture B (NormalMap)", 2D) = "bump" {}
        _HeightMapB("Texture B (HeightMap)", 2D) = "white" {}
        _OcclusionMapB("Texture B (OcclusionMap)", 2D) = "white" {}

        _Blend("Blend", Range(0,1)) = 0.0
    }

        SubShader
        {
            Tags { "RenderType" = "Opaque" }

            Pass
            {
                Name "ForwardLit"
                Tags { "LightMode" = "UniversalForward" }

                HLSLPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
                #pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
                #pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
                #pragma multi_compile _ _SHADOWS_SOFT
                #pragma multi_compile _ _MIXED_LIGHTING_SUBTRACTIVE

                #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
                #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
                #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"

                struct Attributes
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                    float3 normal : NORMAL;
                };

                struct Varyings
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                    float3 worldNormal : TEXCOORD1;
                    float3 worldPos : TEXCOORD2;
                };

                sampler2D _MainTexA;
                sampler2D _MetallicMapA;
                sampler2D _NormalMapA;
                sampler2D _HeightMapA;
                sampler2D _OcclusionMapA;

                sampler2D _MainTexB;
                sampler2D _MetallicMapB;
                sampler2D _NormalMapB;
                sampler2D _HeightMapB;
                sampler2D _OcclusionMapB;

                float _Blend;

                Varyings vert(Attributes input)
                {
                    Varyings output;
                    UNITY_SETUP_INSTANCE_ID(input);
                    float3 worldPos = TransformObjectToWorld(input.vertex);
                    output.vertex = TransformWorldToHClip(worldPos);
                    output.uv = input.uv;
                    output.worldPos = worldPos;
                    output.worldNormal = TransformObjectToWorldNormal(input.normal);
                    return output;
                }

                half4 frag(Varyings input) : SV_Target
                {
                    UNITY_SETUP_INSTANCE_ID(input);

                // Sample the textures and maps for texture A
                half4 baseColorA = tex2D(_MainTexA, input.uv);
                half metallicA = tex2D(_MetallicMapA, input.uv).r;
                half3 normalA = UnpackNormal(tex2D(_NormalMapA, input.uv));
                half heightA = tex2D(_HeightMapA, input.uv).r;
                half occlusionA = tex2D(_OcclusionMapA, input.uv).r;

                // Sample the textures and maps for texture B
                half4 baseColorB = tex2D(_MainTexB, input.uv);
                half metallicB = tex2D(_MetallicMapB, input.uv).r;
                half3 normalB = UnpackNormal(tex2D(_NormalMapB, input.uv));
                half heightB = tex2D(_HeightMapB, input.uv).r;
                half occlusionB = tex2D(_OcclusionMapB, input.uv).r;

                // Blend the properties
                half metallic = lerp(metallicA, metallicB, _Blend);
                half3 normal = normalize(lerp(normalA, normalB, _Blend));
                half height = lerp(heightA, heightB, _Blend);
                half occlusion = lerp(occlusionA, occlusionB, _Blend);

                // Sample the textures and blend them
                half4 blendedColor = lerp(baseColorA, baseColorB, _Blend);

                // Compute lighting
                Light mainLight = GetMainLight(); // Get main directional light
                float3 lightDir = normalize(mainLight.direction);
                float NdotL = max(0.0, dot(input.worldNormal, lightDir));

                // Manually set ambient color
                float3 ambientColor = float3(0.2, 0.2, 0.2);

                // Apply lighting to the blended color
                half3 lighting = NdotL * mainLight.color.rgb + ambientColor;

                // Apply the properties to the final color
                half3 finalColor = blendedColor.rgb * lighting;
                half4 finalColorWithAlpha = half4(finalColor, blendedColor.a);

                return finalColorWithAlpha;
            }

            ENDHLSL
        }
        }
            FallBack "Diffuse"
}
