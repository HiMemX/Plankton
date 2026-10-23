#version 330 core

uniform mat4 view;
uniform mat4 projection;

out vec3 nearPoint;
out vec3 farPoint;

void main()
{
    // Fullscreen triangle
    vec2 positions[3] = vec2[](
        vec2(-1.0, -1.0),
        vec2( 3.0, -1.0),
        vec2(-1.0,  3.0)
    );

    vec2 pos = positions[gl_VertexID];

    mat4 invViewProj = inverse(projection * view);

    // Unproject near and far clip-space positions
    vec4 nearWorld = invViewProj * vec4(pos, -1.0, 1.0);
    vec4 farWorld  = invViewProj * vec4(pos,  1.0, 1.0);

    nearPoint = nearWorld.xyz / nearWorld.w;
    farPoint  = farWorld.xyz / farWorld.w;

    gl_Position = vec4(pos, 0.0, 1.0);
}