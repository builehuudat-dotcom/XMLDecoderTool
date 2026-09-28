namespace XMLDecoderTool
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            txtFilePath = new TextBox();
            label2 = new Label();
            btnChonFile = new Button();
            btnGiaiMa = new Button();
            label3 = new Label();
            cboLoaiXML = new ComboBox();
            dgvData = new DataGridView();
            btnLuu = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvData).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(455, 19);
            label1.Name = "label1";
            label1.Size = new Size(241, 31);
            label1.TabIndex = 0;
            label1.Text = "Công cụ mã hóa XML";
            // 
            // txtFilePath
            // 
            txtFilePath.Location = new Point(178, 75);
            txtFilePath.Name = "txtFilePath";
            txtFilePath.ReadOnly = true;
            txtFilePath.Size = new Size(360, 27);
            txtFilePath.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(82, 78);
            label2.Name = "label2";
            label2.Size = new Size(68, 20);
            label2.TabIndex = 2;
            label2.Text = "File XML:";
            // 
            // btnChonFile
            // 
            btnChonFile.Location = new Point(589, 74);
            btnChonFile.Name = "btnChonFile";
            btnChonFile.Size = new Size(108, 29);
            btnChonFile.TabIndex = 3;
            btnChonFile.Text = "Chọn file";
            btnChonFile.UseVisualStyleBackColor = true;
            btnChonFile.Click += btnChonFile_Click;
            // 
            // btnGiaiMa
            // 
            btnGiaiMa.Location = new Point(324, 147);
            btnGiaiMa.Name = "btnGiaiMa";
            btnGiaiMa.Size = new Size(253, 34);
            btnGiaiMa.TabIndex = 4;
            btnGiaiMa.Text = "Giải mã và đọc dữ liệu";
            btnGiaiMa.UseVisualStyleBackColor = true;
            btnGiaiMa.Click += btnGiaiMa_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(82, 154);
            label3.Name = "label3";
            label3.Size = new Size(90, 20);
            label3.TabIndex = 5;
            label3.Text = "Loại dữ liệu:";
            // 
            // cboLoaiXML
            // 
            cboLoaiXML.FormattingEnabled = true;
            cboLoaiXML.Location = new Point(178, 151);
            cboLoaiXML.Name = "cboLoaiXML";
            cboLoaiXML.Size = new Size(125, 28);
            cboLoaiXML.TabIndex = 6;
            // 
            // dgvData
            // 
            dgvData.AllowUserToAddRows = false;
            dgvData.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvData.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvData.Location = new Point(85, 201);
            dgvData.Name = "dgvData";
            dgvData.ReadOnly = true;
            dgvData.RowHeadersWidth = 51;
            dgvData.Size = new Size(1032, 440);
            dgvData.TabIndex = 7;
            // 
            // btnLuu
            // 
            btnLuu.Location = new Point(589, 147);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(94, 34);
            btnLuu.TabIndex = 8;
            btnLuu.Text = "Lưu";
            btnLuu.UseVisualStyleBackColor = true;
            btnLuu.Click += btnLuu_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1182, 653);
            Controls.Add(btnLuu);
            Controls.Add(dgvData);
            Controls.Add(cboLoaiXML);
            Controls.Add(label3);
            Controls.Add(btnGiaiMa);
            Controls.Add(btnChonFile);
            Controls.Add(label2);
            Controls.Add(txtFilePath);
            Controls.Add(label1);
            Name = "Form1";
            Text = "XML Data Decoder";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvData).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtFilePath;
        private Label label2;
        private Button btnChonFile;
        private Button btnGiaiMa;
        private Label label3;
        private ComboBox cboLoaiXML;
        private DataGridView dgvData;
        private Button btnLuu;
    }
}
