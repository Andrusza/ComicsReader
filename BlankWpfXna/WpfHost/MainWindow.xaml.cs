using System;
using System.Windows;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using XnaGuest;

namespace WpfHost
{
    public partial class MainWindow : Window
    {
        private MainGame mainGame;

        public MainWindow()
        {
            InitializeComponent();

            mainGame = new MainGame(RenderingPanel);
            RenderingPanel.Select();
            RenderingPanel.MouseWheel += new System.Windows.Forms.MouseEventHandler(RenderingPanel_MouseWheel);
        }

        private void RenderingPanel_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            float offset = (float)e.Delta / 10f;
            mainGame.ImageOffset += new Vector2(0, offset);
        }

        private int[] oldPosition = new int[2];
        private int[] offset = new int[2];

        private void RenderingPanel_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            oldPosition[0] = e.X;
            oldPosition[1] = e.Y;
        }

        private void RenderingPanel_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            RenderingPanel.Focus();
        }

        private void RenderingPanel_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                offset[0] = (e.X - oldPosition[0]);
                offset[1] = (e.Y - oldPosition[1]);

                oldPosition[0] = e.X;
                oldPosition[1] = e.Y;
               

                mainGame.ImageOffset += new Vector2(offset[0], -offset[1]);
            }
        }

        private void RenderingPanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowStyle = WindowStyle.None;
            this.WindowState = WindowState.Maximized;
        }

        private void RenderingPanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                this.Width = Screen.PrimaryScreen.Bounds.Width;
                this.Height = Screen.PrimaryScreen.Bounds.Height;
                this.WindowState = WindowState.Normal;
                this.Top = 0;
                this.Left = 0;
            }
        }

        private void Win_MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
        }
    }
}