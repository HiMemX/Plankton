#version 330 core
#include "common.glsl"
#include "functions.glsl"



flat out int instanceFlags;
flat out int dmSelected;
flat out int dmUnselected;
flat out int isLine;
flat out int fragAttr;


//layout (location = 5) in int aFlags; // 8: use Normals, 4: use Colors, 2: has Diffuse, 1: has Lightmap


void main()
{
    isLine = isline;
    instanceFlags = instanceflags;
    dmSelected = drawMaskSelected;
    dmUnselected = drawMaskUnselected;
    fragAttr = containerIndex;
    gl_Position = project * view * (aInstanceMat * vec4(aPosition, 1.0f));

}
