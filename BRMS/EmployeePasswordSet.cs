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
    public partial class EmployeePasswordSet : Form
    {
        string previousWord;
        string newPassword;
        bool errorCheck = false;
        public event Action<string> passwordSet;
        public EmployeePasswordSet()
        {
            InitializeComponent();
            chkBoxPassword.CheckedChanged += chkBoxPassword_ChackedChanged;
        }
        /// <summary>
        /// 패스워드 등록 시 기존 패스워드 받아오기
        /// </summary>
        /// <param name="paasword"></param>
        public void GetPassword(string paasword)
        {
            InitiailzeSet(paasword);
        }
        /// <summary>
        /// 받아온 패스워드 변수 등록
        /// 체크 박스 해제상태로 변경 후 텍스트 박스 암호화 표시
        /// </summary>
        /// <param name="priviosPassword"></param>
        private void InitiailzeSet(string priviosPassword)
        {
            previousWord = priviosPassword;
            chkBoxPassword.Checked = false;
            UpdateCheckBox();
        }
        /// <summary>
        /// 체크 박스 이벤트 핸들러 등록
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkBoxPassword_ChackedChanged(object sender, EventArgs e)
        {
            UpdateCheckBox();
        }
        /// <summary>
        /// 체크 박스 변경 시 작업 내용
        /// </summary>
        private void UpdateCheckBox()
        {
            if (chkBoxPassword.Checked == true)
            {
                tBoxPassword.PasswordChar = default(char);
                tBoxCheck.PasswordChar = default(char);
            }
            else
            {
                tBoxPassword.PasswordChar = '*';
                tBoxCheck.PasswordChar = '*';
            }
        }
        /// <summary>
        /// 저장 전 오류 여부 확인
        /// </summary>
        private void ErrorCheck()
        {
            errorCheck = false;
            string password = tBoxPassword.Text.Trim();
            string check = tBoxCheck.Text.Trim();
            if(password != check)
            {
                cUIManager.ShowMessageBox("암호가 일치 하지 않습니다", "알림", MessageBoxButtons.OK);
                errorCheck = true;
                return;
            }
            if(previousWord == password)
            {
                cUIManager.ShowMessageBox("이전에 사용하던 암호와 동일합니다", "알림", MessageBoxButtons.OK);
                errorCheck = true;
                return;
            }
        }

        
        private void bntSave_Click(object sender, EventArgs e)
        {
            ErrorCheck();
            newPassword = tBoxPassword.Text.Trim();
            if(errorCheck == true)
            {
                return;
            }
            passwordSet?.Invoke(newPassword);
            Close();
        }

        private void bntCancle_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
