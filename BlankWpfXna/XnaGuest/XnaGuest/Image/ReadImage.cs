using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace XnaGuest.Image
{
    public static class ReadImage
    {
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
