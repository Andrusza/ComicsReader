using System;
using System.Drawing;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using SharpCompress.Archive;

namespace XnaGuest
{
    public class ReadArchive : IDisposable
    {
        private IArchive arch;
        private Stream stream;

        public ReadArchive(string pathToFile)
        {
            stream = File.OpenRead(pathToFile);
            arch = ArchiveFactory.Open(stream, SharpCompress.Common.Options.KeepStreamsOpen);
            while (!arch.IsComplete) { }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.Equals(null))
            {
                if (disposing)
                {
                    arch.Dispose();
                }

                arch = null;
                stream.Close();
                stream = null;
            }
        }

        public System.Drawing.Image ReadPageFromRar(int page)
        {
            MemoryStream m = new MemoryStream();
            var entry = arch.GetCertainEntry(page);
            entry.WriteTo(m);
            return Bitmap.FromStream(m);
        }

        public static void Image2Texture(System.Drawing.Image image, GraphicsDevice graphics, ref Texture2D texture)
        {
            if (image == null)
            {
                return;
            }

            if (texture == null || texture.IsDisposed ||
                texture.Width != image.Width ||
                texture.Height != image.Height ||
                texture.Format != SurfaceFormat.Color)
            {
                if (texture != null && !texture.IsDisposed)
                {
                    texture.Dispose();
                }

                texture = new Texture2D(graphics, image.Width, image.Height, false, SurfaceFormat.Color);
            }
            else
            {
                for (int i = 0; i < 16; i++)
                {
                    if (graphics.Textures[i] == texture)
                    {
                        graphics.Textures[i] = null;
                        break;
                    }
                }
            }

            MemoryStream ms = new MemoryStream();
            image.Save(ms, image.RawFormat);

            ms.Seek(0, SeekOrigin.Begin);
            texture = Texture2D.FromStream(graphics, ms, image.Width, image.Height, false);

            ms.Close();
            ms = null;
        }
    }
}