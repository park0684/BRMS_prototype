
namespace BRMS
{
    partial class NumericInputDialog
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
            this.lblName = new System.Windows.Forms.Label();
            this.tBoxNumber = new System.Windows.Forms.TextBox();
            this.bntSave = new System.Windows.Forms.Button();
            this.bntCancle = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 23);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(39, 15);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "label1";
            // 
            // tBoxNumber
            // 
            this.tBoxNumber.Location = new System.Drawing.Point(73, 19);
            this.tBoxNumber.Name = "tBoxNumber";
            this.tBoxNumber.Size = new System.Drawing.Size(133, 23);
            this.tBoxNumber.TabIndex = 1;
            // 
            // bntSave
            // 
            this.bntSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bntSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(154)))), ((int)(((byte)(240)))));
            this.bntSave.FlatAppearance.BorderSize = 0;
            this.bntSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntSave.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bntSave.ForeColor = System.Drawing.Color.White;
            this.bntSave.Location = new System.Drawing.Point(57, 70);
            this.bntSave.Name = "bntSave";
            this.bntSave.Size = new System.Drawing.Size(74, 30);
            this.bntSave.TabIndex = 8;
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
            this.bntCancle.Location = new System.Drawing.Point(137, 70);
            this.bntCancle.Name = "btnCancle";
            this.bntCancle.Size = new System.Drawing.Size(74, 30);
            this.bntCancle.TabIndex = 9;
            this.bntCancle.Text = "취소";
            this.bntCancle.UseVisualStyleBackColor = false;
            this.bntCancle.Click += new System.EventHandler(this.bntCancle_Click);
            // 
            // NumericInputDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(223, 110);
            this.Controls.Add(this.bntSave);
            this.Controls.Add(this.bntCancle);
            this.Controls.Add(this.tBoxNumber);
            this.Controls.Add(this.lblName);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "NumericInputDialog";
            this.Text = "NumericInputDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox tBoxNumber;
        private System.Windows.Forms.Button bntSave;
        private System.Windows.Forms.Button bntCancle;
    }
}