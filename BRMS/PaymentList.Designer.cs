
namespace BRMS
{
    partial class PaymentList
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bntSearch = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panelSupplierList = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.bntPaymentAdd = new System.Windows.Forms.Button();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.panelPaymentList = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCurrentBalance = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.bntSearch);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.panelSupplierList);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(243, 625);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // bntSearch
            // 
            this.bntSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(95)))), ((int)(((byte)(184)))));
            this.bntSearch.FlatAppearance.BorderSize = 0;
            this.bntSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntSearch.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.bntSearch.ForeColor = System.Drawing.Color.White;
            this.bntSearch.Location = new System.Drawing.Point(180, 19);
            this.bntSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bntSearch.Name = "bntSearch";
            this.bntSearch.Size = new System.Drawing.Size(57, 26);
            this.bntSearch.TabIndex = 3;
            this.bntSearch.Text = "검색";
            this.bntSearch.UseVisualStyleBackColor = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 22);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(168, 23);
            this.textBox1.TabIndex = 1;
            // 
            // panelSupplierList
            // 
            this.panelSupplierList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelSupplierList.Location = new System.Drawing.Point(6, 52);
            this.panelSupplierList.Name = "panelSupplierList";
            this.panelSupplierList.Size = new System.Drawing.Size(231, 567);
            this.panelSupplierList.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.bntPaymentAdd);
            this.groupBox2.Controls.Add(this.lblCurrentBalance);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.lblSupplier);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.dtpTo);
            this.groupBox2.Controls.Add(this.dtpFrom);
            this.groupBox2.Controls.Add(this.panelPaymentList);
            this.groupBox2.Location = new System.Drawing.Point(261, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(745, 625);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // bntPaymentAdd
            // 
            this.bntPaymentAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bntPaymentAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(95)))), ((int)(((byte)(184)))));
            this.bntPaymentAdd.FlatAppearance.BorderSize = 0;
            this.bntPaymentAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntPaymentAdd.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.bntPaymentAdd.ForeColor = System.Drawing.Color.White;
            this.bntPaymentAdd.Location = new System.Drawing.Point(663, 16);
            this.bntPaymentAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bntPaymentAdd.Name = "bntPaymentAdd";
            this.bntPaymentAdd.Size = new System.Drawing.Size(76, 26);
            this.bntPaymentAdd.TabIndex = 4;
            this.bntPaymentAdd.Text = "결제등록";
            this.bntPaymentAdd.UseVisualStyleBackColor = false;
            this.bntPaymentAdd.Click += new System.EventHandler(this.bntPaymentAdd_Click);
            // 
            // lblSupplier
            // 
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Location = new System.Drawing.Point(6, 22);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(39, 15);
            this.lblSupplier.TabIndex = 3;
            this.lblSupplier.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(172, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "조회 기간";
            // 
            // dtpTo
            // 
            this.dtpTo.Location = new System.Drawing.Point(384, 19);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(124, 23);
            this.dtpTo.TabIndex = 1;
            // 
            // dtpFrom
            // 
            this.dtpFrom.Location = new System.Drawing.Point(238, 19);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(124, 23);
            this.dtpFrom.TabIndex = 1;
            // 
            // panelPaymentList
            // 
            this.panelPaymentList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPaymentList.Location = new System.Drawing.Point(6, 79);
            this.panelPaymentList.Name = "panelPaymentList";
            this.panelPaymentList.Size = new System.Drawing.Size(733, 540);
            this.panelPaymentList.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "현재 잔액 : ";
            // 
            // lblCurrentBalance
            // 
            this.lblCurrentBalance.AutoSize = true;
            this.lblCurrentBalance.Location = new System.Drawing.Point(82, 52);
            this.lblCurrentBalance.Name = "lblCurrentBalance";
            this.lblCurrentBalance.Size = new System.Drawing.Size(55, 15);
            this.lblCurrentBalance.TabIndex = 3;
            this.lblCurrentBalance.Text = "잔액표시";
            // 
            // PaymentList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1018, 649);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "PaymentList";
            this.Text = "PaymentList";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panelSupplierList;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panelPaymentList;
        private System.Windows.Forms.Button bntSearch;
        private System.Windows.Forms.Button bntPaymentAdd;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label lblCurrentBalance;
        private System.Windows.Forms.Label label2;
    }
}