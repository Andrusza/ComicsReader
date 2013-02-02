using System;
using System.Windows.Forms;

namespace WpfHost
{
    public class OptimizedPanel : Panel
    {
        public OptimizedPanel()
            : base()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.Opaque, true);
        }

        protected override void OnMouseHover(EventArgs e)
        {
            this.Focus();
            base.OnMouseHover(e);
        }
    }
}