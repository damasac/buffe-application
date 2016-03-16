namespace AntForce
{
    partial class frmTimerConfig
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
            this.txtTimer1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTimer2 = new System.Windows.Forms.TextBox();
            this.txtTimer3 = new System.Windows.Forms.TextBox();
            this.btnSet = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTimer4 = new System.Windows.Forms.TextBox();
            this.txtTimer5 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtTimer1
            // 
            this.txtTimer1.Location = new System.Drawing.Point(192, 31);
            this.txtTimer1.Name = "txtTimer1";
            this.txtTimer1.Size = new System.Drawing.Size(100, 20);
            this.txtTimer1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(77, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Timer Config (interval)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(65, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Timer Constant (interval)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(60, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Timer Command (interval)";
            // 
            // txtTimer2
            // 
            this.txtTimer2.Location = new System.Drawing.Point(192, 62);
            this.txtTimer2.Name = "txtTimer2";
            this.txtTimer2.Size = new System.Drawing.Size(100, 20);
            this.txtTimer2.TabIndex = 4;
            // 
            // txtTimer3
            // 
            this.txtTimer3.Location = new System.Drawing.Point(192, 97);
            this.txtTimer3.Name = "txtTimer3";
            this.txtTimer3.Size = new System.Drawing.Size(100, 20);
            this.txtTimer3.TabIndex = 5;
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(171, 224);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(75, 23);
            this.btnSet.TabIndex = 6;
            this.btnSet.Text = "Set";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Visible = false;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(68, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Timer Transfer (interval)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 171);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(156, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Timer Ping connection (interval)";
            // 
            // txtTimer4
            // 
            this.txtTimer4.Location = new System.Drawing.Point(192, 133);
            this.txtTimer4.Name = "txtTimer4";
            this.txtTimer4.Size = new System.Drawing.Size(100, 20);
            this.txtTimer4.TabIndex = 9;
            // 
            // txtTimer5
            // 
            this.txtTimer5.Location = new System.Drawing.Point(192, 168);
            this.txtTimer5.Name = "txtTimer5";
            this.txtTimer5.Size = new System.Drawing.Size(100, 20);
            this.txtTimer5.TabIndex = 10;
            // 
            // frmTimerConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 288);
            this.Controls.Add(this.txtTimer5);
            this.Controls.Add(this.txtTimer4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.txtTimer3);
            this.Controls.Add(this.txtTimer2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTimer1);
            this.Name = "frmTimerConfig";
            this.Text = "Timer Config";
            this.Load += new System.EventHandler(this.frmTimerConfig_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTimer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTimer2;
        private System.Windows.Forms.TextBox txtTimer3;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTimer4;
        private System.Windows.Forms.TextBox txtTimer5;
    }
}