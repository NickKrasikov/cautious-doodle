using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace DoodleUtil
{
    public class BackupInfo
    {
        public string Version
        {
            get;
            set;
        }
        public DateTime Timestamp
        {
            get;
            set;
        }
    }

    public class DBUtils: BaseUtils, IDisposable
    {
        private SqlConnection m_conn;
        public SqlConnection Connection
        {
            get
            {
                if(m_conn == null)
                {
                    m_conn = new SqlConnection(ConnString);
                }
                if (m_conn.State != System.Data.ConnectionState.Open)
                {
                    m_conn.Open();
                }
                return m_conn;
            }
        }

        public string ConnString
        {
            get;
            set;
        }


        public string BackupDirectory
        {
            get;
            set;
        }

        public string DBPrefix
        {
            get;
            set;
        }

        public DBUtils() : base()
        {
        }


        public DBUtils(TextWriter writer): base(writer)
        {
        }

        public string GetCurrentVersion()
        {
            string version = string.Empty;
            try
            {
                var cmd = Connection.CreateCommand();
                cmd.CommandText = string.Format("SELECT TOP 1 ver FROM(SELECT installDate, hotfixID as ver FROM {0}..tInstallationHistory WHERE hotFixID IS NOT NULL UNION SELECT CAST(0 AS datetime) as installDate, value as ver FROM {0}..tPlatinaSettings WHERE name = N'SYSTEM_VersionNumber' ) h1 ORDER BY installDate DESC", DBPrefix);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        version = rdr[0].ToString();
                    }
                }
            }
            catch (SqlException ex)
            {
                version = "none";
            }
            return version.Replace(" ", "_");
        }

        public List<string> GetDBs()
        {
            var result = new List<string>();
                var cmd = Connection.CreateCommand();
                cmd.CommandText = string.Format("SELECT name FROM sysdatabases WHERE name LIKE '{0}%'", DBPrefix);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        result.Add(rdr[0].ToString());
                    }
                }
            return result;
        }

        public void ExecSql(string sql)
        {
            try
            {
                    var cmd = Connection.CreateCommand();
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                logger.WriteLine("Error executing sql statement.");
                logger.WriteLine("Statement: \"{0}\"", sql);
                logger.WriteLine("Error: \"{0}\"", e.ToString());
            }
        }
        public void BackupDBS()
        {
            string version = GetCurrentVersion();
            if (string.IsNullOrWhiteSpace(version))
            {
                logger.WriteLine("Nothing to backup");
            }
            else
            {
                string backupDir = BackupDirectory;
                if(string.IsNullOrEmpty(backupDir))
                {
                    backupDir = ".";
                }
                if(!backupDir.EndsWith(@"\"))
                {
                    backupDir += @"\";
                }
                backupDir = string.Format("{0}{1}\\{2}", backupDir, version, DateTime.Now.ToString(TimestampFormatString));
                backupDir = Path.GetFullPath(backupDir);
                if (!Directory.Exists(backupDir))
                {
                    logger.WriteLine("Backup directory \"{0}\" doesn't exist. Try to create it.", backupDir);
                    Directory.CreateDirectory(backupDir);
                }
                foreach (var dbName in GetDBs())
                {
                    string fName = string.Format(@"{1}\{0}.bak",dbName,backupDir);
                    logger.WriteLine("Backup database \"{0}\" to file \"{1}\"", dbName, fName);
                    string sql = string.Format(@"BACKUP DATABASE {0} TO DISK = N'{1}' WITH FORMAT, COMPRESSION;", dbName, fName);
                    ExecSql(sql);
                    logger.WriteLine("Done", dbName, fName);
                }
            }
        }

        public List<string> GetLocalVersions()
        {
            var result = new List<string>();
            string backupDir = BackupDirectory;
            if (string.IsNullOrEmpty(backupDir))
            {
                backupDir = ".";
            }
            if (!backupDir.EndsWith(@"\"))
            {
                backupDir += @"\";
            }
            if (!Directory.Exists(backupDir))
            {
                logger.WriteLine("Backup directory \"{0}\" doesn't exist.", backupDir);
                return result;
            }
            foreach (var dir in Directory.GetDirectories(backupDir, "*", SearchOption.TopDirectoryOnly))
            {
                result.Add(dir.Substring(dir.LastIndexOf(@"\") + 1).ToLower());
            }
            return result;
        }

        public List<BackupInfo> GetLocalBackups()
        {
            var result = new List<BackupInfo>();
            string backupDir = BackupDirectory;
            if (string.IsNullOrEmpty(backupDir))
            {
                backupDir = ".";
            }
            if (!backupDir.EndsWith(@"\"))
            {
                backupDir += @"\";
            }
            if (!Directory.Exists(backupDir))
            {
                logger.WriteLine("Backup directory \"{0}\" doesn't exist.", backupDir);
                return result;
            }
            foreach (var ver in Directory.GetDirectories(backupDir, "*", SearchOption.TopDirectoryOnly))
            {
                string sVer = ver.Substring(ver.LastIndexOf(@"\") + 1).ToLower();
                foreach (var ts in Directory.GetDirectories(ver, "*", SearchOption.TopDirectoryOnly))
                {
                    string tmp = ts.Substring(ts.LastIndexOf(@"\") + 1).ToLower();
                    DateTime dt;
                    if (DateTime.TryParseExact(tmp, TimestampFormatString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                    {
                        result.Add(new BackupInfo() { Version = sVer, Timestamp = dt });
                    }
                }
            }
            return result;
        }

        public void KillConnections()
        {
            logger.WriteLine("Kill existing connections.");
            var sb = new StringBuilder();
            sb.AppendLine("DECLARE @SQL AS VARCHAR(255)");
            sb.AppendLine("DECLARE @SPID AS SMALLINT");
            sb.AppendLine("DECLARE Murderer CURSOR FOR");
            sb.AppendLine(string.Format("SELECT spid FROM sys.sysprocesses WHERE DB_NAME(dbid) LIKE N'{0}%'", DBPrefix));
            sb.AppendLine("OPEN Murderer");
            sb.AppendLine("FETCH NEXT FROM Murderer INTO @SPID");
            sb.AppendLine("WHILE @@FETCH_STATUS = 0");
            sb.AppendLine("BEGIN");
            sb.AppendLine("SET @SQL = 'Kill ' + CAST(@SPID AS VARCHAR(10)) + ';'");
            sb.AppendLine("EXEC (@SQL)");
            sb.AppendLine("PRINT  ' Process ' + CAST(@SPID AS VARCHAR(10)) +' has been killed'");
            sb.AppendLine("FETCH NEXT FROM Murderer INTO @SPID");
            sb.AppendLine("END");
            sb.AppendLine("CLOSE Murderer");
            sb.AppendLine("DEALLOCATE Murderer");
            ExecSql(sb.ToString());
            logger.WriteLine("Done.");
        }

        public void DropDB(string dbName)
        {
            logger.WriteLine("Drop database \"{0}\".", dbName);
            ExecSql(string.Format("DROP DATABASE {0}", dbName));
            logger.WriteLine("Done.");
        }

        public void DropDBS()
        {
            KillConnections();
            foreach (var db in GetDBs())
                DropDB(db);
        }

        public void RestoreDB(string dbName, string fileName)
        {
            logger.WriteLine("Restore database \"{0}\" from file \"{1}\"", dbName, fileName);
            ExecSql(string.Format(@"RESTORE DATABASE {0} FROM DISK = '{1}' WITH REPLACE", dbName, fileName));
            logger.WriteLine("Done");
        }

        public void RestoreDBS(string version, DateTime timestamp)
        {
            string backupDir = BackupDirectory;
            if (string.IsNullOrEmpty(backupDir))
            {
                backupDir = ".";
            }
            if (!backupDir.EndsWith(@"\"))
            {
                backupDir += @"\";
            }
            backupDir += version;
            if (!backupDir.EndsWith(@"\"))
            {
                backupDir += @"\";
            }
            backupDir += timestamp.ToString(TimestampFormatString);
            backupDir = Path.GetFullPath(backupDir);
            if (Directory.Exists(backupDir))
            {
                logger.WriteLine("Restore databases from directory \"{0}\"", backupDir);
                KillConnections();
                foreach (var file in Directory.GetFiles(backupDir))
                {
                    if (Path.GetExtension(file) == ".bak")
                    {
                        var dbName = Path.GetFileNameWithoutExtension(file);
                        RestoreDB(dbName, file);
                    }
                }
            }
            else
            {
                logger.WriteLine("Directory \"{0}\" doesn't exist. Nothing to restore.", backupDir);
            }
        }

        public void RestoreDBS(string version)
        {
            string backupDir = BackupDirectory;
            if (string.IsNullOrEmpty(backupDir))
            {
                backupDir = ".";
            }
            if (!backupDir.EndsWith(@"\"))
            {
                backupDir += @"\";
            }
            backupDir += version;
            if(Directory.Exists(backupDir))
            {
                logger.WriteLine("Restore databases from directory \"{0}\"", backupDir);
                KillConnections();
                foreach (var file in Directory.GetFiles(backupDir))
                {
                    if(Path.GetExtension(file) == ".bak")
                    {
                        var dbName = Path.GetFileNameWithoutExtension(file);
                        RestoreDB(dbName, file);
                    }
                }
            }
            else
            {
                logger.WriteLine("Directory \"{0}\" doesn't exist. Nothing to restore.", backupDir);
            }
        }

        public void Dispose()
        {
            if(m_conn != null)
                m_conn.Dispose();
        }
    }
}
