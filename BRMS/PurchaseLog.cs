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
    public partial class PurchaseLog : Form
    {
        Dictionary<int, string> accessPermission = new Dictionary<int, string>();
        int accessedEmp = 1;
        cDatabaseConnect dbconn = new cDatabaseConnect();
        cDataGridDefaultSet dgrLog = new cDataGridDefaultSet();
        static Dictionary<string, (int typeCode, string typeString)> parameter = new Dictionary<string, (int, string)>();
        public PurchaseLog()
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
            dgrLog.Dgr.Columns.Add("logOrderType", "구분");
            dgrLog.Dgr.Columns.Add("logSupname", "공급사명");
            dgrLog.Dgr.Columns.Add("logSupCode", "코드");
            dgrLog.Dgr.Columns.Add("logPurDate", "매입/발주일");
            dgrLog.Dgr.Columns.Add("logParam", "파라메터");
            dgrLog.Dgr.Columns.Add("logParam2", "상세");
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
                if (entry.Value.typeCode >= 300 && entry.Value.typeCode < 500)
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
                string query = "";
                //공급사 
                if (row["purlog_type"].ToString().Substring(0, 1) == "3")
                {
                    query = $"SELECT sup_name,sup_code FROM purchase, supplier WHERE pur_code = {row["purlog_param"]} AND sup_code = pur_sup";
                }
                else
                {
                    query = $"SELECT sup_name,sup_code FROM purorder, supplier WHERE pord_code = {row["purlog_param"]} AND sup_code = pord_sup";
                }
                dbconn.SqlReaderQuery(query, readData);
                DataRow dataRow = readData.Rows[0];
                string supName = dataRow["sup_name"].ToString();
                string supcode = dataRow["sup_code"].ToString();
                readData.Clear();
                //매입일
                query = $"Select pur_date FROM purchase WHERE pur_code = {row["purlog_param"]}";
                dbconn.sqlScalaQuery(query, out resultObj);
                string purDate = Convert.ToDateTime(resultObj).ToString("yyyy-MM-dd HH:mm");
                //작업자
                query = $"SELECT emp_name FROM employee WHERE emp_code = {row["purlog_emp"]}";
                dbconn.sqlScalaQuery(query, out resultObj);
                string empName = resultObj.ToString();
                //구분
                string orderType = Convert.ToInt32(row["purlog_order"]) == 1 ? "매입" : "발주";
                int addRow = dgrLog.Dgr.Rows.Add();
                // 로그 데이터 설정
                string before = row["purlog_before"].ToString();
                string after = row["purlog_after"].ToString();
                string parameter2 = row["purlog_param2"].ToString(); ;
                if(parameter2 != "0")
                {
                    query = $"SELECT pdt_name_kr,pdt_number FROM product WHERE pdt_code = {parameter2}";
                    dbconn.SqlReaderQuery(query, readData);
                    DataRow pdtRow = readData.Rows[0];
                    parameter2 = $"{pdtRow[0].ToString().Trim()}({pdtRow[1].ToString().Trim()})";
                }
                switch (Convert.ToInt32(row["purlog_type"]))
                {
                    case 302: // 매입 공급사 변경
                        query = $"SELECT sup_name FROM supplier WHERE sup_code = {before}";
                        dbconn.sqlScalaQuery(query, out resultObj);
                        before = resultObj.ToString();
                        query = $"SELECT sup_name FROM supplier WHERE sup_code = {after}";
                        dbconn.sqlScalaQuery(query, out resultObj);
                        after = resultObj.ToString();
                        break;
                    case 305: // 매입 유형 변경
                        before = cStatusCode.GetPurchaseType(Convert.ToInt32(before));
                        after = cStatusCode.GetPurchaseType(Convert.ToInt32(after));
                        break;
                    case 315: // 매입상품의 매입상태 변경
                        before = cStatusCode.GetCustomerStatus(Convert.ToInt32(before));
                        after = cStatusCode.GetCustomerStatus(Convert.ToInt32(after));
                        break;
                    case 401: // 발주 공급사 변경
                        query = $"SELECT sup_name FROM supplier WHERE sup_code = {before}";
                        dbconn.sqlScalaQuery(query, out resultObj);
                        before = resultObj.ToString();
                        query = $"SELECT sup_name FROM supplier WHERE sup_code = {after}";
                        dbconn.sqlScalaQuery(query, out resultObj);
                        after = resultObj.ToString();
                        break;
                    case 405: // 발주서 상태 변경
                        before = cStatusCode.GetPurchaseOrderStatus(Convert.ToInt32(before));
                        after = cStatusCode.GetPurchaseOrderStatus(Convert.ToInt32(after));
                        break;
                    case 416: // 발주상품의 발주상태 변경
                        before = cStatusCode.GetCustomerStatus(Convert.ToInt32(before));
                        after = cStatusCode.GetCustomerStatus(Convert.ToInt32(after));
                        break;
                }

                string empCode = row["purlog_emp"].ToString();
                string logDate = Convert.ToDateTime(row["purlog_date"]).ToString("yyyy-MM-dd HH:mm");
                string logType = "";

                var typeInfo = parameter.Values.FirstOrDefault(x => x.typeCode == (int)row["purlog_type"]);
                if (typeInfo.typeString != null)
                {
                    logType = typeInfo.typeString;  // 해당 typeString 값을 사용
                }

                // 해당 셀에 값 설정
                dgrLog.Dgr.Rows[addRow].Cells["No"].Value = addRow + 1;
                dgrLog.Dgr.Rows[addRow].Cells["logOrderType"].Value = orderType;
                dgrLog.Dgr.Rows[addRow].Cells["logSupname"].Value = supName;
                dgrLog.Dgr.Rows[addRow].Cells["logSupCode"].Value = supcode;
                dgrLog.Dgr.Rows[addRow].Cells["logParam"].Value = Convert.ToInt32(row["purlog_param"]);
                dgrLog.Dgr.Rows[addRow].Cells["logParam2"].Value = parameter2;
                dgrLog.Dgr.Rows[addRow].Cells["logPurDate"].Value = purDate;
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
            string query = $"SELECT purlog_type, purlog_before, purlog_after, purlog_param, purlog_param2,purlog_order, purlog_emp, purlog_date FROM purchaselog WHERE purlog_date > '{fromDate}' AND purlog_date < '{toDate}'";
            if (cmBoxWorkType.SelectedItem is KeyValuePair<int, string> selectedItem)
            {
                query += $" AND purlog_type = {selectedItem.Key}";
            }
            query += " ORDER BY purlog_date";
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
