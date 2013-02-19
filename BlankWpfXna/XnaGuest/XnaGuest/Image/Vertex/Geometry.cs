using Microsoft.Xna.Framework;

namespace XnaGuest.Image.Vertex
{
    public abstract class Geometry
    {
        private Matrix rotationMatrix = Matrix.Identity;
        private Matrix scaleMatrix = Matrix.Identity;
        private Matrix translationMatrix = Matrix.Identity;
        private Matrix modelMatrix = Matrix.Identity;

        public virtual Matrix ModelMatrix
        {
            get { return modelMatrix; }
            set { modelMatrix = value; }
        }

        protected Matrix RotationMatrix
        {
            get { return rotationMatrix; }
            set { rotationMatrix = value; Update(); }
        }

        protected Matrix ScaleMatrix
        {
            get { return scaleMatrix; }
            set { scaleMatrix = value; Update(); }
        }

        protected Matrix TranslationMatrix
        {
            get { return translationMatrix; }
            set { translationMatrix = value; Update(); }
        }

        public void Rotate(float angleDegree)
        {
            angleDegree = MathHelper.ToRadians(angleDegree);
            RotationMatrix = Matrix.CreateRotationZ(angleDegree);
            Bounds.Rotation(RotationMatrix);
        }

        public void Translate(Vector2 vec)
        {
            TranslationMatrix = Matrix.CreateTranslation(vec.X, vec.Y, 0);
            Bounds.Translate(vec);
        }

        //public void TranslateAndZoom(Vector2 vec, float zoom)
        //{
        //    TranslationMatrix = Matrix.CreateTranslation(-vec.X, -vec.Y, 0);
        //    ScaleMatrix = Matrix.CreateScale(zoom);
        //}

        //public void Translate(float x, float y)
        //{
        //    TranslationMatrix = Matrix.CreateTranslation(-x, -y, 0);
        //}

        private void Update()
        {
            ModelMatrix = scaleMatrix * rotationMatrix * translationMatrix;
        }

        protected Corners bounds;

        public Corners Bounds
        {
            get { return bounds; }
        }

        protected abstract void SetBounds();
    }
}