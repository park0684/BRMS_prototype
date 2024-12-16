
namespace BRMS
{
    partial class SalePayment
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tBoxpaymentUsd = new System.Windows.Forms.TextBox();
            this.tBoxPaymnetKrw = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.bntSave = new System.Windows.Forms.Button();
            this.bntCancle = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(252, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 25);
            this.label2.TabIndex = 34;
            this.label2.Text = "$";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(72, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 25);
            this.label1.TabIndex = 35;
            this.label1.Text = "\\";
            // 
            // tBoxpaymentUsd
            // 
            this.tBoxpaymentUsd.BackColor = System.Drawing.Color.Black;
            this.tBoxpaymentUsd.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.tBoxpaymentUsd.ForeColor = System.Drawing.Color.White;
            this.tBoxpaymentUsd.Location = new System.Drawing.Point(281, 5);
            this.tBoxpaymentUsd.Name = "tBoxpaymentUsd";
            this.tBoxpaymentUsd.Size = new System.Drawing.Size(132, 29);
            this.tBoxpaymentUsd.TabIndex = 33;
            // 
            // tBoxPaymnetKrw
            // 
            this.tBoxPaymnetKrw.BackColor = System.Drawing.Color.Black;
            this.tBoxPaymnetKrw.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.tBoxPaymnetKrw.ForeColor = System.Drawing.Color.White;
            this.tBoxPaymnetKrw.Location = new System.Drawing.Point(105, 5);
            this.tBoxPaymnetKrw.Name = "tBoxPaymnetKrw";
            this.tBoxPaymnetKrw.Size = new System.Drawing.Size(132, 29);
            this.tBoxPaymnetKrw.TabIndex = 32;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblName.Location = new System.Drawing.Point(12, 9);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(57, 25);
            this.lblName.TabIndex = 31;
            this.lblName.Text = "항 목";
            // 
            // bntSave
            // 
            this.bntSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bntSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(154)))), ((int)(((byte)(240)))));
            this.bntSave.FlatAppearance.BorderSize = 0;
            this.bntSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntSave.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bntSave.ForeColor = System.Drawing.Color.White;
            this.bntSave.Location = new System.Drawing.Point(257, 52);
            this.bntSave.Name = "bntSave";
            this.bntSave.Size = new System.Drawing.Size(74, 30);
            this.bntSave.TabIndex = 29;
            this.bntSave.Text = "저장";
            this.bntSave.UseVisualStyleBackColor = false;
            this.bntSave.Click += new System.EventHandler(this.bntSave_Click);
            // 
            // btnCancle
            // 
            this.bntCancle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bntCancle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(154)))), ((int)(((byte)(240)))));
            this.bntCancle.FlatAppearance.BorderSize = 0;
            this.bntCancle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntCancle.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bntCancle.ForeColor = System.Drawing.Color.White;
            this.bntCancle.Location = new System.Drawing.Point(337, 52);
            this.bntCancle.Name = "btnCancle";
            this.bntCancle.Size = new System.Drawing.Size(74, 30);
            this.bntCancle.TabIndex = 30;
            this.bntCancle.Text = "취소";
            this.bntCancle.UseVisualStyleBackColor = false;
            this.bntCancle.Click += new System.EventHandler(this.bntCancle_Click);
            // 
            // SalePayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 94);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tBoxpaymentUsd);
            this.Controls.Add(this.tBoxPaymnetKrw);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.bntSave);
            this.Controls.Add(this.bntCancle);
            this.Name = "SalePayment";
            this.Text = "SalePayment";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tBoxpaymentUsd;
        private System.Windows.Forms.TextBox tBoxPaymnetKrw;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button bntSave;
        private System.Windows.Forms.Button bntCancle;
    }
}