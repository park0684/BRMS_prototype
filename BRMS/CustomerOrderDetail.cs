using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BRMS
{
    public partial class CustomerOrderDetail : Form
    {
        cDatabaseConnect dbconn = new cDatabaseConnect();
        cDataGridDefaultSet DgrOrderDetail = new cDataGridDefaultSet();
        DataTable detailDate = new DataTable();
        DataTable orderlDate = new DataTable();
        DateTime orderDate = new DateTime();
        DateTime saleDate = new DateTime();
        List<string> columnsToToggle = new List<string>();
        Dictionary<string, object> originalValues = new Dictionary<string, object>();
        Dictionary<int, string> accessPermission = new Dictionary<int, string>();
        int accessedEmp = 0;

        int orderCode = 0;                  // 주문서 코드
        int customerCode = 0;               // 회원 코드
        int orderStatus = 0;                // 상태코드
        int salesCode = 0;                  // 판매 코드
        decimal orderExchange = 0;          // 주문서 환율-> 주문서에서 개별적으로 수정 가능
        private object previousValue;       // 데이터그리드의 데이터 수정중 오류 발생하면 수정전 정보로 돌아갈 수 있도록 이전 정보 등록 할 수 있는 오브젝트
        bool salesToggle = false;           // 판매 여부 토글
        bool isEventProcessing = false;     // 이벤트 진행여부 토글    
        decimal bprice = 0;                 // 매입원가
        int amountKrw = 0;                  // 전체 금액(한화)
        decimal amountUsd = 0;              // 전체 금액(미화)
        int feeKrw = 0;                     // 수수료(한화)

        public CustomerOrderDetail()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedDialog;
            panelDataGrid.Controls.Add(DgrOrderDetail.Dgr);
            DgrOrderDetail.Dgr.Dock = DockStyle.Fill;
            cmBoxCountry_Setting();
            cmBoxOrderStatus_Setting();
            lblExchange.Click += lblExchange_Click;
            GetExchange();
            GetOrderDate(DateTime.Now);
            GetSaleDate(DateTime.Now);
            lblCustName.Text = "";
            GridFrom();
            GridNumberSetting();
            DgrOrderDetail.Dgr.CellBeginEdit += Dgr_CellBeginEdit;
            DgrOrderDetail.Dgr.CellValueChanged += DataGridCellChangedEvent;
            //tBoxExchange.KeyPress += Exchaned_KeyPress;
            //tBoxExchange.KeyDown += tBoxExchange_FormatOnLeave;
            //DgrOrderDetail.RightClick += mouse_right;
            DgrOrderDetail.Dgr.MouseClick += DgrpayList_MouseRightClick;
        }

        private void mouse_right(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        /// <summary>
        /// 이벤트 핸들러
        /// true일 경우 데이터 그리드 이벤트가 작동되지 않는다
        /// </summary>
        /// <param name="isEnable"></param> 실행 여부
        /// <returns></returns>
        private bool ToggleEventHandler(bool isEnable)
        {
            isEventProcessing = isEnable;
            if (isEnable == true)
            {
                DgrOrderDetail.Dgr.CellValueChanged -= DataGridCellChangedEvent;
            }
            else
            {
                DgrOrderDetail.Dgr.CellValueChanged += DataGridCellChangedEvent;
            }
            return isEventProcessing;
        }
        /// <summary>
        /// 새로운 주문서라면 환율 정보를 DB에서 가지고 온다
        /// </summary>
        /// <returns></returns>
        private decimal GetExchange()
        {
            if (orderCode == 0)
            {
                string query = "SELECT cf_value FROM config WHERE cf_code = 1";
                object resultObj = new object();
                dbconn.sqlScalaQuery(query, out resultObj);
                orderExchange = Convert.ToDecimal(resultObj);
            }
            lblExchange.Text = orderExchange.ToString("#,##0");
            return orderExchange;

        }
        private void GetOrderDate(DateTime date)
        {
            orderDate = date;
            lblOrderDate.Text = orderDate.ToString("yyyy년MM월dd일 HH시mm분");
        }

        private void GetSaleDate(DateTime date)
        {
            saleDate = date;
            lblSaleDate.Text = saleDate.ToString("yyyy년MM월dd일");
            if (salesToggle == false)
            {
                lblSaleDate.Text = "";
            }
        }
        /// <summary>
        /// DB에서 국가 정보를 조회하여 국가 콤보 박스에 표시
        /// </summary>
        private void cmBoxCountry_Setting()
        {
            cmBoxCountry.DropDownStyle = ComboBoxStyle.DropDownList;
            string query = "SELECT ctry_code, ctry_name FROM country";
            DataTable dataTable = new DataTable();
            dbconn.SqlDataAdapterQuery(query, dataTable);
            cmBoxCountry.Items.Add("-국가 선택-");
            foreach (DataRow row in dataTable.Rows)
            {
                string countryName = row["ctry_name"].ToString();
                cmBoxCountry.Items.Add(countryName);
            }
            cmBoxCountry.SelectedIndex = 0;
        }
        /// <summary>
        /// 주문서 상태 콤보박스에 표시
        /// </summary>
        private void cmBoxOrderStatus_Setting()
        {
            cmBoxOrderStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmBoxOrderStatus.Items.AddRange(new string[] { "취소", "주문", "판매" });
            cmBoxOrderStatus.SelectedIndex = 1;  // '주문' 선택
        }
        private void GridFrom()
        {
            //DataGridViewCheckBoxColumn dgrCheckBox = new DataGridViewCheckBoxColumn();
            //dgrCheckBox.Name = "custOrddCheckBox";
            //dgrCheckBox.HeaderText = "수수료포함";
            DgrOrderDetail.Dgr.Columns.Add("custOrddPdtCode", "제품코드");
            DgrOrderDetail.Dgr.Columns.Add("custOrddPdtNameKr", "제품명(한글)");
            DgrOrderDetail.Dgr.Columns.Add("custOrddPdtNameUs", "제품명(영문)");
            DgrOrderDetail.Dgr.Columns.Add("custOrddPdtNumber", "제품번호");
            DgrOrderDetail.Dgr.Columns.Add("custorddPdtStatus", "제품상태");
            DgrOrderDetail.Dgr.Columns.Add("custOrddOrderQty", "주문수량");
            DgrOrderDetail.Dgr.Columns.Add("custOrddPdtBprice", "매입단가");
            DgrOrderDetail.Dgr.Columns.Add("custOrddPdtSprice", "판매공급가");
            //DgrOrderDetail.Dgr.Columns.Add(dgrCheckBox);
            DgrOrderDetail.Dgr.Columns.Add("custOrddOfferPriceKrw", "제안단가(￦)");
            DgrOrderDetail.Dgr.Columns.Add("custOrddOfferPriceUsd", "제안단가(＄)");
            DgrOrderDetail.Dgr.Columns.Add("custOrddOfferQty", "판매수량");
            DgrOrderDetail.Dgr.Columns.Add("custOrddOfferAmounteKrw", "제안액(￦)");
            DgrOrderDetail.Dgr.Columns.Add("custOrddOfferAmounteUsd", "제안액(＄)");
            DgrOrderDetail.Dgr.Columns.Add("custOrddMargin", "이익율");
            DgrOrderDetail.Dgr.Columns.Add("custOrddProfitKrw", "이익액(￦)");
            DgrOrderDetail.Dgr.Columns.Add("custOrddProfitUsd", "이익액(＄)");
            DgrOrderDetail.Dgr.Columns.Add("custOrddSize", "크기(Cm)");
            DgrOrderDetail.Dgr.Columns.Add("cutOrddvolumetricweight", "부피무게");
            DgrOrderDetail.Dgr.Columns.Add("custOrddweight", "무게");
            DgrOrderDetail.Dgr.Columns.Add("custOrddMemo", "비고");
            DgrOrderDetail.Dgr.Columns.Add("custOrddStatus", "상태");
            DgrOrderDetail.Dgr.Columns.Add("custOrddSeq", "순번");
            DgrOrderDetail.ApplyDefaultColumnSettings();
            
            DgrOrderDetail.Dgr.ScrollBars = ScrollBars.Both;

            DgrOrderDetail.FormatAsStringLeft("custOrddPdtNameKr", "custOrddPdtNameUs", "custOrddPdtNumber", "custOrddRemark");
            DgrOrderDetail.FormatAsStringCenter("custOrddMargin", "custOrddSize", "cutOrddvolumetricweight", "custOrddweight");
            DgrOrderDetail.FormatAsInteger("custOrddOfferPriceKrw", "custOrddPdtSprice", "custOrddProfit", "custOrddOrderQty", "custOrddOfferQty", "custOrddOfferAmounteKrw", "custOrddProfitKrw");
            DgrOrderDetail.FormatAsDecimal("custOrddPdtBprice", "custOrddOfferPriceUsd", "custOrddOfferAmounteUsd", "custOrddProfitUsd");

            foreach (DataGridViewColumn column in DgrOrderDetail.Dgr.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            DgrOrderDetail.Dgr.Columns["custOrddStatus"].Visible = false;
            DgrOrderDetail.Dgr.Columns["custOrddPdtCode"].Visible = false;
            DgrOrderDetail.Dgr.Columns["custOrddSeq"].Visible = false;
            DgrOrderDetail.Dgr.Columns["custOrddPdtNameKr"].ReadOnly = true;
            DgrOrderDetail.Dgr.Columns["custOrddPdtNameUs"].ReadOnly = true;
            DgrOrderDetail.Dgr.Columns["custorddPdtStatus"].ReadOnly = true;
            DgrOrderDetail.Dgr.Columns["custOrddPdtBprice"].ReadOnly = true;
            DgrOrderDetail.Dgr.Columns["custOrddPdtSprice"].ReadOnly = true;
            DgrOrderDetail.Dgr.Columns["custOrddProfitKrw"].ReadOnly = true;
            DgrOrderDetail.Dgr.Columns["custOrddProfitUsd"].ReadOnly = true;
            DgrOrderDetail.Dgr.Columns["custOrddSize"].ReadOnly = true;
            DgrOrderDetail.Dgr.Columns["custOrddweight"].ReadOnly = true;


            DgrOrderDetail.ApplyDefaultColumnSettings();
            DgrOrderDetail.Dgr.Rows.Add();
            DgrOrderDetail.Dgr.Rows[0].Cells[0].Value = 1;
           
           
        }
        private void ContexMenuSet(MouseEventArgs e)
        {
            columnsToToggle.Clear();
            List<string> excludColumn = new List<string> { "No", "custOrddPdtCode", "custOrddSeq","custOrddStatus" };
            foreach (DataGridViewColumn columnName in DgrOrderDetail.Dgr.Columns)
            {
                if (!excludColumn.Contains(columnName.Name))
                {
                    columnsToToggle.Add(columnName.Name);
                }
            }
                
            ContextMenuStrip contextMenu = new ContextMenuStrip();               
            ToolStripSeparator separator = new ToolStripSeparator();
                
            contextMenu.Items.Add("펼처보기");                
            contextMenu.Items.Add("줄여보기");
            contextMenu.Items.Add(separator);

            foreach (string columnName in columnsToToggle)
            {
                // 해당 columnName으로 DataGridViewColumn을 찾음
                DataGridViewColumn column = DgrOrderDetail.Dgr.Columns[columnName];

                // ToolStripMenuItem을 만들 때 HeaderText를 메뉴 항목에 표시하고,
                // columnName은 Tag로 저장
                ToolStripMenuItem columnItem = new ToolStripMenuItem(column.HeaderText)
                {
                    CheckOnClick = true,
                    Checked = column.Visible,  // 현재 컬럼의 가시성에 따라 체크 상태 설정
                    Tag = columnName           // columnName을 Tag로 저장
                };
                
                contextMenu.Items.Add(columnItem);  // 메뉴 항목 추가
            }
            contextMenu.Items.Add(separator);
            contextMenu.Items.Add("인쇄");
            contextMenu.Items.Add("엑셀저장");
            contextMenu.Show(DgrOrderDetail.Dgr, e.Location);
            contextMenu.ItemClicked += new ToolStripItemClickedEventHandler(Menu_Click);

        }
        private void DgrpayList_MouseRightClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                ContexMenuSet(e);
                
            }

        }

        private void Menu_Click(object sender, ToolStripItemClickedEventArgs e)
        {
            string clickedItemText = e.ClickedItem.Text;

            switch (clickedItemText)
            {
                case "펼처보기":
                    OnExpand();
                    break;
                case "줄여보기":
                    OnCollapse();
                    break;
                case "엑셀저장":
                    ExportToExcel();
                    break;
                case "인쇄":
                    PrintGrid();
                    break;
                default:
                    // 칼럼 이름으로 메뉴 항목 클릭 시 가시성 토글
                    //var menuItem = (ToolStripMenuItem)e.ClickedItem;
                    string columnName = e.ClickedItem.Tag.ToString();

                    // 칼럼 이름이 데이터 그리드에 존재하는지 확인
                    if (DgrOrderDetail.Dgr.Columns.Contains(columnName))
                    {
                        var column = DgrOrderDetail.Dgr.Columns[columnName];
                        // Grid 표시 상태 반전으로 표시 또는 해제
                        column.Visible = !column.Visible;
                    }
                    break;


            }
        }
        // 펼쳐보기 기능 구현
        private void OnExpand()
        {
            foreach(string columnName in columnsToToggle)
            {
                DgrOrderDetail.Dgr.Columns[columnName].Visible = true;
            }
            
        }

        // 줄여보기 기능 구현
        private void OnCollapse()
        {
            List<string> collapList= new List<string> {
                "custOrddPdtNameUs",
                "custOrddPdtNumber",
                "custorddPdtStatus",
                "custOrddOrderQty",
                "custOrddPdtBprice",
                "custOrddPdtSprice",
                "custOrddOfferPriceKrw",
            };
            foreach(string columnName in columnsToToggle)
            {
                DgrOrderDetail.Dgr.Columns[columnName].Visible = false;
            }
            foreach (string columnName in collapList)
            {
                DgrOrderDetail.Dgr.Columns[columnName].Visible = true;
            }
        }

        // 엑셀로 내보내기 기능 구현
        private void ExportToExcel()
        {
            cUIManager.ShowMessageBox("엑셀저장", "알림", MessageBoxButtons.OK);
        }

        // 인쇄 기능 구현
        private void PrintGrid()
        {
            cUIManager.ShowMessageBox("인쇄", "알림", MessageBoxButtons.OK);
        }

        // 새로운 사용자 정의 기능 구현
        private void NewCustomFunction()
        {
            // 새로운 기능의 로직
        }
        public void OrderSelectedHandle(int custOrderCode)
        {
            LoadCustOrder(custOrderCode);
        }
        private void LoadCustOrder(int custOrderCode)
        {
            orderCode = custOrderCode;
            string query = "SELECT cordd_code, cordd_seq, cordd_pdt,RTRIM(pdt_number) pdt_number, RTRIM(pdt_name_kr) pdt_name_kr, RTRIM(pdt_name_en) pdt_name_en, " +
                "pdt_status, cordd_orderqty, cordd_bprice, cordd_sprice, cordd_offerkrw, cordd_offeruds, cordd_amountkrw, cordd_amountusd, cordd_offerqty, " +
                "cordd_status, cordd_memo, pdt_width, pdt_length, pdt_height, pdt_weight" +
                $" FROM custorderdetail, product WHERE cordd_code = {orderCode} AND cordd_pdt =  pdt_code ORDER BY cordd_code, cordd_seq";
            dbconn.SqlDataAdapterQuery(query, detailDate);
            GridFill(detailDate);
            query = $"select cord_date,cord_status, cord_cust, cord_sdate, cord_exchange, cord_address, cord_country, cord_sales from custorder  WHERE cord_code ={orderCode} ";
            dbconn.SqlReaderQuery(query, orderlDate);
            DataRow row = orderlDate.Rows[0];
            salesCode = Convert.ToInt32(row["cord_sales"]); // 수정 필요 : 정보가 없을 경우 오류 발생 -> 기록시 0을 입력하게 하던가 null의 경우 예외처리 필요 
            orderStatus = Convert.ToInt32(row["cord_status"]);
            orderDate = Convert.ToDateTime(row["cord_date"]);
            if (orderStatus == 2)
            {
                salesToggle = true;
                saleDate = Convert.ToDateTime(row["cord_sdate"]);
            }
            customerCode = Convert.ToInt32(row["cord_cust"]);
            query = "SELECT cust_name FROM customer WHERE cust_code = " + customerCode;
            object resultObj = new object();
            dbconn.sqlScalaQuery(query, out resultObj);
            orderExchange = Convert.ToInt32(row["cord_exchange"]);
            cmBoxCountry.SelectedIndex = Convert.ToInt32(row["cord_country"]);
            tBoxAddress.Text = Convert.ToString(row["cord_address"]);
            GetOrderDate(orderDate);
            GetSaleDate(saleDate);
            GetExchange();
            lblCustName.Text = Convert.ToString(resultObj);
            cmBoxOrderStatus.SelectedIndex = orderStatus;
            bntOrderDate.Enabled = false;
            if(salesCode != 0)
            {
                bntSaleRegist.Text = "판매 조회";
                bntSaleRegist.BackColor = cUIManager.Color.Green;
            }
        }
        private void GridFill(DataTable dataTable)// 오늘은 요기까지
        {
            if(!isEventProcessing)
            {
                ToggleEventHandler(true);
            }
            //DgrOrderDetail.Dgr.CellValueChanged -= DataGridCellChangedEvent;
            int index = 0;
            string size = "";
            string status = "";
            DgrOrderDetail.Dgr.Rows.Clear();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                DgrOrderDetail.Dgr.Rows.Add();
                
                status = cStatusCode.GetProductStatus(Convert.ToInt32(dataRow["pdt_status"]));
                string width = dataRow["pdt_width"]?.ToString().Trim() ?? "0";
                string length = dataRow["pdt_length"]?.ToString().Trim() ?? "0";
                string height = dataRow["pdt_height"]?.ToString().Trim() ?? "0";
                size = $"{width} X {length} X {height}";
                
                //DgrOrderDetail.Dgr.Rows[rowIndex].Cells["No"].Value = dataRow["purd_seq"].ToString();
                DgrOrderDetail.Dgr.Rows[index].Cells["custOrddPdtCode"].Value = dataRow["cordd_pdt"];
                DgrOrderDetail.Dgr.Rows[index].Cells["custOrddPdtNameKr"].Value = dataRow["pdt_name_kr"];
                DgrOrderDetail.Dgr.Rows[index].Cells["custOrddPdtNameUs"].Value = dataRow["pdt_name_en"];
                DgrOrderDetail.Dgr.Rows[index].Cells["custOrddPdtNumber"].Value = dataRow["pdt_number"];
                DgrOrderDetail.Dgr.Rows[index].Cells["custorddPdtStatus"].Value = status;
                DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOrderQty"].Value = dataRow["cordd_orderqty"];
                DgrOrderDetail.Dgr.Rows[index].Cells["custOrddPdtBprice"].Value = dataRow["cordd_bprice"];
                DgrOrderDetail.Dgr.Rows[index].Cells["custOrddPdtSprice"].Value = dataRow["cordd_sprice"];
                DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferPriceKrw"].Value = dataRow["cordd_offerkrw"];
                DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferPriceUsd"].Value = dataRow["cordd_offeruds"];
                DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferAmounteKrw"].Value = dataRow["cordd_amountkrw"];
                DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferAmounteUsd"].Value = dataRow["cordd_amountusd"];
                DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferQty"].Value = dataRow["cordd_offerqty"];
                DgrOrderDetail.Dgr.Rows[index].Cells["custOrddSize"].Value = size;
                //DgrOrderDetail.Dgr.Rows[index].Cells["cutOrddvolumetricweight"].Value = dataRow[""];
                DgrOrderDetail.Dgr.Rows[index].Cells["custOrddweight"].Value = dataRow["pdt_weight"];
                DgrOrderDetail.Dgr.Rows[index].Cells["custOrddMemo"].Value = dataRow["cordd_memo"];
                DgrOrderDetail.Dgr.Rows[index].Cells["custOrddStatus"].Value = dataRow["cordd_status"];
                DgrOrderDetail.Dgr.Rows[index].Cells["custOrddSeq"].Value = dataRow["cordd_seq"];

                OfferMarginSet(index);
                ProfitSet(index);
                index++;
            }
            LastGridAdd();
            GridNumberSetting();
            ToggleEventHandler(false);
            //DgrOrderDetail.Dgr.CellValueChanged += DataGridCellChangedEvent;
            SetTextBox();
        }
        /// <summary>
        /// 제품번호 셀에 데이터 입력 시 그리드에 제품 정보 입력
        /// 미등록 제품 번호의 경우 미등록 상태로 표시
        /// </summary>
        /// <param name="index"></param>
        private void SearchPdtNumberForCode(int index)
        {
            //string result = "";
            if(!isEventProcessing)
            {
                ToggleEventHandler(true);
            }
            //DgrOrderDetail.Dgr.CellValueChanged -= DataGridCellChangedEvent;
            try
            {
                string query = string.Format("SELECT pdt_code FROM product WHERE pdt_number = '{0}' ", DgrOrderDetail.Dgr.CurrentRow.Cells["custOrddPdtNumber"].Value.ToString());
                object resultObj = new object();
                dbconn.sqlScalaQuery(query, out resultObj);

                if (string.IsNullOrEmpty(resultObj?.ToString()))
                {

                    DgrOrderDetail.Dgr.Rows[index].Cells["custOrddPdtCode"].Value = 0;
                    DgrOrderDetail.Dgr.Rows[index].Cells["custOrddPdtNameKr"].Value = DgrOrderDetail.Dgr.CurrentRow.Cells["custOrddPdtNumber"].Value;
                    DgrOrderDetail.Dgr.Rows[index].Cells["custOrddPdtNameUs"].Value = DgrOrderDetail.Dgr.CurrentRow.Cells["custOrddPdtNumber"].Value;
                    DgrOrderDetail.Dgr.Rows[index].Cells["custorddPdtStatus"].Value = "미등록 제품";
                    DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOrderQty"].Value = 1;
                    DgrOrderDetail.Dgr.Rows[index].Cells["custOrddPdtBprice"].Value = 0;
                    DgrOrderDetail.Dgr.Rows[index].Cells["custOrddPdtSprice"].Value = 0;
                    DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferPriceKrw"].Value = 0;
                    DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferPriceKrw"].Value = 0;
                    DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferQty"].Value = 1;
                    DgrOrderDetail.Dgr.Rows[index].Cells["custOrddSize"].Value = "";
                    DgrOrderDetail.Dgr.Rows[index].Cells["cutOrddvolumetricweight"].Value = 0;
                    DgrOrderDetail.Dgr.Rows[index].Cells["custOrddweight"].Value = 0;
                    DgrOrderDetail.Dgr.Rows[index].Cells["custOrddMemo"].Value = "";
                    DgrOrderDetail.Dgr.Rows[index].Cells["custOrddStatus"].Value = 1;
                    DgrOrderDetail.Dgr.Rows[index].Cells["custOrddSeq"].Value = index + 1;
                    AmountKrwSet(index);
                    AmountUsdSet(index);
                    OfferMarginSet(index);
                    ProfitSet(index);
                    LastGridAdd();
                    GridNumberSetting();
                }
                else
                {
                    AddProductToGrid(Convert.ToInt32(resultObj), index);
                    DgrOrderDetail.Dgr.Rows[index].Cells["custOrddPdtNumber"].ReadOnly = true;
                }
                LastGridAdd();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            ToggleEventHandler(false);
            //DgrOrderDetail.Dgr.CellValueChanged += DataGridCellChangedEvent;
        }
        /// <summary>
        /// 조회한 제품 번호 기준으로 새로운 행에 데이터 입력
        /// </summary>
        /// <param name="pdtCode"></param>
        /// <param name="index"></param>
        private void AddProductToGrid(int pdtCode, int index)
        {
            //셀에 제품 정보가 입력되어 이벤트 핸들러가 작동되지 않도록 이벤트 핸들러 제거
            //DgrOrderDetail.Dgr.CellValueChanged -= DataGridCellChangedEvent;
            //ToggleEventHandler(true);

            //마지막행 찾기
            if (index == 0)
            {
                index = DgrOrderDetail.Dgr.RowCount - 1;
            }
            
            //제품정보 조회 후 결과 담기
            string query = string.Format("SELECT pdt_number, pdt_name_kr, pdt_name_en, pdt_bprice, pdt_sprice_krw, pdt_sprice_usd, pdt_status,ISNULL(pdt_width,0) pdt_width, ISNULL(pdt_length,0) pdt_length, ISNULL(pdt_height,0) pdt_height, ISNULL(pdt_weight,0) pdt_weight FROM product WHERE pdt_code = {0}", pdtCode);
            DataTable resultData = new DataTable();
            dbconn.SqlReaderQuery(query, resultData);
            DataRow dataRow = resultData.Rows[0];
            string pdtNumber = dataRow["pdt_number"]?.ToString().Trim() ?? "";
            string pdtNameKr = dataRow["pdt_name_kr"]?.ToString().Trim() ?? "";
            string pdtNameEn = dataRow["pdt_name_en"]?.ToString().Trim() ?? "";
            string status = cStatusCode.GetProductStatus(Convert.ToInt32(dataRow["pdt_status"]));
            string width = dataRow["pdt_width"]?.ToString().Trim() ?? "0";
            string length = dataRow["pdt_length"]?.ToString().Trim() ?? "0";
            string height = dataRow["pdt_height"]?.ToString().Trim() ?? "0";
            string size = $"{width} X {length} X {height}";
            string weigth = dataRow["pdt_weight"]?.ToString().Trim() ?? "0";
            decimal pdtBprice = dataRow["pdt_bprice"] != DBNull.Value ? Convert.ToDecimal(dataRow["pdt_bprice"]): 0;
            int pdtSpriceKrw = dataRow["pdt_sprice_krw"] != DBNull.Value ? Convert.ToInt32(dataRow["pdt_sprice_krw"]) : 0;
            decimal pdtSpriceUsd = dataRow["pdt_bprice"] != DBNull.Value ? Convert.ToDecimal(dataRow["pdt_sprice_usd"]) : 0;


            DgrOrderDetail.Dgr.Rows[index].Cells["custOrddPdtCode"].Value = pdtCode;
            DgrOrderDetail.Dgr.Rows[index].Cells["custOrddPdtNameKr"].Value = pdtNameEn;
            DgrOrderDetail.Dgr.Rows[index].Cells["custOrddPdtNameUs"].Value = pdtNameKr;
            DgrOrderDetail.Dgr.Rows[index].Cells["custOrddPdtNumber"].Value = pdtNumber;
            DgrOrderDetail.Dgr.Rows[index].Cells["custorddPdtStatus"].Value = status;
            DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOrderQty"].Value = 1;
            DgrOrderDetail.Dgr.Rows[index].Cells["custOrddPdtBprice"].Value = pdtBprice;
            DgrOrderDetail.Dgr.Rows[index].Cells["custOrddPdtSprice"].Value = pdtSpriceKrw;
            DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferPriceKrw"].Value = pdtSpriceKrw;
            DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferPriceUsd"].Value = pdtSpriceUsd;
            DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferQty"].Value = 1;
            
            DgrOrderDetail.Dgr.Rows[index].Cells["custOrddSize"].Value = size;
            DgrOrderDetail.Dgr.Rows[index].Cells["cutOrddvolumetricweight"].Value = "";
            DgrOrderDetail.Dgr.Rows[index].Cells["custOrddweight"].Value = weigth;
            DgrOrderDetail.Dgr.Rows[index].Cells["custOrddStatus"].Value = 1;
            DgrOrderDetail.Dgr.Rows[index].Cells["custOrddSeq"].Value = index+1;
            OfferPriceusdSet(index);
            AmountKrwSet(index);
            AmountUsdSet(index);
            OfferMarginSet(index);
            ProfitSet(index);
            LastGridAdd();
            GridNumberSetting();
            SetTextBox();
            //DgrOrderDetail.Dgr.CellValueChanged += DataGridCellChangedEvent;
            //ToggleEventHandler(false);
        }

        /// <summary>
        /// 환율 정보 수정시 리스트내 상품의 환율적용
        /// </summary>
        private void changedExchang()
        {
            int count = DgrOrderDetail.Dgr.RowCount;
            if (count > 1) 
            {
                DgrOrderDetail.Dgr.CellValueChanged -= DataGridCellChangedEvent;
                for (int i = 0; i < count -1; i++)
                {
                    OfferPriceusdSet(i);
                    AmountUsdSet(i);
                    OfferMarginSet(i);
                    ProfitSet(i);
                }
                SetTextBox();
                DgrOrderDetail.Dgr.CellValueChanged += DataGridCellChangedEvent;
                
            }
        }
        /// <summary>
        /// 마지막 그리드 행 추가 
        /// </summary>
        private void LastGridAdd()
        {
            if (DgrOrderDetail.Dgr.Rows.Count > 0)
            {
                DataGridViewRow lastRow = DgrOrderDetail.Dgr.Rows[DgrOrderDetail.Dgr.Rows.Count - 1];
                var cellValue = lastRow.Cells["custOrddPdtNumber"].Value;

                // 마지막 행의 "purdPdtNumber" 컬럼이 비어있는지 확인
                if (cellValue != null && !string.IsNullOrEmpty(cellValue.ToString()))
                {
                    // 비어있지 않다면 새로운 행 추가
                    DgrOrderDetail.Dgr.Rows.Add();
                }
            }
            else
            {
                // 만약 행이 없다면 첫 번째 행을 추가
                DgrOrderDetail.Dgr.Rows.Add();
            }
        }
        /// <summary>
        /// 데이터 그리드앞 번호 설정
        /// 무효처리된 매입상품은 번호에서 제거
        /// </summary>
        private void GridNumberSetting()
        {
            int number = 1;
            for (int index = 0; index < DgrOrderDetail.Dgr.RowCount - 1; index++)
            {
                if (DgrOrderDetail.Dgr.Rows[index].Cells["custOrddStatus"].Value.ToString() == "1")
                {
                    DgrOrderDetail.Dgr.Rows[index].Cells["No"].Value = number;
                    number++;
                }
            }
        }
        /// <summary>
        /// 셀 편집 이벤트 핸들러
        /// 편집된 셀의 행과 칼럼 확인 후 변경 사항 처리를 위한 매소드 실행
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridCellChangedEvent(object sender, DataGridViewCellEventArgs e)
        {

            //DgrOrderDetail.Dgr.CellValueChanged -= DataGridCellChangedEvent;
            if(!isEventProcessing)
            {
                ToggleEventHandler(true);

                try
                {
                    int SelectIndex = e.RowIndex;
                    string columnName = DgrOrderDetail.Dgr.Columns[DgrOrderDetail.Dgr.CurrentCell.ColumnIndex].Name;
                    switch (columnName)
                    {
                        case "custOrddPdtNumber"://제품번호 Cell 입력 또는 수정
                            SearchPdtNumberForCode(e.RowIndex);
                            break;
                        case "custOrddOrderQty":
                            ChangeOrderQty(e.RowIndex);
                            break;
                        case "custOrddOfferQty":
                            ChangedOfferQty(e.RowIndex);
                            break;
                        case "custOrddOfferPriceKrw":
                            ChagnedOfferPriceKrw(e.RowIndex);
                            break;
                        case "custOrddOfferPriceUsd":
                            ChagnedOfferPriceUsd(e.RowIndex);
                            break;
                        case "custOrddOfferAmounteKrw":
                            ChangedAmountkrw(e.RowIndex);
                            break;
                        case "custOrddOfferAmounteUsd":
                            ChangedAmountUsd(e.RowIndex);
                            break;
                        case "custOrddMargin":
                            Changedmargin(e.RowIndex);
                            break;
                    }
                    SetTextBox();

                }
                finally
                {
                    if (isEventProcessing)
                    {
                        ToggleEventHandler(false);
                    }
                }
            }
            
            
            //DgrOrderDetail.Dgr.CellValueChanged += DataGridCellChangedEvent;
            
        }
        /// <summary>
        /// 셀 편집 시작시 셀 값을 previousValue에 저장
        /// 오류 발생시 편집된 값을 원복처리하는 목적
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgr_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // 편집이 시작된 셀의 값을 previousValue에 저장
            previousValue = DgrOrderDetail.Dgr.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
        }
        /// <summary>
        /// 셀 편집시 오류 여부확인
        /// 공백 또는 Null값이 입력될 경우 previousValue에 입력된 값을 다시 입력 후 종료
        /// 마이너스 값 입력 시 강제 양수로 변경
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private bool ValueCheck(int index, string columnName)
        {
            //string columnName = DgrOrderDetail.Dgr.Columns[DgrOrderDetail.Dgr.CurrentCell.ColumnIndex].Name;
            string cellValue = DgrOrderDetail.Dgr.Rows[index].Cells[columnName].Value?.ToString();
            // 공백 또는 Null 여부 확인
            if (string.IsNullOrWhiteSpace(cellValue))
            {
                DgrOrderDetail.Dgr.Rows[index].Cells[columnName].Value = previousValue;// 이전 값으로 복원
                return false; // 유효하지 않은 값일 경우 false 반환
            }
            // 숫자가 아닌 경우 처리
            if (!decimal.TryParse(cellValue, out _))
            {
                MessageBox.Show("유효하지 않은 값입니다. 숫자만 입력해주세요.", "알림");
                DgrOrderDetail.Dgr.Rows[index].Cells[columnName].Value = previousValue; // 이전 값으로 복원
                return false; // 유효하지 않은 값일 경우 false 반환
            }
            // 마이너스 수량 입력 불가
            if (cellValue.ToString().Contains("-"))
            {
                cellValue = cellValue.Replace("-","");//마이너스 제거
                DgrOrderDetail.Dgr.Rows[index].Cells[columnName].Value = cellValue;
            }
            return true;
            
        }
        /// <summary>
        /// Grid cell의 마진 부분 변경시
        /// 매입가 / (1-(마진율 *0.01)) 로 계산하여 제안단가 한화를 변경
        /// </summary>
        /// <param name="index"></param>
        private void Changedmargin(int index)
        {

            double cellValue = DgrOrderDetail.ConvertToInt(DgrOrderDetail.Dgr.Rows[index].Cells["custOrddMargin"].Value);
            if (!ValueCheck(index, "custOrddMargin"))
            {
                return;
            }
            try
            {
                int cost = DgrOrderDetail.ConvertToInt(DgrOrderDetail.Dgr.Rows[index].Cells["custOrddBprice"].Value);
                double price = cost / (1 - (cellValue * 0.01));
                DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferPriceKrw"].Value = cellValue;
                OfferPriceusdSet(index);
                AmountKrwSet(index);
                AmountUsdSet(index);
                ProfitSet(index);
            }
            catch (Exception ex)
            {
                MessageBox.Show("숫자 형식이 올바르지 않습니다. " + ex.Message, "오류");
                DgrOrderDetail.Dgr.Rows[index].Cells["custOrddMargin"].Value = "0";
            }
        }
        /// <summary>
        /// 주문수량 수정 시 이벤트
        /// 주문수량 수정 시 판매가능 수량 부분도 적용되게 한다.
        /// 단 판매가능 수량이 이미 0일 경우 판매 가능 수량 부분은 수정히지 않고 주무수량만 변경한다 -> 이미 판매가 불가능한 상품이라는 결론을 지었다는 가정이므로
        /// </summary>
        /// <param name="index"></param>
        private void ChangeOrderQty(int index)
        {
            int cellValue = DgrOrderDetail.ConvertToInt(DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOrderQty"].Value);

            if (!ValueCheck(index, "custOrddOrderQty"))
            {
                return;
            }
            try
            {
                int offerQty = DgrOrderDetail.ConvertToInt(DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferQty"].Value);
                if(offerQty != 0)
                {
                    DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferQty"].Value = cellValue;
                }
                ChangedOfferQty(index);


            }
            catch (Exception ex)
            {
                MessageBox.Show("숫자 형식이 올바르지 않습니다. " + ex.Message, "오류");
                DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOrderQty"].Value = previousValue;
            }
            
        }
        /// <summary>
        /// 판매가능 수량 수정 시 이벤트
        /// 판매가능 수량과 제안단가로 제안액과 이익율 계산
        /// </summary>
        /// <param name="index"></param>
        private void ChangedOfferQty(int index)
        {
            int cellValue = DgrOrderDetail.ConvertToInt(DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferQty"].Value);

            if (!ValueCheck(index, "custOrddOfferQty"))
            {
                return;
            }
            try
            {
                AmountKrwSet(index);
                AmountUsdSet(index);
                OfferMarginSet(index);
                ProfitSet(index);
            }
            catch (Exception ex)
            {
                MessageBox.Show("숫자 형식이 올바르지 않습니다. " + ex.Message, "오류");
                DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferQty"].Value = previousValue;
            }   
        }
        /// <summary>
        /// 제안단가(한화) 수정 시 이벤트 
        /// 제안단가(미화)와 제안액 및 이익율등을 수정한다
        /// </summary>
        /// <param name="index"></param>
        private void ChagnedOfferPriceKrw(int index)
        {
            int cellValue = DgrOrderDetail.ConvertToInt(DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferPriceKrw"].Value);
            if (!ValueCheck(index, "custOrddOfferPriceKrw"))
            {
                return;
            }          
            try
            {
                OfferPriceusdSet(index);
                AmountKrwSet(index);
                AmountUsdSet(index);
                OfferMarginSet(index);
                ProfitSet(index);
            }
            catch (Exception ex)
            {
                MessageBox.Show("숫자 형식이 올바르지 않습니다. " + ex.Message, "오류");
                DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferPriceKrw"].Value = previousValue;
            }            
        }
        /// <summary>
        /// 제안단가(미화) 수정 시 이벤트
        /// 변경된 제안단가(미화)로 제안단가(한화)를 수정 한 후 총 제안액과 이익율을 계산한다
        /// </summary>
        /// <param name="index"></param>
        private void ChagnedOfferPriceUsd(int index)
        {
            decimal cellValue = DgrOrderDetail.ConvertToDecimal(DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferPriceUsd"].Value);
            if (!ValueCheck(index, "custOrddOfferPriceUsd"))
            {
                return;
            }
            try
            {
                decimal priceKrw = cellValue * orderExchange;
                DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferPriceKrw"].Value = priceKrw;

                AmountKrwSet(index);
                AmountUsdSet(index);
                OfferMarginSet(index);
                ProfitSet(index);
            }
            catch (Exception ex)
            {
                MessageBox.Show("숫자 형식이 올바르지 않습니다. " + ex.Message, "오류");
                DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferPriceUsd"].Value = previousValue;
            }            
        }
        /// <summary>
        /// 제안액(한화) 변경 시 이벤트
        /// 제안액과 판매가능수량으로 제안단가와 이익율 등을 수정한다
        /// </summary>
        /// <param name="index"></param>
        private void ChangedAmountkrw(int index)
        {
            int cellValue = DgrOrderDetail.ConvertToInt(DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferAmounteKrw"].Value);
            if (!ValueCheck(index, "custOrddOfferAmounteKrw"))
            {
                return;
            }
            try
            {             
                int qty = DgrOrderDetail.ConvertToInt(DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferQty"].Value);
                int priceKrw = cellValue / qty;
                DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferPriceKrw"].Value = priceKrw;

                OfferPriceusdSet(index);
                AmountUsdSet(index);
                OfferMarginSet(index);
                ProfitSet(index);
            }
            catch (Exception ex)
            {
                MessageBox.Show("숫자 형식이 올바르지 않습니다. " + ex.Message, "오류");
                DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferAmounteKrw"].Value = previousValue;
            }
        }
        /// <summary>
        /// 제안액(미화) 수정시 이벤트
        /// 판매가능 수량과 제안액(미화)를 기준으로 제안액(한화)를 계산하여 제안단가와 이익율을 수정한다
        /// </summary>
        /// <param name="index"></param>
        private void ChangedAmountUsd(int index)
        {
            int cellValue = DgrOrderDetail.ConvertToInt(DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferAmounteUsd"].Value);
            if (!ValueCheck(index, "custOrddOfferAmounteUsd"))
            {
                return;
            }
            try
            {
                decimal qty = DgrOrderDetail.ConvertToInt(DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferQty"].Value);
                decimal priceKrw = cellValue/ orderExchange / qty;
                DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferPriceKrw"].Value = priceKrw;
                OfferPriceusdSet(index);
                AmountKrwSet(index);
                OfferMarginSet(index);
                ProfitSet(index);
            }
            catch (Exception ex)
            {
                MessageBox.Show("숫자 형식이 올바르지 않습니다. " + ex.Message, "오류");
                DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferAmounteUsd"].Value = previousValue;
            }
        }
        
        /// <summary>
        /// 그리드의 제안단가 계산
        /// </summary>
        /// <param name="index"></param>
        private void OfferPriceusdSet(int index)
        {
            decimal priceKrw = DgrOrderDetail.ConvertToInt(DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferPriceKrw"].Value);
            decimal priceUsd = priceKrw / orderExchange;
            DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferPriceUsd"].Value = priceUsd.ToString("#,##0.00");
        }
        /// <summary>
        /// 그리드 제안액 채우기
        /// </summary>
        /// <param name="index"></param>
        private void AmountKrwSet(int index)
        {
            int price = Convert.ToInt32(DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferPriceKrw"].Value);
            int qty = Convert.ToInt32(DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferQty"].Value);
            int amount = price * qty;
            DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferAmounteKrw"].Value = amount;
        }
        /// <summary>
        /// 그리드 제안액(미화) 채우기
        /// </summary>
        /// <param name="index"></param>
        private void AmountUsdSet(int index)
        {
            decimal price = DgrOrderDetail.ConvertToDecimal(DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferAmounteKrw"].Value);
            decimal amount = price / orderExchange;
            DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferAmounteUsd"].Value = amount.ToString("#,##0.00");
        }
        /// <summary>
        /// 그리드 이익율 채우기
        /// </summary>
        /// <param name="index"></param>
        private void OfferMarginSet(int index)
        {
            decimal cost = DgrOrderDetail.ConvertToDecimal(DgrOrderDetail.Dgr.Rows[index].Cells["custOrddPdtBprice"].Value);
            decimal price = DgrOrderDetail.ConvertToDecimal(DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferPriceKrw"].Value);
            decimal maring = 0;
            if (cost == 0 && price == 0) 
            {
                maring = 0;
            }
            else if(cost != 0 && price == 0)
            {
                maring = -100;
            }
            else if(cost != 0 && price != 0)
            {
                maring = (price - cost) / price *100;
            }
            
            DgrOrderDetail.Dgr.Rows[index].Cells["custOrddMargin"].Value = maring.ToString("#0.00");

        }
        /// <summary>
        /// 그리드 이이액 채우기
        /// </summary>
        /// <param name="index"></param>
        private void ProfitSet(int index)
        {
            int cost = DgrOrderDetail.ConvertToInt(DgrOrderDetail.Dgr.Rows[index].Cells["custOrddPdtBprice"].Value);
            int price = DgrOrderDetail.ConvertToInt(DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferPricekrw"].Value);
            int qty = DgrOrderDetail.ConvertToInt(DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferQty"].Value);
            decimal porfit = (price * qty) - (cost * qty);
            DgrOrderDetail.Dgr.Rows[index].Cells["custOrddProfitKrw"].Value = porfit;
            DgrOrderDetail.Dgr.Rows[index].Cells["custOrddProfitUsd"].Value = (porfit / orderExchange).ToString("#,##0.00");
        }
        /// <summary>
        /// 하단 텍스트박스 내용 채우기
        /// 전체 판매액과 이익율 등 계산
        /// </summary>
        private void SetTextBox()
        {
            int dgrCounter = DgrOrderDetail.Dgr.RowCount-1;
            decimal price = 0; // 판매액
            decimal purchase = 0; // 매입원가
            decimal rate = 0.04M; // 수수료율
            decimal cost = 0; // 총매입원가
            decimal fit = 0; // 이익액
            decimal fee = 0; // 수수료
            decimal shippingkrw = Convert.ToDecimal(tBoxShippingKRW.Text); // 배송비
            decimal shippingusd = Convert.ToDecimal(tBoxShippingUSD.Text); // 배송비
            decimal totalPricekrw =0; // 최종 판매액
            decimal totalPriceUsd =0; // 최종 판매액
            decimal profitMargin = 0; // 판매가 대비 이익율
            decimal markupMargin = 0; // 원가 대비 이익율
            for (int i = 0; i < dgrCounter; i++)
            {
                if (DgrOrderDetail.Dgr.Rows[i].Cells["custOrddOfferAmounteKrw"].Value.ToString() != "")
                {
                    price = price + DgrOrderDetail.ConvertToDecimal(DgrOrderDetail.Dgr.Rows[i].Cells["custOrddOfferAmounteKrw"].Value);
                }

                if (DgrOrderDetail.Dgr.Rows[i].Cells["custOrddPdtBprice"].Value.ToString() != "")
                {
                    purchase = purchase + (DgrOrderDetail.ConvertToDecimal(DgrOrderDetail.Dgr.Rows[i].Cells["custOrddPdtBprice"].Value) * DgrOrderDetail.ConvertToInt(DgrOrderDetail.Dgr.Rows[i].Cells["custOrddOfferQty"].Value));
                }
            }
            
            totalPricekrw = Math.Round(price + shippingkrw, 0);
            totalPriceUsd = Math.Round((price/orderExchange) + shippingusd, 2);
            fee = Math.Round(totalPriceUsd * rate, 2);
            cost = purchase + Math.Round(fee * orderExchange, 0) + shippingkrw;

            amountKrw = Convert.ToInt32(totalPricekrw);
            amountUsd = totalPriceUsd;
            feeKrw = Convert.ToInt32(Math.Round((fee * orderExchange), 0));
            bprice = Convert.ToInt32(cost);


            fit = totalPricekrw - cost;
            if (fit == 0)
            {
                profitMargin = 0;
                markupMargin = 0;
            }
            else if(totalPricekrw == 0)
            {
                profitMargin = -100;
                markupMargin = -100;
            }
            else
            {
                profitMargin = fit / totalPricekrw * 100;
                markupMargin = fit / purchase * 100;
            }
            bprice = cost;
            amountKrw = Convert.ToInt32(totalPricekrw);
            amountUsd = totalPriceUsd;
            feeKrw = Convert.ToInt32(Math.Round((fee * orderExchange), 0));


            tBoxPriceKrw.Text = Math.Round(price, 2).ToString("#,##0");
            tBoxPriceUsd.Text = Math.Round((price / orderExchange), 2).ToString("#,##0.00");
            lblPurKrw.Text = purchase.ToString("#,##0");
            tBoxTotalPriceKrw.Text = totalPricekrw.ToString("#,##0");
            tBoxTotalPriceUsd.Text = totalPriceUsd.ToString("#,##0.00");
            lblFee.Text = feeKrw.ToString("#,##0") + "(" + fee + ")";
            lblCost.Text = cost.ToString("#,##0.00");
            lblFit.Text = fit.ToString("#,##0.00");
            lblMargin.Text = Math.Round(profitMargin,2) + " %";
            lblRoe.Text = Math.Round(markupMargin, 2) + " %";
        }
        /// <summary>
        /// 회원정보 불러오기
        /// </summary>
        private void LoadCustomerInfo()
        {
            string qeury = $"SELECT cust_name, cust_country, cust_addr FROM customer WHERE cust_code = {customerCode}";
            DataTable resultData = new DataTable();
            dbconn.SqlReaderQuery(qeury, resultData);
            DataRow row = resultData.Rows[0];
            lblCustName.Text = row["cust_name"].ToString();
            cmBoxCountry.SelectedIndex = Convert.ToInt32(row["cust_country"]);
            tBoxAddress.Text = Convert.ToString(row["cust_addr"]);
        }
        /// <summary>
        /// 새로운 주문서 데이터베이스에 저장 함수
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        private void InsertCsutOrderVoucher(SqlConnection connection, SqlTransaction transaction)
        {
            string query = "SELECT MAX(ISNULL(cord_code,0)) + 1 FROM custorder";
            object resultObj = new object();
            dbconn.sqlScalaQuery(query, out resultObj);
            orderCode = Convert.ToInt32(resultObj);
            string sdate = null;
            if(cmBoxOrderStatus.SelectedIndex == 2)
            {
                sdate = saleDate.ToString();
            }
            query = "INSERT INTO custorder (cord_code, cord_date, cord_cust, cord_address, cord_country, cord_status, cord_bprice, cord_amount_krw, cord_amount_usd, cord_staff, cord_idate, cord_udate, cord_sdate, cord_exchange, cord_shipping, cord_fee, cord_memo, cord_sales)" +
                "VALUES(@code, @cordDate, @cust, @address, @country, @status, @brpcie, @amountKrw, @amountUsd, @staff, GETDATE(), GETDATE(), @sdate, @exchange,  @shipping, @fee, @memo, 0)";

            var address = tBoxAddress.Text;
            var country = cmBoxCountry.SelectedIndex;
            var status = cmBoxOrderStatus.SelectedIndex;
            var saledate = string.IsNullOrEmpty(sdate) ? DBNull.Value : (object)sdate;
            var shipping = Convert.ToInt32(tBoxShippingKRW.Text);
            var fee = Convert.ToDouble(lblFee.Text.Split('(')[0].Trim());
            var memo = tBoxMemo.Text;
            SqlParameter[] sqlParameter =
                {
                    new SqlParameter("@code", SqlDbType.Int ){Value = orderCode},
                    new SqlParameter("@cordDate", SqlDbType.DateTime ){Value = orderDate},
                    new SqlParameter("@cust",SqlDbType.Int){Value = customerCode},
                    new SqlParameter("@address", SqlDbType.VarChar ){Value = address},
                    new SqlParameter("@country", SqlDbType.Int ){Value = country},
                    new SqlParameter("@status", SqlDbType.Int ){Value = status},
                    new SqlParameter("@brpcie", SqlDbType.Float ){Value = bprice},
                    new SqlParameter("@amountKrw", SqlDbType.Int ){Value = amountKrw},
                    new SqlParameter("@amountUsd", SqlDbType.Float ){Value = amountUsd},
                    new SqlParameter("@staff", SqlDbType.Int ){Value = 1},
                    new SqlParameter("@sdate", SqlDbType.Date ){Value = saledate},
                    new SqlParameter("@exchange", SqlDbType.Int ){Value = orderExchange},
                    new SqlParameter("@shipping", SqlDbType.Int ){Value = shipping},
                    new SqlParameter("@fee", SqlDbType.Float ){Value = fee},
                    new SqlParameter("@memo", SqlDbType.VarChar ){Value = memo }
                };
            dbconn.ExecuteNonQuery(query, connection, transaction, sqlParameter);
        }
        /// <summary>
        /// 주문서 저장 또는 수정시 상세내역 등록
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        private void InsertCustOrderDetail(SqlConnection connection, SqlTransaction transaction)
        {
            string query;
            for (int index = 0; index < DgrOrderDetail.Dgr.RowCount - 1; index++)
            {
                query = "INSERT INTO custorderdetail(cordd_code, cordd_seq, cordd_pdt, cordd_bprice, cordd_orderqty, cordd_sprice, cordd_offerkrw, cordd_offeruds, cordd_offerqty, cordd_amountkrw, cordd_amountusd, cordd_status, cordd_memo)\n" +
                    "VALUES(@code, @seq, @pdt, @bprice, @orderQty, @sprice, @offerKrw, @offerUsd, @offerQty, @amountKrw, @amountUsd, @status, @memo)";
                var pdtCode = Convert.ToInt32(DgrOrderDetail.Dgr.Rows[index].Cells["custOrddPdtCode"].Value);
                var bPrice = DgrOrderDetail.ConvertToInt(DgrOrderDetail.Dgr.Rows[index].Cells["custOrddPdtBprice"].Value);
                var orderQty = DgrOrderDetail.ConvertToInt(DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOrderQty"].Value);
                var sPrice = DgrOrderDetail.ConvertToInt(DgrOrderDetail.Dgr.Rows[index].Cells["custOrddPdtSprice"].Value);
                var offerPriceKrw = DgrOrderDetail.ConvertToInt(DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferPriceKrw"].Value);
                var offerPriceUsd = DgrOrderDetail.ConvertToDecimal(DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferPriceUsd"].Value);
                var offerQty = DgrOrderDetail.ConvertToInt(DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferQty"].Value);
                var amountKrw = DgrOrderDetail.ConvertToInt(DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferAmounteKrw"].Value);
                var amountUsd = DgrOrderDetail.ConvertToDecimal(DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferAmounteUsd"].Value);
                var status = Convert.ToInt32(DgrOrderDetail.Dgr.Rows[index].Cells["custOrddStatus"].Value);
                var memoValue = DgrOrderDetail.Dgr.Rows[index].Cells["custOrddMemo"].Value?.ToString();
                var memo = string.IsNullOrEmpty(memoValue) ? DBNull.Value : (object)memoValue;
                SqlParameter[] parameters =
                {
                    new SqlParameter("@code", SqlDbType.Int) { Value = orderCode },
                    new SqlParameter("@seq", SqlDbType.Int) { Value = index + 1 },
                    new SqlParameter("@pdt", SqlDbType.Int) { Value = pdtCode },
                    new SqlParameter("@bprice", SqlDbType.Float) { Value = bPrice },
                    new SqlParameter("@orderQty", SqlDbType.Int) { Value = orderQty },
                    new SqlParameter("@sprice", SqlDbType.Int) { Value = sPrice },
                    new SqlParameter("@offerKrw", SqlDbType.Int) { Value = offerPriceKrw },
                    new SqlParameter("@offerUsd", SqlDbType.Float) { Value = offerPriceUsd },
                    new SqlParameter("@offerQty", SqlDbType.Int) { Value = offerQty },
                    new SqlParameter("@amountKrw", SqlDbType.Int) { Value = amountKrw },
                    new SqlParameter("@amountUsd", SqlDbType.Float) { Value = amountUsd },
                    new SqlParameter("@status", SqlDbType.Int) { Value = status },
                    new SqlParameter("@memo", SqlDbType.VarChar) { Value = memo }
                };
                dbconn.ExecuteNonQuery(query, connection, transaction, parameters);
            }
        }
        /// <summary>
        /// 주문서 수정 함수
        /// 기존 주문 상세내역은 삭제
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        private void ModifyCustOrderVoucher(SqlConnection connection, SqlTransaction transaction)
        {
            string sdate = null;
            if (cmBoxOrderStatus.SelectedIndex == 2)
            {
                sdate = saleDate.ToString();
            }
            string query = "UPDATE custorder SET cord_date = @cordDate, cord_cust = @cust, cord_address = @address, cord_country = @country, cord_status = @status, " +
                "cord_bprice = @bprice, cord_amount_krw = @amountKrw, cord_amount_usd = @amountUsd, cord_staff = @staff, cord_udate = GETDATE(),cord_sdate = @sdate, " +
                "cord_exchange = @exchange, cord_shipping = @shipping, cord_fee = @fee, cord_memo = @memo WHERE cord_code = @code";
            SqlParameter[] sqlParameter =
                {
                    new SqlParameter("@code", SqlDbType.Int ){Value = orderCode},
                    new SqlParameter("@cordDate", SqlDbType.DateTime ){Value = orderDate},
                    new SqlParameter("@cust",SqlDbType.Int){Value = customerCode},
                    new SqlParameter("@address", SqlDbType.VarChar ){Value = tBoxAddress.Text},
                    new SqlParameter("@country", SqlDbType.Int ){Value = cmBoxCountry.SelectedIndex},
                    new SqlParameter("@status", SqlDbType.Int ){Value = cmBoxOrderStatus.SelectedIndex},
                    new SqlParameter("@bprice", SqlDbType.Float ){Value = Convert.ToDouble(lblCost.Text.Replace(",",""))},
                    new SqlParameter("@amountKrw", SqlDbType.Int ){Value = Convert.ToInt32(tBoxTotalPriceKrw.Text.Replace(",",""))},
                    new SqlParameter("@amountUsd", SqlDbType.Float ){Value = Convert.ToDouble(tBoxTotalPriceUsd.Text.Replace(",",""))},
                    new SqlParameter("@staff", SqlDbType.Int ){Value = 1},
                    new SqlParameter("@sdate", SqlDbType.Date ){ Value = string.IsNullOrEmpty(sdate) ? DBNull.Value : (object)sdate },
                    new SqlParameter("@exchange", SqlDbType.Int ){Value = orderExchange},
                    new SqlParameter("@shipping", SqlDbType.Int ){Value = Convert.ToInt32(tBoxShippingKRW.Text.Replace(",",""))},
                    new SqlParameter("@fee", SqlDbType.Float ){Value = Convert.ToDouble(lblFee.Text.Split('(')[0].Trim())},
                    new SqlParameter("@memo", SqlDbType.VarChar ){Value = tBoxMemo.Text }
                };
            dbconn.ExecuteNonQuery(query, connection, transaction, sqlParameter);
            orderStatus = cmBoxOrderStatus.SelectedIndex;
            query = "DELETE FROM custorderdetail WHERE cordd_code = @code";
            SqlParameter deletParameter = new SqlParameter("@code", SqlDbType.Int) { Value = orderCode };
            dbconn.ExecuteNonQuery(query, connection, transaction, deletParameter);
        }
        /// <summary>
        /// 판매등록시 전달된 판매제품 정보 테이블 생성
        /// </summary>
        /// <param name="gridView"></param>
        /// <returns></returns>
        private DataTable ConvertGridViewToDataTable(DataGridView gridView)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("custOrddPdtCode", typeof(int));
            dataTable.Columns.Add("custOrddOfferQty", typeof(int));
            dataTable.Columns.Add("custOrddOfferAmounteKrw", typeof(decimal));
            dataTable.Columns.Add("custOrddOfferAmounteUsd", typeof(decimal));

            for (int i =0; i<DgrOrderDetail.Dgr.RowCount -1; i++)
            {
                if(DgrOrderDetail.ConvertToInt(DgrOrderDetail.Dgr.Rows[i].Cells["custOrddOfferQty"].Value) > 0)
                {
                    int qty = DgrOrderDetail.ConvertToInt(DgrOrderDetail.Dgr.Rows[i].Cells["custOrddOfferQty"].Value);
                    if (qty >= 1)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["custOrddPdtCode"] = DgrOrderDetail.Dgr.Rows[i].Cells["custOrddPdtCode"].Value;
                        dataRow["custOrddOfferQty"] = DgrOrderDetail.Dgr.Rows[i].Cells["custOrddOfferQty"].Value;
                        dataRow["custOrddOfferAmounteKrw"] = DgrOrderDetail.Dgr.Rows[i].Cells["custOrddOfferAmounteKrw"].Value;
                        dataRow["custOrddOfferAmounteUsd"] = DgrOrderDetail.Dgr.Rows[i].Cells["custOrddOfferAmounteUsd"].Value;
                        dataTable.Rows.Add(dataRow);
                    }
                }
            }

            return dataTable;
        }
        /// <summary>
        /// 판매등록 시 전달될 판매정보 테이블 생성
        /// </summary>
        /// <returns></returns>
        private DataTable ConvertSalesInfo()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("custOrderCode", typeof(int));
            dataTable.Columns.Add("custCode", typeof(int));
            dataTable.Columns.Add("countryCode", typeof(int));
            dataTable.Columns.Add("address", typeof(string));
            dataTable.Columns.Add("delivyerfeeKrw", typeof(int));
            dataTable.Columns.Add("delivyerfeeUsd", typeof(decimal));
            DataRow dataRow = dataTable.NewRow();
            dataRow["custOrderCode"] = orderCode;
            dataRow["custCode"] = customerCode;
            dataRow["countryCode"] = cmBoxCountry.SelectedIndex;
            dataRow["address"] = tBoxAddress.Text;
            dataRow["delivyerfeeKrw"] = Convert.ToInt32(tBoxShippingKRW.Text);
            dataRow["delivyerfeeUsd"] = Convert.ToDecimal(tBoxShippingUSD.Text);
            dataTable.Rows.Add(dataRow);
            return dataTable;
        }

        private void SalesRegist()
        {
            DataTable prodcutData = ConvertGridViewToDataTable(DgrOrderDetail.Dgr);
            DataTable salesInfo = ConvertSalesInfo();
            SalesRegist salesRegist = new SalesRegist();
            salesRegist.StartPosition = FormStartPosition.CenterParent;
            salesRegist.ForwardOrder(prodcutData, salesInfo);
            salesRegist.ShowDialog();
        }

        private void LoadSales()
        {
            SalesRegist salesRegist = new SalesRegist();
            salesRegist.GetSaleCode(salesCode);
            salesRegist.StartPosition = FormStartPosition.CenterParent;
            salesRegist.ShowDialog();
        }
        /// <summary>
        /// 환율 라벨 클릭시 환율정보 수정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblExchange_Click(object sender, EventArgs e)
        {
            NumericInputDialog numericInputDialog = new NumericInputDialog();
            numericInputDialog.StartPosition = FormStartPosition.CenterParent;
            numericInputDialog.GetValue("환율", orderExchange, false);
            numericInputDialog.ValueSubmit += (decimal value) => { orderExchange = value; };
            numericInputDialog.ShowDialog();
            lblExchange.Text = orderExchange.ToString("#,##0");
            changedExchang();
        }
        /// <summary>
        /// 주문일 등록 버튼
        /// 추가등록 시 기본적으로 실행 시간이 지정되나 수정 가능
        /// 주문서가 이미 저장되었다면 시간 수정은 불가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntOrderDate_Click(object sender, EventArgs e)
        {
            
            DateTimePickerBox dtpBox = new DateTimePickerBox();
            dtpBox.StartPosition = FormStartPosition.CenterParent;
            dtpBox.GetDateTime(DateTime.Parse(lblOrderDate.Text), true);
            dtpBox.DateTiemPick += (orderDate) => { GetOrderDate(orderDate); };
            dtpBox.ShowDialog();
        }
        /// <summary>
        /// 판매일 등록 버튼 실행
        /// 상태가 판매로 지정해야 주문일 등록 가능
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntSaleDate_Click(object sender, EventArgs e)
        {
            if(cmBoxOrderStatus.SelectedIndex == 2)
            {
                DateTime date = new DateTime();
                if (orderCode == 0)
                {
                    date = DateTime.Now;
                }
                else if(string.IsNullOrEmpty(lblSaleDate.Text))
                {
                    date = DateTime.Now;
                }
                else
                {
                    date = DateTime.Parse(lblSaleDate.Text);
                }
                DateTimePickerBox dtpBox = new DateTimePickerBox();
                dtpBox.StartPosition = FormStartPosition.CenterParent;
                dtpBox.GetDateTime(date, false);
                dtpBox.DateTiemPick += (saleDate) => { if (saleDate != null) { salesToggle = true; } };
                dtpBox.DateTiemPick += (saleDate) => { GetSaleDate(saleDate); };

                dtpBox.ShowDialog();
            }
            else
            {
                MessageBox.Show("주문서의 상태를 판매로 변경 후 시도해 주세요");
            }
           
        }
        /// <summary>
        /// 회원조회 버튼 클릭
        /// 회원검색 창이 활성화된다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntCustomer_Click(object sender, EventArgs e)
        {
            CustomerSearchBox customerSearchBox = new CustomerSearchBox();
            customerSearchBox.StartPosition = FormStartPosition.CenterParent;
            customerSearchBox.GetCustomerCode += (custCode) => { customerCode = (custCode); LoadCustomerInfo(); };
            customerSearchBox.ShowDialog();
        }

        /// <summary>
        /// 저장버튼 클릭
        /// 신규 주문서는 신규 등록, 기존 주문서 조회시 수정 진행
        /// 단 이미 판매 등록이된 전표의 경우 수정 불가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntSave_Click(object sender, EventArgs e)
        {
            //DialogResult result = MessageBox.Show("저장 하시겠습니까?", "알림", MessageBoxButtons.YesNo);
            if (orderCode != 0 && orderStatus == 2)
            {
                cUIManager.ShowMessageBox("이미 판매 완료된 주문서는 수정이 불가합니다.", "알림",MessageBoxButtons.OK);
            }
            else if (MessageBox.Show("저장 하시겠습니까?", "알림", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (SqlConnection connection = dbconn.Opensql())
                {
                    SqlTransaction transaction = connection.BeginTransaction();
                    try
                    {
                       
                        if (orderCode == 0)
                        {
                            InsertCsutOrderVoucher(connection, transaction);
                            InsertCustOrderDetail(connection, transaction);
                        }
                        else
                        {
                            ModifyCustOrderVoucher(connection, transaction);
                            InsertCustOrderDetail(connection, transaction);
                        }
                        transaction.Commit();
                        if(orderStatus == 2)
                        {
                            if (cUIManager.ShowMessageBox("판매등록을 하시겠습니까?", "알림", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                SalesRegist();
                            }
                        }
                        Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }

            }
        }
        /// <summary>
        /// 닫기 버튼 클릭
        /// 실행 시 변경 사항이 있더라도 내용을 수정하지 않음
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntCancle_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("전표를 저장하지 않고 종료합니다.\n계속 하시겠습니까?", "알림", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Close();
            }
        }
        /// <summary>
        /// 제품 등록 클릭
        /// 제품검색창을 통해 상품 검색 및 선택 후 그리드에 새로운 상품 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntAddProduct_Click(object sender, EventArgs e)
        {
            ProductSearchBox productSearchBox = new ProductSearchBox();
            productSearchBox.StartPosition = FormStartPosition.CenterParent;
            ToggleEventHandler(true);
            productSearchBox.ProductForword += (pdtCode) => { AddProductToGrid(pdtCode, 0); };
            productSearchBox.ShowDialog();
            ToggleEventHandler(false);
        }
        /// <summary>
        /// 제품제거 클릭시 선택된 셀의 제품을 제거 한다
        /// 단 실제로 삭제되는 것이 아니라 status가 0으로 변경되면서 그리드에서 보여지지 않음
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntProductDelete_Click(object sender, EventArgs e)
        {
            if (!isEventProcessing)
            {
                ToggleEventHandler(true);
            }
            int index = DgrOrderDetail.Dgr.CurrentRow.Index;
            DgrOrderDetail.Dgr.Rows[index].Cells["custOrddStatus"].Value = 0;
            DgrOrderDetail.Dgr.Rows[index].Cells["custOrddOfferQty"].Value = 0;
            ChangedOfferQty(index);
            DgrOrderDetail.Dgr.Rows[index].Visible = false;
            SetTextBox();
            GridNumberSetting();
            ToggleEventHandler(false);
        }
        /// <summary>
        /// 제품정보 버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntProduct_Click(object sender, EventArgs e)
        {
            if (DgrOrderDetail.Dgr.CurrentRow.Index == DgrOrderDetail.Dgr.Rows.Count - 1)//DataGrid의 마지막행은 항상 비어 있으므로 마지막행 선택시 메시지박스 표시
            {
                MessageBox.Show("선택된 상품이 없습니다");
            }
            else
            {
                //선택된 인덱스의 제품코드를 가지고 온다
                int pdtCode = DgrOrderDetail.ConvertToInt(DgrOrderDetail.Dgr.CurrentRow.Cells["custOrddPdtCode"].Value);

                ProductDetail productDetail = new ProductDetail();
                productDetail.StartPosition = FormStartPosition.CenterParent;

                if (pdtCode != 0)//선택된 인덱스의 제품이 등록된 상품의 경우 상품코드를 통해 ProductDetail의 타입을 조회로 놓고 호출하고 
                {
                    productDetail.GetProductInfo(pdtCode);
                    //productDetail.Text = "제품 정보 조회";
                }
                else//등록되지 않은 제품의 경우 ProductDetail의 타입을 등록으로 호출하여 데이터 그리드에 있는 정보를 입력하여 한다
                {
                    MessageBox.Show("미등록 제품 입니다", "알림");
                    string pdtNumber = DgrOrderDetail.Dgr.CurrentRow.Cells["custOrddPdtNameUs"].Value.ToString();
                    string pdtBprice = DgrOrderDetail.Dgr.CurrentRow.Cells["custOrddPdtBprice"].Value.ToString();
                    productDetail.UnregisteredProduct(pdtNumber, pdtBprice, "0");
                    //productDetail.Text = "새제품 등록";

                }

                productDetail.ShowDialog();
            }
        }
        /// <summary>
        /// 주문서 판매로 등록
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntSaleRegist_Click(object sender, EventArgs e)
        {
            if(orderCode == 0) // 주문번호가 없을 경우 판매등록을 할 수 없도록 return 진행
            {
                return;
            }
            if(orderCode != 0 && orderStatus != 2)
            {
                string message = "주문서가 판매 상태가 아닙니다.\n 판매로 변경 후 등록 하세요.";
                cUIManager.ShowMessageBox(message, "알림", MessageBoxButtons.OK);
            }
            else if(salesCode != 0)
            {
                LoadSales();
            }
            else
            {
                SalesRegist();
            }
            

        }
        /// <summary>
        /// 배송비 등록 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntShipping_Click(object sender, EventArgs e)
        {
            if(orderCode != 0 && orderStatus == 2 ) // 판매저장된 상태에서는 가격 정보가 수정되지 않도록 배송비 입력을 차단
            {
                return;
            }
            SalePayment salePayment = new SalePayment();
            salePayment.StartPosition = FormStartPosition.CenterParent;
            int deliFeeKrw = Convert.ToInt32(tBoxShippingKRW.Text);
            decimal deliFeeUsd = Convert.ToDecimal(tBoxShippingUSD.Text);
            int exchange = Convert.ToInt32(orderExchange);
            int krw = 0;
            double usd = 0;
            if (deliFeeKrw == 0)
            {
                krw = deliFeeKrw;
                usd = Convert.ToDouble(deliFeeUsd);
            }
            else
            {
                krw = 0;
                usd = Convert.ToDouble(0);
            }
            salePayment.GetPaymntInfo("배송비", krw, usd, exchange);
            salePayment.ForwardAmount += (resultKrw, resultUsd, resutlType) =>
            {
                if (resutlType == true)
                {
                    tBoxShippingKRW.Text = resultKrw.ToString("#,##0");
                    tBoxShippingUSD.Text = resultUsd.ToString("#,##0.00");
                    SetTextBox();
                }
                else
                {
                    return;
                };
            };
            salePayment.ShowDialog();
        }
    }
}
