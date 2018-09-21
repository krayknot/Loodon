namespace SQLBackup
{
    partial class frmSync
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbSQLtoSQL = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkLocalSource = new System.Windows.Forms.CheckBox();
            this.btnSConnect = new System.Windows.Forms.Button();
            this.btnTestSConnection = new System.Windows.Forms.Button();
            this.cmbSDatabase = new System.Windows.Forms.ComboBox();
            this.txtSPassword = new System.Windows.Forms.TextBox();
            this.txtSUsername = new System.Windows.Forms.TextBox();
            this.txtSServer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkLocal = new System.Windows.Forms.CheckBox();
            this.btnDConnect = new System.Windows.Forms.Button();
            this.btnTestDConnection = new System.Windows.Forms.Button();
            this.cmbDDatabase = new System.Windows.Forms.ComboBox();
            this.txtDPassword = new System.Windows.Forms.TextBox();
            this.txtDUsername = new System.Windows.Forms.TextBox();
            this.txtDServer = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSourcetoDest = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbSQLtoSQL);
            this.groupBox1.Location = new System.Drawing.Point(2, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(582, 47);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Synchronization Database Options";
            // 
            // rdbSQLtoSQL
            // 
            this.rdbSQLtoSQL.AutoSize = true;
            this.rdbSQLtoSQL.Location = new System.Drawing.Point(10, 20);
            this.rdbSQLtoSQL.Name = "rdbSQLtoSQL";
            this.rdbSQLtoSQL.Size = new System.Drawing.Size(79, 17);
            this.rdbSQLtoSQL.TabIndex = 0;
            this.rdbSQLtoSQL.TabStop = true;
            this.rdbSQLtoSQL.Text = "SQL to SQL";
            this.rdbSQLtoSQL.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkLocalSource);
            this.groupBox2.Controls.Add(this.btnSConnect);
            this.groupBox2.Controls.Add(this.btnTestSConnection);
            this.groupBox2.Controls.Add(this.cmbSDatabase);
            this.groupBox2.Controls.Add(this.txtSPassword);
            this.groupBox2.Controls.Add(this.txtSUsername);
            this.groupBox2.Controls.Add(this.txtSServer);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(2, 50);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(291, 198);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Credentials: Source Database";
            // 
            // chkLocalSource
            // 
            this.chkLocalSource.AutoSize = true;
            this.chkLocalSource.Location = new System.Drawing.Point(75, 126);
            this.chkLocalSource.Name = "chkLocalSource";
            this.chkLocalSource.Size = new System.Drawing.Size(134, 17);
            this.chkLocalSource.TabIndex = 14;
            this.chkLocalSource.Text = "Local Server [ Server ]";
            this.chkLocalSource.UseVisualStyleBackColor = true;
            // 
            // btnSConnect
            // 
            this.btnSConnect.Location = new System.Drawing.Point(226, 71);
            this.btnSConnect.Name = "btnSConnect";
            this.btnSConnect.Size = new System.Drawing.Size(57, 23);
            this.btnSConnect.TabIndex = 11;
            this.btnSConnect.Text = "Connect";
            this.btnSConnect.UseVisualStyleBackColor = true;
            this.btnSConnect.Click += new System.EventHandler(this.btnSConnect_Click);
            // 
            // btnTestSConnection
            // 
            this.btnTestSConnection.Location = new System.Drawing.Point(75, 169);
            this.btnTestSConnection.Name = "btnTestSConnection";
            this.btnTestSConnection.Size = new System.Drawing.Size(124, 23);
            this.btnTestSConnection.TabIndex = 10;
            this.btnTestSConnection.Text = "Test Connection";
            this.btnTestSConnection.UseVisualStyleBackColor = true;
            this.btnTestSConnection.Click += new System.EventHandler(this.btnTestSConnection_Click);
            // 
            // cmbSDatabase
            // 
            this.cmbSDatabase.FormattingEnabled = true;
            this.cmbSDatabase.Location = new System.Drawing.Point(75, 99);
            this.cmbSDatabase.Name = "cmbSDatabase";
            this.cmbSDatabase.Size = new System.Drawing.Size(188, 21);
            this.cmbSDatabase.TabIndex = 7;
            // 
            // txtSPassword
            // 
            this.txtSPassword.Location = new System.Drawing.Point(75, 72);
            this.txtSPassword.Name = "txtSPassword";
            this.txtSPassword.PasswordChar = '*';
            this.txtSPassword.Size = new System.Drawing.Size(148, 21);
            this.txtSPassword.TabIndex = 6;
            // 
            // txtSUsername
            // 
            this.txtSUsername.Location = new System.Drawing.Point(75, 48);
            this.txtSUsername.Name = "txtSUsername";
            this.txtSUsername.Size = new System.Drawing.Size(148, 21);
            this.txtSUsername.TabIndex = 5;
            // 
            // txtSServer
            // 
            this.txtSServer.Location = new System.Drawing.Point(75, 24);
            this.txtSServer.Name = "txtSServer";
            this.txtSServer.Size = new System.Drawing.Size(209, 21);
            this.txtSServer.TabIndex = 4;
            this.txtSServer.TextChanged += new System.EventHandler(this.txtSServer_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Database:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Password:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Username:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkLocal);
            this.groupBox3.Controls.Add(this.btnDConnect);
            this.groupBox3.Controls.Add(this.btnTestDConnection);
            this.groupBox3.Controls.Add(this.cmbDDatabase);
            this.groupBox3.Controls.Add(this.txtDPassword);
            this.groupBox3.Controls.Add(this.txtDUsername);
            this.groupBox3.Controls.Add(this.txtDServer);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Location = new System.Drawing.Point(293, 50);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(291, 198);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Credentials: Destination Database";
            // 
            // chkLocal
            // 
            this.chkLocal.AutoSize = true;
            this.chkLocal.Location = new System.Drawing.Point(75, 126);
            this.chkLocal.Name = "chkLocal";
            this.chkLocal.Size = new System.Drawing.Size(156, 17);
            this.chkLocal.TabIndex = 13;
            this.chkLocal.Text = "Local Server [ Destination ]";
            this.chkLocal.UseVisualStyleBackColor = true;
            // 
            // btnDConnect
            // 
            this.btnDConnect.Location = new System.Drawing.Point(227, 71);
            this.btnDConnect.Name = "btnDConnect";
            this.btnDConnect.Size = new System.Drawing.Size(57, 23);
            this.btnDConnect.TabIndex = 12;
            this.btnDConnect.Text = "Connect";
            this.btnDConnect.UseVisualStyleBackColor = true;
            this.btnDConnect.Click += new System.EventHandler(this.btnDConnect_Click);
            // 
            // btnTestDConnection
            // 
            this.btnTestDConnection.Location = new System.Drawing.Point(75, 169);
            this.btnTestDConnection.Name = "btnTestDConnection";
            this.btnTestDConnection.Size = new System.Drawing.Size(124, 23);
            this.btnTestDConnection.TabIndex = 10;
            this.btnTestDConnection.Text = "Test Connection";
            this.btnTestDConnection.UseVisualStyleBackColor = true;
            this.btnTestDConnection.Click += new System.EventHandler(this.btnTestDConnection_Click);
            // 
            // cmbDDatabase
            // 
            this.cmbDDatabase.FormattingEnabled = true;
            this.cmbDDatabase.Location = new System.Drawing.Point(75, 99);
            this.cmbDDatabase.Name = "cmbDDatabase";
            this.cmbDDatabase.Size = new System.Drawing.Size(188, 21);
            this.cmbDDatabase.TabIndex = 7;
            // 
            // txtDPassword
            // 
            this.txtDPassword.Location = new System.Drawing.Point(75, 72);
            this.txtDPassword.Name = "txtDPassword";
            this.txtDPassword.PasswordChar = '*';
            this.txtDPassword.Size = new System.Drawing.Size(148, 21);
            this.txtDPassword.TabIndex = 6;
            // 
            // txtDUsername
            // 
            this.txtDUsername.Location = new System.Drawing.Point(75, 48);
            this.txtDUsername.Name = "txtDUsername";
            this.txtDUsername.Size = new System.Drawing.Size(148, 21);
            this.txtDUsername.TabIndex = 5;
            // 
            // txtDServer
            // 
            this.txtDServer.Location = new System.Drawing.Point(75, 24);
            this.txtDServer.Name = "txtDServer";
            this.txtDServer.Size = new System.Drawing.Size(209, 21);
            this.txtDServer.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Database:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Password:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Username:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Server:";
            // 
            // btnSourcetoDest
            // 
            this.btnSourcetoDest.Location = new System.Drawing.Point(281, 254);
            this.btnSourcetoDest.Name = "btnSourcetoDest";
            this.btnSourcetoDest.Size = new System.Drawing.Size(211, 23);
            this.btnSourcetoDest.TabIndex = 9;
            this.btnSourcetoDest.Text = "Sync: &Source to Destination";
            this.btnSourcetoDest.UseVisualStyleBackColor = true;
            this.btnSourcetoDest.Click += new System.EventHandler(this.btnSourcetoDest_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(499, 254);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmSync
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(586, 286);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSourcetoDest);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSync";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Loodon: Data Sync";
            this.Load += new System.EventHandler(this.frmSync_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbSQLtoSQL;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbSDatabase;
        private System.Windows.Forms.TextBox txtSPassword;
        private System.Windows.Forms.TextBox txtSUsername;
        private System.Windows.Forms.TextBox txtSServer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbDDatabase;
        private System.Windows.Forms.TextBox txtDPassword;
        private System.Windows.Forms.TextBox txtDUsername;
        private System.Windows.Forms.TextBox txtDServer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSourcetoDest;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnTestSConnection;
        private System.Windows.Forms.Button btnTestDConnection;
        private System.Windows.Forms.Button btnSConnect;
        private System.Windows.Forms.Button btnDConnect;
        private System.Windows.Forms.CheckBox chkLocal;
        private System.Windows.Forms.CheckBox chkLocalSource;
    }
}