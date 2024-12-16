using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace BRMS
{
    class cLog
    {
        
        public static Dictionary<string , (int typeCode, string typeString)> logParameter = new Dictionary<string, (int, string)>
        {
            //상품등록정보100
            ["@pdtName_kr"] =       (101, "제품명(한글)"),
            ["@pdtName_en"] =       (102, "제품명(영문)"),
            ["@pdtSup"] =           (103, "공급사"),
            ["@pdtCat"] =           (104, "분류"),
            ["@pdtStatus"] =        (105, "상태"),
            ["@pdtBprice"] =        (106, "매입단가"),
            ["@pdtSprice_krw"] =    (107, "판매단가(한화)"),
            ["@pdtSprice_usd"] =    (118, "판매단가(미화)"),
            ["@pdtWeight"] =        (119, "무게"),
            ["@pdtWidth"] =         (110, "넓이"),
            ["@pdtLength"] =        (111, "길이"),
            ["@pdtHeight"] =        (112, "높이"),
            ["@pdtTax"] =           (113, "과면세"),
            //분류정보 변경150
            ["@catTopName"] =       (151, "대분류명"),
            ["@catMidName"] =       (152, "중분류명"),
            ["@catBotName"] =       (153, "소분류명"),
            //공급사등록정보 200
            ["@supName"] =           (201, "공급사명"),
            ["@supBzNo"] =           (202, "사업자 번호"),
            ["@supBzType"] =         (203, "업종"),
            ["@supIndustry"] =       (204, "업태"),
            ["@supCeoName"] =        (205, "대표자명"),
            ["@supTel"] =            (206, "공급사 전화"),
            ["@supCel"] =            (207, "담당자 휴대폰"),
            ["@supFax"] =            (208, "공급사 팩스"),
            ["@supEMail"] =          (209, "이메일"),
            ["@supUrl"] =            (210, "홈페이지"),
            ["@supMemo"] =           (211, "비고"),
            ["@supBank"] =           (212, "은행"),
            ["@supAccount"] =        (213, "계좌번호"),
            ["@supDepasitor"] =      (214, "예금주"),
            ["@supStatus"] =         (215, "상태"),
            ["@supAddress"] =        (216, "주소"),
            ["@supPayType"] =        (217, "결제유형"),


            //매입내역 300
            ["@purDate"] =          (301, "매입일"),
            ["@supplierCode"] =     (302, "매입공급사"),
            ["@purchaseAmount"] =   (303, "매입액"),
            ["@purPayment"] =       (304, "매입결제액"),
            ["@purType"] =          (305, "매입유형"),
            ["@purchaseNote"] =     (306, "매입 비고"),
            ["@purchaseTaxable"] =  (307, "매입 과세액"),
            ["@purchaseTaxfree"] =  (308, "매입 면세액"),

            ["@purdQty"] =          (312, "제품 매입량"),
            ["@purdBprice"] =       (313, "제품 매입단가"),
            ["@purdAmount"] =       (314, "제품 매입액"),
            ["@purdStatus"] =       (315, "제품 매입상태"),
            ["@purdAdd"] =          (316, "제품 추가"),
            //발주내역 400
            ["pordSup"] =           (401, "발주 공급사"),
            ["pordArrivla"] =       (402, "발주 입고예정일"),
            ["pordAmount"] =        (403, "발주 금액"),
            ["pordNote"] =          (404, "발주 메모"),
            ["pordStatus"] =        (405, "발주 상태"),

            ["@porddPdt"] =         (411, "발주 제품"),
            ["@porddQty"] =         (412, "발주량"),
            ["@porddBprice"] =      (413, "발주 매입단가"),
            ["@porddSprice"] =      (414, "발주 판매단가"),
            ["@porddAmount"] =      (415, "발주액"),
            ["@porddStatus"] =      (416, "발주서 제품 상태"),
            ["@porddAdd"] =         (417, "제품 추가"),
            //결재내역 500
            ["@paySup"] =           (501, "결제 공급사"),
            ["@payCash"] =          (502, "현금 결제액"),
            ["@payTransfer"] =      (503, "계좌이체액"),
            ["@payCredit"] =        (504, "카드 결제액"),
            ["@payNote"] =          (505, "어음 결제액"),
            ["@payDc"] =            (506, "할인"),
            ["@payCoupone"] =       (507, "쿠폰"),
            ["@paySupsiby"] =       (508, "장려금"),
            ["@payEtc"] =           (509, "기타 결제액"),
            ["@payBank"] =          (510, "은행"),
            ["@payAccount"] =       (511, "계좌번호"),
            ["@payDepasitor"] =     (512, "예금주"),
            ["@payMemo"] =          (513, "메모"),
            ["@payDate"] =          (514, "결제일"),
            ["@payStatus"] =        (515, "상태"),

            //주문서 600
            //회원등록정보 700
            ["@custName"] = (701, "회원명"),
            ["@custTell"] = (702, "전화번호"),
            ["@custCell"] = (703, "휴대전화번호"),
            ["@custEmail"] = (704, "이메일"),
            ["@custAddr"] = (705, "주소"),
            ["@custCountry"] = (706, "국가"),
            ["@custStatus"] = (707, "상태"),
            ["@custMemo"] = (708, "메모"),
            //직원내역 800
            ["@empLoging"] = (801, "로그인일시"),
            ["@empName"] = (802, "직원명"),
            ["@empCell"] = (803, "연락처"),
            ["@empAddr"] = (804, "주소"),
            ["@empEmail"] = (805, "이메일"),
            ["@empLevel"] = (806, "직급"),
            ["@empPemission"] = (807, "권한"),
            ["@empPassword"] = (808, "비밀번호"),
            ["@empStatus"] = (809, "상태"),
            ["@empMemo"] = (810, "메모"),
            //직원작업 900
            ["@pdtSearch"] =            (901, "상품 조회"),
            ["@pdtRegisted"] =          (902, "상품 등록"),
            ["@pdtPurSearch"] =         (903, "상품 매입 조회"),
            ["@pdtSaelSearch"] =        (904, "상품 판매 조회"),
            ["@pdtLogSearch"] =         (905, "상품 변경내역 조회"),
            ["@pdtStockSearch"] =       (906, "상품 수불내역 조회"),
            ["@pdtModify"] =            (907, "상품 수정"),
            ["@purSearch"] =            (908, "매입 조회"),
            ["@purModify"] =            (909, "매입 수정"),
            ["@purOrderSearch"] =       (910, "발주 조회"),
            ["@purOrderModify"] =       (911, "발주 수정"),
            ["@paymentSearch"] =        (912, "결제 조회"),
            ["@paymentModify"] =        (913, "결제 수정"),
            ["@custOrderSearch"] =      (914, "주문서 조회"),
            ["@custOrderModify"] =      (915, "주문서 삭제"),
            ["@salesSearch"] =          (916, "판매 내역 조회"),
            ["@salesReportSearch"] =    (917, "판매 조회"),
            ["@customerSearch"] =       (918, "고객 조회"),
            ["@customerRegisted"] =     (919, "고객 등록"),
            ["@customerModify"] =       (920, "고객 수정"),
            ["@customerOrderSearch"] =  (921, "고객 주문 조회"),
            ["@customerSaleSearch"] =   (922, "고객 판매 조회"),
            ["@customerLogSearch"] =    (622, "고객 변경로그 조회"),
            ["@supplierSearch"]=        (623, "공급사 조회"),
            ["@employeeSearch"] =       (940, "직원 조회"),
            ["@employeeRegisted"] =     (941, "직원 등록"),
            ["@employeeModify"] =       (942, "직원 수정"),
            ["@pdtLogSearch"] =         (950, "제품 로그 조회"),
            ["@empLogSearch"] =         (951, "직원 로그 조회"),
            ["@supplierLogSearch"] =    (952, "공급사 로그 조회"),
            ["@purchaseLogSearch"] =    (953, "매입/발주 로그 조회"),
            ["@paymentLogSearch"] =     (954, "결제 로그 조회"),
            ["@customerLogSearch"] =    (955, "회원 로그 조회"),
            ["@payDetailSearch"] =      (924, "결제상세 조회"),
            ["@purRegisted"]=           (925, "매입 등록"),
            ["@purOrderRegisted"] =     (926, "발주 등록")
        };
        public static  void IsModified(Dictionary<string, object> originalValues, Dictionary<string, string> modifiedValues, int param, int accessedEmp, SqlConnection connection, SqlTransaction transaction)
        {
            //딕셔너리를 받아온다
            //받아온 딕셔너리의 키와 originalValues의 키를 통해 두 딕셔너리의 값에 차이가 있는지 확인한다
            //만약 변경이 있다면 cLogManager 클래스의 public void CreatLog()에 키, 변경전 , 변경후 데이터를 전송한다

            foreach (var modifiedItem in modifiedValues)
            {
                // 변경된 값이 originalValues와 다를 경우
                if (originalValues.ContainsKey(modifiedItem.Key) && originalValues[modifiedItem.Key].ToString() != modifiedItem.Value)
                {
                    // 변경사항을 로그로 기록
                    InsertLogToDatabase(modifiedItem.Key, originalValues[modifiedItem.Key].ToString(), modifiedItem.Value, param, accessedEmp, connection, transaction);
                }
            }
        }
        public static void InsertDetailedLogToDatabase(string key, string before, string after, int parameter, int parameter2, int orderType, int empCode, SqlConnection connection, SqlTransaction transaction)
        {
            //SettupParametr();
            var logData = logParameter[key];
            int type = logData.typeCode;
            cDatabaseConnect dbconn = new cDatabaseConnect();
            string query = null;

            switch (type.ToString().Substring(0, 1))
            {
                case "3":
                    query = "INSERT INTO purchaselog (purlog_type, purlog_order, purlog_before,purlog_after,purlog_param,purlog_param2, purlog_emp,purlog_date)"+
                        " VALUES(@type,@orderType,@before,@after,@param,@param2,@empCode,GETDATE())"; 
                    break;
                case "4":
                    query = "INSERT INTO purchaselog (purlog_type, purlog_order, purlog_before,purlog_after,purlog_param,purlog_param2, purlog_emp,purlog_date)"+
                        " VALUES(@type,@orderType,@before,@after,@param,@param2,@empCode,GETDATE())";
                    break;
                case "6":
                    query = "INSERT INTO custorderlog (cordlog_type, cordlog_before, cordlog_after, cordlog_param, cordlog_emp, cordlog_date)";
                    break;
                case "8":
                    query = "INSERT INTO emplog(emplog_type, emplog_before, emplog_after, emplog_param, emplog_param2, emplog_emp, emplog_date)"+
                        " VALUES(@type,@before,@after,@param,@param2,@empCode,GETDATE())"; ;
                    break;
            }
            //query += " VALUES(@type,@orderType,@before,@after,@param,@param2,@empCode,GETDATE())";
            SqlParameter[] sqlParameter =
            {
                //new SqlParameter("@logCode",SqlDbType.Int){Value = Convert.ToInt32(resultObj) },
                new SqlParameter("@param",SqlDbType.Int){Value = parameter},
                new SqlParameter("@param2",SqlDbType.Int){Value = parameter2},
                new SqlParameter("@ordertype",SqlDbType.Int){Value = orderType},
                new SqlParameter("@type",SqlDbType.Int){Value = type},
                new SqlParameter("@before",SqlDbType.NVarChar){Value = before},
                new SqlParameter("@after",SqlDbType.NVarChar){Value = after},
                new SqlParameter("@empCode",SqlDbType.Int){Value = empCode}
            };
            dbconn.ExecuteNonQuery(query, connection, transaction, sqlParameter);
        }
        public static void InsertLogToDatabase(string key, string before, string after, int parameter, int empCode, SqlConnection connection, SqlTransaction transaction)
        {
            //SettupParametr();
            var logData = logParameter[key];
            int type = logData.typeCode;
            cDatabaseConnect dbconn = new cDatabaseConnect();
            string query = null ;
  
            switch(type.ToString().Substring(0,1))
            {
                case "1":
                    query = "INSERT INTO productlog (pdtlog_type,pdtlog_before,pdtlog_after,pdtlog_param,pdtlog_emp,pdtlog_date)";
                    break;
                case "2":
                    query = "INSERT INTO supplierlog (suplog_type,suplog_before,suplog_after,suplog_param,suplog_emp,suplog_date)";
                    break;
                case "3":
                    query = "INSERT INTO purchaselog (purlog_type,purlog_before,purlog_after,purlog_param,purlog_param2, purlog_emp,purlog_date)";
                    break;
                case "4":
                    query = "INSERT INTO purchaselog (purlog_type,purlog_before,purlog_after,purlog_param,purlog_emp,purlog_date)";
                    break;
                case "5":
                    query = "INSERT INTO paymentlog (paylog_type,paylog_before,paylog_after,paylog_param,paylog_emp,paylog_date)";
                    break;
                case "6":
                    query = "INSERT INTO custorderlog (cordlog_type,cordlog_before,cordlog_after,cordlog_param,cordlog_emp,cordlog_date)";
                    break;
                case "7":
                    query = "INSERT INTO customerlog (custlog_type,custlog_before,custlog_after,custlog_param,custlog_emp,custlog_date)";
                    break;
                case "8":
                    query = "INSERT INTO emplog (emplog_type,emplog_before,emplog_after,emplog_param,emplog_emp,emplog_date)";
                    break;
            }
            query += " VALUES(@type,@before,@after,@param,@empCode,GETDATE())";
            SqlParameter[] sqlParameter =
            {
                //new SqlParameter("@logCode",SqlDbType.Int){Value = Convert.ToInt32(resultObj) },
                new SqlParameter("@param",SqlDbType.Int){Value = parameter},
                new SqlParameter("@type",SqlDbType.Int){Value = type},
                new SqlParameter("@before",SqlDbType.NVarChar){Value = before},
                new SqlParameter("@after",SqlDbType.NVarChar){Value = after},
                new SqlParameter("@empCode",SqlDbType.Int){Value = empCode}
            };
            dbconn.ExecuteNonQuery(query, connection, transaction, sqlParameter);
        }
        public static void InsertEmpAccessLogNotConnect(string key, int empCode, int parameter)
        {

            var logDate = logParameter[key];
            int type = logDate.typeCode;
            cDatabaseConnect dbconn = new cDatabaseConnect();
            string query = "INSERT INTO accesslog(acslog_type, acslog_emp, acslog_param, acslog_date)" +
                "VALUES(@logType, @emp, @parma, GETDATE())";

            SqlParameter[] sqlParameter =
            {
                new SqlParameter("@logType",SqlDbType.Int){Value = type},
                new SqlParameter("@emp",SqlDbType.Int){Value = empCode},
                new SqlParameter("@parma",SqlDbType.Int){Value = parameter}
            };
            using (SqlConnection connection = dbconn.Opensql())
            {
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    dbconn.ExecuteNonQuery(query, connection, transaction, sqlParameter);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    cUIManager.ShowMessageBox(ex.Message,"알림",System.Windows.Forms.MessageBoxButtons.OK);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public static void InsertEmpAccessLogConnect(string key, int empCode, int parameter,SqlConnection connection, SqlTransaction transaction)
        {

            var logDate = logParameter[key];
            int type = logDate.typeCode;
            cDatabaseConnect dbconn = new cDatabaseConnect();
            string query = "INSERT INTO accesslog(acslog_type, acslog_emp, acslog_param, acslog_date)" +
                "VALUES(@logType, @emp, @parma, GETDATE())";

            SqlParameter[] sqlParameter =
            {
                new SqlParameter("@logType",SqlDbType.Int){Value = type},
                new SqlParameter("@emp",SqlDbType.Int){Value = empCode},
                new SqlParameter("@parma",SqlDbType.Int){Value = parameter}
            };
            dbconn.ExecuteNonQuery(query, connection, transaction, sqlParameter);
        }
        public static string GetTypeStringByTypeCode(int typeCode)
        {
            foreach (var kvp in logParameter)
            {
                if (kvp.Value.typeCode == typeCode)
                {
                    return kvp.Value.typeString; // typeCode에 해당하는 typeString 반환
                }
            }
            return null;
        }
        public static string GetProductInfo(int code, out string resultString)
        {
            if (code == 0)
            {
                resultString = "";
                return resultString;
            }
            cDatabaseConnect dbconn = new cDatabaseConnect();
            string query = $"SELECT pdt_name_kr, pdt_number FROM product WHERE pdt_code = {code}";
            DataTable dataTable = new DataTable();
            dbconn.SqlReaderQuery(query, dataTable);
            DataRow dataRow = dataTable.Rows[0];
            string name = dataRow[0].ToString().Trim();
            string number = dataRow[1].ToString().Trim();
            resultString = $"{name}({number})";
            return resultString;
        }
        public static string GetPurchaseSupplierInfo(int code, out string resultString)
        {
            int supplierCode = 0;
            if(code == 0)
            {
                resultString = "";
                return resultString;
            }
            cDatabaseConnect dbconn = new cDatabaseConnect();
            string query = $"SELECT pur_sup FROM purchase FROM pur_code = {code}";
            object resultObj = new object();
            dbconn.sqlScalaQuery(query, out resultObj);
            supplierCode = Convert.ToInt32(resultObj);
            GetSupplierInfo(code, out resultString);
            return resultString;
        }
        public static string GetPurOrderSupplierInfo(int code, out string resultString)
        {
            int supplierCode = 0;
            if (code == 0)
            {
                resultString = "";
                return resultString;
            }
            cDatabaseConnect dbconn = new cDatabaseConnect();
            string query = $"SELECT pord_sup FROM purorder FROM pord_code = {code}";
            object resultObj = new object();
            dbconn.sqlScalaQuery(query, out resultObj);
            supplierCode = Convert.ToInt32(resultObj);
            GetSupplierInfo(code, out resultString);
            return resultString;
        }
        public static string GetPaymentSupplierInfo(int code, out string resultString)
        {
            int supplierCode = 0;
            if (code == 0)
            {
                resultString = "";
                return resultString;
            }
            cDatabaseConnect dbconn = new cDatabaseConnect();
            string query = $"SELECT pay_sup FROM payment FROM pay_code = {code}";
            object resultObj = new object();
            dbconn.sqlScalaQuery(query, out resultObj);
            supplierCode = Convert.ToInt32(resultObj);
            GetSupplierInfo(code, out resultString);
            return resultString;
        }
        public static string GetSupplierInfo(int code, out string resultString)
        {
            if(code == 0)
            {
                resultString = "";
                return resultString;
            }
            cDatabaseConnect dbconn = new cDatabaseConnect();
            string query = $"SELECT sup_name FROM supplier WHERE sup_code = {code}";
            object resultObj = new object();
            dbconn.sqlScalaQuery(query, out resultObj);            
            string name = resultObj.ToString().Trim();
            resultString = $"{name}({code})";
            return resultString;
        }
        public static string GetCustomerInfo(int code, out string resultString)
        {
            if (code == 0)
            {
                resultString = "";
                return resultString;
            }
            cDatabaseConnect dbconn = new cDatabaseConnect();
            string query = $"SELECT cust_name FROM customer WHERE cust_code = {code}";
            object resultObj = new object();
            dbconn.sqlScalaQuery(query, out resultObj);
            string name = resultObj.ToString().Trim();
            resultString = $"{name}({code})";
            return resultString;
        }
        public static string GetEmployeeInfo(int code, out string resultString)
        {
            if (code == 0)
            {
                resultString = "";
                return resultString;
            }
            cDatabaseConnect dbconn = new cDatabaseConnect();
            string query = $"SELECT emp_name FROM employee WHERE emp_code = {code}";
            object resultObj = new object();
            dbconn.sqlScalaQuery(query, out resultObj);
            string name = resultObj.ToString().Trim();
            resultString = $"{name}({code})";
            return resultString;
        }
    }
}
