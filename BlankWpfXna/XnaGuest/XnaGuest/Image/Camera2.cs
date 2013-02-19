using System.Windows.Forms;
using Microsoft.Xna.Framework;
using XnaGuest.Image.Vertex;

namespace XnaGuest.Image
{
    public enum Corner
    {
        LowerLeft = 3,
        LowerRight = 2,
        UpperLeft = 0,
        UpperRight = 1,
    }

    public partial class Camera : Geometry
    {
        private int mod(int x, int m)
        {
            return (x % m + m) % m;
        }

        protected override void SetBounds()
        {
            bounds = new Corners(TranslationMatrix.Translation, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        }

        public void AligCameraToCorner(Corner corner, Quad image, int angleDegree)
        {
            int next = angleDegree / 90;
            if (next != 0)
            {
                next = ((int)corner - next);
                next = mod(next,4);
            }
            else
            {
                next = (int)corner;
            }

            image.Rotate(angleDegree);

            Vector2 offset = image.Bounds.AllCorners[(int)corner] - Bounds.AllCorners[next];
            Translate(-offset);
            Rotate(angleDegree);
        }
    }
}