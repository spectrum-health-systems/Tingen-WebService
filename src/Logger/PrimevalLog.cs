// 260723_code
// 260723_documentation

using System;
using System.IO;
using System.Threading;
using TingenWebService.Du;

namespace TingenWebService.Logger
{
    internal class PrimevalLog
    {
        // [260723]
        /// <summary>Creates a primeval log with the specified name and content.</summary>
        /// <param name="logName">The name of the log.</param>
        /// <param name="logContent">The content of the log.</param>
        internal static void Create(string logName, string logContent)
        {
            var logPath = Path.Combine(@"C:\Tingen_Data\Development\PrimevalLog", $"{DateTime.Now:fffffff}-{logName}.primeval");

            Thread.Sleep(5); // Ensure unique timestamp for log file name

            DuFile.DeadDrop(logPath, logContent);
        }
    }
}