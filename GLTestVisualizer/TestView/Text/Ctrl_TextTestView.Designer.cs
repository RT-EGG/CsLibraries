
namespace GLTestVisualizer.TestView.Text
{
    partial class Ctrl_TextTestView
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
            if (disposing && (components != null))
            {
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
            this.GLView = new RtCs.OpenGL.WinForms.GLControl();
            this.FontDialog = new System.Windows.Forms.FontDialog();
            this.TextBoxInput = new System.Windows.Forms.TextBox();
            this.ButtonFont = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // GLView
            // 
            this.GLView.BackColor = System.Drawing.Color.Black;
            this.GLView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GLView.Location = new System.Drawing.Point(0, 0);
            this.GLView.Name = "GLView";
            this.GLView.Size = new System.Drawing.Size(770, 319);
            this.GLView.TabIndex = 0;
            this.GLView.VSync = false;
            this.GLView.OnRenderScene += new System.EventHandler(this.GLView_OnRenderScene);
            // 
            // TextBoxInput
            // 
            this.TextBoxInput.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TextBoxInput.Location = new System.Drawing.Point(0, 342);
            this.TextBoxInput.Multiline = true;
            this.TextBoxInput.Name = "TextBoxInput";
            this.TextBoxInput.Size = new System.Drawing.Size(770, 190);
            this.TextBoxInput.TabIndex = 1;
            this.TextBoxInput.TextChanged += new System.EventHandler(this.TextBoxInput_TextChanged);
            // 
            // ButtonFont
            // 
            this.ButtonFont.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ButtonFont.Location = new System.Drawing.Point(0, 319);
            this.ButtonFont.MaximumSize = new System.Drawing.Size(100, 23);
            this.ButtonFont.Name = "ButtonFont";
            this.ButtonFont.Size = new System.Drawing.Size(100, 23);
            this.ButtonFont.TabIndex = 2;
            this.ButtonFont.Text = "Font...";
            this.ButtonFont.UseVisualStyleBackColor = true;
            this.ButtonFont.Click += new System.EventHandler(this.ButtonFont_Click);
            // 
            // Ctrl_TextTestView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.GLView);
            this.Controls.Add(this.ButtonFont);
            this.Controls.Add(this.TextBoxInput);
            this.Name = "Ctrl_TextTestView";
            this.Size = new System.Drawing.Size(770, 532);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RtCs.OpenGL.WinForms.GLControl GLView;
        private System.Windows.Forms.FontDialog FontDialog;
        private System.Windows.Forms.TextBox TextBoxInput;
        private System.Windows.Forms.Button ButtonFont;
    }
}
