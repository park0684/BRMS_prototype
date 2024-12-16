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
    public partial class ProductSearchBox : Form
    {
        cDatabaseConnect dbconn = new cDatabaseConnect();
        cDataGridDefaultSet DgrPdtSearch = new cDataGridDefaultSet();
        public event Action<int> ProductForword;
        public ProductSearchBox()
        {
            InitializeComponent();
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            panelDataGrid.Controls.Add(DgrPdtSearch.Dgr);
            GridForm();
            DgrPdtSearch.Dgr.Dock = DockStyle.Fill;
            DgrPdtSearch.CellDoubleClick += DgrDgrPdtSearch_CellDoubleClick;
            tBoxSearch.KeyUp += tBoxSearch_KeyUpEnter;
            DgrPdtSearch.Dgr.KeyDown += DgrPdtSearch_KeyDown;
            DgrPdtSearch.ApplyDefaultColumnSettings();
        }

        private void GridForm()
        {
            DgrPdtSearch.Dgr.Columns.Add("pdtCode", "제품코드");
            DgrPdtSearch.Dgr.Columns.Add("pdtNumber", "제품번호");
            DgrPdtSearch.Dgr.Columns.Add("pdtNameKr", "제품명(한글)");
            DgrPdtSearch.Dgr.Columns.Add("pdtNameEn", "제품명(영문)");
            DgrPdtSearch.Dgr.Columns.Add("pdtBprice", "매입가");
            DgrPdtSearch.Dgr.Columns.Add("pdtSpriceKrw", "판매가");
            DgrPdtSearch.Dgr.Columns["pdtCode"].Visible = false;

            DgrPdtSearch.FormatAsStringLeft("pdtNumber", "pdtNameKr", "pdtNameen");
            DgrPdtSearch.FormatAsDecimal("매입가");
            DgrPdtSearch.FormatAsInteger("판매가");

            DgrPdtSearch.Dgr.ReadOnly = true;
            DgrPdtSearch.Dgr.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private void FillGrid(DataTable dataTable)
        {

            foreach (DataRow dataRow in dataTable.Rows)
            {
                int i = dataTable.Rows.IndexOf(dataRow);
                DgrPdtSearch.Dgr.Rows.Add();
                DgrPdtSearch.Dgr.Rows[i].Cells["no"].Value = i + 1;
                DgrPdtSearch.Dgr.Rows[i].Cells["pdtCode"].Value = dataRow["pdt_code"].ToString();
                DgrPdtSearch.Dgr.Rows[i].Cells["pdtNumber"].Value = dataRow["pdt_number"].ToString().Trim();
                DgrPdtSearch.Dgr.Rows[i].Cells["pdtNameKr"].Value = dataRow["pdt_name_kr"].ToString().Trim();
                DgrPdtSearch.Dgr.Rows[i].Cells["pdtNameEn"].Value = dataRow["pdt_name_en"].ToString().Trim();
                DgrPdtSearch.Dgr.Rows[i].Cells["pdtBprice"].Value = dataRow["pdt_bprice"].ToString();
                DgrPdtSearch.Dgr.Rows[i].Cells["pdtSpriceKrw"].Value = dataRow["pdt_sprice_krw"].ToString();

            }
        }
        private void SearchQuery()
        {
            string query = string.Format("SELECT pdt_code,pdt_number,pdt_name_kr,pdt_name_en,pdt_bprice,pdt_sprice_krw FROM product WHERE pdt_number like '%{0}%' \n union\n" +
                "SELECT pdt_code,pdt_number,pdt_name_kr,pdt_name_en,pdt_bprice,pdt_sprice_krw  FROM product WHERE pdt_name_kr LIKE '%{0}%'\n UNION \n" +
                "SELECT pdt_code,pdt_number,pdt_name_kr,pdt_name_en,pdt_bprice,pdt_sprice_krw  FROM product WHERE pdt_name_en LIKE'%{0}%'", tBoxSearch.Text);
            DataTable dataTable = new DataTable();
            dbconn = new cDatabaseConnect();
            dbconn.SqlDataAdapterQuery(query, dataTable);
            FillGrid(dataTable);
            DgrPdtSearch.Dgr.Focus();
        }
        private void tBoxSearch_KeyUpEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchQuery();
            }
        }
        private void DgrPdtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectProduct();
            }
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                // 방향키로 셀 선택 시, 현재 선택된 셀을 커서로 이동시킵니다.
                DgrPdtSearch.Dgr.BeginEdit(true); // 편집을 시작하여 커서를 이동시킵니다.
            }
        }
        private void SelectProduct()
        {
            try
            {
                if(DgrPdtSearch.Dgr.CurrentRow != null)
                {
                    int currentIndex = DgrPdtSearch.Dgr.CurrentRow.Index;
                    int pdtCode = DgrPdtSearch.ConvertToInt(DgrPdtSearch.Dgr.Rows[currentIndex].Cells["pdtCode"].Value);
                    ProductForword?.DynamicInvoke(pdtCode);
                    Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DgrDgrPdtSearch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectProduct();
        }

        private void bntSearch_Click(object sender, EventArgs e)
        {
            SearchQuery();
        }

        private void bntOk_Click(object sender, EventArgs e)
        {
            SelectProduct();
        }

        private void bntClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
