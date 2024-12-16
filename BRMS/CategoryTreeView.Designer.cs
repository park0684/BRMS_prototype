
namespace BRMS
{
    partial class CategoryTreeView
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
            this.bntOk = new System.Windows.Forms.Button();
            this.bntClose = new System.Windows.Forms.Button();
            this.treeViewCategory = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // bntOk
            // 
            this.bntOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(95)))), ((int)(((byte)(184)))));
            this.bntOk.FlatAppearance.BorderSize = 0;
            this.bntOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntOk.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.bntOk.ForeColor = System.Drawing.Color.White;
            this.bntOk.Location = new System.Drawing.Point(97, 377);
            this.bntOk.Name = "bntOk";
            this.bntOk.Size = new System.Drawing.Size(70, 30);
            this.bntOk.TabIndex = 11;
            this.bntOk.Text = "확인";
            this.bntOk.UseVisualStyleBackColor = false;
            this.bntOk.Click += new System.EventHandler(this.bntOk_Click);
            // 
            // bntClose
            // 
            this.bntClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(95)))), ((int)(((byte)(184)))));
            this.bntClose.FlatAppearance.BorderSize = 0;
            this.bntClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntClose.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.bntClose.ForeColor = System.Drawing.Color.White;
            this.bntClose.Location = new System.Drawing.Point(191, 377);
            this.bntClose.Name = "bntClose";
            this.bntClose.Size = new System.Drawing.Size(70, 30);
            this.bntClose.TabIndex = 11;
            this.bntClose.Text = "닫기";
            this.bntClose.UseVisualStyleBackColor = false;
            this.bntClose.Click += new System.EventHandler(this.bntClose_Click);
            // 
            // treeViewCategory
            // 
            this.treeViewCategory.Location = new System.Drawing.Point(12, 12);
            this.treeViewCategory.Name = "treeViewCategory";
            this.treeViewCategory.Size = new System.Drawing.Size(249, 359);
            this.treeViewCategory.TabIndex = 12;
            // 
            // CategoryTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 419);
            this.Controls.Add(this.treeViewCategory);
            this.Controls.Add(this.bntClose);
            this.Controls.Add(this.bntOk);
            this.Name = "CategoryTreeView";
            this.Text = "CategoryTreeView";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button bntOk;
        private System.Windows.Forms.Button bntClose;
        private System.Windows.Forms.TreeView treeViewCategory;
    }
}