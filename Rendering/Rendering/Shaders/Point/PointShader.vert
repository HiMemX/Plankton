#version 330 core
layout(location = 0) in vec3 aPosition;
            
uniform vec3 uPosition;
uniform mat4 view;
uniform mat4 project;


            
void main()
{
    gl_Position = project * view * vec4(uPosition, 1.0f);
}