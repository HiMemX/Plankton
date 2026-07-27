using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Assimp;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace Plankton.Rendering
{
    public class PrimitiveModel
    {

        private int tri_vao;
        private int tri_vbo;
        private int tri_ebo;

        private int line_vao;
        private int line_vbo;
        private int line_ebo;

        private int _ibo;
        public ConcurrentBag<PrimitiveInstance> instances = new();

        private int triIndexCount;
        private int lineIndexCount;


        List<Vector3> triverts = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        List<int> triindices = new List<int>();

        List<Vector3> lineverts = new List<Vector3>();
        List<int> lineindices = new List<int>();


        public string path;

        public PrimitiveModel(string modelPath)
        {
            LoadModel(modelPath);
        }

        private void LoadModel(string path)
        {

            AssimpContext importer = new AssimpContext();


            Scene scene = importer.ImportFile(path, PostProcessSteps.Triangulate);

            if (scene == null || scene.SceneFlags == SceneFlags.Incomplete || scene.RootNode == null)
            {
                throw new Exception("Error loading model with Assimp.");
            }


            Debug.debugWindow.AddEntry("PrimitiveModel.LoadModel", "Imported scene " + path + ", Mesh count: " + scene.MeshCount.ToString());

            // Read position and normal data from mesh
            foreach (Mesh mesh in scene.Meshes)
            {
                Debug.debugWindow.AddEntry("PrimitiveModel.LoadModel", mesh.PrimitiveType.ToString());
                for (int i = 0; i < mesh.Vertices.Count; i++)
                {
                    Vector3D vertex = mesh.Vertices[i];
                    
                    if (mesh.Faces[0].IndexCount == 3)
                    {
                        triverts.Add(new Vector3(vertex.X, vertex.Y, vertex.Z));
                        Vector3D normal = mesh.Normals[i];
                        normals.Add(new Vector3(normal.X, normal.Y, normal.Z));
                    }

                    else if(mesh.Faces[0].IndexCount == 2)
                    {
                        lineverts.Add(new Vector3(vertex.X, vertex.Y, vertex.Z));
                    }

                }

                // Read tri indices
                foreach (Face face in mesh.Faces)
                {


                    if (face.IndexCount == 3)
                    {
                        triindices.Add(face.Indices[0]);
                        triindices.Add(face.Indices[1]);
                        triindices.Add(face.Indices[2]);
                    }

                    else if (face.IndexCount == 2)
                    {
                        lineindices.Add(face.Indices[0]);
                        lineindices.Add(face.Indices[1]);
                    }
                }
            }

            triIndexCount = triindices.Count;
            lineIndexCount = lineindices.Count;


            Debug.debugWindow.AddEntry("PrimitiveModel.LoadModel", "Imported model from " + path);

            InitializeOpenGLBuffers();

            Debug.debugWindow.AddEntry("PrimitiveModel.LoadModel", "Initialized buffers for " + path);

            importer.Dispose();
        }

        private void InitializeOpenGLBuffers()
        {

            // Flatten the tri vertex data
            float[] triData = new float[triverts.Count * 6];
            for (int i = 0; i < triverts.Count; i++)
            {
                triData[i * 6 + 0] = triverts[i].X;
                triData[i * 6 + 1] = triverts[i].Y;
                triData[i * 6 + 2] = triverts[i].Z;

                triData[i * 6 + 3] = normals[i].X;
                triData[i * 6 + 4] = normals[i].Y;
                triData[i * 6 + 5] = normals[i].Z;
            }

            float[] lineData = new float[lineverts.Count * 6];
            for (int i = 0; i < lineverts.Count; i++)
            {
                lineData[i * 6 + 0] = lineverts[i].X;
                lineData[i * 6 + 1] = lineverts[i].Y;
                lineData[i * 6 + 2] = lineverts[i].Z;

                lineData[i * 6 + 3] = 0;
                lineData[i * 6 + 4] = 1;
                lineData[i * 6 + 5] = 0;

            }

            // TRIS
            // Create VAO
            tri_vao = GL.GenVertexArray(); // Not trivago
            GL.BindVertexArray(tri_vao);

            // Create and bind VBO
            tri_vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, tri_vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, triData.Length * sizeof(float), triData, BufferUsageHint.StaticDraw);

            // Create and bind EBO
            tri_ebo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, tri_ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, triindices.Count * sizeof(int), triindices.ToArray(), BufferUsageHint.StaticDraw);

            // Specify vertex attributes
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            // LINES
            // Create VAO
            line_vao = GL.GenVertexArray(); // Not trivago
            GL.BindVertexArray(line_vao);

            // Create and bind VBO
            line_vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, line_vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, lineData.Length * sizeof(float), lineData, BufferUsageHint.StaticDraw);

            // Create and bind EBO
            line_ebo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, line_ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, lineindices.Count * sizeof(int), lineindices.ToArray(), BufferUsageHint.StaticDraw);

            // Specify vertex attributes
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);




            // Create Instance buffer

            _ibo = GL.GenBuffer();

            GL.BindVertexArray(tri_vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _ibo);

            // Instance matrix
            int stride = Marshal.SizeOf<PrimitiveInstance>();
            for (int k = 0; k < 2; k++) // Sets up ibo for tri_vao and line_vao (sneeky weeky)
            {
                for (int i = 0; i < 4; i++)
                {
                    GL.VertexAttribPointer(i + 2, 4, VertexAttribPointerType.Float, false, stride, i * 16);
                    GL.EnableVertexAttribArray(i + 2);
                    GL.VertexAttribDivisor(i + 2, 1);
                }
                // Instance color
                GL.VertexAttribPointer(6, 4, VertexAttribPointerType.Float, false, stride, 64);
                GL.EnableVertexAttribArray(6);
                GL.VertexAttribDivisor(6, 1);

                // Container index
                GL.VertexAttribIPointer(7, 1, VertexAttribIntegerType.Int, stride, (IntPtr)80);
                GL.EnableVertexAttribArray(7);
                GL.VertexAttribDivisor(7, 1);


                GL.VertexAttribIPointer(8, 1, VertexAttribIntegerType.Int, stride, (IntPtr)84);
                GL.EnableVertexAttribArray(8);
                GL.VertexAttribDivisor(8, 1);


                GL.BindVertexArray(line_vao);
                GL.BindBuffer(BufferTarget.ArrayBuffer, _ibo);
            }


            // Unbind VAO
            GL.BindVertexArray(0);

        }

        public void Add(PrimitiveInstance instance)
        {
            instances.Add(instance);
        }

        public void Clear()
        {
            instances.Clear();
        }

        public void Buffer()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, _ibo);
            GL.BufferData(BufferTarget.ArrayBuffer, instances.Count * Marshal.SizeOf<PrimitiveInstance>(), instances.ToArray(), BufferUsageHint.DynamicDraw);
        }

        public void RenderSolid()
        {
            GL.BindVertexArray(tri_vao);
            GL.DrawElementsInstanced(OpenTK.Graphics.OpenGL4.PrimitiveType.Triangles, triIndexCount, DrawElementsType.UnsignedInt, IntPtr.Zero, instances.Count);

        }

        public void RenderNonSolid()
        {
            GL.BindVertexArray(line_vao);
            GL.DrawElementsInstanced(OpenTK.Graphics.OpenGL4.PrimitiveType.Lines, lineIndexCount, DrawElementsType.UnsignedInt, IntPtr.Zero, instances.Count);

        }

        public void Render()
        {
            RenderSolid();
            RenderNonSolid();
        }


        
        
    }


}
