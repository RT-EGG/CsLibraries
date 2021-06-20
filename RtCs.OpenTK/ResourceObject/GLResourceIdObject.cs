namespace RtCs.OpenGL
{
    public class GLResourceIdObject : GLResourceObject
    {
        protected override bool IsResourceCreated => ID != 0;

        public int ID
        { get; protected set; } = 0;

        public static implicit operator int(GLResourceIdObject inObject) => inObject.ID;
    }
}
