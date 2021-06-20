#version 460

layout (location = 0) uniform mat4 inProjectionMatrix;
layout (location = 1) uniform mat4 inModelviewMatrix;

out gl_PerVertex
{
	vec4 gl_Position;
};

void main()
{
    gl_Position = inProjectionMatrix * inModelviewMatrix * vec4(inPosition, 1.0);	
    return;
}