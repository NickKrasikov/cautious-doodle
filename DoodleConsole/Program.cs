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
            dbu.BackupDirectory = ConfigurationManager.AppSettings["BackupFolder"];
            dbu.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString);
            dbu.DBPrefix = ConfigurationManager.AppSettings["DBPrefix"];
            if (command == Commands.List)
            {
                foreach(var item in dbu.GetLocalVersions())
                {
                    Console.Out.WriteLine(item);
                }
            }
            if (command != Commands.List)
            {
                dbu.BackupDBS();
            }
            if (command == Commands.Restore)
            {
                var toVersion = string.Empty;
                if (ArgsDict.Keys.Contains("ver"))
                {
                    toVersion = ArgsDict["ver"];
                    if (dbu.GetLocalVersions().Contains(toVersion))
                    {
                        dbu.RestoreDBS(toVersion);
                    }
                }
            }
            if (command == Commands.Drop)
            {
                dbu.DropDBS();
            }
        }

        private static void ShowUsage()
        {
            Console.Out.WriteLine("Usage:");
            Console.Out.WriteLine("DBUtil.exe backup|restore ver=<version>|drop");
            Console.Out.WriteLine("\tbackup: backup all databases to version folder");
            Console.Out.WriteLine("\trestore: backup all currently installed databases to current version folder, then restore all databases from specified version folder");
            Console.Out.WriteLine("\tdrop: backup all currently installed databases to current version folder, then drop them");
        }
    }
}
