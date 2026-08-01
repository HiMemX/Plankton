#version 330 core
layout(location = 0) in vec4 aPosition;
            
uniform mat4 uTransformation;
uniform mat4 view;
uniform mat4 project;


            
void main()
{
    gl_Position = (uTransformation * aPosition) * view * project;
}