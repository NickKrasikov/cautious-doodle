using System;
using System.IO;
using System.Windows.Forms;

using DoodleUtil;

namespace Doodle
{
    public partial class MainForm : Form
    {
        private TextWriter _writer = null;

        private string ConnString
        {
            get
            {
                return string.Format("Data Source={0};;user id={1};password={2};Initial Catalog=master", txtDBServer.Text, txtDBUser.Text, txtPassword.Text);
            }
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnShowCurrentVersion_Click(object sender, EventArgs e)
        {
            try
            {
                var dbu = new DBUtils();
                using (var conn = new System.Data.SqlClient.SqlConnection(ConnString))
                {
                    dbu.Connection = conn;
                    dbu.BackupDirectory = txtBackupDirectory.Text;
                    dbu.DBPrefix = txtDBPrefix.Text;
                    MessageBox.Show(dbu.GetCurrentVersion());
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void LogException(Exception ex)
        {
            Console.WriteLine("Exception:");
            Console.WriteLine(ex.ToString());
            tcMain.SelectTab(tabConsole);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _writer = new TextBoxStreamWriter(txtConsole);
            Console.SetOut(_writer);
            LoadSettings();
            LoadVersions();
        }

        private void LoadVersions()
        {
            lbVersions.Items.Clear();
            var dbu = new DBUtils();
            dbu.BackupDirectory = txtBackupDirectory.Text;
            foreach (var item in dbu.GetLocalVersions())
            {
                lbVersions.Items.Add(item);
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            try
            { 
                var dbu = new DBUtils();
                using (var conn = new System.Data.SqlClient.SqlConnection(ConnString))
                {
                    dbu.Connection = conn;
                    dbu.BackupDirectory = txtBackupDirectory.Text;
                    dbu.DBPrefix = txtDBPrefix.Text;
                    tcMain.SelectTab(tabConsole);
                    dbu.BackupDBS();
                    LoadVersions();
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }

        }

        private void txtSetting_TextChanged(object sender, EventArgs e)
        {
            btnSaveSettings.Enabled = true;
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            SaveSetting();
            btnSaveSettings.Enabled = false;
        }

        private void SaveSetting()
        {
            DoodleSettings.Default.DBServerString = txtDBServer.Text;
            DoodleSettings.Default.DBUserString = txtDBUser.Text;
            DoodleSettings.Default.DBPasswordString = txtPassword.Text;
            DoodleSettings.Default.BackupDirString = txtBackupDirectory.Text;
            DoodleSettings.Default.DBPrefixString = txtDBPrefix.Text;
            DoodleSettings.Default.Save();
        }

        private void LoadSettings()
        {
            txtDBServer.Text = DoodleSettings.Default.DBServerString;
            txtPassword.Text = DoodleSettings.Default.DBPasswordString;
            txtDBUser.Text = DoodleSettings.Default.DBUserString;
            txtBackupDirectory.Text = DoodleSettings.Default.BackupDirString;
            txtDBPrefix.Text = DoodleSettings.Default.DBPrefixString;
        }


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSetting();
        }

        private void lbVersions_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRestore.Enabled = lbVersions.SelectedIndex != -1;
        }

        private void btnClearConsole_Click(object sender, EventArgs e)
        {
            txtConsole.Text = "";
        }
    }
}
