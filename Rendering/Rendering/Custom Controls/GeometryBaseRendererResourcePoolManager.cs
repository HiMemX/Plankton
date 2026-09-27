using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CSHO;
using HoArchive;
using OpenTK.Graphics.OpenGL4;
using Plankton.Rendering.Base;
using Plankton.Rendering;
using System.Numerics;

namespace Plankton.Custom_Controls
{
    public partial class GeometryBaseRenderer
    {
        public BaseClass GetAssetFromRawblobPool(ulong id)
        {
            return resourcePool.GetAssetFromPool(resourcePool.rawblobPool, id);
        }
        public BaseClass GetAssetFromGeometryPool(ulong id)
        {
            return resourcePool.GetAssetFromPool(resourcePool.aGeometryPool, id);
        }

        public BaseClass GetAssetFromModelPool(ulong id)
        {
            return resourcePool.GetAssetFromPool(resourcePool.aInstancePool, id);
        }

        public void ClearResourcePool()
        {
            resourcePool.Clear();
        }

        public void InitPools()
        {
            MakeCurrent();
            Debug.debugWindow.AddEntry("InitPools", resourcePool.aGeometryPool.Count.ToString());
            Action<BaseClass> initfunc = (entry) => { entry.Init(); };
            resourcePool.DoActionOnPool(resourcePool.aTexturePool, initfunc);
            resourcePool.DoActionOnPool(resourcePool.aInstancePool, initfunc);
            resourcePool.DoActionOnPool(resourcePool.aGeometryPool, initfunc);
        }

        public TOCEntry GetLightKitScene() {
            if (resourcePool.lightKitScenePool.Count() > 0) return resourcePool.lightKitScenePool[0];
            return null;
        }

        public BaseClass AddToResources(TOCEntry entry)
        {
            return resourcePool.AddEntryToPool(entry);
        }

        public void InitModelInstance(ModelInstance instance, uint attr, uint flags, bool onlyupdate, ulong defaultLightKitID) // Container assumed to be instanced
        {
            
            Rendering.Base.BaseClass model = resourcePool.GetAssetFromPool(resourcePool.aInstancePool, instance.modelPrototypeID);

            if (model == null)
            {
                return;
            }

            

            uint childattr = 1;

            InstanceInfo info = new();
            info.matrix = instance.matrix;
            info.parentAttr = attr;
            info.flags = flags;
            info.lightkitIndex = resourcePool.GetAssetIndex(resourcePool.lightKitPool, instance.lightKitID);

            // LightKitEnv (Fix!!!)
            if(resourcePool.lightKitScenePool.Count != 0 &&
                (info.lightkitIndex == -1))
            {
                info.lightkitIndex = resourcePool.GetAssetIndex(resourcePool.lightKitPool, defaultLightKitID);
            }



            ((ModelBase)model).ApplyInstanceMatrixRecursive(info, ref childattr, onlyupdate);
        }

        public void ResetGeometryInstanceLists()
        {
            resourcePool.DoActionOnPool(resourcePool.aGeometryPool, ResetInstanceList);
        }

        void ResetInstanceList(BaseClass entry)
        {
            ((GeometryBase)entry).InstanceInfos = new();
        }

        void BufferAllGeometryInstances()
        {
            foreach (BaseClass entry in resourcePool.aGeometryPool)
            {
                BufferGeometryInstances(entry);
            }
        }

        void BufferGeometryInstances(BaseClass entry)
        {

            GeometryBase geometry = (GeometryBase)entry;

            if (geometry.CulledInstanceInfos.Count == 0) return;

            GL.BindBuffer(BufferTarget.ArrayBuffer, geometry.InstanceBuffer.handle);

            var data = geometry.CulledInstanceInfos.ToArray();


            GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, geometry.CulledInstanceInfos.Count * Marshal.SizeOf<InstanceInfo>(),
                data);

        }

        public void PrepareAllGeometryBuffers()
        {
            MakeCurrent();
            foreach (BaseClass entry in resourcePool.aGeometryPool)
            {
                GeometryBase geometry = (GeometryBase)entry;

                GL.BindBuffer(BufferTarget.ArrayBuffer, geometry.InstanceBuffer.handle);

                GL.BufferData(BufferTarget.ArrayBuffer, geometry.InstanceInfos.Count * Marshal.SizeOf<InstanceInfo>(),
                    IntPtr.Zero, BufferUsageHint.DynamicDraw);
            }
        }

        /*public float[] SerializeInstanceInfos(List<RenderingInternal.InstanceInfo> mats)
        {
            List<float> output = new();
            foreach (RenderingInternal.InstanceInfo mat in mats)
            {
                output.AddRange(Matrix4x4ToFloatArray(mat.matrix));
                output.Add(mat.parentAttr);
            }

            return output.ToArray();
        }*/

        public float[] Matrix4x4ToFloatArray(Matrix4x4 matrix) // Ty chatgpt lololol
        {
            return new float[]
            {
                matrix.M11, matrix.M12, matrix.M13, matrix.M14,  // First column
                matrix.M21, matrix.M22, matrix.M23, matrix.M24,  // Second column
                matrix.M31, matrix.M32, matrix.M33, matrix.M34,  // Third column
                matrix.M41, matrix.M42, matrix.M43, matrix.M44   // Fourth column
            };
        }
    }
}
