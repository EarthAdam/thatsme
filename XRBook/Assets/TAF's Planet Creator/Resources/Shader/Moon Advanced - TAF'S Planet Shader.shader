
Shader "TAF's Planet Shader/Moon - Advanced" {
    Properties {
        _Surface ("Surface", 2D) = "black" {}
        _PrimaryColor ("Primary Color", Color) = (0.4705882,0.3702422,0.3702422,1)
        _SecondaryColor ("Secondary Color", Color) = (0.4874567,0.629543,0.6764706,1)
        _SpecColor ("SpecColor", Color) = (0.3912197,0.6954486,0.7941176,1)
        _SpecPower ("SpecPower", Range(0, 1)) = 0.2641694
        _GlossPower ("GlossPower", Range(0, 1)) = 0.6670839
        _RimColor ("RimColor", Color) = (0.2811419,0.3447967,0.3676471,1)
        _RimThickness ("RimThickness", Range(0, 14)) = 7.472434
        _RimPower ("RimPower", Float ) = 1.7
        _IlluminMap ("IlluminMap", 2D) = "black" {}
        _IlluminColor ("IlluminColor", Color) = (1,0.7941177,0.4852941,1)
        _ShadowSideIntensity ("ShadowSideIntensity", Range(0, 0.2)) = 0.05
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal xboxone ps4 psp2 n3ds wiiu 
            #pragma target 2.0
            uniform float4 _LightColor0;
            uniform float4 _RimColor;
            uniform float _RimThickness;
            uniform float _RimPower;
            uniform sampler2D _IlluminMap; uniform float4 _IlluminMap_ST;
            uniform float4 _IlluminColor;
            uniform float _SpecPower;
            uniform float _GlossPower;
            uniform float4 _SpecColor;
            uniform sampler2D _Surface; uniform float4 _Surface_ST;
            uniform float4 _PrimaryColor;
            uniform float4 _SecondaryColor;
            uniform float _ShadowSideIntensity;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float4 _Surface_var = tex2D(_Surface,TRANSFORM_TEX(i.uv0, _Surface));
                float gloss = (_Surface_var.b*_GlossPower);
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float3 specularColor = ((_Surface_var.b*_SpecPower)*_SpecColor.rgb);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 node_8059 = lerp((_PrimaryColor.rgb*_Surface_var.r),(_SecondaryColor.rgb*_Surface_var.g),_Surface_var.g);
                float3 diffuseColor = node_8059;
                float3 diffuse = directDiffuse * diffuseColor;
////// Emissive:
                float4 _IlluminMap_var = tex2D(_IlluminMap,TRANSFORM_TEX(i.uv0, _IlluminMap));
                float node_1186 = dot(i.normalDir,lightDirection);
                float node_2506 = (1.0 - node_1186);
                float3 node_9067 = (pow(1.0-max(0,dot(normalDirection, viewDirection)),_RimThickness)*_RimPower*node_1186*_RimColor.rgb);
                float3 node_8383 = (node_9067+node_9067);
                float3 emissive = ((_IlluminColor.rgb*_IlluminMap_var.rgb*(node_2506*node_2506*1.3))+(node_8383+node_8383)+(node_8059*_ShadowSideIntensity));
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal xboxone ps4 psp2 n3ds wiiu 
            #pragma target 2.0
            uniform float4 _LightColor0;
            uniform float4 _RimColor;
            uniform float _RimThickness;
            uniform float _RimPower;
            uniform sampler2D _IlluminMap; uniform float4 _IlluminMap_ST;
            uniform float4 _IlluminColor;
            uniform float _SpecPower;
            uniform float _GlossPower;
            uniform float4 _SpecColor;
            uniform sampler2D _Surface; uniform float4 _Surface_ST;
            uniform float4 _PrimaryColor;
            uniform float4 _SecondaryColor;
            uniform float _ShadowSideIntensity;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float4 _Surface_var = tex2D(_Surface,TRANSFORM_TEX(i.uv0, _Surface));
                float gloss = (_Surface_var.b*_GlossPower);
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float3 specularColor = ((_Surface_var.b*_SpecPower)*_SpecColor.rgb);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 node_8059 = lerp((_PrimaryColor.rgb*_Surface_var.r),(_SecondaryColor.rgb*_Surface_var.g),_Surface_var.g);
                float3 diffuseColor = node_8059;
                float3 diffuse = directDiffuse * diffuseColor;
////// Emissive:
				float4 _IlluminMap_var = tex2D(_IlluminMap, TRANSFORM_TEX(i.uv0, _IlluminMap));
				float node_1186 = dot(i.normalDir, lightDirection);
				float node_2506 = (1.0 - node_1186);
				float3 node_9067 = (pow(1.0 - max(0, dot(normalDirection, viewDirection)), _RimThickness)*_RimPower*node_1186*_RimColor.rgb);
				float3 node_8383 = (node_9067 + node_9067);
				float3 emissive = ((_IlluminColor.rgb*_IlluminMap_var.rgb*(node_2506*node_2506*1.3)) + (node_8383 + node_8383) + (node_8059*_ShadowSideIntensity));
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
