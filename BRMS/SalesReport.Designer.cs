
namespace BRMS
{
    partial class SalesReport
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
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmBoxSearchType = new System.Windows.Forms.ComboBox();
            this.dtpDateFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.panelDatagrid = new System.Windows.Forms.Panel();
            this.chkDayli = new System.Windows.Forms.CheckBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.bntCategory = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 190F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmBoxSearchType, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtpDateFrom, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtpDateTo, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 54);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(660, 35);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 35);
            this.label4.TabIndex = 13;
            this.label4.Text = "판매일 : ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label2.Location = new System.Drawing.Point(483, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 35);
            this.label2.TabIndex = 12;
            this.label2.Text = "구분";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmBoxSearchType
            // 
            this.cmBoxSearchType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmBoxSearchType.FormattingEnabled = true;
            this.cmBoxSearchType.Location = new System.Drawing.Point(533, 3);
            this.cmBoxSearchType.Name = "cmBoxSearchType";
            this.cmBoxSearchType.Size = new System.Drawing.Size(124, 23);
            this.cmBoxSearchType.TabIndex = 13;
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.CalendarFont = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dtpDateFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpDateFrom.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dtpDateFrom.Location = new System.Drawing.Point(73, 4);
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
            this.dtpDateTo.Location = new System.Drawing.Point(273, 4);
            this.dtpDateTo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.Size = new System.Drawing.Size(184, 23);
            this.dtpDateTo.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(253, 0);
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
            this.panelDatagrid.Location = new System.Drawing.Point(12, 104);
            this.panelDatagrid.Name = "panelDatagrid";
            this.panelDatagrid.Size = new System.Drawing.Size(908, 513);
            this.panelDatagrid.TabIndex = 12;
            // 
            // chkDayli
            // 
            this.chkDayli.AutoSize = true;
            this.chkDayli.Location = new System.Drawing.Point(688, 63);
            this.chkDayli.Name = "chkDayli";
            this.chkDayli.Size = new System.Drawing.Size(90, 19);
            this.chkDayli.TabIndex = 14;
            this.chkDayli.Text = "일자별 조회";
            this.chkDayli.UseVisualStyleBackColor = true;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.lblCategory.Location = new System.Drawing.Point(63, 16);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(45, 19);
            this.lblCategory.TabIndex = 16;
            this.lblCategory.Text = "label1";
            // 
            // bntCategory
            // 
            this.bntCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(95)))), ((int)(((byte)(184)))));
            this.bntCategory.FlatAppearance.BorderSize = 0;
            this.bntCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntCategory.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.bntCategory.ForeColor = System.Drawing.Color.White;
            this.bntCategory.Location = new System.Drawing.Point(12, 13);
            this.bntCategory.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bntCategory.Name = "bntCategory";
            this.bntCategory.Size = new System.Drawing.Size(45, 26);
            this.bntCategory.TabIndex = 15;
            this.bntCategory.Text = "분류";
            this.bntCategory.UseVisualStyleBackColor = false;
            this.bntCategory.Click += new System.EventHandler(this.bntCategory_Click);
            // 
            // SalesReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 629);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.bntCategory);
            this.Controls.Add(this.chkDayli);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panelDatagrid);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "SalesReport";
            this.Text = "SalesReport";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmBoxSearchType;
        private System.Windows.Forms.DateTimePicker dtpDateFrom;
        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelDatagrid;
        private System.Windows.Forms.CheckBox chkDayli;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Button bntCategory;
    }
}