using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoArchive;

namespace Plankton.Rendering
{
    internal static class Caster
    {
        static List<wmlTypeID> convertable = new()
        {
            wmlTypeID.Effect,
            wmlTypeID.StaticGeometry,
            wmlTypeID.SkinGeometry,
            wmlTypeID.Material,
            wmlTypeID.Model,
            wmlTypeID.GenericShader,
            wmlTypeID.Texture,
            wmlTypeID.RawBlob
        };

        public static Base.BaseClass Cast(TOCEntry asset, ResourcePool resourcePool)
        {
            if (!convertable.Contains(asset.wmlTypeID)) return null;

            Type objtype = asset.entity.GetType();

            
            // Don't kill me please
            if (asset.entity is SB09WiiAsset.Effect)         return new SB09Wii.Effect(asset, resourcePool);
            if (asset.entity is SB09WiiAsset.GeometryAsset)  return new SB09Wii.Geometry(asset, resourcePool);
            if (asset.entity is SB09WiiAsset.Material) return new SB09Wii.Material(asset, resourcePool);
            if (asset.entity is SB09WiiAsset.Model) return new SB09Wii.Model(asset, resourcePool);
            if (asset.entity is SB09WiiAsset.GenericShader) return new SB09Wii.Shader(asset, resourcePool);
            if (asset.entity is SB09WiiAsset.Texture) return new SB09Wii.Texture(asset, resourcePool);
            if (asset.entity is SB09WiiAsset.RawBlob) return new SB09Wii.Rawblob(asset, resourcePool);



            return null;
        }
    }
}
