Shader "phongShader"
{
    Properties
    {
        _Color("Main Color", Color) = (1.0, 0.0, 0.0, 1.0)
        _AmbientColor("Ambient Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _LightIntensity("Light Intensity", Float) = 1.0  // 추가된 빛의 세기 변수
    }
    SubShader
    {
        Pass
        {
            Tags{ "LightMode" = "ForwardBase" }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

#include "UnityCG.cginc"
#include "Lighting.cginc"

float4 _Color;
float4 _AmbientColor;
float _LightIntensity; // 빛의 세기 변수

struct appdata
{
    float4 vertex : POSITION;
    float3 normal : NORMAL;
};

struct v2f
{
    float4 vertex : SV_POSITION;
    float3 worldNormal : TEXCOORD0;
    float3 viewDir : TEXCOORD1;
    float3 lightDir : TEXCOORD2;
};

v2f vert(appdata v)
{
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);
    o.worldNormal = UnityObjectToWorldNormal(v.normal);
    float3 lightDir = normalize(_WorldSpaceLightPos0);
    o.lightDir = lightDir;
    o.viewDir = normalize(_WorldSpaceCameraPos - o.worldNormal);
    return o;
}

fixed4 frag(v2f i) : SV_Target
{
    // 주변광 계산
    float4 ambientReflection = _AmbientColor * _LightIntensity;

    // 확산광 계산
    float3 worldNormal = normalize(i.worldNormal);
    float3 lightDir = i.lightDir;
    float3 diffuseReflection = _LightColor0.rgb * saturate(dot(worldNormal, lightDir)) * _LightIntensity;

    // 반사광 계산
    float3 reflectedDir = reflect(-lightDir, worldNormal);
    float3 viewDir = normalize(_WorldSpaceCameraPos - worldNormal);
    float reflectIntensity = saturate(dot(reflectedDir, viewDir));
    float n = 4.0;
    reflectIntensity = pow(reflectIntensity, n);
    float3 specularReflection = _LightColor0.rgb * reflectIntensity * _LightIntensity;

    fixed4 result = _Color * float4(ambientReflection + diffuseReflection, 1.0) + float4(specularReflection, 1.0);

    float threshold = 0.4;
    float3 banding = floor(result / threshold);
    float3 finalIntensity = banding * threshold;

    return fixed4(finalIntensity.x, finalIntensity.y, finalIntensity.z, 1.0); // 최종 결과 색상 반환
}
ENDCG
        }
    }
}