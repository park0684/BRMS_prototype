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
    public partial class PurchaseList : Form
    {
        cDatabaseConnect dbconn = new cDatabaseConnect();
        cDataGridDefaultSet DgrPurchaseList = new cDataGridDefaultSet();
        bool supplierToggle = false;
        string supplierCode = "";
        int accessedEmp = 0;
        public PurchaseList()
        {
            InitializeComponent();
            panelDatagrid.Controls.Add(DgrPurchaseList.Dgr);
            DgrPurchaseList.Dgr.Dock = DockStyle.Fill;
            GridForm();
            LoadDefault();
            DgrPurchaseList.CellDoubleClick += DgrPurchaseList_CellDoubleClick;
            cBoxpurTypeInfo();
        }
        private void LoadDefault()
        {
            lblSupplierName.Text = "전체";
            dtpRegDateFrom.Format = DateTimePickerFormat.Short;
            dtpRegDateTo.Format = DateTimePickerFormat.Short;

        }
        private void GridForm()
        {
            DgrPurchaseList.Dgr.Columns.Add("purCode", "매입코드");
            DgrPurchaseList.Dgr.Columns.Add("purSupplier", "매입처");
            DgrPurchaseList.Dgr.Columns.Add("purSupcode", "공급사코드");
            DgrPurchaseList.Dgr.Columns.Add("purDate", "매입일");
            DgrPurchaseList.Dgr.Columns.Add("purAmount", "매입액");
            DgrPurchaseList.Dgr.Columns.Add("purPayment", "결제액");
            DgrPurchaseList.Dgr.Columns.Add("purType", "유형");
            DgrPurchaseList.Dgr.Columns.Add("purNote", "비고");
            DgrPurchaseList.Dgr.Columns.Add("purUdate", "수정일");
            DgrPurchaseList.FormatAsStringCenter("purCode", "purSupplier", "purSupcode", "purType");
            DgrPurchaseList.FormatAsDateTime("purDate", "purUdate");
            DgrPurchaseList.FormatAsInteger("purAmount", "purPayment");
            DgrPurchaseList.Dgr.ReadOnly = true;
            DgrPurchaseList.Dgr.Columns["purCode"].Visible = false;
            DgrPurchaseList.ApplyDefaultColumnSettings();
        }
        private void cBoxpurTypeInfo()
        {
            cBoxPurType.Items.Add("전체");
            cBoxPurType.Items.Add("매입");
            cBoxPurType.Items.Add("반품");
            cBoxPurType.SelectedIndex = 0;

        }
        private void GridFill(DataTable dataTable)
        {
            int rowIndex = 0;
            DgrPurchaseList.Dgr.Rows.Clear();
            foreach(DataRow dataRow in dataTable.Rows)
            {
                DgrPurchaseList.Dgr.Rows.Add();
                DgrPurchaseList.Dgr.Rows[rowIndex].Cells["No"].Value = DgrPurchaseList.Dgr.RowCount;
                DgrPurchaseList.Dgr.Rows[rowIndex].Cells["purCode"].Value = dataRow["pur_code"];
                DgrPurchaseList.Dgr.Rows[rowIndex].Cells["purSupplier"].Value = dataRow["sup_name"];
                DgrPurchaseList.Dgr.Rows[rowIndex].Cells["purSupcode"].Value = dataRow["pur_sup"];
                DgrPurchaseList.Dgr.Rows[rowIndex].Cells["purDate"].Value = dataRow["pur_date"];
                DgrPurchaseList.Dgr.Rows[rowIndex].Cells["purAmount"].Value = dataRow["pur_amount"];
                DgrPurchaseList.Dgr.Rows[rowIndex].Cells["purPayment"].Value = dataRow["pur_payment"];
                DgrPurchaseList.Dgr.Rows[rowIndex].Cells["purType"].Value = cBoxPurType.Items[int.Parse(dataRow["pur_type"].ToString())].ToString();
                DgrPurchaseList.Dgr.Rows[rowIndex].Cells["purNote"].Value = dataRow["pur_note"];
                DgrPurchaseList.Dgr.Rows[rowIndex].Cells["purUdate"].Value = dataRow["pur_udate"];
                rowIndex++;

            }
        }
        private void QuerySetting()
        {
            DataTable resultData = new DataTable();
            string query = string.Format("SELECT pur_code, sup_name, pur_sup, pur_date, pur_amount, pur_payment, pur_type, pur_note, pur_udate FROM purchase,supplier " +
                "WHERE pur_sup =  sup_code AND pur_date >= '{0}' ANd pur_date < '{1}' ", dtpRegDateFrom.Value.ToString("yyyy-MM-dd"), dtpRegDateTo.Value.AddDays(1).ToString("yyyy-MM-dd"));
            if (supplierCode != "")
            {
                query = string.Format(query + " AND sup_code ={0}", supplierCode);
            }

            switch (cBoxPurType.SelectedIndex)
            {
                case 1:
                    query = string.Format(query + " AND pur_type = 1");
                    break;
                case 2:
                    query = string.Format(query + " AND pur_type = 2");
                    break;

            }

            dbconn.SqlDataAdapterQuery(query, resultData);
            GridFill(resultData);
            cLog.InsertEmpAccessLogNotConnect("@purSearch", accessedEmp, 0);
        }

        public void RunQuery()
        {
            try
            {
                QuerySetting();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DgrPurchaseList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectPurCode = DgrPurchaseList.ConvertToInt(DgrPurchaseList.Dgr.CurrentRow.Cells["purCode"].Value);
            PurchaseDetail purchaseDetail = new PurchaseDetail();
            purchaseDetail.StartPosition = FormStartPosition.CenterParent;
            purchaseDetail.GetPurchaseCode(selectPurCode);
            cLog.InsertEmpAccessLogNotConnect("@purSearch", accessedEmp, selectPurCode);
            purchaseDetail.ShowDialog();
        }

        private void bntSupplier_Click(object sender, EventArgs e)
        {
            if(supplierToggle == true)
            {
                supplierToggle = false;
                lblSupplierName.Text = "전체";
                supplierCode = "";
            }
            else
            {
                SupplierSelectBox supplierSelectBox = new SupplierSelectBox();
                supplierSelectBox.SupplierSelected += (supCode, supName) => { GetSupplier(supCode, supName); };
                supplierSelectBox.StartPosition = FormStartPosition.CenterParent;
                supplierSelectBox.ShowDialog();
            }
            
        }

        private void GetSupplier(int supCode, string supName)
        {
            lblSupplierName.Text = supName;
            supplierCode = Convert.ToString(supCode);
            supplierToggle = true;
        }


        private void bntPurchaseReg_Click(object sender, EventArgs e)
        {
            PurchaseDetail purchaseDetail = new PurchaseDetail();
            purchaseDetail.StartPosition = FormStartPosition.CenterParent;
            int supCode = 0;
            purchaseDetail.callSup += (supplierCode) => { supCode = supplierCode; };
            purchaseDetail.Addpurchase();
            
            //PurchaseDetail의 Addpurchase 실행시 SupplierSelectBox클래스 호출하여 공급사를 선택.
            //이때 선택하지 않고 닫을 경우 PurchaseDetail 클래스의 supplierCode값은 기본값인 0이다.
            //이 변수를 이벤트로 받아 supCode에 기록, 만약 그 값이 0이라면 PurchaseDetail 호출 자체를 취소한다.
            if (supCode != 0)
            { 
                purchaseDetail.ShowDialog(); 
            };
        }
    }
}
