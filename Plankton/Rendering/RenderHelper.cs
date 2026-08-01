using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics;

using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System.Drawing;
using System.Windows.Forms;

namespace Plankton.Rendering
{
    public class RenderHelper
    {

        public Matrix4 projection;
        public Matrix4 view;

        public PrimitiveModelLib models;

        public GLControl control;

        float[] linevertices = {0,0,0,1,1,0,0,1};
        int linevbo;
        int linevao;
        Shader LineShader;

        float[] pointvertex = { 0, 0, 0 };
        int pointvbo;
        int pointvao;
        Shader PointShader;

        public RenderHelper(GLControl control)
        {
            this.control = control;

            MakeCurrent();
            models = new PrimitiveModelLib();

            InitLineResources();
            InitPointResources();
        }

        void InitLineResources()
        {
            string basepath = "Rendering/Shaders/Line/";
            LineShader = new Shader(basepath + "LineShader.vert", basepath + "LineShader.frag");

            // Create and bind a VAO (Vertex Array Object)
            linevao = GL.GenVertexArray();
            GL.BindVertexArray(linevao);

            linevbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, linevbo);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(linevertices.Length * sizeof(float)), linevertices, BufferUsageHint.StaticDraw);


            // Specify how the vertex data is laid out in the buffer (positions)
            GL.VertexAttribPointer(0, 4, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribDivisor(0, 0);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
        void InitPointResources()
        {
            string basepath = "Rendering/Shaders/Point/";
            PointShader = new Shader(basepath + "PointShader.vert", basepath + "PointShader.frag");
            
            // Create and bind a VAO (Vertex Array Object)
            pointvao = GL.GenVertexArray();
            GL.BindVertexArray(pointvao);

            pointvbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, pointvbo);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(pointvertex.Length * sizeof(float)), pointvertex, BufferUsageHint.StaticDraw);


            // Specify how the vertex data is laid out in the buffer (positions)
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribDivisor(0, 0);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void AddEmpty(PrimitiveInstance instance)
        {
            models.Empty.Add(instance);
        }

        public void AddLine(PrimitiveInstance instance)
        {
            models.Line.Add(instance);
        }

        public void AddSphere(PrimitiveInstance instance)
        {
            models.Sphere.Add(instance);
        }

        public Matrix4x4 GetLineMatrix(System.Numerics.Vector3 pos1, System.Numerics.Vector3 pos2)
        {
            System.Numerics.Vector3 dir = pos2 - pos1;

            return new Matrix4x4(dir.X,dir.Y,dir.Z, 0,0,0,0,0,0,0,0,0,0,0,0,1) * Matrix4x4.CreateTranslation(pos1);
        }

        public void AddCube(PrimitiveInstance instance) {
            models.Cube.Add(instance);
        }

        public void AddCamera(Matrix4x4 mat, Color4 color, float fov)
        {
            float s = 0.3f;
            float d = s / (float)Math.Tan(fov * Math.PI / 360f);

            //SetupShader(Matrix4x4.CreateScale(s, s, d) * mat, color);

            
        }

        public void MakeCurrent()
        {
            if (!control.Context.IsCurrent) control.MakeCurrent();
        }

        public void Buffer()
        {
            MakeCurrent();
            models.BufferAll();
        }

        public void Render()
        {

            MakeCurrent();
            models.RenderAll();
        }

        public void RenderSolid()
        {
            MakeCurrent();
            models.RenderAllSolid();
        }

        public void RenderNonSolid()
        {
            MakeCurrent();
            models.RenderAllNonSolid();
        }

        public void Clear()
        {
            models.ClearAll();
        }


        /*void SetupPrimitiveShader(Shader shader)
        {
            shader.Use();
            shader.SetMatrix4("view", view);
            shader.SetMatrix4("project", projection);
            shader.SetColor4("selectedColor", GlobalRenderSettings.highlightColor);
        }*/

        public void SetupRegularShader()
        {
            Shader shader = models.shaders.regular;
            shader.Use();
            shader.SetMatrix4("view", view);
            shader.SetMatrix4("project", projection);
            shader.SetColor4("selectedColor", GlobalRenderSettings.highlightColor);
        }

        public void SetupSelectionShader()
        {
            Shader shader = models.shaders.selection;
            shader.Use();
            shader.SetMatrix4("view", view);
            shader.SetMatrix4("project", projection);
            shader.SetColor4("selectedColor", GlobalRenderSettings.highlightColor);
        }

        public void SetupHighlightShader(bool useHighlightColor = false)
        {

            Shader shader = models.shaders.highlight;
            shader.Use();
            shader.SetMatrix4("view", view);
            shader.SetMatrix4("project", projection);
            shader.SetColor4("selectedColor", GlobalRenderSettings.highlightColor);
            models.shaders.highlight.SetInt("useCustomColor", useHighlightColor ? 1 : 0);
            models.shaders.highlight.SetColor4("customColor", GlobalRenderSettings.highlightColor);
        }

        public void SetupLineShader(System.Numerics.Vector3 startpos, System.Numerics.Vector3 endpos, Color4 color)
        {
            SetupLineShader(startpos, endpos, color, Matrix4x4.Identity);
        }

        public void SetupLineShader(System.Numerics.Vector3 startpos, System.Numerics.Vector3 endpos, Color4 color, Matrix4x4 transform)
        {

            System.Numerics.Vector3 xvec = endpos - startpos;
            Matrix4x4 mat = new Matrix4x4(xvec.X, xvec.Y, xvec.Z, 0, 0, 0, 0, 0, 0, 0, 0, 0, startpos.X, startpos.Y, startpos.Z, 1) * transform;

            LineShader.Use();
            LineShader.SetColor4("uColor", color);
            LineShader.SetMatrix4("uTransformation", mat);
            LineShader.SetMatrix4("view", view);
            LineShader.SetMatrix4("project", projection);
        }

        public void SetupPointShader(System.Numerics.Vector3 position, Color4 color)
        {

            PointShader.Use();
            PointShader.SetColor4("uColor", color);
            PointShader.SetVector3("uPosition", position);
            PointShader.SetMatrix4("view", view);
            PointShader.SetMatrix4("project", projection);
        }

        public void EnableBlending()
        {

            MakeCurrent();
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.DepthMask(false);
        }

        public void DisableBlending()
        {
            MakeCurrent();
            GL.Disable(EnableCap.Blend);
            GL.DepthMask(true);
        }
        public void EnableDepthTest()
        {
            MakeCurrent();
            GL.Enable(EnableCap.DepthTest);
        }
        public void DisableDepthTest()
        {
            MakeCurrent();
            GL.Disable(EnableCap.DepthTest);
        }

        public void RenderSimpleCube()
        {

        }

        public void RenderEmpty(System.Numerics.Vector3 position, Color4 color, float width)
        {

            SetupLineShader(position - System.Numerics.Vector3.UnitX, position + System.Numerics.Vector3.UnitX, color);
            RenderLine(width);
            SetupLineShader(position - System.Numerics.Vector3.UnitY, position + System.Numerics.Vector3.UnitY, color);
            RenderLine(width);
            SetupLineShader(position - System.Numerics.Vector3.UnitZ, position + System.Numerics.Vector3.UnitZ, color);
            RenderLine(width);
        }



        public void AddCamera(PrimitiveInstance instance, float fov)
        {

            float s = 0.3f;
            float d = s / (float)Math.Tan(fov * Math.PI / 360f);

            //SetupShader(Matrix4x4.CreateScale(s, s, d) * mat, color);

            instance.matrix = Matrix4x4.CreateScale(s, s, d) * instance.matrix;

            models.Camera_16_9.Add(instance);
            /*
            return;

            System.Numerics.Vector3[] points = {new System.Numerics.Vector3(),
                new System.Numerics.Vector3(s, -9/16f * s, d),
                new System.Numerics.Vector3(-s, -9/16f * s, d),
                new System.Numerics.Vector3(-s, 9/16f * s, d),
                new System.Numerics.Vector3(s, 9/16f * s, d),
            
                // Triangle on top
                new System.Numerics.Vector3(s/2, 3f/4f*s, d),
                new System.Numerics.Vector3(-s/2, 3f/4f*s, d),
                new System.Numerics.Vector3(0, s, d),
            };



            for(int i=1; i<5; i++)
            {
                SetupLineShader(points[0], points[i], color, mat);
                RenderLine(width);
            }
            for (int i = 1; i < 5; i++)
            {
                SetupLineShader(points[i], points[(i % 4)+1], color, mat);
                RenderLine(width);
            }
            for(int i=5; i<8; i++)
            {
                SetupLineShader(points[i], points[((i - 4) % 3) + 5], color, mat);
                RenderLine(width);
            }
            */
        }

        public void RenderPoint(System.Numerics.Vector3 position, Color4 color, float thickness)
        {
            MakeCurrent();

            SetupPointShader(position, color);

            GL.PointSize(thickness);

            GL.BindVertexArray(pointvao);
            GL.DrawArrays(PrimitiveType.Points, 0, 1);
            GL.BindVertexArray(0);
        }

        public void RenderLine(float thickness)
        {
            MakeCurrent();
            GL.LineWidth(thickness);

            GL.BindVertexArray(linevao);
            GL.DrawArrays(PrimitiveType.Lines, 0, 2); // 2 vertices (1 line)
            GL.BindVertexArray(0);

        }

        public void RenderLine(System.Numerics.Vector3 startpos, System.Numerics.Vector3 endpos, Color4 color, float thickness)
        {
            MakeCurrent();

            SetupLineShader(startpos, endpos, color);

            RenderLine(thickness);

        }
    }
}
