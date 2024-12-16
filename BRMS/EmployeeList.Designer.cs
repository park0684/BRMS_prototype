
namespace BRMS
{
    partial class EmployeeList
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
            this.bntAddEmployee = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmBoxStatus = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // panelDataGrid
            // 
            this.panelDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDataGrid.Location = new System.Drawing.Point(11, 53);
            this.panelDataGrid.Name = "panelDataGrid";
            this.panelDataGrid.Size = new System.Drawing.Size(778, 388);
            this.panelDataGrid.TabIndex = 8;
            // 
            // bntAddEmployee
            // 
            this.bntAddEmployee.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bntAddEmployee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(95)))), ((int)(((byte)(184)))));
            this.bntAddEmployee.FlatAppearance.BorderSize = 0;
            this.bntAddEmployee.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntAddEmployee.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.bntAddEmployee.ForeColor = System.Drawing.Color.White;
            this.bntAddEmployee.Location = new System.Drawing.Point(699, 9);
            this.bntAddEmployee.Name = "bntAddEmployee";
            this.bntAddEmployee.Size = new System.Drawing.Size(90, 30);
            this.bntAddEmployee.TabIndex = 7;
            this.bntAddEmployee.Text = "직원 추가";
            this.bntAddEmployee.UseVisualStyleBackColor = false;
            this.bntAddEmployee.Click += new System.EventHandler(this.bntAddEmployee_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label1.Location = new System.Drawing.Point(16, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "label1";
            // 
            // cmBoxStatus
            // 
            this.cmBoxStatus.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.cmBoxStatus.FormattingEnabled = true;
            this.cmBoxStatus.Location = new System.Drawing.Point(60, 19);
            this.cmBoxStatus.Name = "cmBoxStatus";
            this.cmBoxStatus.Size = new System.Drawing.Size(121, 23);
            this.cmBoxStatus.TabIndex = 10;
            // 
            // EmployeeList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cmBoxStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelDataGrid);
            this.Controls.Add(this.bntAddEmployee);
            this.Name = "EmployeeList";
            this.Text = "EmployeeList";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelDataGrid;
        private System.Windows.Forms.Button bntAddEmployee;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmBoxStatus;
    }
}