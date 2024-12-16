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
    public partial class SupplierLog : Form
    {
        Dictionary<int, string> accessPermission = new Dictionary<int, string>();
        int accessedEmp = 1;
        cDatabaseConnect dbconn = new cDatabaseConnect();
        cDataGridDefaultSet dgrLog = new cDataGridDefaultSet();
        static Dictionary<string, (int typeCode, string typeString)> parameter = new Dictionary<string, (int, string)>();
        public SupplierLog()
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
            dgrLog.Dgr.Columns.Add("logParam", "파라메터");
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
            dgrLog.Dgr.Columns["logParam"].Visible = false;
        }

        private void ParameterSet()
        {
            foreach (var entry in cLog.logParameter)
            {
                if (entry.Value.typeCode >= 200 && entry.Value.typeCode < 300)
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

                string query = $"SELECT sup_name FROM supplier WHERE sup_code = {row["suplog_param"]}";
                dbconn.sqlScalaQuery(query, out resultObj);
                string supName = resultObj.ToString();

                query = $"SELECT emp_name FROM employee WHERE emp_code = {row["suplog_emp"]}";
                dbconn.sqlScalaQuery(query, out resultObj);
                string empName = resultObj.ToString();

                int addRow = dgrLog.Dgr.Rows.Add();
                // 로그 데이터 설정
                string before = row["suplog_before"].ToString();
                string after = row["suplog_after"].ToString();
                switch (Convert.ToInt32(row["suplog_type"]))
                {
                    case 215:
                        before = cStatusCode.GetSupplierStatus(Convert.ToInt32(before));
                        after = cStatusCode.GetSupplierStatus(Convert.ToInt32(after));
                        break;
                    case 217:
                        before = cStatusCode.GetSupplierPayment(Convert.ToInt32(before));
                        after = cStatusCode.GetTaxStatus(Convert.ToInt32(after));
                        break;
                }
                
                string empCode = row["suplog_emp"].ToString();
                string logDate = Convert.ToDateTime(row["suplog_date"]).ToString("yyyy-MM-dd HH:mm");
                string logType = "";

                var typeInfo = parameter.Values.FirstOrDefault(x => x.typeCode == (int)row["suplog_type"]);
                if (typeInfo.typeString != null)
                {
                    logType = typeInfo.typeString;  // 해당 typeString 값을 사용
                }

                // 해당 셀에 값 설정
                dgrLog.Dgr.Rows[addRow].Cells["No"].Value = addRow + 1;
                dgrLog.Dgr.Rows[addRow].Cells["logSupname"].Value = supName;
                dgrLog.Dgr.Rows[addRow].Cells["logSupCode"].Value = row["suplog_param"];
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
            string query = $"SELECT suplog_type, suplog_before, suplog_after, suplog_param, suplog_emp, suplog_date FROM supplierlog WHERE suplog_date > '{fromDate}' AND suplog_date < '{toDate}'";
            if (cmBoxWorkType.SelectedItem is KeyValuePair<int, string> selectedItem)
            {
                query += $" AND suplog_type = {selectedItem.Key}";
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
                query += $" AND suplog_param IN ({resultString})";
            }
            query += " ORDER BY suplog_date";
            resultData.Rows.Clear();
            dbconn.SqlDataAdapterQuery(query, resultData);
            FillGrid(resultData);
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
    }
}
