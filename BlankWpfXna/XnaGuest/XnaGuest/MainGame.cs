using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XnaGuest.Image;

namespace XnaGuest
{
    public class MainGame
    {
        private GraphicsManager gfx = new GraphicsManager();
        private SpriteBatch spriteBatch;
        private OwnTexture2D image;
        private ReadArchive book;
        private Vector2 rotationOfImageOffset = Vector2.Zero;
        private float rotationOfImage = 0;
        private Rectangle bounds = new Rectangle();

        public MainGame(Control parentControl)
        {
            gfx.Create(parentControl);
            gfx.Draw += new EventHandler<EventArgs>(Draw);
            gfx.Update += new EventHandler<EventArgs>(Update);
            spriteBatch = new SpriteBatch(gfx.GraphicsDevice);

            book = new ReadArchive("C:\\a.cbr");
            UpdateImage(10);
            Rotate(0);
        }

        private Vector2 origin;

        public void UpdateImage(int num_page)
        {
            System.Drawing.Image img = book.ReadPageFromRar(num_page);
            ReadImage.Image2Texture(img, gfx.GraphicsDevice, ref image);
            origin.X = image.Image.Width / 2;
            origin.Y = image.Image.Height / 2;
            rotationOfImageOffset = origin;
        }

        private Vector2 offset;

        public Vector2 ImageOffset
        {
            get { return offset; }
            set { offset = value; gfx.Redraw(); Console.WriteLine(offset.ToString()); }
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

        public void Rotate(float angleDegree)
        {
            rotationOfImage = MathHelper.ToRadians(angleDegree);
            image.Corners.Rotation(angleDegree);
        }

        public void RotateWhenFit(float angleDegree)
        {
            rotationOfImage = MathHelper.ToRadians(angleDegree);
            rotationOfImageOffset = Vector2.Transform(new Vector2(gfx.ParentControl.Width, gfx.ParentControl.Height), Matrix.CreateRotationZ(rotationOfImage));

            rotationOfImageOffset.X = Math.Abs(rotationOfImageOffset.X);
            rotationOfImageOffset.Y = Math.Abs(rotationOfImageOffset.Y);

            origin.X = rotationOfImageOffset.X / 2;
            origin.Y = rotationOfImageOffset.Y / 2;

            bounds = new Rectangle(0, 0, (int)rotationOfImageOffset.X, (int)rotationOfImageOffset.Y);
        }

        private void Draw(object sender, EventArgs e)
        {
            Rotate(0);
            spriteBatch.Begin();
            spriteBatch.Draw(image.Image, offset, null, Color.Wheat, rotationOfImage, image.Corners.UpperRight, 1f, SpriteEffects.None, 0);
            //spriteBatch.Draw(image, bounds, null, Color.Wheat, rotationOfImage, origin, SpriteEffects.None, 0);
            spriteBatch.End();
        }
    }
}