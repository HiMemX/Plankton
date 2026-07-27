#version 400 core
#include "common.glsl"

flat out int FlagsOut;
out vec2 UV1;
out vec2 UV2;
out vec2 UV3;
out vec4 Color;
out vec3 Normal;

out vec2 lightkitUV;
flat out int lightkitidx;
out vec4 lightKitLighting;
out float viewDepth;
out vec2 environmentUV;

uniform int Flags;
uniform int ShaderOpsFlags;


//layout (location = 5) in int aFlags; // 8: use Normals, 4: use Colors, 2: has Diffuse, 1: has Lightmap

bool GetBit(int num, int bit){
    return 1 == ((num >> bit) & 1);
}

void main()
{
    UV1 = aUV1;
    UV2 = aUV2;
    UV3 = aUV3;
    Color = vec4(0,0,0,1);
    Normal = normalize(transpose(inverse(mat3(aInstanceMat))) * aNormal);
    FlagsOut = Flags;

    bool useVertexColor = (0x40 & ShaderOpsFlags) != 0;
    bool useLightKit = (0x100 & ShaderOpsFlags) != 0;

    

    if (useVertexColor){
        Color = vec4(1);
        if (GetBit(Flags, 5)){ // Actually has vertex colors supplied
            Color = aColor;    
        }
    }

    lightkitidx = lightkitIndex;
    if(lightkitIndex != -1 && useLightKit) {
        float align1 = dot(Normal, lightkits[lightkitIndex].light1);// / length(Normal) / length(lightkits[lightkitIndex].light1);
        float align2 = dot(Normal, lightkits[lightkitIndex].light2);// / length(Normal) / length(lightkits[lightkitIndex].light2);

        lightkitUV = 0.4375 * vec2(align1, align2) + vec2(0.53125);
        

    
        float align3 = max(0, dot(Normal, lightkits[lightkitIndex].light3));
        float align4 = max(0, dot(Normal, lightkits[lightkitIndex].light4));

        vec4 lightcolor3 = vec4(lightkits[lightkitIndex].color3 * align3 / 2, 1);//, vec4(0,0,0,1), vec4(1,1,1,1));
        vec4 lightcolor4 = vec4(lightkits[lightkitIndex].color4 * align4 / 2, 1);// vec4(0,0,0,1), vec4(1,1,1,1));

        //lightKitLighting = lightkits[lightkitIndex].ambientcolor + lightcolor3 + lightcolor4;
        
        vec4 vertexPreLighting = vec4(lightkits[lightkitIndex].ambientcolor, 1);

        if(useVertexColor){
            vertexPreLighting = Color;
        }

        lightKitLighting = clamp(vertexPreLighting + lightcolor3 + lightcolor4, vec4(0,0,0,1), vec4(1,1,1,1));

        //lightKitLighting = clamp(Color + vec4(lightkits[lightkitIndex].ambientcolor, 1) + lightcolor3 + lightcolor4, vec4(0,0,0,1), vec4(1,1,1,1));

        //Color = clamp(Color, vec4(0,0,0,1), vec4(1,1,1,1));
        
    }

    vec4 viewPosition = (aInstanceMat * vec4(aPosition, 1.0f)) * view;

    viewDepth = max(-viewPosition.z, 0.0);

    
    vec3 N =
        normalize(Normal * mat3(view));

    environmentUV =
        vec2(
             N.x * 0.5 + 0.5,
             N.y * 0.5 + 0.5
        );

    gl_Position =  viewPosition * project;

}
