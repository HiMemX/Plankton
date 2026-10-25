#version 330 core

out vec4 FragColor;

uniform sampler2DMS inputTex;
uniform int sampleCount;
uniform vec4 edgeColor;

uvec3 getId(ivec2 pos, int sampleIndex)
{
    vec3 value = texelFetch(
        inputTex,
        pos,
        sampleIndex
    ).rgb;

    // Your IDs are stored in an RGBA8 texture,
    // so convert the normalized 0..1 values back to bytes.
    return uvec3(round(value * 255.0));
}

bool different(uvec3 a, uvec3 b)
{
    return any(notEqual(a, b));
}

void main()
{
    ivec2 size = textureSize(inputTex);
    ivec2 pos = ivec2(gl_FragCoord.xy);

    ivec2 left  = ivec2(max(pos.x - 1, 0), pos.y);
    ivec2 right = ivec2(min(pos.x + 1, size.x - 1), pos.y);
    ivec2 down  = ivec2(pos.x, max(pos.y - 1, 0));
    ivec2 up    = ivec2(pos.x, min(pos.y + 1, size.y - 1));

    float edgeSamples = 0.0;

    for (int i = 0; i < sampleCount; i++)
    {
        uvec3 centerId = getId(pos, i);

        bool edge =
            different(centerId, getId(left, i)) ||
            different(centerId, getId(right, i)) ||
            different(centerId, getId(up, i)) ||
            different(centerId, getId(down, i));

        if (edge)
            edgeSamples += 1.0;
    }

    float coverage =
        edgeSamples / float(sampleCount);

    FragColor = vec4(
        edgeColor.rgb,
        edgeColor.a * coverage);
}