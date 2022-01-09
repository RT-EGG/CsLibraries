using System.ComponentModel;
using System.Windows.Forms;

namespace RtCs.WinForms
{
    public static class ComponentExtensions
    {
        public static bool GetDesignMode(this Component inComponent)
        {
            bool mode = (inComponent.Site != null) && inComponent.Site.DesignMode;
            if (!(inComponent is Control)) {
                return mode;
            }

            Control parent = (inComponent as Control).Parent;
            while ((!mode) && (parent != null)) {
                ISite site = parent.Site;
                if (site != null) {
                    mode = site.DesignMode;
                }

                parent = parent.Parent;
            }
            return mode;
        }
    }
}
