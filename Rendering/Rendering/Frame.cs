using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace Plankton.Rendering
{
    public class Frame
    {
        public int FBO;
        public int texture;
        public int depthRBO;

        private int msaaFBO;
        private int msaaTexture;
        private int msaaDepthRBO;

        private readonly int samples;
        private int width;
        private int height;

        public Frame(
            int width,
            int height,
            int samples = 1)
        {
            this.width = width;
            this.height = height;
            this.samples = Math.Max(1, samples);

            Create();
        }


        private void ResizeResolveFramebuffer()
        {
            GL.BindFramebuffer(
                FramebufferTarget.Framebuffer,
                FBO);

            GL.BindTexture(
                TextureTarget.Texture2D,
                texture);

            GL.TexImage2D(
                TextureTarget.Texture2D,
                0,
                PixelInternalFormat.Rgba8,
                width,
                height,
                0,
                PixelFormat.Rgba,
                PixelType.UnsignedByte,
                IntPtr.Zero);

            GL.BindRenderbuffer(
                RenderbufferTarget.Renderbuffer,
                depthRBO);

            GL.RenderbufferStorage(
                RenderbufferTarget.Renderbuffer,
                RenderbufferStorage.DepthComponent24,
                width,
                height);

            if (GL.CheckFramebufferStatus(
                    FramebufferTarget.Framebuffer)
                != FramebufferErrorCode.FramebufferComplete)
            {
                throw new Exception(
                    "Resolve framebuffer setup failed after resize.");
            }
        }

        private void ResizeMultisampleFramebuffer()
        {
            GL.BindFramebuffer(
                FramebufferTarget.Framebuffer,
                msaaFBO);

            GL.BindTexture(
                TextureTarget.Texture2DMultisample,
                msaaTexture);

            GL.TexImage2DMultisample(
                TextureTargetMultisample.Texture2DMultisample,
                samples,
                PixelInternalFormat.Rgba8,
                width,
                height,
                true);

            GL.BindRenderbuffer(
                RenderbufferTarget.Renderbuffer,
                msaaDepthRBO);

            GL.RenderbufferStorageMultisample(
                RenderbufferTarget.Renderbuffer,
                samples,
                RenderbufferStorage.DepthComponent24,
                width,
                height);

            if (GL.CheckFramebufferStatus(
                    FramebufferTarget.Framebuffer)
                != FramebufferErrorCode.FramebufferComplete)
            {
                throw new Exception(
                    "MSAA framebuffer setup failed after resize.");
            }
        }

        public void Resize(int width, int height)
        {
            this.width = width;
            this.height = height;

            ResizeResolveFramebuffer();

            if (samples > 1)
                ResizeMultisampleFramebuffer();

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.BindTexture(TextureTarget.Texture2DMultisample, 0);
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);
        }

        private void Create()
        {
            // Normal framebuffer: this is what BindTexture() exposes.
            FBO = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, FBO);

            texture = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, texture);

            GL.TexImage2D(
                TextureTarget.Texture2D,
                0,
                PixelInternalFormat.Rgba8,
                width,
                height,
                0,
                PixelFormat.Rgba,
                PixelType.UnsignedByte,
                IntPtr.Zero);

            GL.TexParameter(
                TextureTarget.Texture2D,
                TextureParameterName.TextureMinFilter,
                (int)TextureMinFilter.Nearest);

            GL.TexParameter(
                TextureTarget.Texture2D,
                TextureParameterName.TextureMagFilter,
                (int)TextureMagFilter.Nearest);

            GL.TexParameter(
                TextureTarget.Texture2D,
                TextureParameterName.TextureWrapS,
                (int)TextureWrapMode.ClampToEdge);

            GL.TexParameter(
                TextureTarget.Texture2D,
                TextureParameterName.TextureWrapT,
                (int)TextureWrapMode.ClampToEdge);

            GL.FramebufferTexture2D(
                FramebufferTarget.Framebuffer,
                FramebufferAttachment.ColorAttachment0,
                TextureTarget.Texture2D,
                texture,
                0);

            depthRBO = GL.GenRenderbuffer();
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, depthRBO);

            GL.RenderbufferStorage(
                RenderbufferTarget.Renderbuffer,
                RenderbufferStorage.DepthComponent24,
                width,
                height);

            GL.FramebufferRenderbuffer(
                FramebufferTarget.Framebuffer,
                FramebufferAttachment.DepthAttachment,
                RenderbufferTarget.Renderbuffer,
                depthRBO);

            if (samples > 1)
                CreateMultisampleFramebuffer();

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        private void CreateMultisampleFramebuffer()
        {
            msaaFBO = GL.GenFramebuffer();
            GL.BindFramebuffer(
                FramebufferTarget.Framebuffer,
                msaaFBO);

            msaaTexture = GL.GenTexture();

            GL.BindTexture(
                TextureTarget.Texture2DMultisample,
                msaaTexture);

            GL.TexImage2DMultisample(
                TextureTargetMultisample.Texture2DMultisample,
                samples,
                PixelInternalFormat.Rgba8,
                width,
                height,
                true);

            GL.FramebufferTexture2D(
                FramebufferTarget.Framebuffer,
                FramebufferAttachment.ColorAttachment0,
                TextureTarget.Texture2DMultisample,
                msaaTexture,
                0);

            msaaDepthRBO = GL.GenRenderbuffer();

            GL.BindRenderbuffer(
                RenderbufferTarget.Renderbuffer,
                msaaDepthRBO);

            GL.RenderbufferStorageMultisample(
                RenderbufferTarget.Renderbuffer,
                samples,
                RenderbufferStorage.DepthComponent24,
                width,
                height);

            GL.FramebufferRenderbuffer(
                FramebufferTarget.Framebuffer,
                FramebufferAttachment.DepthAttachment,
                RenderbufferTarget.Renderbuffer,
                msaaDepthRBO);

            if (GL.CheckFramebufferStatus(
                    FramebufferTarget.Framebuffer)
                != FramebufferErrorCode.FramebufferComplete)
            {
                throw new Exception(
                    "Multisampled framebuffer not complete!");
            }
        }

        public void Bind()
        {
            GL.BindFramebuffer(
                FramebufferTarget.Framebuffer,
                samples > 1 ? msaaFBO : FBO);
        }

        public void Resolve()
        {
            if (samples <= 1)
                return;

            GL.BindFramebuffer(
                FramebufferTarget.ReadFramebuffer,
                msaaFBO);

            GL.BindFramebuffer(
                FramebufferTarget.DrawFramebuffer,
                FBO);

            GL.BlitFramebuffer(
                0, 0, width, height,
                0, 0, width, height,
                ClearBufferMask.ColorBufferBit,
                BlitFramebufferFilter.Nearest);

            GL.BindFramebuffer(
                FramebufferTarget.Framebuffer,
                0);
        }

        public void BindTexture()
        {
            GL.BindTexture(
                TextureTarget.Texture2D,
                texture);
        }

        public void Unbind()
        {
            GL.BindFramebuffer(
                FramebufferTarget.Framebuffer,
                0);
        }
    }
}
