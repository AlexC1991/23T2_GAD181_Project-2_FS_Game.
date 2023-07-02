Shader "Custom/CartoonyShader" {
    Properties {
        _MainTex ("Main Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineThickness ("Outline Thickness", Range(0, 0.1)) = 0.01
    }
 
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200
 
        CGPROGRAM
        #pragma surface surf Lambert
        
        sampler2D _MainTex;
        fixed4 _OutlineColor;
        half _OutlineThickness;
        
        struct Input {
            float2 uv_MainTex;
        };
 
        void surf (Input IN, inout SurfaceOutput o) {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
            o.Emission = float3(_OutlineColor.rgb * step(_OutlineThickness, length(fwidth(IN.uv_MainTex))));
        }
        ENDCG
    }
    FallBack "Diffuse"
}
