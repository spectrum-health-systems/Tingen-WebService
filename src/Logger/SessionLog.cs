// 260725_code
// 260725_documentation

using System;
using System.IO;
using TingenWebService.Du;
using TingenWebService.Session;

namespace TingenWebService.Logger
{
    /// <summary>Session log logic.</summary>
    internal class SessionLog
    {
        /// <summary>Creates a session log for the given Tingen Web Service session.</summary>
        /// <param name="twsSession">The Tingen Web Service session.</param>
        internal static void Create(TngnWsvcSession twsSession)
        {
            // TODO - Clean this up.

            var sessionRoot = twsSession.TwsFramework.SessionRoot;
            var sessionDate = twsSession.RtConfig.CurrentDate;
            var sessionUser = twsSession.SentOptionObject.OptionUserId;
            var sessionTime = twsSession.RtConfig.CurrentTime;

            var sessionFolder = Path.Combine(sessionRoot, sessionDate, sessionUser, sessionTime);

            DuDirectory.EnsureDirectoryExists(sessionFolder);

            var logName = Path.Combine(sessionFolder, $"{twsSession.SentOptionObject.OptionUserId}.session");

            var logBlueprint = File.ReadAllText(Path.Combine(twsSession.TwsFramework.BlueprintRoot, "SessionLog.blueprint"));

            var endTime  = DateTime.Now.ToString("HHmmss");
            var duration = (DateTime.ParseExact(endTime, "HHmmss", null) - DateTime.ParseExact(twsSession.RtConfig.CurrentTime, "HHmmss", null)).ToString(@"hh\:mm\:ss");

            var logContent = logBlueprint.Replace("~RELEASE~BUILD~", twsSession.RtConfig.ReleaseBuild)
                                         .Replace("~SESSION~DATE~", twsSession.RtConfig.CurrentDate)
                                         .Replace("~SESSION~START~", twsSession.RtConfig.CurrentTime)
                                         .Replace("~SESSION~END~", endTime)
                                         .Replace("~SESSION~DURATION~", duration)
                                         .Replace("~AVATAR~USER~NAME~", twsSession.SentOptionObject.OptionUserId.ToUpper())
                                         .Replace("~AVATAR~SYSTEM~", twsSession.RtConfig.AvatarSystem.ToUpper())
                                         .Replace("~SCRIPT~PARAMETER~", twsSession.SentScriptParameter)
                                         .Replace("~SESSION~DETAILS~", twsSession.SessionDetails);

            LogUtility.WriteLocal(sessionFolder, $"{twsSession.SentOptionObject.OptionUserId}.session", logContent);
        }
    }
}