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
    public partial class CustomerList : Form
    {
        cDatabaseConnect dbconn = new cDatabaseConnect();
        cDataGridDefaultSet custList = new cDataGridDefaultSet();
        private cCryptor cryptor = new cCryptor("YourPassphrase");
        
        Dictionary<int, string> accessPermission = new Dictionary<int, string>();
        int accessedEmp = 0;
        public CustomerList()
        {
            InitializeComponent();
            panelDataGrid.Controls.Add(custList.Dgr);
            custList.Dgr.Dock = DockStyle.Fill;
            ComBoxSetting();
            this.Load += CustomerList_Load;
            checkBoxSaveDate.CheckedChanged += new EventHandler(checkBoxSaveDate_checked);
            checkBoxSaleDate.CheckedChanged += new EventHandler(checkBoxSaleDate_checked);
            tBoxSearchWord.KeyDown += tBoxSearchWord_KeyDown;
            custList.CellDoubleClick += custList_DoubleClick;
            GridForm();
            
        }

        private void CustomerList_Load(object sender, EventArgs e)
        {
            tBoxSearchWord.Focus();  // 폼이 로드된 후 tBoxSearchWord에 포커스 설정
            checkBoxSaveDate.Checked = false;
            checkBoxSaleDate.Checked = false;
            checkBoxSaveDate_checked(checkBoxSaveDate, EventArgs.Empty);
            checkBoxSaleDate_checked(checkBoxSaleDate, EventArgs.Empty);
        }

        private void GridForm()
        {
            custList.Dgr.Columns.Add("custCode", "회원코드");
            custList.Dgr.Columns.Add("custStatus", "상태");
            custList.Dgr.Columns.Add("custName", "회원명");
            custList.Dgr.Columns.Add("custCountry", "국가코드");
            custList.Dgr.Columns.Add("custCountryName", "국가");
            custList.Dgr.Columns.Add("custTel", "전화");
            custList.Dgr.Columns.Add("custCell", "휴대폰");
            custList.Dgr.Columns.Add("custEmail", "이메일");
            custList.Dgr.Columns.Add("custAddress", "주소");
            custList.Dgr.Columns.Add("custRegDate", "등록일");
            custList.Dgr.Columns.Add("custSaleDate", "최종거래일");
            custList.Dgr.Columns.Add("custMemo", "메모");

            custList.FormatAsStringCenter("custStatus", "custCountry", "custTel", "custCell");
            custList.FormatAsStringLeft("custName", "custEmail", "custAddress", "custMemo");
            custList.FormatAsDate("custRegDate", "custSaleDate");
            custList.ApplyDefaultColumnSettings();
            custList.Dgr.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            custList.Dgr.Columns["custCode"].Visible = false;
            custList.Dgr.Columns["custCountry"].Visible = false;
            custList.Dgr.ReadOnly = true;
        }

        private void ComBoxSetting()
        {
            cmBoxSaveDate.DropDownStyle = ComboBoxStyle.DropDownList;
            cmBoxSaveDate.Items.AddRange(new string[] { "등록일", "수정일" });
            cmBoxSaveDate.SelectedIndex = 0;

            cmBoxSaleDate.DropDownStyle = ComboBoxStyle.DropDownList;
            cmBoxSaleDate.Items.AddRange(new string[] { "기간내 거래", "최종 거래일" });
            cmBoxSaleDate.SelectedIndex = 0;

            cmBoxStatus.Items.Add("전체");
            foreach (var status in cStatusCode.SupplierPayment)
            {
                cmBoxStatus.Items.Add(new KeyValuePair<int, string>(status.Key, status.Value));
            }
            cmBoxStatus.DisplayMember = "Value"; // 사용자에게 보여질 값
            cmBoxStatus.ValueMember = "Key";    // 내부적으로 사용할 값
            cmBoxStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmBoxStatus.SelectedIndex = 0;            
        }

        private void checkBoxSaveDate_checked(object sender, EventArgs e)
        {
            if(checkBoxSaveDate.Checked)
            {
                cmBoxSaveDate.Enabled = true;
                dtpSaveDateFrom.Enabled = true;
                dtpSaveDateTo.Enabled = true;
            }
            else
            {
                cmBoxSaveDate.Enabled = false;
                dtpSaveDateFrom.Enabled = false;
                dtpSaveDateTo.Enabled = false;
            }
        }
        private void checkBoxSaleDate_checked(object sender, EventArgs e)
        {
            if (checkBoxSaleDate.Checked)
            {
                cmBoxSaleDate.Enabled = true;
                dtpSaleDateFrom.Enabled = true;
                dtpSaleDateTo.Enabled = true;
            }
            else
            {
                cmBoxSaleDate.Enabled = false;
                dtpSaleDateFrom.Enabled = false;
                dtpSaleDateTo.Enabled = false;
            }
        }
        private void QuerySetting()
        {
            DataTable resultData = new DataTable();
            string queryBase = "SELECT cust_code, cust_name, cust_country,ctry_name, " +
                "cust_tell, cust_cell, cust_email, cust_addr, cust_idate, cust_lastsaledate, cust_status, cust_memo FROM customer, country WHERE cust_country = ctry_code ";
            List<string> queries = new List<string>();  // UNION으로 결합할 쿼리 리스트
            List<string> conditions = new List<string>();

            string word = tBoxSearchWord.Text;

            // 검색어에 따른 쿼리 생성 (UNION 유지)
            if (!string.IsNullOrEmpty(word))
            {
                string searchWordCondition = $"LIKE '%{word}%'";
                queries.Add(queryBase + $" AND cust_name {searchWordCondition}");
                queries.Add(queryBase + $" AND cust_email {searchWordCondition}");
                queries.Add(queryBase + $" AND cust_addr {searchWordCondition}");
                queries.Add(queryBase + $" AND cust_country IN (SELECT ctry_code FROM country WHERE ctry_name {searchWordCondition})");
            }
            else
            {
                queries.Add(queryBase);  // 검색어가 없으면 기본 쿼리만 사용
            }

            // 상태 필터링
            if (cmBoxStatus.SelectedIndex != 0)
            {
                var selectedItem = (KeyValuePair<int, string>)cmBoxStatus.SelectedItem;
                string statusCondition = $"cust_status = {selectedItem.Key}";
                conditions.Add(statusCondition);
            }

            // 등록일 또는 수정일 필터링
            if (checkBoxSaveDate.Checked)
            {
                DateTime fromDate = dtpSaveDateFrom.Value;
                DateTime toDate = dtpSaveDateTo.Value.AddDays(1);  // 종료 날짜에 하루를 더함
                string saveDateCondition = cmBoxSaveDate.SelectedIndex == 0 ? 
                    $"cust_idate BETWEEN '{fromDate:yyyy-MM-dd}' AND '{toDate:yyyy-MM-dd}'" : $"cust_udate BETWEEN '{fromDate:yyyy-MM-dd}' AND '{toDate:yyyy-MM-dd}'";
                conditions.Add(saveDateCondition);
            }

            // 거래일 필터링
            if (checkBoxSaleDate.Checked)
            {
                DateTime fromDate = dtpSaleDateFrom.Value;
                DateTime toDate = dtpSaleDateTo.Value.AddDays(1);
                string saleDateCondition = cmBoxSaleDate.SelectedIndex == 0
                    ? $"cust_code IN (SELECT sale_cust FROM sales WHERE sale_date BETWEEN '{fromDate:yyyy-MM-dd}' AND '{toDate:yyyy-MM-dd}')"
                    : $"cust_lastsaledate BETWEEN '{fromDate:yyyy-MM-dd}' AND '{toDate:yyyy-MM-dd}'";
                conditions.Add(saleDateCondition);
            }

            // 각 쿼리에 조건을 적용 (조건을 WHERE가 있으면 AND, 없으면 WHERE로 처리)
            for (int i = 1; i < queries.Count; i++)
            {
                queries[i] += " AND " + string.Join(" AND ", conditions);
            }
            // 최종 쿼리 결합 (UNION으로 검색어 관련 쿼리 결합 후, 조건 추가)
            string finalQuery = string.Join("\n UNION \n", queries);

            // 결과 데이터베이스 조회 및 그리드 업데이트
            dbconn.SqlDataAdapterQuery(finalQuery, resultData);
            GridFill(resultData);
            cLog.InsertEmpAccessLogNotConnect("@customerSearch", accessedEmp, 0);
        }
       
        private void GridFill(DataTable dataTable)
        {
            //int index = 0;
            custList.Dgr.Rows.Clear();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                var rowIndex = custList.Dgr.Rows.Add();
                var row = custList.Dgr.Rows[rowIndex];            
                
                row.Cells["No"].Value = custList.Dgr.RowCount;
                row.Cells["custCode"].Value = dataRow["cust_code"];
                row.Cells["custStatus"].Value = dataRow["cust_status"].ToString() == "1" ? $"유효" : $"무효";
                row.Cells["custName"].Value = dataRow["cust_name"];
                row.Cells["custCountry"].Value = dataRow["cust_country"];
                row.Cells["custCountryName"].Value = dataRow["ctry_name"];
                row.Cells["custTel"].Value = dataRow["cust_tell"].ToString();
                row.Cells["custCell"].Value = dataRow["cust_cell"].ToString();
                row.Cells["custEmail"].Value = dataRow["cust_email"];
                row.Cells["custAddress"].Value = dataRow["cust_addr"];
                row.Cells["custRegDate"].Value = dataRow["cust_idate"];
                row.Cells["custSaleDate"].Value = dataRow["cust_lastsaledate"];
                row.Cells["custMemo"].Value = dataRow["cust_memo"];

            }
        }
        public void RunQuery()
        {
            try
            {
                QuerySetting();
                cLog.InsertEmpAccessLogNotConnect("@customerSearch", accessedEmp, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ExportExcel()
        {
            try
            {
                custList.ExportToExcel();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void tBoxSearchWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                RunQuery();
            }
        }
        private void custList_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int custCode = custList.ConvertToInt(custList.Dgr.CurrentRow.Cells["custCode"].Value);
            CustomerDetail customerDetail = new CustomerDetail();
            customerDetail.SearchCustomer(custCode);
            cLog.InsertEmpAccessLogNotConnect("@customerSearch", accessedEmp, custCode);
            customerDetail.StartPosition = FormStartPosition.CenterParent;
            customerDetail.ShowDialog();
        }
        private void bntCustomerAdd_Click(object sender, EventArgs e)
        {
            CustomerDetail customerDetail = new CustomerDetail();
            customerDetail.CustAdd();
            customerDetail.StartPosition = FormStartPosition.CenterParent;
            customerDetail.ShowDialog();
            
        }
    }
}
