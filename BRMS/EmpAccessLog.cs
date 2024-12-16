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
    public partial class EmpAccessLog : Form
    {
        Dictionary<int, string> accessPermission = new Dictionary<int, string>();
        int accessedEmp = 1;
        cDatabaseConnect dbconn = new cDatabaseConnect();
        cDataGridDefaultSet dgrLog = new cDataGridDefaultSet();
        static Dictionary<string, (int typeCode, string typeString)> parameter = new Dictionary<string, (int, string)>();
        public EmpAccessLog()
        {
            InitializeComponent();
            pnlDataGrid.Controls.Add(dgrLog.Dgr);
            dgrLog.Dgr.Dock = DockStyle.Fill;
            ParameterSet();
            combBoxSetting();
            DataGridFrom();
        }
        private void DataGridFrom()
        {
            dgrLog.Dgr.Columns.Add("logType", "작업내역");
            dgrLog.Dgr.Columns.Add("logParam", "대상");
            dgrLog.Dgr.Columns.Add("logEmpName", "작업자명");
            dgrLog.Dgr.Columns.Add("logEmp", "직원코드");
            dgrLog.Dgr.Columns.Add("logDate", "작업일");
            dgrLog.Dgr.ReadOnly = true;
            dgrLog.ApplyDefaultColumnSettings();
            dgrLog.Dgr.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgrLog.FormatAsDateTime("logDate");
            dgrLog.FormatAsStringLeft("logType",  "logParam");
            dgrLog.FormatAsStringCenter("logEmpName", "logEmp");
        }
        private void ParameterSet()
        {
            foreach (var entry in cLog.logParameter)
            {
                if (entry.Value.typeCode >= 900 && entry.Value.typeCode < 1000)
                {
                    parameter[entry.Key] = entry.Value;
                }
            }
        }
        private void combBoxSetting()
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
            if (dataTable.Rows.Count < 1)
            {
                return;
            }
            
            foreach (DataRow row in dataTable.Rows)
            {
                DataTable readData = new DataTable();
                object resultObj = new object();
                int paramCode = Convert.ToInt32(row["acslog_param"]);
                int empCode = Convert.ToInt32(row["acslog_emp"]);
                string param = "";


                //작업자 이름 조회
                string query = $"SELECT emp_name FROM employee WHERE emp_code = {empCode}";
                dbconn.sqlScalaQuery(query, out resultObj);
                string empName = resultObj.ToString();

                // 로그 데이터 설정
                switch (Convert.ToInt32(row["acslog_type"]))
                {
                    case 901:// 상품 조회
                    case 902:// 상품 등록
                    case 903:// 상품 매입 조회
                    case 904:// 상품 판매 조회
                    case 905:// 상품 변경내역 조회
                    case 906:// 상품 수불내역 조회
                    case 907:// 상품 수정
                        cLog.GetProductInfo(paramCode, out param);
                        break;
                    case 908:// 매입 조회
                    case 909:// 매입 수정
                        cLog.GetPurchaseSupplierInfo(paramCode, out param);
                        break;
                    case 910:// 발주 조회
                    case 911:// 발주 수정
                        cLog.GetPurOrderSupplierInfo(paramCode, out param);
                        break;
                    case 912:// 결제 조회
                    case 913:// 결제 수정
                        cLog.GetPaymentSupplierInfo(paramCode, out param);
                        break;
                    case 914:// 주문서 조회
                    case 915:// 주문서 삭제
                    case 916:// 판매 내역 조회
                    case 917:// 판매 조회
                    case 918:// 고객 조회
                    case 919:// 고객 등록
                    case 920:// 고객 수정
                    case 921:// 고객 주문 조회
                    case 922:// 고객 판매 조회
                    case 622:// 고객 변경로그 조회
                        cLog.GetCustomerInfo(paramCode, out param);
                        break;
                    case 623:// 공급사 조회
                        cLog.GetSupplierInfo(paramCode, out param);
                        break;
                    case 940:// 직원 조회
                    case 941:// 직원 등록
                    case 942:// 직원 수정
                        cLog.GetEmployeeInfo(paramCode, out param);
                        break;
                    case 950:// 제품 로그 조회
                        cLog.GetProductInfo(paramCode, out param);
                        break;
                    case 951:// 직원 로그 조회
                        cLog.GetEmployeeInfo(paramCode, out param);
                        break;
                    case 952:// 공급사 로그 조회
                    case 953:// 매입/발주 로그 조회
                    case 954:// 결제 로그 조회
                        param = "";
                        break;
                    case 955:// 회원 로그 조회
                        cLog.GetCustomerInfo(paramCode, out param);
                        break;
                    case 924:// 결제상세 조회
                        cLog.GetPaymentSupplierInfo(paramCode, out param);
                        break;
                    case 925:// 매입 등록
                        cLog.GetPurchaseSupplierInfo(paramCode, out param);
                        break;
                    case 926:// 발주 등록
                        cLog.GetPurOrderSupplierInfo(paramCode, out param);
                        break;
                }
                string logDate = Convert.ToDateTime(row["acslog_date"]).ToString("yyyy-MM-dd HH:mm");
                string logType = "";

                var typeInfo = parameter.Values.FirstOrDefault(x => x.typeCode == (int)row["acslog_type"]);
                if (typeInfo.typeString != null)
                {
                    logType = typeInfo.typeString;  // 해당 typeString 값을 사용
                }
                // 해당 셀에 값 설정
                int addRow = dgrLog.Dgr.Rows.Add();
                dgrLog.Dgr.Rows[addRow].Cells["No"].Value = addRow + 1;
                dgrLog.Dgr.Rows[addRow].Cells["logType"].Value = logType;  // 여기서 값을 설정
                dgrLog.Dgr.Rows[addRow].Cells["logParam"].Value = param;
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
            string query = $"SELECT acslog_type, acslog_emp, acslog_param, acslog_date FROM accesslog WHERE acslog_date > '{fromDate}' AND acslog_date < '{toDate}'";
            if (cmBoxWorkType.SelectedItem is KeyValuePair<int, string> selectedItem)
            {
                query += $" AND acslog_type = {selectedItem.Key}";
            }
            if (!string.IsNullOrEmpty(tBoxSearch.Text))
            {
                string pdtQuery = $"SELECT distinct(emp_code) FROM employee WHERE pdt_name like '%{tBoxSearch.Text}%'";
                dbconn.SqlDataAdapterQuery(pdtQuery, resultData);
                string resultString = "";
                foreach (DataRow pdtRow in resultData.Rows)
                {
                    if (string.IsNullOrEmpty(resultString))
                    {
                        resultString = pdtRow[0].ToString();
                    }
                    resultString += ", " + pdtRow[0].ToString();
                }
                query += $"AND acslog_emp IN ({resultString})";
            }
            query += " ORDER BY acslog_date";
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
