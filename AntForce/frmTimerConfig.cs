using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AntForce
{
    public partial class frmTimerConfig : Form
    {
        public frmTimerConfig()
        {
            InitializeComponent();
        }

        private void frmTimerConfig_Load(object sender, EventArgs e)
        {
            txtTimer1.Text = (string)Properties.Settings.Default.valConfig["config_delay"];
            txtTimer2.Text = (string)Properties.Settings.Default.valConfig["constants_delay"];
            txtTimer3.Text = (string)Properties.Settings.Default.valConfig["command_delay"];
            txtTimer4.Text = (string)Properties.Settings.Default.valConfig["sync_delay"];
            txtTimer5.Text = (string)Properties.Settings.Default.valConfig["template_delay"];
        
            txtTimer1.Enabled = false;
            txtTimer2.Enabled = false;
            txtTimer3.Enabled = false;
            txtTimer4.Enabled = false;
            txtTimer5.Enabled = false;
            btnSet.Enabled = false;
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.valConfig["config_delay"] = txtTimer1.Text;
            Properties.Settings.Default.valConfig["constants_delay"] = txtTimer2.Text;
            Properties.Settings.Default.valConfig["command_delay"] = txtTimer3.Text;
            Properties.Settings.Default.valConfig["sync_delay"] = txtTimer4.Text;
            Properties.Settings.Default.valConfig["template_delay"] = txtTimer5.Text;
            Properties.Settings.Default.Save(); // Saves settings in application configuration file
        }
    }
}
