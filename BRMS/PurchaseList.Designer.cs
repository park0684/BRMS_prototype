
namespace BRMS
{
    partial class PurchaseList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dtpRegDateFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpRegDateTo = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bntPurchaseReg = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cBoxPurType = new System.Windows.Forms.ComboBox();
            this.bntSupplier = new System.Windows.Forms.Button();
            this.lblSupplierName = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panelDatagrid = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 165F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 165F));
            this.tableLayoutPanel1.Controls.Add(this.dtpRegDateFrom, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtpRegDateTo, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 53);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(440, 35);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // dtpRegDateFrom
            // 
            this.dtpRegDateFrom.CalendarFont = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dtpRegDateFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpRegDateFrom.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dtpRegDateFrom.Location = new System.Drawing.Point(83, 3);
            this.dtpRegDateFrom.Name = "dtpRegDateFrom";
            this.dtpRegDateFrom.Size = new System.Drawing.Size(159, 23);
            this.dtpRegDateFrom.TabIndex = 3;
            // 
            // dtpRegDateTo
            // 
            this.dtpRegDateTo.CalendarFont = new System.Drawing.Font("맑은 고딕", 9F);
            this.dtpRegDateTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpRegDateTo.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dtpRegDateTo.Location = new System.Drawing.Point(278, 3);
            this.dtpRegDateTo.Name = "dtpRegDateTo";
            this.dtpRegDateTo.Size = new System.Drawing.Size(159, 23);
            this.dtpRegDateTo.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(248, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 35);
            this.label1.TabIndex = 9;
            this.label1.Text = "~";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 35);
            this.label2.TabIndex = 10;
            this.label2.Text = "매입일";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bntPurchaseReg
            // 
            this.bntPurchaseReg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bntPurchaseReg.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.bntPurchaseReg.FlatAppearance.BorderSize = 0;
            this.bntPurchaseReg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntPurchaseReg.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.bntPurchaseReg.ForeColor = System.Drawing.Color.White;
            this.bntPurchaseReg.Location = new System.Drawing.Point(820, 134);
            this.bntPurchaseReg.Name = "bntPurchaseReg";
            this.bntPurchaseReg.Size = new System.Drawing.Size(100, 30);
            this.bntPurchaseReg.TabIndex = 16;
            this.bntPurchaseReg.Text = "매입 등록";
            this.bntPurchaseReg.UseVisualStyleBackColor = false;
            this.bntPurchaseReg.Click += new System.EventHandler(this.bntPurchaseReg_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel2.Controls.Add(this.cBoxPurType, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.bntSupplier, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblSupplierName, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label4, 2, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(500, 35);
            this.tableLayoutPanel2.TabIndex = 14;
            // 
            // cBoxPurType
            // 
            this.cBoxPurType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxPurType.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.cBoxPurType.FormattingEnabled = true;
            this.cBoxPurType.Location = new System.Drawing.Point(343, 3);
            this.cBoxPurType.Name = "cBoxPurType";
            this.cBoxPurType.Size = new System.Drawing.Size(154, 25);
            this.cBoxPurType.TabIndex = 18;
            // 
            // bntSupplier
            // 
            this.bntSupplier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(154)))), ((int)(((byte)(240)))));
            this.bntSupplier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bntSupplier.FlatAppearance.BorderSize = 0;
            this.bntSupplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntSupplier.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.bntSupplier.ForeColor = System.Drawing.Color.White;
            this.bntSupplier.Location = new System.Drawing.Point(3, 3);
            this.bntSupplier.Name = "bntSupplier";
            this.bntSupplier.Size = new System.Drawing.Size(74, 29);
            this.bntSupplier.TabIndex = 2;
            this.bntSupplier.Text = "공급사";
            this.bntSupplier.UseVisualStyleBackColor = false;
            this.bntSupplier.Click += new System.EventHandler(this.bntSupplier_Click);
            // 
            // lblSupplierName
            // 
            this.lblSupplierName.AutoSize = true;
            this.lblSupplierName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSupplierName.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.lblSupplierName.Location = new System.Drawing.Point(83, 0);
            this.lblSupplierName.Name = "lblSupplierName";
            this.lblSupplierName.Size = new System.Drawing.Size(174, 35);
            this.lblSupplierName.TabIndex = 3;
            this.lblSupplierName.Text = "공급사명";
            this.lblSupplierName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label4.Location = new System.Drawing.Point(263, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 35);
            this.label4.TabIndex = 19;
            this.label4.Text = "유형";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelDatagrid
            // 
            this.panelDatagrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDatagrid.Location = new System.Drawing.Point(12, 170);
            this.panelDatagrid.Name = "panelDatagrid";
            this.panelDatagrid.Size = new System.Drawing.Size(908, 447);
            this.panelDatagrid.TabIndex = 17;
            // 
            // PurchaseList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(932, 629);
            this.Controls.Add(this.panelDatagrid);
            this.Controls.Add(this.bntPurchaseReg);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PurchaseList";
            this.Text = "PurchaseList";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DateTimePicker dtpRegDateFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bntPurchaseReg;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button bntSupplier;
        private System.Windows.Forms.Label lblSupplierName;
        private System.Windows.Forms.Panel panelDatagrid;
        private System.Windows.Forms.DateTimePicker dtpRegDateTo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cBoxPurType;
    }
}