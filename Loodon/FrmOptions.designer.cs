namespace SQLBackup
{
    partial class frmOptions
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnBrowseLogFileLocation = new System.Windows.Forms.Button();
            this.txtLogFileSaveLocation = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnView = new System.Windows.Forms.Button();
            this.lstLogFiles = new System.Windows.Forms.ListBox();
            this.chkViewLogFilesonExit = new System.Windows.Forms.CheckBox();
            this.chkEnableLogging = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkUpdates = new System.Windows.Forms.CheckBox();
            this.chkRowNumbers = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkPreventDELETE = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkPreventUPDATE = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(2, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(754, 420);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnRefresh);
            this.tabPage1.Controls.Add(this.btnBrowseLogFileLocation);
            this.tabPage1.Controls.Add(this.txtLogFileSaveLocation);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.btnView);
            this.tabPage1.Controls.Add(this.lstLogFiles);
            this.tabPage1.Controls.Add(this.chkViewLogFilesonExit);
            this.tabPage1.Controls.Add(this.chkEnableLogging);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(746, 394);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Logging";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(583, 361);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 8;
            this.btnRefresh.Text = "&Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnBrowseLogFileLocation
            // 
            this.btnBrowseLogFileLocation.Location = new System.Drawing.Point(368, 86);
            this.btnBrowseLogFileLocation.Name = "btnBrowseLogFileLocation";
            this.btnBrowseLogFileLocation.Size = new System.Drawing.Size(30, 23);
            this.btnBrowseLogFileLocation.TabIndex = 7;
            this.btnBrowseLogFileLocation.Text = "...";
            this.btnBrowseLogFileLocation.UseVisualStyleBackColor = true;
            this.btnBrowseLogFileLocation.Click += new System.EventHandler(this.btnBrowseLogFileLocation_Click);
            // 
            // txtLogFileSaveLocation
            // 
            this.txtLogFileSaveLocation.Enabled = false;
            this.txtLogFileSaveLocation.Location = new System.Drawing.Point(9, 88);
            this.txtLogFileSaveLocation.Name = "txtLogFileSaveLocation";
            this.txtLogFileSaveLocation.Size = new System.Drawing.Size(353, 21);
            this.txtLogFileSaveLocation.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Log file save location:";
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(664, 361);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 23);
            this.btnView.TabIndex = 3;
            this.btnView.Text = "&View";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // lstLogFiles
            // 
            this.lstLogFiles.FormattingEnabled = true;
            this.lstLogFiles.Location = new System.Drawing.Point(486, 39);
            this.lstLogFiles.Name = "lstLogFiles";
            this.lstLogFiles.Size = new System.Drawing.Size(253, 316);
            this.lstLogFiles.TabIndex = 2;
            this.lstLogFiles.DoubleClick += new System.EventHandler(this.lstLogFiles_DoubleClick);
            // 
            // chkViewLogFilesonExit
            // 
            this.chkViewLogFilesonExit.AutoSize = true;
            this.chkViewLogFilesonExit.Location = new System.Drawing.Point(6, 39);
            this.chkViewLogFilesonExit.Name = "chkViewLogFilesonExit";
            this.chkViewLogFilesonExit.Size = new System.Drawing.Size(128, 17);
            this.chkViewLogFilesonExit.TabIndex = 1;
            this.chkViewLogFilesonExit.Text = "View Log Files on Exit";
            this.chkViewLogFilesonExit.UseVisualStyleBackColor = true;
            // 
            // chkEnableLogging
            // 
            this.chkEnableLogging.AutoSize = true;
            this.chkEnableLogging.Location = new System.Drawing.Point(6, 16);
            this.chkEnableLogging.Name = "chkEnableLogging";
            this.chkEnableLogging.Size = new System.Drawing.Size(499, 17);
            this.chkEnableLogging.TabIndex = 0;
            this.chkEnableLogging.Text = "Enable Logging (This will log all the Command Executions with result count and ex" +
    "ceptions thrown.)";
            this.chkEnableLogging.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(746, 394);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Others";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkUpdates);
            this.groupBox1.Controls.Add(this.chkRowNumbers);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(740, 388);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Others";
            // 
            // chkUpdates
            // 
            this.chkUpdates.AutoSize = true;
            this.chkUpdates.Location = new System.Drawing.Point(6, 43);
            this.chkUpdates.Name = "chkUpdates";
            this.chkUpdates.Size = new System.Drawing.Size(167, 17);
            this.chkUpdates.TabIndex = 1;
            this.chkUpdates.Text = "Check for updates on startup";
            this.chkUpdates.UseVisualStyleBackColor = true;
            // 
            // chkRowNumbers
            // 
            this.chkRowNumbers.AutoSize = true;
            this.chkRowNumbers.Location = new System.Drawing.Point(6, 20);
            this.chkRowNumbers.Name = "chkRowNumbers";
            this.chkRowNumbers.Size = new System.Drawing.Size(187, 17);
            this.chkRowNumbers.TabIndex = 0;
            this.chkRowNumbers.Text = "Display row numbers in result grid";
            this.chkRowNumbers.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(589, 428);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(670, 428);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(746, 394);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Permissions";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.chkPreventUPDATE);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.chkPreventDELETE);
            this.groupBox2.Location = new System.Drawing.Point(5, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(734, 389);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Grant or Revoke permissions in functionalities execution";
            // 
            // chkPreventDELETE
            // 
            this.chkPreventDELETE.AutoSize = true;
            this.chkPreventDELETE.Location = new System.Drawing.Point(6, 20);
            this.chkPreventDELETE.Name = "chkPreventDELETE";
            this.chkPreventDELETE.Size = new System.Drawing.Size(151, 17);
            this.chkPreventDELETE.TabIndex = 3;
            this.chkPreventDELETE.Text = "Prevent DELETE command";
            this.chkPreventDELETE.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(198, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "All queries including DELETE will not fire.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(201, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "All queries including UPDATE will not fire.";
            // 
            // chkPreventUPDATE
            // 
            this.chkPreventUPDATE.AutoSize = true;
            this.chkPreventUPDATE.Location = new System.Drawing.Point(6, 61);
            this.chkPreventUPDATE.Name = "chkPreventUPDATE";
            this.chkPreventUPDATE.Size = new System.Drawing.Size(154, 17);
            this.chkPreventUPDATE.TabIndex = 5;
            this.chkPreventUPDATE.Text = "Prevent UPDATE command";
            this.chkPreventUPDATE.UseVisualStyleBackColor = true;
            // 
            // frmOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(757, 458);
            this.ControlBox = false;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.frmOptions_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.ListBox lstLogFiles;
        private System.Windows.Forms.CheckBox chkViewLogFilesonExit;
        private System.Windows.Forms.CheckBox chkEnableLogging;
        private System.Windows.Forms.Button btnBrowseLogFileLocation;
        private System.Windows.Forms.TextBox txtLogFileSaveLocation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkRowNumbers;
        private System.Windows.Forms.CheckBox chkUpdates;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkPreventUPDATE;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkPreventDELETE;
    }
}