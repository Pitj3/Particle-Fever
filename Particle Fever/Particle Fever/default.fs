﻿#version 330 core

layout (location = 0) out vec4 color;

void main()
{
	vec4 texColor = vec4(1, 0, 1, 1);

	color = texColor;
}