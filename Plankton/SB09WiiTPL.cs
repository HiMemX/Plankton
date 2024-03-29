using libWiiSharp;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoArchive;
using System.Speech.Synthesis;
using System.Windows.Forms;

namespace Plankton
{
    internal static class SB09WiiTPL
    {
        public static List<Bitmap> BitmapsFromRawblob(byte[] rawblob)
        {
            TPL tpl = TPL.Load(rawblob.Skip(0x20).ToArray());

            Bitmap colormap = new Bitmap(tpl.ExtractTexture(0));
            Bitmap combinedmap = new Bitmap(tpl.ExtractTexture(0));
            Bitmap alphamap = new Bitmap(colormap.Width, colormap.Height);

            if (tpl.NumOfTextures == 2)
            { // Second channel is used for alpha transparency
                alphamap = new Bitmap(tpl.ExtractTexture(1));

                Color oldcolor;
                int alpha;
                Color newcolor;
                for (int y = 0; y < alphamap.Height; y++)
                {
                    for (int x = 0; x < alphamap.Width; x++)
                    {
                        oldcolor = colormap.GetPixel(x, y);
                        alpha = alphamap.GetPixel(x, y).R; // Just one channel
                        newcolor = Color.FromArgb(alpha, oldcolor.R, oldcolor.G, oldcolor.B);
                        combinedmap.SetPixel(x, y, newcolor);
                    }
                }
            }
            return new List<Bitmap>() { colormap, alphamap, combinedmap};
        }

        public static byte[] RawblobFromBitmaps(Bitmap colormap, Bitmap alphamap, bool alphaused, uint wrapS, uint wrapT, uint minFilter, uint magFilter)
        {
            TPL tpl = new TPL();
            List<Image> images = new() { colormap };
            List<TPL_TextureFormat> formats = new() { TPL_TextureFormat.CMP };
            List<TPL_PaletteFormat> palettes = new() { TPL_PaletteFormat.RGB565 };
            if (alphaused)
            {
                images.Add(alphamap);
                formats.Add(TPL_TextureFormat.I4);
                palettes.Add(TPL_PaletteFormat.IA8);
            }
            // MessageBox.Show(alphaused.ToString());
            tpl.CreateFromImages(images.ToArray(), formats.ToArray(), palettes.ToArray());

            foreach (TPL_TextureHeader textureheader in tpl.tplTextureHeaders)
            {
                textureheader.WrapS = wrapS;
                textureheader.WrapT = wrapT;
                textureheader.MinFilter = minFilter;
                textureheader.MagFilter = magFilter;
            }

            MemoryStreamEndian header = new MemoryStreamEndian(new byte[0x20], false);
            header.Pad(0x03, 0x00);
            header.WriteE(alphaused);
            header.WriteE(alphaused ? 1f : 0f);
            header.WriteE((short)images.Count);
            header.WriteE(alphaused ? (byte)0 : (byte)1);
            header.Pad(1, 0);
            header.WriteE(0x78C87406);
            header.WriteE(0xEE000000);
            header.Pad(0x0C, 0x00);

            return header.ToArray().Concat(tpl.ToByteArray()).ToArray();
        }
    }
}
