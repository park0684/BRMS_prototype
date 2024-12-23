using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace BRMS
{
    public partial class ProductDetail : Form
    {
        cDatabaseConnect dbconn = new cDatabaseConnect();
        DataTable resultTable = new DataTable();
        CategoryTreeView categoryTree = new CategoryTreeView();
        public event Action<int> GetPdtCode;
        int pdtCode = 0;
        int pdtTop = 0;
        int pdtMid = 0;
        int pdtBot = 0;
        double exChange = 1250;
        int pdtSupplierCode = 0;
        int accessedEmp = 1;
        bool errerCheck = false;
        Dictionary<int, string> accessPermission = new Dictionary<int, string>();
        Dictionary<string, object> originalValues = new Dictionary<string, object>();
        cDataGridDefaultSet dgrPurchase = new cDataGridDefaultSet();
        cDataGridDefaultSet dgrSales = new cDataGridDefaultSet();
        cDataGridDefaultSet dgrLog = new cDataGridDefaultSet();
        internal Func<object, object> refresh;
        static Dictionary<string, (int typeCode, string typeString)> parameter = new Dictionary<string, (int, string)>();
        public ProductDetail()
        {
            InitializeComponent();
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            InitializeTabControl();
            InitializeComboBox();
            InitializeDataGrid();
            tBoxPriceUsd.KeyUp += UsdToKrwExchange;
            tBoxPriceKrw.KeyUp += KrwToUsdExchange;
            tboxBprice.KeyUp += tBoxBprice_Keyup;
            tBoxProductNumber.Leave += tBoxProductNumber_Leave;
            tBoxMargin.KeyUp += tBoxMargin_KeyUp;
            dgrSales.CellDoubleClick += dgrSales_CellDoubleClick;
        }
        private void InitializeTabControl()
        {
            // TabControl에 접근하여 페이지 이름 설정
            tabCtrlProduct.TabPages[0].Text = "상품정보";
            tabCtrlProduct.TabPages[1].Text = "매입내역";
            tabCtrlProduct.TabPages[2].Text = "판매내역";
            tabCtrlProduct.TabPages[3].Text = "변경로그";
            //tabCtrlProduct.TabPages[4].Text = "수불내역";

        }
        /// <summary>
        /// 상품상태 콤보박스 설정
        /// </summary>
        private void InitializeComboBox()
        {
            
            
            foreach (var status in cStatusCode.ProductStatus)
            {
                cBoxProductStstus.Items.Add(new KeyValuePair<int, string>(status.Key, status.Value));
            }

            cBoxProductStstus.DisplayMember = "Value"; // 사용자에게 보여질 값
            cBoxProductStstus.ValueMember = "Key";    // 내부적으로 사용할 값
            cBoxProductStstus.DropDownStyle = ComboBoxStyle.DropDownList;
            cBoxProductStstus.SelectedIndex = 0;

            foreach (var status in cStatusCode.TaxStatus)
            {
                cBoxTax.Items.Add(new KeyValuePair<int, string>(status.Key, status.Value));
            }
            cBoxTax.DisplayMember = "Value";
            cBoxTax.ValueMember = "key";
            cBoxTax.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        /// <summary>
        /// 다른 클래스에서 제품코드로 호출 시 상품정보를 가지고온다
        /// </summary>
        /// <param name="code"></param>
        public void GetProductInfo(int code)
        {
            
            if( code != 0)
            {
                pdtCode = code;

                resultTable.Clear();
                string query = string.Format("SELECT RTRIM(pdt_name_kr) pdt_name_kr,RTRIM(pdt_name_en) pdt_name_en,RTRIM(pdt_number) pdt_number,pdt_tax,pdt_top,pdt_mid,pdt_bot,pdt_stock,pdt_status,pdt_bprice,pdt_sprice_krw,pdt_sprice_usd," +
                "pdt_weight,pdt_width,pdt_length,pdt_height,pdt_idate,pdt_udate,pdt_sup FROM product WHERE pdt_code = '{0}'", code);
                dbconn.SqlReaderQuery(query, resultTable);
                
                DataRow resultRow = resultTable.Rows[0];
                
                pdtSupplierCode = Convert.ToInt32(resultRow["pdt_sup"]);
                string pdtNumber = resultRow["pdt_number"].ToString();
                string pdtNameKr = resultRow["pdt_name_kr"].ToString();
                string pdtNameEn = resultRow["pdt_name_en"].ToString();
                decimal pdtBprice = cDataHandler.ConvertToDecimal(resultRow["pdt_bprice"]);
                int pdtPriceKrw = cDataHandler.ConvertToInt(resultRow["pdt_sprice_krw"]);
                decimal pdtPriceUsd = cDataHandler.ConvertToDecimal(resultRow["pdt_sprice_usd"]);
                decimal pdtWidth = cDataHandler.ConvertToDecimal(resultRow["pdt_width"]);
                decimal pdtLength = cDataHandler.ConvertToDecimal(resultRow["pdt_length"]);
                decimal pdtHigth = cDataHandler.ConvertToDecimal(resultRow["pdt_height"]);
                decimal pdtWeigth = cDataHandler.ConvertToDecimal(resultRow["pdt_weight"]);
                int pdtStatus = cDataHandler.ConvertToInt(resultRow["pdt_status"]);
                int pdtTax = cDataHandler.ConvertToInt(resultRow["pdt_tax"]);
                int pdtStock = cDataHandler.ConvertToInt(resultRow["pdt_stock"]);

                tBoxProductNumber.Text =    pdtNumber;
                tBoxProductName_kr.Text =   pdtNameKr;
                tBoxProductName_en.Text =   pdtNameEn;
                tboxBprice.Text =           pdtBprice == 0? "0" :pdtBprice.ToString("#,###.##");
                tBoxPriceKrw.Text =         pdtPriceKrw.ToString("#,##0");
                tBoxPriceUsd.Text =         pdtPriceUsd == 0 ? "0" : pdtPriceUsd.ToString("#,###.##");
                tBoxWidth.Text =            pdtWidth == 0? "0": pdtWidth.ToString("#,###.0");
                tBoxLength.Text =           pdtLength == 0 ? "0" : pdtLength.ToString("#,###.#");
                tBoxHight.Text =            pdtHigth == 0 ? "0" : pdtHigth.ToString("#,###.#");
                tBoxProductWeight.Text =    pdtWeigth == 0 ? "0" : pdtWeigth.ToString("#,###.#");
                cBoxProductStstus.SelectedIndex = pdtStatus - 1;
                cBoxTax.SelectedIndex = pdtTax;
                lblStock.Text = pdtStock.ToString("#,##0");
                if(tBoxWidth.Text == "" || tBoxLength.Text =="" || tBoxHight.Text == "")
                {
                    lblVolumWeight.Text = "0";
                }
                else
                {
                    lblVolumWeight.Text = ((double.Parse(tBoxHight.Text) * double.Parse(tBoxLength.Text) * double.Parse(tBoxWidth.Text)) / 6000).ToString();
                }
                object resultObj = new object();
                query = $"SELECT sup_name FROM supplier WHERE sup_code = {pdtSupplierCode}";
                dbconn.sqlScalaQuery(query, out resultObj);
                lblSupplier.Text = resultObj.ToString().Trim();
                pdtTop = Convert.ToInt32(resultRow["pdt_top"]);
                pdtMid = Convert.ToInt32(resultRow["pdt_mid"]);
                pdtBot = Convert.ToInt32(resultRow["pdt_bot"]);
                //tBoxProductMemo.Text = resultRow["pdt_memo"].ToString();
                ChangeMargin();
                //GetCategory(code);
                GetCategoryInfo(pdtTop, pdtMid, pdtBot);
                this.Text = "제품 정보 조회";
                RegisterOriginalData();
            }
        }
        /// <summary>
        /// 조회된 원본 데이터 originalValues 딕셔너리에 등록
        /// 수정시 원본과 수정본을 비교하여 로그 생성시 before 데이터로 사용
        /// </summary>
        private void RegisterOriginalData()
        {
            originalValues["@pdtName_kr"] = tBoxProductName_kr.Text;
            originalValues["@pdtName_en"] = tBoxProductName_en.Text;
            originalValues["@pdtSup"] = pdtSupplierCode;
            originalValues["@pdtCat"] = $"{pdtTop}_{pdtMid}_{pdtBot}";
            originalValues["@pdtStatus"] = cBoxProductStstus.SelectedIndex+1;
            originalValues["@pdtBprice"] = tboxBprice.Text.Replace(",", "");    
            originalValues["@pdtSprice_krw"] = tBoxPriceKrw.Text.Replace(",", "");
            originalValues["@pdtSprice_usd"] = tBoxPriceUsd.Text.Replace(",", "");
            originalValues["@pdtWeight"] = tBoxProductWeight.Text.Replace(",", "");
            originalValues["@pdtWidth"] = tBoxWidth.Text.Replace(",", "");
            originalValues["@pdtLength"] = tBoxLength.Text.Replace(",", "");
            originalValues["@pdtHeight"] = tBoxHight.Text.Replace(",", "");
            originalValues["@pdtTax"] = cBoxTax.SelectedIndex;
        }

        /// <summary>
        /// 분류 정보 가지고 오기
        /// </summary>
        /// <param name="pdtTop"></param>
        /// <param name="pdtMid"></param>
        /// <param name="pdtBot"></param>
        private void GetCategoryInfo(int pdtTop, int pdtMid, int pdtBot)
        {
            DataTable dataTable = new DataTable();
            DataRow dataRow;
            string query;
            query = string.Format("SELECT cat_name_kr,cat_name_en FROM category WHERE cat_top = {0} AND cat_mid = 0 AND  cat_bot = 0", pdtTop);
            dataTable.Reset();
            dbconn.SqlReaderQuery(query, dataTable);
            dataRow = dataTable.Rows[0];
            lblTopKr.Text = dataRow["cat_name_kr"].ToString();
            lblTopEn.Text = dataRow["cat_name_en"].ToString();

            query = string.Format("SELECT cat_name_kr,cat_name_en FROM category WHERE cat_top = {0} AND cat_mid = {1} AND  cat_bot = 0", pdtTop, pdtMid);
            dataTable.Reset();
            dbconn.SqlReaderQuery(query, dataTable);
            dataRow = dataTable.Rows[0];
            lblMidKr.Text = dataRow["cat_name_kr"].ToString();
            lblMidEn.Text = dataRow["cat_name_en"].ToString();

            query = string.Format("SELECT cat_name_kr,cat_name_en FROM category WHERE cat_top = {0} AND cat_mid = {1} AND  cat_bot = {2}", pdtTop, pdtMid, pdtBot);
            dataTable.Reset();
            dbconn.SqlReaderQuery(query, dataTable);
            dataRow = dataTable.Rows[0];
            lblBotKr.Text = dataRow["cat_name_kr"].ToString();
            lblBotEn.Text = dataRow["cat_name_en"].ToString();
        }
        /// <summary>
        /// 공급사 변경 또는 지정을 위한 공급사 지정 폼 호출
        /// </summary>
        private void GetSupplier()
        {
            SupplierSelectBox supplierSelectBox = new SupplierSelectBox();
            supplierSelectBox.StartPosition = FormStartPosition.CenterParent;
            supplierSelectBox.SupplierSelected += (supCode, supName) => { pdtSupplierCode = supCode; lblSupplier.Text = supName; };
            supplierSelectBox.ShowDialog();
        }
        /// <summary>
        /// 제품번호 입력 후 포커스 이동시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tBoxProductNumber_Leave(object sender, EventArgs e)
        {
            if(tBoxProductNumber.Text != "" )
            {
                tBoxProductNumber_changed();
            }
        }
        /// <summary>
        /// 제품번호 변경 시 동일한 제품번호가 있는지 검색 후 알림 표시
        /// </summary>
        private void tBoxProductNumber_changed()
        {
            Object resultObj = new object();
            string query = string.Format("SELECT pdt_code FROM product WHERE pdt_number = '{0}'", tBoxProductNumber.Text);
            if (pdtCode == 0 )
            {
                dbconn.sqlScalaQuery(query, out resultObj);
                if (resultObj != null)
                {
                    MessageBox.Show("이미 등록된 제품 번호입니다", "알림");
                    GetProductInfo(Convert.ToInt32(resultObj));
                }
            }
            else
            {
                DataRow dataRow = resultTable.Rows[0];
                if (tBoxProductNumber.Text != dataRow["pdt_number"].ToString())
                {
                    dbconn.sqlScalaQuery(query, out resultObj);
                    if (resultObj != null)
                    {
                        MessageBox.Show("이미 등록된 제품 번호입니다", "알림");
                        GetProductInfo(Convert.ToInt32(resultObj));
                    }
                }
            }
        }

        /// <summary>
        /// 매입 또는 주문서에서 제품번호 입력 후 미등록 상품일 경우 신규제품을 할 수 있도록 입력된 제품정보와 가격정보를 그대로 가지고온다
        /// </summary>
        /// <param name="pdtNumber"></param>
        /// <param name="bPrice"></param>
        /// <param name="priceKrw"></param>
        /// <param name="priceUsd"></param>
        public void UnregisteredProduct(string pdtNumber, string bPrice, string priceKrw)
        {
            tBoxProductNumber.Text = pdtNumber;
            tBoxProductName_kr.Text = pdtNumber;
            tBoxProductName_en.Text = pdtNumber;
            tboxBprice.Text = bPrice;
            tBoxPriceKrw.Text = priceKrw;
            tBoxPriceUsd.Text = (double.Parse(priceKrw) / exChange).ToString();
            ChangeMargin();
            cBoxProductStstus.SelectedIndex = 0;
            cBoxTax.SelectedIndex = 1;
            lblTopKr.Text = "";
            lblTopEn.Text = "";
            lblMidKr.Text = "";
            lblMidEn.Text = "";
            lblBotKr.Text = "";
            lblBotEn.Text = "";
            lblSupplier.Text = "";
            this.Text = "새제품 등록";
            tabCtrlProduct.TabPages.Remove(tabPage2);
            tabCtrlProduct.TabPages.Remove(tabPage3);
            tabCtrlProduct.TabPages.Remove(tabPage4);
        }

        /// <summary>
        /// 이익율 변경. 판매가, 매입가 변경시에 자동으로 변경. 상품을 조회시 이익율을 계산하여 표시
        /// </summary>
        private void ChangeMargin()
        {
            double margin = 0;
            if (!string.IsNullOrEmpty(tboxBprice.Text))
            {
                double bprice = Convert.ToDouble(tboxBprice.Text);
                double sprice = Convert.ToDouble(tBoxPriceKrw.Text);
                margin = (sprice - bprice) / sprice * 100;
            }
            tBoxMargin.Text = Convert.ToDouble(tBoxPriceKrw.Text) == 0 ? "0" :margin.ToString("#,###.##");

        }
        /// <summary>
        /// 매입단가 입력시 마진율 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tBoxBprice_Keyup(object sender, KeyEventArgs e)
        {
            ChangeMargin();
        }
        /// <summary>
        /// 판매가 TextBox USD -> KRW로 전환
        /// </summary>
        /// <param name="sener"></param>
        /// <param name="e"></param>
        private void UsdToKrwExchange(object sener, KeyEventArgs e)
        {

            cDataHandler.AllowDecimalTwoPlaces(sener, e, tBoxPriceUsd);
            if (!string.IsNullOrWhiteSpace(tBoxPriceUsd.Text))
            {
                tBoxPriceKrw.Text = (double.Parse(tBoxPriceUsd.Text) * exChange).ToString();
            }

            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(tBoxPriceUsd.Text))
                {
                    tBoxPriceKrw.Text = (double.Parse(tBoxPriceUsd.Text) * exChange).ToString();
                }
            }
            //e.Handled = true;
            ChangeMargin();
        }

        /// <summary>
        /// 판매가 TextBox KRW -> USD로 전환
        /// </summary>
        /// <param name="sener"></param>
        /// <param name="e"></param>
        private void KrwToUsdExchange(object sener, KeyEventArgs e)
        {
            
            cDataHandler.AllowOnlyInteger(sener, e, tBoxPriceKrw);
            if (!string.IsNullOrWhiteSpace(tBoxPriceKrw.Text))
            {
                tBoxPriceUsd.Text = (double.Parse(tBoxPriceKrw.Text)  / exChange).ToString();
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(tBoxPriceKrw.Text))
                {
                    tBoxPriceUsd.Text = (double.Parse(tBoxPriceKrw.Text)  / exChange).ToString();
                }
            }
            ChangeMargin();
        }
        /// <summary>
        /// 마진율 수정시 판매가 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tBoxMargin_KeyUp(object sender, KeyEventArgs e)
        {

            cDataHandler.AllowDecimalTwoPlaces(sender, e, tBoxMargin);
            if(Convert.ToDecimal(tBoxMargin.Text) >= 100  )
            {
                cUIManager.ShowMessageBox("99.99 이하로만 입력이 가능합니다", "알림", MessageBoxButtons.OK);
                tBoxMargin.Text = "0";
            }
            double margin = !string.IsNullOrEmpty(tBoxMargin.Text) ? Convert.ToDouble(tBoxMargin.Text) / 100 : 0;
            double bprice = Convert.ToDouble(tboxBprice.Text);
            double sprice = margin == 1 ? bprice * 100 : bprice / (1 - margin);
            if (!string.IsNullOrWhiteSpace(tBoxMargin.Text))
            {
                tBoxPriceKrw.Text = sprice.ToString("#,###;0;0");
                tBoxPriceUsd.Text = (double.Parse(tBoxPriceKrw.Text) * (1 / exChange)).ToString();
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(tBoxMargin.Text))
                {
                    tBoxPriceKrw.Text = sprice.ToString("#,###;0;0");
                    tBoxPriceUsd.Text = (double.Parse(tBoxPriceKrw.Text) * (1 / exChange)).ToString();
                }
            }
        }
        /// <summary>
        /// 저장 전 입력되지 않은 부분 확인
        /// </summary>
        private void PdtErrorCheck()
        {
            if(pdtTop == 0 || pdtMid == 0 || pdtBot == 0 )
            {
                cUIManager.ShowMessageBox("분류가 선택되지 않았습니다", "알림", MessageBoxButtons.OK);
                errerCheck = true;
                return;
            }
            if(pdtSupplierCode == 0 )
            {
                cUIManager.ShowMessageBox("공급사가 선택되지 않았습니다", "알림", MessageBoxButtons.OK);
                errerCheck = true;
                return;
            }
            if(string.IsNullOrEmpty(tBoxProductNumber.Text))
            {
                cUIManager.ShowMessageBox("제품번호가 입력되지 않았습니다", "알림", MessageBoxButtons.OK);
                errerCheck = true;
                return;
            }
            if (string.IsNullOrEmpty(tBoxProductName_kr.Text))
            {
                cUIManager.ShowMessageBox("제품명이 입력되지 않았습니다", "알림", MessageBoxButtons.OK);
                errerCheck = true;
                return;
            }
            if (string.IsNullOrEmpty(tBoxProductName_en.Text))
            {
                DialogResult result = cUIManager.ShowMessageBox("제품명이 입력되지 않았습니다\n계속 하시겠습니까?", "알림", MessageBoxButtons.YesNo);
                if(result == DialogResult.No)
                {
                    errerCheck = true;
                    return;
                }               
            }
            if (tboxBprice.Text == "0")
            {
                DialogResult result = cUIManager.ShowMessageBox("매입가가 입력되지 않았습니다\n계속 하시겠습니까?", "알림", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    errerCheck = true;
                    return;
                }
            }
            if (tBoxPriceKrw.Text == "0")
            {
                DialogResult result = cUIManager.ShowMessageBox("판매가가 입력되지 않았습니다\n계속 하시겠습니까?", "알림", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    errerCheck = true;
                    return;
                }
            }
        }
        /// <summary>
        /// 새로운 제품 등록 쿼리
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        private void InsertProduct(SqlConnection connection, SqlTransaction transaction)
        {
            object resultObj = new object();
            string query = "SELECT MAX(pdt_code) + 1 FROM product ";
            dbconn.sqlScalaQuery(query, out resultObj);
            query ="INSERT INTO product(pdt_code, pdt_number,pdt_name_kr, pdt_name_en, pdt_sup, pdt_top, pdt_mid, pdt_bot, pdt_status, pdt_bprice, pdt_sprice_krw, pdt_sprice_usd, pdt_weight, pdt_width, pdt_length, pdt_height, pdt_tax, pdt_stock, pdt_idate, pdt_udate) " +
                "\nVALUES(@code, @number, @name_kr, @name_en, @sup, @top, @mid, @bot, @status, @bprice, @sprice_krw, @sprice_usd, @weight, @width, @length, @height,@tax,0, GETDATE(),GETDATE())";
            var getStatus = (KeyValuePair<int, string>)cBoxProductStstus.SelectedItem;
            int pdtStatusIndex = getStatus.Key;
            var getTax = (KeyValuePair<int, string>)cBoxTax.SelectedItem;
            int taxIndex = getTax.Key;

            SqlParameter[] sqlParameter =
            {
                new SqlParameter("@code",SqlDbType.Int){Value = resultObj.ToString()},
                new SqlParameter("@number",SqlDbType.VarChar){Value = tBoxProductNumber.Text},
                new SqlParameter("@name_kr",SqlDbType.VarChar){Value = tBoxProductName_kr.Text},
                new SqlParameter("@name_en",SqlDbType.VarChar){Value = tBoxProductName_en.Text},
                new SqlParameter("@sup",SqlDbType.Int){Value = pdtSupplierCode},
                new SqlParameter("@top",SqlDbType.Int){Value = pdtTop},
                new SqlParameter("@mid",SqlDbType.Int){Value = pdtMid},
                new SqlParameter("@bot",SqlDbType.Int){Value = pdtBot},
                new SqlParameter("@status",SqlDbType.Int){Value = pdtStatusIndex},
                new SqlParameter("@bprice",SqlDbType.Float){Value =  tboxBprice.Text.Replace(",", "")},
                new SqlParameter("@sprice_krw",SqlDbType.Int){Value =  tBoxPriceKrw.Text.Replace(",", "")},
                new SqlParameter("@sprice_usd",SqlDbType.Float){Value = tBoxPriceUsd.Text.Replace(",", "")},
                new SqlParameter("@weight",SqlDbType.Float){Value = string.IsNullOrEmpty(tBoxProductWeight.Text) ? 0 : Convert.ToDecimal(tBoxProductWeight.Text.Replace(",",""))},
                new SqlParameter("@width",SqlDbType.Float){Value =  string.IsNullOrEmpty(tBoxWidth.Text) ? 0 : Convert.ToDecimal(tBoxWidth.Text.Replace(",",""))},
                new SqlParameter("@length",SqlDbType.Float){Value =  string.IsNullOrEmpty(tBoxLength.Text) ? 0 : Convert.ToDecimal(tBoxLength.Text.Replace(",",""))},
                new SqlParameter("@height",SqlDbType.Float){Value =  string.IsNullOrEmpty(tBoxHight.Text) ? 0 : Convert.ToDecimal(tBoxHight.Text.Replace(",",""))},
                new SqlParameter("@tax",SqlDbType.Int){Value = taxIndex},
            };

            dbconn.ExecuteNonQuery(query, connection, transaction, sqlParameter);
            pdtCode = Convert.ToInt32(resultObj);
        }
        /// <summary>
        /// 제품 수정 쿼리
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        private void ModifyProduct(SqlConnection connection, SqlTransaction transaction)
        {
            string query = "UPDATE product SET pdt_number = @pdtNumber, pdt_name_kr = @pdtName_kr, pdt_name_en = @pdtName_en, pdt_sup = @pdtSup, pdt_top =  @pdtTop, pdt_mid =  @pdtMid, pdt_bot = @pdtBot, " +
                "pdt_status =  @pdtStatus, pdt_bprice = @pdtBprice, pdt_sprice_krw = @pdtSprice_krw, pdt_sprice_usd = @pdtSprice_usd, pdt_weight = @pdtWeight, pdt_width = @pdtWidth, pdt_length = @pdtLength" +
                ", pdt_height = @pdtHeight, pdt_udate = GETDATE(), pdt_tax = @pdtTax WHERE pdt_code = @pdtCode";
            var getStatus = (KeyValuePair<int, string>)cBoxProductStstus.SelectedItem;
            int pdtStatusIndex = getStatus.Key;
            var getTax = (KeyValuePair<int, string>)cBoxTax.SelectedItem;
            int taxIndex = getTax.Key;
            SqlParameter[] parameters =
            {
                new SqlParameter("@pdtCode",SqlDbType.Int){Value = pdtCode},
                new SqlParameter("@pdtNumber",SqlDbType.VarChar){Value = tBoxProductNumber.Text},
                new SqlParameter("@pdtName_kr",SqlDbType.VarChar){Value = tBoxProductName_kr.Text},
                new SqlParameter("@pdtName_en",SqlDbType.VarChar){Value = tBoxProductName_en.Text},
                new SqlParameter("@pdtSup",SqlDbType.Int){Value = pdtSupplierCode},
                new SqlParameter("@pdtTop",SqlDbType.Int){Value = pdtTop},
                new SqlParameter("@pdtMid",SqlDbType.Int){Value = pdtMid},
                new SqlParameter("@pdtBot",SqlDbType.Int){Value = pdtBot},
                new SqlParameter("@pdtCat",SqlDbType.VarChar){Value =$"{pdtTop}_{pdtMid}_{pdtBot}"},
                new SqlParameter("@pdtStatus",SqlDbType.Int){Value = pdtStatusIndex},
                new SqlParameter("@pdtBprice",SqlDbType.Float){Value = tboxBprice.Text.Replace(",", "")},
                new SqlParameter("@pdtSprice_krw",SqlDbType.Int){Value = tBoxPriceKrw.Text.Replace(",", "")},
                new SqlParameter("@pdtSprice_usd",SqlDbType.Float){Value = tBoxPriceUsd.Text.Replace(",", "")},
                new SqlParameter("@pdtweight",SqlDbType.Float){Value = string.IsNullOrEmpty(tBoxProductWeight.Text) ? 0 : Convert.ToDecimal(tBoxProductWeight.Text.Replace(",",""))},
                new SqlParameter("@pdtwidth",SqlDbType.Float){Value =  string.IsNullOrEmpty(tBoxWidth.Text) ? 0 : Convert.ToDecimal(tBoxWidth.Text.Replace(",",""))},
                new SqlParameter("@pdtlength",SqlDbType.Float){Value =  string.IsNullOrEmpty(tBoxLength.Text) ? 0 : Convert.ToDecimal(tBoxLength.Text.Replace(",",""))},
                new SqlParameter("@pdtheight",SqlDbType.Float){Value =  string.IsNullOrEmpty(tBoxHight.Text) ? 0 : Convert.ToDecimal(tBoxHight.Text.Replace(",",""))},
                new SqlParameter("@pdtTax",SqlDbType.Int){Value = taxIndex}
            };
            dbconn.ExecuteNonQuery(query, connection, transaction, parameters);
            // 수정된 값들 딕셔너리에 저장
            Dictionary<string, string> modifiedValues = new Dictionary<string, string>();
            foreach (var param in parameters)
            {
                modifiedValues[param.ParameterName] = param.Value.ToString();
            }
            cLog.IsModified(originalValues, modifiedValues, pdtCode, accessedEmp, connection, transaction);
        }
       
        private void InitializeDataGrid()
        {
            panelOrder.Controls.Add(dgrPurchase.Dgr);
            panelSale.Controls.Add(dgrSales.Dgr);
            panelStock.Controls.Add(dgrLog.Dgr);

            dgrPurchase.Dgr.Dock = DockStyle.Fill;
            dgrPurchase.Dgr.Columns.Add("purDate", "매입일");
            dgrPurchase.Dgr.Columns.Add("purType", "구분");
            dgrPurchase.Dgr.Columns.Add("purQty", "매입량");
            dgrPurchase.Dgr.Columns.Add("purAmount", "매입액");
            dgrPurchase.Dgr.Columns.Add("purBprice", "매입단가");
            dgrPurchase.Dgr.Columns.Add("purSupplier", "공급사");
            dgrPurchase.Dgr.ReadOnly = true;
            dgrPurchase.Dgr.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgrPurchase.FormatAsDateTime("purDate");
            dgrPurchase.FormatAsStringCenter("purType", "purSupplier");
            dgrPurchase.FormatAsInteger("purQty", "purAmount");
            dgrPurchase.FormatAsDecimal("purBprice");
            dgrPurchase.ApplyDefaultColumnSettings();

            dgrSales.Dgr.Dock = DockStyle.Fill;
            dgrSales.Dgr.Columns.Add("saleDate", "판매일");
            dgrSales.Dgr.Columns.Add("saleType", "유형");
            dgrSales.Dgr.Columns.Add("saleAmount", "판매액");
            dgrSales.Dgr.Columns.Add("saleSprice", "판매단가");
            dgrSales.Dgr.Columns.Add("saleBprice", "판매원가");
            dgrSales.Dgr.Columns.Add("saleQty", "판매수량");
            dgrSales.Dgr.Columns.Add("saleCustomer", "고객");
            dgrSales.Dgr.Columns.Add("saleCode", "판매코드");
            dgrSales.Dgr.ReadOnly = true;
            dgrSales.Dgr.Columns["saleCode"].Visible = false;
            dgrSales.Dgr.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgrSales.FormatAsDateTime("saleDate");
            dgrSales.FormatAsStringCenter("saleType");
            dgrSales.FormatAsStringLeft("salecustomer");
            dgrSales.FormatAsInteger("saleAmount", "saleSprice", "saleQty");
            dgrSales.FormatAsDecimal("saleBprice");
            dgrSales.ApplyDefaultColumnSettings();

            dgrLog.Dgr.Dock = DockStyle.Fill;
            dgrLog.Dgr.Columns.Add("logParam", "파라메터");
            dgrLog.Dgr.Columns.Add("logType", "작업내역");
            dgrLog.Dgr.Columns.Add("logBefore", "변경전");
            dgrLog.Dgr.Columns.Add("logAfter", "변경후");
            dgrLog.Dgr.Columns.Add("logEmpName", "작업자명");
            dgrLog.Dgr.Columns.Add("logEmp", "직원코드");
            dgrLog.Dgr.Columns.Add("logDate", "변경일");
            dgrLog.Dgr.ReadOnly = true;
            dgrLog.ApplyDefaultColumnSettings();
            dgrLog.Dgr.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgrLog.FormatAsDateTime("logDate");
            dgrLog.FormatAsStringLeft("logPdtNake", "logPdtNumber", "logType", "logBefore", "logAfter");
            dgrLog.FormatAsStringCenter("logEmpName", "logEmp");
            dgrLog.Dgr.Columns["logParam"].Visible = false;
        }
        private void dgrSales_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int saleCode = dgrSales.ConvertToInt(dgrSales.Dgr.CurrentRow.Cells["saleCode"].Value);
            SalesRegist salesRegist = new SalesRegist();
            salesRegist.GetSaleCode(saleCode);
            cLog.InsertEmpAccessLogNotConnect("@customerSaleSearch", accessedEmp, saleCode);
            salesRegist.StartPosition = FormStartPosition.CenterParent;
            salesRegist.ShowDialog();
        }
        private void PurSearchQuery()
        {
            DataTable resultData = new DataTable();
            string fromDate = dtpPurchaserDateFrom.Value.ToString("yyyy-MM-dd");
            string toDate = dtpPurchaseDateTo.Value.AddDays(1).ToString("yyyy-MM-dd");
            string query = "SELECT pur_date, pur_type,purd_qty, purd_amount,purd_bprice,sup_name " +
                "FROM purchase,purdetail,supplier " +
                $"WHERE pur_code = purd_code AND pur_sup = sup_code AND purd_pdt = {pdtCode} AND pur_date > '{fromDate}' AND pur_date < '{toDate}'";
            dbconn.SqlDataAdapterQuery(query, resultData);
            PurchaseGridFill(resultData);
        }
        private void PurchaseGridFill(DataTable dataTable)
        {
            dgrPurchase.Dgr.Rows.Clear();
            foreach (DataRow rusltRow in dataTable.Rows)
            {
                var rowIndex = dgrPurchase.Dgr.Rows.Add();
                var row = dgrPurchase.Dgr.Rows[rowIndex];
                row.Cells["No"].Value = rowIndex + 1;
                row.Cells["purDate"].Value = Convert.ToDateTime(rusltRow["pur_date"]).ToString("yyyy-MM-dd HH:mm");
                row.Cells["purType"].Value = cStatusCode.GetPurchaseType(Convert.ToInt32(rusltRow["pur_type"]));
                row.Cells["purQty"].Value = Convert.ToInt32(rusltRow["purd_qty"]);
                row.Cells["purAmount"].Value = Convert.ToInt32(rusltRow["purd_amount"]);
                row.Cells["purBprice"].Value = Convert.ToDecimal(rusltRow["purd_bprice"]);
                row.Cells["purSupplier"].Value = rusltRow["sup_name"].ToString();
            }
        }
        private void SaleSearchQuery()
        {
            DataTable resultData = new DataTable();
            string fromDate = dtpOrderFrom.Value.ToString("yyyy-MM-dd");
            string toDate = dtpOrderTo.Value.AddDays(1).ToString("yyyy-MM-dd");
            string query = "SELECT sale_date, sale_type, saled_amount_krw,saled_sprice_krw,saled_bprice,saled_qty,sale_cust,sale_code " +
                "FROM sales,saledetail " +
                $"WHERE sale_code = saled_code  AND saled_pdt = {pdtCode} AND sale_date > '{fromDate}' AND sale_date < '{toDate}'";
            dbconn.SqlDataAdapterQuery(query, resultData);
            SalesGridFill(resultData);
        }
        private void SalesGridFill(DataTable dataTable)
        {
            dgrSales.Dgr.Rows.Clear();
            foreach (DataRow resultRow in dataTable.Rows)
            {
                var rowIndex = dgrSales.Dgr.Rows.Add();
                var row = dgrSales.Dgr.Rows[rowIndex];
                object resultObj = new object();
                int custCode = Convert.ToInt32(resultRow["sale_cust"]);
                string custName = "";
                if (custCode != 0)
                {
                    string query = $"SELECT cust_name FROM cust WHERE cust_code = {Convert.ToInt32(resultRow["sale_cust"])}";
                    dbconn.sqlScalaQuery(query, out resultObj);
                    custName = resultObj.ToString();
                }
                row.Cells["No"].Value = rowIndex + 1;
                row.Cells["saleDate"].Value = Convert.ToDateTime(resultRow["sale_date"]).ToString("yyyy-MM-dd HH:mm");
                row.Cells["saleType"].Value = cStatusCode.GetSaleType(Convert.ToInt32(resultRow["sale_type"]));
                row.Cells["saleQty"].Value = Convert.ToInt32(resultRow["saled_qty"]);
                row.Cells["saleAmount"].Value = Convert.ToInt32(resultRow["saled_amount_krw"]);
                row.Cells["saleSprice"].Value = Convert.ToInt32(resultRow["saled_sprice_krw"]);
                row.Cells["saleBprice"].Value = Convert.ToDecimal(resultRow["saled_bprice"]);
                row.Cells["saleCustomer"].Value = custName;
                row.Cells["saleCode"].Value = Convert.ToInt32(resultRow["sale_code"]);

            }
        }
        private void LogGridFill(DataTable dataTable)
        {
            //작업내역 설정
            foreach (var entry in cLog.logParameter)
            {
                if (entry.Value.typeCode >= 100 && entry.Value.typeCode < 200)
                {
                    parameter[entry.Key] = entry.Value;
                }
            }
            //그리드 초기화 후 조회된 내역으로 데이터 그리드에 내용 채우기
            dgrLog.Dgr.Rows.Clear();
            foreach (DataRow row in dataTable.Rows)
            {
                DataTable readData = new DataTable();
                object resultObj = new object();
                // 작업자 명 확인
                string query = $"SELECT emp_name FROM employee WHERE emp_code = {row["pdtlog_emp"]}";
                dbconn.sqlScalaQuery(query, out resultObj);

                
                string empName = resultObj.ToString();
                // 로그 데이터 설정
                string before = row["pdtlog_before"].ToString();
                string after = row["pdtlog_after"].ToString();
                switch (Convert.ToInt32(row["pdtlog_type"]))
                {
                    case 103: //공급사 수정
                        query = $"SELECT sup_name FROM supplier WHERE sup_code = {before}";
                        dbconn.sqlScalaQuery(query, out resultObj);
                        before = resultObj.ToString();
                        query = $"SELECT sup_name FROM supplier WHERE sup_code = {after}";
                        dbconn.sqlScalaQuery(query, out resultObj);
                        after = resultObj.ToString();
                        break;
                    case 104: // 분류 수정
                        string[] beforeParts = before.Split('_');
                        int beforeTop = Convert.ToInt32(beforeParts[0]);
                        int beforeMid = Convert.ToInt32(beforeParts[1]);
                        int beforeBot = Convert.ToInt32(beforeParts[2]);

                        // pdtlog_after를 "1_1_1" 형식으로 분리
                        string[] afterParts = after.Split('_');
                        int afterTop = Convert.ToInt32(afterParts[0]);
                        int afterMid = Convert.ToInt32(afterParts[1]);
                        int afterBot = Convert.ToInt32(afterParts[2]);


                        query = $"SELECT cat_name_kr FROM category WHERE cat_top = {beforeTop}";
                        dbconn.sqlScalaQuery(query, out resultObj);
                        before = resultObj.ToString();
                        query = $"SELECT cat_name_kr FROM category WHERE cat_top = {beforeTop} AND cat_mid ={beforeMid}";
                        dbconn.sqlScalaQuery(query, out resultObj);
                        before += "▶" + resultObj.ToString();
                        query = $"SELECT cat_name_kr FROM category WHERE cat_top = {beforeTop} AND cat_mid ={beforeMid} AND cat_bot = {beforeBot}";
                        dbconn.sqlScalaQuery(query, out resultObj);
                        before += "▶" + resultObj.ToString();

                        query = $"SELECT cat_name_kr FROM category WHERE cat_top = {afterTop}";
                        dbconn.sqlScalaQuery(query, out resultObj);
                        after = resultObj.ToString();
                        query = $"SELECT cat_name_kr FROM category WHERE cat_top = {afterTop} AND cat_mid ={afterMid}";
                        dbconn.sqlScalaQuery(query, out resultObj);
                        after += "▶" + resultObj.ToString();
                        query = $"SELECT cat_name_kr FROM category WHERE cat_top = {afterTop} AND cat_mid ={afterMid} AND cat_bot = {afterBot}";
                        dbconn.sqlScalaQuery(query, out resultObj);
                        after += "▶" + resultObj.ToString();
                        break;
                    case 105: //상태 변경
                        before = cStatusCode.GetProductStatus(Convert.ToInt32(before));
                        after = cStatusCode.GetProductStatus(Convert.ToInt32(after));
                        break;
                    case 113: // 과면세 변경
                        before = cStatusCode.GetTaxStatus(Convert.ToInt32(before));
                        after = cStatusCode.GetTaxStatus(Convert.ToInt32(after));
                        break;
                }

                string empCode = row["pdtlog_emp"].ToString();
                string logDate = Convert.ToDateTime(row["pdtlog_date"]).ToString("yyyy-MM-dd HH:mm");
                string logType = "";

                var typeInfo = parameter.Values.FirstOrDefault(x => x.typeCode == (int)row["pdtlog_type"]);
                if (typeInfo.typeString != null)
                {
                    logType = typeInfo.typeString;  // 해당 typeString 값을 사용
                }
                int addRow = dgrLog.Dgr.Rows.Add();
                // 해당 셀에 값 설정
                dgrLog.Dgr.Rows[addRow].Cells["No"].Value = addRow + 1;
                dgrLog.Dgr.Rows[addRow].Cells["logType"].Value = logType;  // 여기서 값을 설정
                dgrLog.Dgr.Rows[addRow].Cells["logBefore"].Value = before;
                dgrLog.Dgr.Rows[addRow].Cells["logAfter"].Value = after;
                dgrLog.Dgr.Rows[addRow].Cells["logEmpName"].Value = empName;
                dgrLog.Dgr.Rows[addRow].Cells["logEmp"].Value = empCode;
                dgrLog.Dgr.Rows[addRow].Cells["logDate"].Value = logDate;
            }
        }
        private void LogSearchQuery()
        {
            string fromDate = dtpLogkFrom.Value.ToString("yyyy-MM-dd");
            string toDate = dtpLogTo.Value.AddDays(1).ToString("yyyy-MM-dd");
            DataTable resultData = new DataTable();
            string query = $"SELECT pdtlog_type, pdtlog_before, pdtlog_after, pdtlog_param, pdtlog_emp, pdtlog_date FROM productlog WHERE pdtlog_param = {pdtCode} AND pdtlog_date > '{fromDate}' AND pdtlog_date < '{toDate}'";
            query += "ORDER BY pdtlog_date";
            resultData.Rows.Clear();
            dbconn.SqlDataAdapterQuery(query, resultData);
            LogGridFill(resultData);
        }
        private void bntCancle_Click(object sender, EventArgs e)
        {
            Close();
        }
        public void GetCategory(int pdtTop, int pdtMid, int pdtBot)
        {
            this.pdtTop = pdtTop;
            this.pdtMid = pdtMid;
            this.pdtBot = pdtBot;
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            GetSupplier();
        }

        private void bntCancle_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SqlConnection connection = dbconn.Opensql();
            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                PdtErrorCheck();
                if(errerCheck == true)
                {
                    return;
                }
                dbconn.NonQuery("BEGIN TRAN", connection, transaction);// BEGIN TRAN 선언
                if (pdtCode == 0)
                {
                    InsertProduct(connection, transaction);
                    cLog.InsertEmpAccessLogConnect("@pdtRegisted", accessedEmp, pdtCode, connection, transaction);
                }
                else
                {
                    ModifyProduct(connection, transaction);
                    cLog.InsertEmpAccessLogConnect("@pdtModify", accessedEmp, pdtCode, connection, transaction);
                }

                dbconn.NonQuery("COMMIT TRAN", connection, transaction);
                transaction.Commit();
                DialogResult = DialogResult.OK;
                GetPdtCode?.Invoke(pdtCode);
                Close();
            }
            catch (Exception ex)
            {
                dbconn.NonQuery("ROLLBACK TRAN", connection, transaction);
                transaction.Rollback();
                MessageBox.Show(ex.Message);
            }
        }
        
        private void btnPurSearch_Click(object sender, EventArgs e)
        {
            PurSearchQuery();
        }

        private void btnSaleSearch_Click(object sender, EventArgs e)
        {
            SaleSearchQuery();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            CategoryBoard categoryBoard = new CategoryBoard();
            categoryBoard.GetWorkType(0);
            categoryBoard.CategorySelected += (top, mid, bot) => { GetCategory(top, mid, bot); };
            categoryBoard.ShowDialog();
            GetCategoryInfo(pdtTop, pdtMid, pdtBot);
        }

        private void btnLogSearch_Click(object sender, EventArgs e)
        {
            LogSearchQuery();
        }
    }
}
