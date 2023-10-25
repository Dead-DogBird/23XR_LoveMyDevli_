Shader "Custom/MaskedShader"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _MaskTex ("Mask Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags {"Queue"="Overlay" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
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
            sampler2D _MaskTex;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 mask = tex2D(_MaskTex, i.uv);

                if (mask.a > 0.5) {
                    fixed4 col = tex2D(_MainTex, i.uv);
                    return col;
                }
                else {
                    return fixed4(0, 0, 0, 0); // Fully transparent
                }
            }
            ENDCG
        }
    }

    FallBack "Sprites/Default"
}