using System;
using System.IO;
namespace DoodleUtil
{
    public abstract class BaseUtils
    {
        protected TextWriter logger;

        public BaseUtils(): this(Console.Out)
        {
        }
        public BaseUtils(TextWriter writer)
        {
            logger = writer;
        }

    }
}
