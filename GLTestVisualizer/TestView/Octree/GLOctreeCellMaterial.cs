﻿using OpenTK.Graphics.OpenGL4;
using RtCs.MathUtils;
using RtCs.OpenGL;

namespace GLTestVisualizer.TestView.Octree
{
    class GLOctreeCellMaterial : GLMaterial
    {
        public GLOctreeCellMaterial()
        {
            base.Shader = GLRenderShaderProgram.Preset.Color;

            RenderLevel = EGLRenderLevel.Transparent;
            BlendParameters = new GLBlendParameters {
                SourceFactor = BlendingFactor.SrcAlpha,
                DestinationFactor = BlendingFactor.OneMinusSrcAlpha
            };
            return;
        }

        public override void CommitProperties(GLRenderingStatus inRenderingStatus)
        {
            GetProperty<Vector4>("inColor").Value = Color;
            GetProperty<Matrix4x4>("inProjectionMatrix").Value = inRenderingStatus.ProjectionMatrix.CurrentMatrix;
            GetProperty<Matrix4x4>("inModelviewMatrix").Value = inRenderingStatus.ModelViewMatrix.CurrentMatrix;

            base.CommitProperties(inRenderingStatus);
            return;
        }

        public Vector4 Color
        { get; set; } = new Vector4(1.0);
        private new GLRenderShaderProgram Shader => base.Shader;
    }
}