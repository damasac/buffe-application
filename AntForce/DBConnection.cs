using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;

namespace AntForce
{
    class DBConnection
    {
        private DBConnection() { }
           
        private string host = string.Empty;
        private string databaseName = string.Empty;
        private string username = string.Empty;
        private string password = string.Empty;
        private string port = string.Empty;

        /*
        if(Properties.Settings.Default.valConfig["his_db"]){
            host = (string)Properties.Settings.Default.valConfig["his_ip"];
            databaseName = (string)Properties.Settings.Default.valConfig["his_db"];
            username = (string)Properties.Settings.Default.valConfig["his_user"];
            password = (string)Properties.Settings.Default.valConfig["his_password"];
            port = (string)Properties.Settings.Default.valConfig["his_port"];
        }
         */

        public string DatabaseName
        {
            get { return databaseName; }
            set { databaseName = value; }
        }
        public string Host
        {
            get { return host; }
            set { host = value ; }
        }
        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        public string Password {
            get { return password; }
            set { password = value; }
        }
        public string Port
        {
            get { return port; }
            set { port = value; }
        }
        private MySqlConnection Connection = null;
        private static DBConnection _instance = null;
        public static DBConnection Instance()
        {
            if (_instance == null)
                _instance = new DBConnection();
            return _instance;

        }

        public bool IsConnect()
        {
            bool result = false;
            if (Connection == null)
            {
                string StrCon = "";
                if (databaseName == string.Empty)
                {
                    StrCon = string.Format("server={0}; uid={1}; password={2}; port={3}; CharSet=utf8; convert zero datetime=True; default command timeout=360; Connection Timeout=360;", host, username, password, port);
                }
                else
                {
                    StrCon = string.Format("Server={0}; database={1}; UID={2}; password={3}; port={4}; CharSet=utf8; convert zero datetime=True; default command timeout=360; Connection Timeout=360;", host, databaseName, username, password, port);
                }

                try
                {
                    Connection = new MySqlConnection(StrCon);
                    Connection.Open();
                    result = true;
                }
                catch (MySqlException ex)
                {
                    result = false;
                    if (Connection.State == ConnectionState.Open)
                    {
                        Connection.Close();
                    }
                    Connection = null;
                    _instance = null;
                    MessageBox.Show(ex.Message);
                }
              
            }

            return result;
        }

        public MySqlConnection GetConnection()
        {
            return Connection;
        }



        public void Close()
        {
            Connection.Close();
            Connection = null;
            _instance = null;
        }
    }
}
