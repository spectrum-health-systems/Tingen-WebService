// 260811_code
// 260811_documentation

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using TingenWebService.Core.Session;
using TingenWebService.Core.Trove;
using TingenWebService.Du;

namespace TingenWebService.Core.Logger
{
    /// <summary>Provides methods for logging various types of events.</summary>
    internal static class LogEvent
    {
        /// <summary>Logs a daily event with the specified details.</summary>
        /// <param name="sysLogRoot">The root folder of the system log files.</param>
        /// <param name="todayDate">The current date.</param>
        /// <param name="startTime">The start time of the event.</param>
        /// <param name="versionBuild">The version and build information.</param>
        /// <param name="runningLog">The running log content.</param>
        /// <param name="duration">The duration of the event.</param>
        internal static void Daily(string sysLogRoot, string todayDate, string startTime, string versionBuild, string runningLog, string duration)
        {
            //LogEvent.Primeval("PRELOG-TRACE-LogEvent-Daily");

            DuDirectory.ForceExist(sysLogRoot);

            string logContent = Redprint.DailyLog().Replace("~VERSION~BUILD~", versionBuild)
                                                   .Replace("~STARTIME~", startTime)
                                                   .Replace("~RUNNING~LOG~", runningLog)
                                                   .Replace("~DURATION~", duration);

            LogWriter.WriteLocal(sysLogRoot, $"{todayDate}.daily", logContent);
        }

        /// <summary>Logs an error event with the specified details.</summary>
        /// <param name="sysLogRoot">The root folder of the system log files.</param>
        /// <param name="blueprintRoot">The root folder of the blueprint files.</param>
        /// <param name="sessStartDateTime">The start date and time of the session.</param>
        /// <param name="errCode">The error code.</param>
        /// <param name="errMsg">The error message.</param>
        internal static void Error(string sysLogRoot, string blueprintRoot, string sessStartDateTime, string errCode, string errMsg)
        {
            //LogEvent.Primeval("PRELOG-TRACE-LogEvent-Error");

            DuDirectory.ForceExist(sysLogRoot);

            var errorLogBlueprint = File.ReadAllText(Path.Combine(blueprintRoot, "ErrorLogTxt.blueprint"));

            var logContent = errorLogBlueprint.Replace("~SESSION~DATE~TIME~", $"{sessStartDateTime}")
                                              .Replace("~ERROR~CODE~", errCode)
                                              .Replace("~ERROR~MESSAGE~", errMsg);

            LogWriter.WriteLocal(sysLogRoot, $"{sessStartDateTime}-[{errCode}].error", logContent);
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
            /* DEVNOTE: Do not put logger functionality here, it will cause havoc!
             */

            Thread.Sleep(5);

            LogWriter.WriteLocal(@"C:\Tingen_Data\Development\PrimevalLog", $"{DateTime.Now:mmssfffffff}-{logName}.primeval", logContent);
        }

        /// <summary>Logs a session event with the specified session details.</summary>
        /// <param name="sess">The session object containing session details.</param>
        internal static void Session(Sess sess)
        {
            // TODO - Clean this up (but leave "LogEvent." in front of each call to LogEvent.Trace() so that the trace log will show the correct class name).
            // Wait, what?

            LogEvent.Trace(1, sess.Trc.Lmt, sess.Trc.Fld);

            sess.RunningDetail = $"Blah blah blah{Environment.NewLine}"; // TESTING

            DuDirectory.ForceExist(sess.SessionFolder);

            var sessionLogName = Path.Combine(sess.SessionFolder, $"{sess.OptionObject.SentOptionObject.OptionUserId}.session");

            var sessionEndTime = DateTime.Now.ToString("HHmmss");
            var sessionEndMilliseconds = DateTime.Now.ToString("fffffff");

            var sessionDurationTime = (DateTime.ParseExact(sessionEndTime, "HHmmss", null) - DateTime.ParseExact(sess.RuntimeSetting.CurrentTime, "HHmmss", null)).ToString(@"hh\:mm\:ss");
            var sessionDurationMilliseconds = (DateTime.ParseExact(sessionEndMilliseconds, "fffffff", null) - DateTime.ParseExact(sess.RuntimeSetting.CurrentMs, "fffffff", null)).ToString("fffffff");

            if (sessionDurationTime.StartsWith($"00:00:{sess.AppSetting.SessionTimeout}"))
            {
                LogEvent.Trace(4, sess.Trc.Lmt, sess.Trc.Fld);

                var errComponents = SysMsg.ERR1210(sess.OptionObject.SentOptionObject.OptionUserId, sessionDurationMilliseconds);

                LogEvent.Error(sess.FrameworkSetting.SysLogRoot,
                               sess.FrameworkSetting.BlueprintRoot,
                               $"{sess.RuntimeSetting.CurrentDate}-{sess.RuntimeSetting.CurrentTime}",
                               errComponents[0],
                               errComponents[1]);
            }

            // TODO - these need to be combined.

            if (sess.AppSetting.SessionLogTextFormat)
            {
                LogEvent.Trace(4, sess.Trc.Lmt, sess.Trc.Fld);

                var sessLogTxtBP = File.ReadAllText(Path.Combine(sess.FrameworkSetting.BlueprintRoot, "SessLogTxt.blueprint"));

                var logContent = sessLogTxtBP.Replace("~RELEASE~BUILD~", sess.RuntimeSetting.VersionBuild)
                                             .Replace("~SESSION~DATE~", sess.RuntimeSetting.CurrentDate)
                                             .Replace("~SESSION~START~", $"{sess.RuntimeSetting.CurrentTime}:{sess.RuntimeSetting.CurrentMs}")
                                             .Replace("~SESSION~END~", $"{sessionEndTime}:{sessionEndMilliseconds}")
                                             .Replace("~SESSION~DURATION~", $"{sessionDurationTime}:{sessionDurationMilliseconds}")
                                             .Replace("~AVATAR~USER~NAME~", sess.OptionObject.SentOptionObject.OptionUserId.ToUpper())
                                             .Replace("~AVATAR~SYSTEM~", sess.RuntimeSetting.AvatarSystem.ToUpper())
                                             .Replace("~SCRIPT~PARAMETER~", sess.SentScriptParameter)
                                             .Replace("~SESSION~RUNNING~LOG~", sess.RunningLog)
                                             .Replace("~SESSION~DETAILS~", sess.RunningDetail);

                LogWriter.WriteLocal(sess.SessionFolder, $"{sess.OptionObject.SentOptionObject.OptionUserId}.session", logContent); // simplify
            }

            LogEvent.Trace(9, sess.Trc.Lmt, sess.Trc.Fld);

            if (sess.AppSetting.SessionLogMarkdownFormat)
            {
                LogEvent.Trace(4, sess.Trc.Lmt, sess.Trc.Fld);

                var sessLogMdBP = File.ReadAllText(Path.Combine(sess.FrameworkSetting.BlueprintRoot, "SessLogMd.blueprint"));
                var logContent = sessLogMdBP.Replace("~RELEASE~BUILD~", sess.RuntimeSetting.VersionBuild)
                                            .Replace("~SESSION~DATE~", sess.RuntimeSetting.CurrentDate)
                                            .Replace("~SESSION~START~", $"{sess.RuntimeSetting.CurrentTime}:{sess.RuntimeSetting.CurrentMs}")
                                            .Replace("~SESSION~END~", $"{sessionEndTime}:{sessionEndMilliseconds}")
                                            .Replace("~SESSION~DURATION~", $"{sessionDurationTime}:{sessionDurationMilliseconds}")
                                            .Replace("~AVATAR~USER~NAME~", sess.OptionObject.SentOptionObject.OptionUserId.ToUpper())
                                            .Replace("~AVATAR~SYSTEM~", sess.RuntimeSetting.AvatarSystem.ToUpper())
                                            .Replace("~SCRIPT~PARAMETER~", sess.SentScriptParameter)
                                            .Replace("~SESSION~RUNNING~LOG~", sess.RunningLog)
                                            .Replace("~SESSION~DETAILS~", sess.RunningDetail);

                LogWriter.WriteLocal(sess.SessionFolder, $"{sess.OptionObject.SentOptionObject.OptionUserId}.session.md", logContent); // simplify
            }
        }

        /// <summary>Logs a system event with the specified details.</summary>
        /// <param name="logFolder">The folder where the log file will be written.</param>
        /// <param name="logName">The name of the log file.</param>
        /// <param name="logContent">The content of the log entry.</param>
        internal static void SystemLog(string logFolder, string logName, string logContent)
        {
            //LogEvent.Primeval("PRELOG-TRACE-LogEvent-SystemLog");

            LogWriter.WriteLocal(logFolder, logName, logContent);
        }

        /// <summary>Writes a trace log entry when the supplied trace level is within the configured limit.</summary>
        /// <remarks>
        /// <include file='AppData/XmlDoc/Topics.xml' path='Topics/Topic[@name="Logger"]/TraceLogLevels/*'/>
        /// <br/> The <paramref name="classPath"/>, <paramref name="methodName"/>, and <paramref name="lineNumber"/>
        /// parameters are populated automatically by the compiler via caller-info attributes.
        /// </remarks>
        /// <param name="traceLevel">The trace level for this log entry.</param>
        /// <param name="levelLimit">The configured maximum trace level that should be written.</param>
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
            /* DEVNOTE: Do not put logger functionality here, it will cause havoc!
             */

            if (levelLimit != 0 && (traceLevel <= levelLimit))
            {
                Thread.Sleep(levelLimit);

                var logName = $"{DateTime.Now:ssfffffff}-{LogUtility.GetClassName(classPath)}-{methodName}-{lineNumber}.trace";

                LogWriter.WriteLocal(sessionFolder, logName);
            }
        }
    }
}