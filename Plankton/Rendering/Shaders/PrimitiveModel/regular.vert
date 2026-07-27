#version 330 core
#include "common.glsl"
            

out vec3 Normal;
out vec4 Color;
            
void main()
{
    Normal = transpose(inverse(mat3(aInstanceMat))) * aNormal;
    Color = aColor;
    gl_Position = (aInstanceMat * vec4(aPosition, 1.0f)) * view * project;
}