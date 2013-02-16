using System.Collections.Generic;
using Microsoft.Xna.Framework;
using XnaGuest.Image.Vertex;

namespace XnaGuest.Image
{
    public interface IObserver
    {
        void Update(Camera cam);
    }

    public class Camera : Geometry
    {
        private Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, 4.0f / 3.0f, 0.01f, 5000f);
        private Matrix view;
        private List<IObserver> quads = new List<IObserver>();

        public Camera(Vector3 position)
        {
            view = Matrix.CreateLookAt(position, Vector3.Zero, Vector3.Up);
        }

        public Camera()
        {
        }

        public void Attach(IObserver obj)
        {
            quads.Add(obj);
        }

        public void Notify()
        {
            foreach (IObserver obj in quads)
            {
                obj.Update(this);
            }
        }

        public Matrix Projection
        {
            get { return projection; }
            set { projection = value; }
        }

        public Matrix View
        {
            get { return view; }
            set { view = value; Notify(); }
        }
    }
}