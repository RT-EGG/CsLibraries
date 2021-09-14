
namespace GLTestVisualizer.TestView.AlphaBlending
{
    partial class Ctrl_AlphaBlendingTestView
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
            this.glView = new RtCs.OpenGL.GLControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // glView
            // 
            this.glView.BackColor = System.Drawing.Color.Black;
            this.glView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glView.Location = new System.Drawing.Point(0, 0);
            this.glView.Name = "glView";
            this.glView.Size = new System.Drawing.Size(349, 263);
            this.glView.TabIndex = 0;
            this.glView.VSync = false;
            this.glView.OnRenderScene += new RtCs.OpenGL.GLRenderSceneEventHandler(this.glView_OnRenderScene);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 16;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Ctrl_AlphaBlendingTestView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.glView);
            this.Name = "Ctrl_AlphaBlendingTestView";
            this.Size = new System.Drawing.Size(349, 263);
            this.ResumeLayout(false);

        }

        #endregion

        private RtCs.OpenGL.GLControl glView;
        private System.Windows.Forms.Timer timer1;
    }
}
