
namespace GLTestVisualizer.TestView.Texture
{
    partial class Ctrl_TextureTestView
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
            this.ButtonImportImageFile = new System.Windows.Forms.Button();
            this.PanelLayout = new System.Windows.Forms.TableLayoutPanel();
            this.PanelSamplerParameters = new System.Windows.Forms.TableLayoutPanel();
            this.LabelWrap = new System.Windows.Forms.Label();
            this.LabelFilter = new System.Windows.Forms.Label();
            this.ComboFilter = new System.Windows.Forms.ComboBox();
            this.ComboWrap = new System.Windows.Forms.ComboBox();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.GLView = new RtCs.OpenGL.WinForms.GLControl();
            this.LabelBorderR = new System.Windows.Forms.Label();
            this.UpDownBorderR = new System.Windows.Forms.NumericUpDown();
            this.LabelBorderG = new System.Windows.Forms.Label();
            this.UpDownBorderG = new System.Windows.Forms.NumericUpDown();
            this.LabelBorderB = new System.Windows.Forms.Label();
            this.UpDownBorderB = new System.Windows.Forms.NumericUpDown();
            this.LabelBorderA = new System.Windows.Forms.Label();
            this.UpDownBorderA = new System.Windows.Forms.NumericUpDown();
            this.PanelLayout.SuspendLayout();
            this.PanelSamplerParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownBorderR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownBorderG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownBorderB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownBorderA)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonImportImageFile
            // 
            this.ButtonImportImageFile.Location = new System.Drawing.Point(3, 395);
            this.ButtonImportImageFile.Name = "ButtonImportImageFile";
            this.ButtonImportImageFile.Size = new System.Drawing.Size(95, 23);
            this.ButtonImportImageFile.TabIndex = 1;
            this.ButtonImportImageFile.Text = "Import file ...";
            this.ButtonImportImageFile.UseVisualStyleBackColor = true;
            this.ButtonImportImageFile.Click += new System.EventHandler(this.ButtonImportImageFile_Click);
            // 
            // PanelLayout
            // 
            this.PanelLayout.ColumnCount = 1;
            this.PanelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PanelLayout.Controls.Add(this.ButtonImportImageFile, 0, 1);
            this.PanelLayout.Controls.Add(this.GLView, 0, 0);
            this.PanelLayout.Controls.Add(this.PanelSamplerParameters, 0, 2);
            this.PanelLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelLayout.Location = new System.Drawing.Point(0, 0);
            this.PanelLayout.Name = "PanelLayout";
            this.PanelLayout.RowCount = 3;
            this.PanelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PanelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PanelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 146F));
            this.PanelLayout.Size = new System.Drawing.Size(751, 567);
            this.PanelLayout.TabIndex = 2;
            // 
            // PanelSamplerParameters
            // 
            this.PanelSamplerParameters.ColumnCount = 4;
            this.PanelSamplerParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.PanelSamplerParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.PanelSamplerParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.PanelSamplerParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PanelSamplerParameters.Controls.Add(this.UpDownBorderA, 3, 3);
            this.PanelSamplerParameters.Controls.Add(this.LabelBorderA, 2, 3);
            this.PanelSamplerParameters.Controls.Add(this.UpDownBorderB, 4, 2);
            this.PanelSamplerParameters.Controls.Add(this.LabelBorderB, 2, 2);
            this.PanelSamplerParameters.Controls.Add(this.UpDownBorderG, 3, 1);
            this.PanelSamplerParameters.Controls.Add(this.LabelBorderG, 2, 1);
            this.PanelSamplerParameters.Controls.Add(this.LabelBorderR, 2, 0);
            this.PanelSamplerParameters.Controls.Add(this.LabelWrap, 0, 1);
            this.PanelSamplerParameters.Controls.Add(this.LabelFilter, 0, 0);
            this.PanelSamplerParameters.Controls.Add(this.ComboFilter, 1, 0);
            this.PanelSamplerParameters.Controls.Add(this.ComboWrap, 1, 1);
            this.PanelSamplerParameters.Controls.Add(this.UpDownBorderR, 3, 0);
            this.PanelSamplerParameters.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelSamplerParameters.Location = new System.Drawing.Point(3, 424);
            this.PanelSamplerParameters.Name = "PanelSamplerParameters";
            this.PanelSamplerParameters.RowCount = 5;
            this.PanelSamplerParameters.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PanelSamplerParameters.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PanelSamplerParameters.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PanelSamplerParameters.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PanelSamplerParameters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PanelSamplerParameters.Size = new System.Drawing.Size(745, 127);
            this.PanelSamplerParameters.TabIndex = 2;
            // 
            // LabelWrap
            // 
            this.LabelWrap.AutoSize = true;
            this.LabelWrap.Dock = System.Windows.Forms.DockStyle.Right;
            this.LabelWrap.Location = new System.Drawing.Point(5, 26);
            this.LabelWrap.Name = "LabelWrap";
            this.LabelWrap.Size = new System.Drawing.Size(30, 26);
            this.LabelWrap.TabIndex = 4;
            this.LabelWrap.Text = "Wrap";
            this.LabelWrap.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LabelFilter
            // 
            this.LabelFilter.AutoSize = true;
            this.LabelFilter.Dock = System.Windows.Forms.DockStyle.Right;
            this.LabelFilter.Location = new System.Drawing.Point(3, 0);
            this.LabelFilter.Name = "LabelFilter";
            this.LabelFilter.Size = new System.Drawing.Size(32, 26);
            this.LabelFilter.TabIndex = 0;
            this.LabelFilter.Text = "Filter";
            this.LabelFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ComboFilter
            // 
            this.ComboFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboFilter.FormattingEnabled = true;
            this.ComboFilter.Location = new System.Drawing.Point(41, 3);
            this.ComboFilter.Name = "ComboFilter";
            this.ComboFilter.Size = new System.Drawing.Size(121, 20);
            this.ComboFilter.TabIndex = 1;
            this.ComboFilter.SelectedIndexChanged += new System.EventHandler(this.ComboFilter_SelectedIndexChanged);
            // 
            // ComboWrap
            // 
            this.ComboWrap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboWrap.FormattingEnabled = true;
            this.ComboWrap.Location = new System.Drawing.Point(41, 29);
            this.ComboWrap.Name = "ComboWrap";
            this.ComboWrap.Size = new System.Drawing.Size(121, 20);
            this.ComboWrap.TabIndex = 5;
            this.ComboWrap.SelectedIndexChanged += new System.EventHandler(this.ComboWrap_SelectedIndexChanged);
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.Filter = "image file|*.bmp;*.jpg;*.jpeg;*.png|all|*.*";
            this.OpenFileDialog.Title = "Select open image file.";
            // 
            // GLView
            // 
            this.GLView.BackColor = System.Drawing.Color.Black;
            this.GLView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GLView.Location = new System.Drawing.Point(3, 3);
            this.GLView.Name = "GLView";
            this.GLView.Size = new System.Drawing.Size(745, 386);
            this.GLView.TabIndex = 0;
            this.GLView.VSync = false;
            this.GLView.OnRenderScene += new System.EventHandler(this.GLView_OnRenderScene);
            this.GLView.SizeChanged += new System.EventHandler(this.GLView_SizeChanged);
            // 
            // LabelBorderR
            // 
            this.LabelBorderR.AutoSize = true;
            this.LabelBorderR.Dock = System.Windows.Forms.DockStyle.Right;
            this.LabelBorderR.Location = new System.Drawing.Point(168, 0);
            this.LabelBorderR.Name = "LabelBorderR";
            this.LabelBorderR.Size = new System.Drawing.Size(51, 26);
            this.LabelBorderR.TabIndex = 6;
            this.LabelBorderR.Text = "Border R";
            this.LabelBorderR.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UpDownBorderR
            // 
            this.UpDownBorderR.Location = new System.Drawing.Point(225, 3);
            this.UpDownBorderR.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.UpDownBorderR.Name = "UpDownBorderR";
            this.UpDownBorderR.Size = new System.Drawing.Size(121, 19);
            this.UpDownBorderR.TabIndex = 7;
            // 
            // LabelBorderG
            // 
            this.LabelBorderG.AutoSize = true;
            this.LabelBorderG.Dock = System.Windows.Forms.DockStyle.Right;
            this.LabelBorderG.Location = new System.Drawing.Point(168, 26);
            this.LabelBorderG.Name = "LabelBorderG";
            this.LabelBorderG.Size = new System.Drawing.Size(51, 26);
            this.LabelBorderG.TabIndex = 8;
            this.LabelBorderG.Text = "Border G";
            this.LabelBorderG.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UpDownBorderG
            // 
            this.UpDownBorderG.Location = new System.Drawing.Point(225, 29);
            this.UpDownBorderG.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.UpDownBorderG.Name = "UpDownBorderG";
            this.UpDownBorderG.Size = new System.Drawing.Size(121, 19);
            this.UpDownBorderG.TabIndex = 9;
            // 
            // LabelBorderB
            // 
            this.LabelBorderB.AutoSize = true;
            this.LabelBorderB.Dock = System.Windows.Forms.DockStyle.Right;
            this.LabelBorderB.Location = new System.Drawing.Point(168, 52);
            this.LabelBorderB.Name = "LabelBorderB";
            this.LabelBorderB.Size = new System.Drawing.Size(51, 25);
            this.LabelBorderB.TabIndex = 10;
            this.LabelBorderB.Text = "Border B";
            this.LabelBorderB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UpDownBorderB
            // 
            this.UpDownBorderB.Location = new System.Drawing.Point(225, 55);
            this.UpDownBorderB.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.UpDownBorderB.Name = "UpDownBorderB";
            this.UpDownBorderB.Size = new System.Drawing.Size(121, 19);
            this.UpDownBorderB.TabIndex = 11;
            // 
            // LabelBorderA
            // 
            this.LabelBorderA.AutoSize = true;
            this.LabelBorderA.Dock = System.Windows.Forms.DockStyle.Right;
            this.LabelBorderA.Location = new System.Drawing.Point(168, 77);
            this.LabelBorderA.Name = "LabelBorderA";
            this.LabelBorderA.Size = new System.Drawing.Size(51, 25);
            this.LabelBorderA.TabIndex = 12;
            this.LabelBorderA.Text = "Border A";
            this.LabelBorderA.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UpDownBorderA
            // 
            this.UpDownBorderA.Location = new System.Drawing.Point(225, 80);
            this.UpDownBorderA.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.UpDownBorderA.Name = "UpDownBorderA";
            this.UpDownBorderA.Size = new System.Drawing.Size(121, 19);
            this.UpDownBorderA.TabIndex = 13;
            // 
            // Ctrl_TextureTestView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.PanelLayout);
            this.Name = "Ctrl_TextureTestView";
            this.Size = new System.Drawing.Size(751, 567);
            this.PanelLayout.ResumeLayout(false);
            this.PanelSamplerParameters.ResumeLayout(false);
            this.PanelSamplerParameters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownBorderR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownBorderG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownBorderB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownBorderA)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private RtCs.OpenGL.WinForms.GLControl GLView;
        private System.Windows.Forms.Button ButtonImportImageFile;
        private System.Windows.Forms.TableLayoutPanel PanelLayout;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.TableLayoutPanel PanelSamplerParameters;
        private System.Windows.Forms.Label LabelFilter;
        private System.Windows.Forms.ComboBox ComboFilter;
        private System.Windows.Forms.Label LabelWrap;
        private System.Windows.Forms.ComboBox ComboWrap;
        private System.Windows.Forms.NumericUpDown UpDownBorderA;
        private System.Windows.Forms.Label LabelBorderA;
        private System.Windows.Forms.NumericUpDown UpDownBorderB;
        private System.Windows.Forms.Label LabelBorderB;
        private System.Windows.Forms.NumericUpDown UpDownBorderG;
        private System.Windows.Forms.Label LabelBorderG;
        private System.Windows.Forms.Label LabelBorderR;
        private System.Windows.Forms.NumericUpDown UpDownBorderR;
    }
}
