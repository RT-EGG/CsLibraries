
namespace GLTestVisualizer.TestView.Lighting
{
    partial class Ctrl_LightingTestView
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
            this.GLControl = new RtCs.OpenGL.WinForms.GLControl();
            this.UpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // GLControl
            // 
            this.GLControl.BackColor = System.Drawing.Color.Black;
            this.GLControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GLControl.Location = new System.Drawing.Point(0, 0);
            this.GLControl.Name = "GLControl";
            this.GLControl.Size = new System.Drawing.Size(574, 440);
            this.GLControl.TabIndex = 0;
            this.GLControl.VSync = false;
            this.GLControl.OnRenderScene += new System.EventHandler(this.GLControl_OnRenderScene);
            // 
            // UpdateTimer
            // 
            this.UpdateTimer.Enabled = true;
            this.UpdateTimer.Interval = 16;
            this.UpdateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
            // 
            // Ctrl_LightingTestView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.GLControl);
            this.Name = "Ctrl_LightingTestView";
            this.Size = new System.Drawing.Size(574, 440);
            this.ResumeLayout(false);

        }

        #endregion

        private RtCs.OpenGL.WinForms.GLControl GLControl;
        private System.Windows.Forms.Timer UpdateTimer;
    }
}
