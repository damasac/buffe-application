using System.Diagnostics;
using System.Reflection;
using System;
namespace AntForce
{
    partial class AntForce
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AntForce));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPassLogin = new System.Windows.Forms.TextBox();
            this.txtUserLogin = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblQleft = new System.Windows.Forms.Label();
            this.lbCpu = new System.Windows.Forms.Label();
            this.infoSite = new System.Windows.Forms.Label();
            this.infoName = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.connect_db = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtsecretKey = new System.Windows.Forms.TextBox();
            this.listVendor = new System.Windows.Forms.ComboBox();
            this.listDB = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpTopicsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.timer1_config = new System.Windows.Forms.Timer(this.components);
            this.timer2_constant = new System.Windows.Forms.Timer(this.components);
            this.timer3_command = new System.Windows.Forms.Timer(this.components);
            this.timer4_syncdata = new System.Windows.Forms.Timer(this.components);
            this.timer5_ping = new System.Windows.Forms.Timer(this.components);
            this.toolTip_wsHospName = new System.Windows.Forms.ToolTip(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLogin);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtPassLogin);
            this.groupBox1.Controls.Add(this.txtUserLogin);
            this.groupBox1.Location = new System.Drawing.Point(12, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(237, 111);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thai Care Cloud";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(70, 73);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username";
            // 
            // txtPassLogin
            // 
            this.txtPassLogin.Location = new System.Drawing.Point(70, 46);
            this.txtPassLogin.Name = "txtPassLogin";
            this.txtPassLogin.PasswordChar = '*';
            this.txtPassLogin.Size = new System.Drawing.Size(137, 20);
            this.txtPassLogin.TabIndex = 1;
            this.txtPassLogin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassLogin_KeyPress);
            // 
            // txtUserLogin
            // 
            this.txtUserLogin.Location = new System.Drawing.Point(70, 19);
            this.txtUserLogin.Name = "txtUserLogin";
            this.txtUserLogin.Size = new System.Drawing.Size(137, 20);
            this.txtUserLogin.TabIndex = 0;
            this.txtUserLogin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUserLogin_KeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblQleft);
            this.groupBox2.Controls.Add(this.lbCpu);
            this.groupBox2.Controls.Add(this.infoSite);
            this.groupBox2.Controls.Add(this.infoName);
            this.groupBox2.Location = new System.Drawing.Point(265, 41);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(315, 111);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Welcome";
            // 
            // lblQleft
            // 
            this.lblQleft.AutoSize = true;
            this.lblQleft.Location = new System.Drawing.Point(6, 69);
            this.lblQleft.Name = "lblQleft";
            this.lblQleft.Size = new System.Drawing.Size(96, 13);
            this.lblQleft.TabIndex = 3;
            this.lblQleft.Text = "จำนวนคิวที่เหลือ :  ";
            // 
            // lbCpu
            // 
            this.lbCpu.AutoSize = true;
            this.lbCpu.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbCpu.Location = new System.Drawing.Point(6, 90);
            this.lbCpu.Name = "lbCpu";
            this.lbCpu.Size = new System.Drawing.Size(60, 13);
            this.lbCpu.TabIndex = 2;
            this.lbCpu.Text = "CPU time : ";
            // 
            // infoSite
            // 
            this.infoSite.AutoSize = true;
            this.infoSite.Location = new System.Drawing.Point(6, 46);
            this.infoSite.Name = "infoSite";
            this.infoSite.Size = new System.Drawing.Size(70, 13);
            this.infoSite.TabIndex = 1;
            this.infoSite.Text = "สถานบริการ :";
            // 
            // infoName
            // 
            this.infoName.AutoSize = true;
            this.infoName.Location = new System.Drawing.Point(6, 23);
            this.infoName.Name = "infoName";
            this.infoName.Size = new System.Drawing.Size(29, 13);
            this.infoName.TabIndex = 0;
            this.infoName.Text = "ชื่อ : ";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.connect_db);
            this.groupBox3.Controls.Add(this.txtPort);
            this.groupBox3.Controls.Add(this.txtPassword);
            this.groupBox3.Controls.Add(this.txtUsername);
            this.groupBox3.Controls.Add(this.txtHost);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(12, 242);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(237, 161);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Health Information System";
            // 
            // connect_db
            // 
            this.connect_db.Location = new System.Drawing.Point(70, 123);
            this.connect_db.Name = "connect_db";
            this.connect_db.Size = new System.Drawing.Size(75, 23);
            this.connect_db.TabIndex = 8;
            this.connect_db.Text = "Connect";
            this.connect_db.UseVisualStyleBackColor = true;
            this.connect_db.Click += new System.EventHandler(this.connect_db_Click);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(70, 96);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 20);
            this.txtPort.TabIndex = 7;
            this.txtPort.Text = "3306";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(70, 70);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(100, 20);
            this.txtPassword.TabIndex = 6;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(70, 45);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(100, 20);
            this.txtUsername.TabIndex = 5;
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(70, 19);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(100, 20);
            this.txtHost.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Port";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Username";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Host";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.btnStart);
            this.groupBox4.Controls.Add(this.txtsecretKey);
            this.groupBox4.Controls.Add(this.listDB);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Location = new System.Drawing.Point(265, 168);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(315, 235);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Configuration";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 70);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(295, 26);
            this.label10.TabIndex = 7;
            this.label10.Text = "*ตัวเลขหรือภาษาอังกฤษอย่างน้อย 6 หลัก ใช้เป็นกุญแจเข้ารหัส\r\nเลขบัตร ชื่อและสกุล ก" +
    "่อนส่งออกจาก server หน่วยบริการ";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(132, 105);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Visible = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtsecretKey
            // 
            this.txtsecretKey.Location = new System.Drawing.Point(78, 48);
            this.txtsecretKey.Name = "txtsecretKey";
            this.txtsecretKey.Size = new System.Drawing.Size(221, 20);
            this.txtsecretKey.TabIndex = 5;
            // 
            // listVendor
            // 
            this.listVendor.FormattingEnabled = true;
            this.listVendor.Location = new System.Drawing.Point(12, 24);
            this.listVendor.Name = "listVendor";
            this.listVendor.Size = new System.Drawing.Size(219, 21);
            this.listVendor.TabIndex = 4;
            this.listVendor.Text = "Select HIS type...";
            // 
            // listDB
            // 
            this.listDB.FormattingEnabled = true;
            this.listDB.Location = new System.Drawing.Point(78, 19);
            this.listDB.Name = "listDB";
            this.listDB.Size = new System.Drawing.Size(221, 21);
            this.listDB.TabIndex = 3;
            this.listDB.Text = "select database...";
            this.listDB.SelectedIndexChanged += new System.EventHandler(this.listDB_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "การเข้ารหัส";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Database";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(593, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.timerConfigToolStripMenuItem,
            this.logoutToolStripMenuItem});
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.settingToolStripMenuItem.Text = "Setting";
            // 
            // timerConfigToolStripMenuItem
            // 
            this.timerConfigToolStripMenuItem.Name = "timerConfigToolStripMenuItem";
            this.timerConfigToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.timerConfigToolStripMenuItem.Text = "Timer config";
            this.timerConfigToolStripMenuItem.Click += new System.EventHandler(this.timerConfigToolStripMenuItem_Click);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.logoutToolStripMenuItem.Text = "Exit";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpTopicsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // helpTopicsToolStripMenuItem
            // 
            this.helpTopicsToolStripMenuItem.Name = "helpTopicsToolStripMenuItem";
            this.helpTopicsToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.helpTopicsToolStripMenuItem.Text = "Help Topics";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(12, 420);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(567, 246);
            this.txtLog.TabIndex = 5;
            this.txtLog.Text = "";
            // 
            // timer1_config
            // 
            this.timer1_config.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2_constant
            // 
            this.timer2_constant.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timer3_command
            // 
            this.timer3_command.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // timer4_syncdata
            // 
            this.timer4_syncdata.Tick += new System.EventHandler(this.timer4_syncdata_Tick);
            // 
            // timer5_ping
            // 
            this.timer5_ping.Tick += new System.EventHandler(this.timer5_ping_Tick);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipText = "zz";
            this.notifyIcon.BalloonTipTitle = "xx";
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Visible = true;
            this.notifyIcon.Click += new System.EventHandler(this.notifyIcon_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.listVendor);
            this.groupBox5.Location = new System.Drawing.Point(12, 168);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(237, 58);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "HIS Type";
            // 
            // AntForce
            // 
            this.ClientSize = new System.Drawing.Size(593, 683);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AntForce";
            this.Text = "TCC Bot - ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AntForce_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AntForce_FormClosed);
            this.Load += new System.EventHandler(this.AntForce_Load);
            this.Resize += new System.EventHandler(this.AntForce_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPassLogin;
        private System.Windows.Forms.TextBox txtUserLogin;
        private System.Windows.Forms.Label infoSite;
        private System.Windows.Forms.Label infoName;
        private System.Windows.Forms.Button connect_db;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtsecretKey;
        private System.Windows.Forms.ComboBox listVendor;
        private System.Windows.Forms.ComboBox listDB;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem timerConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpTopicsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.Timer timer1_config;
        private System.Windows.Forms.Timer timer2_constant;
        private System.Windows.Forms.Timer timer3_command;
        private System.Windows.Forms.Timer timer4_syncdata;
        private System.Windows.Forms.Timer timer5_ping;
        private System.Windows.Forms.ToolTip toolTip_wsHospName;
        private System.Windows.Forms.Label lbCpu;
        private System.Windows.Forms.Label lblQleft;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.GroupBox groupBox5;

        
    }
}

