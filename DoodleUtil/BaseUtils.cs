using System;
using System.IO;
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
        public string WebRootLocalPath
        {
            get;
            set;
        }
    }

    public abstract class BaseUtils
    {
        protected TextWriter logger;

        public const string TimestampFormatString = "yyyy-MM-dd-HH-mm-ss";

        public string BackupDirectory
        {
            get;
            set;
        }

        public BaseUtils(): this(Console.Out)
        {
        }
        public BaseUtils(TextWriter writer)
        {
            logger = writer;
        }

    }
}
