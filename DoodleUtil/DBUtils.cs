using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace DoodleUtil
{
    public class DBUtils: BaseUtils
    {
        public string GetCurrentVersion()
        {
            string version = string.Empty;
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["PlatinaConnString"].ConnectionString))
            {
                if(conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT TOP 1 ver FROM(SELECT installDate, hotfixID as ver FROM platina..tInstallationHistory WHERE hotFixID IS NOT NULL UNION SELECT CAST(0 AS datetime) as installDate, value as ver FROM platina..tPlatinaSettings WHERE name = N'SYSTEM_VersionNumber' ) h1 ORDER BY installDate DESC";
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        version = rdr[0].ToString(); 
                    }
                }
            }
            return version.Replace(" ", "_");
        }

        public List<string> GetPlatinaDBs()
        {
            var result = new List<string>();
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["PlatinaConnString"].ConnectionString))
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT name FROM sysdatabases WHERE name LIKE 'platina%'";
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        result.Add(rdr[0].ToString());
                    }
                }
            }
            return result;
        }

        public void ExecSql(string sql)
        {
            try
            {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["PlatinaConnString"].ConnectionString))
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
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
                string backupDir = ConfigurationManager.AppSettings["BackupFolder"];
                if(string.IsNullOrEmpty(backupDir))
                {
                    backupDir = ".";
                }
                if(!backupDir.EndsWith(@"\"))
                {
                    backupDir += @"\";
                }
                backupDir = string.Format("{0}{1}", backupDir, version);
                if(!Directory.Exists(backupDir))
                {
                    logger.WriteLine("Backup directory \"{0}\" doesn't exist. Try to create it.", backupDir);
                    Directory.CreateDirectory(backupDir);
                }
                foreach (var dbName in GetPlatinaDBs())
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
            string backupDir = ConfigurationManager.AppSettings["BackupFolder"];
            if (string.IsNullOrEmpty(backupDir))
            {
                backupDir = ".";
            }
            if (!backupDir.EndsWith(@"\"))
            {
                backupDir += @"\";
            }
            foreach (var dir in Directory.GetDirectories(backupDir, "*", SearchOption.TopDirectoryOnly))
            {
                result.Add(dir.Substring(dir.LastIndexOf("\\") + 1).ToLower());
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
            sb.AppendLine("SELECT spid FROM sys.sysprocesses WHERE DB_NAME(dbid) LIKE N'platina%'");
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
            foreach (var db in GetPlatinaDBs())
                DropDB(db);
        }

        public void RestoreDB(string dbName, string fileName)
        {
            logger.WriteLine("Restore database \"{0}\" from file \"{1}\"", dbName, fileName);
            ExecSql(string.Format(@"RESTORE DATABASE {0} FROM DISK = '{1}' WITH REPLACE", dbName, fileName));
            logger.WriteLine("Done");
        }

        public void RestoreDBS(string version)
        {
            string backupDir = ConfigurationManager.AppSettings["BackupFolder"];
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

    }
}
