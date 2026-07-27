layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec3 aNormal;

layout(location = 2) in mat4 aInstanceMat;
layout(location = 6) in vec4 aColor;
layout(location = 7) in int containerIndex;
layout(location = 8) in int instanceflags;

uniform mat4 view;
uniform mat4 project;
uniform vec4 selectedColor;