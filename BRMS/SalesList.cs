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
    public partial class SalesList : Form
    {
        cDatabaseConnect dbconn = new cDatabaseConnect();
        cDataGridDefaultSet SaleList = new cDataGridDefaultSet();
        int accessedEmp = 0;

        public SalesList()
        {
            InitializeComponent();
            panelDatagrid.Controls.Add(SaleList.Dgr);
            SaleList.Dgr.Dock = DockStyle.Fill;
            SaleList.CellDoubleClick += SaleList_CellDoubleClick;
            InitializeComboBox();
            GridForm();
        }

        private void bntSalesRegist_Click(object sender, EventArgs e)
        {
            SalesRegist salesRegist = new SalesRegist();
            salesRegist.StartPosition = FormStartPosition.CenterParent;
            salesRegist.ShowDialog();

        }
        private void InitializeComboBox()
        {
            cmBoxSaleType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmBoxSaleType.Items.AddRange(new string[] { "반품", "판매", "전체" });
            cmBoxSaleType.SelectedIndex = 2;
        }
        private void GridForm()
        {
            SaleList.Dgr.Columns.Add("saleCode", "거래번호");
            SaleList.Dgr.Columns.Add("saleType", "유형");
            SaleList.Dgr.Columns.Add("saleDate", "판매일");
            SaleList.Dgr.Columns.Add("saleAmountKrw", "판매액( ￦)");
            SaleList.Dgr.Columns.Add("saleAmountUsd", "판매액(＄)");
            SaleList.Dgr.Columns.Add("saleCash", "현금");
            SaleList.Dgr.Columns.Add("saleAccount", "계좌");
            SaleList.Dgr.Columns.Add("saleCard", "카드");
            SaleList.Dgr.Columns.Add("salePoint", "포인트");
            SaleList.Dgr.Columns.Add("saleDc", "할인");
            SaleList.Dgr.Columns.Add("saleDelfee", "배송료");
            SaleList.Dgr.Columns.Add("saleCustCode", "회원코드");
            SaleList.Dgr.Columns.Add("saleCustName", "회원명");
            SaleList.Dgr.Columns.Add("saleReward", "적립");
            SaleList.Dgr.Columns.Add("saleDelivery", "배달번호");

            SaleList.FormatAsDateTime("saleDate");
            SaleList.FormatAsStringCenter("SaleType", "saleCode", "saleCustNmme","saleDelivery");
            SaleList.FormatAsStringRight("saleCash", "saleAccount", "saleCard", "salePoint", "saleDc");
            SaleList.FormatAsInteger("saleCode", "saleAmountKrw", "saleReward");
            SaleList.FormatAsDecimal("saleAmountUsd");
            SaleList.Dgr.ReadOnly = true;
            SaleList.ApplyDefaultColumnSettings();
        }
        private void GridFill(DataTable dataTable)
        {
            int rowIndex = 0;
            object resultObj = new object();
            DataTable resultTable = new DataTable();

            SaleList.Dgr.Rows.Clear();
            foreach (DataRow saleDataRow in dataTable.Rows)
            {
                SaleList.Dgr.Rows.Add();
                string query = $"SELECT cust_name FROM customer WHERE cust_code = {saleDataRow["sale_cust"]}";
                dbconn.sqlScalaQuery(query, out resultObj);
                query = $"SELECT spay_cash_krw, spay_cash_use, spay_account_krw, spay_account_usd, spay_credit_krw, spay_credit_usd, spay_point_krw, spay_point_usd, spay_exchenge FROM salepay WHERE spay_salecode = {saleDataRow["sale_code"]} ";
                resultTable.Clear();
                dbconn.SqlReaderQuery(query, resultTable);
                DataRow salepayRow = resultTable.Rows[0];
                int cashKrw = Convert.ToInt32(salepayRow["spay_cash_krw"]);
                int accountKrw = Convert.ToInt32(salepayRow["spay_account_krw"]);
                int cardKrw = Convert.ToInt32(salepayRow["spay_credit_krw"]);
                int pointKrw = Convert.ToInt32(salepayRow["spay_point_krw"]);
                decimal cashUsd = Convert.ToDecimal(salepayRow["spay_cash_use"]);
                decimal accountUsd = Convert.ToDecimal(salepayRow["spay_account_usd"]);
                decimal cardUsd = Convert.ToDecimal(salepayRow["spay_credit_usd"]);
                decimal pointUsd = Convert.ToDecimal(salepayRow["spay_point_usd"]);
                SaleList.Dgr.Rows[rowIndex].Cells["No"].Value = rowIndex + 1;
                SaleList.Dgr.Rows[rowIndex].Cells["saleCode"].Value = saleDataRow["sale_code"];
                SaleList.Dgr.Rows[rowIndex].Cells["saleType"].Value = Convert.ToInt32(saleDataRow["sale_type"]) == 1 ? "판매":"반품";
                SaleList.Dgr.Rows[rowIndex].Cells["saleDate"].Value = saleDataRow["sale_date"];
                SaleList.Dgr.Rows[rowIndex].Cells["saleAmountKrw"].Value = saleDataRow["sale_sprice_krw"];
                SaleList.Dgr.Rows[rowIndex].Cells["saleAmountUsd"].Value = saleDataRow["sale_sprice_usd"];
                SaleList.Dgr.Rows[rowIndex].Cells["saleCash"].Value = $"{cashKrw.ToString("#,##0")}({cashUsd.ToString("#,##0.00")})";
                SaleList.Dgr.Rows[rowIndex].Cells["saleAccount"].Value = $"{accountKrw.ToString("#,##0")}({accountUsd.ToString("#,##0.00")})";
                SaleList.Dgr.Rows[rowIndex].Cells["saleCard"].Value = $"{cardKrw.ToString("#,##0")}({cardUsd.ToString("#,##0.00")})";
                SaleList.Dgr.Rows[rowIndex].Cells["salePoint"].Value = $"{pointKrw.ToString("#,##0")}({pointUsd.ToString("#,##0.00")})";
                SaleList.Dgr.Rows[rowIndex].Cells["saleDc"].Value = saleDataRow["sale_dc"];
                SaleList.Dgr.Rows[rowIndex].Cells["saleDelfee"].Value = saleDataRow["sale_delfee"];
                SaleList.Dgr.Rows[rowIndex].Cells["saleCustCode"].Value = saleDataRow["sale_cust"];
                SaleList.Dgr.Rows[rowIndex].Cells["saleCustName"].Value = resultObj?.ToString().Trim() ?? "";
                SaleList.Dgr.Rows[rowIndex].Cells["saleReward"].Value = saleDataRow["sale_reward"];
                SaleList.Dgr.Rows[rowIndex].Cells["saleDelivery"].Value = saleDataRow["sale_delivery"];
                rowIndex++;

            }
        }
        private void QuerySetting()
        {
            DateTime fromDate = dtpDateFrom.Value;
            DateTime toDate = dtpDateTo.Value;
            toDate = toDate.AddDays(1);
            DataTable resultData = new DataTable();
            string query = "SELECT sale_code, sale_date, sale_cust, sale_type, sale_bprice, sale_sprice_krw, sale_sprice_usd, sale_dc, sale_tax, sale_reward, sale_delivery, sale_delfee FROM sales ";
            List<string> conditions = new List<string>();

            string saveDateCondition = $"WHERE sale_date BETWEEN '{fromDate:yyyy-MM-dd}' AND '{toDate:yyyy-MM-dd}'" ;
            query += saveDateCondition;

            if(cmBoxSaleType.SelectedIndex != 2)
            {
                string saleTypeCondition = $"sale_type = {cmBoxSaleType.SelectedIndex}";
                conditions.Add(saleTypeCondition);
            }

            for(int i = 0; i < conditions.Count; i++ )
            {
                query += " AND " + conditions[i];
            }
            dbconn.SqlDataAdapterQuery(query, resultData);
            GridFill(resultData);
            cLog.InsertEmpAccessLogNotConnect("@saleSearch", accessedEmp, 0);
        }
        /// <summary>
        /// 조회 버튼 클릭
        /// </summary>
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
        private void SaleList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int saleCode = SaleList.ConvertToInt(SaleList.Dgr.CurrentRow.Cells["saleCode"].Value);
            SalesRegist salesRegist = new SalesRegist();
            salesRegist.GetSaleCode(saleCode);
            cLog.InsertEmpAccessLogNotConnect("@customerSaleSearch", accessedEmp, saleCode);
            salesRegist.StartPosition = FormStartPosition.CenterParent;
            salesRegist.ShowDialog();
        }
    }
}
