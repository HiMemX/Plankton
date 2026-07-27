using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoArchive;
using Plankton.Rendering.Base;

namespace Plankton.Rendering.SB09Wii
{
    internal class Model : Rendering.Base.ModelBase
    {
        public SB09WiiAsset.Model modelentity;

        public Model(TOCEntry modelasset, ResourcePool resourcePool) : base(modelasset, resourcePool)
        {
            modelentity = (SB09WiiAsset.Model)modelasset.entity;

            geomMatrices = new();
            modelInstanceMatrices = new();

        }

        public override void ApplyInstanceMatrixRecursive(InstanceInfo info, ref uint childAttr, bool onlyupdate)
        {
            List<BaseClass> subinstances = resourcePool.GetAssetsFromPool(resourcePool.aInstancePool, GetModelIDs());

            for (int s = 0; s < subinstances.Count; s++)
            {
                if (subinstances[s] == null) { continue; }
                if (!(subinstances[s] is ModelBase)) { continue; }

                InstanceInfo newinfo = info.Clone();
                newinfo.matrix = modelInstanceMatrices[s] * info.matrix;

                ((ModelBase)subinstances[s]).ApplyInstanceMatrixRecursive(newinfo, ref childAttr, onlyupdate);
            }

            List<BaseClass> geometries = resourcePool.GetAssetsFromPool(resourcePool.aGeometryPool, GetGeometryIDs());

            for (int g = 0; g < geometries.Count; g++)
            {

                if (geometries[g] == null) { continue; }
                if (!(geometries[g] is GeometryBase)) { continue; } // * instance.geomMatrices[g]

                if (!onlyupdate)
                {
                    InstanceInfo newinfo = info.Clone();
                    newinfo.childAttr = childAttr;
                    ((GeometryBase)geometries[g]).InstanceInfos.Add(newinfo);

                }
                else // Update loop
                {
                    for (int i = 0; i < ((GeometryBase)geometries[g]).InstanceInfos.Count; i++)
                    {
                        if (info.parentAttr != ((GeometryBase)geometries[g]).InstanceInfos[i].parentAttr) { continue; }
                        if (childAttr != ((GeometryBase)geometries[g]).InstanceInfos[i].childAttr) { continue; }
                        //Debug.debugWindow.AddEntry("ApplyInstanceMatrixRecursive", info.parentAttr, g, i);

                        InstanceInfo newinfo = info.Clone();
                        newinfo.childAttr = childAttr;

                        ((GeometryBase)geometries[g]).InstanceInfos[i] = newinfo;

                    }
                }
                childAttr++;
            }
        }

        public override void Update(TOCEntry asset = null)
        {
            base.Update(asset);
            Init();
        }

        public override void Init()
        {
            if (asset.delete) { return; }

            UpdateInstance();
        }

        public override void UpdateAssociates()
        {
            
        }

        public void InitInstance()
        {
            
        }

        public void UpdateInstance()
        {
            
            UpdateInstanceMatrices();

        }


        public override List<ulong> GetModelIDs()
        {
            return modelentity.GetModelIDs();
        }
        public override List<ulong> GetGeometryIDs()
        {
            return modelentity.GetGeometryIDs();
        }

        public override void UpdateInstanceMatrices()
        {
            modelentity.UpdateInstanceMatrices(geomMatrices, modelInstanceMatrices);
        }
    }
}
