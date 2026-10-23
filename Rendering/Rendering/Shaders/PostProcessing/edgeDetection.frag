#version 330 core

in vec2 TexCoord;
out vec4 FragColor;

uniform sampler2D inputTex;
uniform vec2 texelSize;
uniform vec4 edgeColor;

void main()
{
    float up    = texture(inputTex, TexCoord + vec2(0.0, -texelSize.y)).a;
    float down  = texture(inputTex, TexCoord + vec2(0.0,  texelSize.y)).a;
    float left  = texture(inputTex, TexCoord + vec2(-texelSize.x, 0.0)).a;
    float right = texture(inputTex, TexCoord + vec2( texelSize.x, 0.0)).a;

    float diff =
        abs(right - left) +
        abs(up - down);

    float edge = clamp(diff, 0.0, 1.0);

    FragColor = vec4(
        edgeColor.rgb,
        edgeColor.a * edge
    );
}