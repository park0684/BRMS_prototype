
namespace BRMS
{
    partial class ProductSearchBox
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
            this.panelDataGrid = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tBoxSearch = new System.Windows.Forms.TextBox();
            this.bntOk = new System.Windows.Forms.Button();
            this.bntSearch = new System.Windows.Forms.Button();
            this.bntClose = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelDataGrid
            // 
            this.panelDataGrid.Location = new System.Drawing.Point(12, 74);
            this.panelDataGrid.Name = "panelDataGrid";
            this.panelDataGrid.Size = new System.Drawing.Size(776, 359);
            this.panelDataGrid.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 35);
            this.label1.TabIndex = 1;
            this.label1.Text = "검색어";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 230F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tBoxSearch, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(300, 35);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // tBoxSearch
            // 
            this.tBoxSearch.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.tBoxSearch.Location = new System.Drawing.Point(73, 3);
            this.tBoxSearch.Name = "tBoxSearch";
            this.tBoxSearch.Size = new System.Drawing.Size(224, 29);
            this.tBoxSearch.TabIndex = 1;
            // 
            // bntOk
            // 
            this.bntOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(154)))), ((int)(((byte)(240)))));
            this.bntOk.FlatAppearance.BorderSize = 0;
            this.bntOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntOk.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.bntOk.Location = new System.Drawing.Point(604, 20);
            this.bntOk.Name = "bntOk";
            this.bntOk.Size = new System.Drawing.Size(89, 35);
            this.bntOk.TabIndex = 7;
            this.bntOk.Text = "선택";
            this.bntOk.UseVisualStyleBackColor = false;
            this.bntOk.Click += new System.EventHandler(this.bntOk_Click);
            // 
            // bntSearch
            // 
            this.bntSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(154)))), ((int)(((byte)(240)))));
            this.bntSearch.FlatAppearance.BorderSize = 0;
            this.bntSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntSearch.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.bntSearch.Location = new System.Drawing.Point(318, 18);
            this.bntSearch.Name = "bntSearch";
            this.bntSearch.Size = new System.Drawing.Size(89, 35);
            this.bntSearch.TabIndex = 6;
            this.bntSearch.Text = "검색";
            this.bntSearch.UseVisualStyleBackColor = false;
            this.bntSearch.Click += new System.EventHandler(this.bntSearch_Click);
            // 
            // bntClose
            // 
            this.bntClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(154)))), ((int)(((byte)(240)))));
            this.bntClose.FlatAppearance.BorderSize = 0;
            this.bntClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntClose.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.bntClose.Location = new System.Drawing.Point(699, 20);
            this.bntClose.Name = "bntClose";
            this.bntClose.Size = new System.Drawing.Size(89, 35);
            this.bntClose.TabIndex = 7;
            this.bntClose.Text = "닫기";
            this.bntClose.UseVisualStyleBackColor = false;
            this.bntClose.Click += new System.EventHandler(this.bntClose_Click);
            // 
            // ProductSearchBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.bntClose);
            this.Controls.Add(this.panelDataGrid);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.bntOk);
            this.Controls.Add(this.bntSearch);
            this.Name = "ProductSearchBox";
            this.Text = "ProductSearchBox";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelDataGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox tBoxSearch;
        private System.Windows.Forms.Button bntOk;
        private System.Windows.Forms.Button bntSearch;
        private System.Windows.Forms.Button bntClose;
    }
}