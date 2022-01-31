
namespace Snow
{
    partial class FormMain
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
            this.SimulationTimer = new System.Windows.Forms.Timer(this.components);
            this.PanelOptions = new System.Windows.Forms.Panel();
            this.SnowCoverVisibilityConfigurationView = new Snow.View.SnowCoverVisibilityConfigurationView();
            this.SimulationView = new Snow.View.SimulationView();
            this.glControl = new RtCs.OpenGL.WinForms.GLControl();
            this.PanelOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // SimulationTimer
            // 
            this.SimulationTimer.Enabled = true;
            this.SimulationTimer.Interval = 16;
            this.SimulationTimer.Tick += new System.EventHandler(this.SimulationTimer_Tick);
            // 
            // PanelOptions
            // 
            this.PanelOptions.Controls.Add(this.SnowCoverVisibilityConfigurationView);
            this.PanelOptions.Controls.Add(this.SimulationView);
            this.PanelOptions.Dock = System.Windows.Forms.DockStyle.Right;
            this.PanelOptions.Location = new System.Drawing.Point(594, 0);
            this.PanelOptions.Name = "PanelOptions";
            this.PanelOptions.Size = new System.Drawing.Size(206, 450);
            this.PanelOptions.TabIndex = 1;
            // 
            // SnowCoverVisibilityConfigurationView
            // 
            this.SnowCoverVisibilityConfigurationView.Dock = System.Windows.Forms.DockStyle.Top;
            this.SnowCoverVisibilityConfigurationView.Location = new System.Drawing.Point(0, 81);
            this.SnowCoverVisibilityConfigurationView.Model = null;
            this.SnowCoverVisibilityConfigurationView.Name = "SnowCoverVisibilityConfigurationView";
            this.SnowCoverVisibilityConfigurationView.Size = new System.Drawing.Size(206, 172);
            this.SnowCoverVisibilityConfigurationView.TabIndex = 0;
            // 
            // SimulationView
            // 
            this.SimulationView.Dock = System.Windows.Forms.DockStyle.Top;
            this.SimulationView.Location = new System.Drawing.Point(0, 0);
            this.SimulationView.Model = null;
            this.SimulationView.Name = "SimulationView";
            this.SimulationView.Size = new System.Drawing.Size(206, 81);
            this.SimulationView.TabIndex = 1;
            // 
            // glControl
            // 
            this.glControl.BackColor = System.Drawing.Color.Black;
            this.glControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glControl.Location = new System.Drawing.Point(0, 0);
            this.glControl.Name = "glControl";
            this.glControl.Size = new System.Drawing.Size(594, 450);
            this.glControl.TabIndex = 0;
            this.glControl.VSync = false;
            this.glControl.RenderScene += new System.EventHandler(this.glControl_RenderScene);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.glControl);
            this.Controls.Add(this.PanelOptions);
            this.Name = "FormMain";
            this.Text = "Snow";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.PanelOptions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private RtCs.OpenGL.WinForms.GLControl glControl;
        private System.Windows.Forms.Timer SimulationTimer;
        private System.Windows.Forms.Panel PanelOptions;
        private View.SnowCoverVisibilityConfigurationView SnowCoverVisibilityConfigurationView;
        private View.SimulationView SimulationView;
    }
}

