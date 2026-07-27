using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoArchive;
using Plankton.Rendering.Base;
using OpenTK.Graphics.OpenGL4;

namespace Plankton.Rendering.SB09Wii
{
    internal class Texture : Base.TextureBase
    {
        public SB09WiiAsset.Texture entity;

        public Texture(TOCEntry asset, ResourcePool resourcePool) : base(asset, resourcePool)
        {
            this.entity = (SB09WiiAsset.Texture)asset.entity;
        }

        public override void Update(TOCEntry asset = null)
        {
            base.Update(asset);
            Init();
            UpdateAssociates();
        }

        public override ulong GetTextureBufferID()
        {
            return entity.GetTextureBufferID();
        }

        public override void UpdateAssociates()
        {
            resourcePool.DoActionOnPool(resourcePool.aMaterialPool, (BaseClass entry) => {
                if (((MaterialBase)entry).GetTextureIDs().Contains(asset.uidSelf)) { entry.UpdateAssociates(); } // If material asset has this id, init it again
            });

            resourcePool.DoActionOnPool(resourcePool.aGeometryPool, (BaseClass entry) => {
                if (((GeometryBase)entry).GetRendTextureIDs().Contains(asset.uidSelf)) { ((GeometryBase)entry).Init(); } // If geometry asset has this texture, init it again
            });
        }

        public override void Init()
        {
            if (asset.delete) { return; }


            //MessageBox.Show("=" + entry.uidSelf.ToString("X16"));
            
            if (!buffer.hasHandle)
            {
                //    MessageBox.Show("Generating Handle...");
                buffer.handle = GL.GenTexture(); // GL.GenTexture CRASHES
                                                         //    MessageBox.Show("Executed!");
                buffer.hasHandle = true;
            }

            UpdateTexture();
        }



        public void UpdateTexture()
        {
            
            BaseClass texturebuffer = resourcePool.GetAssetFromPool(resourcePool.rawblobPool, GetTextureBufferID());
            if (texturebuffer == null || !(texturebuffer.asset.wmlTypeID is wmlTypeID.RawBlob))
            {
                // Make texture be "texture missing" texture
                buffer.hasHandle = false;

                return;
            }


            GL.BindTexture(TextureTarget.Texture2D, buffer.handle);

            //MessageBox.Show("Texture Bound");

            Bitmap loadingmap = SB09WiiTPL.BitmapsFromRawblob(texturebuffer.asset.data.ToArray())[2];
            BitmapData loadingmapdata = loadingmap.LockBits(new Rectangle(0, 0, loadingmap.Width, loadingmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            //MessageBox.Show("Texture converted");

            hasAlpha = SB09WiiTPL.HasAlpha(texturebuffer.asset.data.ToArray());

            // Default parameters for now
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, loadingmap.Width, loadingmap.Height,
                0, OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte, loadingmapdata.Scan0);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

        }
    }
}
