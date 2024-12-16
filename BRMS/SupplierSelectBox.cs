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
    public partial class SupplierSelectBox : Form
    {
        cDatabaseConnect dbconn = new cDatabaseConnect();
        cDataGridDefaultSet DgrSupplier = new cDataGridDefaultSet();
        public event Action<int,string> SupplierSelected;
        public SupplierSelectBox()
        {
            InitializeComponent();
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            panelDataGrid.Controls.Add(DgrSupplier.Dgr);
            DgrSupplier.Dgr.Dock = DockStyle.Fill;
            DgrSupplier.CellDoubleClick += DgrSupplier_CellDoubleClick;
            GridForm();
            GridFill();
        }

        private void GridForm()
        {
            DgrSupplier.Dgr.Columns.Add("supCode", "사코드");
            DgrSupplier.Dgr.Columns.Add("supName", "공급사명");
            DgrSupplier.Dgr.Columns["No"].Visible = false;
            DgrSupplier.Dgr.ReadOnly = true;
            DgrSupplier.Dgr.Columns["supCode"].Width = 90;
            DgrSupplier.Dgr.Columns["supName"].Width = 180;
            DgrSupplier.FormatAsStringLeft("supName");
            DgrSupplier.FormatAsStringCenter("supCode");
            DgrSupplier.Dgr.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void GridFill()
        {
            DataTable resultData = new DataTable();          
            string query = "SELECT sup_code, sup_name FROM supplier WHERE sup_status != 0";
            dbconn.SqlDataAdapterQuery(query, resultData);
            int rowIndex = 0;
            foreach (DataRow dataRow in resultData.Rows)
            {
                DgrSupplier.Dgr.Rows.Add();
                //DgrSupplier.Dgr.Rows[rowIndex].Cells["No"].Value = DgrSupplier.Dgr.RowCount;
                DgrSupplier.Dgr.Rows[rowIndex].Cells["supCode"].Value = dataRow["sup_code"].ToString();
                DgrSupplier.Dgr.Rows[rowIndex].Cells["supName"].Value = dataRow["sup_name"].ToString();
                rowIndex++;
            }

        }
        private void SelectSupplier()
        {
            int supCode = DgrSupplier.ConvertToInt(DgrSupplier.Dgr.CurrentRow.Cells["supCode"].Value);
            string supName = DgrSupplier.Dgr.CurrentRow.Cells["supName"].Value.ToString();
            SupplierSelected?.Invoke(supCode, supName);
            Close();
        }
        private void DgrSupplier_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectSupplier();
        }
        private void bntClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bntSelect_Click(object sender, EventArgs e)
        {
            SelectSupplier();
        }
    }
}
