
namespace BRMS
{
    partial class CustomerOrderList
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
            this.bntOrderReg = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.bntCustomer = new System.Windows.Forms.Button();
            this.lblCustName = new System.Windows.Forms.Label();
            this.lblTag = new System.Windows.Forms.Label();
            this.lblCustEmail = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.cmBoxStatus = new System.Windows.Forms.ComboBox();
            this.cmBoxDateType = new System.Windows.Forms.ComboBox();
            this.dtpDateFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.panelDatagrid = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bntOrderReg
            // 
            this.bntOrderReg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bntOrderReg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(154)))), ((int)(((byte)(240)))));
            this.bntOrderReg.FlatAppearance.BorderSize = 0;
            this.bntOrderReg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntOrderReg.ForeColor = System.Drawing.Color.White;
            this.bntOrderReg.Location = new System.Drawing.Point(820, 63);
            this.bntOrderReg.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bntOrderReg.Name = "bntOrderReg";
            this.bntOrderReg.Size = new System.Drawing.Size(100, 35);
            this.bntOrderReg.TabIndex = 22;
            this.bntOrderReg.Text = "주문서 등록";
            this.bntOrderReg.UseVisualStyleBackColor = false;
            this.bntOrderReg.Click += new System.EventHandler(this.bntOrderReg_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel2.Controls.Add(this.bntCustomer, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblCustName, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblTag, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblCustEmail, 3, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 63);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(660, 35);
            this.tableLayoutPanel2.TabIndex = 21;
            // 
            // bntCustomer
            // 
            this.bntCustomer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(154)))), ((int)(((byte)(240)))));
            this.bntCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bntCustomer.FlatAppearance.BorderSize = 0;
            this.bntCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntCustomer.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.bntCustomer.ForeColor = System.Drawing.Color.White;
            this.bntCustomer.Location = new System.Drawing.Point(3, 4);
            this.bntCustomer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bntCustomer.Name = "bntCustomer";
            this.bntCustomer.Size = new System.Drawing.Size(74, 27);
            this.bntCustomer.TabIndex = 2;
            this.bntCustomer.Text = "회원검색";
            this.bntCustomer.UseVisualStyleBackColor = false;
            this.bntCustomer.Click += new System.EventHandler(this.bntCustomer_Click);
            // 
            // lblCustName
            // 
            this.lblCustName.AutoSize = true;
            this.lblCustName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCustName.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.lblCustName.Location = new System.Drawing.Point(83, 0);
            this.lblCustName.Name = "lblCustName";
            this.lblCustName.Size = new System.Drawing.Size(194, 35);
            this.lblCustName.TabIndex = 3;
            this.lblCustName.Text = "회원명";
            this.lblCustName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTag
            // 
            this.lblTag.AutoSize = true;
            this.lblTag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTag.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.lblTag.ForeColor = System.Drawing.Color.Black;
            this.lblTag.Location = new System.Drawing.Point(283, 0);
            this.lblTag.Name = "lblTag";
            this.lblTag.Size = new System.Drawing.Size(74, 35);
            this.lblTag.TabIndex = 4;
            this.lblTag.Text = "E메일 :";
            this.lblTag.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCustEmail
            // 
            this.lblCustEmail.AutoSize = true;
            this.lblCustEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCustEmail.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.lblCustEmail.Location = new System.Drawing.Point(363, 0);
            this.lblCustEmail.Name = "lblCustEmail";
            this.lblCustEmail.Size = new System.Drawing.Size(294, 35);
            this.lblCustEmail.TabIndex = 5;
            this.lblCustEmail.Text = "e-mail";
            this.lblCustEmail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 190F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmBoxStatus, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmBoxDateType, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtpDateFrom, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtpDateTo, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 20);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(660, 35);
            this.tableLayoutPanel1.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label2.Location = new System.Drawing.Point(513, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 35);
            this.label2.TabIndex = 12;
            this.label2.Text = "상태";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmBoxStatus
            // 
            this.cmBoxStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmBoxStatus.FormattingEnabled = true;
            this.cmBoxStatus.Location = new System.Drawing.Point(563, 3);
            this.cmBoxStatus.Name = "cmBoxStatus";
            this.cmBoxStatus.Size = new System.Drawing.Size(94, 23);
            this.cmBoxStatus.TabIndex = 13;
            // 
            // cmBoxDateType
            // 
            this.cmBoxDateType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmBoxDateType.FormattingEnabled = true;
            this.cmBoxDateType.Location = new System.Drawing.Point(3, 3);
            this.cmBoxDateType.Name = "cmBoxDateType";
            this.cmBoxDateType.Size = new System.Drawing.Size(94, 23);
            this.cmBoxDateType.TabIndex = 12;
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.CalendarFont = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dtpDateFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpDateFrom.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dtpDateFrom.Location = new System.Drawing.Point(103, 4);
            this.dtpDateFrom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.Size = new System.Drawing.Size(174, 23);
            this.dtpDateFrom.TabIndex = 3;
            // 
            // dtpDateTo
            // 
            this.dtpDateTo.CalendarFont = new System.Drawing.Font("맑은 고딕", 9F);
            this.dtpDateTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpDateTo.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dtpDateTo.Location = new System.Drawing.Point(303, 4);
            this.dtpDateTo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.Size = new System.Drawing.Size(184, 23);
            this.dtpDateTo.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(283, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 35);
            this.label1.TabIndex = 9;
            this.label1.Text = "~";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelDatagrid
            // 
            this.panelDatagrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDatagrid.Location = new System.Drawing.Point(12, 115);
            this.panelDatagrid.Name = "panelDatagrid";
            this.panelDatagrid.Size = new System.Drawing.Size(908, 493);
            this.panelDatagrid.TabIndex = 23;
            // 
            // CustomerOrderList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 629);
            this.Controls.Add(this.bntOrderReg);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panelDatagrid);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "CustomerOrderList";
            this.Text = "CustomerOrderList";
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bntOrderReg;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button bntCustomer;
        private System.Windows.Forms.Label lblCustName;
        private System.Windows.Forms.Label lblTag;
        private System.Windows.Forms.Label lblCustEmail;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmBoxStatus;
        private System.Windows.Forms.ComboBox cmBoxDateType;
        private System.Windows.Forms.DateTimePicker dtpDateFrom;
        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelDatagrid;
    }
}