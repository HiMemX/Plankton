using System;
using System.Numerics;
using OpenTK.Graphics.OpenGL4;

namespace Plankton.Rendering
{
    internal class GridModel : IDisposable
    {
        private readonly int vao;
        private readonly Shader shader;

        private Matrix4x4 view;
        private Matrix4x4 projection;

        public GridModel()
        {
            // Only a VAO is needed
            vao = GL.GenVertexArray();

            string basePath = "Rendering/Shaders/";

            shader = new Shader(
                basePath + "grid.vert",
                basePath + "grid.frag"
            );
        }

        public void SetViewProj(
            Matrix4x4 view,
            Matrix4x4 projection)
        {
            this.view = view;
            this.projection = projection;
        }

        public void Render()
        {
            GL.Enable(EnableCap.Blend);

            GL.BlendFunc(
                BlendingFactor.SrcAlpha,
                BlendingFactor.OneMinusSrcAlpha
            );

            // Don't write transparent grid depth
            GL.DepthMask(false);

            shader.Use();

            shader.SetMatrix4("view", view);
            shader.SetMatrix4("projection", projection);

            GL.BindVertexArray(vao);

            // Render fullscreen triangle
            GL.DrawArrays(
                PrimitiveType.Triangles,
                0,
                3
            );

            GL.BindVertexArray(0);

            GL.DepthMask(true);
            GL.Disable(EnableCap.Blend);
        }

        public void Dispose()
        {
            GL.DeleteVertexArray(vao);
        }
    }
}