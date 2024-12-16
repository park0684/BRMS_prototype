
namespace BRMS
{
    partial class SupplierList
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
            this.tBoxSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bntAddSupplier = new System.Windows.Forms.Button();
            this.panelDataGrid = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // tBoxSearch
            // 
            this.tBoxSearch.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.tBoxSearch.Location = new System.Drawing.Point(61, 6);
            this.tBoxSearch.Name = "tBoxSearch";
            this.tBoxSearch.Size = new System.Drawing.Size(210, 23);
            this.tBoxSearch.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "검색어";
            // 
            // bntAddSupplier
            // 
            this.bntAddSupplier.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bntAddSupplier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(95)))), ((int)(((byte)(184)))));
            this.bntAddSupplier.FlatAppearance.BorderSize = 0;
            this.bntAddSupplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntAddSupplier.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.bntAddSupplier.ForeColor = System.Drawing.Color.White;
            this.bntAddSupplier.Location = new System.Drawing.Point(698, 6);
            this.bntAddSupplier.Name = "bntAddSupplier";
            this.bntAddSupplier.Size = new System.Drawing.Size(90, 30);
            this.bntAddSupplier.TabIndex = 5;
            this.bntAddSupplier.Text = "공급사 추가";
            this.bntAddSupplier.UseVisualStyleBackColor = false;
            this.bntAddSupplier.Click += new System.EventHandler(this.bntAddSupplier_Click);
            // 
            // panelDataGrid
            // 
            this.panelDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDataGrid.Location = new System.Drawing.Point(10, 50);
            this.panelDataGrid.Name = "panelDataGrid";
            this.panelDataGrid.Size = new System.Drawing.Size(778, 388);
            this.panelDataGrid.TabIndex = 6;
            // 
            // SupplierList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelDataGrid);
            this.Controls.Add(this.bntAddSupplier);
            this.Controls.Add(this.tBoxSearch);
            this.Controls.Add(this.label1);
            this.Name = "SupplierList";
            this.Text = "SupplierList";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tBoxSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bntAddSupplier;
        private System.Windows.Forms.Panel panelDataGrid;
    }
}