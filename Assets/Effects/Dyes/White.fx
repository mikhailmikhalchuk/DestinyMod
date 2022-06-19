sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
float3 uColor;
float uSaturation;
float3 uSecondaryColor;
float uTime;
float uOpacity;
float2 uTargetPosition;
float4 uSourceRect;
float4 uLegacyArmorSourceRect;
float2 uWorldPosition;
float2 uImageSize0;
float2 uLegacyArmorSheetSize;
float uRotation;
float uDirection;
float2 uImageSize1;

float4 Tint(float4 oldColor, float4 refColor, float tintProgress)
{
    float4 colorDifference = (refColor - oldColor) * tintProgress;
    return oldColor + colorDifference;
}

float4 White(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 baseColor = tex2D(uImage0, coords);
    if (baseColor.a == 0)
    {
        return baseColor;
    }
    float averageColor = (baseColor.r + baseColor.g + baseColor.g) / 3;
    float4 averageColor4 = float4(averageColor, averageColor, averageColor, baseColor.a);
    return Tint(averageColor4, float4(1, 1, 1, baseColor.a), uSaturation);
}
    
technique Technique1
{
    pass White
    {
        PixelShader = compile ps_2_0 White();
    }
}