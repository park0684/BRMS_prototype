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
        public static void ApplyFormStyle(Form form)
        {
            if (form == null) return;

            // 폼 속성 설정
            form.ControlBox = false;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.BackColor = System.Drawing.Color.White;
        }
        
    }
}
