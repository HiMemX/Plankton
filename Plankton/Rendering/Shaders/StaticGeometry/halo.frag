#version 400 core


flat in uint fragFlags;

in vec4 solidcolor;
out vec4 FragColor;



void main()
{
    
    if((fragFlags & 1) == 1){
        FragColor = solidcolor;
    }
}