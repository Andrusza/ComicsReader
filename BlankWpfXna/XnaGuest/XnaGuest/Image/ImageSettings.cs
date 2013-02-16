using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XnaGuest.XnaWindowsSettings
{
    public class Corners
    {
        private Vector2[] corners = new Vector2[5];
        private Vector2[] originCorners = new Vector2[5];

        public Vector2 UpperLeft
        {
            get { return corners[0]; }
            set { originCorners[0] = value; }
        }

        public Vector2 UpperRight
        {
            get { return corners[1]; }
            set { originCorners[1] = value; }
        }

        public Vector2 LowerLeft
        {
            get { return corners[2]; }
            set { originCorners[2] = value; }
        }

        public Vector2 LowerRight
        {
            get { return corners[3]; }
            set { originCorners[3] = value; }
        }

        public Vector2 Center
        {
            get { return corners[4]; }
            set { originCorners[4] = value; }
        }

        public Corners(Texture2D tex)
        {
            float halfWidth = tex.Width / 2;
            float halfHeight = tex.Height / 2;

            UpperLeft = new Vector2(-halfWidth, -halfHeight);
            UpperRight = new Vector2(halfWidth, -halfHeight);
            LowerLeft = new Vector2(-halfWidth, halfHeight);
            LowerRight = new Vector2(halfWidth, halfHeight);
        }

        public void Rotation(float angleDegree)
        {
            Matrix rotationZ = Matrix.CreateRotationZ(MathHelper.ToRadians(angleDegree));
            for (int i = 0; i < 4; i++)
            {
                corners[i] = Vector2.Transform(originCorners[i], rotationZ);
            }
        }
    }
}