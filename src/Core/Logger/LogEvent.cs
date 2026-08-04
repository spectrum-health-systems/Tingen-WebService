// 260804_code
// 260730_documentation

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
        /// <summary>Logs an error event with the specified details.</summary>
        /// <param name="sysLogRoot">The root folder of the system log files.</param>
        /// <param name="bpRoot">The root folder of the blueprint files.</param>
        /// <param name="sessStartDateTime">The start date and time of the session.</param>
        /// <param name="errCode">The error code.</param>
        /// <param name="errMsg">The error message.</param>
        internal static void Error(string sysLogRoot, string bpRoot, string sessStartDateTime, string errCode, string errMsg)
        {
            DuDirectory.ForceExist(sysLogRoot);

            var errorLogBlueprint = File.ReadAllText(Path.Combine(bpRoot, "ErrorLogTxt.blueprint"));
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
            Thread.Sleep(5);

            LogWriter.WriteLocal(@"C:\Tingen_Data\Development\PrimevalLog", $"{DateTime.Now:fffffff}-{logName}.primeval", logContent);
        }

        /// <summary>Logs a session event with the specified session details.</summary>
        /// <param name="sess">The session object containing session details.</param>
        internal static void Session(Sess sess)
        {
            // TODO - Clean this up (but leave "LogEvent." in front of each call to LogEvent.Trace() so that the trace log will show the correct class name).

            LogEvent.Trace(1, sess.TwsSetting.TraceLimit, sess.SessionFolder);

            var sessionFolder = Path.Combine(sess.FrwkSetting.SessionRoot,
                                             sess.RtSetting.SessionStartDate,
                                             sess.AvatarData.SentOptObj.OptionUserId,
                                             sess.RtSetting.SessionStartTime);

            DuDirectory.ForceExist(sessionFolder);

            var sessionLogName = Path.Combine(sessionFolder, $"{sess.AvatarData.SentOptObj.OptionUserId}.session");

            var sessionEndTime = DateTime.Now.ToString("HHmmss");
            var sessionEndMilliseconds = DateTime.Now.ToString("fffffff");

            var sessionDurationTime = (DateTime.ParseExact(sessionEndTime, "HHmmss", null) - DateTime.ParseExact(sess.RtSetting.SessionStartTime, "HHmmss", null)).ToString(@"hh\:mm\:ss");
            var sessionDurationMilliseconds = (DateTime.ParseExact(sessionEndMilliseconds, "fffffff", null) - DateTime.ParseExact(sess.RtSetting.SessionStartMilliseconds, "fffffff", null)).ToString("fffffff");

            if (sessionDurationTime.StartsWith($"00:00:{sess.TwsSetting.SessTimeout}"))
            {
                LogEvent.Trace(4, sess.TwsSetting.TraceLimit, sess.SessionFolder);

                var errComponents = SysMsg.ERR1210(sess.AvatarData.SentOptObj.OptionUserId, sessionDurationMilliseconds);

                LogEvent.Error(sess.FrwkSetting.SysLogRoot,
                               sess.FrwkSetting.BlueprintRoot,
                               $"{sess.RtSetting.SessionStartDate}-{sess.RtSetting.SessionStartTime}",
                               errComponents[0],
                               errComponents[1]);
            }

            // TODO - these need to be combined.

            if (sess.TwsSetting.SessLogTxt)
            {
                LogEvent.Trace(4, sess.TwsSetting.TraceLimit, sess.SessionFolder);

                var sessLogTxtBP = File.ReadAllText(Path.Combine(sess.FrwkSetting.BlueprintRoot, "SessLogTxt.blueprint"));

                var logContent = sessLogTxtBP.Replace("~RELEASE~BUILD~", sess.RtSetting.ReleaseBuild)
                                             .Replace("~SESSION~DATE~", sess.RtSetting.SessionStartDate)
                                             .Replace("~SESSION~START~", $"{sess.RtSetting.SessionStartTime}:{sess.RtSetting.SessionStartMilliseconds}")
                                             .Replace("~SESSION~END~", $"{sessionEndTime}:{sessionEndMilliseconds}")
                                             .Replace("~SESSION~DURATION~", $"{sessionDurationTime}:{sessionDurationMilliseconds}")
                                             .Replace("~AVATAR~USER~NAME~", sess.AvatarData.SentOptObj.OptionUserId.ToUpper())
                                             .Replace("~AVATAR~SYSTEM~", sess.RtSetting.AvatarSystem.ToUpper())
                                             .Replace("~SCRIPT~PARAMETER~", sess.AvatarData.SentScriptParam)
                                             .Replace("~SESSION~RUNNING~LOG~", sess.RunningLog);

                LogWriter.WriteLocal(sessionFolder, $"{sess.AvatarData.SentOptObj.OptionUserId}.session", logContent); // simplify
            }

            LogEvent.Trace(9, sess.TwsSetting.TraceLimit, sess.SessionFolder);

            if (sess.TwsSetting.SessLogMd)
            {
                LogEvent.Trace(4, sess.TwsSetting.TraceLimit, sess.SessionFolder);

                var sessLogMdBP = File.ReadAllText(Path.Combine(sess.FrwkSetting.BlueprintRoot, "SessLogMd.blueprint"));
                var logContent = sessLogMdBP.Replace("~RELEASE~BUILD~", sess.RtSetting.ReleaseBuild)
                                            .Replace("~SESSION~DATE~", sess.RtSetting.SessionStartDate)
                                            .Replace("~SESSION~START~", $"{sess.RtSetting.SessionStartTime}:{sess.RtSetting.SessionStartMilliseconds}")
                                            .Replace("~SESSION~END~", $"{sessionEndTime}:{sessionEndMilliseconds}")
                                            .Replace("~SESSION~DURATION~", $"{sessionDurationTime}:{sessionDurationMilliseconds}")
                                            .Replace("~AVATAR~USER~NAME~", sess.AvatarData.SentOptObj.OptionUserId.ToUpper())
                                            .Replace("~AVATAR~SYSTEM~", sess.RtSetting.AvatarSystem.ToUpper())
                                            .Replace("~SCRIPT~PARAMETER~", sess.AvatarData.SentScriptParam)
                                            .Replace("~SESSION~RUNNING~LOG~", sess.RunningLog);

                LogWriter.WriteLocal(sessionFolder, $"{sess.AvatarData.SentOptObj.OptionUserId}.session.md", logContent); // simplify
            }

            if (sess.TwsSetting.SessLogHtml)
            {
                LogEvent.Trace(4, sess.TwsSetting.TraceLimit, sess.SessionFolder);

                var sessLogHtmlBP = File.ReadAllText(Path.Combine(sess.FrwkSetting.BlueprintRoot, "SessLogHtml.blueprint"));
                var logContent = sessLogHtmlBP.Replace("~RELEASE~BUILD~", sess.RtSetting.ReleaseBuild)
                                              .Replace("~SESSION~DATE~", sess.RtSetting.SessionStartDate)
                                              .Replace("~SESSION~START~", $"{sess.RtSetting.SessionStartTime}:{sess.RtSetting.SessionStartMilliseconds}")
                                              .Replace("~SESSION~END~", $"{sessionEndTime}:{sessionEndMilliseconds}")
                                              .Replace("~SESSION~DURATION~", $"{sessionDurationTime}:{sessionDurationMilliseconds}")
                                              .Replace("~AVATAR~USER~NAME~", sess.AvatarData.SentOptObj.OptionUserId.ToUpper())
                                              .Replace("~AVATAR~SYSTEM~", sess.RtSetting.AvatarSystem.ToUpper())
                                              .Replace("~SCRIPT~PARAMETER~", sess.AvatarData.SentScriptParam)
                                              .Replace("~SESSION~RUNNING~LOG~", sess.RunningLog);

                LogWriter.WriteLocal(sessionFolder, $"{sess.AvatarData.SentOptObj.OptionUserId}.session.html", logContent); // simplify
            }
        }

        /// <summary>Logs a system event with the specified details.</summary>
        /// <param name="logFolder">The folder where the log file will be written.</param>
        /// <param name="logName">The name of the log file.</param>
        /// <param name="logContent">The content of the log entry.</param>
        internal static void SystemLog(string logFolder, string logName, string logContent)
        {
            LogEvent.Primeval("PRELOG-TRACE_LogEvent-SystemLog");

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
            // Can't put a trace log here!

            if (levelLimit != 0 && (traceLevel <= levelLimit))
            {
                Thread.Sleep(levelLimit);

                var logName = $"{DateTime.Now:ssff-fffff}-{LogWriter.GetClassName(classPath)}-{methodName}-{lineNumber}.trace";

                LogWriter.WriteLocal(sessionFolder, logName);
            }
        }


    }
}