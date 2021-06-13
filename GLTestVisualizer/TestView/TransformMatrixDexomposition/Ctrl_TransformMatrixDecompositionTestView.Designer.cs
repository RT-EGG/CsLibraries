
namespace GLTestVisualizer.TestView.TransformMatrixDexomposition
{
    partial class Ctrl_TransformMatrixDecompositionTestView
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
            this.PanelBaseLayout = new System.Windows.Forms.TableLayoutPanel();
            this.Ctrl_MatrixOutput = new GLTestVisualizer.TestView.TransformMatrixDexomposition.Ctrl_DecompositeTransformMatrixView();
            this.Ctrl_MatrixInput = new GLTestVisualizer.TestView.TransformMatrixDexomposition.Ctrl_DecompositeTransformMatrixView();
            this.GLViewer = new GLTestVisualizer.TestView.Ctrl_GLViewer();
            this.PanelProperties = new System.Windows.Forms.TableLayoutPanel();
            this.LabelRotationOrder = new System.Windows.Forms.Label();
            this.ComboRotationOrder = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.PanelBaseLayout.SuspendLayout();
            this.PanelProperties.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelBaseLayout
            // 
            this.PanelBaseLayout.ColumnCount = 2;
            this.PanelBaseLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PanelBaseLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PanelBaseLayout.Controls.Add(this.Ctrl_MatrixOutput, 1, 2);
            this.PanelBaseLayout.Controls.Add(this.Ctrl_MatrixInput, 0, 2);
            this.PanelBaseLayout.Controls.Add(this.GLViewer, 0, 1);
            this.PanelBaseLayout.Controls.Add(this.PanelProperties, 0, 0);
            this.PanelBaseLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelBaseLayout.Location = new System.Drawing.Point(0, 0);
            this.PanelBaseLayout.Name = "PanelBaseLayout";
            this.PanelBaseLayout.RowCount = 3;
            this.PanelBaseLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.PanelBaseLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PanelBaseLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 145F));
            this.PanelBaseLayout.Size = new System.Drawing.Size(611, 467);
            this.PanelBaseLayout.TabIndex = 0;
            // 
            // Ctrl_MatrixOutput
            // 
            this.Ctrl_MatrixOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Ctrl_MatrixOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Ctrl_MatrixOutput.Enabled = false;
            this.Ctrl_MatrixOutput.Location = new System.Drawing.Point(308, 325);
            this.Ctrl_MatrixOutput.Name = "Ctrl_MatrixOutput";
            this.Ctrl_MatrixOutput.Size = new System.Drawing.Size(300, 139);
            this.Ctrl_MatrixOutput.TabIndex = 1;
            // 
            // Ctrl_MatrixInput
            // 
            this.Ctrl_MatrixInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Ctrl_MatrixInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Ctrl_MatrixInput.Location = new System.Drawing.Point(3, 325);
            this.Ctrl_MatrixInput.Name = "Ctrl_MatrixInput";
            this.Ctrl_MatrixInput.Size = new System.Drawing.Size(299, 139);
            this.Ctrl_MatrixInput.TabIndex = 0;
            this.Ctrl_MatrixInput.TranslationChanged += new GLTestVisualizer.TestView.TransformMatrixDexomposition.Vector3EventHandler(this.Ctrl_MatrixInput_TranslationChanged);
            this.Ctrl_MatrixInput.RotationChanged += new GLTestVisualizer.TestView.TransformMatrixDexomposition.Vector3EventHandler(this.Ctrl_MatrixInput_RotationChanged);
            this.Ctrl_MatrixInput.ScaleChanged += new GLTestVisualizer.TestView.TransformMatrixDexomposition.Vector3EventHandler(this.Ctrl_MatrixInput_ScaleChanged);
            // 
            // GLViewer
            // 
            this.GLViewer.BackColor = System.Drawing.Color.Black;
            this.PanelBaseLayout.SetColumnSpan(this.GLViewer, 2);
            this.GLViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GLViewer.Location = new System.Drawing.Point(3, 36);
            this.GLViewer.Name = "GLViewer";
            this.GLViewer.Size = new System.Drawing.Size(605, 283);
            this.GLViewer.TabIndex = 2;
            this.GLViewer.VSync = false;
            this.GLViewer.OnPaintScene += new GLTestVisualizer.TestView.GLControlPaintEvent(this.GLViewer_OnPaintScene);
            // 
            // PanelProperties
            // 
            this.PanelProperties.ColumnCount = 2;
            this.PanelBaseLayout.SetColumnSpan(this.PanelProperties, 2);
            this.PanelProperties.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.PanelProperties.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PanelProperties.Controls.Add(this.LabelRotationOrder, 0, 0);
            this.PanelProperties.Controls.Add(this.ComboRotationOrder, 1, 0);
            this.PanelProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelProperties.Location = new System.Drawing.Point(3, 3);
            this.PanelProperties.Name = "PanelProperties";
            this.PanelProperties.RowCount = 1;
            this.PanelProperties.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PanelProperties.Size = new System.Drawing.Size(605, 27);
            this.PanelProperties.TabIndex = 3;
            // 
            // LabelRotationOrder
            // 
            this.LabelRotationOrder.AutoSize = true;
            this.LabelRotationOrder.Dock = System.Windows.Forms.DockStyle.Left;
            this.LabelRotationOrder.Location = new System.Drawing.Point(3, 0);
            this.LabelRotationOrder.Name = "LabelRotationOrder";
            this.LabelRotationOrder.Size = new System.Drawing.Size(82, 27);
            this.LabelRotationOrder.TabIndex = 0;
            this.LabelRotationOrder.Text = "RotationOrder :";
            this.LabelRotationOrder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ComboRotationOrder
            // 
            this.ComboRotationOrder.Dock = System.Windows.Forms.DockStyle.Left;
            this.ComboRotationOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboRotationOrder.FormattingEnabled = true;
            this.ComboRotationOrder.Location = new System.Drawing.Point(91, 3);
            this.ComboRotationOrder.Name = "ComboRotationOrder";
            this.ComboRotationOrder.Size = new System.Drawing.Size(121, 20);
            this.ComboRotationOrder.TabIndex = 1;
            this.ComboRotationOrder.SelectedIndexChanged += new System.EventHandler(this.ComboRotationOrder_SelectedIndexChanged);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Ctrl_TransformMatrixDecompositionTestView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.PanelBaseLayout);
            this.Name = "Ctrl_TransformMatrixDecompositionTestView";
            this.Size = new System.Drawing.Size(611, 467);
            this.PanelBaseLayout.ResumeLayout(false);
            this.PanelProperties.ResumeLayout(false);
            this.PanelProperties.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel PanelBaseLayout;
        private TransformMatrixDexomposition.Ctrl_DecompositeTransformMatrixView Ctrl_MatrixInput;
        private TransformMatrixDexomposition.Ctrl_DecompositeTransformMatrixView Ctrl_MatrixOutput;
        private Ctrl_GLViewer GLViewer;
        private System.Windows.Forms.TableLayoutPanel PanelProperties;
        private System.Windows.Forms.Label LabelRotationOrder;
        private System.Windows.Forms.ComboBox ComboRotationOrder;
        private System.Windows.Forms.Timer timer1;
    }
}
