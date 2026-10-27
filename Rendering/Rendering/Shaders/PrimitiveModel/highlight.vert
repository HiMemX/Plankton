#version 330 core
#include "common.glsl"
#include "functions.glsl"

out vec4 solidcolor;
flat out int instanceFlags;
flat out int dmSelected;
flat out int dmUnselected;

uniform vec4 customColor;
uniform int useCustomColor;


//layout (location = 5) in int aFlags; // 8: use Normals, 4: use Colors, 2: has Diffuse, 1: has Lightmap


void main()
{
    instanceFlags = instanceflags;
    dmSelected = drawMaskSelected;
    dmUnselected = drawMaskUnselected;
    
    //solidcolor = selectedColor;
    if(useCustomColor == 1){
        if((instanceFlags & 2) != 0) solidcolor = activeColor;
        else solidcolor = customColor;
    }
    else{
        int attr = containerIndex;
        solidcolor = vec4(attr & 0xFF, (attr >> 8) & 0xFF, (attr >> 16) & 0xFF,  0xFF * ((instanceflags & 2) >> 1)) / 255.0f;//color;
    }

    //if((instanceflags & 1) == 1){
    gl_Position = project * view * (aInstanceMat * vec4(aPosition, 1.0f));
    //}
    //else{
    //    gl_Position = vec4(0,0,0,0); // This is done to protect the stencil buffer from writes which aren't from a selected object.
    //}
}