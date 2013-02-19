using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace XnaGuest.Image
{
    public static class ReadImage
    {
        public static Texture2D Image2Texture(System.Drawing.Image image, GraphicsDevice graphics)
        {
            Texture2D texture = new Texture2D(graphics, image.Width, image.Height, false, SurfaceFormat.Color);

            MemoryStream ms = new MemoryStream();
            image.Save(ms, image.RawFormat);

            ms.Seek(0, SeekOrigin.Begin);
            texture = Texture2D.FromStream(graphics, ms, image.Width, image.Height, false);

            ms.Close();
            ms = null;

            return texture;
        }
    }
}