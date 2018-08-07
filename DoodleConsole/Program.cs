using System;
using System.Collections.Generic;
using System.Linq;

using DoodleUtil;
using System.Configuration;
using System.Data.SqlClient;

namespace DoodleConsole
{
    class Program
    {
        private enum Commands
        {
            Backup,
            Restore,
            Drop,
            List
        }

        private static Commands command;

        private static Dictionary<string, string> ResolveArguments(string[] args)
        {
            var arguments = new Dictionary<string, string>();
            foreach (string argument in args)
            {
                int idx = argument.IndexOf('=');
                if (idx > 0)
                {
                    arguments[argument.Substring(0, idx).ToLower()] = argument.Substring(idx + 1).ToLower();
                }
                else
                {
                    switch (argument.ToLower())
                    {
                        case "backup":
                        case "b":
                            command = Commands.Backup;
                            break;
                        case "restore":
                        case "r":
                            command = Commands.Restore;
                            break;
                        case "drop":
                        case "d":
                            command = Commands.Drop;
                            break;
                        case "list":
                        case "l":
                            command = Commands.List;
                            break;
                    }
                }
            }
            return arguments;
        }

        private static Dictionary<string, string> ArgsDict;

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                ShowUsage();
                return;
            }
            ArgsDict = ResolveArguments(args);
            var dbu = new DBUtils();
            if (!CheckAppConfig())
                return;
            dbu.BackupDirectory = ConfigurationManager.AppSettings["BackupFolder"];
            dbu.ConnString = ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString;
            dbu.DBPrefix = ConfigurationManager.AppSettings["DBPrefix"];
            if (command == Commands.List)
            {
                ShowInfo(dbu);
            }
            if (command == Commands.Backup)
            {
                Backup(dbu);
            }
            if (command == Commands.Restore)
            {
                Restore(dbu);
            }
            if (command == Commands.Drop)
            {
                Drop(dbu);
            }
        }

        private static bool CheckAppConfig()
        {
            string exeName = System.IO.Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            bool res = true;
            if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["BackupFolder"]))
            {
                Console.Out.WriteLine("Couldn't find the value for key=\"BackupFolder\" in <appSettings> section of {0}.config file.", exeName);
                res = false;
            }
            if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["DBPrefix"]))
            {
                Console.Out.WriteLine("Couldn't find the value for key=\"DBPrefix\" in <appSettings> section of {0}.config file.", exeName);
                res = false;
            }
            if (string.IsNullOrWhiteSpace(ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString))
            {
                Console.Out.WriteLine("Couldn't find \"MainConnectionString\" value in  <connectionStrings> section of {0}.config file.", exeName);
                res = false;
            }
            return res;
        }

        private static void Drop(DBUtils dbu)
        {
            var curVer = dbu.GetCurrentVersion();
            if (!curVer.Equals("none", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.Out.Write("Are you sure to drop all \"{0}\" databases? (Y/N)", curVer);
                if (Console.ReadLine().Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                    dbu.DropDBS();
            }
        }

        private static void Backup(DBUtils dbu)
        {
            if (!dbu.GetCurrentVersion().Equals("none", StringComparison.InvariantCultureIgnoreCase))
            {
                dbu.BackupDBS();
            }
        }

        private static void ShowInfo(DBUtils dbu)
        {
            var curVer = dbu.GetCurrentVersion();
            Console.Out.WriteLine("Local backup copies:");
            ICollection<BackupInfo> biColl = new System.Collections.ObjectModel.Collection<BackupInfo>(dbu.GetLocalBackups());
            foreach (var i in biColl.Select(p => p.Version).Distinct())
            {
                Console.Out.WriteLine("\t{0}", i);
            }
            Console.Out.WriteLine("\nCurrently installed:");
            Console.Out.WriteLine("\t{0}", curVer);
        }

        private static void Restore(DBUtils dbu)
        {
            ICollection<BackupInfo> biColl = new System.Collections.ObjectModel.Collection<BackupInfo>(dbu.GetLocalBackups());
            var toVersion = string.Empty;
            if (ArgsDict.Keys.Contains("ver"))
            {
                toVersion = ArgsDict["ver"];
            }
            var tsFilter = string.Empty;
            if (ArgsDict.Keys.Contains("ts"))
            {
                tsFilter = ArgsDict["ts"];
            }
            while (string.IsNullOrEmpty(toVersion))
            {
                Console.Out.WriteLine("Local versions:");
                foreach (var i in biColl.Select(p => p.Version).Distinct())
                {
                    Console.Out.WriteLine("\t{0}",i);
                }
                Console.Out.Write("Enter version to restore (or \"exit\" to exit):");
                var res = Console.ReadLine();
                if (res == "exit")
                    return;
                var bi = biColl.Where(c => c.Version.Equals(res)).FirstOrDefault();
                if(bi != null)
                {
                    toVersion = bi.Version;
                }
            }
            DateTime ts = DateTime.MinValue;
            Dictionary<int, DateTime> tss = new Dictionary<int, DateTime>();
            int num = 0;
            foreach(var bi in biColl.Where(c => c.Version.Equals(toVersion)).OrderByDescending(p => p.Timestamp))
            {
                if(string.IsNullOrWhiteSpace(tsFilter) || bi.Timestamp.ToString(BaseUtils.TimestampFormatString).StartsWith(tsFilter,StringComparison.InvariantCultureIgnoreCase))
                    tss.Add(num++, bi.Timestamp);
            }
            while(ts == DateTime.MinValue)
            {
                Console.Out.WriteLine("Local backups for version {0}:", toVersion);
                foreach (var k in tss.Keys)
                {
                    Console.Out.WriteLine("{0,3:D}\t{1}", k, tss[k]);
                }
                Console.Out.Write("Enter backup number to restore (or \"exit\" to exit):");
                var res = Console.ReadLine();
                if (res == "exit")
                    return;
                num = -1;
                if(int.TryParse(res, out num) && tss.Keys.Contains(num))
                {
                    ts = tss[num];
                }
            }
            if(!dbu.GetCurrentVersion().Equals("none", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.Out.Write("You are going to replace current version \"{0}\" with version \"{1} ({2})\".\nContinue(Y/N)?", dbu.GetCurrentVersion(), toVersion, ts);
                var ans = Console.ReadLine();
                if (ans != "Y" && ans != "y")
                    return;
                Console.Out.Write("Backup current version \"{0}\" before(Y/N)?", dbu.GetCurrentVersion());
                ans = Console.ReadLine();
                if (ans == "Y" || ans == "y")
                {
                    dbu.BackupDBS();
                }
            }
            dbu.RestoreDBS(toVersion, ts);
        }

        private static void ShowUsage()
        {
            Console.Out.WriteLine("Usage:");
            Console.Out.WriteLine("{1} <command> [ver=<version>] [ts=<{0}>]", BaseUtils.TimestampFormatString, System.IO.Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName));
            Console.Out.WriteLine("Supported commands:");
            Console.Out.WriteLine("\tbackup: backup all databases to version folder");
            Console.Out.WriteLine("\tlist: display all local backups and installed version");
            Console.Out.WriteLine("\trestore: backup all currently installed databases to version folder, then restore all databases from specified version folder");
            Console.Out.WriteLine("\tdrop: backup all currently installed databases to current version folder, then drop them");
        }
    }
}
