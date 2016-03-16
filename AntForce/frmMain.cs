using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySql.Data;
using RestSharp;
using RestSharp.Authenticators;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections;
using System.IO;
using System.Threading;
using System.Security.Cryptography;
using System.Timers;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Data.SqlClient;
using System.Web.Script;
using System.Web.Script.Serialization;

namespace AntForce
{
    public partial class AntForce : Form
    {
        DBConnection DBCon = DBConnection.Instance();
        public AntForce()
        {
            InitializeComponent();
            string publishVersion = "";
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                System.Deployment.Application.ApplicationDeployment cd = System.Deployment.Application.ApplicationDeployment.CurrentDeployment;
                publishVersion = string.Format("v{0}.{1}.{2}.{3}", cd.CurrentVersion.Major.ToString(), cd.CurrentVersion.Minor.ToString(), cd.CurrentVersion.Build.ToString(), cd.CurrentVersion.Revision.ToString());
                // show publish version in title or About box...
            }
            this.Text += publishVersion;
            //
            // The path to the key where Windows looks for startup applications
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            // Check to see the current state (running at startup or not)
            if (rkApp.GetValue("TCC-Bot") == null)
            {
                // Add the value in the registry so that the application runs at startup
                rkApp.SetValue("TCC-Bot", Application.ExecutablePath.ToString());
                // Remove the value from the registry so that the application doesn't start
                //rkApp.DeleteValue("MyApp", false);
            }
        }

        private void newConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Properties.Settings.Default.valConfig.Count > 0)
                {

                }
            }
            catch
            {
                MessageBox.Show("Please Login Application!");
            }
        }
        private void timerConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                if (Properties.Settings.Default.valConfig.Count > 0)
                {
                    frmTimerConfig frmTimerConfig = new frmTimerConfig();
                    frmTimerConfig.Show();
                }
            }
            catch
            {
                MessageBox.Show("Please Login Application!");
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about frmabout = new about();
            frmabout.Show();
        }

        int msec = 1000;
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1_config.Stop();

            //set interval
            timer1_config.Interval = int.Parse((float.Parse((string)Properties.Settings.Default.valConfig["config_delay"]) * msec).ToString());
            //set interval
            timer2_constant.Interval = int.Parse((string)Properties.Settings.Default.valConfig["constants_delay"]) * msec;
            //set interval
            timer3_command.Interval = int.Parse((float.Parse((string)Properties.Settings.Default.valConfig["command_delay"]) * msec).ToString());
            //set interval
            timer4_syncdata.Interval = int.Parse((string)Properties.Settings.Default.valConfig["sync_delay"]) * msec;
            //set interval
            timer5_ping.Interval = int.Parse((string)Properties.Settings.Default.valConfig["template_delay"]) * msec;

            //get config
            try
            {
                string urlConfig = string.Format("https://webservice.thaicarecloud.org/buffe-config?token={0}", (string)Properties.Settings.Default.valLogin["access_token"]);
                var client = new RestClient(urlConfig);
                Console.WriteLine("url Config = " + urlConfig);
                var request = new RestRequest(Method.GET);
                client.Timeout = 30000;
                var response = client.Execute(request);
                request.Timeout = 30000;
                var content = response.Content;
                JObject valConfig = JObject.Parse(content);
                Properties.Settings.Default.valConfig = valConfig;
                AppendText("Webservice : Load Webservice config success");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Timer1 no respond" + ex.Message);
            }

            timer1_config.Start();

        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2_constant.Stop();
            try
            {
                string urlConst = string.Format("{0}/buffe-constants?token={1}",
                    (string)Properties.Settings.Default.valConfig["service_url"],
                    (string)Properties.Settings.Default.valLogin["access_token"]);
                Console.WriteLine("url Constant = " + urlConst);
                var client = new RestClient(urlConst);
                var request = new RestRequest(Method.GET);
                client.Timeout = 30000;
                var response = client.Execute(request);
                request.Timeout = 30000;
                var content = response.Content;
                JArray valConst = JArray.Parse(content);
                Properties.Settings.Default.valConst = valConst; //database;
                //replace constant
                int num = 0;
                foreach (var val in Properties.Settings.Default.valConst)
                {
                    if (val["id"].ToString() == "_SECRETKEY_")
                    {
                        Properties.Settings.Default.valConst[num]["value"] = sha256_hash(txtsecretKey.Text);
                    }
                    num++;
                }
                AppendText("Webservice : Load Webservice constant success");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Timer2 no response" + ex.Message);
            }
            timer2_constant.Start();
        }

        MySqlDataReader reader;
        private void timer3_Tick(object sender, EventArgs e)
        {
            timer3_command.Stop();
            //Connect DB
            DBCon.Host = Properties.Settings.Default.hisHost;
            DBCon.Username = Properties.Settings.Default.hisUsername;
            DBCon.Password = Properties.Settings.Default.hisPassword;
            DBCon.DatabaseName = Properties.Settings.Default.hisDBname;
            DBCon.Port = Properties.Settings.Default.hisPort;
            try
            {
                if (DBCon.IsConnect())
                {

                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Conect DB Error = " + ex.Message);
            }
            //end

            //step 1 check status is null (do enable)
            try
            {
                //if (DBCon.IsConnect())
                //{
                //set db connection
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = DBCon.GetConnection();

                //select data
                string query = string.Format("SELECT `id` FROM `{0}`.`buffe_command` WHERE status is null ORDER BY `priority`,`id` ASC", Properties.Settings.Default.valConst[0]["value"].ToString());
                cmd.CommandText = query;
                reader = cmd.ExecuteReader();
                var dt = new DataTable();
                dt.Load(reader);
                var rows = dt.AsEnumerable().ToArray();
                reader.Close();

                foreach (var valTrans in rows)
                {
                    //enable
                    string urlEnable = string.Format("{0}/buffe-command/enable?token={1}&id={2}",
                        (string)Properties.Settings.Default.valConfig["service_url"],
                        (string)Properties.Settings.Default.valLogin["access_token"],
                        valTrans["id"].ToString());
                    var client = new RestClient(urlEnable);
                    var request = new RestRequest(Method.GET);
                    var response = client.Execute(request);
                    AppendText("Command : step 1 check status is null (do enable)");
                    AppendText("Command : (Enable)");

                    //set status = 0 (enable success)
                    query = string.Format("UPDATE `{0}`.`buffe_command` SET `status`='0', `dupdate`=NOW() WHERE id = '{1}'", Properties.Settings.Default.valConst[0]["value"].ToString(), valTrans["id"].ToString());
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }
                //}
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Conect DB Error = " + ex.Message);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            //step 2 check status = 0 (do execute)
            int iderr = 0;
            try
            {
                //if (DBCon.IsConnect())
                //{
                //set db connection
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = DBCon.GetConnection();

                //select data
                string query = string.Format("SELECT `id`, `presql`, `sql`, `status` FROM `{0}`.`buffe_command` WHERE status ='0' ORDER BY `priority`,`id` ASC", Properties.Settings.Default.valConst[0]["value"].ToString());
                cmd.CommandText = query;
                reader = cmd.ExecuteReader();
                var dt = new DataTable();
                dt.Load(reader);
                var rows = dt.AsEnumerable().ToArray();
                reader.Close();

                foreach (var valTrans in rows)
                {
                    iderr = int.Parse(valTrans["id"].ToString());
                    //run pre SQL
                    if (valTrans["presql"].ToString() != "")
                    {
                        //replace constant
                        query = valTrans["presql"].ToString();
                        foreach (var val in Properties.Settings.Default.valConst)
                        {
                            query = query.Replace(val["id"].ToString(), val["value"].ToString());
                            //Console.WriteLine("Command (Execute) = " + val["id"].ToString() + ", value = " + val["value"].ToString());
                        }
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                    }

                    //run pre SQL
                    //replace constant
                    query = valTrans["sql"].ToString();
                    foreach (var val in Properties.Settings.Default.valConst)
                    {
                        query = query.Replace(val["id"].ToString(), val["value"].ToString());
                        //Console.WriteLine("Command (Execute) = " + val["id"].ToString() + ", value = " + val["value"].ToString());
                    }

                    cmd.CommandText = query;
                    reader = cmd.ExecuteReader();
                    AppendText("Command : step 2 check status = 0 (do execute)");
                    AppendText("Command : (Execute)");
                    Console.WriteLine("Command (Execute)");
                    //encode json
                    var r = Serialize(reader);
                    var json = JsonConvert.SerializeObject(r, Formatting.Indented);
                    //Console.WriteLine("Command (Json) = " + json);

                    //update result
                    if (reader != null) reader.Close();

                    //put json result to localhost
                    try
                    {

                        query = MySqlHelper.EscapeString(json);
                        query = string.Format("UPDATE `{0}`.`buffe_command` SET `result`=\"{1}\", status='1', `dupdate`=NOW() WHERE id = '{2}'", Properties.Settings.Default.valConst[0]["value"].ToString(), query, valTrans["id"].ToString());

                        Console.WriteLine("Command (put json result to localhost)");

                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();


                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine("Command Execute error (put json result to localhost) " + ex.Message);
                        //Console.WriteLine("query =  " + query);
                        query = string.Format("UPDATE `{0}`.`buffe_command` SET `clienterr`=\"{1}\", status='1', `dupdate`=NOW() WHERE id = '{2}'", Properties.Settings.Default.valConst[0]["value"].ToString(), MySqlHelper.EscapeString(ex.Message), valTrans["id"].ToString());
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                    }
                }
                //}
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Conect DB Error = " + ex.Message);
                try
                {
                    string query = string.Format("UPDATE `{0}`.`buffe_command` SET `clienterr`=\"{1}\", status='1', `dupdate`=NOW() WHERE id = '{2}'", Properties.Settings.Default.valConst[0]["value"].ToString(), MySqlHelper.EscapeString(ex.Message), iderr);

                    Console.WriteLine("Command (put json result to localhost)");

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = DBCon.GetConnection();
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex1)
                {

                }

            }
            finally
            {
                if (reader != null) reader.Close();


            }

            //step 3 check status = 1 (do sync)
            try
            {
                //if (DBCon.IsConnect())
                //{
                //set db connection
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = DBCon.GetConnection();

                //select data
                string query = string.Format("SELECT id FROM `{0}`.`buffe_command` WHERE status ='1' ORDER BY `priority`,`id` ASC", Properties.Settings.Default.valConst[0]["value"].ToString());
                cmd.CommandText = query;
                reader = cmd.ExecuteReader();
                var dt = new DataTable();
                dt.Load(reader);
                var rows = dt.AsEnumerable().ToArray();
                reader.Close();

                foreach (var valTrans in rows)
                {
                    //send json result to server
                    try
                    {
                        //select data
                        string paramSync = "";
                        query = string.Format("SELECT * FROM `{0}`.`buffe_command` WHERE id ='{1}';", Properties.Settings.Default.valConst[0]["value"].ToString(), valTrans["id"].ToString());
                        cmd.CommandText = query;
                        reader = cmd.ExecuteReader();
                        reader.Read();
                        int FieldCount = reader.FieldCount, n = 0;
                        while (FieldCount > n)
                        {
                            paramSync += reader.GetName(n) + "=" + (reader.IsDBNull(n) ? "" : reader.GetString(n)) + "&";
                            n++;
                        }
                        reader.Close();
                        paramSync = paramSync.Substring(0, paramSync.Length - 1);

                        //Console.WriteLine("paramSync (Command)" + paramSync);

                        try
                        {
                            //sync
                            string urlSync = string.Format("{0}/buffe-command/sync?token={1}&id={2}",
                                (string)Properties.Settings.Default.valConfig["service_url"],
                                (string)Properties.Settings.Default.valLogin["access_token"],
                                 valTrans["id"].ToString());
                            var client = new RestClient(urlSync);
                            client.Timeout = 30000;
                            var request = new RestRequest(Method.POST);
                            request.Timeout = 30000;
                            request.AddHeader("content-type", "application/x-www-form-urlencoded");
                            request.AddHeader("cache-control", "no-cache");
                            request.AddParameter("application/x-www-form-urlencoded", paramSync, ParameterType.RequestBody);
                            var response = client.Execute(request);
                            var content = response.Content;
                            JObject arr = JObject.Parse(content);
                            if ((string)arr["result"] == "OK") {
                                AppendText("Command : step 3 check status = 1 (do sync)");
                                AppendText("Command : (send json result to server)");
                                Console.WriteLine("Command (send json result to server)");

                                //remove Q
                                if (Properties.Settings.Default.valConfig["del_log"].ToString() == "1")
                                {
                                    query = string.Format("DELETE FROM `{0}`.`buffe_command` WHERE id = '{1}'", Properties.Settings.Default.valConst[0]["value"].ToString(), valTrans["id"].ToString());
                                    cmd.CommandText = query;
                                    cmd.ExecuteNonQuery();
                                }
                                else
                                {
                                    //set status = 2 (sync success)
                                    query = string.Format("UPDATE `{0}`.`buffe_command` SET `status`='2', `dupdate`=NOW() WHERE id = '{1}'", Properties.Settings.Default.valConst[0]["value"].ToString(), valTrans["id"].ToString());
                                    cmd.CommandText = query;
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                //set serverr (sync fail)
                                query = string.Format("UPDATE `{0}`.`buffe_command` SET `serverr`=\"{1}\", `dupdate`=NOW() WHERE id = '{2}'", Properties.Settings.Default.valConst[0]["value"].ToString(), MySqlHelper.EscapeString(content), valTrans["id"].ToString());
                                cmd.CommandText = query;
                                cmd.ExecuteNonQuery();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Execute sync result " + ex.Message);
                            //set serverr (sync fail)
                            query = string.Format("UPDATE `{0}`.`buffe_command` SET `serverr`=\"{1}\", `dupdate`=NOW() WHERE id = '{2}'", Properties.Settings.Default.valConst[0]["value"].ToString(), MySqlHelper.EscapeString(ex.Message), valTrans["id"].ToString());
                            cmd.CommandText = query;
                            cmd.ExecuteNonQuery();
                        }

                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine("Execute error (send json result to server) " + ex.Message);
                    }
                }
                //}
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Conect DB Error = " + ex.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
            }

            //step 4 check status < 2 or status is null if record = 0 (do get new command and ctype = SQL)
            try
            {
                //if (DBCon.IsConnect())
                //{
                //set db connection
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = DBCon.GetConnection();

                //select
                string query = string.Format("SELECT count(*) as total FROM `{0}`.`buffe_command` WHERE status < 2 or status is null;", Properties.Settings.Default.valConst[0]["value"].ToString());
                cmd.CommandText = query;
                reader = cmd.ExecuteReader();
                reader.Read();
                int total = int.Parse(reader.GetString(0));
                reader.Close();

                if (total == 0)
                {
                    //Connect DB
                    DBCon.Host = Properties.Settings.Default.hisHost;
                    DBCon.Username = Properties.Settings.Default.hisUsername;
                    DBCon.Password = Properties.Settings.Default.hisPassword;
                    DBCon.DatabaseName = Properties.Settings.Default.hisDBname;
                    DBCon.Port = Properties.Settings.Default.hisPort;
                    try
                    {
                        if (DBCon.IsConnect())
                        {
                            cmd = new MySqlCommand();
                            cmd.Connection = DBCon.GetConnection();
                        }
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine("Conect DB Error = " + ex.Message);
                    }
                    //**
                    string urlComd = string.Format("{0}/buffe-command?token={1}",
                        (string)Properties.Settings.Default.valConfig["service_url"],
                        (string)Properties.Settings.Default.valLogin["access_token"]);
                    AppendText("Command : step 4 check status < 2 or status is null if record = 0 (do get new command and ctype = SQL)");
                    Console.WriteLine("url Command = " + urlComd);
                    var client = new RestClient(urlComd);
                    var request = new RestRequest(Method.GET);
                    client.Timeout = 30000;
                    var response = client.Execute(request);
                    request.Timeout = 30000;
                    var content = response.Content;

                    //int num = response.ContentLength();
                    if (content != "")
                    {
                        JObject valCommand = JObject.Parse(content);

                        AppendText("Webservice : Load Webservice command success");
                        //AppendText("Load Webservice check ping connection success");
                        //Console.WriteLine("a=" + Properties.Settings.Default.valConst);
                        if ((string)valCommand["status"] == null)
                        {
                            AppendText("Command : (GET)" + (string)valCommand["cname"]);

                            //store command
                            if ((string)valCommand["ctype"] != "SQL")
                            {
                                string fields = "", values = "";
                                foreach (var j in valCommand)
                                {
                                    fields += "`" + j.Key + "`, ";
                                    if (j.Key == "dupdate" || j.Key == "dadd")
                                    {
                                        values += "NOW(), ";
                                    }
                                    else if (j.Key == "status")
                                    {
                                        values += "\"0\", ";
                                    }
                                    else
                                    {
                                        values += "\"" + j.Value + "\", ";
                                    }


                                }
                                fields = fields.Substring(0, fields.Length - 2);
                                values = values.Substring(0, values.Length - 2);

                                query = string.Format("REPLACE INTO `{0}`.`buffe_command` ({1}) VALUES({2});", Properties.Settings.Default.valConst[0]["value"].ToString(), fields, values);
                                Console.WriteLine("Command (Store)");
                                try
                                {
                                    cmd.CommandText = query;
                                    cmd.ExecuteNonQuery();
                                    AppendText("Command : (Store)");
                                    Console.WriteLine("Command (Store)");
                                }
                                catch (MySqlException ex)
                                {
                                    Console.WriteLine("Execute error (store command)" + ex.Message);
                                }
                            }
                            else
                            {
                                //run pre SQL
                                if ((string)valCommand["presql"] != "")
                                {
                                    //replace constant
                                    query = (string)valCommand["presql"];
                                    foreach (var val in Properties.Settings.Default.valConst)
                                    {
                                        query = query.Replace(val["id"].ToString(), val["value"].ToString());
                                        //Console.WriteLine("Command (Execute) = " + val["id"].ToString() + ", value = " + val["value"].ToString());
                                    }
                                    cmd.CommandText = query;
                                    cmd.ExecuteNonQuery();
                                }

                                //run pre SQL
                                //replace constant
                                query = (string)valCommand["sql"];
                                foreach (var val in Properties.Settings.Default.valConst)
                                {
                                    query = query.Replace(val["id"].ToString(), val["value"].ToString());
                                    //Console.WriteLine("Command (Execute) = " + val["id"].ToString() + ", value = " + val["value"].ToString());
                                }

                                cmd.CommandText = query;
                                cmd.ExecuteNonQuery();

                                //enable
                                string urlEnable = string.Format("{0}/buffe-command/enable?token={1}&id={2}",
                                    (string)Properties.Settings.Default.valConfig["service_url"],
                                    (string)Properties.Settings.Default.valLogin["access_token"],
                                    (string)valCommand["id"]);
                                client = new RestClient(urlEnable);
                                request = new RestRequest(Method.GET);
                                response = client.Execute(request);
                                AppendText("Command : (Enable)");
                            }
                        }
                    }

                }

                //}
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Conect DB Error = " + ex.Message);
                
                //กรณีที่ Exception เพราะไม่มี table ให้เรียก command ใหม่เพื่อ excute

                //set db connection
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = DBCon.GetConnection();
                //**
                string urlComd = string.Format("{0}/buffe-command?token={1}",
                    (string)Properties.Settings.Default.valConfig["service_url"],
                    (string)Properties.Settings.Default.valLogin["access_token"]);
                AppendText("Command : step 4 check status < 2 or status is null if record = 0 (do get new command and ctype = SQL)");
                Console.WriteLine("url Command = " + urlComd);
                var client = new RestClient(urlComd);
                var request = new RestRequest(Method.GET);
                client.Timeout = 30000;
                var response = client.Execute(request);
                request.Timeout = 30000;
                var content = response.Content;

                //int num = response.ContentLength();
                if (content != "")
                {
                    JObject valCommand = JObject.Parse(content);
                    string query = "";

                    AppendText("Webservice : Load Webservice command success");
                    //AppendText("Load Webservice check ping connection success");
                    //Console.WriteLine("a=" + Properties.Settings.Default.valConst);
                    if ((string)valCommand["status"] == null)
                    {
                        AppendText("Command : (GET)");

                        //store command
                        if ((string)valCommand["ctype"] != "SQL")
                        {
                            string fields = "", values = "";
                            foreach (var j in valCommand)
                            {
                                fields += "`" + j.Key + "`, ";

                                if (j.Key == "dupdate" && j.Key == "dadd")
                                {
                                    values += "NOW(), ";
                                }
                                else if (j.Key == "status")
                                {
                                    values += "\"0\", ";
                                }
                                else
                                {
                                    values += "\"" + j.Value + "\", ";
                                }


                            }
                            fields = fields.Substring(0, fields.Length - 2);
                            values = values.Substring(0, values.Length - 2);

                            query = string.Format("REPLACE INTO `{0}`.`buffe_command` ({1}) VALUES({2});", Properties.Settings.Default.valConst[0]["value"].ToString(), fields, values);
                            try
                            {
                                cmd.CommandText = query;
                                cmd.ExecuteNonQuery();
                                AppendText("Command : (Store)");
                                Console.WriteLine("Command : (Store)");
                            }
                            catch (MySqlException exx)
                            {
                                Console.WriteLine("Execute error (store command)" + exx.Message);
                            }
                        }
                        else
                        {
                            //run pre SQL
                            if ((string)valCommand["presql"] != "")
                            {
                                //replace constant
                                query = (string)valCommand["presql"];
                                foreach (var val in Properties.Settings.Default.valConst)
                                {
                                    query = query.Replace(val["id"].ToString(), val["value"].ToString());
                                    //Console.WriteLine("Command (Execute) = " + val["id"].ToString() + ", value = " + val["value"].ToString());
                                }
                                //
                                try
                                {
                                    cmd.CommandText = query;
                                    cmd.ExecuteNonQuery();
                                }
                                 catch (MySqlException exx)
                                 {
                                     AppendText("Execute error (ctype != \"SQL\" no store command)" + exx.Message);
                                 }
                            }

                            //run pre SQL
                            //replace constant
                            query = (string)valCommand["sql"];
                            foreach (var val in Properties.Settings.Default.valConst)
                            {
                                query = query.Replace(val["id"].ToString(), val["value"].ToString());
                                //Console.WriteLine("Command (Execute) = " + val["id"].ToString() + ", value = " + val["value"].ToString());
                            }

                            try
                            {
                                cmd.CommandText = query;
                                cmd.ExecuteNonQuery();

                                //enable
                                string urlEnable = string.Format("{0}/buffe-command/enable?token={1}&id={2}",
                                    (string)Properties.Settings.Default.valConfig["service_url"],
                                    (string)Properties.Settings.Default.valLogin["access_token"],
                                    (string)valCommand["id"]);
                                client = new RestClient(urlEnable);
                                request = new RestRequest(Method.GET);
                                response = client.Execute(request);
                                AppendText("Command : (Enable)");
                            }
                            catch (MySqlException exx)
                            {
                                AppendText("Execute error (ctype != \"SQL\" no store command)" + exx.Message);
                            }
                        }
                        //
                    }
                }
            
            }
            finally
            {
                if (reader != null) reader.Close();
            }


            timer3_command.Start();
        }

        public IEnumerable<Dictionary<string, object>> Serialize(MySqlDataReader reader)
        {
            var results = new List<Dictionary<string, object>>();
            var cols = new List<string>();
            for (var i = 0; i < reader.FieldCount; i++)
                cols.Add(reader.GetName(i));

            while (reader.Read())
                results.Add(SerializeRow(cols, reader));

            return results;
        }
        private Dictionary<string, object> SerializeRow(IEnumerable<string> cols, MySqlDataReader reader)
        {
            var result = new Dictionary<string, object>();
            int n = 0;
            foreach (var col in cols)
            {
                var value = "";
                if (reader.IsDBNull(n))
                {
                    value = "";
                }
                else
                {
                    TypeCode typeCode = Type.GetTypeCode(reader.GetValue(n).GetType());
                    if (typeCode == TypeCode.DateTime)
                    {
                        try
                        {
                            value = reader.GetString(n);
                            DateTime myDateTime = DateTime.Parse(value);
                            value = myDateTime.ToString("s");
                        }catch{
                            value = "";
                        }
                    }
                    else
                    {
                        value = reader.GetString(n);
                    }
                }
                result.Add(col, value);
                n++;
            }
            return result;
        }

        private void timer4_syncdata_Tick(object sender, EventArgs e)
        {
            timer4_syncdata.Stop();

            //Connect DB
            DBCon.Host = Properties.Settings.Default.hisHost;
            DBCon.Username = Properties.Settings.Default.hisUsername;
            DBCon.Password = Properties.Settings.Default.hisPassword;
            DBCon.DatabaseName = Properties.Settings.Default.hisDBname;
            DBCon.Port = Properties.Settings.Default.hisPort;
            try
            {
                if (DBCon.IsConnect())
                {

                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Conect DB Error = " + ex.Message);
            }

            //step 1 check status = 0 (do execute)
            try
            {
                //if (DBCon.IsConnect())
                //{
                //set db connection
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = DBCon.GetConnection();

                //select limit 10
                string query = string.Format("SELECT `id`, `presql`, `sql`, `status`, `table` FROM `{0}`.`buffe_transfer` WHERE status ='0' or status is null ORDER BY `id` ASC LIMIT 0,{1}", Properties.Settings.Default.valConst[0]["value"].ToString(), Properties.Settings.Default.valConfig["sync_nrec"].ToString());
                cmd.CommandText = query;
                MySqlDataReader dr = cmd.ExecuteReader();
                var dt = new DataTable();
                dt.Load(dr);
                var rows = dt.AsEnumerable().ToArray();
                dr.Close();
                //Console.WriteLine("Command (Execute) = "+ query);

                foreach (var valTrans in rows)
                {
                    //Execute command
                    try
                    {
                        //run pre SQL
                        if (valTrans["presql"].ToString() != "")
                        {
                            //replace constant
                            query = valTrans["presql"].ToString();
                            foreach (var val in Properties.Settings.Default.valConst)
                            {
                                query = query.Replace(val["id"].ToString(), val["value"].ToString());
                                //Console.WriteLine("Command (Execute) = " + val["id"].ToString() + ", value = " + val["value"].ToString());
                            }
                            cmd.CommandText = query;
                            cmd.ExecuteNonQuery();
                        }

                        //run pre SQL
                        //replace constant
                        query = valTrans["sql"].ToString();
                        foreach (var val in Properties.Settings.Default.valConst)
                        {
                            query = query.Replace(val["id"].ToString(), val["value"].ToString());
                            //Console.WriteLine("Command (Execute) = " + val["id"].ToString() + ", value = " + val["value"].ToString());
                        }

                        cmd.CommandText = query;
                        MySqlDataReader reader = cmd.ExecuteReader();
                        AppendText("Transfer : Command (Execute)");
                        //Console.WriteLine("Transfer Command (Execute) "+query);
                        //encode json
                        //JavaScriptSerializer js = new JavaScriptSerializer();
                        //string json = js.Serialize(r);
                        var r = Serialize(reader);
                        var json = JsonConvert.SerializeObject(r, Formatting.Indented);
                        


                        //update result
                        reader.Close();

                        //put json result to localhost
                        try
                        {
                            
                            query = MySqlHelper.EscapeString(json);
                            query = string.Format("UPDATE `{0}`.`buffe_transfer` SET `result`=\"{1}\", status='1', `dupdate`=NOW() WHERE id = '{2}'", Properties.Settings.Default.valConst[0]["value"].ToString(), query, valTrans["id"].ToString());

                            Console.WriteLine("Transfer (put json result to localhost)");

                            cmd.CommandText = query;
                            cmd.ExecuteNonQuery();

                        }
                        catch (MySqlException ex)
                        {
                            Console.WriteLine("Transfer Execute error (put json result to localhost) " + ex.Message);
                            //Console.WriteLine("query =  " + query);
                        }

                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine("Transfer Execute command error = " + ex.Message);
                        //put client error
                        query = string.Format("UPDATE `{0}`.`buffe_transfer` SET `clienterr`=\"{1}\", `status`='9', `dupdate`=NOW() WHERE id = '{2}'", Properties.Settings.Default.valConst[0]["value"].ToString(), ex.Message, valTrans["id"].ToString());
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                    }
                }//end for

            }
            catch (MySqlException ex)
            {

                Console.WriteLine("Conect DB Error = " + ex.Message);

            }

            //step 2 check status = 1 (do sync)
            try
            {
                string cpu = "", ram = "";
                //get cpu /ram
                cpu = getCurrentCpuUsage().ToString("0.00"); ram = getAvailableRAM().ToString("0.00");
                lbCpu.Text = "CPU : " + cpu + "% / RAM : " + ram + "MB";

                //if (DBCon.IsConnect())
                //{
                //set db connection
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = DBCon.GetConnection();

                //count qleft
                string query = string.Format("SELECT count(*) as total FROM `{0}`.`buffe_transfer` WHERE status < 2;",
                    Properties.Settings.Default.valConst[0]["value"].ToString());
                cmd.CommandText = query;
                reader = cmd.ExecuteReader();
                reader.Read();

                int qleft = int.Parse(reader.GetString(0));
                reader.Close();
                if (qleft <= 1)
                    qleft = 0;
                lblQleft.Text = "จำนวนคิวที่เหลือ :  " + qleft.ToString("#,##0") + " คิว";

                //select limit 10
                query = string.Format("SELECT `id`, `table` FROM `{0}`.`buffe_transfer` WHERE status = 1 OR status = 9 ORDER BY `status`,`id` ASC LIMIT 0,{1}", Properties.Settings.Default.valConst[0]["value"].ToString(), Properties.Settings.Default.valConfig["sync_nrec"].ToString());
                cmd.CommandText = query;
                MySqlDataReader dr = cmd.ExecuteReader();
                var dt = new DataTable();
                dt.Load(dr);
                var rows = dt.AsEnumerable().ToArray();
                dr.Close();
                //Console.WriteLine("Command (Execute) = "+ query);
                
                foreach (var valTrans in rows)
                {
                    //Execute select Q error
                    try
                    {

                        //send json result to server
                        try
                        {
                            //count qleft
                            query = string.Format("SELECT count(*) as total FROM `{0}`.`buffe_transfer` WHERE `table` = '{1}' and status < 2;",
                                Properties.Settings.Default.valConst[0]["value"].ToString(),
                                valTrans["table"].ToString()
                                );
                            cmd.CommandText = query;
                            reader = cmd.ExecuteReader();
                            reader.Read();

                            qleft = int.Parse(reader.GetString(0));
                            reader.Close();
                            if (qleft <= 1)
                                qleft = 0;

                            //update qleft
                            query = string.Format("UPDATE `{0}`.`buffe_transfer` SET `qleft`=\"{1}\", `dupdate`=NOW() WHERE id = '{2}'", Properties.Settings.Default.valConst[0]["value"].ToString(), qleft, valTrans["id"].ToString());
                            cmd.CommandText = query;
                            cmd.ExecuteNonQuery();

                            //select data
                            string paramSync = "";
                            query = string.Format("SELECT * FROM `{0}`.`buffe_transfer` WHERE id ='{1}';", Properties.Settings.Default.valConst[0]["value"].ToString(), valTrans["id"].ToString());
                            cmd.CommandText = query;
                            reader = cmd.ExecuteReader();
                            reader.Read();
                            int FieldCount = reader.FieldCount, n = 0;
                            while (FieldCount > n)
                            {
                                if (reader.GetName(n) != "dadd")
                                {
                                    //allow string null
                                    paramSync += reader.GetName(n) + "=" + (reader.IsDBNull(n) ? "" : reader.GetString(n)) + "&";
                                }
                                n++;
                            }
                            reader.Close();
                            paramSync = paramSync.Substring(0, paramSync.Length - 1);

                            AppendText("Transfer : (read json result)");
                            //Console.WriteLine("paramSync (Transfer)" + paramSync);

                            //sync
                            try
                            {
                                string urlSync = string.Format("{0}/buffe-transfer/sync?token={1}&cpu=" + cpu + "&ram=" + ram,
                                    (string)Properties.Settings.Default.valConfig["service_url"],
                                    (string)Properties.Settings.Default.valLogin["access_token"]);
                                var client = new RestClient(urlSync);
                                var request = new RestRequest(Method.POST);
                                client.Timeout = 30000;
                                request.Timeout = 30000;
                                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                                request.AddHeader("cache-control", "no-cache");
                                request.AddParameter("application/x-www-form-urlencoded", paramSync, ParameterType.RequestBody);
                                var response = client.Execute(request);
                                //Delay
                                System.Threading.Thread.Sleep(int.Parse(Properties.Settings.Default.valConfig["sync_delay"].ToString()));

                                var content = response.Content;
                                JObject arr = JObject.Parse(content);
                                if ((string)arr["result"] == "OK")
                                {
                                    Console.WriteLine("Transfer : (send json result to server)");
                                    AppendText("Transfer : (send json result to server)");

                                    //remove Q
                                    if (Properties.Settings.Default.valConfig["del_log"].ToString() == "1")
                                    {
                                        query = string.Format("DELETE FROM `{0}`.`buffe_transfer` WHERE id = '{1}'", Properties.Settings.Default.valConst[0]["value"].ToString(), valTrans["id"].ToString());
                                        cmd.CommandText = query;
                                        cmd.ExecuteNonQuery();
                                    }
                                    else
                                    {
                                        //set status = 2 (sync success)
                                        query = string.Format("UPDATE `{0}`.`buffe_transfer` SET `status`='2', `dupdate`=NOW() WHERE id = '{1}'", Properties.Settings.Default.valConst[0]["value"].ToString(), valTrans["id"].ToString());
                                        cmd.CommandText = query;
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                else
                                {
                                    //set serverr (sync fail)
                                    query = string.Format("UPDATE `{0}`.`buffe_transfer` SET `serverr`=\"{1}\", `status`='9', `dupdate`=NOW() WHERE id = '{2}'", Properties.Settings.Default.valConst[0]["value"].ToString(), MySqlHelper.EscapeString(content), valTrans["id"].ToString());
                                    cmd.CommandText = query;
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error sync result " + ex.Message);
                                //set serverr (sync fail)
                                query = string.Format("UPDATE `{0}`.`buffe_transfer` SET `serverr`=\"{1}\", `status`='9', `dupdate`=NOW() WHERE id = '{2}'", Properties.Settings.Default.valConst[0]["value"].ToString(), MySqlHelper.EscapeString(ex.Message), valTrans["id"].ToString());
                                cmd.CommandText = query;
                                cmd.ExecuteNonQuery();
                            }


                        }
                        catch (MySqlException ex)
                        {
                            Console.WriteLine("Transfer Execute error (status = 1 OR status = 9) " + ex.Message);
                        }
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine("Transfer Execute cerror (select Q error) = " + ex.Message);
                    }
                }//end for

            }
            catch (MySqlException ex)
            {

                Console.WriteLine("Conect DB Error = " + ex.Message);

            }

            timer4_syncdata.Start();
        }

        PerformanceCounter cpuCounter = new PerformanceCounter("Process", "% Processor Time", Process.GetCurrentProcess().ProcessName);
        PerformanceCounter ramCounter = new PerformanceCounter("Process", "% Processor Time", Process.GetCurrentProcess().ProcessName);
        PerformanceCounter total_cpu = new PerformanceCounter("Process", "% Processor Time", "_Total");

        public double getCurrentCpuUsage()
        {
            return cpuCounter.NextValue() / total_cpu.NextValue() * 100.0;
        }

        public float getAvailableRAM()
        {
            return ramCounter.NextValue();
        }

        private void timer5_ping_Tick(object sender, EventArgs e)
        {
            timer5_ping.Stop();
            //
            string cpu = getCurrentCpuUsage().ToString("0.00"), ram = getAvailableRAM().ToString("0.00");
            lbCpu.Text = "CPU : " + cpu + "% / RAM : " + ram + "MB";

            //
            try
            {
                string urlPing = string.Format("{0}/buffe-config/ping?token={1}&cpu=" + cpu + "&ram=" + ram,
                    (string)Properties.Settings.Default.valConfig["service_url"],
                    (string)Properties.Settings.Default.valLogin["access_token"]);
                var client = new RestClient(urlPing);
                var request = new RestRequest(Method.GET);
                client.Timeout = 30000;
                var response = client.Execute(request);
                request.Timeout = 30000;
                var content = response.Content;
                if (content != "")
                {
                    JObject valPing = JObject.Parse(content);
                    Properties.Settings.Default.valPing = valPing;
                    AppendText("Webservice : Load Webservice check ping connection success");
                }
                //Console.WriteLine("Timer5 urlPing " + urlPing);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Timer5 no response " + ex.Message);
            }
            timer5_ping.Start();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (btnLogin.Text == "Login")
            {
                btnLogin.Enabled = false;
                btnLogin.Text = "Login...";

                string urlLogin = "https://webservice.thaicarecloud.org/user";
                string urlConfig, urlConst, urlPing;

                var client = new RestClient(urlLogin);
                client.Authenticator = new HttpBasicAuthenticator(txtUserLogin.Text, txtPassLogin.Text);
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);
                //var content = resp.Content;
                //Console.WriteLine(resp.StatusDescription);
                //Console.WriteLine(resp.StatusCode);

                //Console.WriteLine(content);

                var content = response.Content;
                //Console.WriteLine("content xx = " + content.ToString());
                if (content != "")
                {
                    JObject arr = JObject.Parse(content);
                    Properties.Settings.Default.valLogin = arr;
                    string username = (string)arr["username"];

                    if (username == null)
                    {

                        MessageBox.Show("Incorrect username or password.");
                        btnLogin.Text = "Login";
                        btnLogin.Enabled = true;

                    }
                    else
                    {
                        
                        //save setting
                        Properties.Settings.Default.wsUsername = txtUserLogin.Text;
                        Properties.Settings.Default.wsPassword = txtPassLogin.Text;
                        Properties.Settings.Default.Save();

                        //show info
                        infoName.Text = "ชื่อ : " + arr["name"].ToString();
                        infoSite.Text = "สถานบริการ : (" + arr["sitecode"].ToString() + ") " + arr["hospital"].ToString();


                        //end save
                        //Console.WriteLine(""+username);
                        AppendText("Login success!");

                        //get config
                        urlConfig = string.Format("https://webservice.thaicarecloud.org/buffe-config?token={0}", (string)arr["access_token"]);
                        Console.WriteLine("" + urlConfig);
                        client = new RestClient(urlConfig);
                        response = client.Execute(request);
                        content = response.Content;
                        if (content != "")
                        {
                            //save config
                            JObject valConfig = JObject.Parse(content);
                            Properties.Settings.Default.valConfig = valConfig;
                            AppendText("Webservice : Load Webservice config success");

                            //load constants
                            urlConst = string.Format("{0}/buffe-constants?token={1}",
                                (string)Properties.Settings.Default.valConfig["service_url"],
                                (string)arr["access_token"]);
                            Console.WriteLine("" + urlConst);
                            client = new RestClient(urlConst);
                            response = client.Execute(request);
                            content = response.Content;
                            if (content != "")
                            {
                                JArray valConst = JArray.Parse(content);
                                Properties.Settings.Default.valConst = valConst; //constant;
                                //replace constant
                                int num = 0;
                                foreach (var val in Properties.Settings.Default.valConst)
                                {
                                    if (val["id"].ToString() == "_SECRETKEY_")
                                    {
                                        Properties.Settings.Default.valConst[num]["value"] = sha256_hash(txtsecretKey.Text);
                                    }
                                    num++;
                                }
                                AppendText("Webservice : Load Webservice constant success");


                                urlPing = string.Format("{0}/buffe-config/ping?token={1}",
                                    (string)Properties.Settings.Default.valConfig["service_url"],
                                    (string)arr["access_token"]);
                                Console.WriteLine("" + urlPing);
                                client = new RestClient(urlPing);
                                response = client.Execute(request);
                                content = response.Content;
                                if (content != "")
                                {
                                    JObject valPing = JObject.Parse(content);
                                    Properties.Settings.Default.valPing = valPing;
                                    AppendText("Webservice : Load Webservice check ping connection success");
                                }
                            }
                            else
                            {
                                MessageBox.Show("รับค่า Constant จาก Webservice ไม่สำเร็จกรุณา Login ใหม่");
                                btnLogin.Enabled = true;
                                txtUserLogin.Enabled = true;
                                txtPassLogin.Enabled = true;
                                btnLogin.Text = "Login";
                            }

                            //list vendor
                            urlConst = string.Format("{0}/buffe-constants/his-type?token={1}",
                                (string)Properties.Settings.Default.valConfig["service_url"],
                                (string)arr["access_token"]);
                            Console.WriteLine("" + urlConst);
                            client = new RestClient(urlConst);
                            response = client.Execute(request);
                            content = response.Content;
                            if (content != "")
                            {
                                JArray valConst = JArray.Parse(content);
                                Properties.Settings.Default.valHistype = valConst; //constant;
                                listVendor.DisplayMember = "Text";
                                listVendor.ValueMember = "Value";
                                List<Object> items = new List<Object>();
                                listVendor.Items.Clear();
                                string selectTxt = "";
                                foreach (var val in valConst)
                                {
                                    items.Add(new { Text = val["his_name"].ToString(), Value = val["id"].ToString() });
                                    //listVendor.Items.Add(new { Text = val["his_name"].ToString(), Value = val["id"].ToString() });
                                    if (val["id"].ToString() == Properties.Settings.Default.hisVendor.ToString())
                                    {
                                        selectTxt = val["id"].ToString();
                                    }
                                }
                                listVendor.DataSource = items;
                                listVendor.SelectedValue = selectTxt;
                                Console.WriteLine(items.ToString());
                            }
                            else
                            {
                                MessageBox.Show("รับค่า Vendor จาก Webservice ไม่สำเร็จกรุณา Login ใหม่");
                                btnLogin.Enabled = true;
                                txtUserLogin.Enabled = true;
                                txtPassLogin.Enabled = true;
                                btnLogin.Text = "Login";
                            }

                            //
                            btnLogin.Enabled = true;
                            txtUserLogin.Enabled = false;
                            txtPassLogin.Enabled = false;
                            btnLogin.Text = "Logout";
                            btnStart.Visible = true;
                            //
                            txtHost.Enabled = true;
                            txtUsername.Enabled = true;
                            txtPassword.Enabled = true;
                            txtPort.Enabled = true;
                            groupBox3.Enabled = true;
                            //
                        }
                        else
                        {
                            MessageBox.Show("รับค่า Config จาก Webservice ไม่สำเร็จกรุณา Login ใหม่");
                            btnLogin.Enabled = true;
                            txtUserLogin.Enabled = true;
                            txtPassLogin.Enabled = true;
                            btnLogin.Text = "Login";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("เชื่อมต่อ Webservice ไม่สำเร็จกรุณา Login ใหม่");
                    btnLogin.Enabled = true;
                    txtUserLogin.Enabled = true;
                    txtPassLogin.Enabled = true;
                    btnLogin.Text = "Login";
                }
            }
            else if (btnLogin.Text == "Logout")
            {
                timer1_config.Stop();
                timer2_constant.Stop();
                timer3_command.Stop();
                timer4_syncdata.Stop();
                timer5_ping.Stop();
                btnStart.Text = "Start";
                //
                txtUserLogin.ResetText();
                txtPassLogin.ResetText();
                txtHost.ResetText();
                txtUsername.ResetText();
                txtPassword.ResetText();
                txtPort.Text = "3306";
                connect_db.Text = "Connect";
                listDB.Text = "select database...";
                listVendor.Text = "select vendor...";
                infoName.ResetText();
                infoSite.ResetText();
                txtLog.ResetText();
                txtsecretKey.ResetText();
                //
                groupBox3.Enabled = false;
                listDB.Enabled = false;
                listVendor.Enabled = false;
                txtsecretKey.Enabled = false;
                btnStart.Visible = false;
                //
                btnLogin.Enabled = true;
                txtUserLogin.Enabled = true;
                txtPassLogin.Enabled = true;
                btnLogin.Text = "Login";
                Properties.Settings.Default.Reset();

            }

        }

        int numRows = 0;
        private void AppendText(string Text)
        {
            //Append the new text to the TextBox
            txtLog.Text += Text + "\n";
            numRows++;

            //If the Frozen function isn't enabled then scroll to the bottom of the TextBox
            if (numRows <= 1000)
            {
                txtLog.SelectionStart = txtLog.Text.Length;
                txtLog.SelectionLength = 0;
                txtLog.ScrollToCaret();
            }
            else { txtLog.Text = ""; numRows = 0; }
        }

        public static String sha256_hash(String value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //chk password
            if (Properties.Settings.Default.hisDBname == null || Properties.Settings.Default.hisDBname == "" || Properties.Settings.Default.hisDBname == "select database...")
            {
                MessageBox.Show("กรุณาเลือก Database");
                return;
            }
            else if (listVendor.SelectedIndex < 0)
            {
                MessageBox.Show("กรุณาเลือก Vendor");
                return;
            }
            else if (txtsecretKey.Text.Length < 1)
            {
                MessageBox.Show("กรุณาระบุรหัสสำหรับใช้เข้ารหัสข้อมูล");
                return;
            }
            else if (txtsecretKey.Text.Length < 6)
            {
                MessageBox.Show("รหัสสำหรับใช้เข้ารหัสข้อมูลควรมากกว่า 6 ตัวอักษร");
                return;
            }
            else if (btnLogin.Text == "Login")
            {
                MessageBox.Show("กรุณา Login เข้าสู่ระบบ Thai Care Cloud");
                return;
            }

            //app runing
            else if (btnStart.Text == "Start")
            {
                timer1_config.Start();
                timer2_constant.Start();
                timer3_command.Start();
                timer4_syncdata.Start();
                timer5_ping.Start();
                btnStart.Text = "Stop!";
                //
                groupBox3.Enabled = false;
                listDB.Enabled = false;
                listVendor.Enabled = false;
                txtsecretKey.Enabled = false;
                btnLogin.Enabled = false;
                //send config
                string urlConfig = string.Format("https://webservice.thaicarecloud.org/buffe-config?token={0}&his_type={1}&buffe_version={2}",
                   (string)Properties.Settings.Default.valLogin["access_token"],
                   (Properties.Settings.Default.hisVendor == "" ? Properties.Settings.Default.valHistype[listVendor.SelectedIndex]["id"].ToString() : Properties.Settings.Default.hisVendor),
                   this.Text);
                Console.WriteLine("" + urlConfig);
                var client = new RestClient(urlConfig);
                var request = new RestRequest(Method.GET);
                var response = client.Execute(request);
                //
                DBCon.Host = Properties.Settings.Default.hisHost;
                DBCon.Username = Properties.Settings.Default.hisUsername;
                DBCon.Password = Properties.Settings.Default.hisPassword;
                DBCon.DatabaseName = Properties.Settings.Default.hisDBname;
                DBCon.Port = Properties.Settings.Default.hisPort;
                try
                {
                    if (DBCon.IsConnect())
                    {
                        //replace constant
                        int num = 0;
                        foreach (var val in Properties.Settings.Default.valConst)
                        {
                            if (val["id"].ToString() == "_SECRETKEY_")
                            {
                                Properties.Settings.Default.valConst[num]["value"] = sha256_hash(txtsecretKey.Text);
                            }
                            num++;
                        }
                        //
                        Properties.Settings.Default.hisVendor = Properties.Settings.Default.valHistype[listVendor.SelectedIndex]["id"].ToString();
                        Properties.Settings.Default.secretKey = txtsecretKey.Text;
                        Properties.Settings.Default.Save();
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Conect DB Error = " + ex.Message);
                    MessageBox.Show(ex.Message);
                    btnStart.Text = "Start";
                    //
                    groupBox3.Enabled = true;
                    listDB.Enabled = true;
                    listVendor.Enabled = true;
                    txtsecretKey.Enabled = true;
                    //
                }
                //end
            }
            else if (btnStart.Text == "Stop!")
            {
                timer1_config.Stop();
                timer2_constant.Stop();
                timer3_command.Stop();
                timer4_syncdata.Stop();
                timer5_ping.Stop();
                btnStart.Text = "Start";
                //
                groupBox3.Enabled = true;
                listDB.Enabled = true;
                listVendor.Enabled = true;
                txtsecretKey.Enabled = true;
                btnLogin.Enabled = true;
                //
            }
        }

        private void AntForce_FormClosed(object sender, FormClosedEventArgs e)
        {
            //kill is unnecessary.  I'd just stick with app exit.
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            Application.Exit();
        }

        private void txtPassLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnLogin.PerformClick();
            }
        }

        private void txtUserLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnLogin.PerformClick();
            }
        }

        private void AntForce_Load(object sender, EventArgs e)
        {
            //
            groupBox3.Enabled = false;
            listDB.Enabled = false;
            listVendor.Enabled = false;
            txtsecretKey.Enabled = false;
            //
            if (Properties.Settings.Default.wsPassword != "")
            {
                toolTip_wsPassword.SetToolTip(this.txtPassLogin, Properties.Settings.Default.wsPassword);
                txtPassLogin.Text = Properties.Settings.Default.wsPassword;
            }
            if (Properties.Settings.Default.wsUsername != "")
            {
                txtUserLogin.Text = Properties.Settings.Default.wsUsername;
            }
            //
            txtHost.Text = Properties.Settings.Default.hisHost;
            txtUsername.Text = Properties.Settings.Default.hisUsername;
            txtPassword.Text = Properties.Settings.Default.hisPassword;
            txtPort.Text = Properties.Settings.Default.hisPort;

            //info
            infoName.Text = Properties.Settings.Default.InfoUser;
            infoSite.Text = Properties.Settings.Default.infoSite;

            //key
            txtsecretKey.Text = Properties.Settings.Default.secretKey;

            if (Properties.Settings.Default.hisDBname != null && Properties.Settings.Default.hisDBname != "")
            {
                listDB.Text = null;
                listDB.SelectedText = Properties.Settings.Default.hisDBname;
            }
            if (Properties.Settings.Default.hisVendor != null && Properties.Settings.Default.hisVendor != "")
            {
                //listVendor.Text = null;
                //listVendor.SelectedText = Properties.Settings.Default.hisVendor;
            }

        }

        private void connect_db_Click(object sender, EventArgs e)
        {
            //Properties.Settings.Default.valConfig["his_ip"] = txtHost.Text;
            //Properties.Settings.Default.valConfig["his_db"] = null;
            //Properties.Settings.Default.valConfig["his_user"] = txtUsername.Text;
            //Properties.Settings.Default.valConfig["his_password"] = txtPassword.Text;
            //Properties.Settings.Default.valConfig["his_port"] = txtPort.Text;

            if (connect_db.Text == "Logout")
            {
                txtHost.Enabled = true;
                txtHost.ResetText();
                txtUsername.Enabled = true;
                txtUsername.ResetText();
                txtPassword.Enabled = true;
                txtPassword.ResetText();
                txtPort.Enabled = true;
                txtPort.ResetText();
                connect_db.Text = "Connect";

                listDB.Enabled = false;
                listVendor.Enabled = false;
                txtsecretKey.Enabled = false;
                btnStart.Enabled = false;

                Properties.Settings.Default.hisHost = null;
                Properties.Settings.Default.hisUsername = null;
                Properties.Settings.Default.hisPassword = null;
                Properties.Settings.Default.hisPort = null;

            }

            DBConnection DBConx = DBConnection.Instance();
            DBConx.Host = txtHost.Text;
            DBConx.Username = txtUsername.Text;
            DBConx.Password = txtPassword.Text;
            DBConx.Port = txtPort.Text;

            try
            {

                if (DBConx.IsConnect())
                {
                    //set db connection
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = DBConx.GetConnection();

                    //select limit 10
                    string query = string.Format("SHOW DATABASES");
                    cmd.CommandText = query;
                    MySqlDataReader dr = cmd.ExecuteReader();

                    listDB.Items.Clear();
                    while (dr.Read())
                    {
                        listDB.Items.Add(dr.GetString(0));
                        /*
                        if (Properties.Settings.Default.hisVendor == null)
                        {
                           
                            if (dr.GetString(0) == "hos")
                            {
                                listDB.Text = null;
                                listDB.SelectedText = dr.GetString(0);
                                listVendor.Text = null;
                                listVendor.SelectedText = "HosXP";
                                Properties.Settings.Default.hisDBname = dr.GetString(0);
                                Properties.Settings.Default.hisVendor = "HosXP";
                            }
                            else if (dr.GetString(0) == "jhcisdb")
                            {
                                listDB.Text = null;
                                listDB.SelectedText = dr.GetString(0);
                                listVendor.Text = null;
                                listVendor.SelectedText = "JHCIS";
                                Properties.Settings.Default.hisDBname = dr.GetString(0);
                                Properties.Settings.Default.hisVendor = "JHCIS";
                            }
                             
                        }*/
                    }
                    dr.Close();

                    MessageBox.Show("Connect Server success!");
                    txtHost.Enabled = false;
                    txtUsername.Enabled = false;
                    txtPassword.Enabled = false;
                    txtPort.Enabled = false;
                    connect_db.Text = "Logout";

                    listDB.Enabled = true;
                    listVendor.Enabled = true;
                    txtsecretKey.Enabled = true;
                    //save setting
                    Properties.Settings.Default.hisHost = txtHost.Text;
                    Properties.Settings.Default.hisUsername = txtUsername.Text;
                    Properties.Settings.Default.hisPassword = txtPassword.Text;
                    Properties.Settings.Default.hisPort = txtPort.Text;
                    Properties.Settings.Default.Save();
                    DBConx.Close();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Conect DB Error = " + ex.Message);
                MessageBox.Show(ex.Message);
                DBConx.Close();
            }

        }

        private void listDB_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.hisDBname = listDB.Text;
            //MessageBox.Show(Properties.Settings.Default.hisDBname);
            Properties.Settings.Default.Save();
        }


    }
}