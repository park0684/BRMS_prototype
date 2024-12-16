
namespace BRMS
{
    partial class SupplierSelectBox
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
            this.bntClose = new System.Windows.Forms.Button();
            this.bntSelect = new System.Windows.Forms.Button();
            this.panelDataGrid = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // bntClose
            // 
            this.bntClose.Location = new System.Drawing.Point(208, 410);
            this.bntClose.Name = "bntClose";
            this.bntClose.Size = new System.Drawing.Size(75, 30);
            this.bntClose.TabIndex = 4;
            this.bntClose.Text = "닫기";
            this.bntClose.UseVisualStyleBackColor = true;
            this.bntClose.Click += new System.EventHandler(this.bntClose_Click);
            // 
            // bntSelect
            // 
            this.bntSelect.Location = new System.Drawing.Point(127, 410);
            this.bntSelect.Name = "bntSelect";
            this.bntSelect.Size = new System.Drawing.Size(75, 30);
            this.bntSelect.TabIndex = 3;
            this.bntSelect.Text = "선택";
            this.bntSelect.UseVisualStyleBackColor = true;
            this.bntSelect.Click += new System.EventHandler(this.bntSelect_Click);
            // 
            // panelDataGrid
            // 
            this.panelDataGrid.Location = new System.Drawing.Point(12, 10);
            this.panelDataGrid.Name = "panelDataGrid";
            this.panelDataGrid.Size = new System.Drawing.Size(280, 384);
            this.panelDataGrid.TabIndex = 2;
            // 
            // SupplierSelectBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 450);
            this.Controls.Add(this.bntClose);
            this.Controls.Add(this.bntSelect);
            this.Controls.Add(this.panelDataGrid);
            this.Name = "SupplierSelectBox";
            this.Text = "SupplierSelectBox";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bntClose;
        private System.Windows.Forms.Button bntSelect;
        private System.Windows.Forms.Panel panelDataGrid;
    }
}