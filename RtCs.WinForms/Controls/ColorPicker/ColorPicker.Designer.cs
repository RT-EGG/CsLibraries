
namespace RtCs.WinForms.Controls.ColorPicker
{
    partial class ColorPicker
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.SampleColorPanel = new DoubleBufferedPanel();
            this.SVPicker = new RtCs.WinForms.Controls.ColorPickerBox();
            this.HSlider = new RtCs.WinForms.Controls.ColorSlider();
            this.ColorInput = new RtCs.WinForms.Controls.ColorPicker.ColorInput();
            this.SuspendLayout();
            // 
            // SampleColorPanel
            // 
            this.SampleColorPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SampleColorPanel.BackColor = System.Drawing.Color.White;
            this.SampleColorPanel.Location = new System.Drawing.Point(3, 259);
            this.SampleColorPanel.Name = "SampleColorPanel";
            this.SampleColorPanel.Size = new System.Drawing.Size(93, 145);
            this.SampleColorPanel.TabIndex = 2;
            // 
            // SVPicker
            // 
            this.SVPicker.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SVPicker.LeftBottomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.SVPicker.LeftTopColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.SVPicker.Location = new System.Drawing.Point(3, 3);
            this.SVPicker.MarkerColor = System.Drawing.Color.Black;
            this.SVPicker.Name = "SVPicker";
            this.SVPicker.RightBottomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.SVPicker.RightTopColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.SVPicker.Size = new System.Drawing.Size(250, 250);
            this.SVPicker.TabIndex = 1;
            this.SVPicker.ValueX = 0F;
            this.SVPicker.ValueY = 0F;
            this.SVPicker.ValueChanged += new System.EventHandler(this.SVPicker_ValueChanged);
            // 
            // HSlider
            // 
            this.HSlider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HSlider.BackColor = System.Drawing.Color.Transparent;
            this.HSlider.Colors = new System.Drawing.Color[] {
        System.Drawing.Color.Black,
        System.Drawing.Color.White};
            this.HSlider.Direction = RtCs.WinForms.Controls.EColorSliderDirection.Vertical;
            this.HSlider.Location = new System.Drawing.Point(259, 3);
            this.HSlider.Name = "HSlider";
            this.HSlider.Size = new System.Drawing.Size(31, 250);
            this.HSlider.TabIndex = 0;
            this.HSlider.Value = 0F;
            this.HSlider.ValueLineColor = System.Drawing.Color.Black;
            this.HSlider.ValueLineOverhangLength = 8;
            this.HSlider.ValueLineWidth = 3;
            this.HSlider.ValueChanged += new System.EventHandler(this.HSlider_ValueChanged);
            // 
            // ColorInput
            // 
            this.ColorInput.AlphaControlVisible = true;
            this.ColorInput.Location = new System.Drawing.Point(102, 259);
            this.ColorInput.Name = "ColorInput";
            this.ColorInput.Size = new System.Drawing.Size(188, 145);
            this.ColorInput.TabIndex = 3;
            this.ColorInput.Value = System.Drawing.Color.Transparent;
            this.ColorInput.ValueChanged += new System.EventHandler(this.ColorInput_ValueChanged);
            // 
            // ColorPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ColorInput);
            this.Controls.Add(this.SampleColorPanel);
            this.Controls.Add(this.SVPicker);
            this.Controls.Add(this.HSlider);
            this.Name = "ColorPicker";
            this.Size = new System.Drawing.Size(296, 409);
            this.ResumeLayout(false);

        }

        #endregion

        private ColorSlider HSlider;
        private ColorPickerBox SVPicker;
        private DoubleBufferedPanel SampleColorPanel;
        private ColorInput ColorInput;
    }
}
