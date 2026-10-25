#version 330 core
#include "functions.glsl"

flat in int instanceFlags;
flat in int dmSelected;
flat in int dmUnselected;
in vec4 solidcolor;
out vec4 FragColor;



void main()
{
    if((IsSelected(instanceFlags) && !ShouldDrawHighlight(dmSelected)) || (!IsSelected(instanceFlags) && !ShouldDrawHighlight(dmUnselected))) discard;

	FragColor = solidcolor;
}