// 260728_code
// 260728_documentation

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using TingenWebService.Du;
using TingenWebService.Session;

namespace TingenWebService.Logger
{
    internal class LogEvent
    {
        internal static void SystemLog(string logFolder, string logName, string logContent)
        {
            /* DEVNOTE
             * - Use primeval logs here to debug, since logging functionality has not been initialized yet.
             * - Disable this in production.
             */
            //LogEvent.Primeval("SystemLogFileInitialized");

            if (File.Exists(Path.Combine(logFolder, logName)))
            {
                LogWriter.AppendLocal(logFolder, logName, logContent);
            }
            else
            {
                LogWriter.WriteLocal(logFolder, logName, logContent);
            }
        }

        /// <summary>Logs a primeval event with the specified name and content.</summary>
        /// <param name="logName">The name of the log file.</param>
        /// <param name="logContent">The content of the log file.</param>
        /// <remarks>
        /// There is a 5-millisecond delay before writing the log to ensure that the log file name is unique and does
        /// not conflict with other log entries.
        /// </remarks>
        internal static void Primeval(string logName, string logContent = "")
        {
            Thread.Sleep(5);

            LogWriter.WriteLocal(@"C:\Tingen_Data\Development\PrimevalLog", $"{DateTime.Now:fffffff}-{logName}.primeval", logContent);
        }

        internal static void Session(TngnWsvcSession twsSession)
        {
            // TODO - Clean this up.

            var sessionRoot = twsSession.TwsFramework.SessionRoot;
            var sessionDate = twsSession.RtConfig.CurrentDate;
            var sessionUser = twsSession.AvatarOptionObjects.SentOptionObject.OptionUserId;
            var sessionTime = twsSession.RtConfig.CurrentTime;

            var sessionFolder = Path.Combine(sessionRoot, sessionDate, sessionUser, sessionTime);

            DuDirectory.EnsureDirectoryExists(sessionFolder);

            var logName = Path.Combine(sessionFolder, $"{twsSession.AvatarOptionObjects.SentOptionObject.OptionUserId}.session");

            var logBlueprint = File.ReadAllText(Path.Combine(twsSession.TwsFramework.BlueprintRoot, "SessionLog.blueprint"));

            var endTime         = DateTime.Now.ToString("HHmmss");
            var endMilliseconds = DateTime.Now.ToString("fffffff");

            var durationTime = (DateTime.ParseExact(endTime, "HHmmss", null) - DateTime.ParseExact(twsSession.RtConfig.CurrentTime, "HHmmss", null)).ToString(@"hh\:mm\:ss");
            var durationMilliseconds = (DateTime.ParseExact(endMilliseconds, "fffffff", null) - DateTime.ParseExact(twsSession.RtConfig.CurrentMilliseconds, "fffffff", null)).ToString("fffffff");

            if (durationTime.StartsWith("00:00:10"))
            {
                // File error because this is too long
            }

            var logContent = logBlueprint.Replace("~RELEASE~BUILD~", twsSession.RtConfig.ReleaseBuild)
                                         .Replace("~SESSION~DATE~", twsSession.RtConfig.CurrentDate)
                                         .Replace("~SESSION~START~", twsSession.RtConfig.CurrentTime)
                                         .Replace("~SESSION~END~", endTime)
                                         .Replace("~SESSION~DURATION~", $"{durationTime} ({durationMilliseconds})")
                                         .Replace("~AVATAR~USER~NAME~", twsSession.AvatarOptionObjects.SentOptionObject.OptionUserId.ToUpper())
                                         .Replace("~AVATAR~SYSTEM~", twsSession.RtConfig.AvatarSystem.ToUpper())
                                         .Replace("~SCRIPT~PARAMETER~", twsSession.SentScriptParameter)
                                         .Replace("~SESSION~RUNNING~LOG~", twsSession.RunningLog);

            LogWriter.WriteLocal(sessionFolder, $"{twsSession.AvatarOptionObjects.SentOptionObject.OptionUserId}.session", logContent);
        }

        /// <summary>Writes a trace log entry when the supplied trace level is within the configured limit.</summary>
        /// <remarks>
        /// The <paramref name="classPath"/>, <paramref name="methodName"/>, and <paramref name="lineNumber"/>
        /// parameters are populated automatically by the compiler via caller-info attributes.
        /// </remarks>
        /// <param name="traceLevel">The trace level for this log entry.</param>
        /// <param name="traceLogLimit">The configured maximum trace level that should be written.</param>
        /// <param name="sessionFolder">The session folder where the trace log file will be written.</param>
        /// <param name="classPath">
        /// The full source file path of the caller, supplied automatically by the compiler.
        /// </param>
        /// <param name="methodName">The name of the calling member, supplied automatically by the compiler.</param>
        /// <param name="lineNumber">
        /// The source line number of the caller, supplied automatically by the compiler.
        /// </param>
        /// <example>
        /// <code>
        /// LogEvent.Trace(9, TwsSession.TwsConfig.TraceLevelLimit, TwsSession.SessionFolder);
        /// </code>
        /// </example>
        internal static void Trace(int traceLevel, int levelLimit, string sessionFolder, [CallerFilePath] string classPath = "", [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0)
        {
            if (levelLimit != 0 && (traceLevel <= levelLimit))
            {
                Thread.Sleep(levelLimit);

                var logName = $"{DateTime.Now:ssff-fffff}-{LogWriter.GetClassName(classPath)}-{methodName}-{lineNumber}.trace";

                LogWriter.WriteLocal(sessionFolder, logName);
            }
        }
    }
}