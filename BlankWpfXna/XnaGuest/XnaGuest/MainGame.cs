using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Windows.Forms;
using System.IO;

namespace XnaGuest
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainGame
    {
        GraphicsManager gfx = new GraphicsManager();
        SpriteBatch spriteBatch;
        Texture2D logo;

        public MainGame(Control parentControl)
        {
            gfx.Create(parentControl);
            gfx.Draw += new EventHandler<EventArgs>(Draw);
            gfx.Update += new EventHandler<EventArgs>(Update);
            
            spriteBatch = new SpriteBatch(gfx.GraphicsDevice);
            logo = Texture2D.FromStream(gfx.GraphicsDevice, new StreamReader("logo.png").BaseStream);
        }

        private void Update(object sender, EventArgs e)
        {
            FrameworkDispatcher.Update();
        }


        private void Draw(object sender, EventArgs e)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(logo, Vector2.One * 50, Color.White);
            spriteBatch.End();
        }
    }

}
