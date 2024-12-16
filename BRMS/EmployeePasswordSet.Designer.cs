
namespace BRMS
{
    partial class EmployeePasswordSet
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
            this.tBoxPassword = new System.Windows.Forms.TextBox();
            this.tBoxCheck = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bntSave = new System.Windows.Forms.Button();
            this.bntCancle = new System.Windows.Forms.Button();
            this.chkBoxPassword = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // tBoxPassword
            // 
            this.tBoxPassword.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.tBoxPassword.Location = new System.Drawing.Point(77, 32);
            this.tBoxPassword.Name = "tBoxPassword";
            this.tBoxPassword.Size = new System.Drawing.Size(174, 23);
            this.tBoxPassword.TabIndex = 0;
            // 
            // tBoxCheck
            // 
            this.tBoxCheck.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.tBoxCheck.Location = new System.Drawing.Point(77, 58);
            this.tBoxCheck.Name = "tBoxCheck";
            this.tBoxCheck.Size = new System.Drawing.Size(174, 23);
            this.tBoxCheck.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label1.Location = new System.Drawing.Point(24, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "새 암호";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label2.Location = new System.Drawing.Point(12, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "암호 확인";
            // 
            // bntSave
            // 
            this.bntSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bntSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(154)))), ((int)(((byte)(240)))));
            this.bntSave.FlatAppearance.BorderSize = 0;
            this.bntSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntSave.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bntSave.ForeColor = System.Drawing.Color.White;
            this.bntSave.Location = new System.Drawing.Point(94, 119);
            this.bntSave.Name = "bntSave";
            this.bntSave.Size = new System.Drawing.Size(75, 30);
            this.bntSave.TabIndex = 3;
            this.bntSave.Text = "저장";
            this.bntSave.UseVisualStyleBackColor = false;
            this.bntSave.Click += new System.EventHandler(this.bntSave_Click);
            // 
            // bntCancle
            // 
            this.bntCancle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bntCancle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(154)))), ((int)(((byte)(240)))));
            this.bntCancle.FlatAppearance.BorderSize = 0;
            this.bntCancle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntCancle.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bntCancle.ForeColor = System.Drawing.Color.White;
            this.bntCancle.Location = new System.Drawing.Point(175, 119);
            this.bntCancle.Name = "bntCancle";
            this.bntCancle.Size = new System.Drawing.Size(75, 30);
            this.bntCancle.TabIndex = 4;
            this.bntCancle.Text = "취소";
            this.bntCancle.UseVisualStyleBackColor = false;
            this.bntCancle.Click += new System.EventHandler(this.bntCancle_Click);
            // 
            // chkBoxPassword
            // 
            this.chkBoxPassword.AutoSize = true;
            this.chkBoxPassword.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.chkBoxPassword.Location = new System.Drawing.Point(77, 87);
            this.chkBoxPassword.Name = "chkBoxPassword";
            this.chkBoxPassword.Size = new System.Drawing.Size(90, 19);
            this.chkBoxPassword.TabIndex = 2;
            this.chkBoxPassword.Text = "암호 보이기";
            this.chkBoxPassword.UseVisualStyleBackColor = true;
            // 
            // EmployeePasswordSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 161);
            this.Controls.Add(this.chkBoxPassword);
            this.Controls.Add(this.bntSave);
            this.Controls.Add(this.bntCancle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tBoxCheck);
            this.Controls.Add(this.tBoxPassword);
            this.Name = "EmployeePasswordSet";
            this.Text = "EmployeePasswordSet";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tBoxPassword;
        private System.Windows.Forms.TextBox tBoxCheck;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bntSave;
        private System.Windows.Forms.Button bntCancle;
        private System.Windows.Forms.CheckBox chkBoxPassword;
    }
}