
namespace BRMS
{
    partial class CustomerList
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
            this.bntCustomerAdd = new System.Windows.Forms.Button();
            this.groupBoxDatePikc = new System.Windows.Forms.GroupBox();
            this.cmBoxSaveDate = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmBoxSaleDate = new System.Windows.Forms.ComboBox();
            this.dtpSaveDateFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpSaveDateTo = new System.Windows.Forms.DateTimePicker();
            this.dtpSaleDateFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpSaleDateTo = new System.Windows.Forms.DateTimePicker();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmBoxStatus = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tBoxSearchWord = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelDataGrid = new System.Windows.Forms.Panel();
            this.checkBoxSaleDate = new System.Windows.Forms.CheckBox();
            this.checkBoxSaveDate = new System.Windows.Forms.CheckBox();
            this.groupBoxDatePikc.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bntCustomerAdd
            // 
            this.bntCustomerAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bntCustomerAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(95)))), ((int)(((byte)(184)))));
            this.bntCustomerAdd.FlatAppearance.BorderSize = 0;
            this.bntCustomerAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntCustomerAdd.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.bntCustomerAdd.ForeColor = System.Drawing.Color.White;
            this.bntCustomerAdd.Location = new System.Drawing.Point(906, 99);
            this.bntCustomerAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bntCustomerAdd.Name = "bntCustomerAdd";
            this.bntCustomerAdd.Size = new System.Drawing.Size(100, 25);
            this.bntCustomerAdd.TabIndex = 22;
            this.bntCustomerAdd.Text = "회원 등록";
            this.bntCustomerAdd.UseVisualStyleBackColor = false;
            this.bntCustomerAdd.Click += new System.EventHandler(this.bntCustomerAdd_Click);
            // 
            // groupBoxDatePikc
            // 
            this.groupBoxDatePikc.Controls.Add(this.checkBoxSaleDate);
            this.groupBoxDatePikc.Controls.Add(this.cmBoxSaveDate);
            this.groupBoxDatePikc.Controls.Add(this.checkBoxSaveDate);
            this.groupBoxDatePikc.Controls.Add(this.label2);
            this.groupBoxDatePikc.Controls.Add(this.label4);
            this.groupBoxDatePikc.Controls.Add(this.cmBoxSaleDate);
            this.groupBoxDatePikc.Controls.Add(this.dtpSaveDateFrom);
            this.groupBoxDatePikc.Controls.Add(this.dtpSaveDateTo);
            this.groupBoxDatePikc.Controls.Add(this.dtpSaleDateFrom);
            this.groupBoxDatePikc.Controls.Add(this.dtpSaleDateTo);
            this.groupBoxDatePikc.Location = new System.Drawing.Point(254, 12);
            this.groupBoxDatePikc.Name = "groupBoxDatePikc";
            this.groupBoxDatePikc.Size = new System.Drawing.Size(529, 82);
            this.groupBoxDatePikc.TabIndex = 20;
            this.groupBoxDatePikc.TabStop = false;
            this.groupBoxDatePikc.Text = "조회 기간";
            // 
            // cmBoxSaveDate
            // 
            this.cmBoxSaveDate.FormattingEnabled = true;
            this.cmBoxSaveDate.Location = new System.Drawing.Point(29, 24);
            this.cmBoxSaveDate.Name = "cmBoxSaveDate";
            this.cmBoxSaveDate.Size = new System.Drawing.Size(119, 23);
            this.cmBoxSaveDate.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(324, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 15);
            this.label2.TabIndex = 13;
            this.label2.Text = "~";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(324, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "~";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmBoxSaleDate
            // 
            this.cmBoxSaleDate.FormattingEnabled = true;
            this.cmBoxSaleDate.Location = new System.Drawing.Point(29, 51);
            this.cmBoxSaleDate.Name = "cmBoxSaleDate";
            this.cmBoxSaleDate.Size = new System.Drawing.Size(119, 23);
            this.cmBoxSaleDate.TabIndex = 12;
            // 
            // dtpSaveDateFrom
            // 
            this.dtpSaveDateFrom.CalendarFont = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dtpSaveDateFrom.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dtpSaveDateFrom.Location = new System.Drawing.Point(154, 24);
            this.dtpSaveDateFrom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpSaveDateFrom.Name = "dtpSaveDateFrom";
            this.dtpSaveDateFrom.Size = new System.Drawing.Size(164, 23);
            this.dtpSaveDateFrom.TabIndex = 3;
            // 
            // dtpSaveDateTo
            // 
            this.dtpSaveDateTo.CalendarFont = new System.Drawing.Font("맑은 고딕", 9F);
            this.dtpSaveDateTo.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dtpSaveDateTo.Location = new System.Drawing.Point(349, 24);
            this.dtpSaveDateTo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpSaveDateTo.Name = "dtpSaveDateTo";
            this.dtpSaveDateTo.Size = new System.Drawing.Size(164, 23);
            this.dtpSaveDateTo.TabIndex = 4;
            // 
            // dtpSaleDateFrom
            // 
            this.dtpSaleDateFrom.CalendarFont = new System.Drawing.Font("맑은 고딕", 9F);
            this.dtpSaleDateFrom.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dtpSaleDateFrom.Location = new System.Drawing.Point(154, 51);
            this.dtpSaleDateFrom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpSaleDateFrom.Name = "dtpSaleDateFrom";
            this.dtpSaleDateFrom.Size = new System.Drawing.Size(164, 23);
            this.dtpSaleDateFrom.TabIndex = 5;
            // 
            // dtpSaleDateTo
            // 
            this.dtpSaleDateTo.CalendarFont = new System.Drawing.Font("맑은 고딕", 9F);
            this.dtpSaleDateTo.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dtpSaleDateTo.Location = new System.Drawing.Point(349, 52);
            this.dtpSaleDateTo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpSaleDateTo.Name = "dtpSaleDateTo";
            this.dtpSaleDateTo.Size = new System.Drawing.Size(164, 23);
            this.dtpSaleDateTo.TabIndex = 6;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.lblStatus.Location = new System.Drawing.Point(21, 27);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(31, 15);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "상태";
            // 
            // cmBoxStatus
            // 
            this.cmBoxStatus.FormattingEnabled = true;
            this.cmBoxStatus.Location = new System.Drawing.Point(64, 21);
            this.cmBoxStatus.Name = "cmBoxStatus";
            this.cmBoxStatus.Size = new System.Drawing.Size(77, 23);
            this.cmBoxStatus.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label1.Location = new System.Drawing.Point(9, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "검색어";
            // 
            // tBoxSearchWord
            // 
            this.tBoxSearchWord.Location = new System.Drawing.Point(64, 51);
            this.tBoxSearchWord.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tBoxSearchWord.Name = "tBoxSearchWord";
            this.tBoxSearchWord.Size = new System.Drawing.Size(160, 23);
            this.tBoxSearchWord.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblStatus);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmBoxStatus);
            this.groupBox1.Controls.Add(this.tBoxSearchWord);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(236, 82);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            // 
            // panelDataGrid
            // 
            this.panelDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDataGrid.Location = new System.Drawing.Point(12, 132);
            this.panelDataGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelDataGrid.Name = "panelDataGrid";
            this.panelDataGrid.Size = new System.Drawing.Size(994, 477);
            this.panelDataGrid.TabIndex = 17;
            // 
            // checkBoxSaleDate
            // 
            this.checkBoxSaleDate.AutoSize = true;
            this.checkBoxSaleDate.Location = new System.Drawing.Point(7, 55);
            this.checkBoxSaleDate.Name = "checkBoxSaleDate";
            this.checkBoxSaleDate.Size = new System.Drawing.Size(15, 14);
            this.checkBoxSaleDate.TabIndex = 23;
            this.checkBoxSaleDate.UseVisualStyleBackColor = true;
            // 
            // checkBoxSaveDate
            // 
            this.checkBoxSaveDate.AutoSize = true;
            this.checkBoxSaveDate.Location = new System.Drawing.Point(7, 28);
            this.checkBoxSaveDate.Name = "checkBoxSaveDate";
            this.checkBoxSaveDate.Size = new System.Drawing.Size(15, 14);
            this.checkBoxSaveDate.TabIndex = 24;
            this.checkBoxSaveDate.UseVisualStyleBackColor = true;
            // 
            // CustomerList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 622);
            this.Controls.Add(this.bntCustomerAdd);
            this.Controls.Add(this.groupBoxDatePikc);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panelDataGrid);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "CustomerList";
            this.Text = "CustomerList";
            this.groupBoxDatePikc.ResumeLayout(false);
            this.groupBoxDatePikc.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bntCustomerAdd;
        private System.Windows.Forms.GroupBox groupBoxDatePikc;
        private System.Windows.Forms.ComboBox cmBoxSaveDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmBoxSaleDate;
        private System.Windows.Forms.DateTimePicker dtpSaveDateFrom;
        private System.Windows.Forms.DateTimePicker dtpSaveDateTo;
        private System.Windows.Forms.DateTimePicker dtpSaleDateFrom;
        private System.Windows.Forms.DateTimePicker dtpSaleDateTo;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmBoxStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tBoxSearchWord;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panelDataGrid;
        private System.Windows.Forms.CheckBox checkBoxSaleDate;
        private System.Windows.Forms.CheckBox checkBoxSaveDate;
    }
}