using System.Windows.Forms;

namespace RtCs.WinForms.Controls
{
    public class DoubleBufferedPanel : Panel
    {
        public DoubleBufferedPanel()
            : base()
        {
            DoubleBuffered = true;
        }
    }
}
