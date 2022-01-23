using OpenTK.Graphics.OpenGL4;
using System;

namespace RtCs.OpenGL
{
    public class GLProperties
    {
        public void Collect()
        {
            MaxTessGenLeven = GL.GetInteger(GetPName.MaxTessGenLevel);
            MaxComputeWorkGroupInvocations = GL.GetInteger(GL_MAX_COMPUTE_WORK_GROUP_INVOCATIONS);
            MaxComputeWorkGroupCount = GetIndexedInteger3(GL_MAX_COMPUTE_WORK_GROUP_COUNT);
            MaxComputeWorkGroupSize = GetIndexedInteger3(GL_MAX_COMPUTE_WORK_GROUP_SIZE);

            AfterPropertyCollected?.Invoke(this, EventArgs.Empty);
        }

        public static event EventHandler AfterPropertyCollected;

        public static int MaxTessGenLeven
        { get; private set; } = 64;
        public static int MaxComputeWorkGroupInvocations
        { get; private set; } = 0;
        public static Container3<int> MaxComputeWorkGroupCount
        { get; private set; } = new Container3<int>(1, 1, 1);
        public static Container3<int> MaxComputeWorkGroupSize
        { get; private set; } = new Container3<int>(1, 1, 1);

        private Container3<int> GetInteger3(GetPName inName)
        {
            int[] result = new int[3];
            GL.GetInteger(inName, result);
            return new Container3<int>(result[0], result[1], result[2]);
        }

        private Container3<int> GetIndexedInteger3(GetIndexedPName inName)
            => new Container3<int>(
                    GetIndexedInteger(inName, 0),
                    GetIndexedInteger(inName, 1),
                    GetIndexedInteger(inName, 2)
                );

        private int GetIndexedInteger(GetIndexedPName inName, int inIndex)
        {
            GL.GetInteger(inName, inIndex, out int result);
            return result;
        }

        private static GetPName GL_MAX_COMPUTE_WORK_GROUP_INVOCATIONS = (GetPName)37099;
        private static GetIndexedPName GL_MAX_COMPUTE_WORK_GROUP_COUNT = (GetIndexedPName)37310;
        private static GetIndexedPName GL_MAX_COMPUTE_WORK_GROUP_SIZE = (GetIndexedPName)37311;
    }
}
