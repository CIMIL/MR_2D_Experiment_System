Shader "Unlit/PointCloudNew"
{
    Properties
    {
        _Size("Size", Range(0.1, 2)) = 0.1
        _Testval("Testval", Range(-1, 1)) = 1
        _Testvala("Testvala", Range(-1, 1)) = 1
        _Testvalb("Testvalb", Range(-1, 1)) = 1
        _Testval1("Testval1", Range(-1, 1)) = 1
        _Testval2("Testval2", Range(-1, 1)) = 1
        _XSize("XSize", Range(0.0, 1)) = 1.
        _YSize("YSize", Range(0.0, 1)) = 1.
    }

        SubShader
    {
        Pass
        {
            Cull Off
            CGPROGRAM
            #pragma target 4.0
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct PS_INPUT
            {
                float4 position : SV_POSITION;
                float4 color : COLOR;
                float3 normal : NORMAL;
            };

            float _Size;
            float _Testval;
            float _Testvala;
            float _Testvalb;
            float _Testval1;
            float _Testval2;
            float _XSize;
            float _YSize;

            sampler2D _XYZTex;
            sampler2D _ColorTex;
            float4 _XYZTex_TexelSize;
            float4x4 _Position;

            PS_INPUT vert(appdata_full v, uint vertex_id : SV_VertexID, uint instance_id : SV_InstanceID)
            {
                PS_INPUT o;
                o.normal = v.normal;

                // Compute the UVs
                float2 uvOffset = float2(fmod(instance_id, _XYZTex_TexelSize.z), instance_id / _XYZTex_TexelSize.z);
                float2 uv = uvOffset * _XYZTex_TexelSize.xy;
                uv -= float2(_Testval1, _Testval2);
                uv.x = clamp(uv.x, _XYZTex_TexelSize.x - _Testval1, _XSize - _XYZTex_TexelSize.x - _Testval1);
                uv.y = clamp(uv.y, _XYZTex_TexelSize.y - _Testval2, _YSize - _XYZTex_TexelSize.y - _Testval2);

                // Load the textures
                float4 XYZTexSample = tex2Dlod(_XYZTex, float4(uv, 0.0, 0.0));
                float4 ColorTexSample = tex2Dlod(_ColorTex, float4(uv, 0.0, 0.0));

                // Set the world position
                float4 XYZPos = float4(XYZTexSample.rgb, 1.0f);
                o.position = mul(mul(UNITY_MATRIX_VP, _Position), XYZPos);

                o.color = float4(ColorTexSample.bgr, 1.0f);

                return o;
            }

            fixed4 frag(PS_INPUT i) : SV_Target
            {
                return i.color;
            }

            ENDCG
        }
    }
}

