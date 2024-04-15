Shader"Custom/CartoonShader"
{
    Properties
    {
        _DiffuseColor("DiffuseColor", Color) = (1,1,0,1)
        _LightDirection("LightDirection", Vector) = (1,-1,-1,0)
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

#include "UnityCG.cginc"

struct appdata
{
    float4 vertex : POSITION;
    float3 normal : NORMAL;
};

struct v2f
{
    float4 vertex : SV_POSITION;
    float3 normal : NORMAL;
};

float4 _DiffuseColor;
float4 _LightDirection;

v2f vert(appdata v)
{
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);
    o.normal = v.normal;
    return o;
}

fixed4 frag(v2f i) : SV_Target
{
    float3 normal = normalize(i.normal);
    float3 lightDir = normalize(_LightDirection.xyz);
    float diff = max(dot(normal, lightDir), 0.0);

                // 조명값을 기준으로 각 색상을 선택합니다.
    float3 color;
    if (diff > 0.8)
    {
        color = float3(1.0, 1.0, 0.0); // 노란색
    }
    else if (diff > 0.2)
    {
        color = float3(0.8, 0.8, 0.0); // 연한 노란색
    }
    else
    {
        color = float3(0.0, 0.0, 0.0); // 검은색
    }

    return fixed4(color * _DiffuseColor.rgb, 1.0);
}
ENDCG
}
    }
}