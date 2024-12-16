using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace BRMS
{
    public partial class cDataBaseConfig : Form
    {
       
        private string iniFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dbconn.ini");
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        [DllImport("kernel32")]
        private static extern int WritePrivateProfileString(string section, string key, string value, string filePath);
        private string SetConnectionString;
        private cCryptor cryptor = new cCryptor("YourPassphrase");
        string address;
        string port;
        string user;
        string password;
        string database;

        public cDataBaseConfig()
        {
            InitializeComponent();
            LoadConfig();
            tBoxPassword.PasswordChar = '*';
        }
        private string GetConfigValue(string section, string key, string defaultValue)
        {
            // ini 파일이 없거나 섹션/키가 없을 경우 기본값을 반환
            StringBuilder temp = new StringBuilder(255);
            int length = GetPrivateProfileString(section, key, defaultValue, temp, temp.Capacity, iniFilePath);

            // 파일이 없거나 키가 없을 경우 기본값을 반환하도록 처리
            if (length == 0 && !File.Exists(iniFilePath))
            {
                // ini 파일이 없으면 빈 파일을 생성
                Directory.CreateDirectory(Path.GetDirectoryName(iniFilePath));
                File.WriteAllText(iniFilePath, "[dbconn]\n");
            }
            return temp.ToString();
        }
        private void LoadConfig()
        {
            tBoxAddresse.Text = GetConfigValue("dbconn", "Address", "");
            tBoxPort.Text = GetConfigValue("dbconn", "Port", "");
            tBoxUser.Text = GetConfigValue("dbconn", "id", "");
            string encryptedPassword = GetConfigValue("dbconn", "pw", "");
            try
            {
                // 비밀번호가 암호화된 형태라면 복호화 시도
                if (!string.IsNullOrEmpty(encryptedPassword))
                {
                    tBoxPassword.Text = cryptor.Decrypt(encryptedPassword);
                }
                else
                {
                    // 암호화된 값이 없으면 텍스트 박스에는 정보를 표시하지 않음
                    tBoxPassword.Text = "";
                }
            }
            catch
            {
                // 복호화 실패시 예외처리
                tBoxPassword.Text = "";
            }

            tBoxDatabase.Text = GetConfigValue("dbconn", "database", "");

        }

        private void SaveConfig(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, iniFilePath);
        }
        private bool TestConnection(string address, string port, string user, string password, string database)
        {
            try
            {              
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = address + ',' + port;
                builder.UserID = user;
                builder.Password = password;
                builder.InitialCatalog = database;
                SetConnectionString = builder.ConnectionString;
                
                using (SqlConnection connection = new SqlConnection(SetConnectionString))
                {
                    connection.Open();
                    MessageBox.Show("데이터베이스 연결 성공!");
                    return true;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("데이터베이스 연결 실패: " + ex.Message);
                return false;
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            //텍스트박스 값으로 변수 초기화
            address = tBoxAddresse.Text;
            port = tBoxPort.Text;
            user = tBoxUser.Text;
            password = tBoxPassword.Text;
            database = tBoxDatabase.Text;

            // 새로 입력된 값으로 연결 테스트 실행
            TestConnection(address, port, user, password, database);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            address = tBoxAddresse.Text;
            port = tBoxPort.Text;
            user = tBoxUser.Text;
            password = tBoxPassword.Text;
            database = tBoxDatabase.Text;
            string encryptedPassword = cryptor.Encrypt(password);
            SaveConfig("dbconn", "Address", address);
            SaveConfig("dbconn", "Port", port);
            SaveConfig("dbconn", "id", user);
            SaveConfig("dbconn", "pw", encryptedPassword);
            SaveConfig("dbconn", "database", database);
            Close();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
