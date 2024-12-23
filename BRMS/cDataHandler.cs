using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BRMS
{
    class cDataHandler
    {
        /// <summary>
        /// 문자열 값을 정수로 변환 (콤마 제거 및 null/빈 값 처리 포함)
        /// </summary>
        /// <param name="input">변환할 문자열</param>
        /// <returns>변환된 정수 값 (null 또는 빈 문자열일 경우 0 반환)</returns>
        public static int ConvertToInt(object input)
        {
            if (string.IsNullOrEmpty(input.ToString()))
                return 0;

            return Convert.ToInt32(input.ToString().Replace(",", ""));
        }

        /// <summary>
        /// 문자열 값을 double로 변환 (콤마 제거 및 null/빈 값 처리 포함)
        /// </summary>
        /// <param name="input">변환할 문자열</param>
        /// <returns>변환된 double 값 (null 또는 빈 문자열일 경우 0 반환)</returns>
        public static double ConvertToDouble(object input)
        {
            if (string.IsNullOrEmpty(input.ToString()))
                return 0;

            return Convert.ToDouble(input.ToString().Replace(",", ""));
        }
        /// <summary>
        /// 문자열 값을 decimal로 변환 (콤마 제거 및 null/빈 값 처리 포함)
        /// </summary>
        /// <param name="input">변환할 문자열</param>
        /// <returns>변환된 decimal 값 (null 또는 빈 문자열일 경우 0 반환)</returns>
        public static decimal ConvertToDecimal(object input)
        {
            if (string.IsNullOrEmpty(input.ToString()))
                return 0;

            return Convert.ToDecimal(input.ToString().Replace(",", ""));
        }
        /// <summary>
        /// 키 입력이 유효한지 확인하는 메소드
        /// 텍스트 박스 입력 제한 설정에 사용
        /// 숫자 키(0-9), 백스페이스, 방향키, 홈, 엔드, 탭, 엔터키 허용
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool IsValidKey(KeyEventArgs e)
        {
            return ((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) ||
                    (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9) ||
                    e.KeyCode == Keys.Back ||
                    e.KeyCode == Keys.Left ||
                    e.KeyCode == Keys.Right ||
                    e.KeyCode == Keys.Up ||
                    e.KeyCode == Keys.Down ||
                    e.KeyCode == Keys.Home ||
                    e.KeyCode == Keys.End ||
                    e.KeyCode == Keys.Tab ||
                    e.KeyCode == Keys.Enter);
        }

        /// <summary>
        /// 숫자만 입력 가능 텍스트 박스 (하이픈 제외) 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="textBox"></param>
        public static void AllowOnlyInteger(object sender, KeyEventArgs e, TextBox textBox)
        {
            // 숫자키, 숫자패드, 백스페이스, 방향키, 홈, 엔드, 탭 허용
            if (!IsValidKey(e))
            {
                // 허용되지 않은 키 제거
                cUIManager.ShowMessageBox("숫자만 입력할 수 있습니다.", "알림", MessageBoxButtons.OK);
                textBox.Text = new string(textBox.Text.Where(char.IsDigit).ToArray());
                textBox.SelectionStart = textBox.Text.Length;
            }

            // 텍스트 박스 값이 비어 있을 경우 처리
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                cUIManager.ShowMessageBox("0 이상의 값을 입력해야 합니다", "알림", MessageBoxButtons.OK);
                return;
            }
        }

        /// <summary>
        /// 숫자와 하이픈만 입력 가능 텍스트 박스 (전화번호 입력용) 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="textBox"></param>
        public static void AllowPhoneNumber(object sender, KeyEventArgs e, TextBox textBox)
        {
            // 숫자키, 숫자패드, 백스페이스, 방향키, 홈, 엔드, 탭, 하이픈만 허용
            if (!((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) ||
                  (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9) ||
                  e.KeyCode == Keys.Back ||
                  e.KeyCode == Keys.Left ||
                  e.KeyCode == Keys.Right ||
                  e.KeyCode == Keys.Home ||
                  e.KeyCode == Keys.End ||
                  e.KeyCode == Keys.Tab ||
                  e.KeyCode == Keys.Enter ||
                  e.KeyCode == Keys.OemMinus) ||
                  e.KeyCode == Keys.Subtract)  // 하이픈만 허용
            {
                // 허용되지 않은 키 제거
                //ShowMessageBox("숫자와 하이픈만 입력할 수 있습니다.", "알림", MessageBoxButtons.OK);
                textBox.Text = new string(textBox.Text.Where(c => char.IsDigit(c) || c == '-').ToArray());
                textBox.SelectionStart = textBox.Text.Length;
            }


        }
        /// <summary>
        /// 소수점 한자리까지만 입력 가능한 텍스트박스
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="textBox"></param>
        public static void AllowDecimalOnePlace(object sender, KeyEventArgs e, TextBox textBox)
        {
            // 숫자, 소수점, 방향키, 백스페이스, 홈, 엔드, 탭만 허용
            if (!IsValidKey(e))
            {
                // 허용되지 않은 키 제거
                cUIManager.ShowMessageBox("숫자와 소수점만 입력할 수 있습니다.", "알림", MessageBoxButtons.OK);
                textBox.Text = new string(textBox.Text.Where(c => char.IsDigit(c) || c == '.').ToArray());
                textBox.SelectionStart = textBox.Text.Length;
            }

            // 소수점 1자리만 허용
            if (textBox.Text.Contains('.') && textBox.Text.Substring(textBox.Text.IndexOf('.') + 1).Length > 1)
            {
                //ShowMessageBox("소수점은 한 자리까지만 입력 가능합니다.", "알림", MessageBoxButtons.OK);
                textBox.Text = textBox.Text.Substring(0, textBox.Text.IndexOf('.') + 2); // 한 자리까지만 남기기
                textBox.SelectionStart = textBox.Text.Length;
            }

            // 텍스트 박스 값이 비어 있을 경우 처리
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                cUIManager.ShowMessageBox("0 이상의 값을 입력해야 합니다", "알림", MessageBoxButtons.OK);
                return;
            }
        }

        // 소수점 두자리까지 입력 가능
        public static void AllowDecimalTwoPlaces(object sender, KeyEventArgs e, TextBox textBox)
        {
            if (!IsValidKey(e))
            {
                // 허용되지 않은 키 제거
                cUIManager.ShowMessageBox("숫자와 소수점만 입력할 수 있습니다.", "알림", MessageBoxButtons.OK);
                textBox.Text = new string(textBox.Text.Where(c => char.IsDigit(c) || c == '.').ToArray());
                textBox.SelectionStart = textBox.Text.Length;
            }

            // 소수점 2자리만 허용
            if (textBox.Text.Contains('.') && textBox.Text.Substring(textBox.Text.IndexOf('.') + 1).Length > 2)
            {
                //ShowMessageBox("소수점은 두 자리까지만 입력 가능합니다.", "알림", MessageBoxButtons.OK);
                textBox.Text = textBox.Text.Substring(0, textBox.Text.IndexOf('.') + 3); // 두 자리까지만 남기기
                textBox.SelectionStart = textBox.Text.Length;
            }

            // 텍스트 박스 값이 비어 있을 경우 처리
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                cUIManager.ShowMessageBox("0 이상의 값을 입력해야 합니다", "알림", MessageBoxButtons.OK);
                return;
            }
        }
    }
}
