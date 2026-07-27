#version 330 core
#include "common.glsl"
            

out vec4 solidcolor;

uniform vec4 customColor;
uniform int useCustomColor;

//layout (location = 5) in int aFlags; // 8: use Normals, 4: use Colors, 2: has Diffuse, 1: has Lightmap


void main()
{

    
    //solidcolor = selectedColor;
    if(useCustomColor == 1){
        solidcolor = customColor;
    }
    else{
        int attr = containerIndex;
        solidcolor = vec4(attr & 0xFF, (attr >> 8) & 0xFF, (attr >> 16) & 0xFF, 0xFF) / 255.0f;//color;
    }

    if((instanceflags & 1) == 1){
        gl_Position = (aInstanceMat * vec4(aPosition, 1.0f)) * view * project;
    }
    else{
        gl_Position = vec4(0,0,0,0); // This is done to protect the stencil buffer from writes which aren't from a selected object.
    }
}