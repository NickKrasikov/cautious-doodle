using System;
using System.IO;
namespace DoodleUtil
{
    public abstract class BaseUtils
    {
        protected TextWriter logger;

        public const string TimestampFormatString = "yyyy-MM-dd-HH-mm-ss";

        public BaseUtils(): this(Console.Out)
        {
        }
        public BaseUtils(TextWriter writer)
        {
            logger = writer;
        }

    }
}
