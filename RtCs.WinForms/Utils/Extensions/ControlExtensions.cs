using System.Linq;
using System.Windows.Forms;

namespace RtCs.WinForms
{
    public static class ControlExtensions
    {
        public static void SetEnabledRecursive(this Control inControl, bool inValue)
        {
            inControl.Enabled = inValue;
            foreach (var child in inControl.Controls.OfType<Control>()) {
                child.SetEnabledRecursive(inValue);
            }
        }
    }
}
