using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BRMS
{
    public partial class PaymentList : Form
    {
        cDatabaseConnect dbconn = new cDatabaseConnect();
        cDataGridDefaultSet DgrSupList = new cDataGridDefaultSet();
        cDataGridDefaultSet DgrPayList = new cDataGridDefaultSet();
        int selectSupplier = 1;
        int balance = 0;
        int currentBalane = 0;
        int accessedEmp = 0;
        public PaymentList()
        {
            InitializeComponent();
            panelSupplierList.Controls.Add(DgrSupList.Dgr);
            panelPaymentList.Controls.Add(DgrPayList.Dgr);
            DgrSupList.Dgr.Dock = DockStyle.Fill;
            DgrPayList.Dgr.Dock = DockStyle.Fill;
            GridForm();
            LoadSupplierList();
            DgrPayList.CellDoubleClick += DgrpayList_CellDoubleClick;
            DgrSupList.CellDoubleClick += DgrSupList_CellDoubleClick;
            DgrPayList.Dgr.MouseClick += DgrpayList_MouseRightClick;
            GridhideColumn();
            supplierLabelSet(0);
            dtpFrom.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        }

        private void GridForm()
        {
            DgrSupList.Dgr.Columns["No"].Visible = false;
            DgrSupList.Dgr.Columns.Add("supCode", "코드");
            DgrSupList.Dgr.Columns.Add("supName", "공급사명");
            DgrSupList.Dgr.ReadOnly = true;
            DgrSupList.Dgr.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DgrSupList.Dgr.Columns["supCode"].Width = 50;
            DgrSupList.Dgr.Columns["supName"].Width = 175;
            foreach (DataGridViewColumn supListColumn in DgrSupList.Dgr.Columns)
            {
                supListColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                supListColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            DgrPayList.Dgr.Columns["No"].Visible = false;
            DgrPayList.Dgr.Columns.Add("payCode","코드");
            DgrPayList.Dgr.Columns.Add("workType","유형");
            DgrPayList.Dgr.Columns.Add("payDate","날짜");
            DgrPayList.Dgr.Columns.Add("purchaseAmount","매입액");
            DgrPayList.Dgr.Columns.Add("paymentAmount","결제액");
            DgrPayList.Dgr.Columns.Add("cahsPayment", "현금결제");
            DgrPayList.Dgr.Columns.Add("bankTransfer", "계좌이체");
            DgrPayList.Dgr.Columns.Add("creditPayment", "카드결제");
            DgrPayList.Dgr.Columns.Add("NotePayment", "어음");
            DgrPayList.Dgr.Columns.Add("Discount", "D/C");
            DgrPayList.Dgr.Columns.Add("Coupon", "쿠폰");
            DgrPayList.Dgr.Columns.Add("Supsiby", "장려금");
            DgrPayList.Dgr.Columns.Add("etc", "기타");
            DgrPayList.Dgr.Columns.Add("balance","잔액");
            DgrPayList.Dgr.Columns.Add("employee", "등록자");
            DgrPayList.Dgr.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DgrPayList.ApplyDefaultColumnSettings();

            //칼럼 포멧 설정
            DgrPayList.FormatAsInteger("purchaseAmount", "paymentAmount", "cahsPayment", "bankTransfer", "creditPayment", "NotePayment", "Discount", "Coupon", "Supsiby", "etc", "balance");
            DgrPayList.FormatAsDateTime("payDate");
            DgrPayList.FormatAsStringCenter("payCode", "workType", "employee");

            foreach (DataGridViewColumn payListColumn in DgrPayList.Dgr.Columns)
            {
                payListColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                payListColumn.MinimumWidth = 50;
            }
            
            DgrPayList.Dgr.ReadOnly = true;
        }
        private void LoadSupplierList()
        {
            string query = "SELECT sup_code,sup_name FROM supplier";
            DataTable supplierList = new DataTable();
            dbconn.SqlDataAdapterQuery(query, supplierList);
            int index = 0;
            foreach(DataRow dataRow in supplierList.Rows)
            {
                DgrSupList.Dgr.Rows.Add();
                DgrSupList.Dgr.Rows[index].Cells["supCode"].Value = dataRow["sup_code"].ToString();
                DgrSupList.Dgr.Rows[index].Cells["supName"].Value = dataRow["sup_name"].ToString();
                index++;
            }
        }
        
        /// <summary>
        /// 우클릭 메뉴 펼처보기
        /// </summary>
        private void GridExpandColumn()
        {
            DgrPayList.Dgr.Columns["payCode"].Visible = true;
            DgrPayList.Dgr.Columns["workType"].Visible = true;
            DgrPayList.Dgr.Columns["payDate"].Visible = true;
            DgrPayList.Dgr.Columns["purchaseAmount"].Visible = true;
            DgrPayList.Dgr.Columns["paymentAmount"].Visible = true;
            DgrPayList.Dgr.Columns["cahsPayment"].Visible = true;
            DgrPayList.Dgr.Columns["bankTransfer"].Visible = true;
            DgrPayList.Dgr.Columns["creditPayment"].Visible = true;
            DgrPayList.Dgr.Columns["NotePayment"].Visible = true;
            DgrPayList.Dgr.Columns["Discount"].Visible = true;
            DgrPayList.Dgr.Columns["Coupon"].Visible = true;
            DgrPayList.Dgr.Columns["Supsiby"].Visible = true;
            DgrPayList.Dgr.Columns["etc"].Visible = true;
            DgrPayList.Dgr.Columns["balance"].Visible = true;
            DgrPayList.Dgr.Columns["employee"].Visible = true;
            //DgrPayList.Dgr.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            
        }
        /// <summary>
        /// 우클릭 메뉴 줄여보기
        /// 클래스가 최초 호출시에는 줄여보기로 시작
        /// </summary>
        private void GridhideColumn()
        {
            DgrPayList.Dgr.Columns["payCode"].Visible = true;
            DgrPayList.Dgr.Columns["workType"].Visible = true;
            DgrPayList.Dgr.Columns["payDate"].Visible = true;
            DgrPayList.Dgr.Columns["purchaseAmount"].Visible = true;
            DgrPayList.Dgr.Columns["paymentAmount"].Visible = true;
            DgrPayList.Dgr.Columns["cahsPayment"].Visible = false;
            DgrPayList.Dgr.Columns["bankTransfer"].Visible = false;
            DgrPayList.Dgr.Columns["creditPayment"].Visible = false;
            DgrPayList.Dgr.Columns["NotePayment"].Visible = false;
            DgrPayList.Dgr.Columns["Discount"].Visible = false;
            DgrPayList.Dgr.Columns["Coupon"].Visible = false;
            DgrPayList.Dgr.Columns["Supsiby"].Visible = false;
            DgrPayList.Dgr.Columns["etc"].Visible = false;
            DgrPayList.Dgr.Columns["balance"].Visible = true;
            DgrPayList.Dgr.Columns["employee"].Visible = true;
            //DgrPayList.Dgr.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; 
        }
        /// <summary>
        /// 선택한 공급사로 변수 및 라벨 값 수정
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private int supplierLabelSet(int code)
        {
            selectSupplier = Convert.ToInt32(DgrSupList.Dgr.Rows[code].Cells["supCode"].Value.ToString());
            lblSupplier.Text = "공급사 : " + DgrSupList.Dgr.Rows[code].Cells["supName"].Value.ToString();
            DataTable resultData = new DataTable();
            DateTime nowDate = DateTime.Now.AddDays(1);
            DateTime fromDate = DateTime.Now.AddMonths(-5);
            fromDate = new DateTime(fromDate.Year, fromDate.Month, 1, 0, 0, 0);

            object resultObj = new object();
            currentBalane = 0; // 잔액 0원으로 수정 후 조회 및 계산
            string query = string.Format("SELECT cb_balance FROM closingbalance WHERE cb_sup =  {0} AND cb_date = {1}", selectSupplier, fromDate.AddMonths(-1).ToString("yyyyMM"));
            dbconn.sqlScalaQuery(query, out resultObj);
            currentBalane = Convert.ToInt32(resultObj);
            
            query = string.Format("SELECT ISNULL(SUM(pur_amount),0) FROM purchase WHERE pur_sup = {0} AND pur_date > '{1}' AND pur_date < '{2}'", selectSupplier, fromDate.ToString("yyyy-MM-01"), nowDate.ToString("yyyy-MM-dd"));
            dbconn.sqlScalaQuery(query, out resultObj);
            currentBalane += Convert.ToInt32(resultObj);
            
            query = string.Format("SELECT ISNULL(SUM(pay_paycash + pay_accounttransfer + pay_paycredit + pay_paynote + pay_DC + pay_coupone + pay_supsiby + pay_etc),0) FROM payment WHERE pay_sup = {0} AND pay_date > '{1}' AND pay_date < '{2}' AND pay_status = 1", selectSupplier, fromDate.ToString("yyyy-MM-dd"), nowDate.ToString("yyyy-MM-dd"));
            dbconn.sqlScalaQuery(query, out resultObj);
            currentBalane -= Convert.ToInt32(resultObj);
            lblCurrentBalance.Text = Convert.ToString(currentBalane);
            return currentBalane;
        }

        private void GridFill(DataTable dataTable)
        {
            DgrPayList.Dgr.Rows.Clear();
            int rowIndex = 0;
            int cash = 0;
            int credit = 0;
            int note = 0;
            int account = 0;
            int dc = 0;
            int coupon = 0;
            int supsiby = 0;
            int etc = 0;
            int total = 0;
            int purchase = 0;
            int cashTotal = 0, creditTotal = 0, noteTotal = 0, accountTotal = 0;
            int dcTotal = 0, couponTotal = 0, supsibyTotal = 0, etcTotal = 0, totalPayment = 0, purchaseTotal = 0;
            string workType = "";

            string employee = "";
            foreach (DataRow dataRow in dataTable.Rows)
            {
                //DataRow StatusRow = cBoxStatus.SelectedValue.ToString .[int.Parse(dataRow["pdt_status"].ToString())];
                DgrPayList.Dgr.Rows.Add();
                string query = $"SELECT emp_name FROM employee WHERE emp_code = {dataRow["pay_emp"]}";
                object resultObj = new object();
                dbconn.sqlScalaQuery(query, out resultObj);
                employee = resultObj.ToString();
                purchase = Convert.ToInt32(dataRow["pur_amount"].ToString());
                cash = Convert.ToInt32(dataRow["pay_paycash"].ToString());
                credit = Convert.ToInt32(dataRow["pay_paycredit"].ToString());
                note = Convert.ToInt32(dataRow["pay_paynote"].ToString());
                account = Convert.ToInt32(dataRow["pay_accounttransfer"].ToString());
                dc = Convert.ToInt32(dataRow["pay_DC"].ToString());
                coupon = Convert.ToInt32(dataRow["pay_coupone"].ToString());
                supsiby = Convert.ToInt32(dataRow["pay_supsiby"].ToString());
                etc = Convert.ToInt32(dataRow["pay_etc"].ToString());
                total = cash + credit + note + account + dc + coupon + supsiby + etc;
                switch(dataRow["pay_type"].ToString())
                {
                    case "1":
                        workType = "결제";
                        break;
                    case "2":
                        workType = "매입결제";
                        break;
                    case "0":
                        workType = "매입";
                        break;
                }
                DgrPayList.Dgr.Rows[rowIndex].Cells["payCode"].Value = dataRow["pay_code"];
                DgrPayList.Dgr.Rows[rowIndex].Cells["workType"].Value = workType;
                DgrPayList.Dgr.Rows[rowIndex].Cells["payDate"].Value = Convert.ToDateTime(dataRow["pay_date"]);
                DgrPayList.Dgr.Rows[rowIndex].Cells["purchaseAmount"].Value = purchase;
                DgrPayList.Dgr.Rows[rowIndex].Cells["paymentAmount"].Value = total;
                DgrPayList.Dgr.Rows[rowIndex].Cells["cahsPayment"].Value = cash;
                DgrPayList.Dgr.Rows[rowIndex].Cells["bankTransfer"].Value = account;
                DgrPayList.Dgr.Rows[rowIndex].Cells["creditPayment"].Value = credit;
                DgrPayList.Dgr.Rows[rowIndex].Cells["NotePayment"].Value = note;
                DgrPayList.Dgr.Rows[rowIndex].Cells["Discount"].Value = dc;
                DgrPayList.Dgr.Rows[rowIndex].Cells["Coupon"].Value = coupon;
                DgrPayList.Dgr.Rows[rowIndex].Cells["Supsiby"].Value = supsiby;
                DgrPayList.Dgr.Rows[rowIndex].Cells["etc"].Value = etc;
                DgrPayList.Dgr.Rows[rowIndex].Cells["balance"].Value = balance + purchase - total;
                DgrPayList.Dgr.Rows[rowIndex].Cells["employee"].Value = employee;
                balance = DgrPayList.ConvertToInt(DgrPayList.Dgr.Rows[rowIndex].Cells["balance"].Value);
                rowIndex++;
                // Update totals
                purchaseTotal += purchase;
                cashTotal += cash;
                creditTotal += credit;
                noteTotal += note;
                accountTotal += account;
                dcTotal += dc;
                couponTotal += coupon;
                supsibyTotal += supsiby;
                etcTotal += etc;
                totalPayment += total;
            }
            DgrPayList.Dgr.Rows.Add();
            
            DgrPayList.Dgr.Rows[rowIndex].Cells["payCode"].Value = "합계";
            DgrPayList.Dgr.Rows[rowIndex].Cells["purchaseAmount"].Value = purchaseTotal;
            DgrPayList.Dgr.Rows[rowIndex].Cells["paymentAmount"].Value = totalPayment;
            DgrPayList.Dgr.Rows[rowIndex].Cells["cahsPayment"].Value = cashTotal;
            DgrPayList.Dgr.Rows[rowIndex].Cells["bankTransfer"].Value = accountTotal;
            DgrPayList.Dgr.Rows[rowIndex].Cells["creditPayment"].Value = creditTotal;
            DgrPayList.Dgr.Rows[rowIndex].Cells["NotePayment"].Value = noteTotal;
            DgrPayList.Dgr.Rows[rowIndex].Cells["Discount"].Value = dcTotal;
            DgrPayList.Dgr.Rows[rowIndex].Cells["Coupon"].Value = couponTotal;
            DgrPayList.Dgr.Rows[rowIndex].Cells["Supsiby"].Value = supsibyTotal;
            DgrPayList.Dgr.Rows[rowIndex].Cells["etc"].Value = etcTotal;
            DgrPayList.Dgr.Rows[rowIndex].Cells["balance"].Value = balance;
            
        }
        private void QuerySetting()
        {
            DataTable resultData = new DataTable();
            DateTime fromDate = dtpFrom.Value.AddMonths(-5);
            fromDate = new DateTime(fromDate.Year, fromDate.Month, 1, 0, 0, 0);
            DateTime toDate = dtpFrom.Value.AddDays(-1);

            string query = string.Format("SELECT ISNULL(cb_balance,0) cb_balance  FROM closingbalance WHERE cb_sup = {0} AND cb_date = {1}", selectSupplier,fromDate.AddMonths(-1).ToString("yyyyMM"));
            object resultObj = new object();
            dbconn.sqlScalaQuery(query, out resultObj);
            balance = Convert.ToInt32(resultObj);
            
            query = string.Format("SELECT ISNULL(SUM(pur_amount),0) FROM purchase WHERE pur_sup = {0} AND pur_date > '{1}' AND pur_date < '{2}'", selectSupplier, fromDate.ToString("yyyy-MM-dd"), toDate.ToString("yyyy-MM-dd"));
            dbconn.sqlScalaQuery(query, out resultObj);
            balance += Convert.ToInt32(resultObj);

            query = string.Format("SELECT ISNULL(SUM(pay_paycash + pay_accounttransfer + pay_paycredit + pay_paynote + pay_DC + pay_coupone + pay_supsiby + pay_etc),0) FROM payment WHERE pay_sup = {0} AND pay_date > '{1}' AND pay_date < '{2}' AND pay_status = 1", selectSupplier, fromDate.ToString("yyyy-MM-dd"), toDate.ToString("yyyy-MM-dd"));
            dbconn.sqlScalaQuery(query, out resultObj);
            balance -= Convert.ToInt32(resultObj);
            


            query = string.Format("SELECT pay_code, pay_type, 0 pur_amount ,pay_date, pay_paycash,pay_accounttransfer,pay_paycredit,pay_paynote,pay_DC," +
                "pay_coupone,pay_supsiby,pay_etc,pay_memo,pay_emp " +
                "FROM payment WHERE pay_sup = {0} AND pay_date >= '{1}' AND pay_date < '{2}' AND pay_status = 1 \n UNION ALL " +
                "\nSELECT pur_code, 0 as pay_type, pur_amount, pur_date, 0, 0, 0, 0, 0, 0, 0, 0, '',pur_emp " +
                "FROM purchase WHERE pur_date >= '{1}' AND  pur_date < '{2}' AND pur_sup = {0} ORDER BY pay_date",selectSupplier,dtpFrom.Value.ToString("yyyy-MM-dd hh:mm"), dtpTo.Value.AddDays(1).ToString("yyyy-MM-dd hh:mm"));

            dbconn.SqlDataAdapterQuery(query, resultData);
            GridFill(resultData);
            
        }
        public void RunQuery()
        {
            try
            {

                QuerySetting();
                cLog.InsertEmpAccessLogNotConnect("@paymentSearch", accessedEmp, selectSupplier);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void DgrpayList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int currentIndex = e.RowIndex;
            int lastIndex = DgrPayList.Dgr.RowCount - 1;
            if (currentIndex != lastIndex && Convert.ToString(DgrPayList.Dgr.Rows[currentIndex].Cells["workType"].Value).Contains("결제"))
            {
                int payCode = Convert.ToInt32(DgrPayList.Dgr.Rows[currentIndex].Cells["payCode"].Value);
                PaymentDetail paymentDetail = new PaymentDetail();
                cLog.InsertEmpAccessLogNotConnect("@payDetailSearch", accessedEmp, payCode);
                paymentDetail.LoadPaymentDetail(payCode);
                paymentDetail.ShowDialog();
            }            
        }
        /// <summary>
        /// 우클릭 기능
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgrpayList_MouseRightClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) 
            {
                
                ContextMenuStrip contextMenu = new ContextMenuStrip();
                ToolStripSeparator separator = new ToolStripSeparator();
                contextMenu.Items.Add("펼처보기");
                contextMenu.Items.Add("줄여보기");
                contextMenu.Items.Add(separator);
                // Grid 칼럼 이름을 메뉴 항목으로 추가
                foreach (DataGridViewColumn column in DgrPayList.Dgr.Columns)
                {
                    // 첫 번째 칼럼은 제외
                    if (column.Name != "No")
                    {
                        ToolStripMenuItem columnMenuItem = new ToolStripMenuItem(column.HeaderText)
                        {
                            CheckOnClick = true,
                            Checked = column.Visible
                        };

                        // 칼럼의 Name을 Tag로 저장
                        columnMenuItem.Tag = column.Name;

                        contextMenu.Items.Add(columnMenuItem);
                    }
                }
                contextMenu.Show(panelPaymentList, e.Location);
                contextMenu.ItemClicked += new ToolStripItemClickedEventHandler(Menu_Click);
            }

        }

        private void Menu_Click(object sender, ToolStripItemClickedEventArgs e )
        {
            string clickedItemText = e.ClickedItem.Text;
            
            switch (clickedItemText)
            {
                case "펼처보기":
                    GridExpandColumn();
                    break;
                case "줄여보기":
                    GridhideColumn();
                    break;
                default:
                    // 칼럼 이름으로 메뉴 항목 클릭 시 가시성 토글
                    //var menuItem = (ToolStripMenuItem)e.ClickedItem;
                    string columnName = e.ClickedItem.Tag.ToString();

                    // 칼럼 이름이 데이터 그리드에 존재하는지 확인
                    if (DgrPayList.Dgr.Columns.Contains(columnName))
                    {
                        var column = DgrPayList.Dgr.Columns[columnName];
                        // Grid 표시 상태 반전으로 표시 또는 해제
                        column.Visible = !column.Visible;  
                    }
                    break;


            }
        }
        private void DgrSupList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int currentIndex = e.RowIndex;
            
            supplierLabelSet(currentIndex);
            QuerySetting();

            
        }

        private void bntPaymentAdd_Click(object sender, EventArgs e)
        {

            PaymentDetail paymentDetail = new PaymentDetail();
           
            paymentDetail.AddPayment(selectSupplier,0,currentBalane);
            paymentDetail.ShowDialog();
        }

    }
}
