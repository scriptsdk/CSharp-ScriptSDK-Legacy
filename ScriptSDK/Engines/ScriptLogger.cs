using System;
using System.Diagnostics;
using System.IO;
using ScriptSDK.Data;
using StealthAPI;

namespace ScriptSDK.Engines
{
    /// <summary>
    /// Scriptlogger class expose handles, actions and properties of logging script content.
    /// </summary>
    public static class ScriptLogger
    {
        /// <summary>
        /// Gets or sets if messages via Write\Writeline are exposed to stealth.
        /// </summary>
        public static bool LogToStealth { get; set; }

        /// <summary>
        /// Gets or sets if messages via Write\Writeline are exposed to IDE\Compiler.
        /// </summary>
        public static bool LogToIDE { get; set; }

        /// <summary>
        /// Gets or sets if messages via Write\Writeline are exposed to windows console (if possible).
        /// </summary>
        public static bool LogToConsole { get; set; }

        /// <summary>
        /// Gets or sets if messages via Write\Writeline are exposed to  file.
        /// </summary>
        public static bool LogToFile { get; set; }

        /// <summary>
        /// Gets or sets file name for file logger.
        /// </summary>
        public static string FileName { get; set; }

        /// <summary>
        /// Event wich will be fired whenever Write or Writeline will be called.
        /// </summary>
        public static event EventHandler<ScriptLoggerArgs> OnLogging;

        /// <summary>
        /// Initializer wich is obsole by user. Please check SDK.Initialize()!
        /// </summary>
        public static void Initialize()
        {
            LogToStealth = false;
            LogToIDE = false;
            LogToConsole = false;
            LogToFile = false;
            FileName = "Debug.log";
        }

        /// <summary>
        /// Writes text message without line break.
        /// </summary>
        /// <param name="text"></param>
        public static void Write(string text)
        {
            if (LogToStealth)
                Stealth.Client.AddToSystemJournal(text);
            if (LogToIDE)
                Debug.Write(text);
            if (LogToConsole)
                Console.Write(text);
            if (LogToFile)
                AppendToFile(text);
            OnHandle(new ScriptLoggerArgs {full = false, Text = text});
        }

        /// <summary>
        /// Writes text message with line break.
        /// </summary>
        /// <param name="text"></param>
        public static void WriteLine(string text)
        {
            if (LogToStealth)
                Stealth.Client.AddToSystemJournal(text);
            if (LogToIDE)
                Debug.WriteLine(text);
            if (LogToConsole)
                Console.WriteLine(text);
            if (LogToFile)
                AppendToFile(text);
            OnHandle(new ScriptLoggerArgs {full = true, Text = text});
        }

        private static void AppendToFile(string text)
        {
            try
            {
                using (var op = new StreamWriter(FileName, true))
                {
                    op.WriteLine("{0}, {1}", DateTime.Now, text);
                }
            }
            catch
            {
                // ignored
            }
        }

        private static void OnHandle(ScriptLoggerArgs e)
        {
            var handler = OnLogging;
            if (handler != null)
            {
                handler(typeof (ScriptLogger), e);
            }
        }
    }
}