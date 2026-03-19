Shader "Custom/SkyboxBlended" {
    Properties {
        _Blend ("Blend", Range(0, 1)) = 0
        _Skybox1 ("Skybox 1 (Cubemap)", Cube) = "white" {}
        _Skybox2 ("Skybox 2 (Cubemap)", Cube) = "white" {}
    }
    SubShader {
        Tags { "Queue"="Background" "RenderType"="Background" "PreviewType"="Skybox" }
        Cull Off ZWrite Off

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float3 texcoord : TEXCOORD0;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float3 texcoord : TEXCOORD0;
            };

            samplerCUBE _Skybox1;
            samplerCUBE _Skybox2;
            float _Blend;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                fixed4 tex1 = texCUBE(_Skybox1, i.texcoord);
                fixed4 tex2 = texCUBE(_Skybox2, i.texcoord);
                return lerp(tex1, tex2, _Blend);
            }
            ENDCG
        }
    }
}
