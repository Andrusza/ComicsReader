using System;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XnaGuest
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainGame
    {
        private GraphicsManager gfx = new GraphicsManager();
        private SpriteBatch spriteBatch;
        private Texture2D image;
        private ReadArchive book;

        public MainGame(Control parentControl)
        {
            gfx.Create(parentControl);
            gfx.Draw += new EventHandler<EventArgs>(Draw);
            gfx.Update += new EventHandler<EventArgs>(Update);
            spriteBatch = new SpriteBatch(gfx.GraphicsDevice);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            book = new ReadArchive("C:\\a.cbr");
            System.Drawing.Image img = book.ReadPageFromRar(0);
            ReadArchive.Image2Texture(img, gfx.GraphicsDevice, ref image);

            sw.Stop();
            Console.WriteLine("Elapsed={0}", sw.Elapsed.TotalMilliseconds.ToString());
        }

        public static int x = 1;

        public void UpdateLogo()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            System.Drawing.Image img = book.ReadPageFromRar(x++);
            ReadArchive.Image2Texture(img, gfx.GraphicsDevice, ref image);

            sw.Stop();
            Console.WriteLine("Elapsed={0}", sw.Elapsed.TotalMilliseconds.ToString());
        }

        private Vector2 vec;

        public Vector2 Vec
        {
            get { return vec; }
            set { vec = value; gfx.Redraw(); }
        }

        private void Input()
        {
            //MouseState mouseState = Mouse.GetState();
            //val += mouseState.ScrollWheelValue;
            //if (mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            //{
            //    Console.WriteLine("sad");
            //}
            //vec = new Vector2(0, -val);
        }

        private void Update(object sender, EventArgs e)
        {
            Input();
            FrameworkDispatcher.Update();
        }

        private void Draw(object sender, EventArgs e)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(image, Vec, null, Color.Wheat, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            spriteBatch.End();
        }
    }
}