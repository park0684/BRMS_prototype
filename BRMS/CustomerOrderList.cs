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
    public partial class CustomerOrderList : Form
    {
        cDatabaseConnect dbconn = new cDatabaseConnect();
        cDataGridDefaultSet OrderList = new cDataGridDefaultSet();
        bool customerSelect = true;
        int customerCode = 0;
        string customerName = "";

        Dictionary<int, string> accessPermission = new Dictionary<int, string>();
        int accessedEmp = 0;

        private readonly Dictionary<string, int> statusMapping = new Dictionary<string, int>
        {
            { "주문", 1 },
            { "판매", 2 },
            { "취소", 0 }
        };
        public CustomerOrderList()
        {
            InitializeComponent();
            panelDatagrid.Controls.Add(OrderList.Dgr);
            OrderList.Dgr.Dock = DockStyle.Fill;
            OrderList.CellDoubleClick += OrderList_CellDoubleClick;
            ComboBoxSetting();
            GridForm();
            customerSelectToggle();
        }

        private void ComboBoxSetting()
        {
            cmBoxDateType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmBoxStatus.DropDownStyle = ComboBoxStyle.DropDownList;

            // DateType ComboBox 설정
            cmBoxDateType.Items.AddRange(new string[] { "등록일", "수정일", "판매일" });
            cmBoxDateType.SelectedIndex = 0;

            cmBoxStatus.Items.AddRange(new string[] { "전체", "주문", "판매", "취소", "취소 제외" });
            cmBoxStatus.SelectedIndex = 4;  // '주문' 선택
        }

        private void GridForm()
        {
            OrderList.Dgr.Columns.Add("orderCode", "주문서코드");
            OrderList.Dgr.Columns.Add("orderStatus", "상태");
            OrderList.Dgr.Columns.Add("orderDate", "주문일");
            OrderList.Dgr.Columns.Add("orderSaleDate", "판매일");
            OrderList.Dgr.Columns.Add("orderCustomer", "회원");
            OrderList.Dgr.Columns.Add("orderAmountKrw", "주문액(한화)");
            OrderList.Dgr.Columns.Add("orderAmountUsd", "주문액(미화)");
            OrderList.Dgr.Columns.Add("orderStaff", "담당자");
            OrderList.Dgr.Columns.Add("orderNote", "메모");
            OrderList.Dgr.Columns.Add("orderUpdate", "수정일");
            OrderList.Dgr.Columns.Add("orderExchange", "환율");
            

            OrderList.Dgr.ReadOnly = true;
            OrderList.Dgr.Columns["orderCode"].Visible = false;
            OrderList.Dgr.Columns["orderExchange"].Visible = false;
            //포멧 설정
            OrderList.FormatAsStringLeft("orderCustomer");
            OrderList.FormatAsStringCenter("orderStatus", "orderStaff", "orderNote");
            OrderList.FormatAsInteger("orderAmountKrw");
            OrderList.FormatAsDecimal("orderAmountUsd");
            OrderList.FormatAsDateTime("orderDate", "orderUpdate");
            OrderList.FormatAsDate("orderSaleDate");
            OrderList.ApplyDefaultColumnSettings();
        }

        private void GridFill(DataTable dataTable)
        {
            int rowIndex = 0;
            OrderList.Dgr.Rows.Clear();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                OrderList.Dgr.Rows.Add();
                OrderList.Dgr.Rows[rowIndex].Cells["No"].Value = rowIndex+1;
                OrderList.Dgr.Rows[rowIndex].Cells["orderCode"].Value = dataRow["cord_code"];

                string orderStatusText = cStatusCode.GetCustomerOrderStatus(Convert.ToInt32(dataRow["cord_status"])); //  statusMapping.First(x => x.Value == Convert.ToInt32(dataRow["cord_status"])).Key;
                OrderList.Dgr.Rows[rowIndex].Cells["orderStatus"].Value = orderStatusText;  // 딕셔너리에서 코드로 텍스트 검색
                
                OrderList.Dgr.Rows[rowIndex].Cells["orderDate"].Value = Convert.ToDateTime(dataRow["cord_date"]);
                if(string.IsNullOrEmpty(dataRow["cord_sdate"].ToString()))
                {
                    OrderList.Dgr.Rows[rowIndex].Cells["orderSaleDate"].Value = "";
                }
                else
                {
                    OrderList.Dgr.Rows[rowIndex].Cells["orderSaleDate"].Value = Convert.ToDateTime(dataRow["cord_sdate"]);
                }
                OrderList.Dgr.Rows[rowIndex].Cells["orderCustomer"].Value = dataRow["cust_name"];
                OrderList.Dgr.Rows[rowIndex].Cells["orderAmountKrw"].Value = dataRow["cord_amount_krw"];
                OrderList.Dgr.Rows[rowIndex].Cells["orderAmountUsd"].Value = dataRow["cord_amount_usd"];
                OrderList.Dgr.Rows[rowIndex].Cells["orderStaff"].Value = dataRow["cord_staff"].ToString(); ;
                OrderList.Dgr.Rows[rowIndex].Cells["orderNote"].Value = dataRow["cord_memo"];
                OrderList.Dgr.Rows[rowIndex].Cells["orderUpdate"].Value = Convert.ToDateTime(dataRow["cord_udate"]);
                OrderList.Dgr.Rows[rowIndex].Cells["orderExchange"].Value = dataRow["cord_exchange"];
                rowIndex++;

            }
        }
        private bool customerSelectToggle()
        {
            customerSelect = !customerSelect;
            if(customerSelect == false)
            {
                lblCustName.Visible = false;
                bntCustomer.Text = "회원검색";
                lblCustName.Text = "";
                customerCode = 0;
                customerName = "";
            }
            else
            {
                lblCustName.Visible = true;
                bntCustomer.Text = "지정취소";
                //lblCustName.Text = customerName;


            }
            return customerSelect;
        }
        
        private void SetCustomerInfo()
        {
            string qeury = $"SELECT cust_name FROM customer WHERE cust_code = {customerCode}";
            object resultObj = new object();
            dbconn.sqlScalaQuery(qeury, out resultObj);
            this.customerName = resultObj.ToString();
            lblCustName.Text = customerName;
        }
        private void QuerySetting()
        {
            DataTable resultData = new DataTable();
            string dateType= "";
            switch(cmBoxDateType.SelectedIndex)
            {
                case 0:
                    dateType = "cord_date";
                    break;
                case 1:
                    dateType = "cord_udate";
                    break;
                case 2:
                    dateType = "cord_sdate";
                    break;
            }
            string dateFrom = dtpDateFrom.Value.ToString("yyyy-MM-dd");
            string dateTo = dtpDateTo.Value.AddDays(1).ToString("yyyy-MM-dd");
            string query = "SELECT  cord_code, cord_date, cust_name, cord_status, cord_amount_krw, cord_amount_usd, " +
                "cord_staff, cord_idate, cord_udate, cord_memo, cord_sdate, cord_exchange  FROM  custorder, customer " +
                $"WHERE cord_cust =  cust_code AND {dateType} >= '{dateFrom}' ANd {dateType} < '{dateTo}' ";
            if (customerSelect == true)
            {
                query = string.Format(query + " AND cust_code ={0}", customerCode);
            }

            string selectedText = cmBoxStatus.SelectedItem.ToString();
            int selectedStatusCode = cStatusCode.GetCustomerOrderStatusCode(selectedText);

            if (selectedStatusCode != -1)//statusMapping.TryGetValue(selectedText, out selectedStatusCode))
            {
                // 상태가 딕셔너리에 존재하는 경우만 쿼리에 추가
                query = query + $" AND cord_status = {selectedStatusCode}";
            }
            else if (cmBoxStatus.SelectedIndex == 4)
            {
                query = query + " AND cord_status NOT IN (0)";
            }

            dbconn.SqlDataAdapterQuery(query, resultData);
            GridFill(resultData);
            cLog.InsertEmpAccessLogNotConnect("@customerOrderSearch", accessedEmp, 0);
        }

        public void RunQuery()
        {
            try
            {
                QuerySetting();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void OrderList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int orderCode = OrderList.ConvertToInt(OrderList.Dgr.CurrentRow.Cells["orderCode"].Value);
            CustomerOrderDetail customerOrderDetail = new CustomerOrderDetail();
            customerOrderDetail.StartPosition = FormStartPosition.CenterParent;
            customerOrderDetail.OrderSelectedHandle(orderCode);
            cLog.InsertEmpAccessLogNotConnect("@customerOrderSearch", accessedEmp, orderCode);
            customerOrderDetail.ShowDialog();
        }
        private void bntCustomer_Click(object sender, EventArgs e)
        {
            customerSelectToggle();
            if(customerSelect == true)
            {
                CustomerSearchBox customerSearchBox = new CustomerSearchBox();
                customerSearchBox.StartPosition = FormStartPosition.CenterParent;
                customerSearchBox.GetCustomerCode += (code) => { customerCode = (code); SetCustomerInfo(); };
                customerSearchBox.ShowDialog();
                if (customerCode == 0)
                {
                    customerSelectToggle();
                }
                
            }
        }

        private void bntOrderReg_Click(object sender, EventArgs e)
        {
            CustomerOrderDetail customerOrderDetail = new CustomerOrderDetail();
            customerOrderDetail.StartPosition = FormStartPosition.CenterParent;

            customerOrderDetail.ShowDialog();
        }
    }
}
