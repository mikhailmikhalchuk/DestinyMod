sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
sampler uImage2 : register(s2);
sampler uImage3 : register(s3);
float3 uColor;
float uOpacity;
float3 uSecondaryColor;
float uTime;
float2 uScreenResolution;
float2 uScreenPosition;
float2 uTargetPosition;
float2 uImageOffset;
float uIntensity;
float uProgress;
float2 uDirection;
float2 uZoom;
float2 uImageSize1;
float2 uImageSize2;
float2 uImageSize3;

//----------------------------

const float PI = 3.1415926538; // How does HLSL not have a default const for PI?

bool active; // I like having an active variable in screenshaders because sometimes they'll take a noticable time to deactivate 

// r in these variables meaning ripple
float rCount;
float rSize;
float rSpeed;

float4 Shockwave(float2 coords : TEXCOORD0) : COLOR0
{
    if (!active)
    {
        return tex2D(uImage0, coords);
    }

    float2 targetCoords = (uTargetPosition - uScreenPosition) / uScreenResolution;
    float2 centerCoords = (coords - targetCoords) * (uScreenResolution / uScreenResolution.y);
    float dotField = dot(centerCoords, centerCoords);
    float ripple = dotField * rSize * PI - uProgress * rSpeed;

    if (ripple < 0 && ripple > rCount * -2 * PI)
    {
        ripple = saturate(sin(ripple));
    }
    else
    {
        ripple = 0;
    }

    float2 sampleCoords = coords + ((ripple * uOpacity / uScreenResolution) * centerCoords);

    return tex2D(uImage0, sampleCoords);
}

technique Technique1
{
    pass Shockwave
    {
        PixelShader = compile ps_2_0 Shockwave();
    }
}