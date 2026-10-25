#version 330 core

#include "functions.glsl"

flat in int instanceFlags;
flat in int dmSelected;
flat in int dmUnselected;
flat in int isLine;
flat in int fragAttr;
out vec4 FragColor;



void main()
{
    
    if((IsSelected(instanceFlags) && !ShouldDrawSelected(dmSelected)) || (!IsSelected(instanceFlags) && !ShouldDrawSelected(dmUnselected))) discard;


    FragColor = vec4(fragAttr & 0xFF, (fragAttr >> 8) & 0xFF, (fragAttr >> 16) & 0xFF, 0xFF) / 255.0f;
}