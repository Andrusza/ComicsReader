using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Timers;
using System.Diagnostics;

namespace XnaGuest.Image.Vertex
{
    public class Corners
    {
        private Vector2[] corners = new Vector2[4];

        public Vector2[] AllCorners
        {
            get { return corners; }
            set { corners = value; }
        }

        private Vector2[] originCorners = new Vector2[4];
        private Vector2 center;

        public Vector2 Center
        {
            get { return center; }
            set { center = value; }
        }

        public Vector2 UpperLeft
        {
            get { return AllCorners[0]; }
            set { originCorners[0] = value; }
        }

        public Vector2 UpperRight
        {
            get { return AllCorners[1]; }
            set { originCorners[1] = value; }
        }

        public Vector2 LowerLeft
        {
            get { return AllCorners[3]; }
            set { originCorners[3] = value; }
        }

        public Vector2 LowerRight
        {
            get { return AllCorners[2]; }
            set { originCorners[2] = value; }
        }

        private void SetCorners(float x, float y, int width, int height)
        {
            float halfWidth = width / 2;
            float halfHeight = height / 2;

            Center = new Vector2(x, y);

            LowerLeft = Center + new Vector2(-halfWidth, -halfHeight);
            LowerRight = Center + new Vector2(halfWidth, -halfHeight);

            UpperLeft = Center + new Vector2(-halfWidth, halfHeight);
            UpperRight = Center + new Vector2(halfWidth, halfHeight);

            corners = originCorners;
        }

        public Corners(Vector2 position, int width, int height)
        {
            SetCorners(position.X, position.Y, width, height);
        }

        public Corners(Vector3 position, int width, int height)
        {
            SetCorners(position.X, position.Y, width, height);
        }

        public void Translate(Vector2 translation)
        {
            Parallel.For(0, 3, i =>
            {
                AllCorners[i] += translation;
            });
            Center = translation;
        }

        public void Rotation(Matrix rotationZ)
        {
            Translate(-Center);
            Stopwatch ss = new Stopwatch();
            ss.Start();

            Parallel.For(0, 3, i =>
            {
                AllCorners[i] = Vector2.Transform(corners[i], rotationZ);
            });
            //for (int i = 0; i < 4; i++)
            //{
            //    AllCorners[i] = Vector2.Transform(corners[i], rotationZ);
            //}

            ss.Stop();
            ss.ElapsedMilliseconds.ToString();

            Translate(Center);
        }
    }
}