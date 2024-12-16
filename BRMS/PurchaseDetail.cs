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
    public partial class PurchaseDetail : Form
    {
        cDatabaseConnect dbconn = new cDatabaseConnect();
        cDataGridDefaultSet DgrPurdetail = new cDataGridDefaultSet();
        DataTable resultTable = new DataTable();//매입전표 조회 후 저장된 데이터 테이블. 수정 여부 확인 및 수정시 매입상세내역이 다시 등록되기전 매입량이 참되는데 사용
        public event Action<int> callSup;

        DateTime purDate;
        int purCode = 0;
        int supplierCode = 0;
        int purType = 1;
        int beforePurType = 1;
        int payment = 0;

        Dictionary<string, object> originalValues = new Dictionary<string, object>();
        Dictionary<int, string> accessPermission = new Dictionary<int, string>();
        int accessedEmp = 1;

        bool typeToggle = false; // 매입 반품여부 true : 반품 | fales : 매입
        bool supplierToggle = false;
        bool isNewEntry = true;
        int currentBalance = 0;

        public PurchaseDetail()
        {
            InitializeComponent();
            ControlBox = false;
            //GetDateTime(DateTime.Now);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            panelDataGrid.Controls.Add(DgrPurdetail.Dgr);
            DgrPurdetail.Dgr.Dock = DockStyle.Fill;
            GridForm();
            DgrPurdetail.Dgr.CellValueChanged += DataGridCellChangedEvent;
        }
        /// <summary>
        /// 매입전표 리스트에서 매입 추가 클릭시
        /// 공급사 지정 후 유형이 매입으로 지정되어 실행
        /// </summary>
        public void Addpurchase()
        {
            GetSupplier();
            lblPurchaseType.Text = "매입";
            GetDateTime(DateTime.Now);
            callSup?.Invoke(supplierCode);
        }
        /// <summary>
        /// 전표유형 변경 토글 스위칭 
        /// 전표유형을 클릭하면 매입 또는 반품여부를 표시하고 매입일시 버튼의 텍스트가 매입일시 또는 반품일시로 표시된다
        /// </summary>
        private void TypeToggleSwiching()
        {
            if (typeToggle == false)
            {
                typeToggle = true;
                lblPurchaseType.Text = "반품";
                bntPurDate.Text = "반품일시";
                purType = 2;
                if (DgrPurdetail.Dgr.RowCount > 1)
                {
                    PurReturn();
                }
            }
            else
            {
                typeToggle = false;
                lblPurchaseType.Text = "매입";
                bntPurDate.Text = "매입일시";
                purType = 1;
                if (DgrPurdetail.Dgr.RowCount > 1)
                {
                    PurReturn();
                }

            }
            SetTextBox();
        }
        /// <summary>
        /// 매입 또는 반품으로 전환시 매입수량 반전 처리
        /// 매입일 경우 양수, 반품일 경우 음수 처리함
        /// </summary>
        private void PurReturn()
        {
            
            for(int index = 0; index < DgrPurdetail.Dgr.RowCount-1; index++)
            {
                if(typeToggle==false)
                {
                    DgrPurdetail.Dgr.CellValueChanged -= DataGridCellChangedEvent;
                    int qty = DgrPurdetail.ConvertToInt(DgrPurdetail.Dgr.Rows[index].Cells["purdQty"].Value);
                    DgrPurdetail.Dgr.Rows[index].Cells["purdQty"].Value = (qty * -1).ToString();
                }
            }
            for (int index = 0; index < DgrPurdetail.Dgr.RowCount - 1; index++)
            {
                PurchasQuantityChanged(index);
            }
        }
        /// <summary>
        /// 매입 또는 반품일시의 시간을 받아오는 함수
        /// 처음 실행시 현재시간, 매입전표 조회시 기록된 시간, bntPurDate 버튼을 클릭하면 DateTimePickerBox에서 설정한 값을 받아온다
        /// </summary>
        /// <param name="dateTime"></param>
        private void GetDateTime(DateTime dateTime)
        {
            purDate = dateTime;
            lblPurDate.Text = dateTime.ToString("yyyy년 MM월 dd일 HH시 mm분");
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
            supplierSelectBox.SupplierSelected += (supCode, supName) => { supplierCode = supCode; lblSupplier.Text = supName;};
            supplierSelectBox.ShowDialog();
            GetBalanceFromDatabase();


        }
        /// <summary>
        /// 데이터그리드 기본 폼 설정
        /// </summary>
        private void GridForm()
        {
            DgrPurdetail.Dgr.Columns.Add("purdPdtCode", "제품코드");
            DgrPurdetail.Dgr.Columns.Add("purdPdtTax", "");
            DgrPurdetail.Dgr.Columns.Add("purdPdtNumber", "제품번호");
            DgrPurdetail.Dgr.Columns.Add("purdPdtNameKr", "제품명(한글)");
            DgrPurdetail.Dgr.Columns.Add("purdPdtNameEn", "제품명(영문)");
            DgrPurdetail.Dgr.Columns.Add("purdBprice", "매입단가");
            DgrPurdetail.Dgr.Columns.Add("purdQty", "수량");
            DgrPurdetail.Dgr.Columns.Add("purdAmount", "매입액");
            DgrPurdetail.Dgr.Columns.Add("purdStatus", "상태");

            DgrPurdetail.Dgr.Columns["purdPdtCode"].Visible = false;
            DgrPurdetail.Dgr.Columns["purdStatus"].Visible = false;
            //포멧설정
            DgrPurdetail.FormatAsStringLeft("purdPdtCode", "purdPdtNumber", "purdPdtNameKr", "purdPdtNameEn");
            DgrPurdetail.FormatAsStringCenter("purdPdtTax");
            DgrPurdetail.FormatAsDecimal("purdBprice");
            DgrPurdetail.FormatAsInteger("purdQty", "purdAmount");

            DgrPurdetail.Dgr.Columns["No"].Width = 50;
            DgrPurdetail.Dgr.Columns["purdPdtTax"].Width = 40;
            DgrPurdetail.Dgr.Columns["purdPdtNumber"].Width = 120;
            DgrPurdetail.Dgr.Columns["purdPdtNameKr"].Width = 210;
            DgrPurdetail.Dgr.Columns["purdPdtNameEn"].Width = 210;
            DgrPurdetail.Dgr.Columns["purdBprice"].Width = 119;
            DgrPurdetail.Dgr.Columns["purdQty"].Width = 40;
            DgrPurdetail.Dgr.Columns["purdAmount"].Width = 140;

            DgrPurdetail.Dgr.Columns["No"].ReadOnly = true;
            DgrPurdetail.Dgr.Columns["purdPdtNameKr"].ReadOnly = true;
            DgrPurdetail.Dgr.Columns["purdPdtNameEn"].ReadOnly = true;

            foreach (DataGridViewColumn column in DgrPurdetail.Dgr.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            DgrPurdetail.Dgr.Rows.Add();
            DgrPurdetail.Dgr.Rows[0].Cells[0].Value = "1";
            DgrPurdetail.Dgr.Columns["purdBprice"].ReadOnly = true;
            DgrPurdetail.Dgr.Columns["purdQty"].ReadOnly = true;
            DgrPurdetail.Dgr.Columns["purdAmount"].ReadOnly = true;
            DgrPurdetail.ApplyDefaultColumnSettings();
            
        }
        /// <summary>
        /// 조회된 데이터를 데이터그리드에 기록하는 함수
        /// </summary>
        /// <param name="dataTable"></param>
        private void GridFill(DataTable dataTable)
        {
            DgrPurdetail.Dgr.CellValueChanged -= DataGridCellChangedEvent;
            int rowIndex = 0;
            DgrPurdetail.Dgr.Rows.Clear();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                DgrPurdetail.Dgr.Rows.Add();
                DgrPurdetail.Dgr.Rows[rowIndex].Cells["No"].Value = dataRow["purd_seq"].ToString();
                DgrPurdetail.Dgr.Rows[rowIndex].Cells["purdPdtCode"].Value = dataRow["purd_pdt"].ToString();
                if(dataRow["pdt_tax"].ToString() == "0")
                {
                    DgrPurdetail.Dgr.Rows[rowIndex].Cells["purdPdtTax"].Value = "면";
                }
                else
                {
                    DgrPurdetail.Dgr.Rows[rowIndex].Cells["purdPdtTax"].Value = "과";
                }
                DgrPurdetail.Dgr.Rows[rowIndex].Cells["purdPdtNumber"].Value = dataRow["pdt_number"].ToString();
                DgrPurdetail.Dgr.Rows[rowIndex].Cells["purdPdtNameKr"].Value = dataRow["pdt_name_kr"].ToString();
                DgrPurdetail.Dgr.Rows[rowIndex].Cells["purdPdtNameEn"].Value = dataRow["pdt_name_en"].ToString();
                DgrPurdetail.Dgr.Rows[rowIndex].Cells["purdBprice"].Value = Convert.ToDouble(dataRow["purd_bprice"].ToString());
                DgrPurdetail.Dgr.Rows[rowIndex].Cells["purdQty"].Value = Convert.ToDecimal(dataRow["purd_qty"].ToString());
                DgrPurdetail.Dgr.Rows[rowIndex].Cells["purdAmount"].Value = Convert.ToDecimal(dataRow["purd_amount"].ToString());
                DgrPurdetail.Dgr.Rows[rowIndex].Cells["purdStatus"].Value = dataRow["purd_Status"].ToString();
                DgrPurdetail.Dgr.Rows[rowIndex].Cells["purdBprice"].ReadOnly = false;
                DgrPurdetail.Dgr.Rows[rowIndex].Cells["purdQty"].ReadOnly = false;
                DgrPurdetail.Dgr.Rows[rowIndex].Cells["purdAmount"].ReadOnly = false;
                DgrPurdetail.Dgr.Rows[rowIndex].Cells["purdPdtNumber"].ReadOnly = true;
                if(dataRow["purd_status"].ToString() =="0")
                {
                    DgrPurdetail.Dgr.Rows[rowIndex].Visible = false;
                }
                rowIndex++;
            }
            LastGridAdd();
            GridNumberSetting();
            DgrPurdetail.Dgr.CellValueChanged += DataGridCellChangedEvent;
            SetTextBox();
        }
        /// <summary>
        /// 매입리스트에서 매입전표를 더블클릭하면 매입전표 코드를 받아와 매입상세내역에 대한 함수를 호출하는 함수 
        /// </summary>
        /// <param name="purCode"></param>
        public void GetPurchaseCode(int Code)
        {
            purCode = Code;
            LoadPurchaseDetail(Code);
        }
        /// <summary>
        /// 매입상세 내역 호출하는 함수
        /// 호출된 내역은 GridFill로 데이터 전송
        /// </summary>
        /// <param name="purdCode"></param>
        private void LoadPurchaseDetail(int purdCode)
        {
            purCode = purdCode;
            string query = string.Format("SELECT purd_seq, purd_code,purd_pdt,pdt_tax, RTRIM(pdt_number) pdt_number, RTRIM(pdt_name_kr) pdt_name_kr, RTRIM(pdt_name_en) pdt_name_en,purd_bprice,purd_qty,purd_amount,purd_status" +
                "\nFROM purdetail, product WHERE purd_code ={0} AND purd_pdt = pdt_code ORDER BY purd_seq", purdCode);
            
            dbconn.SqlDataAdapterQuery(query, resultTable);
            GridFill(resultTable);
            query = string.Format("SELECT pur_date, pur_sup, sup_name,pur_type,pur_note FROM purchase,supplier WHERE pur_code ={0} AND pur_sup =  sup_code ", purdCode);
            DataTable readDate = new DataTable();
            object resultObj = new object();
            dbconn.SqlReaderQuery(query, readDate);
            DataRow dataRow = readDate.Rows[0];
            GetDateTime(DateTime.Parse(dataRow["pur_date"].ToString()));
            lblSupplier.Text = dataRow["sup_name"].ToString();
            supplierCode = Convert.ToInt32(dataRow["pur_sup"].ToString());
            purType = Convert.ToInt32(dataRow["pur_type"]);
            beforePurType = Convert.ToInt32(dataRow["pur_type"]);
            lblPurchaseType.Text = cStatusCode.GetPurchaseType(purType);
            typeToggle = purType == 1 ? false : true;
            tBoxPurchaseNote.Text = dataRow["pur_note"].ToString();
            //if (purType == "1")
            //{
            //    lblPurchaseType.Text = "매입";
            //    typeToggle = false;
            //}
            //else
            //{
            //    lblPurchaseType.Text = "반품";
            //    typeToggle = true;
            //}
            GetBalanceFromDatabase();
            OrigenaDate();
            isNewEntry = false;
        }
        /// <summary>
        /// 매입전표 조회 후 매입전표 정보 백업
        /// </summary>
        private void OrigenaDate()
        {
            originalValues["purDate"] = purDate.ToString("yyyy-MM-dd hh:mm:ss");
            originalValues["supplierCode"] = supplierCode;
            originalValues["purchaseAmount"] = tBoxPurcahseAmount.Text.Replace("-","");
            originalValues["purType"] = purType;
            originalValues["@purchaseNote"] = tBoxPurchaseNote.Text;
        }
        /// <summary>
        /// 현재 잔액을 계산하는 함수
        /// </summary>
        private void GetBalanceFromDatabase()
        {
            DateTime nowDate = DateTime.Now.AddDays(1);
            DateTime fromDate = DateTime.Now.AddMonths(-5);
            fromDate = new DateTime(fromDate.Year, fromDate.Month, 1, 0, 0, 0);

            object resultObj = new object();
            currentBalance = 0; // 잔액 0원으로 수정 후 조회 및 계산
            string query = string.Format("SELECT cb_balance FROM closingbalance WHERE cb_sup =  {0} AND cb_date = {1}", supplierCode, fromDate.AddMonths(-1).ToString("yyyyMM"));
            dbconn.sqlScalaQuery(query, out resultObj);
            currentBalance = Convert.ToInt32(resultObj);

            query = string.Format("SELECT ISNULL(SUM(pur_amount),0) FROM purchase WHERE pur_sup = {0} AND pur_date > '{1}' AND pur_date < '{2}'", supplierCode, fromDate.ToString("yyyy-MM-01"), nowDate.ToString("yyyy-MM-dd"));
            dbconn.sqlScalaQuery(query, out resultObj);
            currentBalance += Convert.ToInt32(resultObj);

            query = string.Format("SELECT ISNULL(SUM(pay_paycash + pay_accounttransfer + pay_paycredit + pay_paynote + pay_DC + pay_coupone + pay_supsiby + pay_etc),0) FROM payment WHERE pay_sup = {0} AND pay_date > '{1}' AND pay_date < '{2}' AND pay_status = 1", supplierCode, fromDate.ToString("yyyy-MM-dd"), nowDate.ToString("yyyy-MM-dd"));
            dbconn.sqlScalaQuery(query, out resultObj);
            currentBalance -= Convert.ToInt32(resultObj);

            tBoxTotalBalnace.Text = currentBalance.ToString("#,##0");
        }
        /// <summary>
        /// 데이터 그리드앞 번호 설정
        /// 무효처리된 매입상품은 번호에서 제거
        /// </summary>
        private void GridNumberSetting()
        {
            int number = 1;
            for (int index = 0; index < DgrPurdetail.Dgr.RowCount-1; index++) 
            {
                if(DgrPurdetail.Dgr.Rows[index].Cells["purdStatus"].Value.ToString() == "1")
                {
                    DgrPurdetail.Dgr.Rows[index].Cells["No"].Value = number;
                    number++;
                }
            }
        }
        /// <summary>
        /// DataGrid 행추가 함수
        /// </summary>
        private void LastGridAdd()
        {
            if (DgrPurdetail.Dgr.Rows.Count > 0)
            {
                DataGridViewRow lastRow = DgrPurdetail.Dgr.Rows[DgrPurdetail.Dgr.Rows.Count - 1];
                var cellValue = lastRow.Cells["purdPdtNumber"].Value;

                // 마지막 행의 "purdPdtNumber" 컬럼이 비어있는지 확인
                if (cellValue != null && !string.IsNullOrEmpty(cellValue.ToString()))
                {
                    // 비어있지 않다면 새로운 행 추가
                    DgrPurdetail.Dgr.Rows.Add();
                }
            }
            else
            {
                // 만약 행이 없다면 첫 번째 행을 추가
                DgrPurdetail.Dgr.Rows.Add();
            }
        }
        /// <summary>
        /// 데이터그리드에 제품번호를 입력시 제품번호로 검색
        /// 등록된 제품번호는 제품정보를 그대로 호출 DataGrid에 표시
        /// 미등록 제품번호는 제품명에 미등록 상품이라고 표시
        /// </summary>
        /// <param name="currentIndex"></param>
        private void SearchPdtNumberForCode(int currentIndex)
        {
            //string result = "";
            DgrPurdetail.Dgr.CellValueChanged -= DataGridCellChangedEvent;
            try
            {
                string query = string.Format("SELECT pdt_code FROM product WHERE pdt_number = '{0}' ", DgrPurdetail.Dgr.CurrentRow.Cells["purdPdtNumber"].Value.ToString());
                object resultObj = new object();
                dbconn.sqlScalaQuery(query, out resultObj);
                
                if(string.IsNullOrEmpty(resultObj?.ToString()))
                {
                    //int lastIndex = DgrPurdetail.Dgr.RowCount - 1;
                    DgrPurdetail.Dgr.Rows[currentIndex].Cells["No"].Value = currentIndex+1;
                    DgrPurdetail.Dgr.Rows[currentIndex].Cells["purdPdtCode"].Value = "0";
                    DgrPurdetail.Dgr.Rows[currentIndex].Cells["purdStatus"].Value = "1";
                    DgrPurdetail.Dgr.Rows[currentIndex].Cells["purdPdtTax"].Value = "과";
                    //DgrPurdetail.Dgr.Rows[currentIndex].Cells["purdPdtNumber"].Value = resultObj.ToString();
                    DgrPurdetail.Dgr.Rows[currentIndex].Cells["purdPdtNameKr"].Value = DgrPurdetail.Dgr.CurrentRow.Cells["purdPdtNumber"].Value.ToString();
                    DgrPurdetail.Dgr.Rows[currentIndex].Cells["purdPdtNameEn"].Value = DgrPurdetail.Dgr.CurrentRow.Cells["purdPdtNumber"].Value.ToString();
                    DgrPurdetail.Dgr.Rows[currentIndex].Cells["purdBprice"].Value = "0";
                    DgrPurdetail.Dgr.Rows[currentIndex].Cells["purdQty"].Value = "1";
                    DgrPurdetail.Dgr.Rows[currentIndex].Cells["purdAmount"].Value = "0";
                    DgrPurdetail.Dgr.Rows[currentIndex].Cells["purdBprice"].ReadOnly = false;
                    DgrPurdetail.Dgr.Rows[currentIndex].Cells["purdQty"].ReadOnly = false;
                    DgrPurdetail.Dgr.Rows[currentIndex].Cells["purdAmount"].ReadOnly = false;

                }
                else
                {
                    AddProductToGrid(Convert.ToInt32(resultObj), currentIndex);
                    DgrPurdetail.Dgr.Rows[currentIndex].Cells["purdPdtNumber"].ReadOnly = true;
                }
                LastGridAdd();
                SetTextBox();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            DgrPurdetail.Dgr.CellValueChanged += DataGridCellChangedEvent;
        }

        /// <summary>
        /// DataGrid에 매입단가를 수정시 총매입액 수정 함수
        /// </summary>
        /// <param name="index"></param>
        private void PurchasPriceChanged(int index)
        {

            string cellValue = DgrPurdetail.Dgr.Rows[index].Cells["purdBprice"].Value as string;
            Decimal qty = Convert.ToDecimal(DgrPurdetail.Dgr.Rows[index].Cells["purdQty"].Value.ToString());
            Decimal amount = Convert.ToDecimal(DgrPurdetail.Dgr.Rows[index].Cells["purdAmount"].Value.ToString());
            Decimal price = Convert.ToDecimal(cellValue);
            if (string.IsNullOrEmpty(cellValue))
            {
                cellValue = "0";
            }
            if(cellValue.Contains("-"))
            {
                MessageBox.Show("마이너스 금액은 입력 할 수 없습니다.", "알림");
                DgrPurdetail.Dgr.Rows[index].Cells["purdBprice"].Value = (amount/ qty).ToString();//원래 가격으로 복귀;
            }
            else
            {
                try
                {
                    DgrPurdetail.Dgr.CellValueChanged -= DataGridCellChangedEvent;
                    DgrPurdetail.Dgr.Rows[index].Cells["purdAmount"].Value = price * qty;
                }
            catch(Exception ex)
            {
                MessageBox.Show("숫자 형식이 올바르지 않습니다. " + ex.Message, "오류");
                DgrPurdetail.Dgr.Rows[index].Cells["purdBprice"].Value = (amount / qty).ToString();//원래 가격으로 복귀;
            }
            finally
            {
                DgrPurdetail.Dgr.CellValueChanged += DataGridCellChangedEvent;
            }
            }
            
        }
        /// <summary>
        /// DataGrid 매입수량 수정 시 총매입액 수정 함수
        /// </summary>
        /// <param name="index"></param>
        private void PurchasQuantityChanged(int index)
        {
            string cellValue = DgrPurdetail.Dgr.Rows[index].Cells["purdQty"].Value.ToString().Replace(",","");
            
            //purdQty의 값이 공란 또는 Null일 경우 별도 경고문 생성 후 값을 0으로 강제전환
            if (string.IsNullOrWhiteSpace(cellValue))
            {
                MessageBox.Show("수량은 공란으로 설정 할 수 없습니다\n 정확한 수량을 입력하세요", "알림");
                DgrPurdetail.Dgr.Rows[index].Cells["purdQty"].Value = "0";
            }
            else if (cellValue.ToString().Contains("-")&&purType ==1) // 마이너스 수량 입력 불가
            {
                MessageBox.Show("마이너스 수량은 입력 할 수 없습니다.", "알림");
                DgrPurdetail.Dgr.Rows[index].Cells["purdQty"].Value = "0";
            }
            else
            {
                try
                {
                    DgrPurdetail.Dgr.CellValueChanged -= DataGridCellChangedEvent;
                    decimal price = Convert.ToDecimal(DgrPurdetail.Dgr.Rows[index].Cells["purdBprice"].Value);
                    decimal qty = Convert.ToDecimal(cellValue);
                    if (purType != 1)
                    {
                        qty = qty * -1;
                        DgrPurdetail.Dgr.Rows[index].Cells["purdQty"].Value = (qty).ToString();
                    }
                    DgrPurdetail.Dgr.Rows[index].Cells["purdAmount"].Value = (price * qty).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("숫자 형식이 올바르지 않습니다. " + ex.Message, "오류");
                    DgrPurdetail.Dgr.Rows[index].Cells["purdQty"].Value = "0";
                }
                finally
                {
                    DgrPurdetail.Dgr.CellValueChanged += DataGridCellChangedEvent;
                }
            }
        }
        /// <summary>
        /// DataGrid 매입액 수정시 매입단가 수정
        /// </summary>
        /// <param name="index"></param>
        private void PurchaseAmountChanged(int index)
        {
            Decimal qty = Convert.ToDecimal(DgrPurdetail.Dgr.Rows[index].Cells["purdQty"].Value.ToString());
            Decimal price = Convert.ToDecimal(DgrPurdetail.Dgr.Rows[index].Cells["purdBprice"].Value.ToString());
            string cellValue = DgrPurdetail.Dgr.Rows[index].Cells["purdAmount"].Value as string;
            Decimal amount = Convert.ToDecimal(cellValue);
            // 셀내용이 공백일 경우 0으로 강제 변경
            if (string.IsNullOrEmpty(cellValue)) 
            {
                cellValue = "0";
            }
            if(cellValue.Contains("-")) // 마이너스 수량 입력 불가
            {
                MessageBox.Show("마이너스 금액은 입력 할 수 없습니다.", "알림");
                
                DgrPurdetail.Dgr.Rows[index].Cells["purdAmount"].Value = (qty * price).ToString();//원래 가격으로 복귀;
            }
            else
            {
                try
                {
                    DgrPurdetail.Dgr.CellValueChanged -= DataGridCellChangedEvent;
                    if (purType != 1)
                    {
                        amount = amount * -1;
                    }
                    DgrPurdetail.Dgr.Rows[index].Cells["purdBprice"].Value = (amount / qty).ToString();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("숫자 형식이 올바르지 않습니다. " + ex.Message, "오류");
                    DgrPurdetail.Dgr.Rows[index].Cells["purdAmount"].Value = (qty * price).ToString();//원래 가격으로 복귀;
                }
                finally
                {
                    DgrPurdetail.Dgr.CellValueChanged += DataGridCellChangedEvent;
                }
            }
        }
        /// <summary>
        /// 제품추가 기능 함수
        /// DataGrid에 추가되는 부분은 GridFill 함수를 이용할 수 있도록 대체 고려
        /// </summary>
        /// <param name="pdtCode"></param>
        /// <param name="index"></param>
        private void AddProductToGrid(int pdtCode, int index)
        {
            //셀에 제품 정보가 입력되어 이벤트 핸들러가 작동되지 않도록 이벤트 핸들러 제거
            DgrPurdetail.Dgr.CellValueChanged -= DataGridCellChangedEvent;
            //마지막행 찾기
            if (index == 0)
            {
                index = DgrPurdetail.Dgr.RowCount - 1;
            }
            //제품정보 조회 후 결과 담기
            string query = string.Format("SELECT RTRIM(pdt_number) pdt_number,pdt_tax, RTRIM(pdt_name_kr) pdt_name_kr, RTRIM(pdt_name_en) pdt_name_en, pdt_bprice FROM product WHERE pdt_code = {0}", pdtCode);
            DataTable resultData = new DataTable();
            dbconn.SqlReaderQuery(query, resultData);
            DataRow dataRow = resultData.Rows[0];
            DgrPurdetail.Dgr.Rows[index].Cells["No"].Value = DgrPurdetail.Dgr.RowCount;
            DgrPurdetail.Dgr.Rows[index].Cells["purdPdtCode"].Value = pdtCode;
            DgrPurdetail.Dgr.Rows[index].Cells["purdStatus"].Value = 1;
            if (dataRow["pdt_tax"].ToString() == "0")
            {
                DgrPurdetail.Dgr.Rows[index].Cells["purdPdtTax"].Value = "면";
            }
            else
            {
                DgrPurdetail.Dgr.Rows[index].Cells["purdPdtTax"].Value = "과";
            }
            DgrPurdetail.Dgr.Rows[index].Cells["purdPdtNumber"].Value = dataRow["pdt_number"].ToString();
            DgrPurdetail.Dgr.Rows[index].Cells["purdPdtNameKr"].Value = dataRow["pdt_name_kr"].ToString();
            DgrPurdetail.Dgr.Rows[index].Cells["purdPdtNameEn"].Value = dataRow["pdt_name_en"].ToString();
            DgrPurdetail.Dgr.Rows[index].Cells["purdBprice"].Value = Convert.ToDecimal(dataRow["pdt_bprice"].ToString());
            DgrPurdetail.Dgr.Rows[index].Cells["purdQty"].Value = "1";
            DgrPurdetail.Dgr.Rows[index].Cells["purdAmount"].Value = Convert.ToInt32(dataRow["pdt_bprice"].ToString());
            DgrPurdetail.Dgr.Rows[index].Cells["purdBprice"].ReadOnly = false;
            DgrPurdetail.Dgr.Rows[index].Cells["purdQty"].ReadOnly = false;
            DgrPurdetail.Dgr.Rows[index].Cells["purdAmount"].ReadOnly = false;
            DgrPurdetail.Dgr.Rows[index].Cells["purdPdtNumber"].ReadOnly = true;
            //DgrPurdetail.Dgr.Rows.Add();
            LastGridAdd();
            SetTextBox();
            DgrPurdetail.Dgr.CellValueChanged += DataGridCellChangedEvent;
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
            if(result == DialogResult.Yes)
            {
                Close();
            }
        }
        /// <summary>
        /// 매입 또는 반품일시 버튼 클릭시 DateTimePickerBox 함수 호출 후 시간 설정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntPurDate_Click(object sender, EventArgs e)
        {
            DateTimePickerBox dateTimePickerBox = new DateTimePickerBox();
            dateTimePickerBox.StartPosition = FormStartPosition.CenterParent;
            dateTimePickerBox.GetDateTime(DateTime.Parse(lblPurDate.Text),true);
            dateTimePickerBox.DateTiemPick += (purDatePick) => { this.GetDateTime(purDatePick); };
            dateTimePickerBox.ShowDialog();
        }
        /// <summary>
        /// DataGrid에 입력 또는 수정 사항이 있을 경우 항목 수정 이벤트 핸들러
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridCellChangedEvent(object sender, DataGridViewCellEventArgs e)
        {
            int SelectIndex = DgrPurdetail.Dgr.CurrentCell.RowIndex;
            string columnName = DgrPurdetail.Dgr.Columns[DgrPurdetail.Dgr.CurrentCell.ColumnIndex].Name;
            
            switch(columnName)
            {
                case "purdPdtNumber"://제품번호 Cell 입력 또는 수정
                    SearchPdtNumberForCode(SelectIndex);
                    break;

                case "purdBprice"://매입단가 Cell 입력 또는 수정
                    PurchasPriceChanged(SelectIndex);
                    break;
                case "purdQty"://매입수량 Cell 입력 또는 수정
                    PurchasQuantityChanged(SelectIndex);
                    break;
                case "purdAmount"://매입액 Cell 입력 또는 수정
                    PurchaseAmountChanged(SelectIndex);
                    break;       
            }
            SetTextBox();
        }
        /// <summary>
        /// 매입전표 신규 등록 
        /// </summary>
        private void InsertPurchaseVoucher(SqlConnection connection, SqlTransaction transaction)
        {
            object resultObj = null;
            string query = "SELECT ISNULL(max(pur_code),0) + 1 FROM purchase";
            dbconn.sqlScalaQuery(query, out resultObj);

            query = "INSERT INTO purchase (pur_code, pur_date, pur_idate, pur_udate, pur_sup, pur_amount, pur_payment, pur_type, pur_note, pur_taxable, pur_taxfree) " +
                    "VALUES (@purCode, @purDate, GETDATE(), GETDATE(), @supplierCode, @purchaseAmount,@purPayment, @purType, @purchaseNote, @purchaseTaxable, @purchaseTaxfree)";
            purCode = Convert.ToInt32(resultObj);
            SqlParameter[] parameters =
            {
                new SqlParameter("@purCode", SqlDbType.Int) { Value = purCode },
                new SqlParameter("@purDate", SqlDbType.DateTime) { Value = purDate.ToString("yyyy-MM-dd hh:mm:ss") },
                new SqlParameter("@supplierCode", SqlDbType.Int) { Value = supplierCode },
                new SqlParameter("@purchaseAmount", SqlDbType.Int) { Value = tBoxPurcahseAmount.Text.Replace(",","") },
                new SqlParameter("@purPayment", SqlDbType.Int) { Value =payment},
                new SqlParameter("@purType", SqlDbType.Int) { Value = purType },
                new SqlParameter("@purchaseNote", SqlDbType.NVarChar) { Value = tBoxPurchaseNote.Text },
                new SqlParameter("@purchaseTaxable", SqlDbType.Int) { Value = tBoxPurcahseTaxable.Text.Replace(",","") },
                new SqlParameter("@purchaseTaxfree", SqlDbType.Int) { Value = tBoxPurcahseTaxfree.Text.Replace(",","") }
            };

            dbconn.ExecuteNonQuery(query, connection, transaction, parameters);
        }
        /// <summary>
        /// 매입전표 상세내역 등록
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        private void InsertPurchaseDetail(SqlConnection connection, SqlTransaction transaction)
        {
            string query;
            string purdPdt;
            string purdQty;
            for(int index = 0; index < DgrPurdetail.Dgr.RowCount -1; index++)
            {
                query = "INSERT INTO purdetail(purd_code, purd_pdt,purd_qty,purd_bprice,purd_amount,purd_idate,purd_udate,purd_seq,purd_status)\n" +
                    "VALUES(@purdCode, @purdPdtCode,@purdQty,@purdBprice,@purdAmount,GETDATE(),GETDATE(),@purdSeq,@purdStatus)";
                purdPdt = DgrPurdetail.Dgr.Rows[index].Cells["purdPdtCode"].Value.ToString();
                purdQty = DgrPurdetail.Dgr.Rows[index].Cells["purdQty"].Value.ToString();
                SqlParameter[] parameters =
                {
                    new SqlParameter("@purdCode", SqlDbType.Int ){Value = purCode},
                    new SqlParameter("@purdPdtCode",SqlDbType.Int){Value = purdPdt },
                    new SqlParameter("@purdQty",SqlDbType.Int ){Value = purdQty},
                    new SqlParameter("@purdBprice",SqlDbType.Float){Value = DgrPurdetail.ConvertToDecimal(DgrPurdetail.Dgr.Rows[index].Cells["purdBprice"].Value)},
                    new SqlParameter("@purdAmount",SqlDbType.Int){Value = DgrPurdetail.ConvertToDecimal(DgrPurdetail.Dgr.Rows[index].Cells["purdAmount"].Value)},
                    new SqlParameter("@purdSeq",SqlDbType.Int){Value = index +1 },
                    new SqlParameter("@purdStatus",SqlDbType.Int){Value = DgrPurdetail.Dgr.Rows[index].Cells["purdStatus"].Value.ToString()}
                };
                dbconn.ExecuteNonQuery(query, connection, transaction, parameters);
                UpdateProductStock(connection, transaction, purdPdt, purdQty);
                //수정 로그 기록
                if (isNewEntry == false && resultTable.Rows.Count >= Convert.ToInt32(parameters.First(p => p.ParameterName == "@purdSeq").Value))
                {
                    int seq = Convert.ToInt32(parameters.First(p => p.ParameterName == "@purdSeq").Value) - 1;
                    DataRow originRow = resultTable.Rows[seq];
                    IsPurdModified(originRow, parameters, connection, transaction);
                }
                //수정 대상이나 상품이 추가되어 purd_seq가 기존 값보다 클 경우 추가 로그 등록
                else if(isNewEntry == false && resultTable.Rows.Count < Convert.ToInt32(parameters.First(p => p.ParameterName == "@purdSeq").Value))
                {
                    int pdtCode = DgrPurdetail.ConvertToInt(DgrPurdetail.Dgr.Rows[index].Cells["purdPdtCode"].Value); // 제품 코드 추출
                    string pdtName = DgrPurdetail.Dgr.Rows[index].Cells["purdPdtNameKr"].Value.ToString();
                    string pdtNumber = DgrPurdetail.Dgr.Rows[index].Cells["purdPdtNumber"].Value.ToString();
                    string origin = ""; // 기존 데이터가 없으므로 NULL 처리
                    string modify = $"제품추가";
                    cLog.InsertDetailedLogToDatabase("@purdAdd", origin, modify, purCode, pdtCode, 1, accessedEmp, connection, transaction);
                }

            }
        }
        private void IsPurdModified(DataRow originRow, SqlParameter[] parameters, SqlConnection connection, SqlTransaction transaction)
        {
            // 컬럼 이름과 파라미터 매핑
            var columnsToCheck = new (string Column, string Param)[]
            {
                ("purd_bprice", "@purdBprice"),
                ("purd_qty", "@purdQty"),
                ("purd_amount", "@purdAmount"),
                ("purd_status", "@purdStatus"),
                ("purd_seq", "@purdSeq")
            };

            foreach (var (column, param) in columnsToCheck)
            {
                // 파라미터 값 가져오기
                var paramValue = parameters.First(p => p.ParameterName == param).Value;

                // 원본 값과 비교하여 불일치시 
                if (!originRow[column].ToString().Equals(paramValue?.ToString()))
                {
                    int pdtCode = Convert.ToInt32(originRow["purd_pdt"]);
                    string origin = originRow[column].ToString();
                    string modify = paramValue.ToString();
                    cLog.InsertDetailedLogToDatabase(param, origin, modify, purCode, pdtCode, 1, accessedEmp, connection, transaction);
                }   
            }
        }
        /// <summary>
        /// 현재고 수정
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="productCode"></param>
        /// <param name="qty"></param>
        private void UpdateProductStock(SqlConnection connection, SqlTransaction transaction, string productCode, string qty)
        {
            string query = "UPDATE product SET pdt_stock = pdt_stock + @qty WHERE pdt_code = @pdtCode";
            SqlParameter[] parameters =
            {
                new SqlParameter("@qty",SqlDbType.Int){Value = qty},
                new SqlParameter("@pdtCode",SqlDbType.Int){ Value = productCode }
            };
            dbconn.ExecuteNonQuery(query, connection, transaction, parameters);
        }

        /// <summary>
        /// 매입전표 수정
        /// </summary>
        private void ModifyPurchaseVoucher(SqlConnection connection, SqlTransaction transaction)
        {
            //MessageBox.Show("전표 수정");
            string query = "UPDATE purchase SET  pur_date = @purDate,pur_udate = GETDATE() ,pur_sup = @supCode,pur_amount =@purAmount," +
                "pur_payment = @purPayment,pur_type =@purType,pur_note = @purNote,pur_taxable =@purTaxable,pur_taxfree =@purTaxfree  WHERE pur_code = @purCode";
            SqlParameter[] parameters =
            {
                new SqlParameter("@purDate",SqlDbType.DateTime){Value = purDate.ToString("yyyy-MM-dd hh:mm")},
                new SqlParameter("@supCode",SqlDbType.Int) { Value = supplierCode},
                new SqlParameter("@purAmount",SqlDbType.Int) { Value = tBoxPurcahseAmount.Text.Replace(",","")},
                new SqlParameter("@purPayment",SqlDbType.Int) { Value = int.Parse(tBoxPayment.Text.Replace(",",""))},
                new SqlParameter("@purType",SqlDbType.Int) { Value = purType},
                new SqlParameter("@purNote",SqlDbType.NVarChar) { Value = tBoxPurchaseNote.Text.ToString()},
                new SqlParameter("@purTaxable",SqlDbType.Int) { Value = int.Parse(tBoxPurcahseTaxable.Text.Replace(",",""))},
                new SqlParameter("@purTaxfree", SqlDbType.Int) { Value = int.Parse(tBoxPurcahseTaxfree.Text.Replace(",",""))},
                new SqlParameter("@purCode", SqlDbType.Int) { Value = purCode}
            };
            dbconn.ExecuteNonQuery(query,connection, transaction,parameters);
            
            string purdPdt;
            string purdQty;
            foreach(DataRow row in resultTable.Rows)
            {
                purdPdt = row["purd_pdt"].ToString();
                if (beforePurType == 1 )
                {
                    purdQty = "-" + row["purd_qty"].ToString().Replace("-", "");
                }
                else
                {
                    purdQty = row["purd_qty"].ToString().Replace("-","");
                }

                UpdateProductStock(connection, transaction, purdPdt, purdQty);

            }

            query = "DELETE FROM purdetail WHERE purd_code = @purdCode";
            SqlParameter[] delteparameter =
            {
                new SqlParameter("@purdCode",SqlDbType.Int) { Value = purCode}
            };
            dbconn.ExecuteNonQuery(query, connection, transaction, delteparameter);
            Dictionary<string, string> modifiedValues = new Dictionary<string, string>();
            foreach (var param in parameters)
            {
                modifiedValues[param.ParameterName] = param.Value.ToString();
            }
            IsModified(modifiedValues, connection, transaction);
        }
        private void IsModified(Dictionary<string, string> modifiedValues, SqlConnection connection, SqlTransaction transaction)
        {

            foreach (var modifiedItem in modifiedValues)
            {
                // 변경된 값이 originalValues와 다를 경우
                if (originalValues.ContainsKey(modifiedItem.Key) && originalValues[modifiedItem.Key].ToString() != modifiedItem.Value)
                {
                    // 변경사항을 로그로 기록
                    cLog.InsertDetailedLogToDatabase(modifiedItem.Key, originalValues[modifiedItem.Key].ToString(), modifiedItem.Value, purCode, 0, 1, accessedEmp, connection, transaction);
                }
            }
        }

        /// <summary>
        /// 매입전표내 제품 삭제시 그 삭제내역을 별도로 기록
        /// 매입전표 수정 실행시에만 적용
        /// 삭제로그를 남기고 재고도 다시 수정
        /// </summary>
        private void PurchaseProductDelete()
        {
            DgrPurdetail.Dgr.CellValueChanged -= DataGridCellChangedEvent;
            int index = DgrPurdetail.Dgr.CurrentRow.Index;
            DgrPurdetail.Dgr.Rows[index].Cells["purdStatus"].Value = 0;
            DgrPurdetail.Dgr.Rows[index].Cells["purdQty"].Value = 0;
            PurchasQuantityChanged(index);
            DgrPurdetail.Dgr.Rows[index].Visible = false;
            SetTextBox();
            DgrPurdetail.Dgr.CellValueChanged += DataGridCellChangedEvent;
        }
        
        /// <summary>
        /// 하단 매입금액, 잔액 등 표시하는 텍스트 박스 값 설정
        /// </summary>
        private void SetTextBox()
        {
            Decimal amount = 0;
            Decimal taxable = 0;
            Decimal taxfree = 0;
            Decimal payment = 0;

            for(int index = 0; index < DgrPurdetail.Dgr.RowCount -1; index++)
            {
                if( DgrPurdetail.Dgr.Rows[index].Cells["purdPdtTax"].Value.ToString() == "과" && DgrPurdetail.Dgr.Rows[index].Cells["purdStatus"].Value.ToString() =="1")
                {
                    taxable = taxable + Convert.ToDecimal(DgrPurdetail.Dgr.Rows[index].Cells["purdAmount"].Value.ToString());
                }
                else if(DgrPurdetail.Dgr.Rows[index].Cells["purdPdtTax"].Value.ToString() == "면" && DgrPurdetail.Dgr.Rows[index].Cells["purdStatus"].Value.ToString() == "1")
                {
                    taxfree = taxfree + Convert.ToDecimal(DgrPurdetail.Dgr.Rows[index].Cells["purdAmount"].Value.ToString());
                }
                amount = amount + Convert.ToDecimal(DgrPurdetail.Dgr.Rows[index].Cells["purdAmount"].Value.ToString());
                
            }
            tBoxPurcahseTaxable.Text = taxable.ToString("#,##0");
            tBoxPurcahseTaxfree.Text = taxfree.ToString("#,##0");
            tBoxPurcahseAmount.Text = amount.ToString("#,##0");
            tBoxPurchasePayable.Text = (amount - payment).ToString("#,##0");
        }
        
        /// <summary>
        /// 공급사 버튼 클릭시 공급사 선택
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntSupplier_Click(object sender, EventArgs e)
        {
            GetSupplier();
        }
        /// <summary>
        /// 전표유형 선택 버튼 클릭시 함수 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntOrderType_Click(object sender, EventArgs e)
        {
            TypeToggleSwiching();
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
            productSearchBox.ProductForword += (pdtCode) => { AddProductToGrid(pdtCode,0); };
            productSearchBox.ShowDialog();
        }
        /// <summary>
        /// 제품정보 버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntProduct_Click(object sender, EventArgs e)
        {
            if(DgrPurdetail.Dgr.CurrentRow.Index == DgrPurdetail.Dgr.Rows.Count-1)//DataGrid의 마지막행은 항상 비어 있으므로 마지막행 선택시 메시지박스 표시
            {
                MessageBox.Show("선택된 상품이 없습니다");
            }
            else
            {
                //선택된 인덱스의 제품코드를 가지고 온다
                int pdtCode = DgrPurdetail.ConvertToInt(DgrPurdetail.Dgr.CurrentRow.Cells["purdPdtCode"].Value);

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
                    string pdtNumber = DgrPurdetail.Dgr.CurrentRow.Cells["purdPdtNumber"].Value.ToString();
                    string pdtBprice = DgrPurdetail.Dgr.CurrentRow.Cells["purdBprice"].Value.ToString();
                    productDetail.UnregisteredProduct(pdtNumber, pdtBprice, "0");
                    //productDetail.Text = "새제품 등록";

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
            if(result==DialogResult.Yes)
            {
                 using(SqlConnection connection = dbconn.Opensql())
                 {
                    SqlTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        if (purCode == 0)
                        {
                            InsertPurchaseVoucher(connection,transaction);
                            InsertPurchaseDetail(connection, transaction);
                        }
                        else
                        {
                            ModifyPurchaseVoucher(connection, transaction);
                            InsertPurchaseDetail(connection, transaction);
                        }
                        transaction.Commit();
                        Close();
                    }
                    catch(Exception ex)
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
        /// 제품 삭제 버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntProductDelete_Click(object sender, EventArgs e)
        {
            PurchaseProductDelete();
        }
        /// <summary>
        /// 결제 버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntPayment_Click(object sender, EventArgs e)
        {
            if(purCode == 0 )
            {
                MessageBox.Show("매입전표 등록 후 결제 등록이 가능합니다", "알림");
            }
            else
            {
                string query = "SELECT pay_code FROM payment WHERE pay_purcode = " + purCode;
                object resultObj = new object();
                dbconn.sqlScalaQuery(query, out resultObj);
                int payCode = Convert.ToInt32(resultObj);
                PaymentDetail paymentDetail = new PaymentDetail();
                int supCode = supplierCode;
                int pur = Convert.ToInt32(purCode);
                int payAmount = Convert.ToInt32(tBoxPurcahseAmount.Text.Replace(",", ""));
                if (payCode == 0)
                {
                    paymentDetail.AddPayment(supCode, pur, payAmount);
                }
                else
                {
                    paymentDetail.LoadPaymentDetail(payCode);
                }
                paymentDetail.StartPosition = FormStartPosition.CenterParent;
                paymentDetail.ShowDialog();
            }
            
        }
    }
}
