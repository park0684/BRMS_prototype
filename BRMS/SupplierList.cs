using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BRMS
{
    public partial class SupplierList : Form
    {
        cDatabaseConnect dbconn = new cDatabaseConnect();
        cDataGridDefaultSet DgrSupplierList = new cDataGridDefaultSet();
        int refresh = 0;
        int accessedEmp = 1;
        public SupplierList()
        {
            InitializeComponent();
            panelDataGrid.Controls.Add(DgrSupplierList.Dgr);
            DgrSupplierList.Dgr.Dock = DockStyle.Fill;
            GridForm();
            DgrSupplierList.Dgr.CellDoubleClick += DgrSupplierList_CellDoubleClick;

        }

        private void GridForm()
        {
            List<string> gridNames = new List<string> { "supCode", "supBzname", "supBzNumber", "supTel", "supFax", "supCel", "supEmail", "supUrl", "supMemo" };
            List<string> gridText = new List<string> { "공급사코드", "공급사명", "사업자번호", "전화", "팩스", "휴대전화", "E메일", "URL", "메모" };
            for (int i = 0; i < gridNames.Count; i++)
            {
                // DataGridViewTextBoxColumn을 사용하여 텍스트 열을 생성합니다.
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                column.Name = gridNames[i]; // 열의 이름을 설정합니다.
                column.HeaderText = gridText[i]; // 열의 헤더 텍스트를 설정합니다.
                // DataGridView에 열을 추가합니다.
                DgrSupplierList.Dgr.Columns.Add(column);
            }
            DgrSupplierList.Dgr.ReadOnly = true;
            DgrSupplierList.FormatAsStringLeft("supBzname", "supBzNumber");
            DgrSupplierList.FormatAsStringCenter("supCode", "supTel", "supFax", "supCel", "supEmail", "supUrl");
            DgrSupplierList.ApplyDefaultColumnSettings();
            DgrSupplierList.Dgr.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            
        }
        public void RunQuery()
        {
            string query = "SELECT sup_code,sup_name,sup_bzno,sup_tel,sup_fax,sup_cel,sup_email, sup_url,sup_memo FROM supplier ";
            DataTable resultData = new DataTable();
            if (!string.IsNullOrEmpty(tBoxSearch.Text))
            {
                query = query + string.Format("WHERE sup_name LIKE '%{0}%'", tBoxSearch.Text);
            }
            dbconn.SqlDataAdapterQuery(query, resultData);
            gridFill(resultData);
            cLog.InsertEmpAccessLogNotConnect("@supplierSearch", accessedEmp, 0);
        }
        private void gridFill(DataTable dataTable)
        {
            DgrSupplierList.Dgr.Rows.Clear();
            int rowIndex = 0;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                DgrSupplierList.Dgr.Rows.Add();
                DgrSupplierList.Dgr.Rows[rowIndex].Cells["No"].Value = DgrSupplierList.Dgr.RowCount;
                DgrSupplierList.Dgr.Rows[rowIndex].Cells["supCode"].Value = dataRow["sup_code"].ToString();
                DgrSupplierList.Dgr.Rows[rowIndex].Cells["supBzname"].Value = dataRow["sup_name"].ToString();
                DgrSupplierList.Dgr.Rows[rowIndex].Cells["supBzNumber"].Value = dataRow["sup_bzno"].ToString();
                DgrSupplierList.Dgr.Rows[rowIndex].Cells["supTel"].Value = dataRow["sup_tel"].ToString();
                DgrSupplierList.Dgr.Rows[rowIndex].Cells["supFax"].Value = dataRow["sup_fax"].ToString();
                DgrSupplierList.Dgr.Rows[rowIndex].Cells["supCel"].Value = dataRow["sup_cel"].ToString();
                DgrSupplierList.Dgr.Rows[rowIndex].Cells["supEmail"].Value = dataRow["sup_email"].ToString();
                DgrSupplierList.Dgr.Rows[rowIndex].Cells["supUrl"].Value = dataRow["sup_url"].ToString();
                DgrSupplierList.Dgr.Rows[rowIndex].Cells["supMemo"].Value = dataRow["sup_memo"].ToString();
                rowIndex++;
            }
        }
        private void CallSupplierDetail(int supplierCode)
        {
            SupplierDetail supplierDetail = new SupplierDetail();
            supplierDetail.StartPosition = FormStartPosition.CenterParent;
            supplierDetail.GetSupplierCode(supplierCode);
            cLog.InsertEmpAccessLogNotConnect("@supplierSearch", accessedEmp, supplierCode);
            supplierDetail.refresh += (refreshCode) => refresh = refreshCode;
            supplierDetail.ShowDialog();
            if (refresh == 1)
            {
                DialogResult result = MessageBox.Show("다시 조회 하시겠습니까?", "알림", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    RunQuery();
                    refresh = 0;
                }
            }
        }
        private void DgrSupplierList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int currentRowIndex = e.RowIndex;
            if(currentRowIndex >= 0 )
            {
                DataGridViewRow currentRow = DgrSupplierList.Dgr.Rows[currentRowIndex];
                int supCode = DgrSupplierList.ConvertToInt(currentRow.Cells["supCode"].Value);
                CallSupplierDetail(supCode);
            }
        }

        private void bntAddSupplier_Click(object sender, EventArgs e)
        {
            CallSupplierDetail(0);
        }
    }
}
