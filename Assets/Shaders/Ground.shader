Shader "Custom/CartoonyShader" {
    Properties {
        _MainTex ("Main Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineThickness ("Outline Thickness", Range(0, 0.1)) = 0.01
    }

    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200

        HLSLINCLUDE
        #include <UnityPBSLighting.cginc>

        #include "UnityCG.cginc"
        #include "Library/PackageCache/com.unity.textmeshpro@3.0.6/Editor Resources/Shaders/TMP_Properties.cginc"

        struct Input {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutputStandard o) {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);

            // Toon shading
            float toonLevel = dot(c.rgb, float3(0.3, 0.59, 0.11));
            float toonThreshold = 0.2;
            o.Albedo = lerp(float3(0.0, 0.0, 0.0), float3(1.0, 1.0, 1.0), step(toonThreshold, toonLevel));

            o.Alpha = c.a;

            int _OutlineThickness;
            // Outline effect
            float outline = step(_OutlineThickness, length(fwidth(IN.uv_MainTex)));
            o.Emission = _OutlineColor.rgb * outline;
        }
        ENDHLSL
    }

    FallBack "Diffuse"
}
