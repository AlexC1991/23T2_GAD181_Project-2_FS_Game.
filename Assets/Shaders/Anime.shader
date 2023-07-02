Shader "Custom/AnimeCartoonPencilShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PencilTex ("Pencil Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineWidth ("Outline Width", Range(0, 1)) = 0.1
    }
 
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100
        
        Cull Off
        ZWrite Off
        ColorMask RGB
 
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
 
            sampler2D _MainTex;
            sampler2D _PencilTex;
            float4 _OutlineColor;
            float _OutlineWidth;
 
            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
 
            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 texColor = tex2D(_MainTex, i.uv);

                // Apply outline
                float2 pixelSize = 1.0 / _ScreenParams.xy;
                fixed4 outline = tex2D(_MainTex, i.uv + float2(0, _OutlineWidth) * pixelSize);
                outline.rgb = _OutlineColor.rgb;
                outline.a = texColor.a;

                // Apply pencil texture overlay
                fixed4 pencilTexColor = tex2D(_PencilTex, i.uv);
                fixed4 pencilEffect = texColor + (1 - texColor.a) * pencilTexColor;

                // Combine texture, outline, and pencil effect
                return outline + pencilEffect;
            }
            ENDCG
        }
    }
}
