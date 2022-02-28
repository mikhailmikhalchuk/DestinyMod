sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
float3 uColor;
float3 uSecondaryColor;
float uOpacity : register(C0);
float uSaturation;
float uRotation;
float uTime;
float4 uSourceRect;
float2 uWorldPosition;
float uDirection;
float3 uLightSource;
float2 uImageSize0;
float2 uImageSize1;
float4 uShaderSpecificData;

float4 Tint(float4 oldColor, float4 refColor, float tintProgress)
{
    float4 colorDifference = (refColor - oldColor) * tintProgress;
    return oldColor + colorDifference;
}

float4 EnergySword(float4 position : SV_POSITION, float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    if (!any(color))
    {
        return color;
    }
    float4 noise = tex2D(uImage1, coords.xy + float2(-uShaderSpecificData.y, uShaderSpecificData.z));
    float4 colorTintedBlue = Tint(color, float4(0, 0, 1, 1), uShaderSpecificData.x);
    float4 colorsAdjustedForNoise = Tint(color, noise, uShaderSpecificData.x);
    return colorsAdjustedForNoise * uShaderSpecificData.w;

}

technique Technique1
{
    pass EnergySword
    {
        PixelShader = compile ps_2_0 EnergySword();
    }
}