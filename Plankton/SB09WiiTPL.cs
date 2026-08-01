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
        public static unsafe List<Bitmap> BitmapsFromRawblob(byte[] rawblob)
        {
            if (rawblob == null)
                throw new ArgumentNullException(nameof(rawblob));

            if (rawblob.Length <= 0x20)
                throw new ArgumentException("The raw blob is too small.", nameof(rawblob));

            // Faster and lower-overhead than Skip(...).ToArray().
            byte[] tplData = new byte[rawblob.Length - 0x20];
            Buffer.BlockCopy(rawblob, 0x20, tplData, 0, tplData.Length);

            TPL tpl = TPL.Load(tplData);

            Bitmap combinedMap = null;
            Bitmap colorMap = null;
            Bitmap alphaMap = null;

            try
            {
                // Takes ownership of the extracted Bitmap.
                combinedMap = TakeAs32BppArgb(tpl.ExtractTexture(0));

                int width = combinedMap.Width;
                int height = combinedMap.Height;

                colorMap = new Bitmap(width, height, PixelFormat.Format32bppArgb);

                if (tpl.NumOfTextures == 2)
                {
                    // The second texture is returned as the alpha map and its red
                    // channel is applied to combinedMap.
                    alphaMap = TakeAs32BppArgb(tpl.ExtractTexture(1));

                    if (alphaMap.Width != width || alphaMap.Height != height)
                    {
                        throw new InvalidOperationException(
                            "The color and alpha textures have different dimensions.");
                    }

                    ApplyExternalAlpha(combinedMap, colorMap, alphaMap);
                }
                else
                {
                    alphaMap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
                    SplitEmbeddedAlpha(combinedMap, colorMap, alphaMap);
                }

                return new List<Bitmap>(3)
                    {
                        colorMap,
                        alphaMap,
                        combinedMap
                    };
            }
            catch
            {
                colorMap?.Dispose();
                alphaMap?.Dispose();
                combinedMap?.Dispose();
                throw;
            }
        }

        private static unsafe void ApplyExternalAlpha(
            Bitmap combinedMap,
            Bitmap colorMap,
            Bitmap alphaMap)
        {
            Rectangle area = new Rectangle(
                0, 0, combinedMap.Width, combinedMap.Height);

            BitmapData combinedData = combinedMap.LockBits(
                area,
                ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);

            try
            {
                BitmapData colorData = colorMap.LockBits(
                    area,
                    ImageLockMode.WriteOnly,
                    PixelFormat.Format32bppArgb);

                try
                {
                    BitmapData alphaData = alphaMap.LockBits(
                        area,
                        ImageLockMode.ReadOnly,
                        PixelFormat.Format32bppArgb);

                    try
                    {
                        for (int y = 0; y < area.Height; y++)
                        {
                            uint* combinedPixels = (uint*)(
                                (byte*)combinedData.Scan0 + y * combinedData.Stride);

                            uint* colorPixels = (uint*)(
                                (byte*)colorData.Scan0 + y * colorData.Stride);

                            uint* alphaPixels = (uint*)(
                                (byte*)alphaData.Scan0 + y * alphaData.Stride);

                            for (int x = 0; x < area.Width; x++)
                            {
                                uint color = combinedPixels[x];

                                // Preserve the original color texture.
                                colorPixels[x] = color;

                                // Format32bppArgb is represented as 0xAARRGGBB.
                                // The alpha texture's red channel occupies bits 16-23.
                                uint alpha = (alphaPixels[x] >> 16) & 0xFF;

                                combinedPixels[x] =
                                    (color & 0x00FFFFFFu) | (alpha << 24);
                            }
                        }
                    }
                    finally
                    {
                        alphaMap.UnlockBits(alphaData);
                    }
                }
                finally
                {
                    colorMap.UnlockBits(colorData);
                }
            }
            finally
            {
                combinedMap.UnlockBits(combinedData);
            }
        }

        private static unsafe void SplitEmbeddedAlpha(
            Bitmap combinedMap,
            Bitmap colorMap,
            Bitmap alphaMap)
        {
            Rectangle area = new Rectangle(
                0, 0, combinedMap.Width, combinedMap.Height);

            BitmapData combinedData = combinedMap.LockBits(
                area,
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            try
            {
                BitmapData colorData = colorMap.LockBits(
                    area,
                    ImageLockMode.WriteOnly,
                    PixelFormat.Format32bppArgb);

                try
                {
                    BitmapData alphaData = alphaMap.LockBits(
                        area,
                        ImageLockMode.WriteOnly,
                        PixelFormat.Format32bppArgb);

                    try
                    {
                        for (int y = 0; y < area.Height; y++)
                        {
                            uint* combinedPixels = (uint*)(
                                (byte*)combinedData.Scan0 + y * combinedData.Stride);

                            uint* colorPixels = (uint*)(
                                (byte*)colorData.Scan0 + y * colorData.Stride);

                            uint* alphaPixels = (uint*)(
                                (byte*)alphaData.Scan0 + y * alphaData.Stride);

                            for (int x = 0; x < area.Width; x++)
                            {
                                uint pixel = combinedPixels[x];
                                uint alpha = pixel >> 24;

                                // Original RGB with a fully opaque alpha channel.
                                colorPixels[x] = pixel | 0xFF000000u;

                                // Fully opaque grayscale representation of alpha.
                                alphaPixels[x] =
                                    0xFF000000u | alpha * 0x00010101u;
                            }
                        }
                    }
                    finally
                    {
                        alphaMap.UnlockBits(alphaData);
                    }
                }
                finally
                {
                    colorMap.UnlockBits(colorData);
                }
            }
            finally
            {
                combinedMap.UnlockBits(combinedData);
            }
        }

        /// <summary>
        /// Returns the supplied bitmap directly when it is already 32-bit ARGB.
        /// Otherwise converts it and disposes the supplied bitmap.
        /// </summary>
        private static Bitmap TakeAs32BppArgb(Bitmap source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (source.PixelFormat == PixelFormat.Format32bppArgb)
                return source;

            Bitmap converted = new Bitmap(
                source.Width,
                source.Height,
                PixelFormat.Format32bppArgb);

            try
            {
                using (Graphics graphics = Graphics.FromImage(converted))
                {
                    graphics.DrawImageUnscaled(source, 0, 0);
                }

                return converted;
            }
            catch
            {
                converted.Dispose();
                throw;
            }
            finally
            {
                source.Dispose();
            }
        }

        public static bool HasAlpha(byte[] rawblob)
        {

            TPL tpl = TPL.Load(rawblob.Skip(0x20).ToArray());

            return tpl.NumOfTextures == 2;
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
