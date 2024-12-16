using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRMS
{
    class cStatusCode
    {
        // 회원 상태 코드와 문자열을 저장할 딕셔너리
        public static readonly Dictionary<int, string> CustomerStatus = new Dictionary<int, string>()
        {
            { 0, "무효" },
            { 1, "유효" }
        };

        // 전표 유형 코드와 문자열을 저장할 딕셔너리
        public static readonly Dictionary<int, string> purchaseType = new Dictionary<int, string>()
        {
            { 1, "매입" },
            { 2, "반품" }
        };

        // 발주 전표 상태 코드와 문자열을 저장할 딕셔너리
        public static readonly Dictionary<int, string> PurchaseOrderStatus = new Dictionary<int, string>()
        {
            { 0, "발주취소" },
            { 1, "입고대기" },
            { 2, "입고완료" }
        };

        // 제품 상태 코드와 문자열을 저장할 딕셔너리
        public static readonly Dictionary<int, string> ProductStatus = new Dictionary<int, string>()
        {

            { 1, "판매가능" },
            { 2, "품절" },
            { 3, "단종" },
            { 4, "취급 외" }
        };

        // 세금 상태 코드와 문자열을 저장할 딕셔너리
        public static readonly Dictionary<int, string> TaxStatus = new Dictionary<int, string>()
        {
            { 0, "면세" },
            { 1, "과세" }
        };

        // 주문서 상태 코드와 문자열을 저장할 딕셔너리
        public static readonly Dictionary<int, string> CustomerOrderStatus = new Dictionary<int, string>()
        {
            { 0, "취소" },
            { 1, "주문" },
            { 2, "판매" }
        };
        // 매출결제 유형
        public static readonly Dictionary<int, string> SalePaymentType = new Dictionary<int, string>()
        {
            { 0, "현금" },
            { 1, "계좌이체" },
            { 2, "PG" },
            { 3, "포인트" }
        };
        // 판매 유형
        public static readonly Dictionary<int, string> SaleType = new Dictionary<int, string>()
        {
            { 0, "반품" },
            { 1, "판매" }
        };
        // 직원 상태
        public static readonly Dictionary<int, string> EmployeeStatus = new Dictionary<int, string>()
        {
            {0, "퇴사" },
            {1, "근무" },
            {2, "휴직" }
        };
        // 직원 권한 
        public static readonly Dictionary<int, string> EmployeePermission = new Dictionary<int, string>()
        {
            {101, "제품 조회" },
            {102, "제품 등록/수정" },
            {103, "제품 인쇄" },
            {104, "제품 엑셀" },

            {121, "분류 조회" },
            {122, "분류 등록/수정" },

            {131, "공급사 조회" },
            {132, "공급사 등록/수정" },
            {133, "공급사 인쇄" },
            {134, "공급사 엑셀" },

            {201, "매입발주 조회" },
            {202, "매입발주 등록/수정" },
            {203, "매입발주 인쇄" },
            {204, "매입발주 엑셀" },

            {221, "결제 조회" },
            {222, "결제 등록/수정" },
            {223, "결제 인쇄" },
            {224, "결제 엑셀" },

            {301, "주문서 조회" },
            {302, "주문서 등록/수정" },
            {303, "주문서 인쇄" },
            {304, "주문서 엑셀" },

            {401, "회원 조회" },
            {402, "회원 등록/수정" },
            {403, "회원 인쇄" },
            {404, "회원 엑셀" }
        };
        public static readonly Dictionary<int, string> SupplierStatus = new Dictionary<int, string>()
        {
            {0, "무효" },
            {1, "정상" }
        };
        // 공급사 결제 유형
        public static readonly Dictionary<int, string> SupplierPayment = new Dictionary<int, string>()
        {
           {0,"현금" },
           {1,"계좌이체" },
           {2,"신용카드" },
           {3,"어음" },
        };
        // 상태 문자열을 가져오는 메서드
        public static string GetCustomerStatus(int statusCode)
        {
            return GetStatusString(CustomerStatus, statusCode);
        }

        public static string GetPurchaseType(int typeCode)
        {
            return GetStatusString(purchaseType, typeCode);
        }

        public static string GetPurchaseOrderStatus(int statusCode)
        {
            return GetStatusString(PurchaseOrderStatus, statusCode);
        }

        public static string GetProductStatus(int statusCode)
        {
            return GetStatusString(ProductStatus, statusCode);
        }

        public static string GetTaxStatus(int statusCode)
        {
            return GetStatusString(TaxStatus, statusCode);
        }

        public static string GetCustomerOrderStatus(int statusCode)
        {
            return GetStatusString(CustomerOrderStatus, statusCode);
        }

        public static string GetCustomerSalePaynetType(int statusCode)
        {
            return GetStatusString(SalePaymentType, statusCode);
        }
        public static string GetSaleType(int statusCode)
        {
            return GetStatusString(SaleType, statusCode);
        }
        public static string GetEmployeeStatus(int statusCode)
        {
            return GetStatusString(EmployeeStatus, statusCode);
        }
        public static string GetSupplierStatus(int stausCode)
        {
            return GetStatusString(SupplierStatus, stausCode);
        }
        public static string GetSupplierPayment(int statusCode)
        {
            return GetStatusString(SupplierPayment, statusCode);
        }
        public static string GetEmployeePermission(int statuscode)
        {
            return GetStatusString(EmployeePermission, statuscode);
        }
        // 내부 메서드: 상태 딕셔너리에서 문자열을 조회
        private static string GetStatusString(Dictionary<int, string> statusDictionary, int code)
        {
            if (statusDictionary.TryGetValue(code, out string statusText))
            {
                return statusText;
            }
            return "알 수 없음"; // 해당 코드가 없는 경우
        }

        // 상태이름으로 상태코드 반환
        public static int GetCustomerStatusCode(string status)
        {
            return GetStatusCode(CustomerStatus, status);
        }

        public static int GetPurchaseTypeCode(string status)
        {
            return GetStatusCode(purchaseType, status);
        }

        public static int GetPurchaseOrderStatusCode(string status)
        {
            return GetStatusCode(PurchaseOrderStatus, status);
        }

        public static int GetProductStatusCode(string status)
        {
            return GetStatusCode(ProductStatus, status);
        }

        public static int GetTaxStatusCode(string status)
        {
            return GetStatusCode(TaxStatus, status);
        }

        public static int GetCustomerOrderStatusCode(string status)
        {
            return GetStatusCode(CustomerOrderStatus, status);
        }

        public static int GetCustomerSalePaynetTypeCode(string status)
        {
            return GetStatusCode(SalePaymentType, status);
        }
        public static int GetSaleTypeCode(string status)
        {
            return GetStatusCode(SaleType, status);
        }
        public static int GetEmployeeCode(string status)
        {
            return GetStatusCode(EmployeeStatus, status);
        }
        public static int GetSuppliercode(string status)
        {
            return GetStatusCode(SupplierStatus, status);
        }
        public static int GetSupplierPaymentCode(string status)
        {
            return GetStatusCode(SupplierPayment, status);
        }
        public static int GetEmployeePermissionCode(string status)
        {
            return GetStatusCode(EmployeePermission, status);
        }
        private static int GetStatusCode(Dictionary<int, string> statusDictionary, string statusName)
        {
            foreach (var kvp in statusDictionary)
            {
                if (kvp.Value == statusName)
                {
                    return kvp.Key;
                }
            }
            return -1; // 일치하는 상태가 없는 경우 -1 반환 (또는 다른 적절한 기본값)
        }
    }
}
