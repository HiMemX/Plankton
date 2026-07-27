using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using HoArchive;
using Plankton.Rendering;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Graphics;

namespace Plankton.Rendering.Base
{
    internal abstract class GeometryBase : BaseClass // Copy pasted from deprecated CSHO internal shenanigans
    {
        public GeometryBase(TOCEntry asset, ResourcePool resourcePool) : base(asset, resourcePool) { }
        public abstract List<ulong> GetBufferIDs();
        public abstract ulong GetIndexBufferID();
        public abstract ulong GetMaterialID();
        public abstract List<ulong> GetRendTextureIDs();
        public abstract Vector3 GetBoundSphereCenter();
        public abstract float GetBoundSphereRadius();
        public abstract Vector3 GetAABBCenter();
        
        public virtual void Render()
        {

            if (!CanRender) { return; }

            GL.BindVertexArray(VertexArray.handle);
            GL.DrawElementsInstanced(OpenTK.Graphics.OpenGL4.PrimitiveType.Triangles, elementCount, DrawElementsType.UnsignedInt, (IntPtr)0, CulledInstanceInfos.Count);

        }

        public virtual void RenderInstance(int instanceindex)
        {
            if (!CanRender) { return; }

            GL.BindVertexArray(VertexArray.handle);
            GL.DrawElementsInstancedBaseInstance(
                OpenTK.Graphics.OpenGL4.PrimitiveType.Triangles,
                elementCount,
                DrawElementsType.UnsignedInt,
                IntPtr.Zero,
                1,
                instanceindex);
        }


        // Stuff the renderer needs to handle
        public bool CanRender = false; // General control flag, if something failed just set this to false
        public bool isTransparent = false;

        public int Flags;
        public uint ShaderOpsFlags;

        public Color4 ambientScale = new Color4(1,1,1, 1);
        public float environmentScale = 1;

        public int elementCount;

        public GenericBuffer VertexBuffer = new();
        public GenericBuffer ElementBuffer = new();
        public GenericBuffer VertexArray = new();

        public TextureSet textureSet = new(); // Only references to the actual textures

        public GenericBuffer InstanceBuffer = new();
        public List<InstanceInfo> CulledInstanceInfos = new(); // Temporary storage space for culled instance infos
        public List<InstanceInfo> InstanceInfos = new(); // Contains instancing data
        //public int ShaderHandle; // Global Shader is used

        public List<List<float>> vertexdata;
        public List<int> indexdata;
    }
}
