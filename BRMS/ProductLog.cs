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
    public partial class ProductLog : Form
    {
        Dictionary<int, string> accessPermission = new Dictionary<int, string>();
        int accessedEmp = 1;
        cDatabaseConnect dbconn = new cDatabaseConnect();
        cDataGridDefaultSet dgrLog = new cDataGridDefaultSet();
        static Dictionary<string, (int typeCode, string typeString)> parameter = new Dictionary<string, (int, string)>();
        public ProductLog()
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
            dgrLog.Dgr.Columns.Add("logPdtNake", "제품명");
            dgrLog.Dgr.Columns.Add("logPdtNumber", "제품번호");
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
            dgrLog.FormatAsStringLeft("logPdtNake", "logPdtNumber", "logType", "logBefore", "logAfter");
            dgrLog.FormatAsStringCenter("logEmpName", "logEmp");
            dgrLog.Dgr.Columns["logParam"].Visible = false;
        }
        
        private void ParameterSet()
        {
            foreach (var entry in cLog.logParameter)
            {
                if (entry.Value.typeCode >= 100 && entry.Value.typeCode < 200)
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
                string query = $"SELECT pdt_name_kr, pdt_number  FROM product WHERE pdt_code = {row["pdtlog_param"]}";
                dbconn.SqlReaderQuery(query, readData);
                query = $"SELECT emp_name FROM employee WHERE emp_code = {row["pdtlog_emp"]}";
                dbconn.sqlScalaQuery(query, out resultObj);
                DataRow pdtRow = readData.Rows[0];
                int addRow = dgrLog.Dgr.Rows.Add();
                string empName = resultObj.ToString();
                // 로그 데이터 설정
                string before = row["pdtlog_before"].ToString();
                string after = row["pdtlog_after"].ToString();
                switch(Convert.ToInt32(row["pdtlog_type"]))
                {
                    case 103:
                        query = $"SELECT sup_name FROM supplier WHERE sup_code = {before}";
                        dbconn.sqlScalaQuery(query, out resultObj);
                        before = resultObj.ToString();
                        query = $"SELECT sup_name FROM supplier WHERE sup_code = {after}";
                        dbconn.sqlScalaQuery(query, out resultObj);
                        after = resultObj.ToString();
                        break;
                    case 104:
                        string[] beforeParts = before.Split('_');
                        int beforeTop = int.Parse(beforeParts[0]);
                        int beforeMid = int.Parse(beforeParts[1]);
                        int beforeBot = int.Parse(beforeParts[2]);

                        // pdtlog_after를 "1_1_1" 형식으로 분리
                        string[] afterParts = after.Split('_');
                        int afterTop = int.Parse(afterParts[0]);
                        int afterMid = int.Parse(afterParts[1]);
                        int afterBot = int.Parse(afterParts[2]);


                        query = $"SELECT cat_name_kr FROM category WHERE cat_top = {beforeTop}";
                        dbconn.sqlScalaQuery(query, out resultObj);
                        before = resultObj.ToString();
                        query = $"SELECT cat_name_kr FROM category WHERE cat_top = {beforeTop} AND cat_mid ={beforeMid}";
                        dbconn.sqlScalaQuery(query, out resultObj);
                        before += "▶" + resultObj.ToString();
                        query = $"SELECT cat_name_kr FROM category WHERE cat_top = {beforeTop} AND cat_mid ={beforeMid} AND cat_bot = {beforeBot}";
                        dbconn.sqlScalaQuery(query, out resultObj);
                        before += "▶" + resultObj.ToString();

                        query = $"SELECT cat_name_kr FROM category WHERE cat_top = {afterTop}";
                        dbconn.sqlScalaQuery(query, out resultObj);
                        after = resultObj.ToString();
                        query = $"SELECT cat_name_kr FROM category WHERE cat_top = {afterTop} AND cat_mid ={afterMid}";
                        dbconn.sqlScalaQuery(query, out resultObj);
                        after += "▶" + resultObj.ToString();
                        query = $"SELECT cat_name_kr FROM category WHERE cat_top = {afterTop} AND cat_mid ={afterMid} AND cat_bot = {afterBot}";
                        dbconn.sqlScalaQuery(query, out resultObj);
                        after += "▶" + resultObj.ToString();
                        break;
                    case 105:
                        before = cStatusCode.GetProductStatus(Convert.ToInt32(before));
                        after = cStatusCode.GetProductStatus(Convert.ToInt32(after));
                        break;
                    case 113:
                        before = cStatusCode.GetTaxStatus(Convert.ToInt32(before));
                        after = cStatusCode.GetTaxStatus(Convert.ToInt32(after));
                        break;
                }
                
                
                string pdtName = pdtRow["pdt_name_kr"].ToString();
                string pdtNumber = pdtRow["pdt_number"].ToString();
                
                string empCode = row["pdtlog_emp"].ToString();
                string logDate = Convert.ToDateTime(row["pdtlog_date"]).ToString("yyyy-MM-dd HH:mm");
                string logType = "";

                var typeInfo = parameter.Values.FirstOrDefault(x => x.typeCode == (int)row["pdtlog_type"]);
                if (typeInfo.typeString != null)
                {
                    logType = typeInfo.typeString;  // 해당 typeString 값을 사용
                }

                // 해당 셀에 값 설정
                dgrLog.Dgr.Rows[addRow].Cells["No"].Value = addRow + 1;
                dgrLog.Dgr.Rows[addRow].Cells["logPdtNake"].Value = pdtName;
                dgrLog.Dgr.Rows[addRow].Cells["logPdtNumber"].Value = pdtNumber;
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
            string query = $"SELECT pdtlog_type, pdtlog_before, pdtlog_after, pdtlog_param, pdtlog_emp, pdtlog_date FROM productlog WHERE pdtlog_date > '{fromDate}' AND pdtlog_date < '{toDate}'";
            if (cmBoxWorkType.SelectedItem is KeyValuePair<int, string> selectedItem)
            {
                query += $" AND pdtlog_type = {selectedItem.Key}";
            }
            if (!string.IsNullOrEmpty(tBoxSearch.Text))
            {
                string pdtQuery = $"SELECT distinct(pdt_code) FROM product WHERE pdt_number like '%{tBoxSearch.Text}%' or pdt_name_kr like '%{tBoxSearch.Text}%' or pdt_name_en like '%{tBoxSearch.Text}%'";
                dbconn.SqlDataAdapterQuery(pdtQuery, resultData);
                string resultString ="";
                foreach(DataRow pdtRow in resultData.Rows)
                {
                    if(string.IsNullOrEmpty(resultString))
                    {
                        resultString = pdtRow[0].ToString();
                    }
                    resultString += ", "+ pdtRow[0].ToString();
                }
                query += $" AND pdtlog_param IN ({resultString})";
            }
            query += " ORDER BY pdtlog_date";
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
