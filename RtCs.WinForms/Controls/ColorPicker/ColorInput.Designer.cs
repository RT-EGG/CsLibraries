
namespace RtCs.WinForms.Controls.ColorPicker
{
    partial class ColorInput
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
            this.ElementLabel0 = new System.Windows.Forms.Label();
            this.TypeSelectPanel = new System.Windows.Forms.Panel();
            this.RadioHsvSelectMode = new System.Windows.Forms.RadioButton();
            this.RadioRgbSelectMode = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.AlphaUpDown = new System.Windows.Forms.NumericUpDown();
            this.AlphaSlider = new RtCs.WinForms.Controls.ColorSlider();
            this.AlphaLabel = new System.Windows.Forms.Label();
            this.ElementUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.ElementSlider2 = new RtCs.WinForms.Controls.ColorSlider();
            this.ElementLabel2 = new System.Windows.Forms.Label();
            this.ElementUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.ElementSlider1 = new RtCs.WinForms.Controls.ColorSlider();
            this.ElementLabel1 = new System.Windows.Forms.Label();
            this.ElementUpDown0 = new System.Windows.Forms.NumericUpDown();
            this.ElementSlider0 = new RtCs.WinForms.Controls.ColorSlider();
            this.TypeSelectPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AlphaUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ElementUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ElementUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ElementUpDown0)).BeginInit();
            this.SuspendLayout();
            // 
            // ElementLabel0
            // 
            this.ElementLabel0.AutoSize = true;
            this.ElementLabel0.Dock = System.Windows.Forms.DockStyle.Left;
            this.ElementLabel0.Location = new System.Drawing.Point(3, 28);
            this.ElementLabel0.Name = "ElementLabel0";
            this.ElementLabel0.Size = new System.Drawing.Size(19, 27);
            this.ElementLabel0.TabIndex = 1;
            this.ElementLabel0.Text = "R :";
            this.ElementLabel0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TypeSelectPanel
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.TypeSelectPanel, 3);
            this.TypeSelectPanel.Controls.Add(this.RadioHsvSelectMode);
            this.TypeSelectPanel.Controls.Add(this.RadioRgbSelectMode);
            this.TypeSelectPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TypeSelectPanel.Location = new System.Drawing.Point(3, 3);
            this.TypeSelectPanel.Name = "TypeSelectPanel";
            this.TypeSelectPanel.Padding = new System.Windows.Forms.Padding(3);
            this.TypeSelectPanel.Size = new System.Drawing.Size(261, 22);
            this.TypeSelectPanel.TabIndex = 0;
            // 
            // RadioHsvSelectMode
            // 
            this.RadioHsvSelectMode.AutoSize = true;
            this.RadioHsvSelectMode.Dock = System.Windows.Forms.DockStyle.Left;
            this.RadioHsvSelectMode.Location = new System.Drawing.Point(50, 3);
            this.RadioHsvSelectMode.Name = "RadioHsvSelectMode";
            this.RadioHsvSelectMode.Size = new System.Drawing.Size(46, 16);
            this.RadioHsvSelectMode.TabIndex = 1;
            this.RadioHsvSelectMode.Text = "HSV";
            this.RadioHsvSelectMode.UseVisualStyleBackColor = true;
            // 
            // RadioRgbSelectMode
            // 
            this.RadioRgbSelectMode.AutoSize = true;
            this.RadioRgbSelectMode.Checked = true;
            this.RadioRgbSelectMode.Dock = System.Windows.Forms.DockStyle.Left;
            this.RadioRgbSelectMode.Location = new System.Drawing.Point(3, 3);
            this.RadioRgbSelectMode.Name = "RadioRgbSelectMode";
            this.RadioRgbSelectMode.Size = new System.Drawing.Size(47, 16);
            this.RadioRgbSelectMode.TabIndex = 0;
            this.RadioRgbSelectMode.TabStop = true;
            this.RadioRgbSelectMode.Text = "RGB";
            this.RadioRgbSelectMode.UseVisualStyleBackColor = true;
            this.RadioRgbSelectMode.CheckedChanged += new System.EventHandler(this.RadioSelectModeChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel1.Controls.Add(this.AlphaUpDown, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.AlphaSlider, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.AlphaLabel, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.ElementUpDown2, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.ElementSlider2, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.ElementLabel2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.ElementUpDown1, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.ElementSlider1, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.ElementLabel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.TypeSelectPanel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ElementLabel0, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.ElementUpDown0, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.ElementSlider0, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(267, 139);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // AlphaUpDown
            // 
            this.AlphaUpDown.Location = new System.Drawing.Point(223, 112);
            this.AlphaUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.AlphaUpDown.Name = "AlphaUpDown";
            this.AlphaUpDown.Size = new System.Drawing.Size(41, 19);
            this.AlphaUpDown.TabIndex = 13;
            this.AlphaUpDown.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // AlphaSlider
            // 
            this.AlphaSlider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AlphaSlider.BackColor = System.Drawing.Color.Transparent;
            this.AlphaSlider.Colors = new System.Drawing.Color[] {
        System.Drawing.Color.Transparent,
        System.Drawing.Color.Red};
            this.AlphaSlider.Direction = RtCs.WinForms.Controls.EColorSliderDirection.Horizontal;
            this.AlphaSlider.Location = new System.Drawing.Point(28, 112);
            this.AlphaSlider.Name = "AlphaSlider";
            this.AlphaSlider.Size = new System.Drawing.Size(189, 21);
            this.AlphaSlider.TabIndex = 12;
            this.AlphaSlider.Value = 0F;
            this.AlphaSlider.ValueLineColor = System.Drawing.Color.Black;
            this.AlphaSlider.ValueLineOverhangLength = 8;
            this.AlphaSlider.ValueLineWidth = 3;
            // 
            // AlphaLabel
            // 
            this.AlphaLabel.AutoSize = true;
            this.AlphaLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.AlphaLabel.Location = new System.Drawing.Point(3, 109);
            this.AlphaLabel.Name = "AlphaLabel";
            this.AlphaLabel.Size = new System.Drawing.Size(19, 27);
            this.AlphaLabel.TabIndex = 11;
            this.AlphaLabel.Text = "A :";
            this.AlphaLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ElementUpDown2
            // 
            this.ElementUpDown2.Location = new System.Drawing.Point(223, 85);
            this.ElementUpDown2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ElementUpDown2.Name = "ElementUpDown2";
            this.ElementUpDown2.Size = new System.Drawing.Size(41, 19);
            this.ElementUpDown2.TabIndex = 10;
            this.ElementUpDown2.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // ElementSlider2
            // 
            this.ElementSlider2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ElementSlider2.BackColor = System.Drawing.Color.Transparent;
            this.ElementSlider2.Colors = new System.Drawing.Color[] {
        System.Drawing.Color.Black,
        System.Drawing.Color.Blue};
            this.ElementSlider2.Direction = RtCs.WinForms.Controls.EColorSliderDirection.Horizontal;
            this.ElementSlider2.Location = new System.Drawing.Point(28, 85);
            this.ElementSlider2.Name = "ElementSlider2";
            this.ElementSlider2.Size = new System.Drawing.Size(189, 21);
            this.ElementSlider2.TabIndex = 9;
            this.ElementSlider2.Value = 0F;
            this.ElementSlider2.ValueLineColor = System.Drawing.Color.Black;
            this.ElementSlider2.ValueLineOverhangLength = 8;
            this.ElementSlider2.ValueLineWidth = 3;
            // 
            // ElementLabel2
            // 
            this.ElementLabel2.AutoSize = true;
            this.ElementLabel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.ElementLabel2.Location = new System.Drawing.Point(3, 82);
            this.ElementLabel2.Name = "ElementLabel2";
            this.ElementLabel2.Size = new System.Drawing.Size(19, 27);
            this.ElementLabel2.TabIndex = 8;
            this.ElementLabel2.Text = "B :";
            this.ElementLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ElementUpDown1
            // 
            this.ElementUpDown1.Location = new System.Drawing.Point(223, 58);
            this.ElementUpDown1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ElementUpDown1.Name = "ElementUpDown1";
            this.ElementUpDown1.Size = new System.Drawing.Size(41, 19);
            this.ElementUpDown1.TabIndex = 7;
            this.ElementUpDown1.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // ElementSlider1
            // 
            this.ElementSlider1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ElementSlider1.BackColor = System.Drawing.Color.Transparent;
            this.ElementSlider1.Colors = new System.Drawing.Color[] {
        System.Drawing.Color.Black,
        System.Drawing.Color.Green};
            this.ElementSlider1.Direction = RtCs.WinForms.Controls.EColorSliderDirection.Horizontal;
            this.ElementSlider1.Location = new System.Drawing.Point(28, 58);
            this.ElementSlider1.Name = "ElementSlider1";
            this.ElementSlider1.Size = new System.Drawing.Size(189, 21);
            this.ElementSlider1.TabIndex = 6;
            this.ElementSlider1.Value = 0F;
            this.ElementSlider1.ValueLineColor = System.Drawing.Color.Black;
            this.ElementSlider1.ValueLineOverhangLength = 8;
            this.ElementSlider1.ValueLineWidth = 3;
            // 
            // ElementLabel1
            // 
            this.ElementLabel1.AutoSize = true;
            this.ElementLabel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.ElementLabel1.Location = new System.Drawing.Point(3, 55);
            this.ElementLabel1.Name = "ElementLabel1";
            this.ElementLabel1.Size = new System.Drawing.Size(19, 27);
            this.ElementLabel1.TabIndex = 5;
            this.ElementLabel1.Text = "G :";
            this.ElementLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ElementUpDown0
            // 
            this.ElementUpDown0.Location = new System.Drawing.Point(223, 31);
            this.ElementUpDown0.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ElementUpDown0.Name = "ElementUpDown0";
            this.ElementUpDown0.Size = new System.Drawing.Size(41, 19);
            this.ElementUpDown0.TabIndex = 3;
            this.ElementUpDown0.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // ElementSlider0
            // 
            this.ElementSlider0.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ElementSlider0.BackColor = System.Drawing.Color.Transparent;
            this.ElementSlider0.Colors = new System.Drawing.Color[] {
        System.Drawing.Color.Black,
        System.Drawing.Color.Red};
            this.ElementSlider0.Direction = RtCs.WinForms.Controls.EColorSliderDirection.Horizontal;
            this.ElementSlider0.Location = new System.Drawing.Point(28, 31);
            this.ElementSlider0.Name = "ElementSlider0";
            this.ElementSlider0.Size = new System.Drawing.Size(189, 21);
            this.ElementSlider0.TabIndex = 4;
            this.ElementSlider0.Value = 0F;
            this.ElementSlider0.ValueLineColor = System.Drawing.Color.Black;
            this.ElementSlider0.ValueLineOverhangLength = 8;
            this.ElementSlider0.ValueLineWidth = 3;
            // 
            // ColorInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ColorInput";
            this.Size = new System.Drawing.Size(267, 139);
            this.TypeSelectPanel.ResumeLayout(false);
            this.TypeSelectPanel.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AlphaUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ElementUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ElementUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ElementUpDown0)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label ElementLabel0;
        private System.Windows.Forms.Panel TypeSelectPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.NumericUpDown ElementUpDown0;
        private ColorSlider ElementSlider0;
        private System.Windows.Forms.RadioButton RadioHsvSelectMode;
        private System.Windows.Forms.RadioButton RadioRgbSelectMode;
        private System.Windows.Forms.Label AlphaLabel;
        private System.Windows.Forms.NumericUpDown ElementUpDown2;
        private ColorSlider ElementSlider2;
        private System.Windows.Forms.Label ElementLabel2;
        private System.Windows.Forms.NumericUpDown ElementUpDown1;
        private ColorSlider ElementSlider1;
        private System.Windows.Forms.Label ElementLabel1;
        private System.Windows.Forms.NumericUpDown AlphaUpDown;
        private ColorSlider AlphaSlider;
    }
}
