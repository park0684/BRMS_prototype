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
    public partial class CategoryBoard : Form
    {
        private cDataGridDefaultSet DgrTopCategory;
        private cDataGridDefaultSet DgrMidCategory;
        private cDataGridDefaultSet DgrBotCategory;
        cDatabaseConnect dbconn = new cDatabaseConnect();
        CategoryEdit categoryEdit = new CategoryEdit();
        public event Action<int, int, int> CategorySelected;
        int WorkTye = 0; // 0== 분류 지정 | 1 == 분류설정 | 2 == 분류 조회 조건 설정
        int pdtTop = 0;
        int pdtMid = 0;
        int pdtBot = 0;
        int empCode = 0;
        public CategoryBoard()
        {
            InitializeComponent();
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            DataGridViewForm();
            GetTopCategoryInfo();
            DgrTopCategory.Dgr.CellClick += CurrentDatagridviewTop;
            DgrMidCategory.Dgr.CellClick += CurrentDatagridviewMid;
            DgrBotCategory.Dgr.CellClick += CurrentDatagridviewBot;
        }

        public void GetWorkType(int workType)
        {
            WorkTye = workType;
            {
                if(WorkTye == 1)
                {
                    bntOk.Visible = false;
                }
                else if(WorkTye != 1)
                {
                    bntBotCategoryAdd.Visible = false;
                    bntMidCategoryAdd.Visible = false;
                    bntTopCategoryAdd.Visible = false;
                    bntBotCategoryModify.Visible = false;
                    bntMidCategoryModify.Visible = false;
                    bntTopCategoryModify.Visible = false;
                }
            }
        }

        private void DataGridViewForm()
        {
            DgrTopCategory = new cDataGridDefaultSet();
            DgrMidCategory = new cDataGridDefaultSet();
            DgrBotCategory = new cDataGridDefaultSet();
            panelCategoryTop.Controls.Add(DgrTopCategory.Dgr);
            DgrTopCategory.Dgr.Dock = DockStyle.Fill;
            panelCategoryMid.Controls.Add(DgrMidCategory.Dgr);
            DgrMidCategory.Dgr.Dock = DockStyle.Fill;
            panelCategoryBot.Controls.Add(DgrBotCategory.Dgr);
            DgrBotCategory.Dgr.Dock = DockStyle.Fill;

            DgrTopCategory.Dgr.Columns.Add("catTop", "");
            DgrTopCategory.Dgr.Columns.Add("CatTopNameKr", "분류명");
            DgrTopCategory.Dgr.Columns.Add("CatTopNameEn", "분류명");
            DgrMidCategory.Dgr.Columns.Add("catMid", "");
            DgrMidCategory.Dgr.Columns.Add("CatMidNameKr", "분류명");
            DgrMidCategory.Dgr.Columns.Add("CatMidNameEn", "분류명");
            DgrBotCategory.Dgr.Columns.Add("CatBot", "");
            DgrBotCategory.Dgr.Columns.Add("CatBotNamekr", "분류명");
            DgrBotCategory.Dgr.Columns.Add("CatBotNameEn", "분류명");
            DgrTopCategory.Dgr.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DgrMidCategory.Dgr.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DgrBotCategory.Dgr.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DgrTopCategory.FormatAsStringCenter("catTop", "CatTopNameKr", "CatTopNameEn","catMid", "CatMidNameKr", "CatMidNameEn", "CatBot", "CatBotNamekr", "CatBotNameEn");
            DgrTopCategory.Dgr.ReadOnly = true;
            DgrMidCategory.Dgr.ReadOnly = true;
            DgrBotCategory.Dgr.ReadOnly = true;
            DgrTopCategory.Dgr.Columns["catTop"].Width = 30;
            DgrMidCategory.Dgr.Columns["catMid"].Width = 30;
            DgrBotCategory.Dgr.Columns["CatBot"].Width = 30;
            DgrTopCategory.Dgr.Columns["no"].Visible = false;
            DgrMidCategory.Dgr.Columns["no"].Visible = false;
            DgrBotCategory.Dgr.Columns["no"].Visible = false;


            //그리드 정렬 기능 제외
            // Top DataGridView
            foreach (DataGridViewColumn column in DgrTopCategory.Dgr.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // Mid DataGridView
            foreach (DataGridViewColumn column in DgrMidCategory.Dgr.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // Bot DataGridView
            foreach (DataGridViewColumn column in DgrBotCategory.Dgr.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void GetTopCategoryInfo()
        {
            DgrTopCategory.Dgr.Rows.Clear();
            DataTable dataTable = new DataTable();
            string query = string.Format("SELECT cat_top, cat_name_kr,cat_name_en FROm category WHERE cat_top != 0 AND cat_mid = 0 AND cat_bot = 0");
            dbconn.SqlDataAdapterQuery(query, dataTable);
            int rowIndex = 0;
            foreach(DataRow dataRow in dataTable.Rows)
            {
                DgrTopCategory.Dgr.Rows.Add();
                DgrTopCategory.Dgr.Rows[rowIndex].Cells[1].Value = dataRow[0];
                DgrTopCategory.Dgr.Rows[rowIndex].Cells[2].Value = dataRow[1];
                DgrTopCategory.Dgr.Rows[rowIndex].Cells[3].Value = dataRow[2];
                rowIndex++;
            }
        }

        private void GetMidCategoryInfo()
        {
            DgrMidCategory.Dgr.Rows.Clear();
            DataTable dataTable = new DataTable();
            string query = string.Format("SELECT cat_mid, cat_name_kr, cat_name_en FROM category WHERE cat_top ={0} AND cat_mid != 0 AND cat_bot = 0"
                , DgrTopCategory.Dgr.CurrentRow.Cells[1].Value.ToString());
            dbconn.SqlDataAdapterQuery(query, dataTable);
            int rowIndex = 0;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                DgrMidCategory.Dgr.Rows.Add();
                DgrMidCategory.Dgr.Rows[rowIndex].Cells[1].Value = dataRow[0];
                DgrMidCategory.Dgr.Rows[rowIndex].Cells[2].Value = dataRow[1];
                DgrMidCategory.Dgr.Rows[rowIndex].Cells[3].Value = dataRow[2];
                rowIndex++;
            }
            if(WorkTye == 2)
            {
                DgrMidCategory.Dgr.ClearSelection();
            }
        }

        private void GetBotCategoryInfo()
        {
            DgrBotCategory.Dgr.Rows.Clear();
            DataTable dataTable = new DataTable();
            string query = string.Format("SELECT cat_bot, cat_name_kr, cat_name_en FROM category WHERE cat_top ={0} AND cat_mid = {1} AND cat_bot != 0"
                , DgrTopCategory.Dgr.CurrentRow.Cells[1].Value.ToString(), DgrMidCategory.Dgr.CurrentRow.Cells[1].Value.ToString());
            dbconn.SqlDataAdapterQuery(query, dataTable);
            int rowIndex = 0;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                DgrBotCategory.Dgr.Rows.Add();
                DgrBotCategory.Dgr.Rows[rowIndex].Cells[1].Value = dataRow[0];
                DgrBotCategory.Dgr.Rows[rowIndex].Cells[2].Value = dataRow[1];
                DgrBotCategory.Dgr.Rows[rowIndex].Cells[3].Value = dataRow[2];
                rowIndex++;
            }
            if (WorkTye == 2)
            {
                DgrBotCategory.Dgr.ClearSelection();
            }
        }

        private void CurrentDatagridviewTop(object sener, DataGridViewCellEventArgs e)
        {
            int selectedRowIndex = e.RowIndex;
            if(selectedRowIndex >=0)
            {
                DgrMidCategory.Dgr.Rows.Clear();
                DgrBotCategory.Dgr.Rows.Clear();
                DataGridViewRow selectedRow = DgrTopCategory.Dgr.Rows[selectedRowIndex];
                GetMidCategoryInfo();
                pdtTop = Convert.ToInt32(DgrTopCategory.Dgr.CurrentRow.Cells[1].Value);

            }
        }

        private void CurrentDatagridviewMid(object sener, DataGridViewCellEventArgs e)
        {
            int selectedRowIndex = e.RowIndex;
            if (selectedRowIndex >= 0)
            {
                DgrBotCategory.Dgr.Rows.Clear();
                DataGridViewRow selectedRow = DgrMidCategory.Dgr.Rows[selectedRowIndex];
                GetBotCategoryInfo();
                pdtMid = Convert.ToInt32(DgrMidCategory.Dgr.CurrentRow.Cells[1].Value);
            }
        }

        private void CurrentDatagridviewBot(object sener, DataGridViewCellEventArgs e)
        {
            pdtBot = Convert.ToInt32(DgrBotCategory.Dgr.CurrentRow.Cells[1].Value);
        }
        private void CallCategoryEditFrom()
        {

            categoryEdit.StartPosition = FormStartPosition.CenterParent;
            categoryEdit.ShowDialog();
        }


        private void bntTopCategoryModify_Click(object sender, EventArgs e)
        {
            int row = DgrTopCategory.Dgr.CurrentRow.Index;
            string topCode = DgrTopCategory.Dgr.CurrentRow.Cells["catTop"].Value.ToString();
            string midCode = "0";
            string botCode = "0";
            categoryEdit.GetCategoryinfo(topCode, midCode, botCode, 1);
            CallCategoryEditFrom();
            GetTopCategoryInfo();
        }

        private void bntMidCategoryModify_Click(object sender, EventArgs e)
        {
            if (DgrMidCategory.Dgr.RowCount > 0)
            {
                string topCode = DgrTopCategory.Dgr.CurrentRow.Cells["catTop"].Value.ToString();
                string midCode = DgrMidCategory.Dgr.CurrentRow.Cells["catMid"].Value.ToString();
                string botCode = "0";
                categoryEdit.GetCategoryinfo(topCode, midCode, botCode, 1);
                CallCategoryEditFrom();
                GetMidCategoryInfo();
            }
            else
            {
                MessageBox.Show("수정 할 수 있는 중분류가 없습니다");
            }
        }

        private void bntBotCategoryModify_Click(object sender, EventArgs e)
        {
            if (DgrBotCategory.Dgr.RowCount > 0)
            {
                string topCode = DgrTopCategory.Dgr.CurrentRow.Cells["catTop"].Value.ToString();
                string midCode = DgrMidCategory.Dgr.CurrentRow.Cells["catMid"].Value.ToString();
                string botCode = DgrBotCategory.Dgr.CurrentRow.Cells["catBot"].Value.ToString();
                categoryEdit.GetCategoryinfo(topCode, midCode, botCode, 1);
                CallCategoryEditFrom();
                GetBotCategoryInfo();
            }
            else
            {
                MessageBox.Show("수정 할 수 있는 소분류가 없습니다");
            }
        }

        private void bntTopCategoryAdd_Click(object sender, EventArgs e)
        {
            string topCode = "0";
            string midCode = "0";
            string botCode = "0";
            categoryEdit.GetCategoryinfo(topCode, midCode, botCode, 0);
            CallCategoryEditFrom();
            GetTopCategoryInfo();
        }

        private void bntMidCategoryAdd_Click(object sender, EventArgs e)
        {
            string topCode = DgrTopCategory.Dgr.CurrentRow.Cells["catTop"].Value.ToString();
            string midCode = "0";
            string botCode = "0";
            categoryEdit.GetCategoryinfo(topCode, midCode, botCode, 0);
            CallCategoryEditFrom();
            GetMidCategoryInfo();
        }

        private void bntBotCategoryAdd_Click(object sender, EventArgs e)
        {
            string topCode = DgrTopCategory.Dgr.CurrentRow.Cells["catTop"].Value.ToString();
            string midCode = DgrMidCategory.Dgr.CurrentRow.Cells["catMid"].Value.ToString();
            string botCode = "0";
            if (!string.IsNullOrEmpty(topCode) || !string.IsNullOrEmpty(midCode))
            {
                categoryEdit.GetCategoryinfo(topCode, midCode, botCode, 0);
                CallCategoryEditFrom();
            }
            GetBotCategoryInfo();
        }

        private void bntClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bntOk_Click(object sender, EventArgs e)
        {
            if(WorkTye != 2)
            {
                if (DgrMidCategory.Dgr.CurrentRow == null && DgrBotCategory.Dgr.CurrentRow == null)
                {
                    MessageBox.Show("중,소분류를 선택하세요");
                }
                else if (DgrBotCategory.Dgr.CurrentRow == null)
                {
                    MessageBox.Show("소분류를 선택하세요");
                }
                else
                {
                    pdtTop = Convert.ToInt32(DgrTopCategory.Dgr.CurrentRow.Cells[1].Value);
                    pdtMid = Convert.ToInt32(DgrMidCategory.Dgr.CurrentRow.Cells[1].Value);
                    pdtBot = Convert.ToInt32(DgrBotCategory.Dgr.CurrentRow.Cells[1].Value);
                    //productInfo.GetCategory(pdttop, pdtmid, pdtbot);
                    CategorySelected?.Invoke(pdtTop, pdtMid, pdtBot);
                    Close();
                }
            }
            else
            {
                //productInfo.GetCategory(pdttop, pdtmid, pdtbot);
                CategorySelected?.Invoke(pdtTop, pdtMid, pdtBot);
                Close();
            }
        }
    }
}
