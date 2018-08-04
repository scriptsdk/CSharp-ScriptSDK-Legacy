using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptSDK
{
    public static class Logger
    {
        public static void I(object info)
        {
            var output = DateTime.Now.ToLocalTime() + " INFO: " + (string)info;
            Console.WriteLine(output);
            File.AppendAllText("log.txt", output + "\n");
        }

        public static void E(object info)
        {
            var output = DateTime.Now.ToLocalTime() + " ERROR: " + (string)info;
            Console.WriteLine(output);
            File.AppendAllText("log.txt", output + "\n");
        }
    }
}
