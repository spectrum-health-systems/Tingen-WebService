// 260726_code
// 260726_documentation

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using TingenWebService.Session;

namespace TingenWebService.Logger
{
    internal class LogEvent
    {
        internal static void Daily(string path, string logContent)
        {
            if (File.Exists(path))
            {
                LogUtility.AppendLocal(path, $"{DateTime.Now:fffffff}-DailyLog.log", logContent);
            }
            else
            {
                DailyLog.Create(logContent);
            }
        }

        /// <summary>Logs a primeval event with the specified name and content.</summary>
        /// <param name="logName">The name of the log.</param>
        /// <param name="logContent">The content of the log.</param>
        internal static void Primeval(string logName, string logContent = "")
        {
            Thread.Sleep(5); // Ensure unique timestamp for log file name

            LogUtility.WriteLocal(@"C:\Tingen_Data\Development\PrimevalLog", $"{DateTime.Now:fffffff}-{logName}.primeval", logContent);
        }


        internal static void Session(TngnWsvcSession twsSession) => SessionLog.Create(twsSession);

        /// <summary>Writes a trace log entry when the supplied trace level is within the configured limit.</summary>
        /// <remarks>
        /// The <paramref name="classPath"/>, <paramref name="methodName"/>, and <paramref name="lineNumber"/>
        /// parameters are populated automatically by the compiler via caller-info attributes.
        /// </remarks>
        /// <param name="traceLevel">The trace level for this log entry.</param>
        /// <param name="traceLogLimit">The configured maximum trace level that should be written.</param>
        /// <param name="sessionFolder">The session folder where the trace log file will be written.</param>
        /// <param name="classPath">The full source file path of the caller, supplied automatically by the compiler.</param>
        /// <param name="methodName">The name of the calling member, supplied automatically by the compiler.</param>
        /// <param name="lineNumber">The source line number of the caller, supplied automatically by the compiler.</param>
        /// <example>
        /// <code>
        /// LogEvent.Trace(
        ///     traceLevel:    1,
        ///     traceLevelLimit:    tngnWsvcSession.LogSetting.TraceLogLimit,
        ///     sessionFolder: tngnWsvcSession.Framework.TngnWsvcDataFolder.Session);
        /// </code>
        /// </example>
        internal static void Trace(int traceLevel, int levelLimit, string sessionFolder, [CallerFilePath] string classPath = "", [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0)
        {
            if (levelLimit != 0 && (traceLevel <= levelLimit))
            {
                Thread.Sleep(levelLimit);

                var logName = $"{DateTime.Now:ssff-fffff}-{LogUtility.GetClassName(classPath)}-{methodName}-{lineNumber}.trace";

                LogUtility.WriteLocal(sessionFolder, logName, "");
            }
        }
    }
}