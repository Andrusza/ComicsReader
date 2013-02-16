using Microsoft.Xna.Framework.Graphics;
using XnaGuest.XnaWindowsSettings;

namespace XnaGuest.Image
{
    public class OwnTexture2D
    {
        private Corners imgCorners;
        private Texture2D image;

        public Texture2D Image
        {
            get { return image; }
            set { image = value; imgCorners = new Corners(image); }
        }

        public Corners Corners
        {
            get { return imgCorners; }
            set { imgCorners = value; }
        }

        public OwnTexture2D(Texture2D image)
        {
            this.image = image;
            this.imgCorners = new Corners(this.image);
        }

        public OwnTexture2D()
        {
        }
    }
}