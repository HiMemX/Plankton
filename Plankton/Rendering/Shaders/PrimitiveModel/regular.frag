#version 330 core

out vec4 FragColor;
            
in vec3 Normal;
in vec4 Color;

void main()
{
    vec4 normalInterpolateDark = vec4(0.8, 0.8, 0.8, 1);
    vec4 normalInterpolateLight = vec4(1, 1, 1, 1);
    vec3 lightdir = vec3(0.2f, 1, 0.2f);

    
    float factor = dot(lightdir, Normal) / (length(lightdir) * length(Normal));
    vec4 normalColor = mix(normalInterpolateDark, normalInterpolateLight, factor / 2 + 0.5);

    FragColor = normalColor * Color;
}