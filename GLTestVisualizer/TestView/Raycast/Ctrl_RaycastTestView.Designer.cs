
namespace GLTestVisualizer.TestView.Raycast
{
    partial class Ctrl_RaycastTestView
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
            this.components = new System.ComponentModel.Container();
            this.GLFPSView = new RtCs.OpenGL.WinForms.GLControl();
            this.PanelParameters = new System.Windows.Forms.TableLayoutPanel();
            this.LabelControlDescription = new System.Windows.Forms.Label();
            this.LabelRayLength = new System.Windows.Forms.Label();
            this.UdRayLength = new System.Windows.Forms.NumericUpDown();
            this.CheckShowOctreeGrid = new System.Windows.Forms.CheckBox();
            this.PanelView = new System.Windows.Forms.TableLayoutPanel();
            this.GLTPSView = new RtCs.OpenGL.WinForms.GLControl();
            this.PanelFPSView = new System.Windows.Forms.Panel();
            this.ButtonResetFPSCamera = new System.Windows.Forms.Button();
            this.InvalidateTimer = new System.Windows.Forms.Timer(this.components);
            this.PanelParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UdRayLength)).BeginInit();
            this.PanelView.SuspendLayout();
            this.PanelFPSView.SuspendLayout();
            this.SuspendLayout();
            // 
            // GLFPSView
            // 
            this.GLFPSView.BackColor = System.Drawing.Color.Black;
            this.GLFPSView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GLFPSView.Location = new System.Drawing.Point(0, 0);
            this.GLFPSView.Name = "GLFPSView";
            this.GLFPSView.Size = new System.Drawing.Size(252, 270);
            this.GLFPSView.TabIndex = 0;
            this.GLFPSView.VSync = false;
            this.GLFPSView.RenderScene += new System.EventHandler(this.GLFPSView_OnRenderScene);
            // 
            // PanelParameters
            // 
            this.PanelParameters.ColumnCount = 3;
            this.PanelParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.PanelParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PanelParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 138F));
            this.PanelParameters.Controls.Add(this.LabelControlDescription, 0, 0);
            this.PanelParameters.Controls.Add(this.LabelRayLength, 1, 0);
            this.PanelParameters.Controls.Add(this.UdRayLength, 2, 0);
            this.PanelParameters.Controls.Add(this.CheckShowOctreeGrid, 1, 1);
            this.PanelParameters.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelParameters.Location = new System.Drawing.Point(0, 276);
            this.PanelParameters.Name = "PanelParameters";
            this.PanelParameters.RowCount = 3;
            this.PanelParameters.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PanelParameters.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PanelParameters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PanelParameters.Size = new System.Drawing.Size(516, 104);
            this.PanelParameters.TabIndex = 1;
            // 
            // LabelControlDescription
            // 
            this.LabelControlDescription.AutoSize = true;
            this.LabelControlDescription.BackColor = System.Drawing.Color.Transparent;
            this.LabelControlDescription.Dock = System.Windows.Forms.DockStyle.Left;
            this.LabelControlDescription.Location = new System.Drawing.Point(3, 0);
            this.LabelControlDescription.Name = "LabelControlDescription";
            this.PanelParameters.SetRowSpan(this.LabelControlDescription, 3);
            this.LabelControlDescription.Size = new System.Drawing.Size(78, 104);
            this.LabelControlDescription.TabIndex = 4;
            this.LabelControlDescription.Text = "W: front\r\nS: Back\r\nA: Left\r\nD: Right\r\nQ: Down\r\nE: Up\r\nShift: Boost\r\nMouse: Rotate" +
    "\r\n";
            // 
            // LabelRayLength
            // 
            this.LabelRayLength.AutoSize = true;
            this.LabelRayLength.Dock = System.Windows.Forms.DockStyle.Right;
            this.LabelRayLength.Location = new System.Drawing.Point(306, 0);
            this.LabelRayLength.Name = "LabelRayLength";
            this.LabelRayLength.Size = new System.Drawing.Size(69, 25);
            this.LabelRayLength.TabIndex = 5;
            this.LabelRayLength.Text = "Ray Length :";
            this.LabelRayLength.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UdRayLength
            // 
            this.UdRayLength.DecimalPlaces = 3;
            this.UdRayLength.Location = new System.Drawing.Point(381, 3);
            this.UdRayLength.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.UdRayLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.UdRayLength.Name = "UdRayLength";
            this.UdRayLength.Size = new System.Drawing.Size(120, 19);
            this.UdRayLength.TabIndex = 6;
            this.UdRayLength.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // CheckShowOctreeGrid
            // 
            this.CheckShowOctreeGrid.AutoSize = true;
            this.PanelParameters.SetColumnSpan(this.CheckShowOctreeGrid, 2);
            this.CheckShowOctreeGrid.Dock = System.Windows.Forms.DockStyle.Right;
            this.CheckShowOctreeGrid.Location = new System.Drawing.Point(426, 28);
            this.CheckShowOctreeGrid.Name = "CheckShowOctreeGrid";
            this.CheckShowOctreeGrid.Size = new System.Drawing.Size(87, 16);
            this.CheckShowOctreeGrid.TabIndex = 7;
            this.CheckShowOctreeGrid.Text = "Show octree";
            this.CheckShowOctreeGrid.UseVisualStyleBackColor = true;
            // 
            // PanelView
            // 
            this.PanelView.ColumnCount = 2;
            this.PanelView.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PanelView.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PanelView.Controls.Add(this.GLTPSView, 0, 0);
            this.PanelView.Controls.Add(this.PanelFPSView, 0, 0);
            this.PanelView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelView.Location = new System.Drawing.Point(0, 0);
            this.PanelView.Name = "PanelView";
            this.PanelView.RowCount = 1;
            this.PanelView.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PanelView.Size = new System.Drawing.Size(516, 276);
            this.PanelView.TabIndex = 2;
            // 
            // GLTPSView
            // 
            this.GLTPSView.BackColor = System.Drawing.Color.Black;
            this.GLTPSView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GLTPSView.Location = new System.Drawing.Point(261, 3);
            this.GLTPSView.Name = "GLTPSView";
            this.GLTPSView.Size = new System.Drawing.Size(252, 270);
            this.GLTPSView.TabIndex = 1;
            this.GLTPSView.VSync = false;
            this.GLTPSView.RenderScene += new System.EventHandler(this.GLTPSView_OnRenderScene);
            // 
            // PanelFPSView
            // 
            this.PanelFPSView.Controls.Add(this.ButtonResetFPSCamera);
            this.PanelFPSView.Controls.Add(this.GLFPSView);
            this.PanelFPSView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelFPSView.Location = new System.Drawing.Point(3, 3);
            this.PanelFPSView.Name = "PanelFPSView";
            this.PanelFPSView.Size = new System.Drawing.Size(252, 270);
            this.PanelFPSView.TabIndex = 3;
            // 
            // ButtonResetFPSCamera
            // 
            this.ButtonResetFPSCamera.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonResetFPSCamera.Location = new System.Drawing.Point(164, 3);
            this.ButtonResetFPSCamera.Name = "ButtonResetFPSCamera";
            this.ButtonResetFPSCamera.Size = new System.Drawing.Size(85, 18);
            this.ButtonResetFPSCamera.TabIndex = 2;
            this.ButtonResetFPSCamera.Text = "Reset";
            this.ButtonResetFPSCamera.UseVisualStyleBackColor = true;
            this.ButtonResetFPSCamera.Click += new System.EventHandler(this.ButtonResetFPSCamera_Click);
            // 
            // InvalidateTimer
            // 
            this.InvalidateTimer.Interval = 16;
            this.InvalidateTimer.Tick += new System.EventHandler(this.InvalidateTimer_Tick);
            // 
            // Ctrl_RaycastTestView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.PanelView);
            this.Controls.Add(this.PanelParameters);
            this.Name = "Ctrl_RaycastTestView";
            this.Size = new System.Drawing.Size(516, 380);
            this.PanelParameters.ResumeLayout(false);
            this.PanelParameters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UdRayLength)).EndInit();
            this.PanelView.ResumeLayout(false);
            this.PanelFPSView.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private RtCs.OpenGL.WinForms.GLControl GLFPSView;
        private System.Windows.Forms.TableLayoutPanel PanelParameters;
        private System.Windows.Forms.TableLayoutPanel PanelView;
        private RtCs.OpenGL.WinForms.GLControl GLTPSView;
        private System.Windows.Forms.Timer InvalidateTimer;
        private System.Windows.Forms.Panel PanelFPSView;
        private System.Windows.Forms.Button ButtonResetFPSCamera;
        private System.Windows.Forms.Label LabelControlDescription;
        private System.Windows.Forms.Label LabelRayLength;
        private System.Windows.Forms.NumericUpDown UdRayLength;
        private System.Windows.Forms.CheckBox CheckShowOctreeGrid;
    }
}
