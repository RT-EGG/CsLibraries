
namespace GLTestVisualizer.TestView
{
    partial class Ctrl_ProjectionTestSceneView
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
            this.PanelBaseLayout = new System.Windows.Forms.TableLayoutPanel();
            this.PanelSceneView = new System.Windows.Forms.Panel();
            this.GL3rdPersonView = new GLTestVisualizer.TestView.Ctrl_GLViewer();
            this.SplitterSceneViewer = new System.Windows.Forms.Splitter();
            this.GL1stPersonView = new GLTestVisualizer.TestView.Ctrl_GLViewer();
            this.PanelBaseLayout.SuspendLayout();
            this.PanelSceneView.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelBaseLayout
            // 
            this.PanelBaseLayout.ColumnCount = 1;
            this.PanelBaseLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PanelBaseLayout.Controls.Add(this.PanelSceneView, 0, 0);
            this.PanelBaseLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelBaseLayout.Location = new System.Drawing.Point(0, 0);
            this.PanelBaseLayout.Name = "PanelBaseLayout";
            this.PanelBaseLayout.RowCount = 2;
            this.PanelBaseLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PanelBaseLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PanelBaseLayout.Size = new System.Drawing.Size(810, 503);
            this.PanelBaseLayout.TabIndex = 0;
            // 
            // PanelSceneView
            // 
            this.PanelSceneView.Controls.Add(this.GL3rdPersonView);
            this.PanelSceneView.Controls.Add(this.SplitterSceneViewer);
            this.PanelSceneView.Controls.Add(this.GL1stPersonView);
            this.PanelSceneView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelSceneView.Location = new System.Drawing.Point(3, 3);
            this.PanelSceneView.Name = "PanelSceneView";
            this.PanelSceneView.Size = new System.Drawing.Size(804, 245);
            this.PanelSceneView.TabIndex = 0;
            // 
            // GL3rdPersonView
            // 
            this.GL3rdPersonView.BackColor = System.Drawing.Color.Black;
            this.GL3rdPersonView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GL3rdPersonView.Location = new System.Drawing.Point(377, 0);
            this.GL3rdPersonView.Name = "GL3rdPersonView";
            this.GL3rdPersonView.Size = new System.Drawing.Size(427, 245);
            this.GL3rdPersonView.TabIndex = 2;
            this.GL3rdPersonView.VSync = false;
            // 
            // SplitterSceneViewer
            // 
            this.SplitterSceneViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SplitterSceneViewer.Location = new System.Drawing.Point(372, 0);
            this.SplitterSceneViewer.Name = "SplitterSceneViewer";
            this.SplitterSceneViewer.Size = new System.Drawing.Size(5, 245);
            this.SplitterSceneViewer.TabIndex = 1;
            this.SplitterSceneViewer.TabStop = false;
            // 
            // GL1stPersonView
            // 
            this.GL1stPersonView.BackColor = System.Drawing.Color.Black;
            this.GL1stPersonView.Dock = System.Windows.Forms.DockStyle.Left;
            this.GL1stPersonView.Location = new System.Drawing.Point(0, 0);
            this.GL1stPersonView.Name = "GL1stPersonView";
            this.GL1stPersonView.Size = new System.Drawing.Size(372, 245);
            this.GL1stPersonView.TabIndex = 0;
            this.GL1stPersonView.VSync = false;
            this.GL1stPersonView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GL1stPersonView_MouseDown);
            // 
            // Ctrl_ProjectionTestSceneView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.PanelBaseLayout);
            this.Name = "Ctrl_ProjectionTestSceneView";
            this.Size = new System.Drawing.Size(810, 503);
            this.PanelBaseLayout.ResumeLayout(false);
            this.PanelSceneView.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel PanelBaseLayout;
        private System.Windows.Forms.Panel PanelSceneView;
        private Ctrl_GLViewer GL1stPersonView;
        private Ctrl_GLViewer GL3rdPersonView;
        private System.Windows.Forms.Splitter SplitterSceneViewer;
    }
}
