#version 330 core
#include "common.glsl"



out int fragAttr;


//layout (location = 5) in int aFlags; // 8: use Normals, 4: use Colors, 2: has Diffuse, 1: has Lightmap


void main()
{
    fragAttr = containerIndex;
    gl_Position = (aInstanceMat * vec4(aPosition, 1.0f)) * view * project;

}