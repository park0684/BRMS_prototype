using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BRMS
{
    public partial class SupplierDetail : Form
    {
        cDatabaseConnect dbconn = new cDatabaseConnect();
        int workType = 0;// 0 == 신규 등록 , 1 == 기존 공급사 조회
        int supplierStatus = 1;
        public event Action<int> refresh;
        Dictionary<string, object> originalValues = new Dictionary<string, object>();
        Dictionary<int, string> accessPermission = new Dictionary<int, string>();
        int accessedEmp = 0;
        int suppliercode = 0;
        public SupplierDetail()
        {
            InitializeComponent();
            //MaximizeBox = false;
            //MinimizeBox = false;
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            ComboBoxSetting();
        }

        private void ComboBoxSetting()
        {
            //cBoxPayType.Items.Add("현금");
            //cBoxPayType.Items.Add("계좌이체");
            //cBoxPayType.Items.Add("신용카드");
            //cBoxPayType.Items.Add("어음");
            foreach(var status in cStatusCode.SupplierPayment)
            {
                cBoxPayType.Items.Add(new KeyValuePair<int, string>(status.Key, status.Value));
            }
            cBoxPayType.DisplayMember = "Value"; // 사용자에게 보여질 값
            cBoxPayType.ValueMember = "Key";    // 내부적으로 사용할 값
            cBoxPayType.DropDownStyle = ComboBoxStyle.DropDownList;
            cBoxPayType.SelectedIndex = 0;
        }
        public void GetSupplierInfo(int supCode)
        {
            string query = null;
            if (supCode == 0)
            {
                query = "SELECT MAX(sup_code) + 1 FROM supplier";
                Object resultObj = new object();
                dbconn.sqlScalaQuery(query, out resultObj);
                lblSupplierCode.Text = resultObj.ToString();
            }
            else
            {
                DataTable resultData = new DataTable();
                //query = "SELECT sup_name, sup_bzno,sup_ceoname,sup_tel,sup_cel,sup_fax,sup_email,sup_url,sup_memo FROM supplier WHERE sup_code = " + supCode;
                query = "SELECT sup_code, sup_name, sup_bzno, sup_bztype, sup_industry, sup_ceoname, sup_tel, sup_cel, sup_fax, sup_email, sup_url, sup_memo, sup_bank, sup_account, " +
                    "sup_accname, sup_status, sup_idate, sup_address, sup_paytype FROM supplier WHERE sup_code = " + supCode;
                dbconn.SqlReaderQuery(query, resultData);
                workType = 1;
                suppliercode = supCode;
                DataRow dataRow = resultData.Rows[0];
                supplierStatus = Convert.ToInt32(dataRow["sup_status"]);
                lblSupplierCode.Text = supCode.ToString();
                tBoxSupplierBzName.Text = dataRow["sup_name"].ToString();
                tBoxSupplierBzNum.Text = dataRow["sup_bzno"].ToString();
                tBoxBzType.Text = dataRow["sup_bztype"].ToString();
                tBoxIndustry.Text = dataRow["sup_industry"].ToString();
                tBoxSupplierCeo.Text = dataRow["sup_ceoname"].ToString();
                tBoxTelephon.Text = dataRow["sup_tel"].ToString();
                tBoxCellphon.Text = dataRow["sup_cel"].ToString();
                tBoxFax.Text = dataRow["sup_fax"].ToString();
                tBoxEmail.Text = dataRow["sup_email"].ToString();
                tBoxUrl.Text = dataRow["sup_url"].ToString();
                tBoxMemo.Text = dataRow["sup_memo"].ToString();
                tBoxBank.Text = dataRow["sup_bank"].ToString();
                tBoxAccount.Text = dataRow["sup_account"].ToString();
                tBoxDepasitor.Text = dataRow["sup_accname"].ToString();
                if(dataRow["sup_status"].ToString() == "1")
                {
                    checkBoxStatus.Checked = true;
                }
                else
                {
                    checkBoxStatus.Checked = false;
                }
                tBoxAddress.Text = dataRow["sup_address"].ToString();
                cBoxPayType.SelectedIndex = Convert.ToInt32(dataRow["sup_paytype"]);

                OrigenaDate();
            }
        }
        private void OrigenaDate()
        {
            
            originalValues["@supName"] = tBoxSupplierBzName.Text;
            originalValues["@supBzNo"] = tBoxSupplierBzNum.Text;
            originalValues["@supBzType"] = tBoxBzType.Text;
            originalValues["@supIndustry"] = tBoxIndustry.Text;
            originalValues["@supCeoName"] = tBoxSupplierCeo.Text;
            originalValues["@supTel"] = tBoxTelephon.Text;
            originalValues["@supCel"] = tBoxCellphon.Text;
            originalValues["@supFax"] = tBoxFax.Text;
            originalValues["@supEMail"] = tBoxEmail.Text;
            originalValues["@supUrl"] = tBoxUrl.Text;
            originalValues["@supMemo"] = tBoxMemo.Text;
            originalValues["@supBank"] = tBoxBank.Text;
            originalValues["@supAccount"] = tBoxAccount.Text;
            originalValues["@supDepasitor"] = tBoxDepasitor.Text;
            originalValues["@supStatus"] = supplierStatus;
            originalValues["@supAddress"] = tBoxAddress.Text;
            originalValues["@supPayType"] = cBoxPayType.SelectedIndex;
        }
        private void SupplierAdd(SqlConnection connection, SqlTransaction transaction)
        {
            string query = "SELECT ISNULL(MAX(sup_code),0) + 1 FROM supplier ";
            object resultObj = new object();
            dbconn.sqlScalaQuery(query, out resultObj);
            //query = string.Format("INSERT INTO supplier(sup_code,sup_name, sup_bzno,sup_ceoname,sup_tel,sup_cel,sup_fax,sup_email,sup_url,sup_memo)" +
            //    "VALUES({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')", resultObj.ToString(), tBoxSupplierBzName.Text, tBoxSupplierBzNum.Text, tBoxSupplierCeo.Text, tBoxTelephon.Text, tBoxCellphon.Text, tBoxFax.Text, tBoxEmail.Text, tBoxUrl.Text, tBoxMemo.Text);
            //dbconn.NonQuery(query, connection, transaction);

            query = "INSERT INTO supplier(sup_code,sup_name, sup_bzno,sup_bztype,sup_industry,sup_ceoname,sup_tel,sup_cel,sup_fax,sup_email,sup_url,sup_memo,sup_bank,sup_account,sup_accname,sup_status,sup_idate,sup_address,sup_paytype)" +
                "\n VALUES(@code,@name,@bzNo,@bzType,@industry,@ceoName,@tel,@cel,@fax,@eMail,@url,@memo,@bank,@account,@depasitor,@status,@idate,@address,@payType)";
            SqlParameter[] parameters = 
            {
                new SqlParameter("@code", SqlDbType.Int){Value = resultObj.ToString()},
                new SqlParameter("@name",SqlDbType.VarChar){Value = tBoxSupplierBzName.Text},
                new SqlParameter("@bzNo",SqlDbType.NChar){Value = tBoxSupplierBzNum.Text},
                new SqlParameter("@bzType",SqlDbType.VarChar){Value = tBoxBzType.Text },
                new SqlParameter("@industry",SqlDbType.VarChar){Value = tBoxIndustry.Text},
                new SqlParameter("@ceoName",SqlDbType.VarChar){Value = tBoxSupplierCeo.Text},
                new SqlParameter("@tel",SqlDbType.NChar){Value = tBoxTelephon.Text},
                new SqlParameter("@cel",SqlDbType.NChar){Value = tBoxCellphon.Text},
                new SqlParameter("@fax",SqlDbType.NChar){Value = tBoxFax.Text},
                new SqlParameter("@eMail",SqlDbType.VarChar){Value = tBoxEmail.Text},
                new SqlParameter("@url",SqlDbType.VarChar){Value = tBoxUrl.Text},
                new SqlParameter("@memo",SqlDbType.VarChar){Value = tBoxMemo.Text},
                new SqlParameter("@bank",SqlDbType.VarChar){Value = tBoxBank.Text},
                new SqlParameter("@account",SqlDbType.VarChar){Value = tBoxAccount},
                new SqlParameter("@depasitor",SqlDbType.VarChar){Value = tBoxDepasitor.Text},
                new SqlParameter("@status",SqlDbType.Int){Value = 1 },
                new SqlParameter("@idate",SqlDbType.DateTime){Value = DateTime.Now },
                new SqlParameter("@address",SqlDbType.VarChar){Value = tBoxAddress.Text },
                new SqlParameter("@payType",SqlDbType.Int){Value = cBoxPayType.SelectedIndex }
            };
            dbconn.ExecuteNonQuery(query, connection, transaction, parameters);
        }

        private void SupplierModfy(SqlConnection connection, SqlTransaction transaction)
        {
            if(checkBoxStatus.Checked == true)
            {
                supplierStatus = 1;
            }
            else
            {
                supplierStatus = 0;
            }
            string query = "UPDATE supplier SET sup_name = @supName, sup_bzno =@supBzNo, sup_bztype = @supBzType, sup_industry = @supIndustry, sup_ceoname = @supCeoName, sup_tel =@supTel, sup_cel = @supCel, sup_fax = @supFax," +
                "sup_email = @supEMail, sup_url = @supUrl, sup_memo = @supMemo, sup_bank = @supBank, sup_account = @supAccount, sup_accname = @supdepasitor, sup_status = @supStatus, sup_address = @supAddress , sup_paytype = @supPayType " +
                "WHERE sup_code = @supcode";
            SqlParameter[] parameters =
            {
                new SqlParameter("@supcode",SqlDbType.Int){Value = lblSupplierCode.Text},
                new SqlParameter("@supName",SqlDbType.VarChar){Value = tBoxSupplierBzName.Text},
                new SqlParameter("@supBzNo",SqlDbType.VarChar){Value = tBoxSupplierBzNum.Text},
                new SqlParameter("@supBzType",SqlDbType.VarChar){Value = tBoxBzType.Text },
                new SqlParameter("@supIndustry",SqlDbType.VarChar){Value = tBoxIndustry.Text},
                new SqlParameter("@supCeoName",SqlDbType.VarChar){Value = tBoxSupplierCeo.Text},
                new SqlParameter("@supTel",SqlDbType.VarChar){Value = tBoxTelephon.Text},
                new SqlParameter("@supCel",SqlDbType.VarChar){Value = tBoxCellphon.Text},
                new SqlParameter("@supFax",SqlDbType.VarChar){Value = tBoxFax.Text},
                new SqlParameter("@supEMail",SqlDbType.VarChar){Value = tBoxEmail.Text},
                new SqlParameter("@supUrl",SqlDbType.VarChar){Value = tBoxUrl.Text},
                new SqlParameter("@supMemo",SqlDbType.VarChar){Value = tBoxMemo.Text},
                new SqlParameter("@supBank",SqlDbType.VarChar){Value = tBoxBank.Text},
                new SqlParameter("@supAccount",SqlDbType.VarChar){Value = tBoxAccount.Text},
                new SqlParameter("@supdepasitor",SqlDbType.VarChar){Value = tBoxDepasitor.Text},
                new SqlParameter("@supStatus",SqlDbType.Int){Value = supplierStatus },
                new SqlParameter("@supAddress",SqlDbType.VarChar){Value = tBoxAddress.Text },
                new SqlParameter("@supPayType",SqlDbType.Int){Value = cBoxPayType.SelectedIndex }
            };
            dbconn.ExecuteNonQuery(query, connection, transaction, parameters);
            Dictionary<string, string> modifiedValues = new Dictionary<string, string>();
            foreach (var param in parameters)
            {
                modifiedValues[param.ParameterName] = param.Value.ToString();
            }
            cLog.IsModified(originalValues, modifiedValues, suppliercode, accessedEmp, connection, transaction);
        }
        
        private void bntSave_Click(object sender, EventArgs e)
        {
            SqlConnection connection = dbconn.Opensql();
            SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                dbconn.NonQuery("BEGIN TRAN", connection, transaction);
                if (workType == 0)
                {
                    SupplierAdd(connection, transaction);
                }
                else if (workType == 1)
                {
                    SupplierModfy(connection, transaction);
                }
                dbconn.NonQuery("COMMIT TRAN", connection, transaction);
                transaction.Commit();
                refresh?.Invoke(1);
                Close();
            }
            catch(Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show(ex.Message);
            }
            
        }

        private void bntCancle_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
