
namespace GLTestVisualizer.TestView.Lighting
{
    partial class ColorPickerPopupDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ColorPicker = new RtCs.WinForms.Controls.ColorPicker.ColorPicker();
            this.SuspendLayout();
            // 
            // ColorPicker
            // 
            this.ColorPicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ColorPicker.Location = new System.Drawing.Point(0, 0);
            this.ColorPicker.Name = "ColorPicker";
            this.ColorPicker.Size = new System.Drawing.Size(265, 305);
            this.ColorPicker.TabIndex = 0;
            this.ColorPicker.Value = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ColorPicker.ValueChanged += new System.EventHandler(this.ColorPicker_ValueChanged);
            // 
            // ColorPickerPopupDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 305);
            this.ControlBox = false;
            this.Controls.Add(this.ColorPicker);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ColorPickerPopupDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "ColorPickerDialog";
            this.ResumeLayout(false);

        }

        #endregion

        private RtCs.WinForms.Controls.ColorPicker.ColorPicker ColorPicker;
    }
}