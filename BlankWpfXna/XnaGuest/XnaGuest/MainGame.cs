using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XnaGuest.Image;
using XnaGuest.Image.Vertex;

namespace XnaGuest
{
    public class MainGame : Game
    {
        private GraphicsManager gfx = new GraphicsManager();

        internal GraphicsManager Gfx
        {
            get { return gfx; }
            set { gfx = value; }
        }

        private SpriteBatch spriteBatch;
        private Texture2D image;
        private ReadArchive book;
        private Quad Image;

        public MainGame(Control parentControl)
        {
            gfx.Create(parentControl);
            gfx.Draw += new EventHandler<EventArgs>(Draw);
            gfx.Update += new EventHandler<EventArgs>(Update);
            spriteBatch = new SpriteBatch(gfx.GraphicsDevice);

            book = new ReadArchive("C:\\a.cbr");
            Camera cam = new Camera();
            Image = new Quad(this);
            Image.Projection = cam.Projection;
            cam.Attach(Image);
            cam.View = Matrix.CreateLookAt(new Vector3(0, 0, 1280), Vector3.Zero, Vector3.Up);

            UpdateImage(10);
        }

        public void UpdateImage(int num_page)
        {
            System.Drawing.Image img = book.ReadPageFromRar(num_page);
            ReadImage.Image2Texture(img, Gfx.GraphicsDevice, ref image);
            Image.Texture = image;
        }

        private Vector2 offset;

        public Vector2 ImageOffset
        {
            get { return offset; }
            set { offset = value; Image.Translate(offset); Gfx.Redraw(); }
        }

        private float x = 0;

        private void Input()
        {
            MouseState mouseState = Mouse.GetState();
            x += 0.5f;
        }

        private void Update(object sender, EventArgs e)
        {
            Input();
            FrameworkDispatcher.Update();
        }

        private void Draw(object sender, EventArgs e)
        {
            Image.Draw();
        }
    }
}