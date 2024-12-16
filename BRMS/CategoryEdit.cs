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
    public partial class CategoryEdit : Form
    {
        cDatabaseConnect dbcon = new cDatabaseConnect();
        int empCode = 0;
        int workTypeCode = 0;
        public CategoryEdit()
        {
            InitializeComponent();
            TextBoxDefault();
            FormBorderStyle = FormBorderStyle.FixedDialog;
            ControlBox = false;
        }
        private void TextBoxDefault()
        {
            tBoxTopCode.Enabled = true;
            tBoxTopKr.Enabled = true;
            tBoxTopEn.Enabled = true;
            tBoxMidCode.Visible = false;
            tBoxMidKr.Visible = false;
            tBoxMidEn.Visible = false;
            tBoxBotCode.Visible = false;
            tBoxBotKr.Visible = false;
            tBoxBotEn.Visible = false;
            tBoxMidCode.Enabled = false;
            tBoxMidKr.Enabled = false;
            tBoxMidEn.Enabled = false;
            tBoxBotCode.Enabled = false;
            tBoxBotKr.Enabled = false;
            tBoxBotEn.Enabled = false;
            tBoxTopCode.Text = "";
            tBoxTopKr.Text = "";
            tBoxTopEn.Text = "";
            tBoxMidCode.Text = "";
            tBoxMidKr.Text = "";
            tBoxMidEn.Text = "";
            tBoxBotCode.Text = "";
            tBoxBotKr.Text = "";
            tBoxBotEn.Text = "";


        }

        public void GetCategoryinfo(string topCode, string midCode, string botcode, int workType)
        {
            TextBoxDefault();
            workTypeCode = workType;
            if (workType == 0) // 추가 등록
            {
                object resultObj = new object();
                string query;
                if (topCode == "0")
                {
                    query = "SELECT MAX(cat_top) + 1 FROM category";
                    dbcon.sqlScalaQuery(query, out resultObj);
                    tBoxTopCode.Text = resultObj.ToString();
                    tBoxTopCode.Enabled = false;
                    return;

                }

                if (topCode != "0" && midCode == "0")
                {
                    TopCategorySearch(topCode);
                    query = string.Format("SELECT ISNULL(MAX(cat_mid) + 1,1) FROM category WHERE cat_top = {0} AND cat_mid != 0", topCode);
                    dbcon.sqlScalaQuery(query, out resultObj);
                    tBoxMidCode.Text = resultObj.ToString();
                    tBoxTopCode.Enabled = false;
                    tBoxTopKr.Enabled = false;
                    tBoxTopEn.Enabled = false;
                    tBoxMidCode.Visible = true;
                    tBoxMidKr.Visible = true;
                    tBoxMidEn.Visible = true;
                    tBoxMidKr.Enabled = true;
                    tBoxMidEn.Enabled = true;
                }
                else if (midCode != "0" && botcode == "0")
                {
                    MidCategorySearch(topCode, midCode);
                    query = string.Format("SELECT ISNULL(MAX(cat_bot) + 1,1) FROM category WHERE cat_top = {0} AND cat_mid = {1} AND cat_bot !=0", topCode, midCode);
                    dbcon.sqlScalaQuery(query, out resultObj);
                    tBoxBotCode.Text = resultObj.ToString();
                    tBoxMidCode.Enabled = false;
                    tBoxMidKr.Enabled = false;
                    tBoxMidEn.Enabled = false;
                    tBoxBotCode.Visible = true;
                    tBoxBotKr.Visible = true;
                    tBoxBotEn.Visible = true;
                    tBoxBotKr.Enabled = true;
                    tBoxBotEn.Enabled = true;
                }
                workType = 0;
            }
            else if (workType == 1) // 수정
            {
                if (midCode == "0" && botcode == "0")
                {
                    TopCategorySearch(topCode);
                }
                if (midCode != "0" && botcode == "0")
                {
                    MidCategorySearch(topCode, midCode);
                }
                if (midCode != "0" && botcode != "0")
                {
                    BotCategorySearch(topCode, midCode, botcode);
                }
                workType = 1;
            }

        }

        private void TopCategorySearch(string topCode)
        {
            DataTable resultData = new DataTable();
            string query = string.Format("SELECT cat_name_kr,cat_name_en FROM category WHERE cat_top ={0} AND cat_mid = 0 AND cat_bot =0", topCode);
            dbcon.SqlReaderQuery(query, resultData);
            DataRow dr = resultData.Rows[0];
            tBoxTopCode.Text = topCode;
            tBoxTopKr.Text = dr[0].ToString();
            tBoxTopEn.Text = dr[1].ToString();
            tBoxTopCode.Enabled = false;


        }

        private void MidCategorySearch(string topCode, string midCode)
        {
            TopCategorySearch(topCode);
            DataTable resultData = new DataTable();
            string query = string.Format("SELECT cat_name_kr,cat_name_en FROM category WHERE cat_top ={0} AND cat_mid = {1} AND cat_bot =0", topCode, midCode);
            dbcon.SqlReaderQuery(query, resultData);
            DataRow dr = resultData.Rows[0];
            tBoxMidCode.Text = midCode;
            tBoxMidKr.Text = dr[0].ToString();
            tBoxMidEn.Text = dr[1].ToString();
            tBoxTopCode.Enabled = false;
            tBoxTopKr.Enabled = false;
            tBoxTopEn.Enabled = false;
            tBoxMidCode.Visible = true;
            tBoxMidKr.Visible = true;
            tBoxMidEn.Visible = true;
            tBoxMidCode.Enabled = true;
            tBoxMidKr.Enabled = true;
            tBoxMidEn.Enabled = true;
        }

        private void BotCategorySearch(string topCode, string midCode, string botCode)
        {
            TopCategorySearch(topCode);
            MidCategorySearch(topCode, midCode);
            DataTable resultData = new DataTable();
            string query = string.Format("SELECT cat_name_kr,cat_name_en FROM category WHERE cat_top ={0} AND cat_mid = {1} AND cat_bot ={2}", topCode, midCode, botCode);
            dbcon.SqlReaderQuery(query, resultData);
            DataRow dr = resultData.Rows[0];
            tBoxBotCode.Text = botCode;
            tBoxBotKr.Text = dr[0].ToString();
            tBoxBotEn.Text = dr[1].ToString();
            tBoxBotCode.Visible = true;
            tBoxBotKr.Visible = true;
            tBoxBotEn.Visible = true;
            tBoxMidCode.Enabled = false;
            tBoxMidKr.Enabled = false;
            tBoxMidEn.Enabled = false;
            tBoxMidCode.Visible = true;
            tBoxMidKr.Visible = true;
            tBoxMidEn.Visible = true;
            tBoxBotKr.Enabled = true;
            tBoxBotEn.Enabled = true;
        }

        private void InsertToDatabse(SqlConnection connection, SqlTransaction transaction)
        {
            string top = "0";
            string mid = "0";
            string bot = "0";
            string name_kr = "";
            string name_en = "";
            string query;
            object resultObj;
            if (!string.IsNullOrEmpty(tBoxTopCode.Text))
            {
                top = tBoxTopCode.Text;
            }
            if (!string.IsNullOrEmpty(tBoxMidCode.Text))
            {
                mid = tBoxMidCode.Text;
            }
            if (!string.IsNullOrEmpty(tBoxBotCode.Text))
            {
                bot = tBoxBotCode.Text;
            }
            query = string.Format("SELECT MAX(cat_code) + 1 FROM category ");
            dbcon.sqlScalaQuery(query, out resultObj);
            if (tBoxTopKr.Enabled == true)
            {
                //top = tBoxTopCode.Text;
                name_kr = tBoxTopKr.Text;
                name_en = tBoxTopEn.Text;
            }
            else if (tBoxMidKr.Enabled == true)
            {
                //top = tBoxMidCode.Text;
                name_kr = tBoxMidKr.Text;
                name_en = tBoxMidEn.Text;
            }
            else if (tBoxBotKr.Enabled == true)
            {
                //top = tBoxBotCode.Text;
                name_kr = tBoxBotKr.Text;
                name_en = tBoxBotEn.Text;
            }
            query = string.Format("INSERT INTO category (cat_code, cat_top, cat_mid, cat_bot,cat_name_kr, cat_name_en, cat_idate, cat_udate)" +
                "VALUES({0},{1},{2},{3},'{4}','{5}',{6},{7})", resultObj.ToString(), top, mid, bot, name_kr, name_en, "GETDATE()", "GETDATE()");
            dbcon.NonQuery(query, connection, transaction);
        }

        private void UpdateDatabse(SqlConnection connection, SqlTransaction transaction)
        {

            string top = "0";
            string mid = "0";
            string bot = "0";
            string name_kr = "";
            string name_en = "";
            string query;
            if (tBoxTopKr.Enabled == true)
            {
                top = tBoxTopCode.Text;
                name_kr = tBoxTopKr.Text;
                name_en = tBoxTopEn.Text;
            }
            else if (tBoxMidKr.Enabled == true)
            {
                top = tBoxTopCode.Text;
                mid = tBoxMidCode.Text;
                name_kr = tBoxMidKr.Text;
                name_en = tBoxMidEn.Text;
            }
            else if (tBoxBotKr.Enabled == true)
            {
                top = tBoxTopCode.Text;
                mid = tBoxMidCode.Text;
                bot = tBoxBotCode.Text;
                name_kr = tBoxBotKr.Text;
                name_en = tBoxBotEn.Text;
            }
            query = string.Format("UPDATE category SET cat_top = {0}, cat_mid = {1}, cat_bot = {2}, cat_name_kr ='{3}', cat_name_en = '{4}', cat_udate = GETDATE() " +
                "WHERE cat_top = {0} AND cat_mid = {1} AND cat_bot =  {2} ", top, mid, bot, name_kr, name_en);
            dbcon.NonQuery(query, connection, transaction);
        }
        private void RunQeury()
        {

            using (SqlConnection connection = dbcon.Opensql())
            using (SqlTransaction transaction = connection.BeginTransaction())
                try
                {
                    dbcon.NonQuery("BEGIN TRAN", connection, transaction);// BEGIN TRAN 선언

                    if (workTypeCode == 0)
                    {
                        InsertToDatabse(connection, transaction);
                    }
                    else if (workTypeCode == 1)
                    {
                        UpdateDatabse(connection, transaction);
                    }

                    // 성공시 commit tran  실행
                    dbcon.NonQuery("COMMIT TRAN", connection, transaction);
                    transaction.Commit();
                    Close();
                }

                catch (Exception ex)
                {
                    // 실패 발생시 rollback trna 실행 
                    dbcon.NonQuery("ROLLBACK TRAN", connection, transaction);
                    transaction.Rollback();
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    // 연결 종료
                    connection.Close();

                }

        }
        private void bntSave_Click(object sender, EventArgs e)
        {
            RunQeury();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
