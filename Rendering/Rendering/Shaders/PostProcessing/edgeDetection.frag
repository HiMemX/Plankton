#version 330 core

out vec4 FragColor;

uniform sampler2DMS inputTex;
uniform int sampleCount;

uniform vec4 edgeColor;
uniform vec4 activeEdgeColor;

struct SampleData
{
    uvec3 id;
    bool isActive;
};

SampleData getSample(ivec2 pos, int sampleIndex)
{
    vec4 value = texelFetch(inputTex, pos, sampleIndex);

    SampleData result;
    result.id = uvec3(round(value.rgb * 255.0));
    result.isActive = value.a >= 0.5;

    return result;
}

bool differentId(uvec3 a, uvec3 b)
{
    return any(notEqual(a, b));
}

bool isBackground(uvec3 id)
{
    return all(equal(id, uvec3(0)));
}

void main()
{
    ivec2 size = textureSize(inputTex);
    ivec2 pos = ivec2(gl_FragCoord.xy);

    ivec2 neighbors[4] = ivec2[]
    (
        ivec2(max(pos.x - 1, 0), pos.y),
        ivec2(min(pos.x + 1, size.x - 1), pos.y),
        ivec2(pos.x, max(pos.y - 1, 0)),
        ivec2(pos.x, min(pos.y + 1, size.y - 1))
    );

    float normalCoverage = 0.0;
    float activeCoverage = 0.0;

    for (int sample = 0; sample < sampleCount; sample++)
    {
        SampleData center = getSample(pos, sample);

        for (int n = 0; n < 4; n++)
        {
            SampleData neighbor = getSample(neighbors[n], sample);

            if (differentId(center.id, neighbor.id))
            {
                bool centerIsBackground = isBackground(center.id);

                // If this sample is inside an object, color the edge
                // according to that object's active state.
                if (!centerIsBackground)
                {
                    if (center.isActive)
                        activeCoverage += 1.0;
                    else
                        normalCoverage += 1.0;
                }
                // If this sample is background, color the edge according
                // to the object on the other side.
                else
                {
                    if (neighbor.isActive)
                        activeCoverage += 1.0;
                    else
                        normalCoverage += 1.0;
                }

                break;
            }
        }
    }

    normalCoverage /= float(sampleCount);
    activeCoverage /= float(sampleCount);


    float totalCoverage = normalCoverage + activeCoverage;

    if (totalCoverage <= 0.0)
    {
        FragColor = vec4(0.0);
        return;
    }

    vec3 mixedColor =
        (
            edgeColor.rgb * normalCoverage +
            activeEdgeColor.rgb * activeCoverage
        ) / totalCoverage;

    float mixedAlpha = max(
        edgeColor.a * normalCoverage,
        activeEdgeColor.a * activeCoverage
    );

    FragColor = vec4(mixedColor, mixedAlpha);
}