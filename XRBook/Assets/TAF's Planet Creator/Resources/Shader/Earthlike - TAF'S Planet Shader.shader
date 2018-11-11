
Shader "TAF's Planet Shader/Planet - Earthlike" {
    Properties {
        _LandColor ("LandColor", Color) = (0.6191834,0.6985294,0.1232699,1)
        _WaterColor ("WaterColor", Color) = (0,0.5034485,1,1)
        _IceColor ("IceColor", Color) = (0.6764706,0.9330628,1,1)
        _AtmosphereColor ("AtmosphereColor", Color) = (0.8823529,0.9415822,1,1)
        _SurfaceMap ("SurfaceMap", 2D) = "black" {}
        _SpecColor ("SpecColor", Color) = (0.282353,0.5607843,0.6705883,1)
        _SpecPower ("SpecPower", Range(0, 1)) = 0.7280501
        _GlossPower ("GlossPower", Range(0, 1)) = 0.3191741
        _RimColor ("RimColor", Color) = (0.2509804,0.5372549,0.6313726,1)
        _RimThickness ("RimThickness", Range(0, 14)) = 10.9
        _RimPower ("RimPower", Float ) = 1.7
        _IlluminColor ("IlluminColor", Color) = (1,0.7941177,0.4852941,1)
        _IlluminMap ("IlluminMap", 2D) = "black" {}
        _CloudsColor ("CloudsColor", Color) = (1,1,1,1)
        _CloudsMap ("CloudsMap", 2D) = "black" {}
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
            uniform sampler2D _SurfaceMap; uniform float4 _SurfaceMap_ST;
            uniform sampler2D _IlluminMap; uniform float4 _IlluminMap_ST;
            uniform float4 _IlluminColor;
            uniform float _SpecPower;
            uniform float _GlossPower;
            uniform float4 _SpecColor;
            uniform float4 _AtmosphereColor;
            uniform sampler2D _CloudsMap; uniform float4 _CloudsMap_ST;
            uniform float4 _LandColor;
            uniform float4 _IceColor;
            uniform float4 _WaterColor;
            uniform float4 _CloudsColor;
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
                float4 _SurfaceMap_var = tex2D(_SurfaceMap,TRANSFORM_TEX(i.uv0, _SurfaceMap));
                float gloss = (_SurfaceMap_var.a*_GlossPower);
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float4 _CloudsMap_var = tex2D(_CloudsMap,TRANSFORM_TEX(i.uv0, _CloudsMap));
                float3 specularColor = (((_SurfaceMap_var.a*_SpecPower)*_SpecColor.rgb)*(1.0 - _CloudsMap_var.a));
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 node_1706 = (_AtmosphereColor.rgb*lerp((((_IceColor.rgb*_SurfaceMap_var.b)+(_LandColor.rgb*_SurfaceMap_var.g)+(_WaterColor.rgb*(1.0 - _SurfaceMap_var.g)*(1.0 - _SurfaceMap_var.b))+_SurfaceMap_var.r)*(_SurfaceMap_var.r*1.3)),(_CloudsColor.rgb*_CloudsMap_var.rgb),_CloudsMap_var.a));
                float3 diffuseColor = node_1706;
                float3 diffuse = directDiffuse * diffuseColor;
////// Emissive:
                float4 _IlluminMap_var = tex2D(_IlluminMap,TRANSFORM_TEX(i.uv0, _IlluminMap));
                float node_1186 = dot(i.normalDir,lightDirection);
                float node_2506 = (1.0 - node_1186);
                float3 node_9067 = (pow(1.0-max(0,dot(normalDirection, viewDirection)),_RimThickness)*_RimPower*node_1186*_RimColor.rgb);
                float3 node_8383 = (node_9067+node_9067);
                float3 emissive = ((_IlluminColor.rgb*_IlluminMap_var.rgb*(node_2506*node_2506))+(node_8383+node_8383)+(node_1706*_ShadowSideIntensity));
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
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal xboxone ps4 psp2 n3ds wiiu 
            #pragma target 2.0
            uniform float4 _LightColor0;
            uniform float4 _RimColor;
            uniform float _RimThickness;
            uniform float _RimPower;
            uniform sampler2D _SurfaceMap; uniform float4 _SurfaceMap_ST;
            uniform sampler2D _IlluminMap; uniform float4 _IlluminMap_ST;
            uniform float4 _IlluminColor;
            uniform float _SpecPower;
            uniform float _GlossPower;
            uniform float4 _SpecColor;
            uniform float4 _AtmosphereColor;
            uniform sampler2D _CloudsMap; uniform float4 _CloudsMap_ST;
            uniform float4 _LandColor;
            uniform float4 _IceColor;
            uniform float4 _WaterColor;
            uniform float4 _CloudsColor;
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
                float4 _SurfaceMap_var = tex2D(_SurfaceMap,TRANSFORM_TEX(i.uv0, _SurfaceMap));
                float gloss = (_SurfaceMap_var.a*_GlossPower);
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float4 _CloudsMap_var = tex2D(_CloudsMap,TRANSFORM_TEX(i.uv0, _CloudsMap));
                float3 specularColor = (((_SurfaceMap_var.a*_SpecPower)*_SpecColor.rgb)*(1.0 - _CloudsMap_var.a));
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 node_1706 = (_AtmosphereColor.rgb*lerp((((_IceColor.rgb*_SurfaceMap_var.b)+(_LandColor.rgb*_SurfaceMap_var.g)+(_WaterColor.rgb*(1.0 - _SurfaceMap_var.g)*(1.0 - _SurfaceMap_var.b))+_SurfaceMap_var.r)*(_SurfaceMap_var.r*1.3)),(_CloudsColor.rgb*_CloudsMap_var.rgb),_CloudsMap_var.a));
                float3 diffuseColor = node_1706;
                float3 diffuse = directDiffuse * diffuseColor;
////// Emissive:
				float4 _IlluminMap_var = tex2D(_IlluminMap, TRANSFORM_TEX(i.uv0, _IlluminMap));
				float node_1186 = dot(i.normalDir, lightDirection);
				float node_2506 = (1.0 - node_1186);
				float3 node_9067 = (pow(1.0 - max(0, dot(normalDirection, viewDirection)), _RimThickness)*_RimPower*node_1186*_RimColor.rgb);
				float3 node_8383 = (node_9067 + node_9067);
				float3 emissive = ((_IlluminColor.rgb*_IlluminMap_var.rgb*(node_2506*node_2506)) + (node_8383 + node_8383) + (node_1706*_ShadowSideIntensity));
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
