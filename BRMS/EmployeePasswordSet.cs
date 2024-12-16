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
        public void GetPassword(string paasword)
        {
            InitiailzeSet(paasword);
        }
        private void InitiailzeSet(string priviosPassword)
        {
            previousWord = priviosPassword;
            chkBoxPassword.Checked = false;
            UpdateCheckBox();
        }
        private void chkBoxPassword_ChackedChanged(object sender, EventArgs e)
        {
            UpdateCheckBox();
        }
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
        private void PasswordCheck()
        {
            string password = tBoxPassword.ToString();
            string check = tBoxCheck.ToString();
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
            errorCheck = false;
            PasswordCheck();
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
