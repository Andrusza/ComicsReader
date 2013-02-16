using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace XnaGuest.Image
{
    public static class ReadImage
    {
        public static void Image2Texture(System.Drawing.Image image, GraphicsDevice graphics, ref OwnTexture2D texture)
        {
            if (image == null)
            {
                return;
            }

            if (texture == null || texture.Image.IsDisposed ||
                texture.Image.Width != image.Width ||
                texture.Image.Height != image.Height ||
                texture.Image.Format != SurfaceFormat.Color)
            {
                if (texture != null && !texture.Image.IsDisposed)
                {
                    texture.Image.Dispose();
                }
                texture = new OwnTexture2D();
                texture.Image = new Texture2D(graphics, image.Width, image.Height, false, SurfaceFormat.Color);
            }
            else
            {
                for (int i = 0; i < 16; i++)
                {
                    if (graphics.Textures[i] == texture.Image)
                    {
                        graphics.Textures[i] = null;
                        break;
                    }
                }
            }

            MemoryStream ms = new MemoryStream();
            image.Save(ms, image.RawFormat);

            ms.Seek(0, SeekOrigin.Begin);
            texture.Image = Texture2D.FromStream(graphics, ms, image.Width, image.Height, false);

            ms.Close();
            ms = null;
        }
    }
}