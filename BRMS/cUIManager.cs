using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace BRMS
{
    class cUIManager
    {
        public static DialogResult ShowMessageBox(string message, string caption, MessageBoxButtons buttons)
        {
            return CreatMessageBox(message, caption, buttons);
        }
        /// <summary>
        /// 메시지 박스 생성 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="caption"></param>
        /// <param name="buttons"></param>
        /// <returns></returns>
        private static DialogResult CreatMessageBox(string message, string caption, MessageBoxButtons buttons)
        {
            // 사용자 정의 메시지 박스 생성
            using (Form messageBox = new Form())
            {
                messageBox.Text = caption;
                messageBox.StartPosition = FormStartPosition.CenterParent; // 중앙 위치 설정
                messageBox.ClientSize = new System.Drawing.Size(300, 150);
                messageBox.ControlBox = false;
                messageBox.BackColor = System.Drawing.Color.White;
                // 메시지 레이블 설정
                Label lblMessage = new Label
                {
                    AutoSize = false,
                    TextAlign = System.Drawing.ContentAlignment.MiddleCenter, // 중앙 정렬 설정
                    Dock = DockStyle.Fill, // DockFill로 설정하여 공간을 모두 차지하게 함
                    MaximumSize = new System.Drawing.Size(280, 0),
                    Text = message,
                    Font = new System.Drawing.Font("맑은 고딕", 9F)
                };
                messageBox.Controls.Add(lblMessage);

                // 버튼 패널 설정
                Panel pnlButton = new Panel
                {
                    Dock = DockStyle.Bottom,
                    Height = 50
                };
                messageBox.Controls.Add(pnlButton);

                // 버튼 생성
                CreateMessgeButtons(pnlButton, buttons, messageBox);

                return messageBox.ShowDialog(); // 대화 상자를 모달로 표시
            }
        }

        // 메시지 버튼 생성 메서드
        private static void CreateMessgeButtons(Panel pnlButton, MessageBoxButtons buttons, Form messageBox)
        {
            // 버튼 패널 초기화
            pnlButton.Controls.Clear();
            // OK 버튼 생성
            Button btnOK = new Button
            {
                Text = "확인",
                DialogResult = DialogResult.OK
            };
            btnOK.Click += (sender, e) => { messageBox.Close(); };
            // Cancel 버튼 생성
            Button btnCancel = new Button
            {
                Text = "취소",
                DialogResult = DialogResult.Cancel
            };
            btnCancel.Click += (sender, e) => { messageBox.Close(); };
            // Yes 버튼 생성
            Button btnYes = new Button
            {
                Text = "예",
                DialogResult = DialogResult.Yes
            };
            btnYes.Click += (sender, e) => { messageBox.Close(); };
            // No 버튼 생성
            Button btnNo = new Button
            {
                Text = "아니요",
                DialogResult = DialogResult.No
            };
            btnNo.Click += (sender, e) => { messageBox.Close(); };
            // 버튼 배치 로직
            FlowLayoutPanel panel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(10),
                AutoSize = true
            };
            // MessageBoxButtons에 따른 버튼 추가
            switch (buttons)
            {
                case MessageBoxButtons.OK:
                    panel.Controls.Add(btnOK);
                    break;
                case MessageBoxButtons.OKCancel:
                    panel.Controls.Add(btnCancel);
                    panel.Controls.Add(btnOK);
                    break;
                case MessageBoxButtons.YesNo:
                    panel.Controls.Add(btnNo);
                    panel.Controls.Add(btnYes);
                    break;
                case MessageBoxButtons.YesNoCancel:
                    panel.Controls.Add(btnCancel);
                    panel.Controls.Add(btnNo);
                    panel.Controls.Add(btnYes);
                    break;
            }
            pnlButton.Controls.Add(panel);
        }
        public static class Color
        {
            public static readonly System.Drawing.Color Blue = System.Drawing.Color.FromArgb(81, 161, 243);
            public static readonly System.Drawing.Color Green = System.Drawing.Color.FromArgb(73, 173, 8);
            public static readonly System.Drawing.Color Orange = System.Drawing.Color.FromArgb(240, 144, 1);
            public static readonly System.Drawing.Color Red = System.Drawing.Color.FromArgb(239, 61, 86);
            public static readonly System.Drawing.Color LightGreen = System.Drawing.Color.FromArgb(181, 230, 162);
            public static readonly System.Drawing.Color LightYellow = System.Drawing.Color.FromArgb(255, 235, 156);//255, 235, 156
            public static readonly System.Drawing.Color Gray60 = System.Drawing.Color.FromArgb(153, 153, 153);
            public static readonly System.Drawing.Color DarkNavy = System.Drawing.Color.FromArgb(21, 96, 130);
            public static readonly System.Drawing.Color GreenGray = System.Drawing.Color.FromArgb(192, 200, 192);

        }

        /// <summary>
        /// 문자열 값을 정수로 변환 (콤마 제거 및 null/빈 값 처리 포함)
        /// </summary>
        /// <param name="input">변환할 문자열</param>
        /// <returns>변환된 정수 값 (null 또는 빈 문자열일 경우 0 반환)</returns>
        public static int ConvertToInt(string input)
        {
            if (string.IsNullOrEmpty(input))
                return 0;

            return Convert.ToInt32(input.Replace(",", ""));
        }

        /// <summary>
        /// 문자열 값을 double로 변환 (콤마 제거 및 null/빈 값 처리 포함)
        /// </summary>
        /// <param name="input">변환할 문자열</param>
        /// <returns>변환된 double 값 (null 또는 빈 문자열일 경우 0 반환)</returns>
        public static double ConvertToDouble(string input)
        {
            if (string.IsNullOrEmpty(input))
                return 0;

            return Convert.ToDouble(input.Replace(",", ""));
        }
        /// <summary>
        /// 문자열 값을 decimal로 변환 (콤마 제거 및 null/빈 값 처리 포함)
        /// </summary>
        /// <param name="input">변환할 문자열</param>
        /// <returns>변환된 decimal 값 (null 또는 빈 문자열일 경우 0 반환)</returns>
        public static decimal ConvertToDecimal(string input)
        {
            if (string.IsNullOrEmpty(input))
                return 0;

            return Convert.ToDecimal(input.Replace(",", ""));
        }
        public static bool IsValidKey(KeyEventArgs e)
        {
            return ((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) ||
                    (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9) ||
                    e.KeyCode == Keys.Back ||
                    e.KeyCode == Keys.Left ||
                    e.KeyCode == Keys.Right ||
                    e.KeyCode == Keys.Up ||  // 상 방향키 추가
                    e.KeyCode == Keys.Down || // 하 방향키 추가
                    e.KeyCode == Keys.Home ||
                    e.KeyCode == Keys.End ||
                    e.KeyCode == Keys.Tab ||
                    e.KeyCode == Keys.Enter); // 엔터키 추가
        }
        // 숫자만 입력 가능 (하이픈 제외)
        public static void AllowOnlyInteger(object sender, KeyEventArgs e,TextBox textBox)
        {
            // 숫자키, 숫자패드, 백스페이스, 방향키, 홈, 엔드, 탭 허용
            if (!IsValidKey(e))
            {
                // 허용되지 않은 키 제거
                ShowMessageBox("숫자만 입력할 수 있습니다.", "알림", MessageBoxButtons.OK);
                textBox.Text = new string(textBox.Text.Where(char.IsDigit).ToArray());
                textBox.SelectionStart = textBox.Text.Length;
            }

            // 텍스트 박스 값이 비어 있을 경우 처리
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                ShowMessageBox("0 이상의 값을 입력해야 합니다", "알림", MessageBoxButtons.OK);
                return;
            }
        }

        // 숫자와 하이픈만 입력 가능 (전화번호 입력용)
        public static void AllowPhoneNumber(object sender, KeyEventArgs e,TextBox textBox)
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

        // 소수점 한자리까지 입력 가능
        public static void AllowDecimalOnePlace(object sender, KeyEventArgs e,TextBox textBox)
        {
            // 숫자, 소수점, 방향키, 백스페이스, 홈, 엔드, 탭만 허용
            if (!IsValidKey(e))
            {
                // 허용되지 않은 키 제거
                ShowMessageBox("숫자와 소수점만 입력할 수 있습니다.", "알림", MessageBoxButtons.OK);
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
                ShowMessageBox("0 이상의 값을 입력해야 합니다", "알림", MessageBoxButtons.OK);
                return;
            }
        }

        // 소수점 두자리까지 입력 가능
        public static void AllowDecimalTwoPlaces(object sender, KeyEventArgs e,TextBox textBox)
        {
            if (!IsValidKey(e))
            {
                // 허용되지 않은 키 제거
                ShowMessageBox("숫자와 소수점만 입력할 수 있습니다.", "알림", MessageBoxButtons.OK);
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
                ShowMessageBox("0 이상의 값을 입력해야 합니다", "알림", MessageBoxButtons.OK);
                return;
            }
        }
    }
}
