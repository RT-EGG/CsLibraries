
namespace GLTestVisualizer.TestView.FrustumTest
{
    partial class Ctrl_ProjectionFrustumParameterView
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

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.PanelParameters = new System.Windows.Forms.TableLayoutPanel();
            this.UdDepthLength = new System.Windows.Forms.NumericUpDown();
            this.LabelDepthLength = new System.Windows.Forms.Label();
            this.UdDepthMin = new System.Windows.Forms.NumericUpDown();
            this.LabelDepthMin = new System.Windows.Forms.Label();
            this.LabelDepth = new System.Windows.Forms.Label();
            this.UdSizeY = new System.Windows.Forms.NumericUpDown();
            this.LabelSizeY = new System.Windows.Forms.Label();
            this.UdOffsetY = new System.Windows.Forms.NumericUpDown();
            this.LabelOffsetY = new System.Windows.Forms.Label();
            this.UdSizeX = new System.Windows.Forms.NumericUpDown();
            this.LabelSizeX = new System.Windows.Forms.Label();
            this.LabelSize = new System.Windows.Forms.Label();
            this.LabelOffset = new System.Windows.Forms.Label();
            this.LabelOffsetX = new System.Windows.Forms.Label();
            this.UdOffsetX = new System.Windows.Forms.NumericUpDown();
            this.PanelParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UdDepthLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UdDepthMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UdSizeY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UdOffsetY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UdSizeX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UdOffsetX)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelParameters
            // 
            this.PanelParameters.ColumnCount = 6;
            this.PanelParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.PanelParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.PanelParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.PanelParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.PanelParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.PanelParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.PanelParameters.Controls.Add(this.UdDepthLength, 5, 2);
            this.PanelParameters.Controls.Add(this.LabelDepthLength, 4, 2);
            this.PanelParameters.Controls.Add(this.UdDepthMin, 5, 1);
            this.PanelParameters.Controls.Add(this.LabelDepthMin, 4, 1);
            this.PanelParameters.Controls.Add(this.LabelDepth, 4, 0);
            this.PanelParameters.Controls.Add(this.UdSizeY, 3, 2);
            this.PanelParameters.Controls.Add(this.LabelSizeY, 2, 2);
            this.PanelParameters.Controls.Add(this.UdOffsetY, 1, 2);
            this.PanelParameters.Controls.Add(this.LabelOffsetY, 0, 2);
            this.PanelParameters.Controls.Add(this.UdSizeX, 3, 1);
            this.PanelParameters.Controls.Add(this.LabelSizeX, 2, 1);
            this.PanelParameters.Controls.Add(this.LabelSize, 2, 0);
            this.PanelParameters.Controls.Add(this.LabelOffset, 0, 0);
            this.PanelParameters.Controls.Add(this.LabelOffsetX, 0, 1);
            this.PanelParameters.Controls.Add(this.UdOffsetX, 1, 1);
            this.PanelParameters.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelParameters.Location = new System.Drawing.Point(0, 0);
            this.PanelParameters.Name = "PanelParameters";
            this.PanelParameters.RowCount = 4;
            this.PanelParameters.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PanelParameters.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PanelParameters.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PanelParameters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PanelParameters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.PanelParameters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.PanelParameters.Size = new System.Drawing.Size(500, 133);
            this.PanelParameters.TabIndex = 1;
            // 
            // UdDepthLength
            // 
            this.UdDepthLength.DecimalPlaces = 3;
            this.UdDepthLength.Dock = System.Windows.Forms.DockStyle.Left;
            this.UdDepthLength.Location = new System.Drawing.Point(367, 46);
            this.UdDepthLength.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.UdDepthLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.UdDepthLength.Name = "UdDepthLength";
            this.UdDepthLength.Size = new System.Drawing.Size(120, 19);
            this.UdDepthLength.TabIndex = 14;
            this.UdDepthLength.Value = new decimal(new int[] {
            10001,
            0,
            0,
            131072});
            // 
            // LabelDepthLength
            // 
            this.LabelDepthLength.AutoSize = true;
            this.LabelDepthLength.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelDepthLength.Location = new System.Drawing.Point(319, 43);
            this.LabelDepthLength.Name = "LabelDepthLength";
            this.LabelDepthLength.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.LabelDepthLength.Size = new System.Drawing.Size(42, 25);
            this.LabelDepthLength.TabIndex = 13;
            this.LabelDepthLength.Text = "length :";
            this.LabelDepthLength.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UdDepthMin
            // 
            this.UdDepthMin.DecimalPlaces = 3;
            this.UdDepthMin.Dock = System.Windows.Forms.DockStyle.Left;
            this.UdDepthMin.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.UdDepthMin.Location = new System.Drawing.Point(367, 21);
            this.UdDepthMin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.UdDepthMin.Name = "UdDepthMin";
            this.UdDepthMin.Size = new System.Drawing.Size(120, 19);
            this.UdDepthMin.TabIndex = 7;
            this.UdDepthMin.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            // 
            // LabelDepthMin
            // 
            this.LabelDepthMin.AutoSize = true;
            this.LabelDepthMin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelDepthMin.Location = new System.Drawing.Point(319, 18);
            this.LabelDepthMin.Name = "LabelDepthMin";
            this.LabelDepthMin.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.LabelDepthMin.Size = new System.Drawing.Size(42, 25);
            this.LabelDepthMin.TabIndex = 11;
            this.LabelDepthMin.Text = "min :";
            this.LabelDepthMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LabelDepth
            // 
            this.LabelDepth.AutoSize = true;
            this.PanelParameters.SetColumnSpan(this.LabelDepth, 2);
            this.LabelDepth.Dock = System.Windows.Forms.DockStyle.Left;
            this.LabelDepth.Location = new System.Drawing.Point(319, 0);
            this.LabelDepth.Name = "LabelDepth";
            this.LabelDepth.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.LabelDepth.Size = new System.Drawing.Size(35, 18);
            this.LabelDepth.TabIndex = 10;
            this.LabelDepth.Text = "Depth";
            this.LabelDepth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UdSizeY
            // 
            this.UdSizeY.DecimalPlaces = 3;
            this.UdSizeY.Dock = System.Windows.Forms.DockStyle.Left;
            this.UdSizeY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.UdSizeY.Location = new System.Drawing.Point(184, 46);
            this.UdSizeY.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UdSizeY.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.UdSizeY.Name = "UdSizeY";
            this.UdSizeY.Size = new System.Drawing.Size(120, 19);
            this.UdSizeY.TabIndex = 12;
            this.UdSizeY.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.UdSizeY.ValueChanged += new System.EventHandler(this.UdSizeY_Changed);
            // 
            // LabelSizeY
            // 
            this.LabelSizeY.AutoSize = true;
            this.LabelSizeY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelSizeY.Location = new System.Drawing.Point(161, 43);
            this.LabelSizeY.Name = "LabelSizeY";
            this.LabelSizeY.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.LabelSizeY.Size = new System.Drawing.Size(17, 25);
            this.LabelSizeY.TabIndex = 8;
            this.LabelSizeY.Text = "y :";
            this.LabelSizeY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UdOffsetY
            // 
            this.UdOffsetY.DecimalPlaces = 3;
            this.UdOffsetY.Dock = System.Windows.Forms.DockStyle.Left;
            this.UdOffsetY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.UdOffsetY.Location = new System.Drawing.Point(26, 46);
            this.UdOffsetY.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UdOffsetY.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.UdOffsetY.Name = "UdOffsetY";
            this.UdOffsetY.Size = new System.Drawing.Size(120, 19);
            this.UdOffsetY.TabIndex = 9;
            // 
            // LabelOffsetY
            // 
            this.LabelOffsetY.AutoSize = true;
            this.LabelOffsetY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelOffsetY.Location = new System.Drawing.Point(3, 43);
            this.LabelOffsetY.Name = "LabelOffsetY";
            this.LabelOffsetY.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.LabelOffsetY.Size = new System.Drawing.Size(17, 25);
            this.LabelOffsetY.TabIndex = 6;
            this.LabelOffsetY.Text = "y :";
            this.LabelOffsetY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UdSizeX
            // 
            this.UdSizeX.DecimalPlaces = 3;
            this.UdSizeX.Dock = System.Windows.Forms.DockStyle.Left;
            this.UdSizeX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.UdSizeX.Location = new System.Drawing.Point(184, 21);
            this.UdSizeX.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UdSizeX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.UdSizeX.Name = "UdSizeX";
            this.UdSizeX.Size = new System.Drawing.Size(120, 19);
            this.UdSizeX.TabIndex = 5;
            this.UdSizeX.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            // 
            // LabelSizeX
            // 
            this.LabelSizeX.AutoSize = true;
            this.LabelSizeX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelSizeX.Location = new System.Drawing.Point(161, 18);
            this.LabelSizeX.Name = "LabelSizeX";
            this.LabelSizeX.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.LabelSizeX.Size = new System.Drawing.Size(17, 25);
            this.LabelSizeX.TabIndex = 4;
            this.LabelSizeX.Text = "x :";
            this.LabelSizeX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LabelSize
            // 
            this.LabelSize.AutoSize = true;
            this.PanelParameters.SetColumnSpan(this.LabelSize, 2);
            this.LabelSize.Dock = System.Windows.Forms.DockStyle.Left;
            this.LabelSize.Location = new System.Drawing.Point(161, 0);
            this.LabelSize.Name = "LabelSize";
            this.LabelSize.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.LabelSize.Size = new System.Drawing.Size(26, 18);
            this.LabelSize.TabIndex = 1;
            this.LabelSize.Text = "Size";
            this.LabelSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LabelOffset
            // 
            this.LabelOffset.AutoSize = true;
            this.PanelParameters.SetColumnSpan(this.LabelOffset, 2);
            this.LabelOffset.Dock = System.Windows.Forms.DockStyle.Left;
            this.LabelOffset.Location = new System.Drawing.Point(3, 0);
            this.LabelOffset.Name = "LabelOffset";
            this.LabelOffset.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.LabelOffset.Size = new System.Drawing.Size(37, 18);
            this.LabelOffset.TabIndex = 0;
            this.LabelOffset.Text = "Offset";
            this.LabelOffset.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LabelOffsetX
            // 
            this.LabelOffsetX.AutoSize = true;
            this.LabelOffsetX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelOffsetX.Location = new System.Drawing.Point(3, 18);
            this.LabelOffsetX.Name = "LabelOffsetX";
            this.LabelOffsetX.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.LabelOffsetX.Size = new System.Drawing.Size(17, 25);
            this.LabelOffsetX.TabIndex = 2;
            this.LabelOffsetX.Text = "x :";
            this.LabelOffsetX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UdOffsetX
            // 
            this.UdOffsetX.DecimalPlaces = 3;
            this.UdOffsetX.Dock = System.Windows.Forms.DockStyle.Left;
            this.UdOffsetX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.UdOffsetX.Location = new System.Drawing.Point(26, 21);
            this.UdOffsetX.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UdOffsetX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.UdOffsetX.Name = "UdOffsetX";
            this.UdOffsetX.Size = new System.Drawing.Size(120, 19);
            this.UdOffsetX.TabIndex = 3;
            // 
            // Ctrl_ProjectionFrustumParameterView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.PanelParameters);
            this.Name = "Ctrl_ProjectionFrustumParameterView";
            this.Size = new System.Drawing.Size(592, 133);
            this.PanelParameters.ResumeLayout(false);
            this.PanelParameters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UdDepthLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UdDepthMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UdSizeY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UdOffsetY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UdSizeX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UdOffsetX)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel PanelParameters;
        private System.Windows.Forms.NumericUpDown UdDepthLength;
        private System.Windows.Forms.Label LabelDepthLength;
        private System.Windows.Forms.NumericUpDown UdDepthMin;
        private System.Windows.Forms.Label LabelDepthMin;
        private System.Windows.Forms.Label LabelDepth;
        private System.Windows.Forms.NumericUpDown UdSizeY;
        private System.Windows.Forms.Label LabelSizeY;
        private System.Windows.Forms.NumericUpDown UdOffsetY;
        private System.Windows.Forms.Label LabelOffsetY;
        private System.Windows.Forms.NumericUpDown UdSizeX;
        private System.Windows.Forms.Label LabelSizeX;
        private System.Windows.Forms.Label LabelSize;
        private System.Windows.Forms.Label LabelOffset;
        private System.Windows.Forms.Label LabelOffsetX;
        private System.Windows.Forms.NumericUpDown UdOffsetX;
    }
}
