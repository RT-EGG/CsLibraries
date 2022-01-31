
namespace Snow.View
{
    partial class SnowCoverVisibilityConfigurationView
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
            this.LabelChannelVisibility = new System.Windows.Forms.Label();
            this.PanelChannelVisibility = new System.Windows.Forms.Panel();
            this.CheckChannelVisibilityB = new System.Windows.Forms.CheckBox();
            this.CheckChannelVisibilityG = new System.Windows.Forms.CheckBox();
            this.CheckChannelVisibilityR = new System.Windows.Forms.CheckBox();
            this.PanelPolygonMode = new System.Windows.Forms.Panel();
            this.RadioRenderLineMode = new System.Windows.Forms.RadioButton();
            this.RadioRenderFaceMode = new System.Windows.Forms.RadioButton();
            this.PanelTessLevel = new System.Windows.Forms.TableLayoutPanel();
            this.UdOuterLevel = new System.Windows.Forms.NumericUpDown();
            this.LabelInnerLevel = new System.Windows.Forms.Label();
            this.UdInnerLevel = new System.Windows.Forms.NumericUpDown();
            this.LabelOuterLevel = new System.Windows.Forms.Label();
            this.PanelHeight = new System.Windows.Forms.TableLayoutPanel();
            this.UdHeightScale = new System.Windows.Forms.NumericUpDown();
            this.LabelHeightScale = new System.Windows.Forms.Label();
            this.PanelRenderMode = new System.Windows.Forms.Panel();
            this.RadioRenderOffsetMode = new System.Windows.Forms.RadioButton();
            this.RadioRenderNormalMode = new System.Windows.Forms.RadioButton();
            this.RadioRenderSurfaceMode = new System.Windows.Forms.RadioButton();
            this.PanelChannelVisibility.SuspendLayout();
            this.PanelPolygonMode.SuspendLayout();
            this.PanelTessLevel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UdOuterLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UdInnerLevel)).BeginInit();
            this.PanelHeight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UdHeightScale)).BeginInit();
            this.PanelRenderMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // LabelChannelVisibility
            // 
            this.LabelChannelVisibility.AutoSize = true;
            this.LabelChannelVisibility.Dock = System.Windows.Forms.DockStyle.Top;
            this.LabelChannelVisibility.Location = new System.Drawing.Point(0, 0);
            this.LabelChannelVisibility.Name = "LabelChannelVisibility";
            this.LabelChannelVisibility.Size = new System.Drawing.Size(46, 12);
            this.LabelChannelVisibility.TabIndex = 0;
            this.LabelChannelVisibility.Text = "Channel";
            // 
            // PanelChannelVisibility
            // 
            this.PanelChannelVisibility.Controls.Add(this.CheckChannelVisibilityB);
            this.PanelChannelVisibility.Controls.Add(this.CheckChannelVisibilityG);
            this.PanelChannelVisibility.Controls.Add(this.CheckChannelVisibilityR);
            this.PanelChannelVisibility.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelChannelVisibility.Location = new System.Drawing.Point(0, 12);
            this.PanelChannelVisibility.Name = "PanelChannelVisibility";
            this.PanelChannelVisibility.Padding = new System.Windows.Forms.Padding(3);
            this.PanelChannelVisibility.Size = new System.Drawing.Size(215, 22);
            this.PanelChannelVisibility.TabIndex = 1;
            // 
            // CheckChannelVisibilityB
            // 
            this.CheckChannelVisibilityB.AutoSize = true;
            this.CheckChannelVisibilityB.BackColor = System.Drawing.Color.Blue;
            this.CheckChannelVisibilityB.Dock = System.Windows.Forms.DockStyle.Left;
            this.CheckChannelVisibilityB.ForeColor = System.Drawing.Color.White;
            this.CheckChannelVisibilityB.Location = new System.Drawing.Point(67, 3);
            this.CheckChannelVisibilityB.Name = "CheckChannelVisibilityB";
            this.CheckChannelVisibilityB.Size = new System.Drawing.Size(32, 16);
            this.CheckChannelVisibilityB.TabIndex = 3;
            this.CheckChannelVisibilityB.Text = "B";
            this.CheckChannelVisibilityB.UseVisualStyleBackColor = false;
            this.CheckChannelVisibilityB.CheckedChanged += new System.EventHandler(this.CheckChannelVisibilityB_CheckedChanged);
            // 
            // CheckChannelVisibilityG
            // 
            this.CheckChannelVisibilityG.AutoSize = true;
            this.CheckChannelVisibilityG.BackColor = System.Drawing.Color.Green;
            this.CheckChannelVisibilityG.Dock = System.Windows.Forms.DockStyle.Left;
            this.CheckChannelVisibilityG.ForeColor = System.Drawing.Color.White;
            this.CheckChannelVisibilityG.Location = new System.Drawing.Point(35, 3);
            this.CheckChannelVisibilityG.Name = "CheckChannelVisibilityG";
            this.CheckChannelVisibilityG.Size = new System.Drawing.Size(32, 16);
            this.CheckChannelVisibilityG.TabIndex = 2;
            this.CheckChannelVisibilityG.Text = "G";
            this.CheckChannelVisibilityG.UseVisualStyleBackColor = false;
            this.CheckChannelVisibilityG.CheckedChanged += new System.EventHandler(this.CheckChannelVisibilityG_CheckedChanged);
            // 
            // CheckChannelVisibilityR
            // 
            this.CheckChannelVisibilityR.AutoSize = true;
            this.CheckChannelVisibilityR.BackColor = System.Drawing.Color.Red;
            this.CheckChannelVisibilityR.Dock = System.Windows.Forms.DockStyle.Left;
            this.CheckChannelVisibilityR.ForeColor = System.Drawing.Color.White;
            this.CheckChannelVisibilityR.Location = new System.Drawing.Point(3, 3);
            this.CheckChannelVisibilityR.Name = "CheckChannelVisibilityR";
            this.CheckChannelVisibilityR.Size = new System.Drawing.Size(32, 16);
            this.CheckChannelVisibilityR.TabIndex = 1;
            this.CheckChannelVisibilityR.Text = "R";
            this.CheckChannelVisibilityR.UseVisualStyleBackColor = false;
            this.CheckChannelVisibilityR.CheckedChanged += new System.EventHandler(this.CheckChannelVisibilityR_CheckedChanged);
            // 
            // PanelPolygonMode
            // 
            this.PanelPolygonMode.Controls.Add(this.RadioRenderLineMode);
            this.PanelPolygonMode.Controls.Add(this.RadioRenderFaceMode);
            this.PanelPolygonMode.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelPolygonMode.Location = new System.Drawing.Point(0, 56);
            this.PanelPolygonMode.Name = "PanelPolygonMode";
            this.PanelPolygonMode.Padding = new System.Windows.Forms.Padding(3);
            this.PanelPolygonMode.Size = new System.Drawing.Size(215, 22);
            this.PanelPolygonMode.TabIndex = 2;
            // 
            // RadioRenderLineMode
            // 
            this.RadioRenderLineMode.AutoSize = true;
            this.RadioRenderLineMode.Dock = System.Windows.Forms.DockStyle.Left;
            this.RadioRenderLineMode.Location = new System.Drawing.Point(51, 3);
            this.RadioRenderLineMode.Name = "RadioRenderLineMode";
            this.RadioRenderLineMode.Size = new System.Drawing.Size(44, 16);
            this.RadioRenderLineMode.TabIndex = 1;
            this.RadioRenderLineMode.Text = "Line";
            this.RadioRenderLineMode.UseVisualStyleBackColor = true;
            this.RadioRenderLineMode.CheckedChanged += new System.EventHandler(this.RadioRenderPolygonMode_CheckedChanged);
            // 
            // RadioRenderFaceMode
            // 
            this.RadioRenderFaceMode.AutoSize = true;
            this.RadioRenderFaceMode.Checked = true;
            this.RadioRenderFaceMode.Dock = System.Windows.Forms.DockStyle.Left;
            this.RadioRenderFaceMode.Location = new System.Drawing.Point(3, 3);
            this.RadioRenderFaceMode.Name = "RadioRenderFaceMode";
            this.RadioRenderFaceMode.Size = new System.Drawing.Size(48, 16);
            this.RadioRenderFaceMode.TabIndex = 0;
            this.RadioRenderFaceMode.TabStop = true;
            this.RadioRenderFaceMode.Text = "Face";
            this.RadioRenderFaceMode.UseVisualStyleBackColor = true;
            this.RadioRenderFaceMode.CheckedChanged += new System.EventHandler(this.RadioRenderPolygonMode_CheckedChanged);
            // 
            // PanelTessLevel
            // 
            this.PanelTessLevel.ColumnCount = 2;
            this.PanelTessLevel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.PanelTessLevel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PanelTessLevel.Controls.Add(this.UdOuterLevel, 1, 1);
            this.PanelTessLevel.Controls.Add(this.LabelInnerLevel, 0, 0);
            this.PanelTessLevel.Controls.Add(this.UdInnerLevel, 1, 0);
            this.PanelTessLevel.Controls.Add(this.LabelOuterLevel, 0, 1);
            this.PanelTessLevel.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelTessLevel.Location = new System.Drawing.Point(0, 78);
            this.PanelTessLevel.Name = "PanelTessLevel";
            this.PanelTessLevel.RowCount = 2;
            this.PanelTessLevel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PanelTessLevel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PanelTessLevel.Size = new System.Drawing.Size(215, 50);
            this.PanelTessLevel.TabIndex = 3;
            // 
            // UdOuterLevel
            // 
            this.UdOuterLevel.Dock = System.Windows.Forms.DockStyle.Left;
            this.UdOuterLevel.Location = new System.Drawing.Point(75, 28);
            this.UdOuterLevel.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.UdOuterLevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UdOuterLevel.Name = "UdOuterLevel";
            this.UdOuterLevel.Size = new System.Drawing.Size(87, 19);
            this.UdOuterLevel.TabIndex = 3;
            this.UdOuterLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UdOuterLevel.ValueChanged += new System.EventHandler(this.UdOuterLevel_ValueChanged);
            // 
            // LabelInnerLevel
            // 
            this.LabelInnerLevel.AutoSize = true;
            this.LabelInnerLevel.Dock = System.Windows.Forms.DockStyle.Left;
            this.LabelInnerLevel.Location = new System.Drawing.Point(3, 0);
            this.LabelInnerLevel.Name = "LabelInnerLevel";
            this.LabelInnerLevel.Size = new System.Drawing.Size(63, 25);
            this.LabelInnerLevel.TabIndex = 0;
            this.LabelInnerLevel.Text = "InnerLevel :";
            this.LabelInnerLevel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UdInnerLevel
            // 
            this.UdInnerLevel.Dock = System.Windows.Forms.DockStyle.Left;
            this.UdInnerLevel.Location = new System.Drawing.Point(75, 3);
            this.UdInnerLevel.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.UdInnerLevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UdInnerLevel.Name = "UdInnerLevel";
            this.UdInnerLevel.Size = new System.Drawing.Size(87, 19);
            this.UdInnerLevel.TabIndex = 1;
            this.UdInnerLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UdInnerLevel.ValueChanged += new System.EventHandler(this.UdInnerLevel_ValueChanged);
            // 
            // LabelOuterLevel
            // 
            this.LabelOuterLevel.AutoSize = true;
            this.LabelOuterLevel.Dock = System.Windows.Forms.DockStyle.Left;
            this.LabelOuterLevel.Location = new System.Drawing.Point(3, 25);
            this.LabelOuterLevel.Name = "LabelOuterLevel";
            this.LabelOuterLevel.Size = new System.Drawing.Size(66, 25);
            this.LabelOuterLevel.TabIndex = 2;
            this.LabelOuterLevel.Text = "OuterLevel :";
            this.LabelOuterLevel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PanelHeight
            // 
            this.PanelHeight.ColumnCount = 2;
            this.PanelHeight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.PanelHeight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PanelHeight.Controls.Add(this.UdHeightScale, 1, 0);
            this.PanelHeight.Controls.Add(this.LabelHeightScale, 0, 0);
            this.PanelHeight.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelHeight.Location = new System.Drawing.Point(0, 128);
            this.PanelHeight.Name = "PanelHeight";
            this.PanelHeight.RowCount = 2;
            this.PanelHeight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PanelHeight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PanelHeight.Size = new System.Drawing.Size(215, 50);
            this.PanelHeight.TabIndex = 4;
            // 
            // UdHeightScale
            // 
            this.UdHeightScale.DecimalPlaces = 3;
            this.UdHeightScale.Dock = System.Windows.Forms.DockStyle.Left;
            this.UdHeightScale.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.UdHeightScale.Location = new System.Drawing.Point(81, 3);
            this.UdHeightScale.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.UdHeightScale.Name = "UdHeightScale";
            this.UdHeightScale.Size = new System.Drawing.Size(87, 19);
            this.UdHeightScale.TabIndex = 2;
            this.UdHeightScale.ValueChanged += new System.EventHandler(this.UdHeightScale_ValueChanged);
            // 
            // LabelHeightScale
            // 
            this.LabelHeightScale.AutoSize = true;
            this.LabelHeightScale.Dock = System.Windows.Forms.DockStyle.Left;
            this.LabelHeightScale.Location = new System.Drawing.Point(3, 0);
            this.LabelHeightScale.Name = "LabelHeightScale";
            this.LabelHeightScale.Size = new System.Drawing.Size(72, 25);
            this.LabelHeightScale.TabIndex = 1;
            this.LabelHeightScale.Text = "HeightScale :";
            this.LabelHeightScale.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PanelRenderMode
            // 
            this.PanelRenderMode.Controls.Add(this.RadioRenderOffsetMode);
            this.PanelRenderMode.Controls.Add(this.RadioRenderNormalMode);
            this.PanelRenderMode.Controls.Add(this.RadioRenderSurfaceMode);
            this.PanelRenderMode.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelRenderMode.Location = new System.Drawing.Point(0, 34);
            this.PanelRenderMode.Name = "PanelRenderMode";
            this.PanelRenderMode.Padding = new System.Windows.Forms.Padding(3);
            this.PanelRenderMode.Size = new System.Drawing.Size(215, 22);
            this.PanelRenderMode.TabIndex = 5;
            // 
            // RadioRenderOffsetMode
            // 
            this.RadioRenderOffsetMode.AutoSize = true;
            this.RadioRenderOffsetMode.Dock = System.Windows.Forms.DockStyle.Left;
            this.RadioRenderOffsetMode.Location = new System.Drawing.Point(124, 3);
            this.RadioRenderOffsetMode.Name = "RadioRenderOffsetMode";
            this.RadioRenderOffsetMode.Size = new System.Drawing.Size(55, 16);
            this.RadioRenderOffsetMode.TabIndex = 2;
            this.RadioRenderOffsetMode.Text = "Offset";
            this.RadioRenderOffsetMode.UseVisualStyleBackColor = true;
            this.RadioRenderOffsetMode.CheckedChanged += new System.EventHandler(this.RadioRenderMode_CheckedChanged);
            // 
            // RadioRenderNormalMode
            // 
            this.RadioRenderNormalMode.AutoSize = true;
            this.RadioRenderNormalMode.Dock = System.Windows.Forms.DockStyle.Left;
            this.RadioRenderNormalMode.Location = new System.Drawing.Point(65, 3);
            this.RadioRenderNormalMode.Name = "RadioRenderNormalMode";
            this.RadioRenderNormalMode.Size = new System.Drawing.Size(59, 16);
            this.RadioRenderNormalMode.TabIndex = 1;
            this.RadioRenderNormalMode.Text = "Normal";
            this.RadioRenderNormalMode.UseVisualStyleBackColor = true;
            this.RadioRenderNormalMode.CheckedChanged += new System.EventHandler(this.RadioRenderMode_CheckedChanged);
            // 
            // RadioRenderSurfaceMode
            // 
            this.RadioRenderSurfaceMode.AutoSize = true;
            this.RadioRenderSurfaceMode.Checked = true;
            this.RadioRenderSurfaceMode.Dock = System.Windows.Forms.DockStyle.Left;
            this.RadioRenderSurfaceMode.Location = new System.Drawing.Point(3, 3);
            this.RadioRenderSurfaceMode.Name = "RadioRenderSurfaceMode";
            this.RadioRenderSurfaceMode.Size = new System.Drawing.Size(62, 16);
            this.RadioRenderSurfaceMode.TabIndex = 0;
            this.RadioRenderSurfaceMode.TabStop = true;
            this.RadioRenderSurfaceMode.Text = "Surface";
            this.RadioRenderSurfaceMode.UseVisualStyleBackColor = true;
            this.RadioRenderSurfaceMode.CheckedChanged += new System.EventHandler(this.RadioRenderMode_CheckedChanged);
            // 
            // SnowCoverVisibilityConfigurationView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PanelHeight);
            this.Controls.Add(this.PanelTessLevel);
            this.Controls.Add(this.PanelPolygonMode);
            this.Controls.Add(this.PanelRenderMode);
            this.Controls.Add(this.PanelChannelVisibility);
            this.Controls.Add(this.LabelChannelVisibility);
            this.Name = "SnowCoverVisibilityConfigurationView";
            this.Size = new System.Drawing.Size(215, 295);
            this.PanelChannelVisibility.ResumeLayout(false);
            this.PanelChannelVisibility.PerformLayout();
            this.PanelPolygonMode.ResumeLayout(false);
            this.PanelPolygonMode.PerformLayout();
            this.PanelTessLevel.ResumeLayout(false);
            this.PanelTessLevel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UdOuterLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UdInnerLevel)).EndInit();
            this.PanelHeight.ResumeLayout(false);
            this.PanelHeight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UdHeightScale)).EndInit();
            this.PanelRenderMode.ResumeLayout(false);
            this.PanelRenderMode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelChannelVisibility;
        private System.Windows.Forms.Panel PanelChannelVisibility;
        private System.Windows.Forms.CheckBox CheckChannelVisibilityB;
        private System.Windows.Forms.CheckBox CheckChannelVisibilityG;
        private System.Windows.Forms.CheckBox CheckChannelVisibilityR;
        private System.Windows.Forms.Panel PanelPolygonMode;
        private System.Windows.Forms.RadioButton RadioRenderLineMode;
        private System.Windows.Forms.RadioButton RadioRenderFaceMode;
        private System.Windows.Forms.TableLayoutPanel PanelTessLevel;
        private System.Windows.Forms.NumericUpDown UdOuterLevel;
        private System.Windows.Forms.Label LabelInnerLevel;
        private System.Windows.Forms.NumericUpDown UdInnerLevel;
        private System.Windows.Forms.Label LabelOuterLevel;
        private System.Windows.Forms.TableLayoutPanel PanelHeight;
        private System.Windows.Forms.NumericUpDown UdHeightScale;
        private System.Windows.Forms.Label LabelHeightScale;
        private System.Windows.Forms.Panel PanelRenderMode;
        private System.Windows.Forms.RadioButton RadioRenderOffsetMode;
        private System.Windows.Forms.RadioButton RadioRenderNormalMode;
        private System.Windows.Forms.RadioButton RadioRenderSurfaceMode;
    }
}
