
namespace GLTestVisualizer
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
            this.ComboTestContent = new System.Windows.Forms.ComboBox();
            this.PanelContentView = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // ComboTestContent
            // 
            this.ComboTestContent.Dock = System.Windows.Forms.DockStyle.Top;
            this.ComboTestContent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboTestContent.FormattingEnabled = true;
            this.ComboTestContent.Location = new System.Drawing.Point(4, 4);
            this.ComboTestContent.Margin = new System.Windows.Forms.Padding(4);
            this.ComboTestContent.MaximumSize = new System.Drawing.Size(233, 0);
            this.ComboTestContent.Name = "ComboTestContent";
            this.ComboTestContent.Size = new System.Drawing.Size(233, 23);
            this.ComboTestContent.TabIndex = 0;
            this.ComboTestContent.SelectionChangeCommitted += new System.EventHandler(this.ComboTestContent_SelectionChangeCommitted);
            // 
            // PanelContentView
            // 
            this.PanelContentView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelContentView.Location = new System.Drawing.Point(4, 27);
            this.PanelContentView.Margin = new System.Windows.Forms.Padding(4);
            this.PanelContentView.Name = "PanelContentView";
            this.PanelContentView.Padding = new System.Windows.Forms.Padding(4);
            this.PanelContentView.Size = new System.Drawing.Size(873, 531);
            this.PanelContentView.TabIndex = 1;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 562);
            this.Controls.Add(this.PanelContentView);
            this.Controls.Add(this.ComboTestContent);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormMain";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox ComboTestContent;
        private System.Windows.Forms.Panel PanelContentView;
    }
}

