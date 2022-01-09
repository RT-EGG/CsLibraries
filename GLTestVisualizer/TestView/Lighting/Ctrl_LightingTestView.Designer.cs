
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
            this.UpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.PanelParameterEditor = new System.Windows.Forms.TableLayoutPanel();
            this.PanelSphereMaterialEditor = new System.Windows.Forms.TableLayoutPanel();
            this.ButtonSphereMaterialEmission = new GLTestVisualizer.TestView.Lighting.ColorSelectButton();
            this.ButtonSphereMaterialDiffuse = new GLTestVisualizer.TestView.Lighting.ColorSelectButton();
            this.ButtonSphereMaterialSpecular = new GLTestVisualizer.TestView.Lighting.ColorSelectButton();
            this.LabelSphereMaterialEmission = new System.Windows.Forms.Label();
            this.LabelSphereMaterialSpecular = new System.Windows.Forms.Label();
            this.LabelSphereMaterialDiffuse = new System.Windows.Forms.Label();
            this.LabelSphereMaterialAmbient = new System.Windows.Forms.Label();
            this.LabelSphereMaterial = new System.Windows.Forms.Label();
            this.ButtonSphereMaterialAmbient = new GLTestVisualizer.TestView.Lighting.ColorSelectButton();
            this.GLControl = new RtCs.OpenGL.WinForms.GLControl();
            this.PanelParameterEditor.SuspendLayout();
            this.PanelSphereMaterialEditor.SuspendLayout();
            this.SuspendLayout();
            // 
            // UpdateTimer
            // 
            this.UpdateTimer.Enabled = true;
            this.UpdateTimer.Interval = 16;
            this.UpdateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
            // 
            // PanelParameterEditor
            // 
            this.PanelParameterEditor.ColumnCount = 3;
            this.PanelParameterEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 182F));
            this.PanelParameterEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PanelParameterEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.PanelParameterEditor.Controls.Add(this.PanelSphereMaterialEditor, 0, 0);
            this.PanelParameterEditor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelParameterEditor.Location = new System.Drawing.Point(0, 302);
            this.PanelParameterEditor.Name = "PanelParameterEditor";
            this.PanelParameterEditor.RowCount = 1;
            this.PanelParameterEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PanelParameterEditor.Size = new System.Drawing.Size(574, 138);
            this.PanelParameterEditor.TabIndex = 1;
            // 
            // PanelSphereMaterialEditor
            // 
            this.PanelSphereMaterialEditor.ColumnCount = 2;
            this.PanelSphereMaterialEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.PanelSphereMaterialEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PanelSphereMaterialEditor.Controls.Add(this.ButtonSphereMaterialEmission, 1, 4);
            this.PanelSphereMaterialEditor.Controls.Add(this.ButtonSphereMaterialDiffuse, 1, 2);
            this.PanelSphereMaterialEditor.Controls.Add(this.ButtonSphereMaterialSpecular, 1, 3);
            this.PanelSphereMaterialEditor.Controls.Add(this.LabelSphereMaterialEmission, 0, 4);
            this.PanelSphereMaterialEditor.Controls.Add(this.LabelSphereMaterialSpecular, 0, 3);
            this.PanelSphereMaterialEditor.Controls.Add(this.LabelSphereMaterialDiffuse, 0, 2);
            this.PanelSphereMaterialEditor.Controls.Add(this.LabelSphereMaterialAmbient, 0, 1);
            this.PanelSphereMaterialEditor.Controls.Add(this.LabelSphereMaterial, 0, 0);
            this.PanelSphereMaterialEditor.Controls.Add(this.ButtonSphereMaterialAmbient, 1, 1);
            this.PanelSphereMaterialEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelSphereMaterialEditor.Location = new System.Drawing.Point(3, 3);
            this.PanelSphereMaterialEditor.Name = "PanelSphereMaterialEditor";
            this.PanelSphereMaterialEditor.RowCount = 6;
            this.PanelSphereMaterialEditor.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PanelSphereMaterialEditor.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PanelSphereMaterialEditor.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PanelSphereMaterialEditor.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PanelSphereMaterialEditor.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PanelSphereMaterialEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PanelSphereMaterialEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.PanelSphereMaterialEditor.Size = new System.Drawing.Size(176, 132);
            this.PanelSphereMaterialEditor.TabIndex = 0;
            // 
            // ButtonSphereMaterialEmission
            // 
            this.ButtonSphereMaterialEmission.BackColor = System.Drawing.Color.White;
            this.ButtonSphereMaterialEmission.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonSphereMaterialEmission.Location = new System.Drawing.Point(66, 102);
            this.ButtonSphereMaterialEmission.Name = "ButtonSphereMaterialEmission";
            this.ButtonSphereMaterialEmission.Size = new System.Drawing.Size(107, 23);
            this.ButtonSphereMaterialEmission.TabIndex = 14;
            this.ButtonSphereMaterialEmission.UseVisualStyleBackColor = false;
            this.ButtonSphereMaterialEmission.Value = System.Drawing.Color.White;
            this.ButtonSphereMaterialEmission.ValueChanged += new System.EventHandler(this.ButtonSphereMaterialEmission_ValueChanged);
            // 
            // ButtonSphereMaterialDiffuse
            // 
            this.ButtonSphereMaterialDiffuse.BackColor = System.Drawing.Color.White;
            this.ButtonSphereMaterialDiffuse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonSphereMaterialDiffuse.Location = new System.Drawing.Point(66, 44);
            this.ButtonSphereMaterialDiffuse.Name = "ButtonSphereMaterialDiffuse";
            this.ButtonSphereMaterialDiffuse.Size = new System.Drawing.Size(107, 23);
            this.ButtonSphereMaterialDiffuse.TabIndex = 13;
            this.ButtonSphereMaterialDiffuse.UseVisualStyleBackColor = false;
            this.ButtonSphereMaterialDiffuse.Value = System.Drawing.Color.White;
            this.ButtonSphereMaterialDiffuse.ValueChanged += new System.EventHandler(this.ButtonSphereMaterialDiffuse_ValueChanged);
            // 
            // ButtonSphereMaterialSpecular
            // 
            this.ButtonSphereMaterialSpecular.BackColor = System.Drawing.Color.White;
            this.ButtonSphereMaterialSpecular.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonSphereMaterialSpecular.Location = new System.Drawing.Point(66, 73);
            this.ButtonSphereMaterialSpecular.Name = "ButtonSphereMaterialSpecular";
            this.ButtonSphereMaterialSpecular.Size = new System.Drawing.Size(107, 23);
            this.ButtonSphereMaterialSpecular.TabIndex = 12;
            this.ButtonSphereMaterialSpecular.UseVisualStyleBackColor = false;
            this.ButtonSphereMaterialSpecular.Value = System.Drawing.Color.White;
            this.ButtonSphereMaterialSpecular.ValueChanged += new System.EventHandler(this.ButtonSphereMaterialSpecular_ValueChanged);
            // 
            // LabelSphereMaterialEmission
            // 
            this.LabelSphereMaterialEmission.AutoSize = true;
            this.LabelSphereMaterialEmission.Dock = System.Windows.Forms.DockStyle.Right;
            this.LabelSphereMaterialEmission.Location = new System.Drawing.Point(3, 99);
            this.LabelSphereMaterialEmission.Name = "LabelSphereMaterialEmission";
            this.LabelSphereMaterialEmission.Size = new System.Drawing.Size(57, 29);
            this.LabelSphereMaterialEmission.TabIndex = 7;
            this.LabelSphereMaterialEmission.Text = "Emission :";
            this.LabelSphereMaterialEmission.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LabelSphereMaterialSpecular
            // 
            this.LabelSphereMaterialSpecular.AutoSize = true;
            this.LabelSphereMaterialSpecular.Dock = System.Windows.Forms.DockStyle.Right;
            this.LabelSphereMaterialSpecular.Location = new System.Drawing.Point(5, 70);
            this.LabelSphereMaterialSpecular.Name = "LabelSphereMaterialSpecular";
            this.LabelSphereMaterialSpecular.Size = new System.Drawing.Size(55, 29);
            this.LabelSphereMaterialSpecular.TabIndex = 5;
            this.LabelSphereMaterialSpecular.Text = "Specular :";
            this.LabelSphereMaterialSpecular.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LabelSphereMaterialDiffuse
            // 
            this.LabelSphereMaterialDiffuse.AutoSize = true;
            this.LabelSphereMaterialDiffuse.Dock = System.Windows.Forms.DockStyle.Right;
            this.LabelSphereMaterialDiffuse.Location = new System.Drawing.Point(12, 41);
            this.LabelSphereMaterialDiffuse.Name = "LabelSphereMaterialDiffuse";
            this.LabelSphereMaterialDiffuse.Size = new System.Drawing.Size(48, 29);
            this.LabelSphereMaterialDiffuse.TabIndex = 3;
            this.LabelSphereMaterialDiffuse.Text = "Diffuse :";
            this.LabelSphereMaterialDiffuse.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LabelSphereMaterialAmbient
            // 
            this.LabelSphereMaterialAmbient.AutoSize = true;
            this.LabelSphereMaterialAmbient.Dock = System.Windows.Forms.DockStyle.Right;
            this.LabelSphereMaterialAmbient.Location = new System.Drawing.Point(3, 12);
            this.LabelSphereMaterialAmbient.Name = "LabelSphereMaterialAmbient";
            this.LabelSphereMaterialAmbient.Size = new System.Drawing.Size(57, 29);
            this.LabelSphereMaterialAmbient.TabIndex = 1;
            this.LabelSphereMaterialAmbient.Text = "Ambitent :";
            this.LabelSphereMaterialAmbient.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LabelSphereMaterial
            // 
            this.LabelSphereMaterial.AutoSize = true;
            this.PanelSphereMaterialEditor.SetColumnSpan(this.LabelSphereMaterial, 2);
            this.LabelSphereMaterial.Dock = System.Windows.Forms.DockStyle.Left;
            this.LabelSphereMaterial.Location = new System.Drawing.Point(3, 0);
            this.LabelSphereMaterial.Name = "LabelSphereMaterial";
            this.LabelSphereMaterial.Size = new System.Drawing.Size(85, 12);
            this.LabelSphereMaterial.TabIndex = 0;
            this.LabelSphereMaterial.Text = "Sphere Material";
            // 
            // ButtonSphereMaterialAmbient
            // 
            this.ButtonSphereMaterialAmbient.BackColor = System.Drawing.Color.White;
            this.ButtonSphereMaterialAmbient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonSphereMaterialAmbient.Location = new System.Drawing.Point(66, 15);
            this.ButtonSphereMaterialAmbient.Name = "ButtonSphereMaterialAmbient";
            this.ButtonSphereMaterialAmbient.Size = new System.Drawing.Size(107, 23);
            this.ButtonSphereMaterialAmbient.TabIndex = 11;
            this.ButtonSphereMaterialAmbient.UseVisualStyleBackColor = false;
            this.ButtonSphereMaterialAmbient.Value = System.Drawing.Color.White;
            this.ButtonSphereMaterialAmbient.ValueChanged += new System.EventHandler(this.ButtonSphereMaterialAmbient_ValueChanged);
            // 
            // GLControl
            // 
            this.GLControl.BackColor = System.Drawing.Color.Black;
            this.GLControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GLControl.Location = new System.Drawing.Point(0, 0);
            this.GLControl.Name = "GLControl";
            this.GLControl.Size = new System.Drawing.Size(574, 302);
            this.GLControl.TabIndex = 0;
            this.GLControl.VSync = false;
            this.GLControl.OnRenderScene += new System.EventHandler(this.GLControl_OnRenderScene);
            // 
            // Ctrl_LightingTestView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.GLControl);
            this.Controls.Add(this.PanelParameterEditor);
            this.Name = "Ctrl_LightingTestView";
            this.Size = new System.Drawing.Size(574, 440);
            this.PanelParameterEditor.ResumeLayout(false);
            this.PanelSphereMaterialEditor.ResumeLayout(false);
            this.PanelSphereMaterialEditor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private RtCs.OpenGL.WinForms.GLControl GLControl;
        private System.Windows.Forms.Timer UpdateTimer;
        private System.Windows.Forms.TableLayoutPanel PanelParameterEditor;
        private System.Windows.Forms.TableLayoutPanel PanelSphereMaterialEditor;
        private System.Windows.Forms.Label LabelSphereMaterial;
        private System.Windows.Forms.Label LabelSphereMaterialEmission;
        private System.Windows.Forms.Label LabelSphereMaterialSpecular;
        private System.Windows.Forms.Label LabelSphereMaterialDiffuse;
        private System.Windows.Forms.Label LabelSphereMaterialAmbient;
        private ColorSelectButton ButtonSphereMaterialAmbient;
        private ColorSelectButton ButtonSphereMaterialEmission;
        private ColorSelectButton ButtonSphereMaterialDiffuse;
        private ColorSelectButton ButtonSphereMaterialSpecular;
    }
}
