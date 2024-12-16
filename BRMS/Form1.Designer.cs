
namespace BRMS
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panelHead = new System.Windows.Forms.Panel();
            this.btnDatabaseConnect = new System.Windows.Forms.Button();
            this.panelSideMenu = new System.Windows.Forms.Panel();
            this.panelCustomerMenu = new System.Windows.Forms.Panel();
            this.btnCustomerLog = new System.Windows.Forms.Button();
            this.bntCustSaleList = new System.Windows.Forms.Button();
            this.bntCustomerList = new System.Windows.Forms.Button();
            this.bntCoustomer = new System.Windows.Forms.Button();
            this.panelSalseNenu = new System.Windows.Forms.Panel();
            this.bntOrderList = new System.Windows.Forms.Button();
            this.bntSalseList = new System.Windows.Forms.Button();
            this.bntSalesReport = new System.Windows.Forms.Button();
            this.bntSalseMenu = new System.Windows.Forms.Button();
            this.panelSupplyMenu = new System.Windows.Forms.Panel();
            this.btnPaymentLog = new System.Windows.Forms.Button();
            this.btnPurchaseLog = new System.Windows.Forms.Button();
            this.btnSupplierLog = new System.Windows.Forms.Button();
            this.bntSupplierPayment = new System.Windows.Forms.Button();
            this.bntSupplierOrder = new System.Windows.Forms.Button();
            this.bntPurchase = new System.Windows.Forms.Button();
            this.bntSupplier = new System.Windows.Forms.Button();
            this.bntSupplierMenu = new System.Windows.Forms.Button();
            this.panelBasicMenu = new System.Windows.Forms.Panel();
            this.btnAccessLog = new System.Windows.Forms.Button();
            this.btnEmpLog = new System.Windows.Forms.Button();
            this.btnProductLog = new System.Windows.Forms.Button();
            this.bntEmployee = new System.Windows.Forms.Button();
            this.bntCategory = new System.Windows.Forms.Button();
            this.bntProduct = new System.Windows.Forms.Button();
            this.bntBasicMenu = new System.Windows.Forms.Button();
            this.panelMenuTitle = new System.Windows.Forms.Panel();
            this.panelControlMenu = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.bntSearch = new System.Windows.Forms.Button();
            this.panelViewer = new System.Windows.Forms.Panel();
            this.panelHead.SuspendLayout();
            this.panelSideMenu.SuspendLayout();
            this.panelCustomerMenu.SuspendLayout();
            this.panelSalseNenu.SuspendLayout();
            this.panelSupplyMenu.SuspendLayout();
            this.panelBasicMenu.SuspendLayout();
            this.panelControlMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHead
            // 
            this.panelHead.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.panelHead.Controls.Add(this.btnDatabaseConnect);
            this.panelHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHead.Location = new System.Drawing.Point(0, 0);
            this.panelHead.Name = "panelHead";
            this.panelHead.Size = new System.Drawing.Size(1184, 60);
            this.panelHead.TabIndex = 0;
            // 
            // btnDatabaseConnect
            // 
            this.btnDatabaseConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDatabaseConnect.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btnDatabaseConnect.Location = new System.Drawing.Point(1108, 4);
            this.btnDatabaseConnect.Name = "btnDatabaseConnect";
            this.btnDatabaseConnect.Size = new System.Drawing.Size(64, 50);
            this.btnDatabaseConnect.TabIndex = 0;
            this.btnDatabaseConnect.Text = "연결설정";
            this.btnDatabaseConnect.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDatabaseConnect.UseVisualStyleBackColor = true;
            this.btnDatabaseConnect.Click += new System.EventHandler(this.btnDatabaseConnect_Click);
            // 
            // panelSideMenu
            // 
            this.panelSideMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.panelSideMenu.Controls.Add(this.panelCustomerMenu);
            this.panelSideMenu.Controls.Add(this.bntCoustomer);
            this.panelSideMenu.Controls.Add(this.panelSalseNenu);
            this.panelSideMenu.Controls.Add(this.bntSalseMenu);
            this.panelSideMenu.Controls.Add(this.panelSupplyMenu);
            this.panelSideMenu.Controls.Add(this.bntSupplierMenu);
            this.panelSideMenu.Controls.Add(this.panelBasicMenu);
            this.panelSideMenu.Controls.Add(this.bntBasicMenu);
            this.panelSideMenu.Controls.Add(this.panelMenuTitle);
            this.panelSideMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSideMenu.Location = new System.Drawing.Point(0, 60);
            this.panelSideMenu.Name = "panelSideMenu";
            this.panelSideMenu.Size = new System.Drawing.Size(150, 801);
            this.panelSideMenu.TabIndex = 1;
            // 
            // panelCustomerMenu
            // 
            this.panelCustomerMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(58)))), ((int)(((byte)(88)))));
            this.panelCustomerMenu.Controls.Add(this.btnCustomerLog);
            this.panelCustomerMenu.Controls.Add(this.bntCustSaleList);
            this.panelCustomerMenu.Controls.Add(this.bntCustomerList);
            this.panelCustomerMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCustomerMenu.Location = new System.Drawing.Point(0, 646);
            this.panelCustomerMenu.Name = "panelCustomerMenu";
            this.panelCustomerMenu.Size = new System.Drawing.Size(150, 85);
            this.panelCustomerMenu.TabIndex = 8;
            // 
            // btnCustomerLog
            // 
            this.btnCustomerLog.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCustomerLog.FlatAppearance.BorderSize = 0;
            this.btnCustomerLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCustomerLog.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.btnCustomerLog.ForeColor = System.Drawing.Color.White;
            this.btnCustomerLog.Location = new System.Drawing.Point(0, 70);
            this.btnCustomerLog.Name = "btnCustomerLog";
            this.btnCustomerLog.Size = new System.Drawing.Size(150, 35);
            this.btnCustomerLog.TabIndex = 2;
            this.btnCustomerLog.Text = "고객 변경 로그";
            this.btnCustomerLog.UseVisualStyleBackColor = true;
            this.btnCustomerLog.Click += new System.EventHandler(this.btnCustomerLog_Click);
            // 
            // bntCustSaleList
            // 
            this.bntCustSaleList.Dock = System.Windows.Forms.DockStyle.Top;
            this.bntCustSaleList.FlatAppearance.BorderSize = 0;
            this.bntCustSaleList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntCustSaleList.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.bntCustSaleList.ForeColor = System.Drawing.Color.White;
            this.bntCustSaleList.Location = new System.Drawing.Point(0, 35);
            this.bntCustSaleList.Name = "bntCustSaleList";
            this.bntCustSaleList.Size = new System.Drawing.Size(150, 35);
            this.bntCustSaleList.TabIndex = 1;
            this.bntCustSaleList.Text = "회원별 판매 현황";
            this.bntCustSaleList.UseVisualStyleBackColor = true;
            // 
            // bntCustomerList
            // 
            this.bntCustomerList.Dock = System.Windows.Forms.DockStyle.Top;
            this.bntCustomerList.FlatAppearance.BorderSize = 0;
            this.bntCustomerList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntCustomerList.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.bntCustomerList.ForeColor = System.Drawing.Color.White;
            this.bntCustomerList.Location = new System.Drawing.Point(0, 0);
            this.bntCustomerList.Name = "bntCustomerList";
            this.bntCustomerList.Size = new System.Drawing.Size(150, 35);
            this.bntCustomerList.TabIndex = 0;
            this.bntCustomerList.Text = "회원 목록";
            this.bntCustomerList.UseVisualStyleBackColor = true;
            this.bntCustomerList.Click += new System.EventHandler(this.bntCustomerList_Click);
            // 
            // bntCoustomer
            // 
            this.bntCoustomer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.bntCoustomer.Dock = System.Windows.Forms.DockStyle.Top;
            this.bntCoustomer.FlatAppearance.BorderSize = 0;
            this.bntCoustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntCoustomer.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.bntCoustomer.ForeColor = System.Drawing.Color.White;
            this.bntCoustomer.Location = new System.Drawing.Point(0, 601);
            this.bntCoustomer.Name = "bntCoustomer";
            this.bntCoustomer.Size = new System.Drawing.Size(150, 45);
            this.bntCoustomer.TabIndex = 7;
            this.bntCoustomer.Text = "회원 관리";
            this.bntCoustomer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bntCoustomer.UseVisualStyleBackColor = false;
            this.bntCoustomer.Click += new System.EventHandler(this.bntCoustomer_Click);
            // 
            // panelSalseNenu
            // 
            this.panelSalseNenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(58)))), ((int)(((byte)(88)))));
            this.panelSalseNenu.Controls.Add(this.bntOrderList);
            this.panelSalseNenu.Controls.Add(this.bntSalseList);
            this.panelSalseNenu.Controls.Add(this.bntSalesReport);
            this.panelSalseNenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSalseNenu.Location = new System.Drawing.Point(0, 496);
            this.panelSalseNenu.Name = "panelSalseNenu";
            this.panelSalseNenu.Size = new System.Drawing.Size(150, 105);
            this.panelSalseNenu.TabIndex = 6;
            // 
            // bntOrderList
            // 
            this.bntOrderList.Dock = System.Windows.Forms.DockStyle.Top;
            this.bntOrderList.FlatAppearance.BorderSize = 0;
            this.bntOrderList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntOrderList.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.bntOrderList.ForeColor = System.Drawing.Color.White;
            this.bntOrderList.Location = new System.Drawing.Point(0, 70);
            this.bntOrderList.Name = "bntOrderList";
            this.bntOrderList.Size = new System.Drawing.Size(150, 35);
            this.bntOrderList.TabIndex = 2;
            this.bntOrderList.Text = "주문서";
            this.bntOrderList.UseVisualStyleBackColor = true;
            this.bntOrderList.Click += new System.EventHandler(this.bntOrderList_Click);
            // 
            // bntSalseList
            // 
            this.bntSalseList.Dock = System.Windows.Forms.DockStyle.Top;
            this.bntSalseList.FlatAppearance.BorderSize = 0;
            this.bntSalseList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntSalseList.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.bntSalseList.ForeColor = System.Drawing.Color.White;
            this.bntSalseList.Location = new System.Drawing.Point(0, 35);
            this.bntSalseList.Name = "bntSalseList";
            this.bntSalseList.Size = new System.Drawing.Size(150, 35);
            this.bntSalseList.TabIndex = 1;
            this.bntSalseList.Text = "판매 내역";
            this.bntSalseList.UseVisualStyleBackColor = true;
            this.bntSalseList.Click += new System.EventHandler(this.bntSalseList_Click);
            // 
            // bntSalesReport
            // 
            this.bntSalesReport.Dock = System.Windows.Forms.DockStyle.Top;
            this.bntSalesReport.FlatAppearance.BorderSize = 0;
            this.bntSalesReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntSalesReport.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.bntSalesReport.ForeColor = System.Drawing.Color.White;
            this.bntSalesReport.Location = new System.Drawing.Point(0, 0);
            this.bntSalesReport.Name = "bntSalesReport";
            this.bntSalesReport.Size = new System.Drawing.Size(150, 35);
            this.bntSalesReport.TabIndex = 0;
            this.bntSalesReport.Text = "판매 현황";
            this.bntSalesReport.UseVisualStyleBackColor = true;
            this.bntSalesReport.Click += new System.EventHandler(this.bntSalesReport_Click);
            // 
            // bntSalseMenu
            // 
            this.bntSalseMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.bntSalseMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.bntSalseMenu.FlatAppearance.BorderSize = 0;
            this.bntSalseMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntSalseMenu.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.bntSalseMenu.ForeColor = System.Drawing.Color.White;
            this.bntSalseMenu.Location = new System.Drawing.Point(0, 451);
            this.bntSalseMenu.Name = "bntSalseMenu";
            this.bntSalseMenu.Size = new System.Drawing.Size(150, 45);
            this.bntSalseMenu.TabIndex = 5;
            this.bntSalseMenu.Text = "판매 관리";
            this.bntSalseMenu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bntSalseMenu.UseVisualStyleBackColor = false;
            this.bntSalseMenu.Click += new System.EventHandler(this.bntSalseMenu_Click);
            // 
            // panelSupplyMenu
            // 
            this.panelSupplyMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(58)))), ((int)(((byte)(88)))));
            this.panelSupplyMenu.Controls.Add(this.btnPaymentLog);
            this.panelSupplyMenu.Controls.Add(this.bntSupplierPayment);
            this.panelSupplyMenu.Controls.Add(this.btnPurchaseLog);
            this.panelSupplyMenu.Controls.Add(this.bntSupplierOrder);
            this.panelSupplyMenu.Controls.Add(this.bntPurchase);
            this.panelSupplyMenu.Controls.Add(this.btnSupplierLog);
            this.panelSupplyMenu.Controls.Add(this.bntSupplier);
            this.panelSupplyMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSupplyMenu.Location = new System.Drawing.Point(0, 342);
            this.panelSupplyMenu.Name = "panelSupplyMenu";
            this.panelSupplyMenu.Size = new System.Drawing.Size(150, 109);
            this.panelSupplyMenu.TabIndex = 4;
            // 
            // btnPaymentLog
            // 
            this.btnPaymentLog.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPaymentLog.FlatAppearance.BorderSize = 0;
            this.btnPaymentLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPaymentLog.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.btnPaymentLog.ForeColor = System.Drawing.Color.White;
            this.btnPaymentLog.Location = new System.Drawing.Point(0, 210);
            this.btnPaymentLog.Name = "btnPaymentLog";
            this.btnPaymentLog.Size = new System.Drawing.Size(150, 35);
            this.btnPaymentLog.TabIndex = 6;
            this.btnPaymentLog.Text = "결제 로그";
            this.btnPaymentLog.UseVisualStyleBackColor = true;
            this.btnPaymentLog.Click += new System.EventHandler(this.btnPaymentLog_Click);
            // 
            // btnPurchaseLog
            // 
            this.btnPurchaseLog.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPurchaseLog.FlatAppearance.BorderSize = 0;
            this.btnPurchaseLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPurchaseLog.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.btnPurchaseLog.ForeColor = System.Drawing.Color.White;
            this.btnPurchaseLog.Location = new System.Drawing.Point(0, 175);
            this.btnPurchaseLog.Name = "btnPurchaseLog";
            this.btnPurchaseLog.Size = new System.Drawing.Size(150, 35);
            this.btnPurchaseLog.TabIndex = 5;
            this.btnPurchaseLog.Text = "매입/발주 로그";
            this.btnPurchaseLog.UseVisualStyleBackColor = true;
            this.btnPurchaseLog.Click += new System.EventHandler(this.btnPurchaseLog_Click);
            // 
            // btnSupplierLog
            // 
            this.btnSupplierLog.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSupplierLog.FlatAppearance.BorderSize = 0;
            this.btnSupplierLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSupplierLog.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.btnSupplierLog.ForeColor = System.Drawing.Color.White;
            this.btnSupplierLog.Location = new System.Drawing.Point(0, 140);
            this.btnSupplierLog.Name = "btnSupplierLog";
            this.btnSupplierLog.Size = new System.Drawing.Size(150, 35);
            this.btnSupplierLog.TabIndex = 4;
            this.btnSupplierLog.Text = "공급사 변경 로그";
            this.btnSupplierLog.UseVisualStyleBackColor = true;
            this.btnSupplierLog.Click += new System.EventHandler(this.btnSupplierLog_Click);
            // 
            // bntSupplierPayment
            // 
            this.bntSupplierPayment.Dock = System.Windows.Forms.DockStyle.Top;
            this.bntSupplierPayment.FlatAppearance.BorderSize = 0;
            this.bntSupplierPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntSupplierPayment.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.bntSupplierPayment.ForeColor = System.Drawing.Color.White;
            this.bntSupplierPayment.Location = new System.Drawing.Point(0, 105);
            this.bntSupplierPayment.Name = "bntSupplierPayment";
            this.bntSupplierPayment.Size = new System.Drawing.Size(150, 35);
            this.bntSupplierPayment.TabIndex = 3;
            this.bntSupplierPayment.Text = "공급사 결제";
            this.bntSupplierPayment.UseVisualStyleBackColor = true;
            this.bntSupplierPayment.Click += new System.EventHandler(this.bntSupplierPayment_Click);
            // 
            // bntSupplierOrder
            // 
            this.bntSupplierOrder.Dock = System.Windows.Forms.DockStyle.Top;
            this.bntSupplierOrder.FlatAppearance.BorderSize = 0;
            this.bntSupplierOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntSupplierOrder.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.bntSupplierOrder.ForeColor = System.Drawing.Color.White;
            this.bntSupplierOrder.Location = new System.Drawing.Point(0, 70);
            this.bntSupplierOrder.Name = "bntSupplierOrder";
            this.bntSupplierOrder.Size = new System.Drawing.Size(150, 35);
            this.bntSupplierOrder.TabIndex = 2;
            this.bntSupplierOrder.Text = "발주";
            this.bntSupplierOrder.UseVisualStyleBackColor = true;
            this.bntSupplierOrder.Click += new System.EventHandler(this.bntSupplierOrder_Click);
            // 
            // bntPurchase
            // 
            this.bntPurchase.Dock = System.Windows.Forms.DockStyle.Top;
            this.bntPurchase.FlatAppearance.BorderSize = 0;
            this.bntPurchase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntPurchase.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.bntPurchase.ForeColor = System.Drawing.Color.White;
            this.bntPurchase.Location = new System.Drawing.Point(0, 35);
            this.bntPurchase.Name = "bntPurchase";
            this.bntPurchase.Size = new System.Drawing.Size(150, 35);
            this.bntPurchase.TabIndex = 1;
            this.bntPurchase.Text = "매입";
            this.bntPurchase.UseVisualStyleBackColor = true;
            this.bntPurchase.Click += new System.EventHandler(this.bntPurchase_Click);
            // 
            // bntSupplier
            // 
            this.bntSupplier.Dock = System.Windows.Forms.DockStyle.Top;
            this.bntSupplier.FlatAppearance.BorderSize = 0;
            this.bntSupplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntSupplier.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.bntSupplier.ForeColor = System.Drawing.Color.White;
            this.bntSupplier.Location = new System.Drawing.Point(0, 0);
            this.bntSupplier.Name = "bntSupplier";
            this.bntSupplier.Size = new System.Drawing.Size(150, 35);
            this.bntSupplier.TabIndex = 0;
            this.bntSupplier.Text = "공급사 관리";
            this.bntSupplier.UseVisualStyleBackColor = true;
            this.bntSupplier.Click += new System.EventHandler(this.bntSupplier_Click);
            // 
            // bntSupplierMenu
            // 
            this.bntSupplierMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.bntSupplierMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.bntSupplierMenu.FlatAppearance.BorderSize = 0;
            this.bntSupplierMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntSupplierMenu.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.bntSupplierMenu.ForeColor = System.Drawing.Color.White;
            this.bntSupplierMenu.Location = new System.Drawing.Point(0, 297);
            this.bntSupplierMenu.Name = "bntSupplierMenu";
            this.bntSupplierMenu.Size = new System.Drawing.Size(150, 45);
            this.bntSupplierMenu.TabIndex = 3;
            this.bntSupplierMenu.Text = "공급사/매입 관리";
            this.bntSupplierMenu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bntSupplierMenu.UseVisualStyleBackColor = false;
            this.bntSupplierMenu.Click += new System.EventHandler(this.bntSupplierMenu_Click);
            // 
            // panelBasicMenu
            // 
            this.panelBasicMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(58)))), ((int)(((byte)(88)))));
            this.panelBasicMenu.Controls.Add(this.btnAccessLog);
            this.panelBasicMenu.Controls.Add(this.btnEmpLog);
            this.panelBasicMenu.Controls.Add(this.bntEmployee);
            this.panelBasicMenu.Controls.Add(this.bntCategory);
            this.panelBasicMenu.Controls.Add(this.btnProductLog);
            this.panelBasicMenu.Controls.Add(this.bntProduct);
            this.panelBasicMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBasicMenu.Location = new System.Drawing.Point(0, 85);
            this.panelBasicMenu.Name = "panelBasicMenu";
            this.panelBasicMenu.Size = new System.Drawing.Size(150, 212);
            this.panelBasicMenu.TabIndex = 2;
            // 
            // btnAccessLog
            // 
            this.btnAccessLog.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAccessLog.FlatAppearance.BorderSize = 0;
            this.btnAccessLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccessLog.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.btnAccessLog.ForeColor = System.Drawing.Color.White;
            this.btnAccessLog.Location = new System.Drawing.Point(0, 175);
            this.btnAccessLog.Name = "btnAccessLog";
            this.btnAccessLog.Size = new System.Drawing.Size(150, 35);
            this.btnAccessLog.TabIndex = 5;
            this.btnAccessLog.Text = "직원접속로그";
            this.btnAccessLog.UseVisualStyleBackColor = true;
            this.btnAccessLog.Click += new System.EventHandler(this.btnAccessLog_Click);
            // 
            // btnEmpLog
            // 
            this.btnEmpLog.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnEmpLog.FlatAppearance.BorderSize = 0;
            this.btnEmpLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmpLog.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.btnEmpLog.ForeColor = System.Drawing.Color.White;
            this.btnEmpLog.Location = new System.Drawing.Point(0, 140);
            this.btnEmpLog.Name = "btnEmpLog";
            this.btnEmpLog.Size = new System.Drawing.Size(150, 35);
            this.btnEmpLog.TabIndex = 4;
            this.btnEmpLog.Text = "직원변경로그";
            this.btnEmpLog.UseVisualStyleBackColor = true;
            this.btnEmpLog.Click += new System.EventHandler(this.btnEmpLog_Click);
            
            // 
            // bntEmployee
            // 
            this.bntEmployee.Dock = System.Windows.Forms.DockStyle.Top;
            this.bntEmployee.FlatAppearance.BorderSize = 0;
            this.bntEmployee.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntEmployee.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.bntEmployee.ForeColor = System.Drawing.Color.White;
            this.bntEmployee.Location = new System.Drawing.Point(0, 70);
            this.bntEmployee.Name = "bntEmployee";
            this.bntEmployee.Size = new System.Drawing.Size(150, 35);
            this.bntEmployee.TabIndex = 3;
            this.bntEmployee.Text = "직원관리";
            this.bntEmployee.UseVisualStyleBackColor = true;
            this.bntEmployee.Click += new System.EventHandler(this.bntEmployee_Click);
            // 
            // bntCategory
            // 
            this.bntCategory.Dock = System.Windows.Forms.DockStyle.Top;
            this.bntCategory.FlatAppearance.BorderSize = 0;
            this.bntCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntCategory.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.bntCategory.ForeColor = System.Drawing.Color.White;
            this.bntCategory.Location = new System.Drawing.Point(0, 35);
            this.bntCategory.Name = "bntCategory";
            this.bntCategory.Size = new System.Drawing.Size(150, 35);
            this.bntCategory.TabIndex = 2;
            this.bntCategory.Text = "분류";
            this.bntCategory.UseVisualStyleBackColor = true;
            this.bntCategory.Click += new System.EventHandler(this.bntCategory_Click);
            // 
            // btnProductLog
            // 
            this.btnProductLog.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnProductLog.FlatAppearance.BorderSize = 0;
            this.btnProductLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProductLog.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.btnProductLog.ForeColor = System.Drawing.Color.White;
            this.btnProductLog.Location = new System.Drawing.Point(0, 105);
            this.btnProductLog.Name = "btnProductLog";
            this.btnProductLog.Size = new System.Drawing.Size(150, 35);
            this.btnProductLog.TabIndex = 1;
            this.btnProductLog.Text = "제품변경로그";
            this.btnProductLog.UseVisualStyleBackColor = true;
            this.btnProductLog.Click += new System.EventHandler(this.btnProductLog_Click);
            // 
            // bntProduct
            // 
            this.bntProduct.Dock = System.Windows.Forms.DockStyle.Top;
            this.bntProduct.FlatAppearance.BorderSize = 0;
            this.bntProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntProduct.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.bntProduct.ForeColor = System.Drawing.Color.White;
            this.bntProduct.Location = new System.Drawing.Point(0, 0);
            this.bntProduct.Name = "bntProduct";
            this.bntProduct.Size = new System.Drawing.Size(150, 35);
            this.bntProduct.TabIndex = 0;
            this.bntProduct.Text = "제품관리";
            this.bntProduct.UseVisualStyleBackColor = true;
            this.bntProduct.Click += new System.EventHandler(this.bntProduct_Click);
            // 
            // bntBasicMenu
            // 
            this.bntBasicMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.bntBasicMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.bntBasicMenu.FlatAppearance.BorderSize = 0;
            this.bntBasicMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntBasicMenu.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.bntBasicMenu.ForeColor = System.Drawing.Color.White;
            this.bntBasicMenu.Location = new System.Drawing.Point(0, 40);
            this.bntBasicMenu.Name = "bntBasicMenu";
            this.bntBasicMenu.Size = new System.Drawing.Size(150, 45);
            this.bntBasicMenu.TabIndex = 1;
            this.bntBasicMenu.Text = "기초메뉴";
            this.bntBasicMenu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bntBasicMenu.UseVisualStyleBackColor = false;
            this.bntBasicMenu.Click += new System.EventHandler(this.bntBasicMenu_Click);
            // 
            // panelMenuTitle
            // 
            this.panelMenuTitle.BackColor = System.Drawing.Color.White;
            this.panelMenuTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMenuTitle.Location = new System.Drawing.Point(0, 0);
            this.panelMenuTitle.Name = "panelMenuTitle";
            this.panelMenuTitle.Size = new System.Drawing.Size(150, 40);
            this.panelMenuTitle.TabIndex = 0;
            // 
            // panelControlMenu
            // 
            this.panelControlMenu.BackColor = System.Drawing.Color.White;
            this.panelControlMenu.Controls.Add(this.btnPrint);
            this.panelControlMenu.Controls.Add(this.btnExportExcel);
            this.panelControlMenu.Controls.Add(this.bntSearch);
            this.panelControlMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControlMenu.Location = new System.Drawing.Point(150, 60);
            this.panelControlMenu.Name = "panelControlMenu";
            this.panelControlMenu.Size = new System.Drawing.Size(1034, 40);
            this.panelControlMenu.TabIndex = 2;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Location = new System.Drawing.Point(948, 3);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(34, 34);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "인쇄";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportExcel.Location = new System.Drawing.Point(988, 3);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(34, 34);
            this.btnExportExcel.TabIndex = 1;
            this.btnExportExcel.Text = "엑셀";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // bntSearch
            // 
            this.bntSearch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.bntSearch.Image = ((System.Drawing.Image)(resources.GetObject("bntSearch.Image")));
            this.bntSearch.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.bntSearch.Location = new System.Drawing.Point(444, 0);
            this.bntSearch.Name = "bntSearch";
            this.bntSearch.Size = new System.Drawing.Size(59, 40);
            this.bntSearch.TabIndex = 0;
            this.bntSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.bntSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bntSearch.UseVisualStyleBackColor = true;
            this.bntSearch.Click += new System.EventHandler(this.bntSearch_Click);
            // 
            // panelViewer
            // 
            this.panelViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelViewer.Location = new System.Drawing.Point(150, 100);
            this.panelViewer.Name = "panelViewer";
            this.panelViewer.Size = new System.Drawing.Size(1034, 761);
            this.panelViewer.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 861);
            this.Controls.Add(this.panelViewer);
            this.Controls.Add(this.panelControlMenu);
            this.Controls.Add(this.panelSideMenu);
            this.Controls.Add(this.panelHead);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panelHead.ResumeLayout(false);
            this.panelSideMenu.ResumeLayout(false);
            this.panelCustomerMenu.ResumeLayout(false);
            this.panelSalseNenu.ResumeLayout(false);
            this.panelSupplyMenu.ResumeLayout(false);
            this.panelBasicMenu.ResumeLayout(false);
            this.panelControlMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHead;
        private System.Windows.Forms.Panel panelSideMenu;
        private System.Windows.Forms.Panel panelSalseNenu;
        private System.Windows.Forms.Button bntOrderList;
        private System.Windows.Forms.Button bntSalseList;
        private System.Windows.Forms.Button bntSalesReport;
        private System.Windows.Forms.Button bntSalseMenu;
        private System.Windows.Forms.Panel panelSupplyMenu;
        private System.Windows.Forms.Button bntSupplierPayment;
        private System.Windows.Forms.Button bntSupplierOrder;
        private System.Windows.Forms.Button bntPurchase;
        private System.Windows.Forms.Button bntSupplier;
        private System.Windows.Forms.Button bntSupplierMenu;
        private System.Windows.Forms.Panel panelBasicMenu;
        private System.Windows.Forms.Button btnProductLog;
        private System.Windows.Forms.Button bntEmployee;
        private System.Windows.Forms.Button bntCategory;
        private System.Windows.Forms.Button bntProduct;
        private System.Windows.Forms.Button bntBasicMenu;
        private System.Windows.Forms.Panel panelMenuTitle;
        private System.Windows.Forms.Panel panelControlMenu;
        private System.Windows.Forms.Panel panelViewer;
        private System.Windows.Forms.Button bntSearch;
        private System.Windows.Forms.Button bntCoustomer;
        private System.Windows.Forms.Panel panelCustomerMenu;
        private System.Windows.Forms.Button bntCustSaleList;
        private System.Windows.Forms.Button bntCustomerList;
        private System.Windows.Forms.Button btnDatabaseConnect;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnSupplierLog;
        private System.Windows.Forms.Button btnEmpLog;
        private System.Windows.Forms.Button btnPurchaseLog;
        private System.Windows.Forms.Button btnAccessLog;
        private System.Windows.Forms.Button btnPaymentLog;
        private System.Windows.Forms.Button btnCustomerLog;
    }
}

