using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XnaGuest.Image.Vertex
{
    public class Quad : Geometry,IObserver
    {
        private BasicEffect effect;

        private VertexBuffer vertexBuffer;
        private GraphicsDevice device;

        public Quad(MainGame game)
        {
            device = game.Gfx.GraphicsDevice;
            Initialize();
        }

        public Matrix View
        {
            get { return effect.View; }
            set { effect.View = value; }
        }

        public Matrix Projection
        {
            get { return effect.Projection; }
            set { effect.Projection = value; }
        }

        public Texture2D Texture
        {
            get { return effect.Texture; }
            set { effect.Texture = value; }
        }

        public void Draw()
        {
            device.SetVertexBuffer(vertexBuffer, 0);
            effect.World = ModelMatrix;

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                device.DrawPrimitives(PrimitiveType.TriangleList, 0, 2);
            }
        }

        public void Initialize()
        {
            VertexPositionTexture[] verts = new VertexPositionTexture[6];
            verts[0] = new VertexPositionTexture(new Vector3(-650, 995, 0), new Vector2(0, 0));
            verts[1] = new VertexPositionTexture(new Vector3(650, 995, 0), new Vector2(1, 0));
            verts[2] = new VertexPositionTexture(new Vector3(-650, -995, 0), new Vector2(0, 1));

            verts[3] = verts[1];
            verts[4] = new VertexPositionTexture(new Vector3(650, -955, 0), new Vector2(1, 1));
            verts[5] = verts[2];

            vertexBuffer = new VertexBuffer(device,
                                            VertexPositionTexture.VertexDeclaration,
                                            verts.Length,
                                            BufferUsage.WriteOnly);

            vertexBuffer.SetData(verts);

            effect = new BasicEffect(device);
            effect.TextureEnabled = true;
        }

        public void Update(Camera cam)
        {
            View = cam.View;
        }
    }
}