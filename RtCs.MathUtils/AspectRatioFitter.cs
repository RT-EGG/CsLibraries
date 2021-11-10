namespace RtCs.MathUtils
{
    public class AspectRatioFitter2D
    {
        public Vector2 CalcFitRectSizeInParent(Vector2 inSize, Vector2 inParentSize)
        {
            if ((inParentSize.x / inSize.x) > (inParentSize.y / inSize.y)) {
                // fit to y-direction
                return new Vector2(inSize.x * (inParentSize.y / inSize.y), inParentSize.y);
            } else {
                // fit to x-direction
                return new Vector2(inParentSize.x, inSize.y * (inParentSize.x / inSize.x));
            }
        }

        public Vector2 CalcFitRectSizeWrapParent(Vector2 inSize, Vector2 inParentSize)
        {
            if ((inParentSize.x / inSize.x) < (inParentSize.y / inSize.y)) {
                // fit to y-direction
                return new Vector2(inSize.x * (inParentSize.y / inSize.y), inParentSize.y);
            } else {
                // fit to x-direction
                return new Vector2(inParentSize.x, inSize.y * (inParentSize.x / inSize.x));
            }
        }
    }
}
