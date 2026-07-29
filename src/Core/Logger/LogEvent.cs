// 260729_code
// 260729_documentation

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using TingenWebService.Core.Trove;
using TingenWebService.Du;

namespace TingenWebService.Core.Logger
{
    internal static class LogEvent
    {
        internal static void Error(string sysLogRoot, string bpRoot, string sessStartDateTime, string errCode, string errMsg)
        {
            DuDirectory.EnsureDirectoryExists(sysLogRoot);

            var errorLogBlueprint = File.ReadAllText(Path.Combine(bpRoot, "ErrorLogTxt.bp"));
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

        internal static void Session(Sess sess)
        {
            // TODO - Clean this up.

            var sessionFolder = Path.Combine(sess.FrameworkSetting.SessionRoot,
                                             sess.RuntimeSetting.SessionStartDate,
                                             sess.AvatarData.SentOptObj.OptionUserId,
                                             sess.RuntimeSetting.SessionStartTime);

            DuDirectory.EnsureDirectoryExists(sessionFolder);

            var sessionLogName = Path.Combine(sessionFolder, $"{sess.AvatarData.SentOptObj.OptionUserId}.session");

            var sessionEndTime = DateTime.Now.ToString("HHmmss");
            var sessionEndMilliseconds = DateTime.Now.ToString("fffffff");

            var sessionDurationTime = (DateTime.ParseExact(sessionEndTime, "HHmmss", null) - DateTime.ParseExact(sess.RuntimeSetting.SessionStartTime, "HHmmss", null)).ToString(@"hh\:mm\:ss");
            var sessionDurationMilliseconds = (DateTime.ParseExact(sessionEndMilliseconds, "fffffff", null) - DateTime.ParseExact(sess.RuntimeSetting.SessionStartMilliseconds, "fffffff", null)).ToString("fffffff");

            if (sessionDurationTime.StartsWith($"00:00:{sess.TwsSetting.SessTimeout}"))
            {
                var errComponents = SysMsg.ERR1210(sess.AvatarData.SentOptObj.OptionUserId, sessionDurationMilliseconds);

                LogEvent.Error(sess.FrameworkSetting.SysLogRoot,
                               sess.FrameworkSetting.BlueprintRoot,
                               $"{sess.RuntimeSetting.SessionStartDate}-{sess.RuntimeSetting.SessionStartTime}",
                               errComponents[0],
                               errComponents[1]);
            }

            // TODO - these need to be combined.

            if (sess.TwsSetting.SessLogTxt)
            {
                var sessLogTxtBP = File.ReadAllText(Path.Combine(sess.FrameworkSetting.BlueprintRoot, "SessLogTxt.bp"));
                var logContent = sessLogTxtBP.Replace("~RELEASE~BUILD~", sess.RuntimeSetting.ReleaseBuild)
                                             .Replace("~SESSION~DATE~", sess.RuntimeSetting.SessionStartDate)
                                             .Replace("~SESSION~START~", $"{sess.RuntimeSetting.SessionStartTime}:{sess.RuntimeSetting.SessionStartMilliseconds}")
                                             .Replace("~SESSION~END~", $"{sessionEndTime}:{sessionEndMilliseconds}")
                                             .Replace("~SESSION~DURATION~", $"{sessionDurationTime}:{sessionDurationMilliseconds}")
                                             .Replace("~AVATAR~USER~NAME~", sess.AvatarData.SentOptObj.OptionUserId.ToUpper())
                                             .Replace("~AVATAR~SYSTEM~", sess.RuntimeSetting.AvatarSystem.ToUpper())
                                             .Replace("~SCRIPT~PARAMETER~", sess.AvatarData.SentScriptParam)
                                             .Replace("~SESSION~RUNNING~LOG~", sess.RunningLog);

                LogWriter.WriteLocal(sessionFolder, $"{sess.AvatarData.SentOptObj.OptionUserId}.session", logContent); // simplify
            }

            if (sess.TwsSetting.SessLogMd)
            {
                var sessLogMdBP = File.ReadAllText(Path.Combine(sess.FrameworkSetting.BlueprintRoot, "SessLogMd.bp"));
                var logContent = sessLogMdBP.Replace("~RELEASE~BUILD~", sess.RuntimeSetting.ReleaseBuild)
                                             .Replace("~SESSION~DATE~", sess.RuntimeSetting.SessionStartDate)
                                             .Replace("~SESSION~START~", $"{sess.RuntimeSetting.SessionStartTime}:{sess.RuntimeSetting.SessionStartMilliseconds}")
                                             .Replace("~SESSION~END~", $"{sessionEndTime}:{sessionEndMilliseconds}")
                                             .Replace("~SESSION~DURATION~", $"{sessionDurationTime}:{sessionDurationMilliseconds}")
                                             .Replace("~AVATAR~USER~NAME~", sess.AvatarData.SentOptObj.OptionUserId.ToUpper())
                                             .Replace("~AVATAR~SYSTEM~", sess.RuntimeSetting.AvatarSystem.ToUpper())
                                             .Replace("~SCRIPT~PARAMETER~", sess.AvatarData.SentScriptParam)
                                             .Replace("~SESSION~RUNNING~LOG~", sess.RunningLog);

                LogWriter.WriteLocal(sessionFolder, $"{sess.AvatarData.SentOptObj.OptionUserId}.session.md", logContent); // simplify
            }

            if (sess.TwsSetting.SessLogHtml)
            {
                var sessLogHtmlBP = File.ReadAllText(Path.Combine(sess.FrameworkSetting.BlueprintRoot, "SessLogHtml.bp"));
                var logContent = sessLogHtmlBP.Replace("~RELEASE~BUILD~", sess.RuntimeSetting.ReleaseBuild)
                                             .Replace("~SESSION~DATE~", sess.RuntimeSetting.SessionStartDate)
                                             .Replace("~SESSION~START~", $"{sess.RuntimeSetting.SessionStartTime}:{sess.RuntimeSetting.SessionStartMilliseconds}")
                                             .Replace("~SESSION~END~", $"{sessionEndTime}:{sessionEndMilliseconds}")
                                             .Replace("~SESSION~DURATION~", $"{sessionDurationTime}:{sessionDurationMilliseconds}")
                                             .Replace("~AVATAR~USER~NAME~", sess.AvatarData.SentOptObj.OptionUserId.ToUpper())
                                             .Replace("~AVATAR~SYSTEM~", sess.RuntimeSetting.AvatarSystem.ToUpper())
                                             .Replace("~SCRIPT~PARAMETER~", sess.AvatarData.SentScriptParam)
                                             .Replace("~SESSION~RUNNING~LOG~", sess.RunningLog);

                LogWriter.WriteLocal(sessionFolder, $"{sess.AvatarData.SentOptObj.OptionUserId}.session.html", logContent); // simplify
            }
        }

        internal static void SystemLog(string logFolder, string logName, string logContent)
        {
            /* DEVNOTE
             * - Use primeval logs here to debug, since logging functionality has not been initialized yet.
             * - Disable this in production.
             */
            LogEvent.Primeval("${DateTime.Now:yyMMdd-HHmmss-fffffff}-[ERR1140]-FrameworkValidationFailed", SysMsg.ERR1140(path, ex.Message)[1]);
            //LogEvent.Primeval("$"{DateTime.Now:yyMMdd-HHmmss}-DEBUG-LogEvent.SystemLog");

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