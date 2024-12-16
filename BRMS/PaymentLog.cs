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
    public partial class PaymentLog : Form
    {
        Dictionary<int, string> accessPermission = new Dictionary<int, string>();
        int accessedEmp = 1;
        cDatabaseConnect dbconn = new cDatabaseConnect();
        cDataGridDefaultSet dgrLog = new cDataGridDefaultSet();
        static Dictionary<string, (int typeCode, string typeString)> parameter = new Dictionary<string, (int, string)>();
        public PaymentLog()
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
            dgrLog.Dgr.Columns.Add("logSupname", "공급사명");
            dgrLog.Dgr.Columns.Add("logSupCode", "코드");
            dgrLog.Dgr.Columns.Add("logPayDate", "결제일");
            dgrLog.Dgr.Columns.Add("logParam", "전표코드");
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
            dgrLog.FormatAsStringLeft("logSupname", "logType", "logBefore", "logAfter");
            dgrLog.FormatAsStringCenter("logEmpName", "logSupCode", "logEmp");
            
        }

        private void ParameterSet()
        {
            foreach (var entry in cLog.logParameter)
            {
                if (entry.Value.typeCode >= 500 && entry.Value.typeCode < 600)
                {
                    parameter[entry.Key] = entry.Value;
                }
            }
        }
        private void checkBoxSetting()
        {

            foreach (var entry in parameter)
            {
                cmBoxWorkType.Items.Add(new KeyValuePair<int, string>(entry.Value.typeCode, entry.Value.typeString));
            }

            cmBoxWorkType.DisplayMember = "Value"; // 사용자에게 보여질 값
            cmBoxWorkType.ValueMember = "Key";    // 내부적으로 사용할 값
            cmBoxWorkType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmBoxWorkType.SelectedValue = 0;
        }
        private void FillGrid(DataTable dataTable)
        {
            dgrLog.Dgr.Rows.Clear();
            foreach (DataRow row in dataTable.Rows)
            {
                DataTable readData = new DataTable();
                object resultObj = new object();

                string query = $"SELECT sup_code,sup_name FROM supplier WHERE sup_code = (SELECT pay_sup FROM payment WHERE pay_code = {row["paylog_param"]}) ";
                dbconn.SqlReaderQuery(query, readData);
                DataRow dataRow = readData.Rows[0];
                string supName = dataRow["sup_name"].ToString();
                string supCode = dataRow["sup_code"].ToString();
                query = $"SELECT emp_name FROM employee WHERE emp_code = {row["paylog_emp"]}";
                dbconn.sqlScalaQuery(query, out resultObj);
                string empName = resultObj.ToString();

                int addRow = dgrLog.Dgr.Rows.Add();
                // 로그 데이터 설정
                string before = row["paylog_before"].ToString();
                string after = row["paylog_after"].ToString();
                if(Convert.ToInt32(row["paylog_type"]) == 515)
                {
                    before = cStatusCode.GetSupplierStatus(Convert.ToInt32(before));
                    after = cStatusCode.GetSupplierStatus(Convert.ToInt32(after));
                }
                int payCode = Convert.ToInt32(row["paylog_param"]);
                query = $"SELECT pay_date FROM payment WHERE pay_code = {row["paylog_param"]} ";
                dbconn.sqlScalaQuery(query, out resultObj);
                string payDate = Convert.ToDateTime(resultObj).ToString("yyyy-MM-dd HH:mm");
                string empCode = row["paylog_emp"].ToString();
                string logDate = Convert.ToDateTime(row["paylog_date"]).ToString("yyyy-MM-dd HH:mm");
                string logType = "";

                var typeInfo = parameter.Values.FirstOrDefault(x => x.typeCode == (int)row["paylog_type"]);
                if (typeInfo.typeString != null)
                {
                    logType = typeInfo.typeString;  // 해당 typeString 값을 사용
                }

                // 해당 셀에 값 설정
                dgrLog.Dgr.Rows[addRow].Cells["No"].Value = addRow + 1;
                dgrLog.Dgr.Rows[addRow].Cells["logSupname"].Value = supName;
                dgrLog.Dgr.Rows[addRow].Cells["logSupCode"].Value = supCode;
                dgrLog.Dgr.Rows[addRow].Cells["logPayDate"].Value = payDate;
                dgrLog.Dgr.Rows[addRow].Cells["logParam"].Value = payCode;
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
            string query = $"SELECT paylog_type, paylog_before, paylog_after, paylog_param, paylog_emp, paylog_date FROM paymentlog WHERE paylog_date > '{fromDate}' AND paylog_date < '{toDate}'";
            if (cmBoxWorkType.SelectedItem is KeyValuePair<int, string> selectedItem)
            {
                query += $" AND paylog_type = {selectedItem.Key}";
            }
            if (!string.IsNullOrEmpty(tBoxSearch.Text))
            {
                string supQuery = $"SELECT distinct(sup_code) FROM supplier WHERE sup_name LIKE '%{tBoxSearch.Text}%'";
                dbconn.SqlDataAdapterQuery(supQuery, resultData);
                string resultString = "";
                foreach (DataRow supRow in resultData.Rows)
                {
                    if (string.IsNullOrEmpty(resultString))
                    {
                        resultString = supRow[0].ToString();
                    }
                    resultString += ", " + supRow[0].ToString();
                }
                query += $" AND paylog_param IN ({resultString})";
            }
            query += " ORDER BY paylog_date";
            resultData.Rows.Clear();
            dbconn.SqlDataAdapterQuery(query, resultData);
            FillGrid(resultData);
        }
        public void RunQuery()
        {
            try
            {
                QuerySetting();
                cLog.InsertEmpAccessLogNotConnect("@paymentLogSearch", accessedEmp, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
