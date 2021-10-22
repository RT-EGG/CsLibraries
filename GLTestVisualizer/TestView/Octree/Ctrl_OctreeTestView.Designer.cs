
namespace GLTestVisualizer.TestView.Octree
{
    partial class Ctrl_OctreeTestView
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
            this.glView = new RtCs.OpenGL.WinForms.GLControl();
            this.ValidationTimer = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.TrackBarTime = new System.Windows.Forms.TrackBar();
            this.CheckBoxAutoTime = new System.Windows.Forms.CheckBox();
            this.AutoTrackTimer = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarTime)).BeginInit();
            this.SuspendLayout();
            // 
            // glView
            // 
            this.glView.BackColor = System.Drawing.Color.Black;
            this.glView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glView.Location = new System.Drawing.Point(0, 0);
            this.glView.Name = "glView";
            this.glView.Size = new System.Drawing.Size(455, 390);
            this.glView.TabIndex = 0;
            this.glView.VSync = false;
            this.glView.OnRenderScene += new RtCs.OpenGL.WinForms.GLRenderSceneEventHandler(this.glView_OnRenderScene);
            // 
            // ValidationTimer
            // 
            this.ValidationTimer.Interval = 16;
            this.ValidationTimer.Tick += new System.EventHandler(this.ValidationTimer_Tick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.TrackBarTime, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.CheckBoxAutoTime, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 390);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(455, 29);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // TrackBarTime
            // 
            this.TrackBarTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TrackBarTime.LargeChange = 10;
            this.TrackBarTime.Location = new System.Drawing.Point(3, 3);
            this.TrackBarTime.Maximum = 1000;
            this.TrackBarTime.Name = "TrackBarTime";
            this.TrackBarTime.Size = new System.Drawing.Size(397, 23);
            this.TrackBarTime.TabIndex = 0;
            this.TrackBarTime.TickFrequency = 0;
            // 
            // CheckBoxAutoTime
            // 
            this.CheckBoxAutoTime.AutoSize = true;
            this.CheckBoxAutoTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CheckBoxAutoTime.Location = new System.Drawing.Point(406, 3);
            this.CheckBoxAutoTime.Name = "CheckBoxAutoTime";
            this.CheckBoxAutoTime.Size = new System.Drawing.Size(46, 23);
            this.CheckBoxAutoTime.TabIndex = 1;
            this.CheckBoxAutoTime.Text = "auto";
            this.CheckBoxAutoTime.UseVisualStyleBackColor = true;
            this.CheckBoxAutoTime.CheckedChanged += new System.EventHandler(this.CheckBoxAutoTime_CheckedChanged);
            // 
            // AutoTrackTimer
            // 
            this.AutoTrackTimer.Interval = 1;
            this.AutoTrackTimer.Tick += new System.EventHandler(this.AutoTrackTimer_Tick);
            // 
            // Ctrl_OctreeTestView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.glView);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Ctrl_OctreeTestView";
            this.Size = new System.Drawing.Size(455, 419);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarTime)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private RtCs.OpenGL.WinForms.GLControl glView;
        private System.Windows.Forms.Timer ValidationTimer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TrackBar TrackBarTime;
        private System.Windows.Forms.CheckBox CheckBoxAutoTime;
        private System.Windows.Forms.Timer AutoTrackTimer;
    }
}
