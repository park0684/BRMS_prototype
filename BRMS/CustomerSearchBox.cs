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
    public partial class CustomerSearchBox : Form
    {

        cDatabaseConnect dbconn = new cDatabaseConnect();
        cDataGridDefaultSet DgrSearchBox = new cDataGridDefaultSet();
        public event Action<int> GetCustomerCode;
        public CustomerSearchBox()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedDialog;
            panelDataGrid.Controls.Add(DgrSearchBox.Dgr);
            GridForm();
            DgrSearchBox.Dgr.Dock = DockStyle.Fill;
            tBoxSearch.KeyUp += tBoxSearch_KeyUp;
            DgrSearchBox.Dgr.KeyDown += DgrSearchBox_KeyDown;
            DgrSearchBox.CellDoubleClick += DgrSearchBox_CellDoubleClick;
        }

        private void GridForm()
        {
            DgrSearchBox.Dgr.Columns.Add("custCode", "회원코드");
            DgrSearchBox.Dgr.Columns.Add("custName", "회원명");
            DgrSearchBox.Dgr.Columns.Add("custCountry", "국가");
            DgrSearchBox.Dgr.Columns.Add("custAddress", "주소");
            DgrSearchBox.Dgr.Columns.Add("custCell", "휴대폰");
            DgrSearchBox.Dgr.Columns.Add("custTell", "일반전화");
            DgrSearchBox.Dgr.Columns.Add("custEmail", "E메일");
            DgrSearchBox.Dgr.Columns["custCode"].Visible = false;

            DgrSearchBox.FormatAsStringLeft("custName", "custEmail", "custCountry", "custAddress");
            DgrSearchBox.FormatAsStringCenter("custCell", "custTell");
            DgrSearchBox.Dgr.ReadOnly = true;
            DgrSearchBox.Dgr.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DgrSearchBox.ApplyDefaultColumnSettings();
        }
        private void FillGrid(DataTable dataTable)
        {
            DgrSearchBox.Dgr.Rows.Clear();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                int i = dataTable.Rows.IndexOf(dataRow);
                DgrSearchBox.Dgr.Rows.Add();
                DgrSearchBox.Dgr.Rows[i].Cells["no"].Value = i + 1;
                DgrSearchBox.Dgr.Rows[i].Cells["custCode"].Value = dataRow["cust_code"].ToString();
                DgrSearchBox.Dgr.Rows[i].Cells["custName"].Value = dataRow["cust_name"].ToString();
                DgrSearchBox.Dgr.Rows[i].Cells["custCountry"].Value = dataRow["ctry_name"].ToString();
                DgrSearchBox.Dgr.Rows[i].Cells["custCell"].Value = dataRow["cust_cell"].ToString();
                DgrSearchBox.Dgr.Rows[i].Cells["custTell"].Value = dataRow["cust_tell"].ToString();
                DgrSearchBox.Dgr.Rows[i].Cells["custEmail"].Value = dataRow["cust_email"].ToString();
                
            }
        }
        private void SearchQuery()
        {
            string query = string.Format("SELECT cust_code, cust_name, (SELECT ctry_name FROM country WHERE ctry_code = cust_country) ctry_name, cust_cell, cust_tell, cust_email FROM customer WHERE cust_name like '%{0}%' \n union\n " +
                "SELECT cust_code, cust_name, ctry_name, cust_cell, cust_tell, cust_email FROM customer, country WHERE ctry_name LIKE '%{0}%' AND cust_country = ctry_code \n UNION \n" +
                "SELECT cust_code, cust_name, (SELECT ctry_name FROM country WHERE ctry_code = cust_country) ctry_name, cust_cell, cust_tell, cust_email FROM customer WHERE cust_email LIKE'%{0}%'", tBoxSearch.Text);
            DataTable dataTable = new DataTable(); 
            dbconn = new cDatabaseConnect();        ;
            dbconn.SqlDataAdapterQuery(query, dataTable);
            FillGrid(dataTable);
            DgrSearchBox.Dgr.Focus();
        }

        private void SelectedCustomer()
        {
            try
            {
                if (DgrSearchBox.Dgr.CurrentRow != null)
                {
                    int currentIndex = DgrSearchBox.Dgr.CurrentRow.Index;
                    int custCode = DgrSearchBox.ConvertToInt(DgrSearchBox.Dgr.Rows[currentIndex].Cells["custCode"].Value) ;
                    string custName = DgrSearchBox.Dgr.Rows[currentIndex].Cells["custName"].Value.ToString();
                    GetCustomerCode?.DynamicInvoke(custCode);
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DgrSearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectedCustomer();
            }
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                // 방향키로 셀 선택 시, 현재 선택된 셀을 커서로 이동시킵니다.
                DgrSearchBox.Dgr.BeginEdit(true); // 편집을 시작하여 커서를 이동시킵니다.
            }
        }
        private void tBoxSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchQuery();
            }
        }
        private void DgrSearchBox_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedCustomer();
        }

        private void bntOk_Click(object sender, EventArgs e)
        {
            SelectedCustomer();
        }

        private void bntSearch_Click(object sender, EventArgs e)
        {
            SearchQuery();
        }

        private void bntClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
