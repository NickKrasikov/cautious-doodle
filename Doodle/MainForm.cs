using System;
using System.IO;
using System.Windows.Forms;

using DoodleUtil;
using System.ComponentModel;
using Subro.Controls;

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

        private DBUtils m_DBUtils;
        protected DBUtils DBUtils
        {
            get
            {
                if (m_DBUtils == null)
                {
                    m_DBUtils = new DBUtils(new TextBoxStreamWriter(txtConsole) as TextWriter);
                }
                m_DBUtils.BackupDirectory = txtBackupDirectory.Text;
                m_DBUtils.DBPrefix = txtDBPrefix.Text;
                m_DBUtils.ConnString = this.ConnString;
                return m_DBUtils;
            }
        }

        public MainForm()
        {
            InitializeComponent();
            dgvVersions.AutoGenerateColumns = true;
            var grouper = new Subro.Controls.DataGridViewGrouper(dgvVersions);
            grouper.SetGroupOn("Version");
            grouper.DisplayGroup += grouper_DisplayGroup;
            grouper.Options.StartCollapsed = true;
            grouper.CollapseAll();

        }

        void grouper_DisplayGroup(object sender, GroupDisplayEventArgs e)
        {
            e.Summary = "contains " + e.Group.Count + " rows";
        }

        private BindingList<BackupInfo> GetVersionsList()
        {
            return new BindingList<BackupInfo>(DBUtils.GetLocalBackups());
        }

        private void btnShowCurrentVersion_Click(object sender, EventArgs e)
        {
            try
            {
                    MessageBox.Show(DBUtils.GetCurrentVersion());
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
            backupInfoBindingSource.DataSource = GetVersionsList();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            try
            { 
                    tcMain.SelectTab(tabConsole);
                    DBUtils.BackupDBS();
                    LoadVersions();
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

        private void btnClearConsole_Click(object sender, EventArgs e)
        {
            txtConsole.Text = "";
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvVersions.CurrentRow != null && dgvVersions.CurrentRow.DataBoundItem != null)
                {
                    BackupInfo bi = dgvVersions.CurrentRow.DataBoundItem as BackupInfo;
                    if (bi != null)
                    {
                        if (MessageBox.Show(string.Format("You are going to replace current version \"{0}\" with version \"{1} ({2})\".\nContinue?", DBUtils.GetCurrentVersion(), bi.Version, bi.Timestamp), "Please confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            DialogResult res = MessageBox.Show("Create backup of current database?", "Please confirm", MessageBoxButtons.YesNoCancel);
                            if (res == DialogResult.Cancel)
                                return;
                            tcMain.SelectTab(tabConsole);
                            if (res == DialogResult.Yes)
                            {
                                DBUtils.BackupDBS();
                            }
                            DBUtils.RestoreDBS(bi.Version, bi.Timestamp);
                            LoadVersions();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private void dgvVersions_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv != null)
            {
                btnRestore.Enabled = (dgv.CurrentRow != null && dgv.CurrentRow.DataBoundItem != null && dgv.CurrentRow.DataBoundItem is BackupInfo);
            }
        }

        private void tabRestore_Enter(object sender, EventArgs e)
        {
            LoadVersions();
        }
    }
}
