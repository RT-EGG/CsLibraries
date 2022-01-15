
namespace GLTestVisualizer.TestView.SphereMesh
{
    partial class Ctrl_SphereMeshTestView
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
            this.PanelEditor = new System.Windows.Forms.TableLayoutPanel();
            this.PanelUVSphereEditor = new System.Windows.Forms.TableLayoutPanel();
            this.UdUVStacks = new System.Windows.Forms.NumericUpDown();
            this.LabelUVStacks = new System.Windows.Forms.Label();
            this.LabelUVSlices = new System.Windows.Forms.Label();
            this.UdUVSlices = new System.Windows.Forms.NumericUpDown();
            this.LabelUVSphere = new System.Windows.Forms.Label();
            this.PanelICOSphereEditor = new System.Windows.Forms.TableLayoutPanel();
            this.LabelICOSubdivision = new System.Windows.Forms.Label();
            this.UdICOSubdivision = new System.Windows.Forms.NumericUpDown();
            this.LabelICOSphere = new System.Windows.Forms.Label();
            this.LabelRoundedCubeSphere = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.UdRoundedCubeSubdivision = new System.Windows.Forms.NumericUpDown();
            this.LabelRoundedCubeSubdivision = new System.Windows.Forms.Label();
            this.RenderTimer = new System.Windows.Forms.Timer(this.components);
            this.GLViewer = new RtCs.OpenGL.WinForms.GLControl();
            this.PanelEditor.SuspendLayout();
            this.PanelUVSphereEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UdUVStacks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UdUVSlices)).BeginInit();
            this.PanelICOSphereEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UdICOSubdivision)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UdRoundedCubeSubdivision)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelEditor
            // 
            this.PanelEditor.ColumnCount = 3;
            this.PanelEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.PanelEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.PanelEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.PanelEditor.Controls.Add(this.PanelUVSphereEditor, 0, 1);
            this.PanelEditor.Controls.Add(this.LabelUVSphere, 0, 0);
            this.PanelEditor.Controls.Add(this.PanelICOSphereEditor, 1, 1);
            this.PanelEditor.Controls.Add(this.LabelICOSphere, 1, 0);
            this.PanelEditor.Controls.Add(this.LabelRoundedCubeSphere, 2, 0);
            this.PanelEditor.Controls.Add(this.tableLayoutPanel1, 2, 1);
            this.PanelEditor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelEditor.Location = new System.Drawing.Point(0, 552);
            this.PanelEditor.Margin = new System.Windows.Forms.Padding(4);
            this.PanelEditor.Name = "PanelEditor";
            this.PanelEditor.RowCount = 2;
            this.PanelEditor.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PanelEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PanelEditor.Size = new System.Drawing.Size(594, 114);
            this.PanelEditor.TabIndex = 1;
            // 
            // PanelUVSphereEditor
            // 
            this.PanelUVSphereEditor.ColumnCount = 2;
            this.PanelUVSphereEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.PanelUVSphereEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PanelUVSphereEditor.Controls.Add(this.UdUVStacks, 1, 1);
            this.PanelUVSphereEditor.Controls.Add(this.LabelUVStacks, 0, 1);
            this.PanelUVSphereEditor.Controls.Add(this.LabelUVSlices, 0, 0);
            this.PanelUVSphereEditor.Controls.Add(this.UdUVSlices, 1, 0);
            this.PanelUVSphereEditor.Location = new System.Drawing.Point(4, 19);
            this.PanelUVSphereEditor.Margin = new System.Windows.Forms.Padding(4);
            this.PanelUVSphereEditor.Name = "PanelUVSphereEditor";
            this.PanelUVSphereEditor.RowCount = 3;
            this.PanelUVSphereEditor.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PanelUVSphereEditor.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PanelUVSphereEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PanelUVSphereEditor.Size = new System.Drawing.Size(189, 78);
            this.PanelUVSphereEditor.TabIndex = 3;
            // 
            // UdUVStacks
            // 
            this.UdUVStacks.Dock = System.Windows.Forms.DockStyle.Left;
            this.UdUVStacks.Location = new System.Drawing.Point(58, 35);
            this.UdUVStacks.Margin = new System.Windows.Forms.Padding(4);
            this.UdUVStacks.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.UdUVStacks.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.UdUVStacks.Name = "UdUVStacks";
            this.UdUVStacks.Size = new System.Drawing.Size(122, 23);
            this.UdUVStacks.TabIndex = 3;
            this.UdUVStacks.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.UdUVStacks.ValueChanged += new System.EventHandler(this.UdUVSubdivision_ValueChanged);
            // 
            // LabelUVStacks
            // 
            this.LabelUVStacks.AutoSize = true;
            this.LabelUVStacks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelUVStacks.Location = new System.Drawing.Point(4, 31);
            this.LabelUVStacks.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelUVStacks.Name = "LabelUVStacks";
            this.LabelUVStacks.Size = new System.Drawing.Size(46, 31);
            this.LabelUVStacks.TabIndex = 2;
            this.LabelUVStacks.Text = "Stacks :";
            this.LabelUVStacks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LabelUVSlices
            // 
            this.LabelUVSlices.AutoSize = true;
            this.LabelUVSlices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelUVSlices.Location = new System.Drawing.Point(4, 0);
            this.LabelUVSlices.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelUVSlices.Name = "LabelUVSlices";
            this.LabelUVSlices.Size = new System.Drawing.Size(46, 31);
            this.LabelUVSlices.TabIndex = 0;
            this.LabelUVSlices.Text = "Slices :";
            this.LabelUVSlices.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UdUVSlices
            // 
            this.UdUVSlices.Dock = System.Windows.Forms.DockStyle.Left;
            this.UdUVSlices.Location = new System.Drawing.Point(58, 4);
            this.UdUVSlices.Margin = new System.Windows.Forms.Padding(4);
            this.UdUVSlices.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.UdUVSlices.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.UdUVSlices.Name = "UdUVSlices";
            this.UdUVSlices.Size = new System.Drawing.Size(122, 23);
            this.UdUVSlices.TabIndex = 1;
            this.UdUVSlices.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.UdUVSlices.ValueChanged += new System.EventHandler(this.UdUVSubdivision_ValueChanged);
            // 
            // LabelUVSphere
            // 
            this.LabelUVSphere.AutoSize = true;
            this.LabelUVSphere.Location = new System.Drawing.Point(4, 0);
            this.LabelUVSphere.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelUVSphere.Name = "LabelUVSphere";
            this.LabelUVSphere.Size = new System.Drawing.Size(22, 15);
            this.LabelUVSphere.TabIndex = 2;
            this.LabelUVSphere.Text = "UV";
            // 
            // PanelICOSphereEditor
            // 
            this.PanelICOSphereEditor.ColumnCount = 2;
            this.PanelICOSphereEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.PanelICOSphereEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PanelICOSphereEditor.Controls.Add(this.LabelICOSubdivision, 0, 0);
            this.PanelICOSphereEditor.Controls.Add(this.UdICOSubdivision, 1, 0);
            this.PanelICOSphereEditor.Location = new System.Drawing.Point(201, 19);
            this.PanelICOSphereEditor.Margin = new System.Windows.Forms.Padding(4);
            this.PanelICOSphereEditor.Name = "PanelICOSphereEditor";
            this.PanelICOSphereEditor.RowCount = 2;
            this.PanelICOSphereEditor.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PanelICOSphereEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PanelICOSphereEditor.Size = new System.Drawing.Size(189, 60);
            this.PanelICOSphereEditor.TabIndex = 0;
            // 
            // LabelICOSubdivision
            // 
            this.LabelICOSubdivision.AutoSize = true;
            this.LabelICOSubdivision.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelICOSubdivision.Location = new System.Drawing.Point(4, 0);
            this.LabelICOSubdivision.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelICOSubdivision.Name = "LabelICOSubdivision";
            this.LabelICOSubdivision.Size = new System.Drawing.Size(74, 31);
            this.LabelICOSubdivision.TabIndex = 0;
            this.LabelICOSubdivision.Text = "Subdivision :";
            this.LabelICOSubdivision.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UdICOSubdivision
            // 
            this.UdICOSubdivision.Dock = System.Windows.Forms.DockStyle.Left;
            this.UdICOSubdivision.Location = new System.Drawing.Point(86, 4);
            this.UdICOSubdivision.Margin = new System.Windows.Forms.Padding(4);
            this.UdICOSubdivision.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.UdICOSubdivision.Name = "UdICOSubdivision";
            this.UdICOSubdivision.Size = new System.Drawing.Size(90, 23);
            this.UdICOSubdivision.TabIndex = 1;
            this.UdICOSubdivision.ValueChanged += new System.EventHandler(this.UdICOSubdivision_ValueChanged);
            // 
            // LabelICOSphere
            // 
            this.LabelICOSphere.AutoSize = true;
            this.LabelICOSphere.Location = new System.Drawing.Point(201, 0);
            this.LabelICOSphere.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelICOSphere.Name = "LabelICOSphere";
            this.LabelICOSphere.Size = new System.Drawing.Size(26, 15);
            this.LabelICOSphere.TabIndex = 1;
            this.LabelICOSphere.Text = "ICO";
            // 
            // LabelRoundedCubeSphere
            // 
            this.LabelRoundedCubeSphere.AutoSize = true;
            this.LabelRoundedCubeSphere.Location = new System.Drawing.Point(398, 0);
            this.LabelRoundedCubeSphere.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelRoundedCubeSphere.Name = "LabelRoundedCubeSphere";
            this.LabelRoundedCubeSphere.Size = new System.Drawing.Size(85, 15);
            this.LabelRoundedCubeSphere.TabIndex = 4;
            this.LabelRoundedCubeSphere.Text = "Rounded Cube";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.UdRoundedCubeSubdivision, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.LabelRoundedCubeSubdivision, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(398, 19);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(192, 62);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // UdRoundedCubeSubdivision
            // 
            this.UdRoundedCubeSubdivision.Dock = System.Windows.Forms.DockStyle.Left;
            this.UdRoundedCubeSubdivision.Location = new System.Drawing.Point(86, 4);
            this.UdRoundedCubeSubdivision.Margin = new System.Windows.Forms.Padding(4);
            this.UdRoundedCubeSubdivision.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.UdRoundedCubeSubdivision.Name = "UdRoundedCubeSubdivision";
            this.UdRoundedCubeSubdivision.Size = new System.Drawing.Size(92, 23);
            this.UdRoundedCubeSubdivision.TabIndex = 2;
            this.UdRoundedCubeSubdivision.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.UdRoundedCubeSubdivision.ValueChanged += new System.EventHandler(this.UdRoundedCubeSubdivision_ValueChanged);
            // 
            // LabelRoundedCubeSubdivision
            // 
            this.LabelRoundedCubeSubdivision.AutoSize = true;
            this.LabelRoundedCubeSubdivision.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelRoundedCubeSubdivision.Location = new System.Drawing.Point(4, 0);
            this.LabelRoundedCubeSubdivision.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelRoundedCubeSubdivision.Name = "LabelRoundedCubeSubdivision";
            this.LabelRoundedCubeSubdivision.Size = new System.Drawing.Size(74, 31);
            this.LabelRoundedCubeSubdivision.TabIndex = 1;
            this.LabelRoundedCubeSubdivision.Text = "Subdivision :";
            this.LabelRoundedCubeSubdivision.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // RenderTimer
            // 
            this.RenderTimer.Enabled = true;
            this.RenderTimer.Interval = 10;
            this.RenderTimer.Tick += new System.EventHandler(this.RenderTimer_Tick);
            // 
            // GLViewer
            // 
            this.GLViewer.API = OpenTK.Windowing.Common.ContextAPI.OpenGL;
            this.GLViewer.APIVersion = new System.Version(3, 3, 0, 0);
            this.GLViewer.BackColor = System.Drawing.Color.Black;
            this.GLViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GLViewer.Flags = OpenTK.Windowing.Common.ContextFlags.Default;
            this.GLViewer.IsEventDriven = true;
            this.GLViewer.Location = new System.Drawing.Point(0, 0);
            this.GLViewer.Margin = new System.Windows.Forms.Padding(4);
            this.GLViewer.Name = "GLViewer";
            this.GLViewer.Profile = OpenTK.Windowing.Common.ContextProfile.Core;
            this.GLViewer.Size = new System.Drawing.Size(594, 552);
            this.GLViewer.TabIndex = 2;
            this.GLViewer.OnRenderScene += new System.EventHandler(this.GLViewer_OnRenderScene);
            // 
            // Ctrl_SphereMeshTestView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.Controls.Add(this.GLViewer);
            this.Controls.Add(this.PanelEditor);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Ctrl_SphereMeshTestView";
            this.Size = new System.Drawing.Size(594, 666);
            this.PanelEditor.ResumeLayout(false);
            this.PanelEditor.PerformLayout();
            this.PanelUVSphereEditor.ResumeLayout(false);
            this.PanelUVSphereEditor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UdUVStacks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UdUVSlices)).EndInit();
            this.PanelICOSphereEditor.ResumeLayout(false);
            this.PanelICOSphereEditor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UdICOSubdivision)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UdRoundedCubeSubdivision)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel PanelEditor;
        private System.Windows.Forms.TableLayoutPanel PanelICOSphereEditor;
        private System.Windows.Forms.Label LabelICOSubdivision;
        private System.Windows.Forms.NumericUpDown UdICOSubdivision;
        private System.Windows.Forms.Label LabelICOSphere;
        private RtCs.OpenGL.WinForms.GLControl GLViewer;
        private System.Windows.Forms.Timer RenderTimer;
        private System.Windows.Forms.TableLayoutPanel PanelUVSphereEditor;
        private System.Windows.Forms.NumericUpDown UdUVStacks;
        private System.Windows.Forms.Label LabelUVStacks;
        private System.Windows.Forms.Label LabelUVSlices;
        private System.Windows.Forms.NumericUpDown UdUVSlices;
        private System.Windows.Forms.Label LabelUVSphere;
        private System.Windows.Forms.Label LabelRoundedCubeSphere;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.NumericUpDown UdRoundedCubeSubdivision;
        private System.Windows.Forms.Label LabelRoundedCubeSubdivision;
    }
}
