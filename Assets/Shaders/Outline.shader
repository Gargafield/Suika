Shader "Custom/Outline" {
    Properties {
        _WireColor ("Wire Color", Color) = (1,1,1,1)
        _WireThickness ("Wire Thickness", Range(0,10)) = 1
    }
    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        LOD 100

        Pass {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            fixed4 _WireColor;
            float _WireThickness;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                float2 uv = i.uv;
                float edgeWidth = 0.05 * _WireThickness / 10.0;
                
                float left = uv.x <= edgeWidth;
                float right = uv.x >= (1 - edgeWidth);
                float top = uv.y >= (1 - edgeWidth);
                float bottom = uv.y <= edgeWidth;

                float wire = (left || right) || (top || bottom);
                
                return fixed4(_WireColor.rgb, wire * _WireColor.a);
            }
            ENDCG
        }
    }
}
