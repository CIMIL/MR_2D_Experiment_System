Shader "Unlit/MyPointCloudShader"
//======= Copyright (c) Stereolabs Corporation, All rights reserved. ===============
//Displays point cloud though geometry
//Shader "ZED/ZED PointCloud"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Size("Size", Range(0.1,2)) = 0.1
		_Testval("Testval", Range(-1,1)) = 1
		_Testvala("Testvala", Range(-1,1)) = 1
		_Testvalb("Testvalb", Range(-1,1)) = 1
	    _Testval1("Testval1", Range(1,-1)) = 1
		_Testval2("Testval2", Range(1,-1)) = 1
		_XSize("XSize", Range(0.0,1)) = 1.
		_YSize("YSize", Range(0.0,1)) = 1.
	}
		SubShader
		{


			Pass
			{
				Cull Off
				CGPROGRAM
				#pragma target 5.0
				#pragma vertex vert
				#pragma fragment frag


				#include "UnityCG.cginc"


				struct PS_INPUT
				{
					float4 position : SV_POSITION;
					float4 color : COLOR;
					float3 normal : NORMAL;

				};

				sampler2D _MainTex;
				float4 _MainTex_ST;

				sampler2D _XYZTex;
				sampler2D _ColorTex;
				float4 _XYZTex_TexelSize;
				float4x4 _Position;

				float _Size;
				float _Testval;
				float _Testvala;
				float _Testvalb;
				float _Testval1;
				float _Testval2;
				float _XSize;
				float _YSize;

				PS_INPUT vert(appdata_full v, uint vertex_id : SV_VertexID, uint instance_id : SV_InstanceID)
				{
					PS_INPUT o;
					o.normal = v.normal;

					//Compute the UVS
					
					float2 uv = float2(
								clamp(fmod(instance_id, ((_XYZTex_TexelSize.z * _Testval)* ((_XYZTex_TexelSize.z * _Testvala)))) * _XYZTex_TexelSize.x - _Testval1, _XYZTex_TexelSize.x - _Testval1, _XSize - _XYZTex_TexelSize.x - _Testval1),
								clamp(((instance_id - fmod(instance_id, (_XYZTex_TexelSize.z * _Testval)) - _XYZTex_TexelSize.x - _Testval1) / _XYZTex_TexelSize.z * _Testval)
									* _XYZTex_TexelSize.y - _Testval2, _XYZTex_TexelSize.y - _Testval2, _YSize - _XYZTex_TexelSize.y - _Testval2)
								);
					

					/*
					float2 uv = float2(
								clamp(fmod(instance_id, ((_XYZTex_TexelSize.z * (_Testval1)))) * _XYZTex_TexelSize.x + _Testval, _XYZTex_TexelSize.x + _Testval, 1 - _XYZTex_TexelSize.x + _Testval),
								clamp(((instance_id - fmod(instance_id, _XYZTex_TexelSize.z * ((_Testval1))) - _XYZTex_TexelSize.x - _Testval) / _XYZTex_TexelSize.z * ((_Testval1)))
									* _XYZTex_TexelSize.y - _Testval, _XYZTex_TexelSize.y - _Testval, 1.0 - _XYZTex_TexelSize.y - _Testval)
								);

					*/



					//Load the texture
					float4 XYZPos = float4(tex2Dlod(_XYZTex, float4(uv, 0.0, 0.0)).rgb ,1.0f);

					//Set the World pos
					o.position = mul(mul(UNITY_MATRIX_VP, _Position), XYZPos);

					o.color = float4(tex2Dlod(_ColorTex, float4(uv, 0.0, 0.0)).bgr ,1.0f);

					return o;
				}

				struct gs_out {
					float4 position : SV_POSITION;
					float4 color : COLOR;
				};



				fixed4 frag(PS_INPUT i) : SV_Target
				{
					return i.color;
				}
				ENDCG
			}
		}
}
