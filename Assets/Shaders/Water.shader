Shader "Custom/StylizedWater" {
    Properties {
        _Color ("Water Color", Color) = (1, 1, 1, 1)
        _FoamColor ("Foam Color", Color) = (1, 1, 1, 1)
        _FoamThreshold ("Foam Threshold", Range(0, 1)) = 0.5
        _WaveSpeed ("Wave Speed", Range(0, 10)) = 1
        _WaveStrength ("Wave Strength", Range(0, 1)) = 0.5
        _WaveFrequency ("Wave Frequency", Range(0, 10)) = 1
        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineWidth ("Outline Width", Range(0, 0.1)) = 0.01
        _FoamTexture ("Foam Texture", 2D) = "white" {}
        _FoamScrollSpeed ("Foam Scroll Speed", Range(-10, 10)) = 1
        _RandomOffsetSpeed ("Random Offset Speed", Range(0, 10)) = 1
    }
    
    SubShader {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
        LOD 200
        
        ZWrite Off
        Cull Off
        Lighting Off
        
        Pass {
            Blend One OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"
            
            struct appdata {
                float4 vertex : POSITION;
            };
            
            struct v2f {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };
            
            fixed4 _Color;
            fixed4 _FoamColor;
            float _FoamThreshold;
            float _WaveSpeed;
            float _WaveStrength;
            float _WaveFrequency;
            fixed4 _OutlineColor;
            float _OutlineWidth;
            sampler2D _FoamTexture;
            float _FoamScrollSpeed;
            float _RandomOffsetSpeed;
            
            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.vertex.xy;
                return o;
            }
            
            float2 RandomOffset(float2 uv, float time, float speed, float offsetSpeed) {
                float2 offset = float2(sin(time * speed), cos(time * speed));
                float2 randomOffset = normalize(uv - 0.5) * offset * offsetSpeed;
                return uv + randomOffset;
            }
            
            fixed4 frag (v2f i) : SV_Target {
                float2 uv = i.uv * 0.01;  // Adjust tiling based on your needs
                
                float waveTime = _Time.y * _WaveSpeed;
                float2 waveOffset = float2(cos(waveTime), sin(waveTime));
                float2 distortion = waveOffset * _WaveStrength * sin(uv.x * _WaveFrequency + uv.y * _WaveFrequency);
                
                float distFromCenter = length(i.uv - 0.5);
                float outline = smoothstep(1.0 - _OutlineWidth, 1.0, distFromCenter);
                
                float foam = smoothstep(_FoamThreshold, _FoamThreshold + _OutlineWidth, distFromCenter);
                
                fixed4 col = _Color;
                col.rgb *= 1.0 - outline;
                col.rgb += _OutlineColor.rgb * outline;
                
                fixed4 foamColor = _FoamColor;
                
                float2 foamUV = i.uv * _FoamScrollSpeed + waveTime * _FoamScrollSpeed * 0.1;
                float foamTextureValue = tex2D(_FoamTexture, foamUV).r;
                
                foamUV = RandomOffset(foamUV, _Time.y, _FoamScrollSpeed, _RandomOffsetSpeed);
                
                foamColor.a *= foam * foamTextureValue;
                col = lerp(col, foamColor, foam);
                
                return col;
            }
            ENDCG
        }
    }
}
