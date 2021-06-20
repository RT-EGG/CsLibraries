#version 460

out vec4 outColor;

layout (location = 2) uniform vec4 inColor;

void main()
{
    outColor = inColor;
    return;
}