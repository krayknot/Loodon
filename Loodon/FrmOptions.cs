using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Loodon;

namespace SQLBackup
{
    public partial class frmOptions : Form
    {
        public frmOptions()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveLoggingSettings();
                SaveOthersSettings();
                SavePermissionSettings();

                MessageBox.Show("Some Settings will take effect on restarting Loodon", "Options", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Please 'Run As Administrator' to save the settings.", "Options", MessageBoxButtons.OK, MessageBoxIcon.Information);               
                
            }
            
        }

        private void SavePermissionSettings()
        {
            ClsCommon common = new ClsCommon();

            if (chkPreventDELETE.CheckState == CheckState.Checked)
            {
                common.Write("PREVENTDELETE", "TRUE");
            }
            else
            {
                common.Write("PREVENTDELETE", "FALSE");
            }

            if (chkPreventUPDATE.CheckState == CheckState.Checked)
            {
                common.Write("PREVENTUPDATE", "TRUE");
            }
            else
            {
                common.Write("PREVENTUPDATE", "FALSE");
            }            
        }

        private void SaveOthersSettings()
        {
            ClsCommon common = new ClsCommon();

            if (chkRowNumbers.CheckState == CheckState.Checked)
            {
                common.Write("ROWNUMBERS", "TRUE");
            }
            else
            {
                common.Write("ROWNUMBERS", "FALSE");
            }

            if (chkUpdates.CheckState == CheckState.Checked)
            {
                common.Write("UPDATES", "TRUE");
            }
            else
            {
                common.Write("UPDATES", "FALSE");
            }
        }

        private void SaveLoggingSettings()
        {
            ClsCommon common = new ClsCommon();

            if (chkEnableLogging.CheckState == CheckState.Checked)
            {
                common.Write("ENABLELOGGING", "TRUE");
            }
            else
            {
                common.Write("ENABLELOGGING", "FALSE");
            }

            if (chkViewLogFilesonExit.CheckState == CheckState.Checked)
            {
                common.Write("VIEWLOGFILESONEXIT", "TRUE");
            }
            else
            {
                common.Write("VIEWLOGFILESONEXIT", "FALSE");
            }

            common.Write("LOGFILESAVELOCATION", txtLogFileSaveLocation.Text);                      


        }

        private void btnBrowseLogFileLocation_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            fbd.ShowNewFolderButton = true;

            fbd.ShowDialog();
            txtLogFileSaveLocation.Text = fbd.SelectedPath;

            btnRefresh_Click(sender, e);
        }

        private void frmOptions_Load(object sender, EventArgs e)
        {
            LoadLogSettings(sender,e);
            LoadOthersSettings();
            LoadPermissionSettings();
        }

        private void LoadPermissionSettings()
        {
            ClsCommon common = new ClsCommon();

            if (common.Read("PREVENTDELETE") == "TRUE")
            {
                chkPreventDELETE.CheckState = CheckState.Checked;
            }
            else
            {
                chkPreventDELETE.CheckState = CheckState.Unchecked;
            }

            if (common.Read("PREVENTUPDATE") == "TRUE")
            {
                chkPreventUPDATE.CheckState = CheckState.Checked;
            }
            else
            {
                chkPreventUPDATE.CheckState = CheckState.Unchecked;
            }
        }

        private void LoadOthersSettings()
        {
            ClsCommon common = new ClsCommon();

            if (common.Read("ROWNUMBERS") == "TRUE")
            {
                chkRowNumbers.CheckState = CheckState.Checked;
            }
            else
            {
                chkRowNumbers.CheckState = CheckState.Unchecked;
            }

            if (common.Read("UPDATES") == "TRUE")
            {
                chkUpdates.CheckState = CheckState.Checked;
            }
            else
            {
                chkUpdates.CheckState = CheckState.Unchecked;
            }
        }

        private void LoadLogSettings(object sender, EventArgs e)
        {
            ClsCommon common = new ClsCommon();

            if (common.Read("ENABLELOGGING") == "TRUE")
            {
                chkEnableLogging.CheckState = CheckState.Checked;
            }
            else
            {
                chkEnableLogging.CheckState = CheckState.Unchecked;
            }

            if (common.Read("VIEWLOGFILESONEXIT") == "TRUE")
            {
                chkViewLogFilesonExit.CheckState = CheckState.Checked;
            }
            else
            {
                chkViewLogFilesonExit.CheckState = CheckState.Unchecked;
            }

            txtLogFileSaveLocation.Text = common.Read("LOGFILESAVELOCATION");

            btnRefresh_Click(sender, e);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (txtLogFileSaveLocation.Text.Length > 0)
            {
                DirectoryInfo dinfo = new DirectoryInfo(txtLogFileSaveLocation.Text);
                FileInfo[] Files = dinfo.GetFiles("*.txt");

                lstLogFiles.Items.Clear();
                foreach (FileInfo file in Files)
                {
                    lstLogFiles.Items.Add(file.Name);
                }

            }
        }

        private void lstLogFiles_DoubleClick(object sender, EventArgs e)
        {
            OpenSelectedFile();
        }

        private void OpenSelectedFile()
        {
            if (lstLogFiles.SelectedItems.Count > 0)
            {
                string filePath = txtLogFileSaveLocation.Text + "/" + lstLogFiles.SelectedItem.ToString();
                System.Diagnostics.Process.Start(filePath);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            OpenSelectedFile();
        }
    }
}
