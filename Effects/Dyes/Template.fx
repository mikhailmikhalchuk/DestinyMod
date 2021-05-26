sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
float3 uColor;
float3 uSecondaryColor;
float uOpacity;
float uSaturation;
float uRotation;
float uTime;
float4 uSourceRect;
float2 uWorldPosition;
float uDirection;
float3 uLightSource;
float2 uImageSize0;
float2 uImageSize1;

float4 GGDye(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 colour = tex2D(uImage0, coords);
    float luminosity = (colour.r + colour.g + colour.b) / 3;
    colour.rgb *= ((coords.x * uColor) + ((1 - coords.x) * uSecondaryColor)) * luminosity;
    return colour * sampleColor;
}

technique Technique1
{
    pass GuardianGamesDyePass
    {
        PixelShader = compile ps_2_0 GGDye();
    }
}