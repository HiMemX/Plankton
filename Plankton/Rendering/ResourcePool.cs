using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoArchive;
using Plankton.Rendering.Base;
using Plankton.Special_Editors.Level_Editor.EditableContainers;

namespace Plankton.Rendering
{
    public class ResourcePool
    {

        public List<BaseClass> aEffectPool = new();
        public List<BaseClass> aGeometryPool = new();
        public List<BaseClass> aInstancePool = new();
        public List<BaseClass> aMaterialPool = new();
        public List<BaseClass> aShaderPool = new();
        public List<BaseClass> aTexturePool = new();
        public List<BaseClass> rawblobPool = new();
        public List<TOCEntry> lightKitPool = new(); // Not a specialized class, will be Truth or Square Wii only for now (Future me should probably make seperate renderers alltogether)
        public List<TOCEntry> lightKitScenePool = new(); // Not specialized aswell, fix!!!
        public List<TOCEntry> fogPool = new(); // I think I need to redo a lot of the rendering stuff

        
        public void Clear()
        {
            aEffectPool.Clear();
            aGeometryPool.Clear();
            aInstancePool.Clear();
            aMaterialPool.Clear();
            aShaderPool.Clear();
            aTexturePool.Clear();
            rawblobPool.Clear();
            lightKitPool.Clear();
            lightKitScenePool.Clear();
            fogPool.Clear();
        }



        public BaseClass AddEntryToPool(TOCEntry entry)
        {
            BaseClass o = Caster.Cast(entry, this);

            if (o is EffectBase) { aEffectPool.Add(o); }
            else if (o is GeometryBase) { aGeometryPool.Add(o); }
            else if (o is ModelBase) { aInstancePool.Add(o); }
            else if (o is MaterialBase) { aMaterialPool.Add(o); }
            else if (o is ShaderBase) { aShaderPool.Add(o); }
            else if (o is TextureBase) { aTexturePool.Add(o); }
            else if (o is RawblobBase) { rawblobPool.Add(o); }
            else if (entry.wmlTypeID == wmlTypeID.LightKit) { lightKitPool.Add(entry); }
            else if (entry.wmlTypeID == wmlTypeID.LightKitScene) lightKitScenePool.Add(entry);
            else if (entry.wmlTypeID == wmlTypeID.Fog) fogPool.Add(entry);

            if (o != null)
            {
                o.asset.OnUpdate.Add((TOCEntry entry) => { o.UpdateAssociates(); });
            }

                return o;
        }

        public void DoActionOnPool(List<BaseClass> pool, Action<BaseClass> act)
        {
            foreach(BaseClass entry in pool)
            {
                act(entry);
            }
        }

        public void DoActionOnPool(List<BaseClass> pool, Action<TOCEntry> act)
        {
            foreach(BaseClass entry in pool)
            {
                act(entry.asset);
            }
        }


        public void DoActionOnEffectPool(Action<TOCEntry> act)
        {
            foreach (EffectBase entry in aEffectPool)
            {
                act(entry.asset);
            }
        }


        public void DoActionOnGeometryPool(Action<TOCEntry> act)
        {
            foreach(GeometryBase entry in aGeometryPool)
            {
                act(entry.asset);
            }
        }

        public void DoActionOnModelPool(Action<TOCEntry> act)
        {
            foreach (ModelBase entry in aInstancePool)
            {
                act(entry.asset);
            }
        }

        public void DoActionOnMaterialPool(Action<TOCEntry> act)
        {
            foreach (MaterialBase entry in aMaterialPool)
            {
                act(entry.asset);
            }
        }

        public void DoActionOnShaderPool(Action<TOCEntry> act)
        {
            foreach (ShaderBase entry in aShaderPool)
            {
                act(entry.asset);
            }
        }

        public void DoActionOnTexturePool(Action<TOCEntry> act)
        {
            foreach (TextureBase entry in aTexturePool)
            {
                act(entry.asset);
            }
        }

        public int GetAssetIndex(List<TOCEntry> pool, ulong id)
        {
            for(int i=0; i<pool.Count; i++)
            {
                if (pool[i].uidSelf == id) return i;
            }
            return -1;
        }

        public int GetAssetIndex(List<BaseClass> pool, ulong id)
        {
            for (int i = 0; i < pool.Count; i++)
            {
                if (pool[i].asset.uidSelf == id) return i;
            }
            return -1;
        }

        public TOCEntry GetAssetFromPool(List<TOCEntry> pool, ulong id)
        {
            foreach (TOCEntry entry in pool)
            {
                if (entry.uidSelf == id) { return entry; }
            }

            return null;
        }

        public BaseClass GetAssetFromPool(List<BaseClass> pool, ulong id)
        {
            foreach (BaseClass entry in pool)
            {
                if (entry.asset.uidSelf == id) { return entry; }
            }

            return null;
        }

        public List<TOCEntry> GetAssetsFromPool(List<TOCEntry> pool, List<ulong> ids)
        {
            List<TOCEntry> output = new();

            foreach (ulong id in ids)
            {
                output.Add(GetAssetFromPool(pool, id));
            }
            return output;
        }
        public List<BaseClass> GetAssetsFromPool(List<BaseClass> pool, List<ulong> ids)
        {
            List<BaseClass> output = new();

            foreach (ulong id in ids)
            {
                output.Add(GetAssetFromPool(pool, id));
            }
            return output;
        }
    }
}
