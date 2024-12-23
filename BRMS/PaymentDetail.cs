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
    public partial class PaymentDetail : Form
    {
        Dictionary<string, object> originalValues = new Dictionary<string, object>();
        Dictionary<int, string> accessPermission = new Dictionary<int, string>();
        int accessedEmp = 1;
        int paymentCode = 0;
        int supCode = 0;
        bool PurchasePayment = false;
        int purchaseCode = 0;
        int voucherType = 1;
        int payAmount = 0;
        int paymentType = 0;
        string bank = "";
        string account = "";
        string Depasitor = "";
        DateTime paymentDate;
        DataTable resultTable = new DataTable();
        cDatabaseConnect dbconn = new cDatabaseConnect();
        DataTable supplierTable = new DataTable();
        public PaymentDetail()
        {
            InitializeComponent();
            DefaultTextBoxStatus();
            tBoxCashPayment.TextChanged += TextBox_TextChanged;
            tBoxCreditPayment.TextChanged += TextBox_TextChanged;
            tBoxNotePayment.TextChanged += TextBox_TextChanged;
            tBoxAccuntTransfer.TextChanged += TextBox_TextChanged;
            tBoxDiscount.TextChanged += TextBox_TextChanged;
            tBoxCoupon.TextChanged += TextBox_TextChanged;
            tBoxSupsiby.TextChanged += TextBox_TextChanged;
            tBoxEtc.TextChanged += TextBox_TextChanged;

        }

        private void DefaultTextBoxStatus()
        {
            tBoxCashPayment.Text = "0";
            tBoxCreditPayment.Text = "0";
            tBoxNotePayment.Text = "0";
            tBoxAccuntTransfer.Text = "0";
            tBoxDiscount.Text = "0";
            tBoxCoupon.Text = "0";
            tBoxSupsiby.Text = "0";
            tBoxEtc.Text = "0";
            tBoxTotalPayment.Text = "0";
            CalculateTotalPayment();
            tBoxTotalPayment.ReadOnly = true;
        }
        /// <summary>
        /// 텍스트 박스의 값을 숫자로 변환 후 합산
        /// </summary>
        private void CalculateTotalPayment()
        {
            int cash = TryParseOrDefault(tBoxCashPayment.Text);
            int credit = TryParseOrDefault(tBoxCreditPayment.Text);
            int note = TryParseOrDefault(tBoxNotePayment.Text);
            int account = TryParseOrDefault(tBoxAccuntTransfer.Text);
            int dc = TryParseOrDefault(tBoxDiscount.Text);
            int coupon = TryParseOrDefault(tBoxCoupon.Text);
            int supsiby = TryParseOrDefault(tBoxSupsiby.Text);
            int etc = TryParseOrDefault(tBoxEtc.Text);

            int total = cash + credit + note + account + dc + coupon + supsiby + etc;
            tBoxTotalPayment.Text = total.ToString();
        }
        /// <summary>
        /// 숫자이외 문자 입력시 0으로 변환
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private int TryParseOrDefault(string input)
        {
            return int.TryParse(input, out int result) ? result : 0;
        }
        /// <summary>
        /// 결제대상 금액 받아오는 함수
        /// 매입전표에서 결제 등록시 매입액을
        /// 결제내역에서 결제 등록시 잔액을 받아온다
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public int GetAmount(int amount)
        {
            payAmount = amount;
            return payAmount;
                
        }
        private void SetPaymentType(int Amount, int Type)
        {
            switch(Type)
            {
                case 0:
                    tBoxCashPayment.Text = Amount.ToString("#,##0");
                    break;
                case 1:
                    tBoxAccuntTransfer.Text = Amount.ToString("#,##0");
                    break;
                case 2:
                    tBoxCreditPayment.Text = Amount.ToString("#,##0");
                    break;
                case 3:
                    tBoxNotePayment.Text = Amount.ToString("#,##0");
                    break;
            }    
        }
        /// <summary>
        /// 텍스트박스 내용 변경시 작동하는 이벤트 핸들러
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalPayment();
        }
        public void AddPayment(int supplierCode, int purchase, int amount)
        {
            string query = string.Format("SELECT RTRIM(sup_name) FROM supplier WHERE sup_code = {0}", supplierCode);
            object resultObj = new object();
            dbconn.sqlScalaQuery(query, out resultObj);
            lblsupplier.Text = resultObj.ToString();
            LoadSupplierInfo(supplierCode);
            SetAccountInfo();
            payAmount = amount;
            purchaseCode = purchase;
            supCode = supplierCode;    
            if(purchase != 0)
            {
                PurchasePayment = true;
            }
            GetDateTime(DateTime.Now);
            bntDelete.Visible = false;
            SetPaymentType(payAmount, paymentType);
        }
        private void LoadSupplierInfo(int supplierCode)
        {
            string query = "SELECT sup_name,sup_bank,sup_account,sup_accname,sup_paytype FROM supplier WHERE sup_code = " + supplierCode;
            dbconn.SqlReaderQuery(query, supplierTable);
            DataRow dataRow = supplierTable.Rows[0];
            lblsupplier.Text = Convert.ToString(dataRow["sup_name"]);
            paymentType = Convert.ToInt32(dataRow["sup_paytype"]);
            bank = Convert.ToString(dataRow["sup_bank"]);
            account = Convert.ToString(dataRow["sup_account"]);
            Depasitor = Convert.ToString(dataRow["sup_accname"]);          

        }
        private void SetAccountInfo()
        {
            tBoxBank.Text = bank;
            tBoxAccount.Text = account;
            tBoxDepasitor.Text = Depasitor;
        }

        private int TotalPrice()
        {
            int cash = Convert.ToInt32(tBoxCashPayment.Text);
            int credit = Convert.ToInt32(tBoxCreditPayment.Text);
            int note = Convert.ToInt32(tBoxNotePayment.Text);
            int account = Convert.ToInt32(tBoxAccuntTransfer.Text);
            int dc = Convert.ToInt32(tBoxDiscount.Text);
            int coupon = Convert.ToInt32(tBoxCoupon.Text);
            int supsiby = Convert.ToInt32(tBoxSupsiby.Text);
            int etc = Convert.ToInt32(tBoxEtc.Text);
            int total = cash + credit + note + account + dc + coupon + supsiby + etc;
            tBoxTotalPayment.Text = total.ToString();
            return total;

        }
        /// <summary>
        /// 결제전호 호출
        /// </summary>
        /// <param name="paymentCode"></param>
        public void LoadPaymentDetail(int getPaymentCode)
        {
            string query = string.Format("SELECT ( SELECT sup_name FROM supplier WHERE sup_code = pay_sup) sup_name ,pay_paycash,pay_accounttransfer,pay_paycredit," +
                "pay_date,pay_paynote,pay_DC,pay_coupone,pay_supsiby,pay_etc,pay_bank,pay_account,pay_depasitor,pay_memo,pay_purcode FROM payment WHERE pay_code = {0} ", getPaymentCode);
            dbconn.SqlReaderQuery(query, resultTable);
            DataRow dataRow = resultTable.Rows[0];
            tBoxCashPayment.Text = dataRow["pay_paycash"].ToString();
            tBoxCreditPayment.Text = dataRow["pay_paycredit"].ToString();
            tBoxNotePayment.Text = dataRow["pay_paynote"].ToString();
            tBoxAccuntTransfer.Text = dataRow["pay_accounttransfer"].ToString();
            tBoxDiscount.Text = dataRow["pay_DC"].ToString();
            tBoxCoupon.Text = dataRow["pay_coupone"].ToString();
            tBoxSupsiby.Text = dataRow["pay_supsiby"].ToString();
            tBoxEtc.Text = dataRow["pay_etc"].ToString();
            tBoxBank.Text = dataRow["pay_bank"].ToString();
            tBoxAccount.Text = dataRow["pay_account"].ToString();
            tBoxDepasitor.Text = dataRow["pay_depasitor"].ToString();
            tBoxNote.Text = dataRow["pay_memo"].ToString();
            GetDateTime(Convert.ToDateTime(dataRow["pay_date"]));
            TotalPrice();
            bntSave.Text = "수정";
            bntDelete.Visible = true;
            paymentCode = getPaymentCode;
            purchaseCode = Convert.ToInt32(dataRow["pay_purcode"]);
            RegisterOriginalData();
        }

        /// <summary>
        /// 조회된 원본 데이터 originalValues 딕셔너리에 등록
        /// 수정시 원본과 수정본을 비교하여 로그 생성시 before 데이터로 사용
        /// </summary>
        private void RegisterOriginalData()
        {
            originalValues["@payCash"] = tBoxCashPayment.Text;
            originalValues["@payTransfer"] = tBoxAccuntTransfer.Text;
            originalValues["@payCredit"] = tBoxCreditPayment.Text;
            originalValues["@payNote"] = tBoxNotePayment.Text;
            originalValues["@payDc"] = tBoxDiscount.Text;
            originalValues["@payCoupone"] = tBoxCoupon.Text;
            originalValues["@paySupsiby"] = tBoxSupsiby.Text;
            originalValues["@payEtc"] = tBoxEtc.Text;
            originalValues["@payBank"] = tBoxBank.Text;
            originalValues["@payAccount"] = tBoxAccount.Text;
            originalValues["@payDepasitor"] = tBoxDepasitor.Text;
            originalValues["@payMemo"] = tBoxNote.Text;
            originalValues["@payDate"] = paymentDate;
        }
        /// <summary>
        /// 결제전표 저장
        /// </summary>
        private void InsertPayment(SqlConnection connection, SqlTransaction transaction)
        {
            string query = "SELECT ISNULL(MAX(pay_code),0) + 1 FROM payment";
            object resultObj = new object();
            dbconn.sqlScalaQuery(query, out resultObj);
            paymentCode = Convert.ToInt32(resultObj.ToString());
            query = "INSERT INTO payment(pay_code,pay_sup,pay_type,pay_purcode,pay_paycash,pay_accounttransfer,pay_paycredit,pay_paynote,pay_DC,pay_coupone,pay_supsiby,pay_etc,pay_bank,pay_account,pay_depasitor,pay_memo,pay_idate,pay_udate,pay_date,pay_status,pay_emp ) " +
                "VALUES(@code, @paySup,@payType, @payPurCode, @payCash,@payTransfer, @payCredit, @payNote,@payDc, @payCoupone, @paySupsiby, @payEtc, @payBank, @payAccount, @payDepasitor, @payMemo, GETDATE(), GETDATE(), @payDate,1, @payEmp)";
            if (purchaseCode != 0)
            {
                voucherType = 2;
            }


            SqlParameter[] parameter =
            {
                new SqlParameter("@code",SqlDbType.Int){Value = paymentCode},
                new SqlParameter("@paySup",SqlDbType.Int){Value = supCode },
                new SqlParameter("@payType",SqlDbType.Int){Value = voucherType},
                new SqlParameter("@payPurCode",SqlDbType.Int){Value = purchaseCode},
                new SqlParameter("@payCash",SqlDbType.Int){Value = tBoxCashPayment.Text.Replace(",","") },
                new SqlParameter("@payTransfer",SqlDbType.Int){Value =tBoxAccuntTransfer.Text.Replace(",","")  },
                new SqlParameter("@payCredit",SqlDbType.Int){Value =tBoxCreditPayment.Text.Replace(",","")  },
                new SqlParameter("@payNote",SqlDbType.Int){Value = tBoxNotePayment.Text.Replace(",","") },
                new SqlParameter("@payDc",SqlDbType.Int){Value = tBoxDiscount.Text.Replace(",","") },
                new SqlParameter("@payCoupone",SqlDbType.Int){Value = tBoxCoupon.Text.Replace(",","") },
                new SqlParameter("@paySupsiby",SqlDbType.Int){Value = tBoxSupsiby.Text.Replace(",","") },
                new SqlParameter("@payEtc",SqlDbType.Int){Value = tBoxEtc.Text.Replace(",","") },
                new SqlParameter("@payBank",SqlDbType.VarChar){Value = tBoxBank.Text },
                new SqlParameter("@payAccount",SqlDbType.VarChar){Value = tBoxAccount.Text},
                new SqlParameter("@payDepasitor",SqlDbType.VarChar){Value = tBoxDepasitor.Text },
                new SqlParameter("@payMemo",SqlDbType.VarChar){Value = tBoxNote.Text },
                new SqlParameter("@payDate",SqlDbType.DateTime){Value = paymentDate},
                new SqlParameter("@payEmp",SqlDbType.Int){Value = accessedEmp}
            };
            dbconn.ExecuteNonQuery(query, connection, transaction, parameter);
            if (purchaseCode != 0)
            {
                PurcasePaymentUpdate(connection, transaction);
            }
        }
        private void UpdatePayment(SqlConnection connection, SqlTransaction transaction)
        {
            string query = "UPDATE payment SET pay_paycash = @payCash,pay_accounttransfer = @payTransfer, pay_paycredit = @payCredit, " +
                "pay_paynote = @payCredit ,pay_DC = @payDc ,pay_coupone = @payCoupone ,pay_supsiby = @paySupsiby ,pay_etc =@payEtc ," +
                "pay_bank = @payBank ,pay_account = @payAccount ,pay_depasitor = @payDepasitor ,pay_memo = @payMemo ,pay_udate = GETDATE() ,pay_date = @payDate WHERE pay_code = @code";
            SqlParameter[] parameters =
            {
                new SqlParameter("@code",SqlDbType.Int){Value = paymentCode},
                new SqlParameter("@payCash",SqlDbType.Int){Value = tBoxCashPayment.Text.Replace(",","") },
                new SqlParameter("@payTransfer",SqlDbType.Int){Value =tBoxAccuntTransfer.Text.Replace(",","")  },
                new SqlParameter("@payCredit",SqlDbType.Int){Value =tBoxCreditPayment.Text.Replace(",","")  },
                new SqlParameter("@payNote",SqlDbType.Int){Value = tBoxNotePayment.Text.Replace(",","") },
                new SqlParameter("@payDc",SqlDbType.Int){Value = tBoxDiscount.Text.Replace(",","") },
                new SqlParameter("@payCoupone",SqlDbType.Int){Value = tBoxCoupon.Text.Replace(",","") },
                new SqlParameter("@paySupsiby",SqlDbType.Int){Value = tBoxSupsiby.Text.Replace(",","") },
                new SqlParameter("@payEtc",SqlDbType.Int){Value = tBoxEtc.Text.Replace(",","") },
                new SqlParameter("@payBank",SqlDbType.VarChar){Value = tBoxBank.Text },
                new SqlParameter("@payAccount",SqlDbType.VarChar){Value = tBoxAccount.Text},
                new SqlParameter("@payDepasitor",SqlDbType.VarChar){Value = tBoxDepasitor.Text },
                new SqlParameter("@payMemo",SqlDbType.VarChar){Value = tBoxNote.Text },
                new SqlParameter("@payDate",SqlDbType.DateTime){Value = paymentDate}
            };
            dbconn.ExecuteNonQuery(query, connection, transaction, parameters);
            if(purchaseCode != 0)
            {
                PurcasePaymentUpdate(connection, transaction);
            }
            Dictionary<string, string> modifiedValues = new Dictionary<string, string>();
            foreach (var param in parameters)
            {
                modifiedValues[param.ParameterName] = param.Value.ToString();
            }
            cLog.IsModified(originalValues, modifiedValues, paymentCode, accessedEmp, connection, transaction);
        }

        private void DeletePayment(SqlConnection connection, SqlTransaction transaction)
        {
            string query = "UPDATE payment SET pay_status = 0, pay_udate = GETDATE() WHERE pay_code = @code";
            SqlParameter[] parameter =
            {
                new SqlParameter("@code", SqlDbType.Int) { Value = paymentCode },
            };
            dbconn.ExecuteNonQuery(query, connection, transaction, parameter);
            cLog.InsertLogToDatabase("@payStatus", "1", "0", paymentCode,accessedEmp, connection, transaction);

            if(purchaseCode != 0)
            {
                SqlParameter[] purParameter =
           {
                new SqlParameter("@purCode", SqlDbType.Int) { Value = purchaseCode }
            };
                query = "UPDATE purchase SET pur_payment = 0, pur_udate = GETDATE() WHERE pur_code = @purCode";
                dbconn.ExecuteNonQuery(query, connection, transaction, purParameter);
            }
        }
        private void PurcasePaymentUpdate(SqlConnection connection, SqlTransaction transaction)
        {
            string query = "UPDATE purchase SET pur_payment = @payment WHERE pur_code = @purCode";
            SqlParameter[] parameters =
            {
                new SqlParameter("@payment", SqlDbType.Int) { Value = tBoxTotalPayment.Text.Replace(",", "") },
                new SqlParameter("@purCode", SqlDbType.Int) { Value = purchaseCode }
            };
            dbconn.ExecuteNonQuery(query, connection, transaction, parameters);
        }
        private DateTime GetDateTime(DateTime dateTime)
        {
            paymentDate = dateTime;
            lblPaymentDate.Text = dateTime.ToString("yyyy-MM-dd HH:mm");
            
            return paymentDate;
        }
        private void bntSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = dbconn.Opensql())
            {
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    if (paymentCode == 0)
                    {
                        InsertPayment(connection, transaction);
                    }
                    else
                    {
                        UpdatePayment(connection, transaction);
                        cLog.InsertEmpAccessLogConnect("@paymentModify", accessedEmp, paymentCode, connection, transaction);
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
        }

        private void bntClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bntDelete_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = dbconn.Opensql())
            {
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    DeletePayment(connection, transaction);
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
        }

        private void bntPaymentDate_Click(object sender, EventArgs e)
        {
            DateTimePickerBox dateTimePickerBox = new DateTimePickerBox();
            dateTimePickerBox.StartPosition = FormStartPosition.CenterParent;
            dateTimePickerBox.DateTiemPick += (purDatePick) => { GetDateTime(purDatePick); };
            dateTimePickerBox.GetDateTime(DateTime.Parse(lblPaymentDate.Text),true);
            dateTimePickerBox.ShowDialog();
        }

        private void bntAccountSet_Click(object sender, EventArgs e)
        {
            SetAccountInfo();
        }
    }
}
