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
    }

}
