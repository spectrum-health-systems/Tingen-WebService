// 260722_code
// 260722_documentation

using System.IO;
using System.Reflection;
using TingenWebService.Core.Avatar;
using TingenWebService.Core.Logger;
using TingenWebService.Core.TingenWsvcSession;

namespace TingenWebService.Module.OpenIncident
{
    /// <summary>Handles OpenIncident Module events.</summary>
    internal class OpenIncidentEvent
    {
        /// <include file='AppData/XmlDocumentation/TngnWsvc.xml' path='TngnWsvc/Class[@name="CommonDefinition"]/ExecutingAssembly/*'/>
        public static string ExeAsm { get; set; } = Assembly.GetExecutingAssembly().GetName().Name;



        /// <summary>Handles the OpenIncident form load event.</summary>
        /// <remarks>
        /// This method determines whether the current incident is new or existing. New incidents are logged and
        /// allowed to continue, while existing incidents may trigger access verification when the option object
        /// has not yet been completed.
        /// </remarks>
        /// <param name="tngnWsvcSession">The current Tingen Web Service session being processed.</param>
        /// <param name="openIncidentConfig">The loaded OpenIncident module configuration.</param>
        /// <example>
        /// <code>
        /// OpenIncidentEvent.FormLoad(tngnWsvcSession, openIncidentConfig);
        /// </code>
        /// </example>
        internal static void FormLoad(TngnWsvcSession tngnWsvcSession, OpenIncidentConfig openIncidentConfig)
        {
            /* Setup some variables that are used throughout this method so they are easier to read/understand.
             */
            var traceLogLimit = tngnWsvcSession.LogSetting.TraceLogLimit;
            var sessionFolder = tngnWsvcSession.Framework.TngnWsvcDataFolder.Session;

            LogEvent.Trace(1, traceLogLimit, sessionFolder, ExeAsm);

            // If the Brief Incident Description field is empty, we consider this a new incident report. Otherwise, it's an existing incident.
            bool newIncident = string.IsNullOrWhiteSpace(tngnWsvcSession.OptObj.Worker.GetFieldValue(openIncidentConfig.BriefIncidentDescriptionFieldId));

            if (newIncident)
            {
                LogEvent.Trace(2, traceLogLimit, sessionFolder, ExeAsm);

                var runningTitle = $"A new Incident report was created by {tngnWsvcSession.Runtime.AvatarUserId}";
                var runningBody  = "";

                SessionLog.AddToRunningLog(tngnWsvcSession, LogComponents.GetCallerInfo(), runningTitle, runningBody);
                AvatarOptionObject.ToReturn(tngnWsvcSession, 0);
            }
            else
            {
                ////var testPath         = Path.Combine(tngnWsvcSession.Framework.TngnWsvcDataFolder.Config, "test1.txt");

                ////File.WriteAllText(testPath, "Test content1");

                LogEvent.Trace(2, traceLogLimit, sessionFolder, ExeAsm);

                if (tngnWsvcSession.OptObj.Completed == null)
                {
                    OpenIncidentLogic.VerifyAccess(tngnWsvcSession, openIncidentConfig);
                }
            }
        }

        /// <summary>Handles the OpenIncident pre-file event.</summary>
        /// <remarks>
        /// This method validates the program of incident and verifies whether the current user is the original
        /// author before allowing the incident to be submitted.
        /// </remarks>
        /// <param name="tngnWsvcSession">The current Tingen Web Service session being processed.</param>
        /// <param name="openIncidentConfig">The loaded OpenIncident module configuration.</param>
        /// <example>
        /// <code>
        /// OpenIncidentEvent.PreFile(tngnWsvcSession, openIncidentConfig);
        /// </code>
        /// </example>
        internal static void PreFile(TngnWsvcSession tngnWsvcSession, OpenIncidentConfig openIncidentConfig)
        {
            LogEvent.Trace(1, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

            if (tngnWsvcSession.OptObj.Completed == null)
            {
                var programOfIncidentIsValid = OpenIncidentLogic.IsProgramOfIncidentValid(tngnWsvcSession, openIncidentConfig);
                var isOriginalAuthor             = OpenIncidentLogic.IsOriginalAuthor(tngnWsvcSession, openIncidentConfig);

                if (programOfIncidentIsValid && isOriginalAuthor)
                {
                    LogEvent.Trace(2, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                    AvatarOptionObject.ToReturn(tngnWsvcSession, 0);
                }
                else if (!programOfIncidentIsValid)
                {
                    LogEvent.Trace(2, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                    AvatarOptionObject.ToReturn(tngnWsvcSession, openIncidentConfig.InvalidProgramOfIncidentErrCode, openIncidentConfig.InvalidProgramOfIncidentMsg);
                }
                else if (programOfIncidentIsValid && !isOriginalAuthor)
                {
                    LogEvent.Trace(2, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                    AvatarOptionObject.ToReturn(tngnWsvcSession, openIncidentConfig.NotOriginalAuthorSubmitErrCode, openIncidentConfig.NotOriginalAuthorSubmitMsg);
                }
            }
        }
    }
}