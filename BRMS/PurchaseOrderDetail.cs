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
    public partial class PurchaseOrderDetail : Form
    {
        cDatabaseConnect dbconn = new cDatabaseConnect();
        cDataGridDefaultSet DgrPurOrderdetail = new cDataGridDefaultSet();
        public event Action<int> callSup;
        int purOrderCode = 0;
        DataTable resultTable = new DataTable();
        DateTime OrderDate;
        DateTime arrivalDate;
        int supplierCode = 0;
        int purOrderStatus = 1;
        int purOrdType = 1;
        bool isNewEntry = true;
        bool typeToggle = false;
        Dictionary<string, object> originalValues = new Dictionary<string, object>();
        Dictionary<int, string> accessPermission = new Dictionary<int, string>();
        int accessedEmp = 1;
        public PurchaseOrderDetail()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedDialog;
            panelDataGrid.Controls.Add(DgrPurOrderdetail.Dgr);
            DgrPurOrderdetail.Dgr.Dock = DockStyle.Fill;
            GridForm();
            DgrPurOrderdetail.Dgr.CellValueChanged += DataGridCellChangedEvent;
        }

        /// <summary>
        /// 데이터그리드 기본 폼 설정
        /// </summary>
        private void GridForm()
        {
            DgrPurOrderdetail.Dgr.Columns.Add("purOrderdPdtCode", "제품코드");
            DgrPurOrderdetail.Dgr.Columns.Add("purOrderdPdtTax", "");
            DgrPurOrderdetail.Dgr.Columns.Add("purOrderdPdtNumber", "제품번호");
            DgrPurOrderdetail.Dgr.Columns.Add("purOrderdPdtNameKr", "제품명(한글)");
            DgrPurOrderdetail.Dgr.Columns.Add("purOrderdBprice", "매입단가");
            DgrPurOrderdetail.Dgr.Columns.Add("purOrderdSprice", "판매단가");
            DgrPurOrderdetail.Dgr.Columns.Add("purOrderdPdtStock", "현재고");
            DgrPurOrderdetail.Dgr.Columns.Add("purOrderdQty", "발주량");
            DgrPurOrderdetail.Dgr.Columns.Add("purOrderdAmount", "발주액");
            DgrPurOrderdetail.Dgr.Columns.Add("purOrderdStatus", "상태");

            DgrPurOrderdetail.Dgr.Columns["purOrderdPdtCode"].Visible = false;
            DgrPurOrderdetail.Dgr.Columns["purOrderdStatus"].Visible = false;
            //포멧설정
            DgrPurOrderdetail.FormatAsStringLeft("purOrderdPdtNumber", "purOrderdPdtNameKr", "purOrderdStatus");
            DgrPurOrderdetail.FormatAsStringCenter("purOrderdPdtTax", "purOrderdStatus");
            DgrPurOrderdetail.FormatAsDecimal("purOrderdBprice");
            DgrPurOrderdetail.FormatAsInteger("purOrderdSprice", "purOrderdQty", "purOrderdAmount", "purOrderdPdtStock");

            DgrPurOrderdetail.Dgr.Columns["No"].Width = 50;
            DgrPurOrderdetail.Dgr.Columns["purOrderdPdtTax"].Width = 40;
            DgrPurOrderdetail.Dgr.Columns["purOrderdPdtNumber"].MinimumWidth = 120;
            DgrPurOrderdetail.Dgr.Columns["purOrderdPdtNameKr"].MinimumWidth = 210;
            DgrPurOrderdetail.Dgr.Columns["purOrderdBprice"].MinimumWidth = 80;
            DgrPurOrderdetail.Dgr.Columns["purOrderdSprice"].MinimumWidth = 80;
            DgrPurOrderdetail.Dgr.Columns["purOrderdPdtStock"].Width = 40;
            DgrPurOrderdetail.Dgr.Columns["purOrderdQty"].Width = 40;
            DgrPurOrderdetail.Dgr.Columns["purOrderdAmount"].MinimumWidth = 80;
            DgrPurOrderdetail.Dgr.Columns["purOrderdStatus"].MinimumWidth = 40;

            DgrPurOrderdetail.Dgr.Columns["No"].ReadOnly = true;
            DgrPurOrderdetail.Dgr.Columns["purOrderdPdtNameKr"].ReadOnly = true;
            DgrPurOrderdetail.Dgr.Columns["purOrderdBprice"].ReadOnly = true;
            DgrPurOrderdetail.Dgr.Columns["purOrderdSprice"].ReadOnly = true;
            DgrPurOrderdetail.Dgr.Columns["purOrderdQty"].ReadOnly = true;
            DgrPurOrderdetail.Dgr.Columns["purOrderdPdtStock"].ReadOnly = true;
            DgrPurOrderdetail.Dgr.Columns["purOrderdAmount"].ReadOnly = true;

            foreach (DataGridViewColumn column in DgrPurOrderdetail.Dgr.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            DgrPurOrderdetail.Dgr.Rows.Add();
            DgrPurOrderdetail.Dgr.Rows[0].Cells[0].Value = "1";
            DgrPurOrderdetail.ApplyDefaultColumnSettings();

            //DgrPurdetail.Dgr.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        /// <summary>
        /// 발주전표 신규 생성
        /// 발주 리스트에서 등록을 클릭하면 함수 실행
        /// </summary>
        public void AddpurOrder()
        {
            GetSupplier();
            lblOrderType.Text = "매입";
            bntStatus.Visible = false;
            lblOrderStatus.Visible = false;
            GetOrderDateTime(DateTime.Now);
            GetArrivalDate(DateTime.Now);
            callSup?.Invoke(supplierCode);
        }
        /// <summary>
        /// 전표의 매입,반품 상태 변경
        /// </summary>
        private void TypeToggleSwiching()
        {
            if (typeToggle == false)
            {
                typeToggle = true;
                //bntOrderDate.Text = "반품일시";
                purOrdType = 2;
                if (DgrPurOrderdetail.Dgr.RowCount > 1)
                {
                    PurReturn();
                }
            }
            else
            {
                typeToggle = false;
                //bntOrderDate.Text = "매입일시";
                purOrdType = 1;
                if (DgrPurOrderdetail.Dgr.RowCount > 1)
                {
                    PurReturn();
                }

            }
            lblOrderType.Text = cStatusCode.GetPurchaseType(purOrdType);
            bntOrderDate.Text = cStatusCode.GetPurchaseType(purOrdType)+"일시";
            SetTextBox();
        }
        /// <summary>
        /// 클릭시 매입 또는 반품의 구분을 변경
        /// 이미 기록된 수량 및 금액은 반전
        /// </summary>
        private void PurReturn()
        {
            
            for(int index = 0; index < DgrPurOrderdetail.Dgr.RowCount-1; index++)
            {
                if(typeToggle==false)//매입일 경우
                {
                    DgrPurOrderdetail.Dgr.CellValueChanged -= DataGridCellChangedEvent;
                    int qty = DgrPurOrderdetail.ConvertToInt(DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdQty"].Value);
                    DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdQty"].Value = (qty * -1).ToString();
                }
            }
            for (int index = 0; index < DgrPurOrderdetail.Dgr.RowCount - 1; index++)
            {
                PurchasQuantityChanged(index);
            }
        }
        /// <summary>
        /// 발주일 
        /// </summary>
        /// <param name="dateTime"></param>
        private void GetOrderDateTime(DateTime dateTime)
        {
            OrderDate = dateTime;
            lblOrderDate.Text = dateTime.ToString("yyyy년 MM월 dd일 HH시 mm분");
        }
        private DateTime GetArrivalDate(DateTime date)
        {
            arrivalDate = date;
            lblArrivalDate.Text = date.ToString("yyyy년 MM월 dd일");
            return arrivalDate;
        }
        /// <summary>
        /// 매입 공급사를 변경하는 함수
        /// 매입전표 추가시 먼저 공급사를 선택하게 한다. 
        /// 매입전표 호출 후 매입공급사 버튼을 클릭하면 공급사를 수정할 수 있다
        /// </summary>
        private void GetSupplier()
        {
            SupplierSelectBox supplierSelectBox = new SupplierSelectBox();
            supplierSelectBox.StartPosition = FormStartPosition.CenterParent;
            supplierSelectBox.SupplierSelected += (supCode, supName) => { supplierCode = supCode; lblSupplier.Text = supName; };
            supplierSelectBox.ShowDialog();
        }
        private void GridNumberSetting()
        {
            int number = 1;
            for (int index = 0; index < DgrPurOrderdetail.Dgr.RowCount - 1; index++)
            {
                if (DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdStatus"].Value.ToString() == "1")
                {
                    DgrPurOrderdetail.Dgr.Rows[index].Cells["No"].Value = number;
                    number++;
                }
            }
        }
        /// <summary>
        /// DataGrid 로우 추가
        /// </summary>
        private void LastGridAdd()
        {
            if (DgrPurOrderdetail.Dgr.Rows.Count > 0)
            {
                DataGridViewRow lastRow = DgrPurOrderdetail.Dgr.Rows[DgrPurOrderdetail.Dgr.Rows.Count - 1];
                var cellValue = lastRow.Cells["purOrderdPdtNumber"].Value;

                // 마지막 행의 "purOrderdPdtNumber" 컬럼이 비어있는지 확인
                if (cellValue != null && !string.IsNullOrEmpty(cellValue.ToString()))
                {
                    // 비어있지 않다면 새로운 행 추가
                    DgrPurOrderdetail.Dgr.Rows.Add();
                }
            }
            else
            {
                // 만약 행이 없다면 첫 번째 행을 추가
                DgrPurOrderdetail.Dgr.Rows.Add();
            }
        }
        private void GridFill(DataTable dataTable)
        {
            DgrPurOrderdetail.Dgr.CellValueChanged -= DataGridCellChangedEvent;
            int rowIndex = 0;
            DgrPurOrderdetail.Dgr.Rows.Clear();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                DgrPurOrderdetail.Dgr.Rows.Add();
                //DgrPurOrderdetail.Dgr.Rows[rowIndex].Cells["No"].Value = dataRow["purd_seq"].ToString();
                DgrPurOrderdetail.Dgr.Rows[rowIndex].Cells["purOrderdPdtCode"].Value = dataRow["pordd_pdt"];
                if (dataRow["pdt_tax"].ToString() == "0")
                {
                    DgrPurOrderdetail.Dgr.Rows[rowIndex].Cells["purOrderdPdtTax"].Value = "면";
                }
                else
                {
                    DgrPurOrderdetail.Dgr.Rows[rowIndex].Cells["purOrderdPdtTax"].Value = "과";
                }
                DgrPurOrderdetail.Dgr.Rows[rowIndex].Cells["purOrderdPdtNumber"].Value = dataRow["pdt_number"];
                DgrPurOrderdetail.Dgr.Rows[rowIndex].Cells["purOrderdPdtNameKr"].Value = dataRow["pdt_name_kr"];
                DgrPurOrderdetail.Dgr.Rows[rowIndex].Cells["purOrderdBprice"].Value = dataRow["pordd_bprice"];
                DgrPurOrderdetail.Dgr.Rows[rowIndex].Cells["purOrderdSprice"].Value = dataRow["pordd_sprice"];
                DgrPurOrderdetail.Dgr.Rows[rowIndex].Cells["purOrderdPdtStock"].Value = dataRow["pdt_stock"];
                DgrPurOrderdetail.Dgr.Rows[rowIndex].Cells["purOrderdQty"].Value = dataRow["pordd_qty"];
                DgrPurOrderdetail.Dgr.Rows[rowIndex].Cells["purOrderdAmount"].Value = dataRow["pordd_amount"];
                DgrPurOrderdetail.Dgr.Rows[rowIndex].Cells["purOrderdStatus"].Value = dataRow["pordd_Status"];
                DgrPurOrderdetail.Dgr.Rows[rowIndex].Cells["purOrderdQty"].ReadOnly = false;
                DgrPurOrderdetail.Dgr.Rows[rowIndex].Cells["purOrderdPdtNumber"].ReadOnly = true;
                if (dataRow["pordd_status"].ToString() == "0")
                {
                    
                    DgrPurOrderdetail.Dgr.Rows[rowIndex].Visible = false;
                }
                rowIndex++;
            }
            LastGridAdd();
            GridNumberSetting();
            DgrPurOrderdetail.Dgr.CellValueChanged += DataGridCellChangedEvent;
            SetTextBox();
        }

        public void GetPurOrderCode(int Code)
        {
            purOrderCode = Code;
            LoadPurOrderDetail(Code);
        }
        /// <summary>
        /// 매입상세 내역 호출하는 함수
        /// 호출된 내역은 GridFill로 데이터 전송
        /// </summary>
        /// <param name="purOrderCode"></param>
        private void LoadPurOrderDetail(int OrderCode)
        {
            purOrderCode = OrderCode;
            string query = "SELECT pordd_seq, pordd_code,pordd_pdt,pdt_tax, RTRIM(pdt_number) pdt_number, RTRIM(pdt_name_kr) pdt_name_kr, pordd_bprice,pordd_sprice,pordd_qty,pdt_stock,pordd_amount,pordd_status" +
                $"\nFROM purorderdetail, product WHERE pordd_code ={purOrderCode} AND pordd_pdt = pdt_code ORDER BY pordd_seq";

            dbconn.SqlDataAdapterQuery(query, resultTable);
            GridFill(resultTable);

            query = $"SELECT pord_date,pord_arrivaldate, pord_sup, sup_name,pord_type, pord_status FROM purorder,supplier WHERE pord_code ={purOrderCode} AND pord_sup =  sup_code ";
            DataTable readDate = new DataTable();
            object resultObj = new object();
            dbconn.SqlReaderQuery(query, readDate);
            DataRow dataRow = readDate.Rows[0];
            GetOrderDateTime(DateTime.Parse(dataRow["pord_date"].ToString()));
            GetArrivalDate(DateTime.Parse(dataRow["pord_arrivaldate"].ToString()));
            
            lblSupplier.Text = dataRow["sup_name"].ToString();
            supplierCode = Convert.ToInt32(dataRow["pord_sup"].ToString());
            purOrdType = Convert.ToInt32(dataRow["pord_type"]);
            lblOrderType.Text = cStatusCode.GetPurchaseType(purOrdType);
            if (purOrdType == 1)
            {
                //lblOrderType.Text = "매입";
                typeToggle = false;
            }
            else
            {
                //lblOrderType.Text = "반품";
                typeToggle = true;
            }

            bntStatus.Visible = true;
            lblOrderStatus.Visible = true;
            purOrderStatus = Convert.ToInt32(dataRow["pord_status"]);
            int orderStatus = Convert.ToInt32(dataRow["pord_status"]);
            lblOrderStatus.Text = cStatusCode.GetPurchaseOrderStatus(orderStatus);
            isNewEntry = false;
            OrigenaDate();
        }
        private void OrigenaDate()
        {
            originalValues["@pordDate"] = OrderDate;
            originalValues["@pordSup"] = supplierCode;
            originalValues["@pordArrivla"] = arrivalDate;
            originalValues["@pordAmount"] = tBoxPurOrderAmount.Text.Replace("-", "");
            originalValues["@purType"] = purOrdType;
            originalValues["@pordNote"] = tBoxPurOrderNote.Text;
            originalValues["@pordStatus"] = purOrderStatus;

        }
        /// <summary>
        /// 데이터그리드에 제품번호를 입력시 제품번호로 검색
        /// 등록된 제품번호는 제품정보를 그대로 호출 DataGrid에 표시
        /// 미등록 제품번호는 제품명에 미등록 상품이라고 표시
        /// </summary>
        /// <param name="currentIndex"></param>
        private void SearchPdtNumberForCode(int currentIndex)
        {
            DgrPurOrderdetail.Dgr.CellValueChanged -= DataGridCellChangedEvent;
            try
            {
                string query = string.Format("SELECT pdt_code FROM product WHERE pdt_number = '{0}' ", DgrPurOrderdetail.Dgr.CurrentRow.Cells["purOrderdPdtNumber"].Value.ToString());
                object resultObj = new object();
                dbconn.sqlScalaQuery(query, out resultObj);

                if (string.IsNullOrEmpty(resultObj?.ToString()))
                {
                    MessageBox.Show("미등록 제품번호 입니다.", "알림");
                }
                else
                {
                    AddProductToGrid(Convert.ToInt32(resultObj), currentIndex);
                    DgrPurOrderdetail.Dgr.Rows[currentIndex].Cells["purOrderdPdtNumber"].ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            DgrPurOrderdetail.Dgr.CellValueChanged += DataGridCellChangedEvent;
        }

        /// <summary>
        /// 발주량 수정 시 발주액 수정
        /// </summary>
        /// <param name="index"></param>
        private void PurchasQuantityChanged(int index)
        {
            string cellValue = DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdQty"].Value.ToString().Replace(",", "");

            //purOrderdQty의 값이 공란 또는 Null일 경우 별도 경고문 생성 후 값을 0으로 강제전환
            if (string.IsNullOrWhiteSpace(cellValue))
            {
                MessageBox.Show("수량은 공란으로 설정 할 수 없습니다\n 정확한 수량을 입력하세요", "알림");
                DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdQty"].Value = "0";

            }
            else if (cellValue.ToString().Contains("-") && purOrdType == 1) // 마이너스 수량 입력 불가
            {
                MessageBox.Show("마이너스 수량은 입력 할 수 없습니다.", "알림");
                DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdQty"].Value = "0";
            }
            else
            {
                try
                {
                    DgrPurOrderdetail.Dgr.CellValueChanged -= DataGridCellChangedEvent;
                    decimal price = Convert.ToDecimal(DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdBprice"].Value);
                    decimal qty = Convert.ToDecimal(cellValue);
                    if (purOrdType != 1)
                    {
                        qty = qty * -1;
                        DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdQty"].Value = (qty).ToString();
                    }
                    DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdAmount"].Value = (price * qty).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("숫자 형식이 올바르지 않습니다. " + ex.Message, "오류");
                    DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdQty"].Value = "0";
                }
                finally
                {
                    DgrPurOrderdetail.Dgr.CellValueChanged += DataGridCellChangedEvent;
                }
            }
        }
        /// <summary>
        /// 상품검색 후 상품정보 DataGrid에 등록
        /// </summary>
        /// <param name="pdtCode"></param>
        /// <param name="index"></param>
        private void AddProductToGrid(int pdtCode, int index)
        {
            //셀에 제품 정보가 입력되어 이벤트 핸들러가 작동되지 않도록 이벤트 핸들러 제거
            DgrPurOrderdetail.Dgr.CellValueChanged -= DataGridCellChangedEvent;
            //마지막행 찾기
            if (index == 0)
            {
                index = DgrPurOrderdetail.Dgr.RowCount - 1;
            }
            //제품정보 조회 후 결과 담기
            string query = string.Format("SELECT RTRIM(pdt_number) pdt_number,pdt_tax, RTRIM(pdt_name_kr) pdt_name_kr, pdt_bprice,pdt_sprice_krw,pdt_stock FROM product WHERE pdt_code = {0}", pdtCode);
            DataTable resultData = new DataTable();
            dbconn.SqlReaderQuery(query, resultData);
            DataRow dataRow = resultData.Rows[0];
            DgrPurOrderdetail.Dgr.Rows[index].Cells["No"].Value = DgrPurOrderdetail.Dgr.RowCount;
            DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdPdtCode"].Value = pdtCode;
            DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdPdtCode"].Value = 1;
            if (dataRow["pdt_tax"].ToString() == "0")
            {
                DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdPdtTax"].Value = "면";
            }
            else
            {
                DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdPdtTax"].Value = "과";
            }
            DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdPdtNumber"].Value = dataRow["pdt_number"];
            DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdPdtNameKr"].Value = dataRow["pdt_name_kr"];
            DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdSprice"].Value = dataRow["pdt_Sprice_krw"];
            DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdBprice"].Value = dataRow["pdt_bprice"];
            DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdQty"].Value = "1";
            DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdPdtStock"].Value = dataRow["pdt_stock"];
            DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdAmount"].Value = dataRow["pdt_bprice"];
            DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdStatus"].Value = "1";
            DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdBprice"].ReadOnly = true;
            DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdQty"].ReadOnly = false;
            DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdAmount"].ReadOnly = true;
            DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdPdtNumber"].ReadOnly = true;
            //DgrPurdetail.Dgr.Rows.Add();
            LastGridAdd();
            GridNumberSetting();
            SetTextBox();
            DgrPurOrderdetail.Dgr.CellValueChanged += DataGridCellChangedEvent;
        }
        /// <summary>
        /// 데이터 그리드 변경시 이벤트 핸들러
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridCellChangedEvent(object sender, DataGridViewCellEventArgs e)
        {
            int SelectIndex = DgrPurOrderdetail.Dgr.CurrentCell.RowIndex;
            string columnName = DgrPurOrderdetail.Dgr.Columns[DgrPurOrderdetail.Dgr.CurrentCell.ColumnIndex].Name;

            switch (columnName)
            {
                case "purOrderdPdtNumber"://제품번호 Cell 입력 또는 수정
                    SearchPdtNumberForCode(SelectIndex);
                    break;
                case "purOrderdQty"://매입수량 Cell 입력 또는 수정
                    PurchasQuantityChanged(SelectIndex);
                    break;
            }
            SetTextBox();
        }
        /// <summary>
        /// 하단 매입금액, 잔액 등 표시하는 텍스트 박스 값 설정
        /// </summary>
        private void SetTextBox()
        {
            Decimal amount = 0;
            Decimal taxable = 0;
            Decimal taxfree = 0;


            for (int index = 0; index < DgrPurOrderdetail.Dgr.RowCount - 1; index++)
            {
                if (DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdPdtTax"].Value.ToString() == "과" && DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdStatus"].Value.ToString() == "1")
                {
                    taxable = taxable + DgrPurOrderdetail.ConvertToInt(DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdAmount"].Value);
                }
                else if (DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdPdtTax"].Value.ToString() == "면" && DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdStatus"].Value.ToString() == "1")
                {
                    taxfree = taxfree + DgrPurOrderdetail.ConvertToInt(DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdAmount"].Value);
                }
                amount = amount + DgrPurOrderdetail.ConvertToInt(DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdAmount"].Value);

            }
            tBoxPurOrderTaxable.Text = taxable.ToString("#,##0");
            tBoxPurOrderTaxfree.Text = taxfree.ToString("#,##0");
            tBoxPurOrderAmount.Text = amount.ToString("#,##0");
        }

        /// <summary>
        /// 매입전표 신규 등록 
        /// </summary>
        private void InsertPurOrderVoucherIntoDatabase(SqlConnection connection, SqlTransaction transaction)
        {
            object resultObj = null;
            string query = "SELECT ISNULL(max(pord_code),0) + 1 FROM purorder";
            dbconn.sqlScalaQuery(query, out resultObj);

            query = "INSERT INTO purorder (pord_code, pord_sup, pord_date, pord_arrivaldate, pord_amount, pord_idate, pord_udate, pord_type, pord_note, pord_status) " +
                    "VALUES (@pordCode, @pordSup, @pordDate, @pordArrivla, @pordAmount, GETDATE(), GETDATE(), @pordType, @pordNote, @pordStatus)";
            purOrderCode = Convert.ToInt32(resultObj);
            SqlParameter[] parameters =
            {
                new SqlParameter("@pordCode", SqlDbType.Int) { Value = purOrderCode },
                new SqlParameter("@pordSup", SqlDbType.Int) { Value = supplierCode },
                new SqlParameter("@pordDate", SqlDbType.DateTime) { Value = OrderDate },
                new SqlParameter("@pordArrivla", SqlDbType.Date) { Value = arrivalDate },
                new SqlParameter("@pordAmount", SqlDbType.Int) { Value =Convert.ToInt32(tBoxPurOrderAmount.Text.Replace(",",""))},
                new SqlParameter("@pordType", SqlDbType.Int) { Value = purOrdType},
                new SqlParameter("@pordNote", SqlDbType.VarChar) { Value = tBoxPurOrderNote.Text.Trim() },
                new SqlParameter("@pordStatus", SqlDbType.Int) { Value = purOrderStatus }
            };

            dbconn.ExecuteNonQuery(query, connection, transaction, parameters);
        }
        /// <summary>
        /// 매입전표 상세내역 등록
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        private void InsertPurOrderDetailIntoDatabase(SqlConnection connection, SqlTransaction transaction)
        {
            string query;
            for (int index = 0; index < DgrPurOrderdetail.Dgr.RowCount - 1; index++)
            {
                query = "INSERT INTO purorderdetail(pordd_code,pordd_pdt, pordd_qty, pordd_bprice, pordd_sprice, pordd_amount, pordd_idate, pordd_udate, pordd_seq, pordd_status)\n" +
                    "VALUES(@porddCode, @porddPdt, @porddQty, @porddBprice,  @porddSprice, @porddAmount, GETDATE(), GETDATE(), @porddSeq, @porddStatus)";
                SqlParameter[] parameters =
                {
                    new SqlParameter("@porddCode", SqlDbType.Int ){Value = purOrderCode},
                    new SqlParameter("@porddPdt",SqlDbType.Int){Value = DgrPurOrderdetail.ConvertToInt(DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdPdtCode"].Value)},
                    new SqlParameter("@porddQty",SqlDbType.Int){Value = DgrPurOrderdetail.ConvertToInt(DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdQty"].Value)},
                    new SqlParameter("@porddBprice",SqlDbType.Float){Value = DgrPurOrderdetail.ConvertToDecimal(DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdBprice"].Value)},
                    new SqlParameter("@porddSprice",SqlDbType.Int){Value = DgrPurOrderdetail.ConvertToInt(DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdSprice"].Value)},
                    new SqlParameter("@porddAmount",SqlDbType.Int){Value = DgrPurOrderdetail.ConvertToInt(DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdAmount"].Value)},
                    new SqlParameter("@porddSeq",SqlDbType.Int){Value = index +1 },
                    new SqlParameter("@porddStatus",SqlDbType.Int){Value = DgrPurOrderdetail.ConvertToInt(DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdStatus"].Value)},
                };
                dbconn.ExecuteNonQuery(query, connection, transaction, parameters);
                
                //수정 로그 기록
                if (isNewEntry == false && resultTable.Rows.Count >= Convert.ToInt32(parameters.First(p => p.ParameterName == "@porddSeq").Value))
                {
                    int seq = Convert.ToInt32(parameters.First(p => p.ParameterName == "@porddSeq").Value) - 1;
                    DataRow originRow = resultTable.Rows[seq];
                    IsPurdModified(originRow, parameters, connection, transaction);
                }
                //수정 대상이나 상품이 추가되어 purd_seq가 기존 값보다 클 경우 추가 로그 등록
                else if (isNewEntry == false && resultTable.Rows.Count < Convert.ToInt32(parameters.First(p => p.ParameterName == "@porddSeq").Value))
                {
                    int pdtCode = DgrPurOrderdetail.ConvertToInt(DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdPdtCode"].Value); // 제품 코드 추출
                    string pdtName = DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdPdtNameKr"].Value.ToString();
                    string pdtNumber = DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdPdtNumber"].Value.ToString();
                    string origin = ""; // 기존 데이터가 없으므로 공백 처리
                    string modify = $"제품추가";
                    cLog.InsertDetailedLogToDatabase("@porddAdd", origin, modify, purOrderCode, pdtCode, 2, accessedEmp, connection, transaction);
                }
            }
        }
        private void IsPurdModified(DataRow originRow, SqlParameter[] parameters, SqlConnection connection, SqlTransaction transaction)
        {
            // 컬럼 이름과 파라미터 매핑
            var columnsToCheck = new (string Column, string Param)[]
            {
                ("pordd_bprice", "@porddBprice"),
                ("pordd_qty", "@porddQty"),
                ("pordd_amount", "@porddAmount"),
                ("pordd_status", "@porddStatus"),
                ("pordd_seq", "@porddSeq")
            };

            foreach (var (column, param) in columnsToCheck)
            {
                // 파라미터 값 가져오기
                var paramValue = parameters.First(p => p.ParameterName == param).Value;

                // 원본 값과 비교하여 불일치시 
                if (!originRow[column].ToString().Equals(paramValue?.ToString()))
                {
                    int pdtCode = Convert.ToInt32(originRow["pordd_pdt"]);
                    string origin = originRow[column].ToString();
                    string modify = paramValue.ToString();
                    cLog.InsertDetailedLogToDatabase(param, origin, modify, purOrderCode, pdtCode, 2, accessedEmp, connection, transaction);
                }
            }
        }
        
    
        
        /// <summary>
        /// 전표 수정시 실행 쿼리
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        private void ModifyPurOrderVoucherInDatabase(SqlConnection connection, SqlTransaction transaction)
        {
            //MessageBox.Show("전표 수정");
            string query = "UPDATE purorder SET pord_sup = @pordSup, pord_date = @pordDate, pord_arrivaldate = @pordArrivla, " +
                "pord_amount = @pordAmount,  pord_udate = GETDATE(), pord_type = @pordType, pord_note = @pordNote, pord_status = @pordStatus WHERE pord_code = @pordCode";
            SqlParameter[] parameters =
            {
                new SqlParameter("@pordCode", SqlDbType.Int) { Value = purOrderCode },
                new SqlParameter("@pordSup", SqlDbType.Int) { Value = supplierCode },
                new SqlParameter("@pordDate", SqlDbType.DateTime) { Value = OrderDate },
                new SqlParameter("@pordArrivla", SqlDbType.Date) { Value = arrivalDate },
                new SqlParameter("@pordAmount", SqlDbType.Int) { Value =Convert.ToInt32(tBoxPurOrderAmount.Text.Replace(",",""))},
                new SqlParameter("@pordType", SqlDbType.Int) { Value = purOrdType},
                new SqlParameter("@pordNote", SqlDbType.VarChar) { Value = tBoxPurOrderNote.Text.Trim() },
                new SqlParameter("@pordStatus", SqlDbType.Int) { Value = purOrderStatus }
            };
            dbconn.ExecuteNonQuery(query, connection, transaction, parameters);

            
            query = "DELETE FROM purorderdetail WHERE pordd_code = @pordCode";
            SqlParameter[] delteparameter =
            {
                new SqlParameter("@pordCode",SqlDbType.Int) { Value = purOrderCode}
            };
            dbconn.ExecuteNonQuery(query, connection, transaction, delteparameter);
            Dictionary<string, string> modifiedValues = new Dictionary<string, string>();
            foreach (var param in parameters)
            {
                modifiedValues[param.ParameterName] = param.Value.ToString();
            }
            IsModified(modifiedValues, connection, transaction);
        }
        /// <summary>
        /// 전표 변경 여부 확인 후 로그 삽입
        /// </summary>
        /// <param name="modifiedValues"></param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        private void IsModified(Dictionary<string, string> modifiedValues, SqlConnection connection, SqlTransaction transaction)
        {

            foreach (var modifiedItem in modifiedValues)
            {
                // 변경된 값이 originalValues와 다를 경우
                if (originalValues.ContainsKey(modifiedItem.Key) && originalValues[modifiedItem.Key].ToString() != modifiedItem.Value)
                {
                    // 변경사항을 로그로 기록
                    cLog.InsertLogToDatabase(modifiedItem.Key, originalValues[modifiedItem.Key].ToString(), modifiedItem.Value, purOrderCode, accessedEmp, connection, transaction);
                }
            }
        }

        /// <summary>
        /// 발주전표내 제품 삭제
        /// </summary>
        private void PurchaseProductDelete()
        {
            DgrPurOrderdetail.Dgr.CellValueChanged -= DataGridCellChangedEvent;
            int index = DgrPurOrderdetail.Dgr.CurrentRow.Index;
            DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdStatus"].Value = 0;
            DgrPurOrderdetail.Dgr.Rows[index].Cells["purOrderdQty"].Value = 0;
            DgrPurOrderdetail.Dgr.Rows[index].Visible = false;
            SetTextBox();
            DgrPurOrderdetail.Dgr.CellValueChanged += DataGridCellChangedEvent;
        }

        /// <summary>
        /// 입고 상태 버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntStatus_Click(object sender, EventArgs e)
        {
            if(purOrderStatus == 1)
            {
                purOrderStatus = 2;
                bntStatus.Text = "입고취소";
                bntArrivalDate.Text = "입고완료일";

            }
            else if(purOrderStatus == 2)
            {
                purOrderStatus = 1;
                bntStatus.Text = "입고확인";
                bntArrivalDate.Text = "입고요청일";
            }
            lblOrderStatus.Text = cStatusCode.GetPurchaseOrderStatus(purOrderStatus);
        }

        /// <summary>
        /// 제품추가 버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntAddProduct_Click(object sender, EventArgs e)
        {
            ProductSearchBox productSearchBox = new ProductSearchBox();
            productSearchBox.StartPosition = FormStartPosition.CenterParent;
            productSearchBox.ProductForword += (pdtCode) => { AddProductToGrid(pdtCode, 0); };
            productSearchBox.ShowDialog();
        }

        /// <summary>
        /// 제품삭제 버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntProductDelete_Click(object sender, EventArgs e)
        {
            PurchaseProductDelete();
        }

        /// <summary>
        /// 제품정보 버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntProduct_Click(object sender, EventArgs e)
        {
            if (DgrPurOrderdetail.Dgr.CurrentRow.Index == DgrPurOrderdetail.Dgr.Rows.Count - 1)//DataGrid의 마지막행은 항상 비어 있으므로 마지막행 선택시 메시지박스 표시
            {
                MessageBox.Show("선택된 상품이 없습니다");
            }
            else
            {
                //선택된 인덱스의 제품코드를 가지고 온다
                int pdtCode = DgrPurOrderdetail.ConvertToInt( DgrPurOrderdetail.Dgr.CurrentRow.Cells["purOrderdPdtCode"].Value);

                ProductDetail productDetail = new ProductDetail();
                productDetail.StartPosition = FormStartPosition.CenterParent;

                if (pdtCode != 0)//선택된 인덱스의 제품이 등록된 상품의 경우 상품코드를 통해 ProductDetail의 타입을 조회로 놓고 호출하고 
                {
                    productDetail.GetProductInfo(pdtCode);
                    //productDetail.Text = "제품 정보 조회";
                }
                productDetail.ShowDialog();
            }
        }

        /// <summary>
        /// 저장 버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntSave_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("저장 하시겠습니까?", "알림", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                using (SqlConnection connection = dbconn.Opensql())
                {
                    SqlTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        if (purOrderCode == 0)
                        {
                            InsertPurOrderVoucherIntoDatabase(connection, transaction);
                            InsertPurOrderDetailIntoDatabase(connection, transaction);
                            cLog.InsertEmpAccessLogConnect("@purOrderRegisted", accessedEmp, purOrderCode, connection, transaction);
                        }
                        else
                        {
                            ModifyPurOrderVoucherInDatabase(connection, transaction);
                            InsertPurOrderDetailIntoDatabase(connection, transaction);
                            cLog.InsertEmpAccessLogConnect("@purOrderModify", accessedEmp, purOrderCode, connection, transaction);
                        }
                        transaction.Commit();
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
        /// 유형 버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntOrderType_Click(object sender, EventArgs e)
        {
            TypeToggleSwiching();
        }

        /// <summary>
        /// 발주일 버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntOrderDate_Click(object sender, EventArgs e)
        {
            DateTimePickerBox dateTimePickerBox = new DateTimePickerBox();
            dateTimePickerBox.StartPosition = FormStartPosition.CenterParent;
            dateTimePickerBox.GetDateTime(DateTime.Parse(lblOrderDate.Text), true);
            dateTimePickerBox.DateTiemPick += (purOrderDate) => { GetOrderDateTime(purOrderDate); };
            dateTimePickerBox.ShowDialog();
        }

        /// <summary>
        /// 입고요청일 버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntArrivalDate_Click(object sender, EventArgs e)
        {
            DateTimePickerBox dateTimePickerBox = new DateTimePickerBox();
            dateTimePickerBox.StartPosition = FormStartPosition.CenterParent;
            dateTimePickerBox.GetDateTime(DateTime.Parse(lblArrivalDate.Text), false);
            dateTimePickerBox.DateTiemPick += (arrivalDate) => { GetArrivalDate(arrivalDate); };
            dateTimePickerBox.ShowDialog();            
        }

        /// <summary>
        /// 공급사 버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntSupplier_Click(object sender, EventArgs e)
        {
            GetSupplier();
        }

        /// <summary>
        /// 취소 버튼 클릭
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
    }
}
