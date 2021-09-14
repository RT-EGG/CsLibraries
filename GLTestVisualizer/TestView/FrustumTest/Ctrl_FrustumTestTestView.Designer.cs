
namespace GLTestVisualizer.TestView.FrustumTest
{
    partial class Ctrl_FrustumTestTestView
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ComboProjectionType = new System.Windows.Forms.ComboBox();
            this.CheckBoxAutoAspect = new System.Windows.Forms.CheckBox();
            this.PanelParameterView = new System.Windows.Forms.Panel();
            this.GLThrirdPersonView = new RtCs.OpenGL.GLControl();
            this.PanelViews = new System.Windows.Forms.TableLayoutPanel();
            this.GLFirstPersonView = new RtCs.OpenGL.GLControl();
            this.ButtonRandomizer = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.PanelViews.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 16;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.ComboProjectionType, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.CheckBoxAutoAspect, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 325);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(619, 28);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // ComboProjectionType
            // 
            this.ComboProjectionType.Dock = System.Windows.Forms.DockStyle.Left;
            this.ComboProjectionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboProjectionType.FormattingEnabled = true;
            this.ComboProjectionType.Location = new System.Drawing.Point(3, 3);
            this.ComboProjectionType.Name = "ComboProjectionType";
            this.ComboProjectionType.Size = new System.Drawing.Size(187, 20);
            this.ComboProjectionType.TabIndex = 0;
            this.ComboProjectionType.SelectionChangeCommitted += new System.EventHandler(this.ComboProjectionType_SelectionChangeCommitted);
            // 
            // CheckBoxAutoAspect
            // 
            this.CheckBoxAutoAspect.AutoSize = true;
            this.CheckBoxAutoAspect.Dock = System.Windows.Forms.DockStyle.Right;
            this.CheckBoxAutoAspect.Location = new System.Drawing.Point(521, 3);
            this.CheckBoxAutoAspect.Name = "CheckBoxAutoAspect";
            this.CheckBoxAutoAspect.Size = new System.Drawing.Size(95, 22);
            this.CheckBoxAutoAspect.TabIndex = 1;
            this.CheckBoxAutoAspect.Text = "AutoAspect";
            this.CheckBoxAutoAspect.UseVisualStyleBackColor = true;
            this.CheckBoxAutoAspect.CheckedChanged += new System.EventHandler(this.CheckBoxAutoAspect_CheckedChanged);
            // 
            // PanelParameterView
            // 
            this.PanelParameterView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelParameterView.Location = new System.Drawing.Point(0, 353);
            this.PanelParameterView.Name = "PanelParameterView";
            this.PanelParameterView.Size = new System.Drawing.Size(619, 140);
            this.PanelParameterView.TabIndex = 2;
            // 
            // GLThrirdPersonView
            // 
            this.GLThrirdPersonView.BackColor = System.Drawing.Color.Black;
            this.GLThrirdPersonView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GLThrirdPersonView.Location = new System.Drawing.Point(312, 3);
            this.GLThrirdPersonView.Name = "GLThrirdPersonView";
            this.GLThrirdPersonView.Size = new System.Drawing.Size(304, 319);
            this.GLThrirdPersonView.TabIndex = 0;
            this.GLThrirdPersonView.VSync = false;
            this.GLThrirdPersonView.OnRenderScene += new RtCs.OpenGL.GLRenderSceneEventHandler(this.GLThridPersonView_OnRenderScene);
            // 
            // PanelViews
            // 
            this.PanelViews.ColumnCount = 2;
            this.PanelViews.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PanelViews.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PanelViews.Controls.Add(this.GLThrirdPersonView, 1, 0);
            this.PanelViews.Controls.Add(this.GLFirstPersonView, 0, 0);
            this.PanelViews.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelViews.Location = new System.Drawing.Point(0, 0);
            this.PanelViews.Name = "PanelViews";
            this.PanelViews.RowCount = 1;
            this.PanelViews.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PanelViews.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 325F));
            this.PanelViews.Size = new System.Drawing.Size(619, 325);
            this.PanelViews.TabIndex = 3;
            // 
            // GLFirstPersonView
            // 
            this.GLFirstPersonView.BackColor = System.Drawing.Color.Black;
            this.GLFirstPersonView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GLFirstPersonView.Location = new System.Drawing.Point(3, 3);
            this.GLFirstPersonView.Name = "GLFirstPersonView";
            this.GLFirstPersonView.Size = new System.Drawing.Size(303, 319);
            this.GLFirstPersonView.TabIndex = 1;
            this.GLFirstPersonView.VSync = false;
            this.GLFirstPersonView.OnRenderScene += new RtCs.OpenGL.GLRenderSceneEventHandler(this.GLFirstPersonView_OnRenderScene);
            // 
            // ButtonRandomizer
            // 
            this.ButtonRandomizer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonRandomizer.Location = new System.Drawing.Point(532, 0);
            this.ButtonRandomizer.Name = "ButtonRandomizer";
            this.ButtonRandomizer.Size = new System.Drawing.Size(84, 23);
            this.ButtonRandomizer.TabIndex = 4;
            this.ButtonRandomizer.Text = "Randomize";
            this.ButtonRandomizer.UseVisualStyleBackColor = true;
            this.ButtonRandomizer.Click += new System.EventHandler(this.ButtonRandomizer_Click);
            // 
            // Ctrl_FrustumTestTestView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.ButtonRandomizer);
            this.Controls.Add(this.PanelViews);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.PanelParameterView);
            this.Name = "Ctrl_FrustumTestTestView";
            this.Size = new System.Drawing.Size(619, 493);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.PanelViews.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private RtCs.OpenGL.GLControl GLThrirdPersonView;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox ComboProjectionType;
        private System.Windows.Forms.CheckBox CheckBoxAutoAspect;
        private System.Windows.Forms.Panel PanelParameterView;
        private System.Windows.Forms.TableLayoutPanel PanelViews;
        private RtCs.OpenGL.GLControl GLFirstPersonView;
        private System.Windows.Forms.Button ButtonRandomizer;
    }
}
