Shader "Unlit/TestWindow"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}
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

            float4 _MainTex_ST;

    // Helper functions
    float SmoothStep(float edge0, float edge1, float x)
    {
        float t = saturate((x - edge0) / (edge1 - edge0));
        return t * t * (3.0 - 2.0 * t);
    }

    float B(float a, float b, float edge, float t)
    {
        return SmoothStep(a - edge, a + edge, t) * SmoothStep(b + edge, b - edge, t);
    }

    float3 N31(float p)
    {
        // Corrected line
        float3 p3 = frac(p * float3(0.1031, 0.11369, 0.13787));
        p3 += dot(p3, p3.yzx + 19.19);
        return frac(float3((p3.x + p3.y) * p3.z, (p3.x + p3.z) * p3.y, (p3.y + p3.z) * p3.x));
    }

    float DistLine(float3 ro, float3 rd, float3 p)
    {
        return length(cross(p - ro, rd));
    }

    float Remap(float a, float b, float c, float d, float t)
    {
        return ((t - a) / (b - a)) * (d - c) + c;
    }

    // GetDrops simulation (rain-like effect)
    float2 GetDrops(float2 uv, float seed, float m)
    {
        float t = _Time.y + m * 30.0;
        uv.y += t * 0.05;
        uv *= float2(10.0, 2.5) * 2.0;

        float2 id = floor(uv);
        float3 n = N31(id.x + (id.y + seed) * 546.3524);
        float2 bd = frac(uv);
        bd -= 0.5;
        bd.y *= 4.0;
        bd.x += (n.x - 0.5) * 0.6;

        t += n.z * 6.28;
        float slide = cos(t + cos(t)) + sin(2.0 * t) * 0.2 + sin(4.0 * t) * 0.02;

        bd.y += slide * 2.0;
        float d = length(bd);

        float mainDrop = SmoothStep(0.2, 0.1, d);
        return bd * mainDrop;
    }

    // Camera setup for perspective
    void CameraSetup(float2 uv, float3 pos, float3 lookat, float zoom, float m, out float3 ro, out float3 rd)
    {
        ro = pos;
        float3 f = normalize(lookat - ro);
        float3 r = cross(float3(0.0, 1.0, 0.0), f);
        float3 u = cross(f, r);

        float2 offs = GetDrops(uv, 1.0, m);
        float3 center = ro + f * zoom;
        float3 i = center + (uv.x - offs.x) * r + (uv.y - offs.y) * u;

        rd = normalize(i - ro);
    }

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

    v2f vert(appdata v)
    {
        v2f o;
        o.vertex = UnityObjectToClipPos(v.vertex);
        o.uv = TRANSFORM_TEX(v.uv, _MainTex);
        return o;
    }

    // Fragment shader
    fixed4 frag(v2f i) : SV_Target
    {
        float2 uv = i.uv;

        // Setup the camera (we'll use the camera setup logic)
        float3 ro, rd;
        CameraSetup(uv, float3(0.0, 1.0, 5.0), float3(0.0, 1.0, 0.0), 2.0, 0.5, ro, rd);

        // Headlight effect (simplified)
        float3 headlightColor = float3(0.8, 0.8, 1.0);
        float3 headlight = headlightColor * smoothstep(0.0, 1.0, uv.y);

        return fixed4(headlight, 1.0);
    }
    ENDCG
}
    }
        FallBack "Diffuse"
}