using OpenTK.Graphics.OpenGL4;

namespace RtCs.OpenGL
{
    /// <summary>
    /// The shader program object especial for gpu computation.
    /// </summary>
    /// <remarks>
    /// Compute shader program should attach only shader unit for compute shading, ComputeShader.
    /// </remarks>
    public class GLComputeShaderProgram : GLShaderProgram
    {
        public GLComputeShaderProgram()
        {
            AfterLinked += _ => {
                if (Linked) {
                    const int GL_COMPUTE_WORK_GROUP_SIZE = 0x8267;

                    int[] size = new int[3];
                    GL.GetProgram(ID, (GetProgramParameterName)GL_COMPUTE_WORK_GROUP_SIZE, size);
                    WorkGroupSize = new Container3<int>(size[0], size[1], size[2]);
                } else {
                    WorkGroupSize = new Container3<int>(-1, -1, -1);
                }
            };
        }

        /// <summary>
        /// Dispatch the program.
        /// </summary>
        /// <param name="inNumGroupX">The number of local work groups that will be dispatched in the X dimension.</param>
        /// <param name="inNumGroupY">The number of local work groups that will be dispatched in the Y dimension.</param>
        /// <param name="inNumGroupZ">The number of local work groups that will be dispatched in the Z dimension.</param>
        /// <remarks>
        /// Read more, see official [reference](https://www.khronos.org/registry/OpenGL-Refpages/gl4/html/glDispatchCompute.xhtml).
        /// </remarks>
        //public void Dispatch(int inNumGroupX, int inNumGroupY, int inNumGroupZ)
        //{
        //    GL.DispatchCompute(inNumGroupX, inNumGroupY, inNumGroupZ);
        //}

        /// <summary>
        /// The size value described by local_size in shader code.
        /// </summary>
        public Container3<int> WorkGroupSize
        { get; private set; } = new Container3<int>();

        /// <summary>
        /// The number of invocations in a single local work group (i.e., the product of the three dimensions) that may be dispatched to a compute shader.
        /// </summary>
        /// <remarks>
        /// The value may be not correct value until initialize opengl context.
        /// </remarks>
        public static int MaxComputeWorkGroupInvocations => GLProperties.MaxComputeWorkGroupInvocations;
        /// <summary>
        /// The maximum number of work groups that may be dispatched to a compute shader. Indices 0, 1, and 2 correspond to the X, Y and Z dimensions, respectively.
        /// </summary>
        /// <remarks>
        /// The value may be not correct value until initialize opengl context.
        /// </remarks>
        public static Container3<int> MaxComputeWorkGroupCount => GLProperties.MaxComputeWorkGroupCount;
        /// <summary>
        /// The maximum size of a work groups that may be used during compilation of a compute shader. Indices 0, 1, and 2 correspond to the X, Y and Z dimensions, respectively.
        /// </summary>
        /// <remarks>
        /// The value may be not correct value until initialize opengl context.
        /// </remarks>
        public static Container3<int> MaxComputeWorkGroupSize => GLProperties.MaxComputeWorkGroupSize;
    }
}
