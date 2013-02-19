using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XnaGuest
{
    internal class GraphicsManager
    {
        private GraphicsDevice device;
        private Control parentControl;
        private PresentationParameters presentParams;

        public GraphicsDevice GraphicsDevice { get { return device; } }

        public Control ParentControl { get { return parentControl; } }

        public event EventHandler<EventArgs> Draw;

        public System.Drawing.Size Size { get { return parentControl.Size; } }

        public float AspectRation { get { return (float)Size.Width / (float)Size.Height; } }

        public Color BackgroundColor { get; set; }

        private Timer timer = new Timer();

        public void Create(Control parentControl)
        {
            this.parentControl = parentControl;
            presentParams = new PresentationParameters();
            presentParams.IsFullScreen = false;
            presentParams.BackBufferWidth = parentControl.ClientSize.Width;
            presentParams.BackBufferHeight = parentControl.ClientSize.Height;
            presentParams.DeviceWindowHandle = parentControl.Handle;
            presentParams.MultiSampleCount = 4;
            device = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.HiDef, presentParams);
            device.DeviceLost += new EventHandler<EventArgs>(OnDeviceLost);

            parentControl.Paint += new PaintEventHandler(OnPaint);
            parentControl.Disposed += new EventHandler(OnDisposed);
            parentControl.SizeChanged += new EventHandler(OnSizeChanged);
            BackgroundColor = new Color(0.8f, 0.8f, 0.8f, 0);
            timer.Interval = 10;
            timer.Tick += new EventHandler(TimerTick);
            timer.Enabled = true;
        }

        public event EventHandler<EventArgs> Update;

        public event EventHandler<EventArgs> SizeChanged;

        private void TimerTick(object sender, EventArgs e)
        {
            if (Update != null)
                Update(sender, e);
            Redraw();
        }

        public void Redraw()
        {
            parentControl.Invalidate();
        }

        private void OnDeviceLost(object sender, EventArgs e)
        {
            device.Reset(presentParams);
        }

        private void OnSizeChanged(object sender, EventArgs e)
        {
            if (parentControl.ClientSize.Width == 0 || parentControl.ClientSize.Height == 0)
                return;
            presentParams.BackBufferWidth = parentControl.ClientSize.Width;
            presentParams.BackBufferHeight = parentControl.ClientSize.Height;

            device.Reset(presentParams);
            parentControl.Invalidate();
            if (SizeChanged != null)
                SizeChanged(sender, e);
        }

        private void OnDisposed(object sender, EventArgs e)
        {
            if (device != null)
                device.Dispose();
            device = null;
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            device.Clear(ClearOptions.Target, BackgroundColor, 0.0f, 0);
            if (Draw != null)
                Draw(this, new EventArgs());
            device.Present();
        }
    }
}