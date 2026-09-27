#version 400 core


flat in uint fragAttr;
out vec4 FragColor;



void main()
{

    FragColor = vec4(fragAttr & 0xFF, (fragAttr >> 8) & 0xFF, (fragAttr >> 16) & 0xFF, 0xFF) / 255.0f;
}