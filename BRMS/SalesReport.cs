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
    public partial class SalesReport : Form
    {
        cDatabaseConnect dbconn = new cDatabaseConnect();
        cDataGridDefaultSet dgrReport = new cDataGridDefaultSet();
        int pdtCatTop = 0;
        int pdtCatMid = 0;
        int pdtCatBot = 0;
        bool categoryToggle = false;
        string[] columnNames = { };
        string[] columnTexts = { };
        int accessedEmp = 0;
        public SalesReport()
        {
            InitializeComponent();
            cmBoxSearchTypeSet();
            lblCategory.Text = "전체";
            panelDatagrid.Controls.Add(dgrReport.Dgr);
            dgrReport.Dgr.Dock = DockStyle.Fill;
            cmBoxSearchType.SelectionChangeCommitted += cmBoxSearchType_SelectionChangeCommitted;
            cmBoxSearchType_SelectionChangeCommitted(cmBoxSearchType, EventArgs.Empty);
            dgrReport.Dgr.ColumnHeaderMouseClick += dgrReport_ColumnHeaderMouseClick;
            chkDayli.CheckedChanged += chkDayli_Checkd;
            GridForm(columnNames, columnTexts);
        }

        /// <summary>
        /// 조회 항목 콤보박스 설정
        /// </summary>
        private void cmBoxSearchTypeSet()
        {
            cmBoxSearchType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmBoxSearchType.Items.Add("단품별");
            cmBoxSearchType.Items.Add("분류별");
            cmBoxSearchType.Items.Add("공급사별");
            cmBoxSearchType.Items.Add("일자별");
            cmBoxSearchType.SelectedIndex = 1;
        }
        /// <summary>
        /// 일자별 체크박스 체크시 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkDayli_Checkd(object sender, EventArgs e)
        {
            cmBoxSearchType_SelectionChangeCommitted(cmBoxSearchType, EventArgs.Empty);
        }
        /// <summary>
        /// 분류 버튼 클릭 후 받아온 분류 정보로 라벨에 표시
        /// </summary>
        /// <param name="top"></param>
        /// <param name="mid"></param>
        /// <param name="bot"></param>
        private void GetCategoryInfo(int top, int mid, int bot)
        {
            DataTable dataTable = new DataTable();
            DataRow dataRow;
            string query;
            string categoryText;

            query = $"SELECT cat_name_kr,cat_name_en FROM category WHERE cat_top = {top} AND cat_mid = 0 AND  cat_bot = 0";
            dataTable.Reset();
            dbconn.SqlReaderQuery(query, dataTable);
            dataRow = dataTable.Rows[0];
            categoryText = string.Format("{0}({1})", dataRow["cat_name_kr"].ToString(), dataRow["cat_name_en"].ToString());
            if (mid != 0)
            {
                query = $"SELECT cat_name_kr,cat_name_en FROM category WHERE cat_top = {top} AND cat_mid = {mid} AND  cat_bot = 0";
                dataTable.Reset();
                dbconn.SqlReaderQuery(query, dataTable);
                dataRow = dataTable.Rows[0];
                categoryText = categoryText + string.Format(" ▶ {0}({1})", dataRow["cat_name_kr"].ToString(), dataRow["cat_name_en"].ToString());
            }

            if (mid != 0 && bot != 0)
            {
                query = $"SELECT cat_name_kr,cat_name_en FROM category WHERE cat_top = {top} AND cat_mid = {mid} AND  cat_bot = {bot}";
                dataTable.Reset();
                dbconn.SqlReaderQuery(query, dataTable);
                dataRow = dataTable.Rows[0];
                categoryText = categoryText + string.Format(" ▶ {0}({1})", dataRow["cat_name_kr"].ToString(), dataRow["cat_name_en"].ToString());
            }
            lblCategory.Text = categoryText;
            pdtCatTop = top;
            pdtCatMid = mid;
            pdtCatBot = bot;
            categoryToggle = true;
        }
        /// <summary>
        /// 조회 항목 콤보박스 수정시 
        /// </summary>
        /// <param name="sener"></param>
        /// <param name="e"></param>
        private void cmBoxSearchType_SelectionChangeCommitted(object sener, EventArgs e)
        {
            int searchType = cmBoxSearchType.SelectedIndex;
            
            switch (searchType)
            {           
                case 0:
                    columnNames = new string[]{ "pdtCode", "pdtNameKr", "pdtNumber", "revenue", "cost", "profit", "margin", "ratio", "sale", "return", "dc", "tax" };
                    columnTexts = new string[] {"제품코드", "제품명", "제품번호", "매출액", "예정원가", "예정이익", "이익율", "구성비", "판매", "반품", "할인", "부가세" };
                    
                    break;
                case 1:
                    columnNames = new string[] { "catTop","catMid","catBot","categoryName", "revenue", "cost", "profit", "margin", "ratio", "sale", "return", "dc", "tax" };
                    columnTexts = new string[] { "대", "중", "소", "분류명", "매출액", "예정원가", "예정이익", "이익율", "구성비", "판매", "반품", "할인", "부가세" };
                    break;
                case 2:
                    columnNames = new string[] { "supplierCode", "supplier", "revenue", "cost", "profit", "margin", "ratio", "sale", "return", "dc", "tax" };
                    columnTexts = new string[] { "공급사코드", "공급사명", "매출액", "예정원가", "예정이익", "이익율", "구성비", "판매", "반품", "할인", "부가세" };
                    break;
                case 3:
                    columnNames = new string[] { "saleDate", "revenue", "cost", "profit", "margin", "ratio", "sale", "return", "dc", "tax" };
                    columnTexts = new string[] { "매출일", "매출액", "예정원가", "예정이익", "이익율", "구성비", "판매", "반품", "할인", "부가세" };
                    chkDayli.Checked = false;
                    chkDayli.Enabled = false;
                    break;


            }
            if(searchType == 3)
            {
                chkDayli.Enabled = false;
            }
            else
            {
                chkDayli.Enabled = true;
            }
            //GridForm(columnNames, columnTexts);
        }
        private void GridForm(string[] name, string[] text)
        {

            dgrReport.Dgr.Columns.Clear();
            for (int i = 0; i < name.Length; i++)
            {
                dgrReport.Dgr.Columns.Add(name[i], text[i]);
            }
            if(chkDayli.Checked && cmBoxSearchType.SelectedIndex !=3)
            {
                dgrReport.Dgr.Columns.Add("saleDate", "매출일");
            }

            dgrReport.FormatAsInteger("revenue", "cost", "profit", "dc", "tax", "fee", "sale", "return");
            dgrReport.FormatAsDecimal("margin", "ratio");
            dgrReport.FormatAsStringLeft("pdtNameKr", "pdtNumber", "categoryName", "supplier");
            dgrReport.FormatAsStringCenter("categoryCode", "supplierCode", "saleDate");
            dgrReport.ApplyDefaultColumnSettings();
            dgrReport.Dgr.ReadOnly = true;
            dgrReport.Dgr.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            switch (cmBoxSearchType.SelectedIndex)
            {
                case 0:
                    dgrReport.Dgr.Columns["pdtCode"].Visible = false;
                    dgrReport.Dgr.Columns["pdtNameKr"].DefaultCellStyle.BackColor = Color.FromArgb(234, 238, 244);
                    dgrReport.Dgr.Columns["pdtNameKr"].DefaultCellStyle.SelectionBackColor = Color.FromArgb(234, 238, 244);
                    dgrReport.Dgr.Columns["pdtNameKr"].DefaultCellStyle.Padding = new Padding(1);
                    break;
                case 1:
                    dgrReport.Dgr.Columns["catTop"].Visible = false;
                    dgrReport.Dgr.Columns["catMid"].Visible = false;
                    dgrReport.Dgr.Columns["catBot"].Visible = false;
                    break;
            }
            if(chkDayli.Checked)
            {
                foreach (DataGridViewColumn column in dgrReport.Dgr.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }

        }
       /// <summary>
       /// 데이터 그리드 마지막 합계 행 정렬 고정
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void dgrReport_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgrReport.ExcludeLastRowSort(e);

        }
        
        /// <summary>
        /// 판매조회 쿼리 설정
        /// </summary>
        private void SearchQuerySetting()
        {
            string fromDate = dtpDateFrom.Value.ToString("yyyy-MM-dd");
            string toDate = dtpDateTo.Value.AddDays(1).ToString("yyyy-MM-dd");

            dgrReport.Dgr.Rows.Clear();
            DataTable saleTable = new DataTable();
            int searchType = cmBoxSearchType.SelectedIndex;

            string query;
            string selectQuery = "SELECT ";
            string whereQuery = $" WHERE ";
            string groupby = "GROUP BY ";
            string fromQuery = "FROM " ;
            string selectCommonnss = "sale_type saletype ,sum(saled_amount_krw) revenue ,sum(saled_bprice) bprice, sum(saled_dc) discount," +
                $" SUM(CASE WHEN pdt_tax <> 0 THEN saled_amount_krw ELSE 0 END) tax,SUM(CASE WHEN pdt_tax = 0 THEN saled_amount_krw ELSE 0 END) taxfee ";
            string fromCommonnss =  "saledetail, product ";
            string whereCommonnss = "sale_code = saled_code AND saled_pdt = pdt_code ";
            string orderByQuery = "ORDER BY ";
            if (chkDayli.Checked)
            {
                selectQuery += "sale_dt, ";
                fromQuery += $" (SELECT CONVERT(char(8),sale_date,112) as sale_dt,sale_code,sale_type FROM sales WHERE sale_date > '{fromDate}' AND sale_date < '{toDate}' ) as sales_tmp, ";
                groupby += "sale_dt, ";
                orderByQuery += "sale_dt, ";
            }
            else
            {
                fromQuery += "sales, ";
                whereQuery += $"sale_date > '{fromDate}' AND sale_date < '{toDate}' AND ";
                
            }
            switch (searchType)
            {
                case 0:
                    selectQuery += "pdt_code, sum(saled_qty) as qty, ";
                    groupby += "pdt_code, ";
                    orderByQuery += "pdt_code ";
                    break;
                case 1:
                    if (pdtCatTop == 0)
                    {
                        selectQuery += "pdt_top cattop, 0 catmid, 0 catbot, ";
                        groupby += "pdt_top, ";
                        orderByQuery += "pdt_top ";

                    }
                    else if (pdtCatTop != 0 && pdtCatMid == 0)
                    {
                        selectQuery += "pdt_top cattop, pdt_mid catmid , 0 catbot, ";
                        groupby += "pdt_top, pdt_mid, ";
                        orderByQuery += "pdt_top, pdt_mid ";
                    }
                    else if (pdtCatTop != 0 && pdtCatMid != 0 && pdtCatBot == 0)
                    {
                        selectQuery += "pdt_top cattop, pdt_mid catmid , pdt_mid catbot, ";
                        groupby += "pdt_top ,pdt_mid ,pdt_bot, ";
                        orderByQuery += "pdt_top, pdt_mid, pdt_bot ";
                    }
                    
                    break;
                case 2:
                    selectQuery += "pdt_sup, ";
                    groupby += "pdt_sup ";
                    orderByQuery += "pdt_sup";
                    break;
                case 3:
                    selectQuery += "sale_dt, ";
                    fromQuery = $"FROM (SELECT CONVERT(char(8),sale_date,112) as sale_dt,sale_code,sale_type FROM sales WHERE sale_date > '{fromDate}' AND sale_date < '{toDate}' ) as sales_tmp, ";
                    whereQuery = "WHERE ";
                    groupby += "sale_dt, ";
                    orderByQuery = "ORDER BY sale_dt";
                    break;
            }
            if (categoryToggle == true /*&& searchType != 1*/)
            {
                if (pdtCatTop != 0 && pdtCatMid == 0)
                {
                    whereCommonnss += $"AND pdt_top = {pdtCatTop} ";
                    groupby += " pdt_top, ";
                }
                else if (pdtCatTop != 0 && pdtCatMid != 0 && pdtCatBot == 0)
                {
                    whereCommonnss += $"AND pdt_top = {pdtCatTop} AND pdt_mid = {pdtCatMid} ";
                    groupby += " pdt_top, pdt_mid, ";
                }
                else
                {
                    whereCommonnss += $"AND pdt_top = {pdtCatTop} AND pdt_mid = {pdtCatMid} AND pdt_bot = {pdtCatBot} ";
                    groupby += " pdt_top ,pdt_mid ,pdt_bot, ";
                }
            }
            groupby += "sale_type ";
            query = selectQuery + selectCommonnss + fromQuery + fromCommonnss + whereQuery + whereCommonnss + groupby + orderByQuery;
            GridForm(columnNames, columnTexts);
            dbconn.SqlDataAdapterQuery(query, saleTable);
            //Aggregate(saleTable);
            FillGrid(saleTable);
            cLog.InsertEmpAccessLogNotConnect("@saleReportSearch", accessedEmp, 0);
        }
        private void FillGrid(DataTable dataTable)
        {
            int totalRevenue = 0;           //전체 매출액
            decimal totalBprice = 0;        // 전체 예정원가
            decimal totalProfit = 0;        // 전체 예정이익
            int totalDC = 0;                // 전체 할인액
            int totalTax = 0;               // 전체 과세액
            int totalSale = 0;              // 전체 판매액
            int totalReturn = 0;            // 전체 반품액
            decimal totalMargin = 0;        // 전체 마진율
            int searchType = cmBoxSearchType.SelectedIndex;
            bool dayliChecked = chkDayli.Checked;
           
            // 합산 데이터를 저장할 Dictionary 생성
            var dayliSum = new Dictionary<string, (decimal dayliRevenue, decimal dayliBprice, decimal dayliProfit, decimal dayliMargin, decimal dayRatio, int dayliDc, int dayliSale, int dayliReturn, int dayliTax)>();
            
            DataTable dayliTable = new DataTable();
            
            object resultObj = new object();
            string query = "";
            //전체 합계 계산
            foreach(DataRow row in dataTable.Rows)
            {
                int revenue = Convert.ToInt32(row["revenue"]);
                decimal bprice = Convert.ToDecimal(row["bprice"]);
                
                int sale = row["saletype"].ToString() == "1" ? Convert.ToInt32(revenue) : 0;
                int returns = row["saletype"].ToString() == "0" ? Convert.ToInt32(revenue) : 0;
                int dc = Convert.ToInt32(row["discount"]);
                int tax = Convert.ToInt32(row["tax"]);
                totalRevenue += revenue;
                totalBprice += bprice;
                totalSale += sale;
                totalReturn += returns;
                totalDC += dc;
                totalTax += tax;
            }
            totalProfit = Convert.ToDecimal(totalRevenue) - totalBprice;
            totalMargin = totalProfit == 0 ? 0 : totalProfit / Convert.ToDecimal(totalRevenue) * 100;
            
            //일자별 합산 
           
            if (chkDayli.Checked)
            {
                // 데이터 그룹화 및 합산
                foreach (var saleGroup in dataTable.AsEnumerable().GroupBy(row => row.Field<string>("sale_dt")))
                {
                    string saleDate = saleGroup.Key;
                    decimal sumRevenue = saleGroup.Sum(r => Convert.ToDecimal(r["revenue"]));
                    decimal sumCost = saleGroup.Sum(r => Convert.ToDecimal(r["bprice"]));
                    decimal sumDiscount = saleGroup.Sum(r => Convert.ToDecimal(r["discount"]));
                    decimal sumTax = saleGroup.Sum(r => Convert.ToDecimal(r["tax"]));

                    // sale과 return의 조건별 합산
                    int sumSale = saleGroup.Where(r => Convert.ToInt32(r["saletype"]) == 1).Sum(r => Convert.ToInt32(r["revenue"]));
                    int sumReturn = saleGroup.Where(r => Convert.ToInt32(r["saletype"]) == 0).Sum(r => Convert.ToInt32(r["revenue"]));

                    // profit, margin, ratio 계산
                    decimal profit = sumRevenue - sumCost;
                    decimal margin = (sumRevenue != 0) ? (profit / sumRevenue) * 100 : 0;
                    decimal ratio = (totalRevenue != 0) ? (sumRevenue / totalRevenue) * 100 : 0;

                    // Dictionary에 추가
                    dayliSum[saleDate] = (dayliRevenue: sumRevenue, dayliBprice: sumCost, dayliProfit: profit, dayliMargin: margin, dayRatio: ratio,
                                          dayliDc: (int)sumDiscount, dayliSale: sumSale, dayliReturn: sumReturn, dayliTax: (int)sumTax);
                }
               
            }
            // 중복여부 확인 후 데이터 그리드에 기록
            foreach (DataRow row in dataTable.Rows)
            {
                int revenue = Convert.ToInt32(row["revenue"]);
                decimal bprice = Convert.ToDecimal(row["bprice"]);
                decimal profit = revenue - bprice;
                decimal margin = revenue != 0 ? Math.Round(profit / revenue * 100, 2) : 0;
                int sale = row["saletype"].ToString() == "1" ? Convert.ToInt32(revenue) : 0;
                int returns = row["saletype"].ToString() == "0" ? Convert.ToInt32(revenue) : 0;
                int dc = Convert.ToInt32(row["discount"]);
                int tax = Convert.ToInt32(row["tax"]);
                int rowIndex = 0;
                string saleDate ;// row["sale_dt"].ToString();
                bool duplicate = false;
                // searchType에 따라 중복을 확인할 필드 결정
                switch (searchType)
                {
                    case 0: // pdtCode로 중복 확인
                        foreach (DataGridViewRow searchRow in dgrReport.Dgr.Rows)
                        {
                            int dataGridPdtCode = Convert.ToInt32(searchRow.Cells["pdtCode"].Value);
                            int rowPdtCode = Convert.ToInt32(row["pdt_code"]);
                            bool isDuplicate = dataGridPdtCode == rowPdtCode;

                            // chkDayli가 체크된 경우 saleDate 조건 추가
                            if (chkDayli.Checked)
                            {
                                isDuplicate &= searchRow.Cells["saleDate"].Value != null && searchRow.Cells["saleDate"].Value == row["sale_dt"];
                            }
                            if (isDuplicate)
                            {
                                rowIndex = searchRow.Index;
                                duplicate = true;
                                break;
                            }
                        }
                        break;

                    case 1: // catTop, catMid, catBot으로 중복 확인
                        foreach (DataGridViewRow searchRow in dgrReport.Dgr.Rows)
                        {
                            int gridCatTop = Convert.ToInt32(searchRow.Cells["catTop"].Value);
                            int gridCatMid = Convert.ToInt32(searchRow.Cells["catMid"].Value);
                            int gridCatBot = Convert.ToInt32(searchRow.Cells["catBot"].Value);

                            int rowCatTop = Convert.ToInt32(row["cattop"]);
                            int rowCatMid = Convert.ToInt32(row["catmid"]);
                            int rowCatBot = Convert.ToInt32(row["catbot"]);

                            bool isDuplicate = (gridCatTop == rowCatTop && gridCatMid == rowCatMid && gridCatBot == rowCatBot);

                            // chkDayli가 체크된 경우 saleDate 조건 추가
                            if (chkDayli.Checked)
                            {
                                isDuplicate &= searchRow.Cells["saleDate"].Value != null && searchRow.Cells["saleDate"].Value.ToString() == row["sale_dt"].ToString();
                            }
                            if (isDuplicate)
                            {
                                rowIndex = searchRow.Index;
                                duplicate = true;
                                break;
                            }
                        }
                        break;

                    case 2: // supCode로 중복 확인
                        foreach (DataGridViewRow searchRow in dgrReport.Dgr.Rows)
                        {
                            bool isDuplicate = searchRow.Cells["supplierCode"].Value != null && searchRow.Cells["supplierCode"].Value == row["pdt_sup"];

                            // chkDayli가 체크된 경우 saleDate 조건 추가
                            if (chkDayli.Checked)
                            {
                                isDuplicate &= searchRow.Cells["saleDate"].Value != null && searchRow.Cells["saleDate"].Value == row["sale_dt"];
                            }
                            if (isDuplicate)
                            {
                                rowIndex = searchRow.Index;
                                duplicate = true;
                                break;
                            }
                        }
                        break;

                    case 3: // saleDate로 중복 확인
                        foreach (DataGridViewRow searchRow in dgrReport.Dgr.Rows)
                        {
                            string dataGridSaleDate = searchRow.Cells["saleDate"].Value.ToString();
                            string rowSaleDate = row["sale_dt"].ToString();
                            bool isDuplicate =  dataGridSaleDate == rowSaleDate;

                            if (isDuplicate)
                            {
                                rowIndex = searchRow.Index;
                                duplicate = true;
                                break;
                            }
                        }
                        break;
                }
                if (duplicate == false)
                {
                    rowIndex = dgrReport.Dgr.RowCount;
                    dgrReport.Dgr.Rows.Add();
                    //일자별 체크시 현재의 날짜와 마지막 데이터 그리드의 매출일자가 다를 경우 일자별 합계를 데이터 그리드에 추가 
                    if(chkDayli.Checked)
                    {
                        saleDate = row["sale_dt"].ToString();
                        
                        if (rowIndex != 0 && saleDate != dgrReport.Dgr.Rows[rowIndex - 1].Cells["saleDate"].Value.ToString())
                        {
                            
                            AddDaliSaleDate(rowIndex, dayliSum);
                            rowIndex = dgrReport.Dgr.RowCount;
                            dgrReport.Dgr.Rows.Add();
                            
                        }
                    }
                    
                    switch (searchType)
                    {
                        case 0:
                            DataTable readData = new DataTable();
                            query = $"SELECT pdt_name_kr, pdt_number FROM product WHERE pdt_code = {row["pdt_code"]}";

                            dbconn.SqlReaderQuery(query, readData);
                            DataRow readrow = readData.Rows[0];
                            dgrReport.Dgr.Rows[rowIndex].Cells["pdtCode"].Value = row["pdt_code"];
                            dgrReport.Dgr.Rows[rowIndex].Cells["pdtNameKr"].Value = readrow[0].ToString().Trim();
                            dgrReport.Dgr.Rows[rowIndex].Cells["pdtNumber"].Value = readrow[1].ToString().Trim();
                            break;

                        case 1:
                            query = $"SELECT cat_name_kr FROM category WHERE cat_top ={row["cattop"]} AND cat_mid = {row["catmid"]} AND cat_bot = {row["catbot"]}";
                            dbconn.sqlScalaQuery(query, out resultObj);
                            dgrReport.Dgr.Rows[rowIndex].Cells["catTop"].Value = row["cattop"];
                            dgrReport.Dgr.Rows[rowIndex].Cells["catMid"].Value = row["catmid"];
                            dgrReport.Dgr.Rows[rowIndex].Cells["catbot"].Value = row["catbot"];
                            dgrReport.Dgr.Rows[rowIndex].Cells["categoryName"].Value = resultObj.ToString();
                            break;

                        case 2:
                            query = $"SELECT sup_name FROM supplier WHERE sup_code = {row["supCode"]}";
                            dbconn.sqlScalaQuery(query, out resultObj);
                            dgrReport.Dgr.Rows[rowIndex].Cells["supplierCode"].Value = row["supCode"];
                            dgrReport.Dgr.Rows[rowIndex].Cells["supplier"].Value = resultObj.ToString();
                            break;

                        case 3:
                            dgrReport.Dgr.Rows[rowIndex].Cells["saleDate"].Value = row["sale_dt"].ToString();
                            break;
                    }
                    dgrReport.Dgr.Rows[rowIndex].Cells["revenue"].Value = revenue == 0 ? 0 : revenue;
                    dgrReport.Dgr.Rows[rowIndex].Cells["cost"].Value = bprice == 0 ? 0 : bprice;
                    dgrReport.Dgr.Rows[rowIndex].Cells["profit"].Value = profit == 0 ? 0 : profit;
                    dgrReport.Dgr.Rows[rowIndex].Cells["margin"].Value = margin == 0 ? 0 : margin;
                    dgrReport.Dgr.Rows[rowIndex].Cells["ratio"].Value = revenue ==0 ? 0 : Convert.ToDecimal(revenue) / Convert.ToDecimal(totalRevenue) * 100;
                    dgrReport.Dgr.Rows[rowIndex].Cells["sale"].Value = sale == 0 ? 0 : sale;
                    dgrReport.Dgr.Rows[rowIndex].Cells["return"].Value = returns == 0 ? 0 : returns;
                    dgrReport.Dgr.Rows[rowIndex].Cells["dc"].Value = dc == 0 ? 0 : dc;
                    dgrReport.Dgr.Rows[rowIndex].Cells["tax"].Value = tax == 0 ? 0 : tax;
                    if(chkDayli.Checked && searchType != 3)
                    {
                       
                        dgrReport.Dgr.Rows[rowIndex].Cells["saleDate"].Value = row["sale_dt"];
                    }

                }
                else
                {
                    // 이미 있는 경우 특정 컬럼 값을 합산합니다.
                    var existingRow = dgrReport.Dgr.Rows[rowIndex];

                    existingRow.Cells["revenue"].Value = Convert.ToInt32(existingRow.Cells["revenue"].Value) + revenue;
                    existingRow.Cells["cost"].Value = Convert.ToDecimal(existingRow.Cells["cost"].Value) + bprice;
                    existingRow.Cells["dc"].Value = Convert.ToInt32(existingRow.Cells["dc"].Value) + dc;
                    existingRow.Cells["tax"].Value = Convert.ToInt32(existingRow.Cells["tax"].Value) + tax;
                    existingRow.Cells["sale"].Value = Convert.ToInt32(existingRow.Cells["sale"].Value) + sale;
                    existingRow.Cells["return"].Value = Convert.ToInt32(existingRow.Cells["return"].Value) + returns;

                    // profit과 margin 계산 후 업데이트
                    decimal updatedRevenue = Convert.ToDecimal(existingRow.Cells["revenue"].Value);
                    decimal updatedCost = Convert.ToDecimal(existingRow.Cells["cost"].Value);
                    decimal updatedProfit = updatedRevenue - updatedCost;
                    decimal updatedMargin = updatedRevenue != 0 && updatedRevenue  != 0? Math.Round(updatedProfit / updatedRevenue * 100, 2) : 0;
                    decimal updatedRatio = updatedRevenue == 0 ? 0 : updatedRevenue / totalRevenue * 100;
                    existingRow.Cells["profit"].Value = updatedProfit;
                    existingRow.Cells["margin"].Value = updatedMargin;
                    existingRow.Cells["ratio"].Value = updatedRatio;
                }

            }
            if(dayliChecked == true)
            {
                int dayliRow = dgrReport.Dgr.Rows.Add();
                AddDaliSaleDate(dayliRow, dayliSum);
            }
            int newRow = dgrReport.Dgr.Rows.Add();
            switch(searchType)
            {
                case 0:
                    dgrReport.Dgr.Rows[newRow].Cells["pdtNameKr"].Value = "합계";
                    break;
                case 1:
                    dgrReport.Dgr.Rows[newRow].Cells["categoryName"].Value = "합계";
                    break;
                case 2:
                    dgrReport.Dgr.Rows[newRow].Cells["supplier"].Value = "합계";
                    break;
                case 3:
                    dgrReport.Dgr.Rows[newRow].Cells["saleDate"].Value = "합계";
                    break;
            }
            decimal totalRatio = totalRevenue == 0 ? 0 : 100;
            dgrReport.Dgr.Rows[newRow].Cells["revenue"].Value = totalRevenue;
            dgrReport.Dgr.Rows[newRow].Cells["cost"].Value = totalBprice;
            dgrReport.Dgr.Rows[newRow].Cells["profit"].Value = totalProfit;
            dgrReport.Dgr.Rows[newRow].Cells["margin"].Value = totalMargin;
            dgrReport.Dgr.Rows[newRow].Cells["ratio"].Value = totalRatio;
            dgrReport.Dgr.Rows[newRow].Cells["sale"].Value = totalSale;
            dgrReport.Dgr.Rows[newRow].Cells["return"].Value = totalReturn;
            dgrReport.Dgr.Rows[newRow].Cells["dc"].Value = totalDC;
            dgrReport.Dgr.Rows[newRow].Cells["tax"].Value = totalTax;
            dgrReport.Dgr.Rows[newRow].DefaultCellStyle.BackColor = Color.LightGray;
            dgrReport.Dgr.Rows[newRow].DefaultCellStyle.SelectionBackColor = Color.LightGray;
        }
        private void AddDaliSaleDate(int rowIndex, Dictionary<string, (decimal dayliRevenue, decimal dayliBprice, decimal dayliProfit, decimal dayliMargin, decimal dayRatio, int dayliDc, int dayliSale, int dayliReturn, int dayliTax)> dayliSum)
        {
            string keyValue = dgrReport.Dgr.Rows[rowIndex - 1].Cells["saleDate"].Value.ToString();
            if (dayliSum.TryGetValue(keyValue, out var dayliValue))
            {
                dgrReport.Dgr.Rows[rowIndex].Cells["revenue"].Value = dayliValue.dayliRevenue;
                dgrReport.Dgr.Rows[rowIndex].Cells["cost"].Value = dayliValue.dayliBprice;
                dgrReport.Dgr.Rows[rowIndex].Cells["profit"].Value = dayliValue.dayliProfit;
                dgrReport.Dgr.Rows[rowIndex].Cells["margin"].Value = dayliValue.dayliMargin;
                dgrReport.Dgr.Rows[rowIndex].Cells["ratio"].Value = dayliValue.dayRatio;
                dgrReport.Dgr.Rows[rowIndex].Cells["sale"].Value = dayliValue.dayliSale;
                dgrReport.Dgr.Rows[rowIndex].Cells["return"].Value = dayliValue.dayliReturn;
                dgrReport.Dgr.Rows[rowIndex].Cells["dc"].Value = dayliValue.dayliDc;
                dgrReport.Dgr.Rows[rowIndex].Cells["tax"].Value = dayliValue.dayliTax;
                dgrReport.Dgr.Rows[rowIndex].Cells["saleDate"].Value = keyValue;
            }

            switch (cmBoxSearchType.SelectedIndex)
            {
                case 0:
                    dgrReport.Dgr.Rows[rowIndex].Cells["pdtNameKr"].Value = keyValue;
                    break;
                case 1:
                    dgrReport.Dgr.Rows[rowIndex].Cells["categoryName"].Value = keyValue;
                    break;
                case 2:
                    dgrReport.Dgr.Rows[rowIndex].Cells["supplier"].Value = keyValue;
                    break;
            }
            dgrReport.Dgr.Rows[rowIndex].DefaultCellStyle.BackColor = cUIManager.Color.GreenGray;
            dgrReport.Dgr.Rows[rowIndex].DefaultCellStyle.SelectionBackColor = cUIManager.Color.GreenGray;
        }


        public void RunQuery()
        {
            try
            {
                SearchQuerySetting();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bntCategory_Click(object sender, EventArgs e)
        {
            if (categoryToggle == true)
            {
                categoryToggle = false;
                lblCategory.Text = "전체";
                pdtCatTop = 0;
                pdtCatMid = 0;
                pdtCatBot = 0;
            }
            else
            {
               
                CategoryBoard categoryBoard = new CategoryBoard();
                categoryBoard.CategorySelected += (top, mid, bot) => { GetCategoryInfo(top, mid, bot); };
                categoryBoard.GetWorkType(2);
                categoryBoard.ShowDialog();
                if (pdtCatTop != 0) { categoryToggle = true; }
            }
        }


    }
}
