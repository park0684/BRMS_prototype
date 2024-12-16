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
    public partial class CustomerLog : Form
    {
        Dictionary<int, string> accessPermission = new Dictionary<int, string>();
        int accessedEmp = 1;
        cDatabaseConnect dbconn = new cDatabaseConnect();
        cDataGridDefaultSet dgrLog = new cDataGridDefaultSet();
        static Dictionary<string, (int typeCode, string typeString)> parameter = new Dictionary<string, (int, string)>();
        public CustomerLog()
        {
            InitializeComponent();
            pnlDataGrid.Controls.Add(dgrLog.Dgr);
            dgrLog.Dgr.Dock = DockStyle.Fill;
            ParameterSet();
            checkBoxSetting();
            DataGridFrom();
        }
        private void DataGridFrom()
        {
            dgrLog.Dgr.Columns.Add("logCustName", "회원명");            
            dgrLog.Dgr.Columns.Add("logParam", "코드");
            dgrLog.Dgr.Columns.Add("logType", "작업내역");
            dgrLog.Dgr.Columns.Add("logBefore", "변경전");
            dgrLog.Dgr.Columns.Add("logAfter", "변경후");
            dgrLog.Dgr.Columns.Add("logEmpName", "작업자명");
            dgrLog.Dgr.Columns.Add("logEmp", "직원코드");
            dgrLog.Dgr.Columns.Add("logDate", "변경일");
            dgrLog.Dgr.ReadOnly = true;
            dgrLog.ApplyDefaultColumnSettings();
            dgrLog.Dgr.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgrLog.FormatAsDateTime("logDate");
            dgrLog.FormatAsStringLeft("logCustName", "logType", "logBefore", "logAfter");
            dgrLog.FormatAsStringCenter("logEmpName", "logSupCode", "logEmp", "logParam");

        }

        private void ParameterSet()
        {
            foreach (var entry in cLog.logParameter)
            {
                if (entry.Value.typeCode >= 700 && entry.Value.typeCode < 800)
                {
                    parameter[entry.Key] = entry.Value;
                }
            }
        }
        private void checkBoxSetting()
        {
            cmBoxWorkType.Items.Add("전체");
            foreach (var entry in parameter)
            {
                cmBoxWorkType.Items.Add(new KeyValuePair<int, string>(entry.Value.typeCode, entry.Value.typeString));
            }

            cmBoxWorkType.DisplayMember = "Value"; // 사용자에게 보여질 값
            cmBoxWorkType.ValueMember = "Key";    // 내부적으로 사용할 값
            cmBoxWorkType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmBoxWorkType.SelectedIndex = 0;
        }
        private void FillGrid(DataTable dataTable)
        {
            dgrLog.Dgr.Rows.Clear();
            foreach (DataRow row in dataTable.Rows)
            {
                DataTable readData = new DataTable();
                object resultObj = new object();
                int custCode = Convert.ToInt32(row["custlog_param"]);
                string query = $"SELECT cust_name FROM customer WHERE cust_code = {custCode} ";
                dbconn.sqlScalaQuery(query, out resultObj);
                
                string custName = resultObj.ToString();
                
                query = $"SELECT emp_name FROM employee WHERE emp_code = {row["custlog_emp"]}";
                dbconn.sqlScalaQuery(query, out resultObj);
                string empName = resultObj.ToString();

                int addRow = dgrLog.Dgr.Rows.Add();
                // 로그 데이터 설정
                string before = row["custlog_before"].ToString();
                string after = row["custlog_after"].ToString();
                switch (Convert.ToInt32(row["custlog_type"]))
                {
                    case 706://국가
                        query = $"SELECT ctry_name FROM country WHERE ctry_code = {before}";
                        dbconn.sqlScalaQuery(query, out resultObj);
                        before = resultObj.ToString();
                        query = $"SELECT ctry_name FROM country WHERE ctry_code = {after}";
                        dbconn.sqlScalaQuery(query, out resultObj);
                        after = resultObj.ToString();
                        break;
                    case 707://회원상태
                        before = cStatusCode.GetCustomerStatus(Convert.ToInt32(before));
                        after = cStatusCode.GetCustomerStatus(Convert.ToInt32(after));
                        break;
                }

                string empCode = row["custlog_emp"].ToString();
                string logDate = Convert.ToDateTime(row["custlog_date"]).ToString("yyyy-MM-dd HH:mm");
                string logType = "";

                var typeInfo = parameter.Values.FirstOrDefault(x => x.typeCode == (int)row["custlog_type"]);
                if (typeInfo.typeString != null)
                {
                    logType = typeInfo.typeString;  // 해당 typeString 값을 사용
                }

                // 해당 셀에 값 설정
                dgrLog.Dgr.Rows[addRow].Cells["No"].Value = addRow + 1;
                dgrLog.Dgr.Rows[addRow].Cells["logCustName"].Value = custName;;
                dgrLog.Dgr.Rows[addRow].Cells["logParam"].Value = custCode;
                dgrLog.Dgr.Rows[addRow].Cells["logType"].Value = logType;  // 여기서 값을 설정
                dgrLog.Dgr.Rows[addRow].Cells["logBefore"].Value = before;
                dgrLog.Dgr.Rows[addRow].Cells["logAfter"].Value = after;
                dgrLog.Dgr.Rows[addRow].Cells["logEmpName"].Value = empName;
                dgrLog.Dgr.Rows[addRow].Cells["logEmp"].Value = empCode;
                dgrLog.Dgr.Rows[addRow].Cells["logDate"].Value = logDate;
            }
        }
        private void QuerySetting()
        {
            string fromDate = dtpDateFrom.Value.ToString("yyyy-MM-dd");
            string toDate = dtpDateTo.Value.AddDays(1).ToString("yyyy-MM-dd");
            DataTable resultData = new DataTable();
            string query = $"SELECT custlog_type, custlog_before, custlog_after, custlog_param, custlog_emp, custlog_date FROM customerlog WHERE custlog_date > '{fromDate}' AND custlog_date < '{toDate}'";
            if(cmBoxWorkType.SelectedItem is KeyValuePair<int, string> selectedItem)
            {
                query += $" AND custlog_type = {selectedItem.Key}";
            }

            if (!string.IsNullOrEmpty(tBoxSearch.Text))
            {
                string subQuery = $"SELECT distinct(cust_code) FROM customer WHERE cust_name LIKE '%{tBoxSearch.Text}%'";
                dbconn.SqlDataAdapterQuery(subQuery, resultData);
                string resultString = "";
                foreach (DataRow subRow in resultData.Rows)
                {
                    if (string.IsNullOrEmpty(resultString))
                    {
                        resultString = subRow[0].ToString();
                    }
                    resultString += ", " + subRow[0].ToString();
                }
                query += $"AND custlog_param IN ({resultString})";
            }
            query += "ORDER BY custlog_date";
            resultData.Rows.Clear();
            dbconn.SqlDataAdapterQuery(query, resultData);
            FillGrid(resultData);
        }
        public void RunQuery()
        {
            try
            {
                QuerySetting();
                cLog.InsertEmpAccessLogNotConnect("@customerLogSearch", accessedEmp, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
