using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using XnaGuest.Image.Vertex;

namespace XnaGuest.Image
{
    public interface IObserver
    {
        void Update(Camera cam);
    }

    public partial class Camera : Geometry
    {
        private static Matrix projection = Matrix.CreateOrthographic(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, 0.0f, 10f);
        private List<IObserver> quads = new List<IObserver>();

        public Camera()
        {
            SetBounds();
        }

        public void Attach(IObserver obj)
        {
            quads.Add(obj);
        }

        private void Notify()
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