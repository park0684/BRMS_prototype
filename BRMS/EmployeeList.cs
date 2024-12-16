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
    public partial class EmployeeList : Form
    {
        cDatabaseConnect dbconn = new cDatabaseConnect();
        cDataGridDefaultSet dgrEmp = new cDataGridDefaultSet();
        int accessedEmp = 1;
        public EmployeeList()
        {
            InitializeComponent();
            cmBoxStatus.Items.Clear();
            InitializeComboBox();
            panelDataGrid.Controls.Add(dgrEmp.Dgr);
            dgrEmp.Dgr.Dock = DockStyle.Fill;
            FormGrid();
            dgrEmp.CellDoubleClick += dgrEmp_CellDoubleClick;
        }
        private void InitializeComboBox()
        {
           
            foreach (var status in cStatusCode.EmployeeStatus)
            {
                cmBoxStatus.Items.Add(new KeyValuePair<int, string>(status.Key, status.Value));
            }
            cmBoxStatus.Items.Add("전체");
           
            cmBoxStatus.DisplayMember = "Value"; // 사용자에게 보여질 값
            cmBoxStatus.ValueMember = "Key";    // 내부적으로 사용할 값
            cmBoxStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmBoxStatus.SelectedIndex = 1;
        }
        private void FormGrid()
        {
            dgrEmp.Dgr.Columns.Add("empCode", "직원코드");
            dgrEmp.Dgr.Columns.Add("empName", "직원명");
            dgrEmp.Dgr.Columns.Add("empLevel", "직책");
            dgrEmp.Dgr.Columns.Add("empCell", "연락처");
            dgrEmp.Dgr.Columns.Add("empEmail", "이메일");
            dgrEmp.Dgr.Columns.Add("empAddr", "주소");
            dgrEmp.Dgr.Columns.Add("empStatus", "상태");
            dgrEmp.Dgr.Columns.Add("empIdate", "등록일");
            dgrEmp.Dgr.Columns.Add("empUdate", "수정일");
            dgrEmp.Dgr.Columns.Add("emptMemo", "메모");
            dgrEmp.Dgr.ReadOnly = true;
            dgrEmp.Dgr.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgrEmp.FormatAsDateTime("empIdate", "empUdate");
            dgrEmp.FormatAsStringCenter("empName", "empLevel", "empCell", "empEmail", "empStatus");
            dgrEmp.FormatAsStringLeft("empAddr", "emptMemo");
            dgrEmp.Dgr.Columns["empCode"].Visible = false;
            dgrEmp.ApplyDefaultColumnSettings();

        }
        public void RunQuery()
        {
            dgrEmp.Dgr.Rows.Clear();
            string query = "SELECT emp_code,emp_name, emp_level, emp_cell, emp_email, emp_addr, emp_status,emp_idate, emp_udate, emp_memo FROM employee";
            if(cmBoxStatus.SelectedIndex != cmBoxStatus.Items.Count - 1)
            {
                query += $" WHERE emp_status = {cmBoxStatus.SelectedIndex}";
            }
            DataTable resultData = new DataTable();
            dbconn.SqlDataAdapterQuery(query, resultData);
            GridFill(resultData);
            cLog.InsertEmpAccessLogNotConnect("@employeeSearch", accessedEmp, 0);
        }

        private void GridFill(DataTable dataTable)
        {
            foreach(DataRow row in dataTable.Rows)
            {
                int newRow = dgrEmp.Dgr.Rows.Add();
                dgrEmp.Dgr.Rows[newRow].Cells["No"].Value = newRow + 1;
                dgrEmp.Dgr.Rows[newRow].Cells["empCode"].Value = row["emp_code"];
                dgrEmp.Dgr.Rows[newRow].Cells["empName"].Value = row["emp_name"];
                dgrEmp.Dgr.Rows[newRow].Cells["empLevel"].Value = row["emp_level"];
                dgrEmp.Dgr.Rows[newRow].Cells["empCell"].Value = row["emp_cell"];
                dgrEmp.Dgr.Rows[newRow].Cells["empEmail"].Value = row["emp_email"];
                dgrEmp.Dgr.Rows[newRow].Cells["empAddr"].Value = row["emp_addr"];
                dgrEmp.Dgr.Rows[newRow].Cells["empStatus"].Value = cStatusCode.GetEmployeeStatus(Convert.ToInt32(row["emp_status"]));
                dgrEmp.Dgr.Rows[newRow].Cells["empIdate"].Value = row["emp_idate"];
                dgrEmp.Dgr.Rows[newRow].Cells["empUdate"].Value = row["emp_udate"];
                dgrEmp.Dgr.Rows[newRow].Cells["emptMemo"].Value = row["emp_memo"];
            }

        }

        private void CallEmployeeDetail(int empCode)
        {
            EmployeeDetail employeeDetail = new EmployeeDetail();
            employeeDetail.StartPosition = FormStartPosition.CenterParent;
            employeeDetail.CallEmployee(empCode);           
            //productDetail.refresh += (refreshCode) => refresh = refreshCode;
            employeeDetail.ShowDialog();
        }
        private void dgrEmp_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int currentRowIndex = e.RowIndex;
            if (currentRowIndex >= 0)
            {
                DataGridViewRow currentRow = dgrEmp.Dgr.Rows[currentRowIndex];
                int empCode = dgrEmp.ConvertToInt(currentRow.Cells["empCode"].Value);
                CallEmployeeDetail(empCode);                
            }
        }
        private void bntAddEmployee_Click(object sender, EventArgs e)
        {
            CallEmployeeDetail(0);
        }
    }
}
