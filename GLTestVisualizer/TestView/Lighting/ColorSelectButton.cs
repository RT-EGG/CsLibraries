using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GLTestVisualizer.TestView.Lighting
{
    class ColorSelectButton : Button
    {
        public ColorSelectButton()
        {
            Text = "";
            return;
        }

        [Category("Action")]
        public event EventHandler ValueChanged;

        [Category("Appearance")]
        public Color Value
        {
            get => BackColor;
            set => BackColor = value;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            ShowPopup();
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);

            ValueChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ShowPopup()
        {
            if (m_Dialog != null) {
                m_Dialog.Close();
                m_Dialog = null;
                return;
            }

            m_Dialog = new ColorPickerPopupDialog();
            m_Dialog.ValueChanged += (sender, e) => {
                BackColor = m_Dialog.Value;
            };
            m_Dialog.FormClosed += (sender, e) => {
                m_Dialog = null;
            };

            Point dialogPosition = Parent.PointToScreen(Location);
            dialogPosition.X += Width;
            m_Dialog.Location = dialogPosition;
            m_Dialog.StartPosition = FormStartPosition.Manual;
            m_Dialog.Show();
        }

        private ColorPickerPopupDialog m_Dialog = null;
    }
}
