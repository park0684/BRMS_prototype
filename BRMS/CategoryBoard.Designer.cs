
namespace BRMS
{
    partial class CategoryBoard
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
            this.bntTopCategoryModify = new System.Windows.Forms.Button();
            this.bntTopCategoryAdd = new System.Windows.Forms.Button();
            this.bntOk = new System.Windows.Forms.Button();
            this.bntClose = new System.Windows.Forms.Button();
            this.groupBoxTopCategory = new System.Windows.Forms.GroupBox();
            this.panelCategoryTop = new System.Windows.Forms.Panel();
            this.groupBoxMidCategory = new System.Windows.Forms.GroupBox();
            this.panelCategoryMid = new System.Windows.Forms.Panel();
            this.bntMidCategoryAdd = new System.Windows.Forms.Button();
            this.bntMidCategoryModify = new System.Windows.Forms.Button();
            this.groupBoxBotCategory = new System.Windows.Forms.GroupBox();
            this.panelCategoryBot = new System.Windows.Forms.Panel();
            this.bntBotCategoryAdd = new System.Windows.Forms.Button();
            this.bntBotCategoryModify = new System.Windows.Forms.Button();
            this.groupBoxTopCategory.SuspendLayout();
            this.groupBoxMidCategory.SuspendLayout();
            this.groupBoxBotCategory.SuspendLayout();
            this.SuspendLayout();
            // 
            // bntTopCategoryModify
            // 
            this.bntTopCategoryModify.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(95)))), ((int)(((byte)(184)))));
            this.bntTopCategoryModify.FlatAppearance.BorderSize = 0;
            this.bntTopCategoryModify.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntTopCategoryModify.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.bntTopCategoryModify.ForeColor = System.Drawing.Color.White;
            this.bntTopCategoryModify.Location = new System.Drawing.Point(10, 340);
            this.bntTopCategoryModify.Name = "bntTopCategoryModify";
            this.bntTopCategoryModify.Size = new System.Drawing.Size(70, 30);
            this.bntTopCategoryModify.TabIndex = 1;
            this.bntTopCategoryModify.Text = "수정";
            this.bntTopCategoryModify.UseVisualStyleBackColor = false;
            this.bntTopCategoryModify.Click += new System.EventHandler(this.bntTopCategoryModify_Click);
            // 
            // bntTopCategoryAdd
            // 
            this.bntTopCategoryAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(95)))), ((int)(((byte)(184)))));
            this.bntTopCategoryAdd.FlatAppearance.BorderSize = 0;
            this.bntTopCategoryAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntTopCategoryAdd.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.bntTopCategoryAdd.ForeColor = System.Drawing.Color.White;
            this.bntTopCategoryAdd.Location = new System.Drawing.Point(90, 340);
            this.bntTopCategoryAdd.Name = "bntTopCategoryAdd";
            this.bntTopCategoryAdd.Size = new System.Drawing.Size(70, 30);
            this.bntTopCategoryAdd.TabIndex = 1;
            this.bntTopCategoryAdd.Text = "추가";
            this.bntTopCategoryAdd.UseVisualStyleBackColor = false;
            this.bntTopCategoryAdd.Click += new System.EventHandler(this.bntTopCategoryAdd_Click);
            // 
            // bntOk
            // 
            this.bntOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(95)))), ((int)(((byte)(184)))));
            this.bntOk.FlatAppearance.BorderSize = 0;
            this.bntOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntOk.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.bntOk.ForeColor = System.Drawing.Color.White;
            this.bntOk.Location = new System.Drawing.Point(694, 419);
            this.bntOk.Name = "bntOk";
            this.bntOk.Size = new System.Drawing.Size(70, 30);
            this.bntOk.TabIndex = 5;
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
            this.bntClose.Location = new System.Drawing.Point(770, 419);
            this.bntClose.Name = "bntClose";
            this.bntClose.Size = new System.Drawing.Size(70, 30);
            this.bntClose.TabIndex = 4;
            this.bntClose.Text = "닫기";
            this.bntClose.UseVisualStyleBackColor = false;
            this.bntClose.Click += new System.EventHandler(this.bntClose_Click);
            // 
            // groupBoxTopCategory
            // 
            this.groupBoxTopCategory.Controls.Add(this.panelCategoryTop);
            this.groupBoxTopCategory.Controls.Add(this.bntTopCategoryAdd);
            this.groupBoxTopCategory.Controls.Add(this.bntTopCategoryModify);
            this.groupBoxTopCategory.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.groupBoxTopCategory.Location = new System.Drawing.Point(10, 27);
            this.groupBoxTopCategory.Name = "groupBoxTopCategory";
            this.groupBoxTopCategory.Size = new System.Drawing.Size(270, 380);
            this.groupBoxTopCategory.TabIndex = 6;
            this.groupBoxTopCategory.TabStop = false;
            this.groupBoxTopCategory.Text = "대분류";
            // 
            // panelCategoryTop
            // 
            this.panelCategoryTop.Location = new System.Drawing.Point(10, 20);
            this.panelCategoryTop.Name = "panelCategoryTop";
            this.panelCategoryTop.Size = new System.Drawing.Size(250, 300);
            this.panelCategoryTop.TabIndex = 3;
            // 
            // groupBoxMidCategory
            // 
            this.groupBoxMidCategory.Controls.Add(this.panelCategoryMid);
            this.groupBoxMidCategory.Controls.Add(this.bntMidCategoryAdd);
            this.groupBoxMidCategory.Controls.Add(this.bntMidCategoryModify);
            this.groupBoxMidCategory.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.groupBoxMidCategory.Location = new System.Drawing.Point(290, 27);
            this.groupBoxMidCategory.Name = "groupBoxMidCategory";
            this.groupBoxMidCategory.Size = new System.Drawing.Size(270, 380);
            this.groupBoxMidCategory.TabIndex = 9;
            this.groupBoxMidCategory.TabStop = false;
            this.groupBoxMidCategory.Text = "중분류";
            // 
            // panelCategoryMid
            // 
            this.panelCategoryMid.Location = new System.Drawing.Point(10, 20);
            this.panelCategoryMid.Name = "panelCategoryMid";
            this.panelCategoryMid.Size = new System.Drawing.Size(250, 300);
            this.panelCategoryMid.TabIndex = 10;
            // 
            // bntMidCategoryAdd
            // 
            this.bntMidCategoryAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(95)))), ((int)(((byte)(184)))));
            this.bntMidCategoryAdd.FlatAppearance.BorderSize = 0;
            this.bntMidCategoryAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntMidCategoryAdd.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.bntMidCategoryAdd.ForeColor = System.Drawing.Color.White;
            this.bntMidCategoryAdd.Location = new System.Drawing.Point(90, 340);
            this.bntMidCategoryAdd.Name = "bntMidCategoryAdd";
            this.bntMidCategoryAdd.Size = new System.Drawing.Size(70, 30);
            this.bntMidCategoryAdd.TabIndex = 8;
            this.bntMidCategoryAdd.Text = "추가";
            this.bntMidCategoryAdd.UseVisualStyleBackColor = false;
            this.bntMidCategoryAdd.Click += new System.EventHandler(this.bntMidCategoryAdd_Click);
            // 
            // bntMidCategoryModify
            // 
            this.bntMidCategoryModify.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(95)))), ((int)(((byte)(184)))));
            this.bntMidCategoryModify.FlatAppearance.BorderSize = 0;
            this.bntMidCategoryModify.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntMidCategoryModify.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.bntMidCategoryModify.ForeColor = System.Drawing.Color.White;
            this.bntMidCategoryModify.Location = new System.Drawing.Point(10, 340);
            this.bntMidCategoryModify.Name = "bntMidCategoryModify";
            this.bntMidCategoryModify.Size = new System.Drawing.Size(70, 30);
            this.bntMidCategoryModify.TabIndex = 9;
            this.bntMidCategoryModify.Text = "수정";
            this.bntMidCategoryModify.UseVisualStyleBackColor = false;
            this.bntMidCategoryModify.Click += new System.EventHandler(this.bntMidCategoryModify_Click);
            // 
            // groupBoxBotCategory
            // 
            this.groupBoxBotCategory.Controls.Add(this.panelCategoryBot);
            this.groupBoxBotCategory.Controls.Add(this.bntBotCategoryAdd);
            this.groupBoxBotCategory.Controls.Add(this.bntBotCategoryModify);
            this.groupBoxBotCategory.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.groupBoxBotCategory.Location = new System.Drawing.Point(570, 27);
            this.groupBoxBotCategory.Name = "groupBoxBotCategory";
            this.groupBoxBotCategory.Size = new System.Drawing.Size(270, 380);
            this.groupBoxBotCategory.TabIndex = 10;
            this.groupBoxBotCategory.TabStop = false;
            this.groupBoxBotCategory.Text = "소분류";
            // 
            // panelCategoryBot
            // 
            this.panelCategoryBot.Location = new System.Drawing.Point(10, 20);
            this.panelCategoryBot.Name = "panelCategoryBot";
            this.panelCategoryBot.Size = new System.Drawing.Size(250, 300);
            this.panelCategoryBot.TabIndex = 11;
            // 
            // bntBotCategoryAdd
            // 
            this.bntBotCategoryAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(95)))), ((int)(((byte)(184)))));
            this.bntBotCategoryAdd.FlatAppearance.BorderSize = 0;
            this.bntBotCategoryAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntBotCategoryAdd.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.bntBotCategoryAdd.ForeColor = System.Drawing.Color.White;
            this.bntBotCategoryAdd.Location = new System.Drawing.Point(90, 340);
            this.bntBotCategoryAdd.Name = "bntBotCategoryAdd";
            this.bntBotCategoryAdd.Size = new System.Drawing.Size(70, 30);
            this.bntBotCategoryAdd.TabIndex = 9;
            this.bntBotCategoryAdd.Text = "추가";
            this.bntBotCategoryAdd.UseVisualStyleBackColor = false;
            this.bntBotCategoryAdd.Click += new System.EventHandler(this.bntBotCategoryAdd_Click);
            // 
            // bntBotCategoryModify
            // 
            this.bntBotCategoryModify.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(95)))), ((int)(((byte)(184)))));
            this.bntBotCategoryModify.FlatAppearance.BorderSize = 0;
            this.bntBotCategoryModify.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntBotCategoryModify.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.bntBotCategoryModify.ForeColor = System.Drawing.Color.White;
            this.bntBotCategoryModify.Location = new System.Drawing.Point(10, 340);
            this.bntBotCategoryModify.Name = "bntBotCategoryModify";
            this.bntBotCategoryModify.Size = new System.Drawing.Size(70, 30);
            this.bntBotCategoryModify.TabIndex = 10;
            this.bntBotCategoryModify.Text = "수정";
            this.bntBotCategoryModify.UseVisualStyleBackColor = false;
            this.bntBotCategoryModify.Click += new System.EventHandler(this.bntBotCategoryModify_Click);
            // 
            // CategoryBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(854, 461);
            this.Controls.Add(this.groupBoxBotCategory);
            this.Controls.Add(this.groupBoxMidCategory);
            this.Controls.Add(this.groupBoxTopCategory);
            this.Controls.Add(this.bntClose);
            this.Controls.Add(this.bntOk);
            this.Name = "CategoryBoard";
            this.Text = "CategoryBoard";
            this.groupBoxTopCategory.ResumeLayout(false);
            this.groupBoxMidCategory.ResumeLayout(false);
            this.groupBoxBotCategory.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button bntTopCategoryModify;
        private System.Windows.Forms.Button bntTopCategoryAdd;
        private System.Windows.Forms.Button bntOk;
        private System.Windows.Forms.Button bntClose;
        private System.Windows.Forms.GroupBox groupBoxTopCategory;
        private System.Windows.Forms.GroupBox groupBoxMidCategory;
        private System.Windows.Forms.Button bntMidCategoryAdd;
        private System.Windows.Forms.Button bntMidCategoryModify;
        private System.Windows.Forms.GroupBox groupBoxBotCategory;
        private System.Windows.Forms.Button bntBotCategoryAdd;
        private System.Windows.Forms.Button bntBotCategoryModify;
        private System.Windows.Forms.Panel panelCategoryTop;
        private System.Windows.Forms.Panel panelCategoryMid;
        private System.Windows.Forms.Panel panelCategoryBot;
    }
}