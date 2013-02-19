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

        private GraphicsManager Gfx
        {
            get { return gfx; }
            set { gfx = value; }
        }

        private ReadArchive book;
        private Quad image;
        private Camera camera = new Camera();

        public MainGame(Control parentControl)
        {
            gfx.Create(parentControl);
            gfx.Draw += new EventHandler<EventArgs>(Draw);
            gfx.Update += new EventHandler<EventArgs>(Update);

            book = new ReadArchive("C:\\a.cbr");

            UpdateImage(125);
        }

        public void UpdateImage(int num_page)
        {
            System.Drawing.Image bitmap = book.ReadPageFromRar(num_page);
            Texture2D texture = ReadImage.Image2Texture(bitmap, Gfx.GraphicsDevice);

            image = new Quad(gfx.GraphicsDevice, texture);
            camera.Attach(image);
            camera.AligCameraToCorner(Corner.UpperLeft, image,270);
            offset = new Vector2(camera.ModelMatrix.Translation.X,camera.ModelMatrix.Translation.Y);
            
        }

        private Vector2 offset;

        public Vector2 ImageOffset
        {
            get { return offset; }
            set { offset = value; camera.Translate(offset); Gfx.Redraw(); }
        }

        private void Input()
        {
            MouseState mouseState = Mouse.GetState();
        }

        private void Update(object sender, EventArgs e)
        {
            Input();
            FrameworkDispatcher.Update();
        }

        float x = 0;
        private void Draw(object sender, EventArgs e)
        {
           
            image.Draw();
        }
    }
}