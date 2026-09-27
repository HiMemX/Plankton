layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec3 aNormal;
layout (location = 2) in vec4 aColor;
layout (location = 3) in vec2 aUV1;
layout (location = 4) in vec2 aUV2;
layout (location = 5) in vec2 aUV3;

layout (location = 6) in mat4 aInstanceMat;
layout(location = 10) in int instanceflags;
layout (location = 11) in int attr;
layout (location = 12) in int childattr; // Unused

layout (location = 13) in int lightkitIndex;

struct lightkit{
	vec3 light1;
	vec3 light2;

	vec3 light3;
	vec3 light4;

	vec3 color3;
	vec3 color4;

	vec3 ambientcolor;
};

uniform lightkit lightkits[12]; // Max count is 12 (I think most levels only have like 2-3) (Haha nope SL07 has 9! Hope that is the max cause I only can use 16 texture units)


uniform mat4 project;
uniform mat4 view;