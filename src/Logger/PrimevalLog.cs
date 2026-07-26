// 260723_code
// 260723_documentation

using System;
using System.Threading;

namespace TingenWebService.Logger
{
    internal class PrimevalLog
    {
        /// <summary>Creates a primeval log with the specified name and content.</summary>
        /// <param name="logName">The name of the log.</param>
        /// <param name="logContent">The content of the log.</param>
        internal static void Create(string logName, string logContent)
        {
            Thread.Sleep(5); // Ensure unique timestamp for log file name

            LogUtility.WriteLocal(@"C:\Tingen_Data\Development\PrimevalLog", $"{DateTime.Now:fffffff}-{logName}.primeval", logContent);
        }
    }
}