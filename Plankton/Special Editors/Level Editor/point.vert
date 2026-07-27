#version 330 core
layout (location = 0) in vec3 aPosition;



uniform mat4 project;
uniform mat4 view;
uniform vec4 color;

out vec4 solidcolor;



//layout (location = 5) in int aFlags; // 8: use Normals, 4: use Colors, 2: has Diffuse, 1: has Lightmap


void main()
{
    solidcolor = color;

    gl_Position = vec4(0, 0, 0, 1) * view * project; // //vec4(aPosition, 1.0f) 
    gl_PointSize = 10.0; 
}