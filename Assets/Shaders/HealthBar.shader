Shader "Unlit/HealthBar" {
    Properties{ // input data
        [NoScaleOffset] _MainTex("Texture", 2D) = "white" {}
        _Hp("Hp", Range(0,1)) = 1
    }
        SubShader{
            // subshader tags
            Tags {
                "RenderType" = "Opaque" // tag to inform the render pipeline of what type this is
            }
            Pass {


                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"

                sampler2D _MainTex;
                float _Hp;

                // automatically filled out by Unity
                struct MeshData { // per-vertex mesh data
                    float4 vertex : POSITION; // local space vertex position
                    float4 uv0 : TEXCOORD0; // uv0 diffuse/normal map textures
                };

                // data passed from the vertex shader to the fragment shader
                // this will interpolate/blend across the triangle!
                struct Interpolators {
                    float4 vertex : SV_POSITION; // clip space position
                    float2 uv : TEXCOORD1;
                };

                Interpolators vert(MeshData v) {
                    Interpolators o;
                    o.vertex = UnityObjectToClipPos(v.vertex); // local space to clip space
                    o.uv = v.uv0; //(v.uv0 + _Offset) * _Scale; // passthrough
                    return o;
                }

                float4 frag(Interpolators i) : SV_Target{
                    float healthbarMask = i.uv.x < _Hp;

                    clip(healthbarMask - 0.5);

                    float3 colorHp = tex2D(_MainTex, float2(_Hp, i.uv.y));

                    if (_Hp < 0.3) {
                        float flash = cos(_Time.y * 4) * 0.4 + 1;

                        colorHp *= flash;
                    }

                    float4 outColor = float4(colorHp, 1);

                    return outColor;

                }

                ENDCG
            }
        }
}
