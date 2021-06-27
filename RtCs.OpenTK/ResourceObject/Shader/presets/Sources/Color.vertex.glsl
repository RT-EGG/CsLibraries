#version 460

layout (location = 0) uniform mat4 inProjectionMatrix;
layout (location = 1) uniform mat4 inModelviewMatrix;

layout (location = 0) in vec3 inPosition;

out gl_PerVertex
{
	vec4 gl_Position;
};

void main()
{
    gl_Position = inProjectionMatrix * inModelviewMatrix * vec4(inPosition, 1.0);	
    return;
}