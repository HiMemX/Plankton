#version 330 core
#include "common.glsl"
#include "functions.glsl"
            
flat out int instanceFlags;
flat out int dmSelected;
flat out int dmUnselected;
flat out int isLine;
out vec3 Normal;
out vec4 Color;
            
void main()
{
    isLine = isline;
    instanceFlags = instanceflags;
    dmSelected = drawMaskSelected;
    dmUnselected = drawMaskUnselected;
    Normal = transpose(inverse(mat3(aInstanceMat))) * aNormal;
    Color = aColor;
    gl_Position = project * view * (aInstanceMat * vec4(aPosition, 1.0f));
}