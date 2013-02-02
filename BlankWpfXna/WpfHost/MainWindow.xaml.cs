using System;
using System.Windows;
using XnaGuest;
using Microsoft.Xna.Framework;

namespace WpfHost
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            mainGame.UpdateLogo();
        }

        private void MainWindow_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            Console.WriteLine(e.Delta);
        }

        private void RenderingPanel_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mainGame.Vec += new Vector2(0, -10);
        }

        private void RenderingPanel_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Console.WriteLine("Dasdas");
        }
    }
}