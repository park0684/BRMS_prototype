
namespace BRMS
{
    partial class DateTimePickerBox
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
            this.dtpBox = new System.Windows.Forms.DateTimePicker();
            this.cBoxHour = new System.Windows.Forms.ComboBox();
            this.cBoxMinute = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bntOk = new System.Windows.Forms.Button();
            this.bntClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dtpBox
            // 
            this.dtpBox.CalendarFont = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dtpBox.CustomFormat = "yyyy-MM-dd";
            this.dtpBox.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dtpBox.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBox.Location = new System.Drawing.Point(12, 12);
            this.dtpBox.Name = "dtpBox";
            this.dtpBox.Size = new System.Drawing.Size(123, 23);
            this.dtpBox.TabIndex = 4;
            // 
            // cBoxHour
            // 
            this.cBoxHour.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.cBoxHour.FormattingEnabled = true;
            this.cBoxHour.Location = new System.Drawing.Point(156, 12);
            this.cBoxHour.Name = "cBoxHour";
            this.cBoxHour.Size = new System.Drawing.Size(68, 23);
            this.cBoxHour.TabIndex = 5;
            // 
            // cBoxMinute
            // 
            this.cBoxMinute.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.cBoxMinute.FormattingEnabled = true;
            this.cBoxMinute.Location = new System.Drawing.Point(259, 12);
            this.cBoxMinute.Name = "cBoxMinute";
            this.cBoxMinute.Size = new System.Drawing.Size(68, 23);
            this.cBoxMinute.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.label1.Location = new System.Drawing.Point(230, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 19);
            this.label1.TabIndex = 6;
            this.label1.Text = "시";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.label2.Location = new System.Drawing.Point(333, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 19);
            this.label2.TabIndex = 6;
            this.label2.Text = "분";
            // 
            // bntOk
            // 
            this.bntOk.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(154)))), ((int)(((byte)(240)))));
            this.bntOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntOk.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.bntOk.Location = new System.Drawing.Point(230, 55);
            this.bntOk.Name = "bntOk";
            this.bntOk.Size = new System.Drawing.Size(60, 25);
            this.bntOk.TabIndex = 46;
            this.bntOk.Text = "확인";
            this.bntOk.UseVisualStyleBackColor = true;
            this.bntOk.Click += new System.EventHandler(this.bntOk_Click);
            // 
            // bntClose
            // 
            this.bntClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(154)))), ((int)(((byte)(240)))));
            this.bntClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntClose.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.bntClose.Location = new System.Drawing.Point(296, 55);
            this.bntClose.Name = "bntClose";
            this.bntClose.Size = new System.Drawing.Size(60, 25);
            this.bntClose.TabIndex = 46;
            this.bntClose.Text = "닫기";
            this.bntClose.UseVisualStyleBackColor = true;
            this.bntClose.Click += new System.EventHandler(this.bntClose_Click);
            // 
            // DateTimePickerBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 92);
            this.Controls.Add(this.bntClose);
            this.Controls.Add(this.bntOk);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cBoxMinute);
            this.Controls.Add(this.cBoxHour);
            this.Controls.Add(this.dtpBox);
            this.Name = "DateTimePickerBox";
            this.Text = "DateTimePickerBox";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpBox;
        private System.Windows.Forms.ComboBox cBoxHour;
        private System.Windows.Forms.ComboBox cBoxMinute;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bntOk;
        private System.Windows.Forms.Button bntClose;
    }
}