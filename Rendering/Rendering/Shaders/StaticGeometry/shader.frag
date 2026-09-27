#version 400 core


in vec2 UV1;
in vec2 UV2;
in vec2 UV3;
in vec4 Color;
in vec3 Normal;
flat in int FlagsOut;

in vec2 lightkitUV;
flat in int lightkitidx;
in vec4 lightKitLighting;
in float viewDepth;
in vec2 environmentUV;

out vec4 FragColor;

uniform sampler2D diffuseMap;
uniform sampler2D lightMap;
uniform sampler2D environmentMap;
uniform sampler2D diffuseMap1;
uniform sampler2D blendMap;
uniform sampler2D lightkitLookups[12];

uniform vec4 ambientScale;
uniform float environmentScale;

uniform int ShaderOpsFlags;

struct FogInfo{
    vec4 fogColor;
    float start;
    float end;
    int fogMode; // 0 == None, 1 == Linear, etc.
};
uniform FogInfo fog;

const int FOG_NONE        = 0;
const int FOG_LINEAR      = 1;
const int FOG_EXPONENTIAL = 2;
const int FOG_EXPONENT2   = 3;
const int FOG_REVERSE_EXP = 4;
const int FOG_REVERSE_EXP2 = 5;

float calculateFogCoordinate(
    float viewDepth)
{
    float fogRange = fog.end - fog.start;

    if (abs(fogRange) < 1e-6)
        return viewDepth >= fog.end ? 1.0 : 0.0;

    return clamp(
        (viewDepth - fog.start) / fogRange,
        0.0,
        1.0
    );
}

float applyFogCurve(float x)
{
    x = clamp(x, 0.0, 1.0);

    switch (fog.fogMode)
    {
        case FOG_LINEAR:
            return x;

        // GX-style exponential curve.
        case FOG_EXPONENTIAL:
            return 1.0 - exp2(-8.0 * x);

        case FOG_EXPONENT2:
            return 1.0 - exp2(-8.0 * x * x);

        case FOG_REVERSE_EXP:
            return exp2(-8.0 * (1.0 - x));

        case FOG_REVERSE_EXP2:
        {
            float reverseX = 1.0 - x;
            return exp2(-8.0 * reverseX * reverseX);
        }

        default:
            return 0.0;
    }
}

float calculateFogFactor(float viewDepth)
{
    if (fog.fogMode == FOG_NONE)
        return 0.0;

    float coordinate = calculateFogCoordinate(
        viewDepth
    );

    float factor = applyFogCurve(coordinate);

    return clamp(factor, 0.0, 1.0);
}

bool GetBit(int num, int bit){
    return 1 == ((num >> bit) & 1);
}

void main()
{
    bool useLightMap = (ShaderOpsFlags & 0x02) != 0;
    bool useDiffuseMap = (ShaderOpsFlags & 0x01) != 0;
    //bool useUV3 = GetBit(FlagsOut, 2);
    //bool useUV2 = GetBit(FlagsOut, 3);
    //bool useUV1 = GetBit(FlagsOut, 4);
    bool useVertexColor = (ShaderOpsFlags & 0x40) != 0;
    bool useLightKits = (ShaderOpsFlags & 0x100) != 0;
    bool useEnvironmentMap = (ShaderOpsFlags & 0x04) != 0;
    bool useBlendMap = (ShaderOpsFlags & 0x100000) != 0; // Might be the other way around
    bool useDiffuseMap1 = (ShaderOpsFlags & 0x080000) != 0; // These two

    vec4 diffuseColor = vec4(1,1,1,1);
    vec4 lightMapColor = vec4(1,1,1,1);
    vec4 environmentMapColor = vec4(0,0,0,0);
    vec4 vertexColor = vec4(0,0,0,0);
    vec4 diffuse1Color = vec4(1,1,1,1);
    float blendValue = 0;
    //vec4 normalColor = vec4(1,1,1,1);

    FragColor = vec4(0);
    
    if(useLightMap){
        lightMapColor = texture(lightMap, UV2);
    }
    if(useDiffuseMap){
        diffuseColor = texture(diffuseMap, UV1);
    }
    if(useDiffuseMap1){
        diffuse1Color = texture(diffuseMap1, UV1);
    }
    if(useBlendMap){
        if(useLightMap) blendValue = texture(blendMap, UV3).r;
        else{ blendValue = texture(blendMap, UV2).r; }
    }

    if(useEnvironmentMap){
        environmentMapColor = texture(environmentMap, environmentUV);
    }


    FragColor = vec4(useLightKits, (ShaderOpsFlags & 0x40) != 0, 0, 1);
    
    //return;



    if(useBlendMap && useDiffuseMap1) diffuseColor = mix(diffuseColor,diffuse1Color, blendValue);
    
    FragColor = ambientScale * diffuseColor * lightMapColor;
    

    vec3 combinedLighting = vec3(0);

    if (useVertexColor){
        combinedLighting = Color.rgb;
    }

    if (lightkitidx != -1 && (useLightKits)){
        
        
        // Indexing directly yields glitchy results so this is the workaround
        if(lightkitidx == 0) vertexColor = texture(lightkitLookups[0], lightkitUV);
        else if(lightkitidx == 1) vertexColor = texture(lightkitLookups[1], lightkitUV);
        else if(lightkitidx == 2) vertexColor = texture(lightkitLookups[2], lightkitUV);
        else if(lightkitidx == 3) vertexColor = texture(lightkitLookups[3], lightkitUV);
        else if(lightkitidx == 4) vertexColor = texture(lightkitLookups[4], lightkitUV);
        else if(lightkitidx == 5) vertexColor = texture(lightkitLookups[5], lightkitUV);
        else if(lightkitidx == 6) vertexColor = texture(lightkitLookups[6], lightkitUV);
        else if(lightkitidx == 7) vertexColor = texture(lightkitLookups[7], lightkitUV);
        else if(lightkitidx == 8) vertexColor = texture(lightkitLookups[8], lightkitUV);
        else if(lightkitidx == 9) vertexColor = texture(lightkitLookups[9], lightkitUV);
        else if(lightkitidx == 10) vertexColor = texture(lightkitLookups[10], lightkitUV);
        else if(lightkitidx == 11) vertexColor = texture(lightkitLookups[11], lightkitUV);

        combinedLighting = lightKitLighting.rgb + vertexColor.rgb;
        
    }

    if(useVertexColor || useLightKits){
        FragColor.rgb *= 2 * clamp(combinedLighting, 0, 1);
    }
    
    if(!useLightKits && !useVertexColor && !useLightMap) FragColor.rgb = vec3(0);


    FragColor.rgb = clamp(FragColor.rgb + environmentScale * environmentMapColor.rgb, 0, 1);
    
    float fogFactor = calculateFogFactor(viewDepth);

    vec3 foggedRgb = mix(
        FragColor.rgb,
        fog.fogColor.rgb,
        fogFactor
    );

    FragColor.rgb = foggedRgb;
    

}