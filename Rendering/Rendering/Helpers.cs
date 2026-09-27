using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace Plankton.Rendering
{
    internal static class Helpers
    {
        public static void GenerateHandle(GenericBuffer buffer)
        {
            if (!buffer.hasHandle)
            {
                buffer.handle = GL.GenBuffer();
                buffer.hasHandle = true;
            }
        }

        public static void GenerateVertexArrayHandle(GenericBuffer buffer)
        {
            if (!buffer.hasHandle)
            {
                buffer.handle = GL.GenVertexArray();
                buffer.hasHandle = true;
            }
        }
    }



}
