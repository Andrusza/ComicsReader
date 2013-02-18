using System.Collections.Generic;
using Microsoft.Xna.Framework;
using XnaGuest.Image.Vertex;
using System.Windows.Forms;

namespace XnaGuest.Image
{
    public interface IObserver
    {
        void Update(Camera cam);
    }

    public class Camera : Geometry
    {
        private static Matrix projection = Matrix.CreateOrthographic(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, 0.1f, 1000f);
        private List<IObserver> quads = new List<IObserver>();

        public Camera() { }

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

        public static Matrix Projection
        {
            get { return projection; }
            set { projection = value; }
        }

        public override Matrix ModelMatrix
        {
            get
            {
                return base.ModelMatrix;
            }
            set
            {
                base.ModelMatrix = value; Notify();
            }
        }
    }
}