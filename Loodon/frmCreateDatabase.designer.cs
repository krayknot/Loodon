namespace Loodon
{
    partial class FrmCreateDatabase
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
            this.txtDBName = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.vScrollBar3 = new System.Windows.Forms.VScrollBar();
            this.vScrollBar4 = new System.Windows.Forms.VScrollBar();
            this.txtDFbypercent = new System.Windows.Forms.TextBox();
            this.txtDFinmegabytes = new System.Windows.Forms.TextBox();
            this.radioButton7 = new System.Windows.Forms.RadioButton();
            this.rdbDFinmegabytes = new System.Windows.Forms.RadioButton();
            this.txtLFLDFFilename = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDFDataFilename = new System.Windows.Forms.TextBox();
            this.txtDFMDFFilename = new System.Windows.Forms.TextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.radioButton9 = new System.Windows.Forms.RadioButton();
            this.radioButton10 = new System.Windows.Forms.RadioButton();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.vScrollBar5 = new System.Windows.Forms.VScrollBar();
            this.vScrollBar6 = new System.Windows.Forms.VScrollBar();
            this.txtLFbypercent = new System.Windows.Forms.TextBox();
            this.txtLFinimegabytes = new System.Windows.Forms.TextBox();
            this.radioButton11 = new System.Windows.Forms.RadioButton();
            this.rdbLFinmegabytes = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLFFilename = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtDBName
            // 
            this.txtDBName.Location = new System.Drawing.Point(9, 20);
            this.txtDBName.Name = "txtDBName";
            this.txtDBName.Size = new System.Drawing.Size(273, 21);
            this.txtDBName.TabIndex = 0;
            this.txtDBName.TextChanged += new System.EventHandler(this.txtDBName_TextChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textBox7);
            this.groupBox5.Controls.Add(this.radioButton5);
            this.groupBox5.Controls.Add(this.radioButton6);
            this.groupBox5.Location = new System.Drawing.Point(234, 68);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(256, 88);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Maximum file size";
            // 
            // textBox7
            // 
            this.textBox7.Enabled = false;
            this.textBox7.Location = new System.Drawing.Point(184, 48);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(48, 21);
            this.textBox7.TabIndex = 2;
            this.textBox7.Text = "5000";
            // 
            // radioButton5
            // 
            this.radioButton5.Location = new System.Drawing.Point(24, 48);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(160, 24);
            this.radioButton5.TabIndex = 1;
            this.radioButton5.Text = "Restricted file growth (MB)";
            // 
            // radioButton6
            // 
            this.radioButton6.Checked = true;
            this.radioButton6.Location = new System.Drawing.Point(24, 24);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(144, 16);
            this.radioButton6.TabIndex = 0;
            this.radioButton6.TabStop = true;
            this.radioButton6.Text = "Unrestricted file growth";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.vScrollBar3);
            this.groupBox6.Controls.Add(this.vScrollBar4);
            this.groupBox6.Controls.Add(this.txtDFbypercent);
            this.groupBox6.Controls.Add(this.txtDFinmegabytes);
            this.groupBox6.Controls.Add(this.radioButton7);
            this.groupBox6.Controls.Add(this.rdbDFinmegabytes);
            this.groupBox6.Location = new System.Drawing.Point(6, 68);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(222, 88);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "File growth";
            // 
            // vScrollBar3
            // 
            this.vScrollBar3.Location = new System.Drawing.Point(184, 48);
            this.vScrollBar3.Name = "vScrollBar3";
            this.vScrollBar3.Size = new System.Drawing.Size(16, 16);
            this.vScrollBar3.TabIndex = 5;
            // 
            // vScrollBar4
            // 
            this.vScrollBar4.Enabled = false;
            this.vScrollBar4.Location = new System.Drawing.Point(184, 16);
            this.vScrollBar4.Name = "vScrollBar4";
            this.vScrollBar4.Size = new System.Drawing.Size(16, 16);
            this.vScrollBar4.TabIndex = 4;
            // 
            // txtDFbypercent
            // 
            this.txtDFbypercent.Location = new System.Drawing.Point(128, 48);
            this.txtDFbypercent.Name = "txtDFbypercent";
            this.txtDFbypercent.Size = new System.Drawing.Size(56, 21);
            this.txtDFbypercent.TabIndex = 3;
            this.txtDFbypercent.Text = "10";
            // 
            // txtDFinmegabytes
            // 
            this.txtDFinmegabytes.Enabled = false;
            this.txtDFinmegabytes.Location = new System.Drawing.Point(128, 16);
            this.txtDFinmegabytes.Name = "txtDFinmegabytes";
            this.txtDFinmegabytes.Size = new System.Drawing.Size(56, 21);
            this.txtDFinmegabytes.TabIndex = 2;
            this.txtDFinmegabytes.Text = "1";
            // 
            // radioButton7
            // 
            this.radioButton7.Checked = true;
            this.radioButton7.Location = new System.Drawing.Point(16, 48);
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.Size = new System.Drawing.Size(104, 24);
            this.radioButton7.TabIndex = 1;
            this.radioButton7.TabStop = true;
            this.radioButton7.Text = "By percent";
            // 
            // rdbDFinmegabytes
            // 
            this.rdbDFinmegabytes.Location = new System.Drawing.Point(16, 16);
            this.rdbDFinmegabytes.Name = "rdbDFinmegabytes";
            this.rdbDFinmegabytes.Size = new System.Drawing.Size(104, 24);
            this.rdbDFinmegabytes.TabIndex = 0;
            this.rdbDFinmegabytes.Text = "in megabytes";
            // 
            // txtLFLDFFilename
            // 
            this.txtLFLDFFilename.Location = new System.Drawing.Point(302, 20);
            this.txtLFLDFFilename.Name = "txtLFLDFFilename";
            this.txtLFLDFFilename.Size = new System.Drawing.Size(176, 21);
            this.txtLFLDFFilename.TabIndex = 5;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.txtDBName);
            this.groupBox7.Location = new System.Drawing.Point(3, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(515, 47);
            this.groupBox7.TabIndex = 6;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Database Name";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.tabControl2);
            this.groupBox8.Location = new System.Drawing.Point(3, 51);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(515, 210);
            this.groupBox8.TabIndex = 7;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Data File | Log File Details";
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Location = new System.Drawing.Point(5, 17);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(504, 188);
            this.tabControl2.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox5);
            this.tabPage4.Controls.Add(this.groupBox6);
            this.tabPage4.Controls.Add(this.groupBox9);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(496, 162);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "Data File";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.label3);
            this.groupBox9.Controls.Add(this.label2);
            this.groupBox9.Controls.Add(this.txtDFDataFilename);
            this.groupBox9.Controls.Add(this.txtDFMDFFilename);
            this.groupBox9.Location = new System.Drawing.Point(6, 8);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(484, 54);
            this.groupBox9.TabIndex = 4;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "File Details";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(227, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "MDF Filename:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "DB Filename:";
            // 
            // txtDFDataFilename
            // 
            this.txtDFDataFilename.Location = new System.Drawing.Point(81, 20);
            this.txtDFDataFilename.Name = "txtDFDataFilename";
            this.txtDFDataFilename.Size = new System.Drawing.Size(112, 21);
            this.txtDFDataFilename.TabIndex = 2;
            // 
            // txtDFMDFFilename
            // 
            this.txtDFMDFFilename.Location = new System.Drawing.Point(306, 20);
            this.txtDFMDFFilename.Name = "txtDFMDFFilename";
            this.txtDFMDFFilename.Size = new System.Drawing.Size(172, 21);
            this.txtDFMDFFilename.TabIndex = 1;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.groupBox10);
            this.tabPage5.Controls.Add(this.groupBox11);
            this.tabPage5.Controls.Add(this.groupBox1);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(496, 162);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "Log File";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.textBox14);
            this.groupBox10.Controls.Add(this.radioButton9);
            this.groupBox10.Controls.Add(this.radioButton10);
            this.groupBox10.Location = new System.Drawing.Point(234, 68);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(256, 88);
            this.groupBox10.TabIndex = 7;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Maximum file size";
            // 
            // textBox14
            // 
            this.textBox14.Enabled = false;
            this.textBox14.Location = new System.Drawing.Point(184, 48);
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new System.Drawing.Size(48, 21);
            this.textBox14.TabIndex = 2;
            this.textBox14.Text = "5000";
            // 
            // radioButton9
            // 
            this.radioButton9.Location = new System.Drawing.Point(24, 48);
            this.radioButton9.Name = "radioButton9";
            this.radioButton9.Size = new System.Drawing.Size(160, 24);
            this.radioButton9.TabIndex = 1;
            this.radioButton9.Text = "Restricted file growth (MB)";
            // 
            // radioButton10
            // 
            this.radioButton10.Checked = true;
            this.radioButton10.Location = new System.Drawing.Point(24, 24);
            this.radioButton10.Name = "radioButton10";
            this.radioButton10.Size = new System.Drawing.Size(144, 16);
            this.radioButton10.TabIndex = 0;
            this.radioButton10.TabStop = true;
            this.radioButton10.Text = "Unrestricted file growth";
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.vScrollBar5);
            this.groupBox11.Controls.Add(this.vScrollBar6);
            this.groupBox11.Controls.Add(this.txtLFbypercent);
            this.groupBox11.Controls.Add(this.txtLFinimegabytes);
            this.groupBox11.Controls.Add(this.radioButton11);
            this.groupBox11.Controls.Add(this.rdbLFinmegabytes);
            this.groupBox11.Location = new System.Drawing.Point(6, 68);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(223, 88);
            this.groupBox11.TabIndex = 6;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "File growth";
            // 
            // vScrollBar5
            // 
            this.vScrollBar5.Location = new System.Drawing.Point(184, 48);
            this.vScrollBar5.Name = "vScrollBar5";
            this.vScrollBar5.Size = new System.Drawing.Size(16, 16);
            this.vScrollBar5.TabIndex = 5;
            // 
            // vScrollBar6
            // 
            this.vScrollBar6.Enabled = false;
            this.vScrollBar6.Location = new System.Drawing.Point(184, 16);
            this.vScrollBar6.Name = "vScrollBar6";
            this.vScrollBar6.Size = new System.Drawing.Size(16, 16);
            this.vScrollBar6.TabIndex = 4;
            // 
            // txtLFbypercent
            // 
            this.txtLFbypercent.Location = new System.Drawing.Point(128, 48);
            this.txtLFbypercent.Name = "txtLFbypercent";
            this.txtLFbypercent.Size = new System.Drawing.Size(56, 21);
            this.txtLFbypercent.TabIndex = 3;
            this.txtLFbypercent.Text = "10";
            // 
            // txtLFinimegabytes
            // 
            this.txtLFinimegabytes.Enabled = false;
            this.txtLFinimegabytes.Location = new System.Drawing.Point(128, 16);
            this.txtLFinimegabytes.Name = "txtLFinimegabytes";
            this.txtLFinimegabytes.Size = new System.Drawing.Size(56, 21);
            this.txtLFinimegabytes.TabIndex = 2;
            this.txtLFinimegabytes.Text = "1";
            // 
            // radioButton11
            // 
            this.radioButton11.Checked = true;
            this.radioButton11.Location = new System.Drawing.Point(16, 48);
            this.radioButton11.Name = "radioButton11";
            this.radioButton11.Size = new System.Drawing.Size(104, 24);
            this.radioButton11.TabIndex = 1;
            this.radioButton11.TabStop = true;
            this.radioButton11.Text = "By percent";
            // 
            // rdbLFinmegabytes
            // 
            this.rdbLFinmegabytes.Location = new System.Drawing.Point(16, 16);
            this.rdbLFinmegabytes.Name = "rdbLFinmegabytes";
            this.rdbLFinmegabytes.Size = new System.Drawing.Size(104, 24);
            this.rdbLFinmegabytes.TabIndex = 0;
            this.rdbLFinmegabytes.Text = "in megabytes";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtLFLDFFilename);
            this.groupBox1.Controls.Add(this.txtLFFilename);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(6, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(484, 54);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "File Details";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(227, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "LDF Filename:";
            // 
            // txtLFFilename
            // 
            this.txtLFFilename.Location = new System.Drawing.Point(81, 20);
            this.txtLFFilename.Name = "txtLFFilename";
            this.txtLFFilename.Size = new System.Drawing.Size(112, 21);
            this.txtLFFilename.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Log Filename:";
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(339, 267);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 8;
            this.btnCreate.Text = "&Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(421, 267);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "&Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmCreateDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(521, 297);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox7);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCreateDatabase";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Loodon: Create Database";
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.VScrollBar vScrollBar3;
        private System.Windows.Forms.VScrollBar vScrollBar4;
        private System.Windows.Forms.TextBox txtDFbypercent;
        private System.Windows.Forms.TextBox txtDFinmegabytes;
        private System.Windows.Forms.RadioButton radioButton7;
        private System.Windows.Forms.RadioButton rdbDFinmegabytes;
        private System.Windows.Forms.TextBox txtLFLDFFilename;
        private System.Windows.Forms.TextBox txtDBName;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.RadioButton radioButton9;
        private System.Windows.Forms.RadioButton radioButton10;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.VScrollBar vScrollBar5;
        private System.Windows.Forms.VScrollBar vScrollBar6;
        private System.Windows.Forms.TextBox txtLFbypercent;
        private System.Windows.Forms.TextBox txtLFinimegabytes;
        private System.Windows.Forms.RadioButton radioButton11;
        private System.Windows.Forms.RadioButton rdbLFinmegabytes;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDFDataFilename;
        private System.Windows.Forms.TextBox txtDFMDFFilename;
        private System.Windows.Forms.TextBox txtLFFilename;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button button2;
    }
}