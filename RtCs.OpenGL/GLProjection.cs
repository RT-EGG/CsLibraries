using RtCs.MathUtils;
using System;

namespace RtCs.OpenGL
{
    /// <summary>
    /// Base class of projection.
    /// </summary>
    public abstract class GLProjection
    {
        /// <summary>
        /// Projection matrix made from the projection settings.
        /// </summary>
        public abstract Matrix4x4 ProjectionMatrix
        { get; }
    }

    /// <summary>
    /// Ortho projection.
    /// </summary>
    public sealed class GLOrthoProjection : GLProjection
    {
        /// <summary>
        /// Left edge position of near clip plane.
        /// </summary>
        public float Left
        { get; set; } = -1.0f;
        /// <summary>
        /// Right edge position of near clip plane.
        /// </summary>
        public float Right
        { get; set; } = 1.0f;
        /// <summary>
        /// Bottom edge position of near clip plane.
        /// </summary>
        public float Bottom
        { get; set; } = -1.0f;
        /// <summary>
        /// Top edge position of near clip plane.
        /// </summary>
        public float Top
        { get; set; } = 1.0f;

        /// <summary>
        /// Distance to near clip plane.
        /// </summary>
        public float Near
        { get; set; } = 0.01f;
        /// <summary>
        /// Distance to far clip plane.
        /// </summary>
        public float Far
        { get; set; } = 100.0f;

        public void SetSymmetrical(float inWidth, float inHeight)
        {
            Left = -inWidth * 0.5f;
            Right = inWidth * 0.5f;
            Bottom = -inHeight * 0.5f;
            Top = inHeight * 0.5f;
        }

        public override Matrix4x4 ProjectionMatrix
            => Matrix4x4.MakeOrtho(Left, Right, Bottom, Top, Near, Far);
    }

    /// <summary>
    /// Perspective projection.
    /// </summary>
    public sealed class GLPerspectiveProjection : GLProjection
    {
        /// <summary>
        /// Left edge position of near clip plane.
        /// </summary>
        public float Left
        { get; set; } = -1.0f;
        /// <summary>
        /// Right edge position of near clip plane.
        /// </summary>
        public float Right
        { get; set; } = 1.0f;
        /// <summary>
        /// Bottom edge position of near clip plane.
        /// </summary>
        public float Bottom
        { get; set; } = -1.0f;
        /// <summary>
        /// Top edge position of near clip plane.
        /// </summary>
        public float Top
        { get; set; } = 1.0f;

        /// <summary>
        /// Distance to near clip plane.
        /// </summary>
        public float Near
        { get; set; } = 0.01f;
        /// <summary>
        /// Distance to far clip plane.
        /// </summary>
        public float Far
        { get; set; } = 100.0f;

        /// <summary>
        /// Set near plane rect.
        /// </summary>
        /// <param name="inFovYinDeg">Vertical angle of field of view in degrees. It must be bigger than 0 and smaller than 180.</param>
        /// <param name="inFovXinDeg">Horizontal angle of field of view in degrees. It must be bigger than 0 and smaller than 180.</param>
        /// <param name="inOffsetX">Horizontal offset from center of near clip plane.</param>
        /// <param name="inOffsetY">Vertical offset from center of near clip plane.</param>
        public void SetAngles(float inFovYinDeg, float inFovXinDeg, float inOffsetX = 0.0f, float inOffsetY = 0.0f)
        {
            if ((inFovYinDeg <= 0.0f) || (inFovYinDeg >= 180.0f)) {
                throw new ArgumentOutOfRangeException($"{nameof(inFovYinDeg)} must be bigger than 0 and smaller than 180.");
            }
            if ((inFovXinDeg <= 0.0f) || (inFovXinDeg >= 180.0f)) {
                throw new ArgumentOutOfRangeException($"{nameof(inFovXinDeg)} must be bigger than 0 and smaller than 180.");
            }

            float y = (float)Math.Tan(inFovYinDeg.DegToRad() * 0.5f) * Near;
            float x = (float)Math.Tan(inFovXinDeg.DegToRad() * 0.5f) * Near;

            Left = inOffsetX - x;
            Right = inOffsetX + x;
            Bottom = inOffsetY - y;
            Top = inOffsetY + y;
            return;
        }

        /// <summary>
        /// Set near plane rect.
        /// </summary>
        /// <param name="inFovYinDeg">Vertical angle of field of view in degrees. It must be bigger than 0 and smaller than 180.</param>
        /// <param name="inAspectRatio">Aspect ratio of near clip plane.</param>
        /// <param name="inOffsetX">Horizontal offset from center of near clip plane.</param>
        /// <param name="inOffsetY">Vertical offset from center of near clip plane.</param>
        public void SetAngleAndAspectRatio(float inFovYinDeg, float inAspectRatio, float inOffsetX = 0.0f, float inOffsetY = 0.0f)
        {
            if ((inFovYinDeg <= 0.0f) || (inFovYinDeg >= 180.0f)) {
                throw new ArgumentOutOfRangeException($"{nameof(inFovYinDeg)} must be bigger than 0 and smaller than 180.");
            }

            float y = (float)Math.Tan(inFovYinDeg.DegToRad() * 0.5f) * Near;
            float x = y * inAspectRatio;

            Left = inOffsetX - x;
            Right = inOffsetX + x;
            Bottom = inOffsetY - y;
            Top = inOffsetY + y;
        }

        /// <summary>
        /// Set near plane rect.
        /// </summary>
        /// <param name="inFovYinDeg">Vertical angle of field of view in degrees. It must be bigger than 0 and smaller than 180.</param>
        /// <param name="inViewWidth">Width of projection target (e.g. OpenGL canvas.).</param>
        /// <param name="inViewHeight">Height of projection target (e.g. OpenGL canvas.).</param>
        /// <param name="inOffsetX">Horizontal offset from center of near clip plane.</param>
        /// <param name="inOffsetY">Vertical offset from center of near clip plane.</param>
        public void SetAngleAndViewportSize(float inFovYinDeg, float inViewWidth, float inViewHeight, float inOffsetX = 0.0f, float inOffsetY = 0.0f)
            => SetAngleAndAspectRatio(inFovYinDeg, inViewWidth / inViewHeight, inOffsetX, inOffsetY);

        public override Matrix4x4 ProjectionMatrix
            => Matrix4x4.MakePerspective(Left, Right, Bottom, Top, Near, Far);
    }
}
