
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
            this.PanelProperties = new System.Windows.Forms.TableLayoutPanel();
            this.LabelRotationOrder = new System.Windows.Forms.Label();
            this.ComboRotationOrder = new System.Windows.Forms.ComboBox();
            this.GLViewer = new RtCs.OpenGL.WinForms.GLControl();
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
            this.PanelBaseLayout.Controls.Add(this.PanelProperties, 0, 0);
            this.PanelBaseLayout.Controls.Add(this.GLViewer, 0, 1);
            this.PanelBaseLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelBaseLayout.Location = new System.Drawing.Point(0, 0);
            this.PanelBaseLayout.Margin = new System.Windows.Forms.Padding(4);
            this.PanelBaseLayout.Name = "PanelBaseLayout";
            this.PanelBaseLayout.RowCount = 3;
            this.PanelBaseLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.PanelBaseLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PanelBaseLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 181F));
            this.PanelBaseLayout.Size = new System.Drawing.Size(713, 584);
            this.PanelBaseLayout.TabIndex = 0;
            // 
            // Ctrl_MatrixOutput
            // 
            this.Ctrl_MatrixOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Ctrl_MatrixOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Ctrl_MatrixOutput.Enabled = false;
            this.Ctrl_MatrixOutput.Location = new System.Drawing.Point(361, 408);
            this.Ctrl_MatrixOutput.Margin = new System.Windows.Forms.Padding(5);
            this.Ctrl_MatrixOutput.Name = "Ctrl_MatrixOutput";
            this.Ctrl_MatrixOutput.Size = new System.Drawing.Size(347, 171);
            this.Ctrl_MatrixOutput.TabIndex = 1;
            // 
            // Ctrl_MatrixInput
            // 
            this.Ctrl_MatrixInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Ctrl_MatrixInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Ctrl_MatrixInput.Location = new System.Drawing.Point(5, 408);
            this.Ctrl_MatrixInput.Margin = new System.Windows.Forms.Padding(5);
            this.Ctrl_MatrixInput.Name = "Ctrl_MatrixInput";
            this.Ctrl_MatrixInput.Size = new System.Drawing.Size(346, 171);
            this.Ctrl_MatrixInput.TabIndex = 0;
            this.Ctrl_MatrixInput.TranslationChanged += new GLTestVisualizer.TestView.TransformMatrixDexomposition.Vector3EventHandler(this.Ctrl_MatrixInput_TranslationChanged);
            this.Ctrl_MatrixInput.RotationChanged += new GLTestVisualizer.TestView.TransformMatrixDexomposition.Vector3EventHandler(this.Ctrl_MatrixInput_RotationChanged);
            this.Ctrl_MatrixInput.ScaleChanged += new GLTestVisualizer.TestView.TransformMatrixDexomposition.Vector3EventHandler(this.Ctrl_MatrixInput_ScaleChanged);
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
            this.PanelProperties.Location = new System.Drawing.Point(4, 4);
            this.PanelProperties.Margin = new System.Windows.Forms.Padding(4);
            this.PanelProperties.Name = "PanelProperties";
            this.PanelProperties.RowCount = 1;
            this.PanelProperties.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PanelProperties.Size = new System.Drawing.Size(705, 33);
            this.PanelProperties.TabIndex = 3;
            // 
            // LabelRotationOrder
            // 
            this.LabelRotationOrder.AutoSize = true;
            this.LabelRotationOrder.Dock = System.Windows.Forms.DockStyle.Left;
            this.LabelRotationOrder.Location = new System.Drawing.Point(4, 0);
            this.LabelRotationOrder.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelRotationOrder.Name = "LabelRotationOrder";
            this.LabelRotationOrder.Size = new System.Drawing.Size(88, 33);
            this.LabelRotationOrder.TabIndex = 0;
            this.LabelRotationOrder.Text = "RotationOrder :";
            this.LabelRotationOrder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ComboRotationOrder
            // 
            this.ComboRotationOrder.Dock = System.Windows.Forms.DockStyle.Left;
            this.ComboRotationOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboRotationOrder.FormattingEnabled = true;
            this.ComboRotationOrder.Location = new System.Drawing.Point(100, 4);
            this.ComboRotationOrder.Margin = new System.Windows.Forms.Padding(4);
            this.ComboRotationOrder.Name = "ComboRotationOrder";
            this.ComboRotationOrder.Size = new System.Drawing.Size(140, 23);
            this.ComboRotationOrder.TabIndex = 1;
            this.ComboRotationOrder.SelectedIndexChanged += new System.EventHandler(this.ComboRotationOrder_SelectedIndexChanged);
            // 
            // GLViewer
            // 
            this.GLViewer.API = OpenTK.Windowing.Common.ContextAPI.OpenGL;
            this.GLViewer.APIVersion = new System.Version(3, 3, 0, 0);
            this.GLViewer.BackColor = System.Drawing.Color.Black;
            this.PanelBaseLayout.SetColumnSpan(this.GLViewer, 2);
            this.GLViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GLViewer.Flags = OpenTK.Windowing.Common.ContextFlags.Default;
            this.GLViewer.IsEventDriven = true;
            this.GLViewer.Location = new System.Drawing.Point(4, 45);
            this.GLViewer.Margin = new System.Windows.Forms.Padding(4);
            this.GLViewer.Name = "GLViewer";
            this.GLViewer.Profile = OpenTK.Windowing.Common.ContextProfile.Core;
            this.GLViewer.Size = new System.Drawing.Size(705, 354);
            this.GLViewer.TabIndex = 4;
            this.GLViewer.OnRenderScene += new System.EventHandler(this.GLViewr_OnRenderScene);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Ctrl_TransformMatrixDecompositionTestView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.Controls.Add(this.PanelBaseLayout);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Ctrl_TransformMatrixDecompositionTestView";
            this.Size = new System.Drawing.Size(713, 584);
            this.PanelBaseLayout.ResumeLayout(false);
            this.PanelProperties.ResumeLayout(false);
            this.PanelProperties.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel PanelBaseLayout;
        private TransformMatrixDexomposition.Ctrl_DecompositeTransformMatrixView Ctrl_MatrixInput;
        private TransformMatrixDexomposition.Ctrl_DecompositeTransformMatrixView Ctrl_MatrixOutput;
        private System.Windows.Forms.TableLayoutPanel PanelProperties;
        private System.Windows.Forms.Label LabelRotationOrder;
        private System.Windows.Forms.ComboBox ComboRotationOrder;
        private System.Windows.Forms.Timer timer1;
        private RtCs.OpenGL.WinForms.GLControl GLViewer;
    }
}
