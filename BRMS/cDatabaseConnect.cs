using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;


namespace BRMS
{
    class cDatabaseConnect
    {
        //데이터 베이스 연결 설정 ini 파일 불러오기
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        private string str_sqlconnection;
        private cCryptor cryptor = new cCryptor("YourPassphrase");
        private SqlConnection connection;
        private SqlTransaction transaction;

        StringBuilder db_addr = new StringBuilder();
        StringBuilder db_port = new StringBuilder();
        StringBuilder db_id = new StringBuilder();
        StringBuilder db_pw = new StringBuilder(255);
        StringBuilder db_name = new StringBuilder();

        public cDatabaseConnect()
        {
            GetPrivateProfileString("dbconn", "Address", "", db_addr, db_addr.Capacity, Directory.GetCurrentDirectory() + "\\dbconn.ini");
            GetPrivateProfileString("dbconn", "port", "", db_port, db_port.Capacity, Directory.GetCurrentDirectory() + "\\dbconn.ini");
            GetPrivateProfileString("dbconn", "id", "", db_id, db_id.Capacity, Directory.GetCurrentDirectory() + "\\dbconn.ini");
            GetPrivateProfileString("dbconn", "pw", "", db_pw, db_pw.Capacity, Directory.GetCurrentDirectory() + "\\dbconn.ini");
            GetPrivateProfileString("dbconn", "database", "", db_name, db_name.Capacity, Directory.GetCurrentDirectory() + "\\dbconn.ini");
            string password = db_pw.ToString();
            password = cryptor.Decrypt(password);  // 암호화된 비밀번호 복호화
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = db_addr.ToString() + ',' + db_port.ToString();
            builder.UserID = db_id.ToString();
            builder.Password = password;
            builder.InitialCatalog = db_name.ToString();
            str_sqlconnection = builder.ConnectionString;
        }

        public bool IsTransactionActive()
        {
            return transaction != null && transaction.Connection != null && transaction.Connection.State == ConnectionState.Open;
        }

        public SqlConnection Opensql()
        {
            SqlConnection connectdb = new SqlConnection(str_sqlconnection);
            connectdb.Open();

            return connectdb;

        }



        //단일 row 결과물 조회 쿼리
        public void SqlReaderQuery(string query, DataTable dataTable)
        {
            try
            {
                using (SqlCommand command = new SqlCommand(query, Opensql()) )
                {
                    
                    SqlDataReader reader = command.ExecuteReader();
                    dataTable.Clear();
                    dataTable.Columns.Clear();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        dataTable.Columns.Add(reader.GetName(i));
                    }
                    if (reader.Read())
                    {
                        DataRow row = dataTable.NewRow();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row[i] = reader[i].ToString();
                        }
                        dataTable.Rows.Add(row);
                    }
                    reader.Close();
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show("연결 오류 :" + ex.Message);
            }

        }


        //단일 값 조회
        public void sqlScalaQuery(string query, out object objResult)
        {
            objResult = null;
            try
            {
                using (SqlConnection connection = Opensql())
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    objResult = command.ExecuteScalar();

                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("조회 오류 :" + ex.Message);
            }
        }

        //INSERT , UPDATE, DELETE 등 조회가 없는 쿼리
        //public void SqlNonQuery(string query, SqlConnection connection, SqlTransaction transaction = null, params SqlParameter[] parameters)
        //{
        //    try
        //    {
        //        using (SqlConnection connection = Opensql())
        //        {
        //            SqlCommand command = new SqlCommand(query, connection);
        //            command.ExecuteNonQuery();
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        MessageBox.Show("등록/수정 오류 :" + ex.Message);
        //    }
        //}
        public void ExecuteNonQuery(string query,SqlConnection connection, SqlTransaction transaction = null, params SqlParameter[] parameters)
        {
            
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                if (transaction != null)
                {
                    command.Transaction = transaction;
                }

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                command.ExecuteNonQuery();
            }
        }
        public void NonQuery(string query, SqlConnection connection, SqlTransaction transaction = null)
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Assign the transaction to the command if provided
                if (transaction != null)
                {
                    command.Transaction = transaction;
                }

                command.ExecuteNonQuery();
            }

        }



        //다량의 row 일괄 조회
        public void SqlDataAdapterQuery(string query, DataTable dataTable)
        {
            try
            {
                using (SqlConnection connection = Opensql())
                {
                    //DataSet dataSet = new DataSet();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter();
                    dataAdapter.SelectCommand = new SqlCommand(query, connection);
                    dataAdapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("데이터 조회 오류 : " + ex.Message);
            }
        }

        public void BeginTransaction()
        {
            if (connection == null || connection.State != ConnectionState.Open)
            {
                Opensql();
            }
            transaction = connection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (transaction != null)
            {
                transaction.Commit();
                transaction = null;
            }
        }

        public void RollbackTransaction()
        {
            if (transaction != null)
            {
                transaction.Rollback();
                transaction = null;
            }
        }
    }
}
