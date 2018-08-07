namespace Doodle
{
    partial class MainForm
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
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.txtDBPrefix = new System.Windows.Forms.TextBox();
            this.lblDBPrefix = new System.Windows.Forms.Label();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.txtBackupDirectory = new System.Windows.Forms.TextBox();
            this.lblBackupDirectory = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtDBUser = new System.Windows.Forms.TextBox();
            this.lblDBUser = new System.Windows.Forms.Label();
            this.txtDBServer = new System.Windows.Forms.TextBox();
            this.lblDBServer = new System.Windows.Forms.Label();
            this.tabBackup = new System.Windows.Forms.TabPage();
            this.btnBackup = new System.Windows.Forms.Button();
            this.btnShowCurrentVersion = new System.Windows.Forms.Button();
            this.tabRestore = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRestore = new System.Windows.Forms.Button();
            this.btnCurrentVersion = new System.Windows.Forms.Button();
            this.tabConsole = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnClearConsole = new System.Windows.Forms.Button();
            this.dgvVersions = new System.Windows.Forms.DataGridView();
            this.versionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timestampDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.backupInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tcMain.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.tabBackup.SuspendLayout();
            this.tabRestore.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabConsole.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVersions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.backupInfoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tabSettings);
            this.tcMain.Controls.Add(this.tabBackup);
            this.tcMain.Controls.Add(this.tabRestore);
            this.tcMain.Controls.Add(this.tabConsole);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(671, 376);
            this.tcMain.TabIndex = 0;
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.txtDBPrefix);
            this.tabSettings.Controls.Add(this.lblDBPrefix);
            this.tabSettings.Controls.Add(this.btnSaveSettings);
            this.tabSettings.Controls.Add(this.txtBackupDirectory);
            this.tabSettings.Controls.Add(this.lblBackupDirectory);
            this.tabSettings.Controls.Add(this.txtPassword);
            this.tabSettings.Controls.Add(this.lblPassword);
            this.tabSettings.Controls.Add(this.txtDBUser);
            this.tabSettings.Controls.Add(this.lblDBUser);
            this.tabSettings.Controls.Add(this.txtDBServer);
            this.tabSettings.Controls.Add(this.lblDBServer);
            this.tabSettings.Location = new System.Drawing.Point(4, 22);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabSettings.Size = new System.Drawing.Size(663, 350);
            this.tabSettings.TabIndex = 2;
            this.tabSettings.Text = "Settings";
            this.tabSettings.UseVisualStyleBackColor = true;
            // 
            // txtDBPrefix
            // 
            this.txtDBPrefix.Location = new System.Drawing.Point(102, 111);
            this.txtDBPrefix.Name = "txtDBPrefix";
            this.txtDBPrefix.Size = new System.Drawing.Size(197, 20);
            this.txtDBPrefix.TabIndex = 5;
            this.txtDBPrefix.TextChanged += new System.EventHandler(this.txtSetting_TextChanged);
            // 
            // lblDBPrefix
            // 
            this.lblDBPrefix.AutoSize = true;
            this.lblDBPrefix.Location = new System.Drawing.Point(9, 111);
            this.lblDBPrefix.Name = "lblDBPrefix";
            this.lblDBPrefix.Size = new System.Drawing.Size(85, 13);
            this.lblDBPrefix.TabIndex = 9;
            this.lblDBPrefix.Text = "Database name:";
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.Enabled = false;
            this.btnSaveSettings.Location = new System.Drawing.Point(12, 143);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(75, 23);
            this.btnSaveSettings.TabIndex = 0;
            this.btnSaveSettings.Text = "Save";
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
            // 
            // txtBackupDirectory
            // 
            this.txtBackupDirectory.Location = new System.Drawing.Point(102, 85);
            this.txtBackupDirectory.Name = "txtBackupDirectory";
            this.txtBackupDirectory.Size = new System.Drawing.Size(197, 20);
            this.txtBackupDirectory.TabIndex = 4;
            this.txtBackupDirectory.Text = "E:\\BackupDBs\\";
            this.txtBackupDirectory.TextChanged += new System.EventHandler(this.txtSetting_TextChanged);
            // 
            // lblBackupDirectory
            // 
            this.lblBackupDirectory.AutoSize = true;
            this.lblBackupDirectory.Location = new System.Drawing.Point(9, 85);
            this.lblBackupDirectory.Name = "lblBackupDirectory";
            this.lblBackupDirectory.Size = new System.Drawing.Size(76, 13);
            this.lblBackupDirectory.TabIndex = 6;
            this.lblBackupDirectory.Text = "Backup folder:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(102, 59);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(197, 20);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtSetting_TextChanged);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(9, 59);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "Password:";
            // 
            // txtDBUser
            // 
            this.txtDBUser.Location = new System.Drawing.Point(102, 33);
            this.txtDBUser.Name = "txtDBUser";
            this.txtDBUser.Size = new System.Drawing.Size(197, 20);
            this.txtDBUser.TabIndex = 2;
            this.txtDBUser.Text = "sa";
            this.txtDBUser.TextChanged += new System.EventHandler(this.txtSetting_TextChanged);
            // 
            // lblDBUser
            // 
            this.lblDBUser.AutoSize = true;
            this.lblDBUser.Location = new System.Drawing.Point(9, 33);
            this.lblDBUser.Name = "lblDBUser";
            this.lblDBUser.Size = new System.Drawing.Size(79, 13);
            this.lblDBUser.TabIndex = 2;
            this.lblDBUser.Text = "DB User name:";
            // 
            // txtDBServer
            // 
            this.txtDBServer.Location = new System.Drawing.Point(102, 7);
            this.txtDBServer.Name = "txtDBServer";
            this.txtDBServer.Size = new System.Drawing.Size(197, 20);
            this.txtDBServer.TabIndex = 1;
            this.txtDBServer.Text = "(local)";
            this.txtDBServer.TextChanged += new System.EventHandler(this.txtSetting_TextChanged);
            // 
            // lblDBServer
            // 
            this.lblDBServer.AutoSize = true;
            this.lblDBServer.Location = new System.Drawing.Point(9, 7);
            this.lblDBServer.Name = "lblDBServer";
            this.lblDBServer.Size = new System.Drawing.Size(88, 13);
            this.lblDBServer.TabIndex = 0;
            this.lblDBServer.Text = "DB Server name:";
            // 
            // tabBackup
            // 
            this.tabBackup.Controls.Add(this.btnBackup);
            this.tabBackup.Controls.Add(this.btnShowCurrentVersion);
            this.tabBackup.Location = new System.Drawing.Point(4, 22);
            this.tabBackup.Name = "tabBackup";
            this.tabBackup.Padding = new System.Windows.Forms.Padding(3);
            this.tabBackup.Size = new System.Drawing.Size(663, 350);
            this.tabBackup.TabIndex = 0;
            this.tabBackup.Text = "Backup";
            this.tabBackup.UseVisualStyleBackColor = true;
            // 
            // btnBackup
            // 
            this.btnBackup.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBackup.Location = new System.Drawing.Point(3, 26);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(657, 23);
            this.btnBackup.TabIndex = 1;
            this.btnBackup.Text = "Backup";
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // btnShowCurrentVersion
            // 
            this.btnShowCurrentVersion.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnShowCurrentVersion.Location = new System.Drawing.Point(3, 3);
            this.btnShowCurrentVersion.Name = "btnShowCurrentVersion";
            this.btnShowCurrentVersion.Size = new System.Drawing.Size(657, 23);
            this.btnShowCurrentVersion.TabIndex = 0;
            this.btnShowCurrentVersion.Text = "Current version";
            this.btnShowCurrentVersion.UseVisualStyleBackColor = true;
            this.btnShowCurrentVersion.Click += new System.EventHandler(this.btnShowCurrentVersion_Click);
            // 
            // tabRestore
            // 
            this.tabRestore.Controls.Add(this.panel2);
            this.tabRestore.Controls.Add(this.panel1);
            this.tabRestore.Location = new System.Drawing.Point(4, 22);
            this.tabRestore.Name = "tabRestore";
            this.tabRestore.Size = new System.Drawing.Size(663, 350);
            this.tabRestore.TabIndex = 3;
            this.tabRestore.Text = "Restore";
            this.tabRestore.UseVisualStyleBackColor = true;
            this.tabRestore.Enter += new System.EventHandler(this.tabRestore_Enter);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvVersions);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 49);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(663, 301);
            this.panel2.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRestore);
            this.panel1.Controls.Add(this.btnCurrentVersion);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(663, 49);
            this.panel1.TabIndex = 0;
            // 
            // btnRestore
            // 
            this.btnRestore.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRestore.Enabled = false;
            this.btnRestore.Location = new System.Drawing.Point(0, 23);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(663, 23);
            this.btnRestore.TabIndex = 1;
            this.btnRestore.Text = "Restore";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // btnCurrentVersion
            // 
            this.btnCurrentVersion.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCurrentVersion.Location = new System.Drawing.Point(0, 0);
            this.btnCurrentVersion.Name = "btnCurrentVersion";
            this.btnCurrentVersion.Size = new System.Drawing.Size(663, 23);
            this.btnCurrentVersion.TabIndex = 0;
            this.btnCurrentVersion.Text = "Current version";
            this.btnCurrentVersion.UseVisualStyleBackColor = true;
            this.btnCurrentVersion.Click += new System.EventHandler(this.btnShowCurrentVersion_Click);
            // 
            // tabConsole
            // 
            this.tabConsole.Controls.Add(this.panel4);
            this.tabConsole.Controls.Add(this.panel3);
            this.tabConsole.Location = new System.Drawing.Point(4, 22);
            this.tabConsole.Name = "tabConsole";
            this.tabConsole.Padding = new System.Windows.Forms.Padding(3);
            this.tabConsole.Size = new System.Drawing.Size(663, 350);
            this.tabConsole.TabIndex = 1;
            this.tabConsole.Text = "Console";
            this.tabConsole.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtConsole);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 30);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(657, 317);
            this.panel4.TabIndex = 3;
            // 
            // txtConsole
            // 
            this.txtConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConsole.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConsole.Location = new System.Drawing.Point(0, 0);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ReadOnly = true;
            this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtConsole.Size = new System.Drawing.Size(657, 317);
            this.txtConsole.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnClearConsole);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(657, 27);
            this.panel3.TabIndex = 2;
            // 
            // btnClearConsole
            // 
            this.btnClearConsole.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnClearConsole.Location = new System.Drawing.Point(0, 0);
            this.btnClearConsole.Name = "btnClearConsole";
            this.btnClearConsole.Size = new System.Drawing.Size(657, 23);
            this.btnClearConsole.TabIndex = 2;
            this.btnClearConsole.Text = "Clear";
            this.btnClearConsole.UseVisualStyleBackColor = true;
            this.btnClearConsole.Click += new System.EventHandler(this.btnClearConsole_Click);
            // 
            // dgvVersions
            // 
            this.dgvVersions.AllowUserToAddRows = false;
            this.dgvVersions.AllowUserToDeleteRows = false;
            this.dgvVersions.AllowUserToOrderColumns = true;
            this.dgvVersions.AutoGenerateColumns = false;
            this.dgvVersions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVersions.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvVersions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVersions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.versionDataGridViewTextBoxColumn,
            this.timestampDataGridViewTextBoxColumn});
            this.dgvVersions.DataSource = this.backupInfoBindingSource;
            this.dgvVersions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVersions.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvVersions.Location = new System.Drawing.Point(0, 0);
            this.dgvVersions.MultiSelect = false;
            this.dgvVersions.Name = "dgvVersions";
            this.dgvVersions.RowHeadersVisible = false;
            this.dgvVersions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVersions.Size = new System.Drawing.Size(663, 301);
            this.dgvVersions.TabIndex = 1;
            this.dgvVersions.SelectionChanged += new System.EventHandler(this.dgvVersions_SelectionChanged);
            // 
            // versionDataGridViewTextBoxColumn
            // 
            this.versionDataGridViewTextBoxColumn.DataPropertyName = "Version";
            this.versionDataGridViewTextBoxColumn.HeaderText = "Version";
            this.versionDataGridViewTextBoxColumn.Name = "versionDataGridViewTextBoxColumn";
            // 
            // timestampDataGridViewTextBoxColumn
            // 
            this.timestampDataGridViewTextBoxColumn.DataPropertyName = "Timestamp";
            this.timestampDataGridViewTextBoxColumn.HeaderText = "Timestamp";
            this.timestampDataGridViewTextBoxColumn.Name = "timestampDataGridViewTextBoxColumn";
            // 
            // backupInfoBindingSource
            // 
            this.backupInfoBindingSource.AllowNew = false;
            this.backupInfoBindingSource.DataSource = typeof(DoodleUtil.BackupInfo);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 376);
            this.Controls.Add(this.tcMain);
            this.Name = "MainForm";
            this.Text = "Doodle";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tcMain.ResumeLayout(false);
            this.tabSettings.ResumeLayout(false);
            this.tabSettings.PerformLayout();
            this.tabBackup.ResumeLayout(false);
            this.tabRestore.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tabConsole.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVersions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.backupInfoBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tabBackup;
        private System.Windows.Forms.TabPage tabConsole;
        private System.Windows.Forms.Button btnShowCurrentVersion;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.TabPage tabSettings;
        private System.Windows.Forms.TextBox txtDBServer;
        private System.Windows.Forms.Label lblDBServer;
        private System.Windows.Forms.TextBox txtDBUser;
        private System.Windows.Forms.Label lblDBUser;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtBackupDirectory;
        private System.Windows.Forms.Label lblBackupDirectory;
        private System.Windows.Forms.Button btnSaveSettings;
        private System.Windows.Forms.TabPage tabRestore;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Button btnCurrentVersion;
        private System.Windows.Forms.TextBox txtDBPrefix;
        private System.Windows.Forms.Label lblDBPrefix;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txtConsole;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnClearConsole;
        private System.Windows.Forms.DataGridView dgvVersions;
        private System.Windows.Forms.BindingSource backupInfoBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn versionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timestampDataGridViewTextBoxColumn;
    }
}

