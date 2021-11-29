namespace RtCs.OpenGL.WinForms.Texture.Text
{
    /// <summary>
    /// Various size information of the character rendered by specified font.
    /// </summary>
    public class CharacterMetrics
    {
        /// <summary>
        ///  Constructor.
        /// </summary>
        /// <param name="inPixelOffsetX">Initialize value of PixelOffset.</param>
        /// <param name="inPixelWidth">Initialize value of PixelWidth.</param>
        /// <param name="inFeedWidth">Initialize value of FeedWidth.</param>
        public CharacterMetrics(int inPixelOffsetX, int inPixelWidth, float inFeedWidth)
        {
            PixelOffsetX = inPixelOffsetX;
            PixelWidth = inPixelWidth;
            FeedWidth = inFeedWidth;
            return;
        }

        /// <summary>
        /// Distance from the left edge of the canvas to the first valid pixel when rendered.
        /// </summary>
        public readonly int PixelOffsetX;

        /// <summary>
        /// Width from the first valid pixel on the left edge to the last valid pixel on the right edge.
        /// </summary>
        public readonly int PixelWidth;

        /// <summary>
        /// Width to the point where the next character should be rendered.
        /// </summary>
        public readonly float FeedWidth;
    }
}
