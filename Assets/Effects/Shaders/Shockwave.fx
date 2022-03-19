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
float rStrength;
float rSize;
float rBuffer;
float rDiameter;
float rCount;
float rCountScale;

float4 Shockwave(float4 position : SV_POSITION, float2 coords : TEXCOORD0) : COLOR0
{
    float screenRatio = uScreenResolution.y / uScreenResolution.x;
    float2 targetPixelPos = (uTargetPosition - uScreenPosition) / uScreenResolution;
    float2 targetScreenCoords = (targetPixelPos - float2(0, 0.5)) * float2(1, screenRatio) + float2(0, 0.5);
    float2 screenCoords = (coords - float2(0, 0.5)) * float2(1, screenRatio) + float2(0, 0.5);
    float distance = length(screenCoords - targetScreenCoords);
    float mask = (1 - smoothstep(rSize - rBuffer, rSize, distance)) * smoothstep(rSize - rDiameter - rBuffer, rSize - rDiameter, distance);
    /*float totalIterations = 0;
    for (float applyCount = 0; applyCount < rCount; applyCount++)
    {
        if (++totalIterations > 5)
        {
            break;
        }

        float adjustedRSize = rSize - rCountScale * applyCount;
        float smoothenDistance = smoothstep(adjustedRSize - rHarshness, adjustedRSize, distance);
        if (applyCount % 2 == 0)
        {
            mask *= 1 - smoothenDistance;
        }
        else
        {
            mask *= smoothenDistance;
        }
    }*/

    float2 centreCoords = saturate(sin(screenCoords - targetScreenCoords)) * rStrength * mask; // normalize(screenCoords - targetScreenCoords) * rStrength * mask; // Also is a thing you can try
    //return float4(float3(mask, mask, mask), 1);
    return tex2D(uImage0, coords - centreCoords);
}

technique Technique1
{
    pass Shockwave
    {
        PixelShader = compile ps_2_0 Shockwave();
    }
}