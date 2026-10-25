#version 330 core
#include "functions.glsl"

out vec4 FragColor;
            
flat in int instanceFlags;
flat in int isLine;
flat in int dmSelected;
flat in int dmUnselected;
in vec3 Normal;
in vec4 Color;

void main()
{
    if( IsSelected(instanceFlags) && !ShouldDrawRegular(dmSelected)) discard;
    if(!IsSelected(instanceFlags) && !ShouldDrawRegular(dmUnselected)) discard;


    vec4 normalInterpolateDark = vec4(0.8, 0.8, 0.8, 1);
    vec4 normalInterpolateLight = vec4(1, 1, 1, 1);
    vec3 lightdir = vec3(0.2f, 1, 0.2f);


    
    float factor = dot(lightdir, Normal) / (length(lightdir) * length(Normal));
    vec4 normalColor = mix(normalInterpolateDark, normalInterpolateLight, factor / 2 + 0.5);

    FragColor = normalColor * Color;

    //FragColor = vec4(IsSelected(instanceFlags), float(dmSelected) / 7.0, float(dmUnselected) / 7.0, 1);
}