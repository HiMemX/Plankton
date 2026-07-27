#version 400 core
#include "common.glsl"


uniform vec4 color;

out vec4 solidcolor;
out uint fragFlags;



void main()
{
    fragFlags = instanceflags;
    solidcolor = vec4(attr & 0xFF, (attr >> 8) & 0xFF, (attr >> 16) & 0xFF, 0xFF) / 255.0f;//color;

    if((instanceflags & 1) == 1){
        gl_Position = (aInstanceMat * vec4(aPosition, 1.0f)) * view * project;
    }
    else{
        gl_Position = vec4(0,0,0,0); // This is done to protect the stencil buffer from writes which aren't from a selected object.
    }

}