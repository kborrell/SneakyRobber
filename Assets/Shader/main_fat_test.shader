// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Experiments/Main_fat_test" 
{
    Properties 
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _GradientTex ("Local Gradient (RGB)", 2D) = "white" {}
       	_DiffuseTex ("Diffuse Gradient (RGB)", 2D) = "white" {}

        _VertexColor ("Vertex Color", Color) = (1,1,1,1)
        _DiffuseColor ("Diffuse Color", Color) = (1,1,1,1)

       	_SpecColor ("Specular Color", Color) = (1,1,1,1) 
        _SpecularIntencity ("Specular Intencity", Range (0, 10)) = 1
       	_SpecularGloss ("Specular Gloss", Range (0, 200)) = 1
		_fatness ("fat", range (0, 0.5)) = 0
    }
    Category
    {

        Tags 
        {
            "Queue" = "Geometry"
            "RenderType"="Opaque"
            "LightMode"="ForwardBase"
        }

		SubShader 
		{
			Pass
			{
				ZWrite On
				Cull Off
				CGPROGRAM

				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest
				#pragma glsl_no_auto_normalization
				#include "UnityCG.cginc"

				sampler2D _MainTex;
				half4 _MainTex_ST;

				sampler2D _GradientTex;
				half4 _GradientTex_ST;

				sampler2D _DiffuseTex;
				half4 _DiffuseTex_ST;

				fixed4 _VertexColor;
				fixed4 _DiffuseColor;

				fixed4 _SpecColor;
				float _SpecularIntencity;
				float _SpecularGloss;
				float _fatness;

				uniform float4 _LightColor0; 


				struct Input
				{
					float4 pos		: SV_POSITION;
					half2 uv		: TEXCOORD0;
					half2 uv2		: TEXCOORD1;

					float4 posWorld 	: TEXCOORD2;
					float3 normalDir 	: TEXCOORD3;
					half2 diffuseUV 	: TEXCOORD4;
					half2 diffuse 		: TEXCOORD5;
		   		};



				Input vert(appdata_full a)
				{
					Input o;
					o.pos = UnityObjectToClipPos(a.vertex + a.normal*_fatness);
					o.uv = TRANSFORM_TEX(a.texcoord, _MainTex);
					o.uv2 = TRANSFORM_TEX(a.texcoord2, _GradientTex);

					float4x4 modelMatrix = unity_ObjectToWorld;
	    			float4x4 modelMatrixInverse = unity_WorldToObject;

	    			float3 normalDirection = normalize(mul(float4(a.normal, 0.0), modelMatrixInverse).xyz);
					float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);

					float diffuse = dot(normalDirection, lightDirection);
					float diffuse01 = (diffuse + 1) * 0.5;  // [-1, 1] => [0, 1]

					o.posWorld = mul(modelMatrix, a.vertex);
					o.normalDir = normalDirection; 

					o.diffuse = half2(clamp(-diffuse, 0, 1), diffuse01);

					float4 tex = a.texcoord2 - float4(0.5, 0.5, 0, 0);
					o.diffuseUV = mul(unity_WorldToObject, tex).xy + float2(0.5, 0.5);

					float4 worldVertex = mul(unity_ObjectToWorld, a.vertex);
					float4 worldCenter = mul(unity_ObjectToWorld, float4(0,0,0,1));
					float offsetY = worldVertex.y - worldCenter.y;
					float scaleUV = clamp(offsetY + 0.5, 0, 1);
					o.diffuseUV = half2(scaleUV, scaleUV);

					return o;
				}



				fixed4 frag(Input i):COLOR
				{
					float3 normalDirection = normalize(i.normalDir);
					float3 viewDirection = normalize(_WorldSpaceCameraPos - i.posWorld.xyz);
					float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);

					float lightProjection = max(0, dot(normalDirection, lightDirection));
					float3 lightReflection = reflect(-lightDirection, normalDirection);
					float dotDirection = max(0.0, dot(lightReflection, viewDirection));
					float specular = pow(dotDirection, _SpecularGloss);

					float4 specularReflection = _SpecColor * _LightColor0 * _SpecularIntencity * specular * lightProjection;

					fixed4 mainColor = tex2D(_MainTex, i.uv);
					fixed4 gradientColor = tex2D(_GradientTex, i.uv2);
					fixed4 diffuseColor = 
//					lerp(fixed4(1,0,0,1), fixed4(0,1,0,1), i.diffuseUV.y);
					tex2D(_DiffuseTex, i.diffuseUV);
					diffuseColor.rgb = lerp(fixed3(1,1,1), diffuseColor.rgb, i.diffuse.x);
//					diffuseColor.rgb *= diffuseColor.rgb;
//					if(-i.diffuse.x > 0)
//					{
//						diffuseColor.rgb = (0,0,0);
//					}

					fixed4 resultColor = mainColor * gradientColor * diffuseColor * _LightColor0 * _VertexColor + specularReflection;
					return resultColor;
				}
				ENDCG
			}
		}
    }
}

