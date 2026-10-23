#version 330 core

in vec3 nearPoint;
in vec3 farPoint;

out vec4 FragColor;

uniform mat4 view;
uniform mat4 projection;

// --------------------------------------------
// Configuration
// --------------------------------------------

const float BASE_SPACING = 1.0;
const float GRID_DIVISIONS = 10.0;

// Brightness of minor and major grid lines
const float MINOR_OPACITY = 0.23;
const float MAJOR_OPACITY = 0.35;

// Grid color
const vec3 GRID_COLOR = vec3(0.65);

// --------------------------------------------
// Pixel-filtered grid
// --------------------------------------------

float gridCoverage(
    vec2 worldPos,
    vec2 worldPixelSize,
    float spacing
)
{
    // Grid coordinates at this scale
    vec2 uv = worldPos / spacing;

    // Crucially, derivatives are calculated
    // BEFORE applying the changing LOD scale.
    vec2 pixelSize = max(
        worldPixelSize / spacing,
        vec2(0.000001)
    );

    // Distance to the nearest grid line
    vec2 dist = abs(
        fract(uv + 0.5) - 0.5
    );

    // Convert line distance into screen pixels
    vec2 distPixels = dist / pixelSize;

    // Approximately one-pixel-wide lines
    vec2 coverage = 1.0 - smoothstep(
        vec2(0.0),
        vec2(0.85),
        distPixels
    );

    // Fade out this resolution before its
    // cells become too small to resolve.
    float density = max(
        pixelSize.x,
        pixelSize.y
    );

    float densityFade = 1.0 - smoothstep(
        0.25,
        0.65,
        density
    );

    return max(coverage.x, coverage.y)
         * densityFade;
}

// --------------------------------------------
// Colored world axes
// --------------------------------------------

float axisCoverage(float coordinate, float pixelSize)
{
    float distPixels = abs(coordinate)
                     / max(pixelSize, 0.000001);

    return 1.0 - smoothstep(
        0.0,
        1.2,
        distPixels
    );
}

// --------------------------------------------
// Main
// --------------------------------------------

void main()
{
    // ----------------------------------------
    // Ray-plane intersection
    // ----------------------------------------

    vec3 rayDir = farPoint - nearPoint;

    if (abs(rayDir.y) < 0.000001)
        discard;

    // Infinite XZ plane at Y = 0
    float t = -nearPoint.y / rayDir.y;

    if (t <= 0.0 || t >= 1.0)
        discard;

    vec3 worldPos = nearPoint + rayDir * t;

    // ----------------------------------------
    // Calculate screen-space derivatives
    // ----------------------------------------

    // Calculate these once in world space.
    // Do not differentiate LOD-scaled UVs.
    vec2 dx = dFdx(worldPos.xz);
    vec2 dy = dFdy(worldPos.xz);

    vec2 worldPixelSize = max(
        vec2(
            length(vec2(dx.x, dy.x)),
            length(vec2(dx.y, dy.y))
        ),
        vec2(0.000001)
    );

    // Largest world-space pixel footprint
    float footprint = max(
        worldPixelSize.x,
        worldPixelSize.y
    );

    // ----------------------------------------
    // Continuous logarithmic LOD
    // ----------------------------------------

    // Select a new scale when the previous
    // grid approaches a few pixels per cell.
    float lod = max(
        0.0,
        log(footprint * 4.0 / BASE_SPACING)
            / log(GRID_DIVISIONS)
    );

    float level = floor(lod);

    // Gradually transition between levels
    float blend = smoothstep(
        0.0,
        0.55,
        fract(lod)
    );

    // Three consecutive grid resolutions
    float spacing0 =
        BASE_SPACING * pow(GRID_DIVISIONS, level);

    float spacing1 =
        spacing0 * GRID_DIVISIONS;

    float spacing2 =
        spacing1 * GRID_DIVISIONS;

    // ----------------------------------------
    // Evaluate grid resolutions
    // ----------------------------------------

    float grid0 = gridCoverage(
        worldPos.xz,
        worldPixelSize,
        spacing0
    );

    float grid1 = gridCoverage(
        worldPos.xz,
        worldPixelSize,
        spacing1
    );

    float grid2 = gridCoverage(
        worldPos.xz,
        worldPixelSize,
        spacing2
    );

    // ----------------------------------------
    // Blend between grid resolutions
    // ----------------------------------------

    // Fine grid transitions into the next
    // resolution as its cells get smaller.
    float minor = mix(
        grid0,
        grid1,
        blend
    );

    // Major divisions are one level larger.
    // They are only slightly more prominent.
    float major = mix(
        grid1,
        grid2,
        blend
    );

    float alpha = max(
        minor * MINOR_OPACITY,
        major * MAJOR_OPACITY
    );

    vec3 color = GRID_COLOR;

    // ----------------------------------------
    // World axes
    // ----------------------------------------

    // X axis (Z = 0)
    float xAxis = axisCoverage(
        worldPos.z,
        worldPixelSize.y
    );

    // Z axis (X = 0)
    float zAxis = axisCoverage(
        worldPos.x,
        worldPixelSize.x
    );

    // Red X axis
    color = mix(
        color,
        vec3(0.8, 0, 0),
        xAxis
    );

    // Green Z axis
    color = mix(
        color,
        vec3(0, 0, 0.8),
        zAxis
    );

    alpha = max(
        alpha,
        max(xAxis, zAxis) * 0.75
    );

    // ----------------------------------------
    // Horizon fade

    // ----------------------------------------

    // Fade toward grazing viewing angles.
    float horizonAngle = abs(
        normalize(rayDir).y
    );

    float horizonFade = smoothstep(
        0.005,
        0.045,
        horizonAngle
    );

    alpha *= horizonFade;

    if (alpha < 0.001)
        discard;

    // ----------------------------------------
    // Output
    // ----------------------------------------

    FragColor = vec4(color, alpha);

    // Correct depth for the ground plane
    vec4 clipPos =
        projection * view * vec4(worldPos, 1.0);

    gl_FragDepth =
        (clipPos.z / clipPos.w) * 0.5 + 0.5;
}