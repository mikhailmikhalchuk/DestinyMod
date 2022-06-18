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

float4 White(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 baseColor = tex2D(uImage0, coords);
    float averageColor = (baseColor.r + baseColor.g + baseColor.g) / 3;
    float averageColorButBrighter = averageColor * 2;
    return float4(averageColorButBrighter, averageColorButBrighter, averageColorButBrighter, averageColorButBrighter);
}
    
technique Technique1
{
    pass White
    {
        PixelShader = compile ps_2_0 White();
    }
}