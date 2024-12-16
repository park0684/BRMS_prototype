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
    public partial class ProductList : Form
    {
        cDatabaseConnect dbconn = new cDatabaseConnect();
        private cDataGridDefaultSet DgrProductList;

        int pdtCatTop = 0;
        int pdtCatMid = 0;
        int pdtCatBot = 0;
        bool categoryToggle = false;
        int accessedEmp = 1;

        public ProductList()
        {
            InitializeComponent();
            DgrProductList = new cDataGridDefaultSet();
            panelDataGrid.Controls.Add(DgrProductList.Dgr);
            DgrProductList.Dgr.Dock = DockStyle.Fill;
            tBoxSearchWord.KeyUp += KeyUpEnter;
            DgrProductList.CellDoubleClick += DgrProductList_CellDoubleClick;
            InitializeDefaultValues();
            GridForm();
            DateTypeSetting();
        }

        private void GridForm()
        {
            DgrProductList.Dgr.Columns.Add("pdtCode", "부품코드");
            DgrProductList.Dgr.Columns.Add("pdtStatusCode", "상태");
            DgrProductList.Dgr.Columns.Add("pdtStatus", "상태");
            DgrProductList.Dgr.Columns.Add("pdtNumber", "제품번호");
            DgrProductList.Dgr.Columns.Add("pdtNamekr", "제품명(한글)");
            DgrProductList.Dgr.Columns.Add("pdtNameUs", "제품명(영문)");
            DgrProductList.Dgr.Columns.Add("pdtBprice", "매입단가");
            DgrProductList.Dgr.Columns.Add("pdtPriceKrw", "판매단가(￦)");
            DgrProductList.Dgr.Columns.Add("pdtPriceUsd", "판매단가($)");
            DgrProductList.Dgr.Columns.Add("pdtTop", "대분류코드");
            DgrProductList.Dgr.Columns.Add("pdtTopName", "대분류");
            DgrProductList.Dgr.Columns.Add("pdtMid", "중분류코드");
            DgrProductList.Dgr.Columns.Add("pdtMidName", "중분류");
            DgrProductList.Dgr.Columns.Add("pdtBot", "소분류코드");
            DgrProductList.Dgr.Columns.Add("pdtBotName", "소분류");
            DgrProductList.Dgr.Columns.Add("pdtIdate", "등록일");
            DgrProductList.Dgr.Columns.Add("pdtUdate", "수정일");
            DgrProductList.Dgr.ReadOnly = true;
            DgrProductList.Dgr.Columns["pdtCode"].Visible = false;
            DgrProductList.Dgr.Columns["pdtTop"].Visible = false;
            DgrProductList.Dgr.Columns["pdtMid"].Visible = false;
            DgrProductList.Dgr.Columns["pdtBot"].Visible = false;
            DgrProductList.Dgr.Columns["pdtStatusCode"].Visible = false;
            //DgrProductList.Dgr.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            DgrProductList.ApplyDefaultColumnSettings();

            //포멧설정
            DgrProductList.FormatAsInteger("pdtPriceKrw");
            DgrProductList.FormatAsDecimal("pdtBprice", "pdtPriceUsd");
            DgrProductList.FormatAsStringLeft("pdtNumber", "pdtNamekr", "pdtNameUs");
            DgrProductList.FormatAsStringCenter("pdtStatus", "pdtTopName", "pdtMidName", "pdtBotName");
            DgrProductList.FormatAsDecimal("pdtIdate", "pdtUdate");

        }

        private void InitializeDefaultValues()
        {
            lblCategory.Text = "전체";
            //cBoxDateType1.SelectedIndex = 0;
            //cBoxDateType2.SelectedIndex = 0;
            cBoxStatus.SelectedItem = "판매가능";
            dtpType1From.Enabled = false;
            dtpType1To.Enabled = false;
            dtpType2From.Enabled = false;
            dtpType2To.Enabled = false;
            dtpType1From.Format = DateTimePickerFormat.Short;
            dtpType1To.Format = DateTimePickerFormat.Short;
            dtpType2From.Format = DateTimePickerFormat.Short;
            dtpType2To.Format = DateTimePickerFormat.Short;
        }

        private void DateTypeSetting()
        {
            List<string> dateType1List = new List<string> {"등록/수정 선택","등록일", "수정일" };
            cBoxDateType1.DataSource = dateType1List;
            cBoxDateType1.DropDownStyle = ComboBoxStyle.DropDownList;
            List<string> dataType2List = new List<string> 
            { "판매/매입 선택",
                "판매 O", "매입 O", 
                "판매&매입 O", 
                "판매 X", 
                "매입 X,",
                "판매&매입 X", 
                "판매 O, 매입 X",
                "판매 X, 매입 O" 
            };
            cBoxDateType2.DataSource = dataType2List;
            cBoxDateType2.DropDownStyle = ComboBoxStyle.DropDownList;
            List<string> statusList = new List<string> { "전체", "판매가능", "품절", "단종", "취급 외" };
            cBoxStatus.DataSource = statusList;
            cBoxStatus.SelectedIndex = 1;
            cBoxStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void DatePickerEanble(ComboBox comboBox, DateTimePicker fromDate, DateTimePicker toDate)
        {
            if(comboBox.SelectedIndex != 0)
            {
                fromDate.Enabled = true;
                toDate.Enabled = true;
            }
            else
            {
                fromDate.Enabled = false;
                toDate.Enabled = false;
            }
        }

        private void cBoxDateType1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DatePickerEanble(cBoxDateType1, dtpType1From, dtpType1To);
        }

        private void cBoxDateType2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DatePickerEanble(cBoxDateType2, dtpType2From, dtpType2To);
        }


        private void girdFill(DataTable dataTable)
        {
            DgrProductList.Dgr.Rows.Clear();
            int rowIndex = 0;
            
            foreach (DataRow dataRow in dataTable.Rows)
            {
                
                //DataRow StatusRow = cBoxStatus.SelectedValue.ToString .[int.Parse(dataRow["pdt_status"].ToString())];
                DgrProductList.Dgr.Rows.Add();
                DgrProductList.Dgr.Rows[rowIndex].Cells["No"].Value = DgrProductList.Dgr.RowCount;
                DgrProductList.Dgr.Rows[rowIndex].Cells["pdtcode"].Value = dataRow["pdt_code"].ToString();
                DgrProductList.Dgr.Rows[rowIndex].Cells["pdtStatusCode"].Value = dataRow["pdt_status"].ToString();
                DgrProductList.Dgr.Rows[rowIndex].Cells["pdtStatus"].Value = cBoxStatus.Items[int.Parse(dataRow["pdt_status"].ToString())].ToString(); ;//StatusRow[1].ToString();
                DgrProductList.Dgr.Rows[rowIndex].Cells["pdtNumber"].Value = dataRow[2].ToString();
                DgrProductList.Dgr.Rows[rowIndex].Cells["pdtNamekr"].Value = dataRow[3].ToString();
                DgrProductList.Dgr.Rows[rowIndex].Cells["pdtNameUs"].Value = dataRow[4].ToString();
                DgrProductList.Dgr.Rows[rowIndex].Cells["pdtBprice"].Value = Convert.ToDecimal(dataRow["pdt_bprice"]);
                DgrProductList.Dgr.Rows[rowIndex].Cells["pdtPriceKrw"].Value = Convert.ToDecimal(dataRow["pdt_sprice_krw"]);
                DgrProductList.Dgr.Rows[rowIndex].Cells["pdtPriceUsd"].Value = Convert.ToDecimal(dataRow["pdt_sprice_usd"]);
                DgrProductList.Dgr.Rows[rowIndex].Cells["pdtTopName"].Value = dataRow[11].ToString();
                DgrProductList.Dgr.Rows[rowIndex].Cells["pdtMidName"].Value = dataRow[12].ToString();
                DgrProductList.Dgr.Rows[rowIndex].Cells["pdtBotName"].Value = dataRow[13].ToString();
                DgrProductList.Dgr.Rows[rowIndex].Cells["pdtIdate"].Value = Convert.ToDateTime(dataRow["pdt_idate"].ToString()).ToString("yyyy-MM-dd HH:mm"); ;
                DgrProductList.Dgr.Rows[rowIndex].Cells["pdtUdate"].Value = Convert.ToDateTime(dataRow["pdt_udate"].ToString()).ToString("yyyy-MM-dd HH:mm");

                rowIndex++;

            }
        }

        private void bntCategory_Click(object sender, EventArgs e)
        {
            if(categoryToggle == true)
            {
                categoryToggle = false;
                lblCategory.Text = "전체";
                pdtCatTop = 0;
                pdtCatMid = 0;
                pdtCatBot = 0;
            }
            else
            {
                //CategoryTreeView categoryTreeView = new CategoryTreeView();
                //categoryTreeView.CategorySelected += (top, mid, bot) => { GetCategoryInfo(top, mid, bot); };
                //categoryTreeView.ShowDialog();
                CategoryBoard categoryBoard = new CategoryBoard();
                categoryBoard.CategorySelected += (top, mid, bot) => { GetCategoryInfo(top, mid, bot); };
                categoryBoard.GetWorkType(2);
                categoryBoard.ShowDialog();
            }

        }

        private void GetCategoryInfo(int top, int mid, int bot)
        {
            DataTable dataTable = new DataTable();
            DataRow dataRow;
            string query;
            string categoryText;

            query = string.Format("SELECT cat_name_kr,cat_name_en FROM category WHERE cat_top = {0} AND cat_mid = 0 AND  cat_bot = 0", top);
            dataTable.Reset();
            dbconn.SqlReaderQuery(query, dataTable);
            dataRow = dataTable.Rows[0];
            categoryText = string.Format("{0}({1})", dataRow["cat_name_kr"].ToString(), dataRow["cat_name_en"].ToString());
            if (mid != 0)
            {
                query = string.Format("SELECT cat_name_kr,cat_name_en FROM category WHERE cat_top = {0} AND cat_mid = {1} AND  cat_bot = 0", top, mid);
                dataTable.Reset();
                dbconn.SqlReaderQuery(query, dataTable);
                dataRow = dataTable.Rows[0];
                categoryText = categoryText + string.Format(" ▶ {0}({1})", dataRow["cat_name_kr"].ToString(), dataRow["cat_name_en"].ToString());
            }

            if (mid != 0 && bot != 0)
            {
                query = string.Format("SELECT cat_name_kr,cat_name_en FROM category WHERE cat_top = {0} AND cat_mid = {1} AND  cat_bot = {2}", top, mid, bot);
                dataTable.Reset();
                dbconn.SqlReaderQuery(query, dataTable);
                dataRow = dataTable.Rows[0];
                categoryText = categoryText + string.Format(" ▶ {0}({1})", dataRow["cat_name_kr"].ToString(), dataRow["cat_name_en"].ToString());
            }
            lblCategory.Text = categoryText;
            this.pdtCatTop = top;
            pdtCatMid = mid;
            pdtCatBot = bot;
            categoryToggle = true;
        }

        private void DgrProductList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int currentRowIndex = e.RowIndex;
            if (currentRowIndex >= 0)
            {
                DataGridViewRow currentRow = DgrProductList.Dgr.Rows[currentRowIndex];
                int pdtCode = DgrProductList.ConvertToInt(currentRow.Cells["pdtCode"].Value);

                ProductDetail productDetail = new ProductDetail();
                productDetail.StartPosition = FormStartPosition.CenterParent;
                productDetail.GetProductInfo(pdtCode);
                cLog.InsertEmpAccessLogNotConnect("@pdtSearch", accessedEmp, pdtCode);
                //productDetail.refresh += (refreshCode) => refresh = refreshCode;
                productDetail.ShowDialog();
                //if (refresh == 1)
                //{
                //    DialogResult result = MessageBox.Show("다시 조회 하시겠습니까?", "알림", MessageBoxButtons.YesNo);
                //    if (result == DialogResult.Yes)
                //    {
                //        RunQuery();
                //        refresh = 0;
                //    }
                //}

            }
        }

        private void QuerySetting()
        {
            DataTable reusltdata = new DataTable();

            string query;
            string SELECT= "SELECT pdt_code,pdt_status,RTRIM(pdt_number) as pdt_number, RTRIM(pdt_name_kr), RTRIM(pdt_name_en),pdt_bprice,pdt_sprice_krw,pdt_sprice_usd, pdt_top,pdt_mid,pdt_bot,\n" +
                "(SELECT cat_name_kr FROM category WHERE cat_top = pdt_top AND cat_mid = 0 AND cat_bot = 0) ,\n" +
                "(SELECT cat_name_kr FROM category WHERE cat_top = pdt_top AND cat_mid = pdt_mid AND cat_bot = 0 ) ,\n" +
                "(SELECT cat_name_kr FROM category WHERE cat_top = pdt_top AND cat_mid = pdt_mid AND cat_bot = pdt_bot) ,pdt_idate, pdt_udate FROM product";
            string WHERE = null;

            //if(string.IsNullOrEmpty(tBoxSearchWord.Text) && categoryToggle== false && cBoxDateType1.SelectedIndex == 0 && cBoxDateType2.SelectedIndex==0)
            //{
            //    if(MessageBox.Show("검색 조건 없이 상품 검색시 시간이 소요 될 수 있습니다.\n계속 하시겠습니까?","알림",MessageBoxButtons.YesNo)==DialogResult.Yes)
                
            //}
           
           
           
            if(categoryToggle==true)
            {
                if(!string.IsNullOrEmpty(tBoxSearchWord.Text))
                {
                    WHERE = string.Format(WHERE + " AND pdt_top = {0}",pdtCatTop);
                }
                else
                {
                    WHERE = string.Format(" pdt_top = {0}", pdtCatTop);
                }
                if (pdtCatMid != 0)
                {
                    WHERE = string.Format(WHERE + " AND pdt_mid = {0}", pdtCatMid);
                }
                if(pdtCatBot != 0)
                {
                    WHERE = string.Format(WHERE + " AND pdt_bot = {0}", pdtCatBot);
                }
            }
            if(cBoxDateType1.SelectedIndex != 0)
            {
                string DateType1 = null;
                string dtpFrom = dtpType1From.Text;
                string dtpTo = dtpType1To.Value.AddDays(1).ToString("yyyy-MM-dd");

            
                switch (cBoxDateType1.SelectedIndex)
                {
                    case 1:
                        DateType1 = $"pdt_idate >= '{dtpFrom}' AND pdt_idate <= '{dtpTo}'";
                        break;
                    case 2:
                        DateType1 = $"pdt_udate >= '{dtpFrom}' AND pdt_udate <= '{dtpTo}'";
                        break;
                }
                if (!string.IsNullOrEmpty(tBoxSearchWord.Text) || !string.IsNullOrEmpty(WHERE))
                {
                    WHERE = string.Format(WHERE + " AND " + DateType1);
                }
                else
                {
                    WHERE = DateType1;
                }
            }
            if(cBoxDateType2.SelectedIndex != 0)
            {
                 string dtpFrom = dtpType2From.Value.ToString("yyyy-MM-dd");
                 string dtpTo = dtpType2To.Value.AddDays(1).ToString("yyyy-MM-dd");
                   
                 string DateType2 = null;
                 switch (cBoxDateType2.SelectedIndex)
                 {
                     case 1:
                         
                         DateType2 = $" pdt_code IN (SELECT DISTINCT(saled_pdt) FROM sales,saledetail WHERE sale_date >= '{dtpFrom}' AND sale_date <='{dtpTo}' AND sale_code =  saled_code)";
                         break;
                     case 2:
                         DateType2 = $" pdt_code IN (SELECT DISTINCT(purd_pdt) FROM purchase,purdetail WHERE pur_date >= '{dtpFrom}' AND pur_date <='{dtpTo}' AND pur_code = purd_code)";
                         break;
                     case 3:
                         DateType2 = $" pdt_code IN (SELECT DISTINCT(saled_pdt) FROM sales,saledetail WHERE sale_date >= '{dtpFrom}' AND sale_date <='{dtpTo}' AND sale_code =  saled_code) AND" +
                             $" pdt_code IN (SELECT DISTINCT(purd_pdt) FROM purchase,purdetail WHERE pur_date >= '{dtpFrom}' AND pur_date <='{dtpTo}' AND pur_code = purd_code)";
                         break;
                     case 4:
                         DateType2 = $" pdt_code NOT IN (SELECT DISTINCT(saled_pdt) FROM sales,saledetail WHERE sale_date >= '{dtpFrom}' AND sale_date <='{dtpTo}' AND sale_code =  saled_code)";
                         break;
                     case 5:
                         DateType2 = $" pdt_code NOT IN (SELECT DISTINCT(purd_pdt) FROM purchase,purdetail WHERE pur_date >= '{dtpFrom}' AND pur_date <='{dtpTo}' AND pur_code = purd_code)";
                         break;
                     case 6:
                         DateType2 = $" pdt_code NOT IN (SELECT DISTINCT(saled_pdt) FROM sales,saledetail WHERE sale_date >= '{dtpFrom}' AND sale_date <='{dtpTo}' AND sale_code =  saled_code) AND" +
                             $" pdt_code NOT IN (SELECT DISTINCT(purd_pdt) FROM purchase,purdetail WHERE pur_date >= '{dtpFrom}' AND pur_date <='{dtpTo}' AND pur_code = purd_code)";
                         break;
                     case 7:
                         DateType2 = $" pdt_code IN (SELECT DISTINCT(saled_pdt) FROM sales,saledetail WHERE sale_date >= '{dtpFrom}' AND sale_date <='{dtpTo}' AND sale_code =  saled_code) AND" +
                             $" pdt_code NOT IN (SELECT DISTINCT(purd_pdt) FROM purchase,purdetail WHERE pur_date >= '{dtpFrom}' AND pur_date <='{dtpTo}' AND pur_code = purd_code)";
                         break;
                     case 8:
                         DateType2 = $" pdt_code NOT IN (SELECT DISTINCT(saled_pdt) FROM sales,saledetail WHERE sale_date >= '{dtpFrom}' AND sale_date <='{dtpTo}' AND sale_code =  saled_code) AND" +
                             $" pdt_code IN (SELECT DISTINCT(purd_pdt) FROM purchase,purdetail WHERE pur_date >= '{dtpFrom}' AND pur_date <='{dtpTo}' AND pur_code = purd_code)";
                         break;
                 }
                 if (!string.IsNullOrEmpty(tBoxSearchWord.Text) || !string.IsNullOrEmpty(WHERE))
                 {
                     WHERE = WHERE + " AND " + DateType2;
                 }
                 else
                 {
                     WHERE =  DateType2;
                 }
            }
            if(cBoxStatus.SelectedIndex != 0)
            {
                string Status = string.Format(" pdt_status ={0} ", cBoxStatus.SelectedIndex.ToString());
                if(!string.IsNullOrEmpty(tBoxSearchWord.Text) || !string.IsNullOrEmpty(WHERE))
                {
                    WHERE = WHERE + " AND " + Status;
                }
                else
                {
                    WHERE =  Status;
                }
            }
            if (string.IsNullOrEmpty(tBoxSearchWord.Text))
            {
                query = string.Format("{0} WHERE {1} ORDER BY pdt_number", SELECT, WHERE);
            }
            else
            {
                query = string.Format("{0} WHERE pdt_name_kr LIKE '%{1}%' {2} \nunion \n {0} WHERE pdt_name_en LIKE '%{1}%' {2} \nunion \n {0} WHERE pdt_number LIKE '%{1}%' {2} ORDER BY pdt_number", SELECT, tBoxSearchWord.Text, WHERE);
            }
            
            //MessageBox.Show(query);
            dbconn.SqlDataAdapterQuery(query, reusltdata);
            girdFill(reusltdata);
            cLog.InsertEmpAccessLogNotConnect("@pdtSearch", accessedEmp, 0);
        }

        

        public void RunQuery()
        {
            if(tBoxSearchWord.Text.Contains("'"))
            {
                MessageBox.Show("검색어에 작은 따옴표( ' )가 표함된 경우 조회가 불가능 합니다", "알림");
            }
            else
            {
                try
                {
                    if (string.IsNullOrEmpty(tBoxSearchWord.Text) && categoryToggle == false && cBoxDateType1.SelectedIndex == 0 && cBoxDateType2.SelectedIndex == 0)
                    {
                        if (MessageBox.Show("검색 조건 없이 상품 검색시 시간이 소요 될 수 있습니다.\n계속 하시겠습니까?", "알림", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            QuerySetting();
                        }
                    }
                    else
                    {
                        QuerySetting();
                    }
                }   
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void KeyUpEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                RunQuery();
            }
        }

        private void bntProductAdd_Click(object sender, EventArgs e)
        {
            ProductDetail productDetail = new ProductDetail();
            productDetail.StartPosition = FormStartPosition.CenterParent;
            productDetail.UnregisteredProduct("", "0", "0");
            productDetail.ShowDialog();
        }
    }
}
