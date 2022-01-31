
namespace Snow.View
{
    partial class SimulationView
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
            this.ButtonInitializeSimulation = new System.Windows.Forms.Button();
            this.PanelSimulationScale = new System.Windows.Forms.TableLayoutPanel();
            this.LabelSimulationScale = new System.Windows.Forms.Label();
            this.TrackBarSimulationScale = new System.Windows.Forms.TrackBar();
            this.UdSimulationScale = new System.Windows.Forms.NumericUpDown();
            this.PanelSimulationScale.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarSimulationScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UdSimulationScale)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonInitializeSimulation
            // 
            this.ButtonInitializeSimulation.Dock = System.Windows.Forms.DockStyle.Top;
            this.ButtonInitializeSimulation.Location = new System.Drawing.Point(0, 0);
            this.ButtonInitializeSimulation.Name = "ButtonInitializeSimulation";
            this.ButtonInitializeSimulation.Size = new System.Drawing.Size(340, 23);
            this.ButtonInitializeSimulation.TabIndex = 0;
            this.ButtonInitializeSimulation.Text = "Initialize";
            this.ButtonInitializeSimulation.UseVisualStyleBackColor = true;
            this.ButtonInitializeSimulation.Click += new System.EventHandler(this.ButtonInitializeSimulation_Click);
            // 
            // PanelSimulationScale
            // 
            this.PanelSimulationScale.ColumnCount = 3;
            this.PanelSimulationScale.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.PanelSimulationScale.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PanelSimulationScale.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.PanelSimulationScale.Controls.Add(this.LabelSimulationScale, 0, 0);
            this.PanelSimulationScale.Controls.Add(this.TrackBarSimulationScale, 1, 0);
            this.PanelSimulationScale.Controls.Add(this.UdSimulationScale, 2, 0);
            this.PanelSimulationScale.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelSimulationScale.Location = new System.Drawing.Point(0, 23);
            this.PanelSimulationScale.Name = "PanelSimulationScale";
            this.PanelSimulationScale.Padding = new System.Windows.Forms.Padding(3);
            this.PanelSimulationScale.RowCount = 1;
            this.PanelSimulationScale.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PanelSimulationScale.Size = new System.Drawing.Size(340, 31);
            this.PanelSimulationScale.TabIndex = 1;
            // 
            // LabelSimulationScale
            // 
            this.LabelSimulationScale.AutoSize = true;
            this.LabelSimulationScale.Dock = System.Windows.Forms.DockStyle.Right;
            this.LabelSimulationScale.Location = new System.Drawing.Point(6, 3);
            this.LabelSimulationScale.Name = "LabelSimulationScale";
            this.LabelSimulationScale.Size = new System.Drawing.Size(59, 25);
            this.LabelSimulationScale.TabIndex = 0;
            this.LabelSimulationScale.Text = "TimeStep :";
            this.LabelSimulationScale.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TrackBarSimulationScale
            // 
            this.TrackBarSimulationScale.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TrackBarSimulationScale.Location = new System.Drawing.Point(71, 6);
            this.TrackBarSimulationScale.Maximum = 80;
            this.TrackBarSimulationScale.Minimum = 1;
            this.TrackBarSimulationScale.Name = "TrackBarSimulationScale";
            this.TrackBarSimulationScale.Size = new System.Drawing.Size(198, 19);
            this.TrackBarSimulationScale.TabIndex = 1;
            this.TrackBarSimulationScale.TickFrequency = 0;
            this.TrackBarSimulationScale.Value = 1;
            this.TrackBarSimulationScale.ValueChanged += new System.EventHandler(this.TrackBarSimulationScale_ValueChanged);
            // 
            // UdSimulationScale
            // 
            this.UdSimulationScale.DecimalPlaces = 1;
            this.UdSimulationScale.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UdSimulationScale.Location = new System.Drawing.Point(275, 6);
            this.UdSimulationScale.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.UdSimulationScale.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.UdSimulationScale.Name = "UdSimulationScale";
            this.UdSimulationScale.Size = new System.Drawing.Size(59, 19);
            this.UdSimulationScale.TabIndex = 2;
            this.UdSimulationScale.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UdSimulationScale.ValueChanged += new System.EventHandler(this.UdSimulationScale_ValueChanged);
            // 
            // SimulationView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PanelSimulationScale);
            this.Controls.Add(this.ButtonInitializeSimulation);
            this.Name = "SimulationView";
            this.Size = new System.Drawing.Size(340, 81);
            this.PanelSimulationScale.ResumeLayout(false);
            this.PanelSimulationScale.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarSimulationScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UdSimulationScale)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ButtonInitializeSimulation;
        private System.Windows.Forms.TableLayoutPanel PanelSimulationScale;
        private System.Windows.Forms.Label LabelSimulationScale;
        private System.Windows.Forms.TrackBar TrackBarSimulationScale;
        private System.Windows.Forms.NumericUpDown UdSimulationScale;
    }
}
