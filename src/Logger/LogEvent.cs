// 260728_code
// 260728_documentation

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using TingenWebService.Du;
using TingenWebService.Session;
using TingenWebService.Trove;

namespace TingenWebService.Logger
{
    internal class LogEvent
    {
        internal static void Error(string systemLogRoot, string blueprintRoot, string sessionStartDateTime, string errorCode, string errorMessage)
        {
            DuDirectory.EnsureDirectoryExists(systemLogRoot);

            var errorLogBlueprint = File.ReadAllText(Path.Combine(blueprintRoot, "ErrorLog.blueprint"));
            var logContent = errorLogBlueprint.Replace("~SESSION~DATE~TIME~", $"{sessionStartDateTime}")
                                              .Replace("~ERROR~CODE~", errorCode)
                                              .Replace("~LOG~MESSAGE~", errorMessage);

            LogWriter.WriteLocal(systemLogRoot, $"{sessionStartDateTime}-[{errorCode}].error", logContent);
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

        internal static void Session(TwsSession twsSession)
        {
            // TODO - Clean this up.

            var sessionFolder = Path.Combine(twsSession.TwsFramework.SessionRoot,
                                             twsSession.RtConfig.SessionStartDate,
                                             twsSession.AvatarOptionObjects.SentOptionObject.OptionUserId,
                                             twsSession.RtConfig.SessionStartTime);

            DuDirectory.EnsureDirectoryExists(sessionFolder);

            var sessionLogName = Path.Combine(sessionFolder, $"{twsSession.AvatarOptionObjects.SentOptionObject.OptionUserId}.session");



            var sessionEndTime = DateTime.Now.ToString("HHmmss");
            var sessionEndMilliseconds = DateTime.Now.ToString("fffffff");

            var sessionDurationTime = (DateTime.ParseExact(sessionEndTime, "HHmmss", null) - DateTime.ParseExact(twsSession.RtConfig.SessionStartTime, "HHmmss", null)).ToString(@"hh\:mm\:ss");
            var sessionDurationMilliseconds = (DateTime.ParseExact(sessionEndMilliseconds, "fffffff", null) - DateTime.ParseExact(twsSession.RtConfig.SessionStartMilliseconds, "fffffff", null)).ToString("fffffff");

            if (sessionDurationTime.StartsWith($"00:00:{twsSession.TwsConfig.SessionTimeout}"))
            {
                LogEvent.Error(twsSession.TwsFramework.SysLogRoot,
                               twsSession.TwsFramework.BlueprintRoot,
                               $"{twsSession.RtConfig.SessionStartDate}-{twsSession.RtConfig.SessionStartTime}",
                               "7362",
                               Epistle.Error7362(twsSession.TwsConfig.SessionTimeout));
            }

            var sessionLogBlueprint = File.ReadAllText(Path.Combine(twsSession.TwsFramework.BlueprintRoot, "SessionLog.blueprint"));
            var logContent = sessionLogBlueprint.Replace("~RELEASE~BUILD~", twsSession.RtConfig.ReleaseBuild)
                                                .Replace("~SESSION~DATE~", twsSession.RtConfig.SessionStartDate)
                                                .Replace("~SESSION~START~", $"{twsSession.RtConfig.SessionStartTime}:{twsSession.RtConfig.SessionStartMilliseconds}")
                                                .Replace("~SESSION~END~", $"{sessionEndTime}:{sessionEndMilliseconds}")
                                                .Replace("~SESSION~DURATION~", $"{sessionDurationTime}:{sessionDurationMilliseconds}")
                                                .Replace("~AVATAR~USER~NAME~", twsSession.AvatarOptionObjects.SentOptionObject.OptionUserId.ToUpper())
                                                .Replace("~AVATAR~SYSTEM~", twsSession.RtConfig.AvatarSystem.ToUpper())
                                                .Replace("~SCRIPT~PARAMETER~", twsSession.SentScriptParameter)
                                                .Replace("~SESSION~RUNNING~LOG~", twsSession.RunningLog);

            LogWriter.WriteLocal(sessionFolder, $"{twsSession.AvatarOptionObjects.SentOptionObject.OptionUserId}.session", logContent); // simplify
        }

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