using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BRMS
{
    public partial class CustomerDetail : Form
    {
        cDatabaseConnect dbconn = new cDatabaseConnect();
        cDataGridDefaultSet custOrder = new cDataGridDefaultSet();
        cDataGridDefaultSet custSale = new cDataGridDefaultSet();
        cDataGridDefaultSet custLog = new cDataGridDefaultSet();
        DataTable resultTable = new DataTable();
        int custCode = 0;
        int ccustStatus = 1;
        int workType = 0; // 0 등록, 1 기존회원 조회
        Dictionary<string, object> originalValues = new Dictionary<string, object>();
        Dictionary<int, string> accessPermission = new Dictionary<int, string>();
        static Dictionary<string, (int typeCode, string typeString)> parameter = new Dictionary<string, (int, string)>();
        int accessedEmp = 1;
        public CustomerDetail()
        {
            InitializeComponent();
            InitializeTabControl();
            InitializeComboBox();
            InitalizeLable();
            InitializeDateTimePicker();
            cUIManager.ApplyFormStyle(this);
            MaximizeBox = false;
            custOrder.CellDoubleClick += custOrder_DuobleClick;
            GridForm();
            tBoxTell.KeyUp += tBoxTell_keyup;
            tBoxCell.KeyUp += tBoxCell_keyup;
            parameter = cLog.GetFilteredParameters(700, 800);
        }
        private void InitializeTabControl()
        {
            tabCtrlCustomer.TabPages[0].Text = "회원정보"; 
            tabCtrlCustomer.TabPages[1].Text = "주문내역"; 
            tabCtrlCustomer.TabPages[2].Text = "판매내역";
            tabCtrlCustomer.TabPages[3].Text = "주요제품";
            tabCtrlCustomer.TabPages[4].Text = "변경로그";

        }
       
        private void InitializeComboBox()
        {
            cmBoxStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            foreach (var status in cStatusCode.CustomerStatus)
            {
                cmBoxStatus.Items.Add(status.Value); // 상태 문자열 추가
            }

            cmBoxStatus.SelectedIndex = 1;


            cmBoxCountry.DropDownStyle = ComboBoxStyle.DropDownList;
            string query = "SELECT ctry_code, ctry_name FROM country";
            DataTable dataTable = new DataTable();
            dbconn.SqlDataAdapterQuery(query, dataTable);
            cmBoxCountry.Items.Add("-국가 선택-");
            foreach (DataRow row in dataTable.Rows)
            {
                string countryName = row["ctry_name"].ToString();
                cmBoxCountry.Items.Add(countryName);
            }
            cmBoxCountry.SelectedIndex = 0;
        }
        /// <summary>
        /// 라벨 기본 설정
        /// </summary>
        private void InitalizeLable()
        {
            lblRegDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            lblUpdate.Text = "";
            lblSaleDate.Text = "";
            lblOrderCount.Text = "0";
            lblOrderAmount.Text = "0";
        }
        /// <summary>
        /// 타임피커 기본 설정
        /// </summary>
        private void InitializeDateTimePicker()
        {
            DateTime fromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime toDate = DateTime.Now;
            dtpOrderDateTo.Value = toDate;
            dtpSaleDateTo.Value = toDate;
            dtpOrderDateFrom.Value = fromDate;
            dtpSaleDateFrom.Value = fromDate;
        }
        /// <summary>
        /// 주문, 판매내역 데이터그리드 생성
        /// </summary>
        private void GridForm()
        {
            panelOrderDataGrid.Controls.Add(custOrder.Dgr);
            custOrder.Dgr.Dock = DockStyle.Fill;
            panelSaleDataGrid.Controls.Add(custSale.Dgr);
            custSale.Dgr.Dock = DockStyle.Fill;
            pnlLogDataGrid.Controls.Add(custLog.Dgr);
            custLog.Dgr.Dock = DockStyle.Fill;

            custOrder.Dgr.Columns.Add("custOrderCode", "주문코드");
            custOrder.Dgr.Columns.Add("custOrderStatus", "상태");
            custOrder.Dgr.Columns.Add("custOrderDate", "주문일");
            custOrder.Dgr.Columns.Add("custOrderAmontKrw", "주문액(￦)");
            custOrder.Dgr.Columns.Add("custOrderAmontUsd", "주문액(＄)");

            custOrder.Dgr.Columns["custOrderCode"].Visible = false;
            custOrder.FormatAsDateTime("custOrderDate");
            custOrder.FormatAsInteger("custOrderAmontKrw");
            custOrder.FormatAsDecimal("custOrderAmontUsd");
            custSale.FormatAsStringCenter("custOrderStatus");
            custOrder.Dgr.ReadOnly = true;
            custOrder.Dgr.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            custOrder.ApplyDefaultColumnSettings();

            custSale.Dgr.Columns.Add("saleCode", "판매코드");
            custSale.Dgr.Columns.Add("saleType", "유형");
            custSale.Dgr.Columns.Add("saleDate", "판매일");
            custSale.Dgr.Columns.Add("saleStaff", "담당자");
            custSale.Dgr.Columns.Add("saleAmountKrw", "판매액(￦)");
            custSale.Dgr.Columns.Add("saleAmountUsd", "판매액(＄)");
            
            custSale.Dgr.Columns["saleCode"].Visible = false;
            custSale.FormatAsDateTime("saleDate");
            custSale.FormatAsInteger("saleAmountKrw");
            custSale.FormatAsDecimal("saleAmountUsd");
            custSale.FormatAsStringCenter("saleStaff");
            custSale.Dgr.ReadOnly = true;
            custSale.Dgr.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            custSale.ApplyDefaultColumnSettings();

            custLog.Dgr.Columns.Add("logType", "작업내역");
            custLog.Dgr.Columns.Add("logBefore", "변경전");
            custLog.Dgr.Columns.Add("logAfter", "변경후");
            custLog.Dgr.Columns.Add("logEmp", "작업자");
            custLog.Dgr.Columns.Add("logDate", "변경일");
            custLog.Dgr.ReadOnly = true;
            custLog.ApplyDefaultColumnSettings();
            custLog.Dgr.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            custLog.FormatAsDateTime("logDate");
            custLog.FormatAsStringLeft( "logType", "logBefore", "logAfter");
            custLog.FormatAsStringCenter("logEmp", "logParam");

        }
        /// <summary>
        /// 고객추가시 탭페이지 설정
        /// </summary>
        public void CustAdd()
        {
            workType = 0;
            tabCtrlCustomer.TabPages.Remove(tabPage2);
            tabCtrlCustomer.TabPages.Remove(tabPage3);
            tabCtrlCustomer.TabPages.Remove(tabPage4);
            cmBoxCountry.SelectedIndex = 122;
        }
        public void SearchCustomer(int code)
        {
            LoadCustomerInfo(code);
        }
        private void SetTextBoxHelper(TextBox textBox, object value)
        {
            string stringValue = value as string;
            textBox.Text = !string.IsNullOrWhiteSpace(stringValue) ? stringValue.Trim() : "";
            textBox.Tag = textBox.Text;
        }
        private string GetDateFormatHelper(object dateValue)
        {
            if (dateValue == null || dateValue == DBNull.Value || string.IsNullOrWhiteSpace(dateValue.ToString()))
            {
                return string.Empty;
            }

            return Convert.ToDateTime(dateValue).ToString("yyyy-MM-dd HH:mm");
        }
        private void tBoxTell_keyup(object sender, KeyEventArgs e)
        {
            cDataHandler.AllowPhoneNumber(sender, e, tBoxTell);
        }
        private void tBoxCell_keyup(object sender, KeyEventArgs e)
        {
            cDataHandler.AllowPhoneNumber(sender, e, tBoxCell);
        }
        /// <summary>
        /// 선택된 회원정보 표시
        /// </summary>
        /// <param name="custmerCode"></param>
        private void LoadCustomerInfo(int custmerCode)
        {
            custCode = custmerCode;
            workType = 1;
            var cryptor = new cCryptor("shared-passphrase");
            string query = "SELECT cust_name, cust_country, cust_tell, cust_cell, cust_email, cust_addr, cust_status, cust_idate, cust_udate, cust_lastsaledate, cust_memo,cust_key1, cust_key2, cust_point FROM customer WHERE cust_code = " + custmerCode;
            
            dbconn.SqlReaderQuery(query, resultTable);
            DataRow row = resultTable.Rows[0];

            string custTell = row["cust_tell"].ToString();
            string key1 = row["cust_key1"].ToString();
            string custCell = row["cust_cell"].ToString();
            string key2 = row["cust_key2"].ToString();
            int point = cDataHandler.ConvertToInt(row["cust_point"]);
            // cust_tell 복호화
            if (!string.IsNullOrEmpty(custTell) && !string.IsNullOrEmpty(key1))
            {
                
                custTell = cryptor.NumberDecrypt(custTell, key1);
            }

            // cust_cell 복호화
            if (!string.IsNullOrEmpty(custCell) && !string.IsNullOrEmpty(key2))
            {
                custCell = cryptor.NumberDecrypt(custCell, key2);
                
            }

            SetTextBoxHelper(tBoxCustName, row["cust_name"]);
            SetTextBoxHelper(tBoxEmail, row["cust_email"]);
            SetTextBoxHelper(tBoxTell, custTell);
            SetTextBoxHelper(tBoxCell, custCell);
            SetTextBoxHelper(tBoxAddress, row["cust_addr"]);
            SetTextBoxHelper(tBoxMemo, row["cust_memo"]);
            lblCustPoint.Text = point.ToString("#,##0");
            lblRegDate.Text = GetDateFormatHelper(row["cust_idate"]);
            lblUpdate.Text = GetDateFormatHelper(row["cust_udate"]);
            lblSaleDate.Text = GetDateFormatHelper(row["cust_lastsaledate"]);

            cmBoxCountry.SelectedIndex = row["cust_country"] != DBNull.Value ? Convert.ToInt32(row["cust_country"]) : 0;
            cmBoxCountry.Tag = cmBoxCountry.SelectedItem;
            cmBoxStatus.SelectedIndex = row["cust_status"] != DBNull.Value ? Convert.ToInt32(row["cust_status"]) : 0;
            cmBoxStatus.Tag = cmBoxStatus.SelectedIndex;
            ccustStatus = Convert.ToInt32(row["cust_status"]);
            RegisterOriginalData();
        }
        /// <summary>
        /// 조회된 원본 데이터 originalValues 딕셔너리에 등록
        /// 수정시 원본과 수정본을 비교하여 로그 생성시 before 데이터로 사용
        /// </summary>
        private void RegisterOriginalData()
        {
            originalValues["@custName"] = tBoxCustName.Text;
            originalValues["@custTell"] = tBoxTell.Text;
            originalValues["@custCell"] = tBoxCell.Text;
            originalValues["@custEmail"] = tBoxEmail.Text;
            originalValues["@custAddr"] = tBoxAddress.Text;
            originalValues["@custCountry"] = cmBoxCountry.SelectedIndex;
            originalValues["@custStatus"] = cmBoxStatus.SelectedIndex;
            originalValues["@custMemo"] = tBoxMemo.Text;
        }
        /// <summary>
        /// 주문내역 조회
        /// </summary>
        private void LoadOrderData()
        {
            string fromDate = dtpOrderDateFrom.Value.ToString("yyyy-MM-dd");
            string toDate = dtpOrderDateTo.Value.AddDays(1).ToString("yyyy-MM-dd");
            string query = $"SELECT cord_code, cord_date, cord_status, cord_amount_krw, cord_amount_usd FROM custorder WHERE cord_cust = {custCode} AND cord_date > '{fromDate}' AND cord_date < '{toDate}'";
            DataTable dataTable = new DataTable();
            dbconn.SqlDataAdapterQuery(query, dataTable);
            custOrder.Dgr.Rows.Clear();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                var rowIndex = custOrder.Dgr.Rows.Add();
                var row = custOrder.Dgr.Rows[rowIndex];
                row.Cells["No"].Value = custOrder.Dgr.RowCount;
                row.Cells["custOrderCode"].Value = dataRow["cord_code"];
                row.Cells["custOrderStatus"].Value = cStatusCode.GetCustomerOrderStatus(Convert.ToInt32(dataRow["cord_status"]));
                row.Cells["custOrderDate"].Value = Convert.ToDateTime(dataRow["cord_date"]).ToString("yyyy-MM-dd");
                row.Cells["custOrderAmontKrw"].Value = dataRow["cord_amount_krw"];
                row.Cells["custOrderAmontUsd"].Value = dataRow["cord_amount_usd"];

            }
        }
        /// <summary>
        /// 주문내역 더블 클릭시 상세내역 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void custOrder_DuobleClick(object sender, DataGridViewCellEventArgs e)
        {
            int orderCode = custOrder.ConvertToInt(custOrder.Dgr.CurrentRow.Cells["custOrderCode"].Value);
            CustomerOrderDetail customerOrderDetail = new CustomerOrderDetail();
            customerOrderDetail.StartPosition = FormStartPosition.CenterParent;
            customerOrderDetail.OrderSelectedHandle(orderCode);
            customerOrderDetail.ShowDialog();
        }
        /// <summary>
        /// 판매내역 조회
        /// </summary>
        private void LoadSaleData()
        {
            string fromDate = dtpSaleDateFrom.Value.ToString("yyyy-MM-dd");
            string toDate = dtpSaleDateTo.Value.AddDays(1).ToString("yyyy-MM-dd");
            string query = $"SELECT sale_code, sale_date, sale_emp, sale_type, sale_sprice_krw, sale_sprice_usd FROM sales WHERE sale_cust = {custCode} AND sale_date > '{fromDate}' AND sale_date < '{toDate}' ORDER BY sale_date";
            DataTable dataTable = new DataTable();
            dbconn.SqlDataAdapterQuery(query, dataTable);
            custSale.Dgr.Rows.Clear();
            foreach(DataRow dataRow in dataTable.Rows)
            {
                var rowIndex = custSale.Dgr.Rows.Add();
                var row = custSale.Dgr.Rows[rowIndex];
                row.Cells["No"].Value = custSale.Dgr.RowCount ;
                row.Cells["saleCode"].Value = dataRow["sale_code"];
                row.Cells["saleType"].Value = cStatusCode.GetSaleType(Convert.ToInt32(dataRow["sale_type"]));
                row.Cells["saleStaff"].Value = Convert.ToInt32(dataRow["sale_emp"]);
                row.Cells["saleDate"].Value = Convert.ToDateTime(dataRow["sale_date"]).ToString("yyyy-MM-dd");
                row.Cells["saleAmountKrw"].Value = dataRow["sale_sprice_krw"];
                row.Cells["saleAmountUsd"].Value = dataRow["sale_sprice_usd"];

            }
        }
        private void LogSearchQuery()
        {
            string fromDate = dtpLogDateFrom.Value.ToString("yyyy-MM-dd");
            string toDate = dtpLogDateTo.Value.AddDays(1).ToString("yyyy-MM-dd");
            DataTable resultData = new DataTable();
            string query = $"SELECT custlog_type, custlog_before, custlog_after, custlog_emp, custlog_date FROM customerlog WHERE custlog_date > '{fromDate}' AND custlog_date < '{toDate}' AND custlog_param = {custCode} ORDER BY custlog_date";

            dbconn.SqlDataAdapterQuery(query, resultData);
            LoadCustLog(resultData);
        }
        private void LoadCustLog(DataTable dataTable)
        {
            custLog.Dgr.Rows.Clear();
            foreach (DataRow row in dataTable.Rows)
            {
                DataTable readData = new DataTable();
                object resultObj = new object();

                string query = $"SELECT emp_name FROM employee WHERE emp_code = {row["custlog_emp"]}";
                dbconn.sqlScalaQuery(query, out resultObj);
                string empName = resultObj.ToString();

                int addRow = custLog.Dgr.Rows.Add();
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
                custLog.Dgr.Rows[addRow].Cells["No"].Value = addRow + 1;
                custLog.Dgr.Rows[addRow].Cells["logType"].Value = logType; 
                custLog.Dgr.Rows[addRow].Cells["logBefore"].Value = before;
                custLog.Dgr.Rows[addRow].Cells["logAfter"].Value = after;
                custLog.Dgr.Rows[addRow].Cells["logEmp"].Value = empName + $"({empCode})";
                custLog.Dgr.Rows[addRow].Cells["logDate"].Value = logDate;
            }
        }
        /// <summary>
        /// 새 고객 등록 쿼리
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        private void InsertCustomer(SqlConnection connection, SqlTransaction transaction)
        {
            string query = "SELECT MAX(ISNULL(cust_code,0)) + 1 FROM customer";
            object resutObj;
            dbconn.sqlScalaQuery(query, out resutObj);
            //전화번호 암호화
            var cryptor = new cCryptor("shared-passphrase");
            string custTell = tBoxTell.Text;
            string custKey1 = "";
            if (!string.IsNullOrEmpty(custTell) && custTell.Replace("-", "").Length > 6)
            {
                var phoneEncryptResult = cryptor.NumberEncrypt(custTell);
                custTell = phoneEncryptResult.MaskedPhone;
                custKey1 = phoneEncryptResult.keyValue;
            }
           //휴대폰번호 암호화
            string custCell = tBoxCell.Text;
            string custKey2 = "";
            if (!string.IsNullOrEmpty(custCell) && custCell.Replace("-", "").Length > 6)
            {
                var phoneEncryptResult = cryptor.NumberEncrypt(custCell);
                custCell = phoneEncryptResult.MaskedPhone;
                custKey2 = phoneEncryptResult.keyValue;
            }

            query = "INSERT INTO customer(cust_code, cust_name, cust_tell, cust_cell, cust_email, cust_addr, cust_country, cust_status, cust_idate, cust_udate, cust_memo, cust_key1, cust_key2)\n" +
                "VALUES(@code, @name, @tell, @cell, @email, @addr, @country, @status, GETDATE(),  GETDATE(), @memo, @key1, @key2)";
            SqlParameter[] parameter =
            {
                new SqlParameter("@code", SqlDbType.Int){Value = resutObj},
                new SqlParameter("@name", SqlDbType.VarChar){Value = tBoxCustName.Text},
                new SqlParameter("@tell", SqlDbType.VarChar){Value = custTell},
                new SqlParameter("@cell", SqlDbType.VarChar){Value = custCell},
                new SqlParameter("@email", SqlDbType.VarChar){Value = tBoxEmail.Text},
                new SqlParameter("@addr", SqlDbType.VarChar){Value = tBoxAddress.Text},
                new SqlParameter("@country", SqlDbType.Int){Value = cmBoxCountry.SelectedIndex},
                new SqlParameter("@status", SqlDbType.Int){Value = cmBoxStatus.SelectedIndex},
                new SqlParameter("@memo", SqlDbType.VarChar){Value = tBoxMemo.Text},
                new SqlParameter("@key1", SqlDbType.VarChar){Value = custKey1},
                new SqlParameter("@key2", SqlDbType.VarChar){Value = custKey2}
            };
            dbconn.ExecuteNonQuery(query, connection, transaction, parameter);
        }
        /// <summary>
        /// 고객 정보 수정 쿼리
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        private void ModifyCustomer(SqlConnection connection, SqlTransaction transaction)
        {
            //전화번호 암호화
            var cryptor = new cCryptor("shared-passphrase");
            string custTell = tBoxTell.Text;
            string custKey1 = "";
            if (!string.IsNullOrEmpty(custTell) && custTell.Replace("-", "").Length > 6)
            {
                var phoneEncryptResult = cryptor.NumberEncrypt(custTell);
                custTell = phoneEncryptResult.MaskedPhone;
                custKey1 = phoneEncryptResult.keyValue;
            }
            //휴대폰번호 암호화
            string custCell = tBoxCell.Text;
            string custKey2 = "";
            if (!string.IsNullOrEmpty(custCell) && custCell.Replace("-", "").Length > 6)
            {
                var phoneEncryptResult = cryptor.NumberEncrypt(custCell);
                custCell = phoneEncryptResult.MaskedPhone;
                custKey2 = phoneEncryptResult.keyValue;
            }

            string query = "UPDATE customer SET cust_name = @custName, cust_tell = @custTell, cust_cell = @custCell, cust_email = @custEmail, cust_addr = @custAddr, " +
                "cust_country = @custCountry, cust_status = @custStatus, cust_udate = GETDATE(), cust_memo =@custMemo, cust_key1= @key1, cust_key2 =@key2 WHERE cust_code = @code";
            SqlParameter[] parameter =
            {
                new SqlParameter("@code", SqlDbType.Int){Value = custCode},
                new SqlParameter("@custName", SqlDbType.VarChar){Value = tBoxCustName.Text},
                new SqlParameter("@custTell", SqlDbType.VarChar){Value = custTell},
                new SqlParameter("@custCell", SqlDbType.VarChar){Value = custCell },
                new SqlParameter("@custEmail", SqlDbType.VarChar){Value = tBoxEmail.Text},
                new SqlParameter("@custAddr", SqlDbType.VarChar){Value = tBoxAddress.Text},
                new SqlParameter("@custCountry", SqlDbType.Int){Value = cmBoxCountry.SelectedIndex},
                new SqlParameter("@custStatus", SqlDbType.Int){Value = cmBoxStatus.SelectedIndex},
                new SqlParameter("@custMemo", SqlDbType.VarChar){Value = tBoxMemo.Text},
                 new SqlParameter("@key1", SqlDbType.VarChar){Value = custKey1},
                new SqlParameter("@key2", SqlDbType.VarChar){Value = custKey2}
            };
            dbconn.ExecuteNonQuery(query, connection, transaction, parameter);
            parameter[2].Value = tBoxTell.Text;
            parameter[3].Value = tBoxCell.Text;
            Dictionary<string, string> modifiedValues = new Dictionary<string, string>();
            foreach (var param in parameter)
            {
                modifiedValues[param.ParameterName] = param.Value.ToString();
            }
            cLog.IsModified(originalValues, modifiedValues, custCode, accessedEmp, connection, transaction);
        }
        private void bntOrderSearch_Click(object sender, EventArgs e)
        {
            LoadOrderData();
        }

        private void bntSaleSearch_Click(object sender, EventArgs e)
        {
            LoadSaleData();
        }
        private void bntCancle_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bntSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = dbconn.Opensql())
            {
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {

                    if (custCode == 0)
                    {
                        InsertCustomer(connection, transaction);
                        cLog.InsertEmpAccessLogConnect("@customerRegisted", accessedEmp, custCode, connection, transaction);
                    }
                    else
                    {
                        ModifyCustomer(connection, transaction);
                        cLog.InsertEmpAccessLogConnect("@customerModify", accessedEmp, custCode, connection, transaction);
                    }
                    transaction.Commit();
                    Close();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            Close();
        }

        private void btnLogSearch_Click(object sender, EventArgs e)
        {
            LogSearchQuery();
        }
    }
}
