// 251113_code
// 260520_documentation.

using System;
using System.Reflection;
using TingenWebService.Core.Avatar;
using TingenWebService.Core.Catalog;
using TingenWebService.Core.Logger;
using TingenWebService.Core.Query;
using TingenWebService.Core.TingenWsvcSession;

namespace TingenWebService.Module.DoseChangeEvaluationOtp
{
    /// <summary>Handles Dose Change Evaluation OTP module events for the current Tingen Web Service session.</summary>
    /// <remarks>
    /// This type dispatches module actions based on the current script parameter and evaluates provider
    /// authorization rules for supported event phases.
    /// </remarks>
    internal class DoseChangeEvaluationOtpEvent
    {
        /// <include file='AppData/XmlDocumentation/TngnWsvc.xml' path='TngnWsvc/Class[@name="CommonDefinition"]/ExecutingAssembly/*'/>
        public static string ExeAsm { get; set; } = Assembly.GetExecutingAssembly().GetName().Name;

        /// <summary>Parses the current module event and routes processing to the matching handler.</summary>
        /// <remarks>
        /// This method loads the module configuration and executes the appropriate handler when the module is
        /// enabled for the current session.
        /// </remarks>
        /// <param name="tngnWsvcSession">The current Tingen Web Service session being processed.</param>
        /// <example>
        /// <code>
        /// DoseChangeEvaluationOtpEvent.Parse(tngnWsvcSession);
        /// </code>
        /// </example>
        internal static void Parse(TngnWsvcSession tngnWsvcSession)
        {
            LogEvent.Trace(1, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

            var moduleConfigName = "DoseChangeEvaluationOtp.config";
            var moduleConfigPath = $@"{tngnWsvcSession.Framework.TngnWsvcDataFolder.Config}\{moduleConfigName}";
            var moduleConfig     = DoseChangeEvaluationOtpConfig.Load(moduleConfigPath, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session);

            if (moduleConfig.Mode.Equals("enabled", System.StringComparison.OrdinalIgnoreCase))
            {
                LogEvent.Trace(2, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                switch (tngnWsvcSession.ScriptParameter.OriginalScriptParameter.ToLower())
                {
                    case "_formload":
                        LogEvent.Trace(3, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                        FormLoad(tngnWsvcSession, moduleConfig);

                        break;

                    case "_prefile":
                        LogEvent.Trace(3, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                        PreFile(tngnWsvcSession, moduleConfig);

                        break;

                    case "_postfile":
                        LogEvent.Trace(3, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                        PostFile(tngnWsvcSession, moduleConfig);

                        break;

                    case "_prescriberisauthorizing":
                        LogEvent.Trace(3, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                        PrescriberIsAuthorizing(tngnWsvcSession, moduleConfig);

                        break;

                    default:
                        LogEvent.Trace(3, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                        // TODO Hard error for unsupported script parameter?

                        break;
                }
            }
        }

        /// <summary>Handles the module logic for the form load script event.</summary>
        /// <remarks>
        /// This method currently records trace logging for the form load phase and accepts the active module
        /// configuration for future event-specific processing.
        /// </remarks>
        /// <param name="tngnWsvcSession">The current Tingen Web Service session being processed.</param>
        /// <param name="moduleConfig">The loaded module configuration for the current session.</param>
        /// <example>
        /// <code>
        /// DoseChangeEvaluationOtpEvent.FormLoad(tngnWsvcSession, moduleConfig);
        /// </code>
        /// </example>
        internal static void FormLoad(TngnWsvcSession tngnWsvcSession, DoseChangeEvaluationOtpConfig moduleConfig)
        {
            LogEvent.Trace(1, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);
        }

        /// <summary>Handles the module logic for the pre-file script event.</summary>
        /// <remarks>
        /// This method currently records trace logging for the pre-file phase and accepts the active module
        /// configuration for future event-specific processing.
        /// </remarks>
        /// <param name="tngnWsvcSession">The current Tingen Web Service session being processed.</param>
        /// <param name="moduleConfig">The loaded module configuration for the current session.</param>
        /// <example>
        /// <code>
        /// DoseChangeEvaluationOtpEvent.PreFile(tngnWsvcSession, moduleConfig);
        /// </code>
        /// </example>
        internal static void PreFile(TngnWsvcSession tngnWsvcSession, DoseChangeEvaluationOtpConfig moduleConfig)
        {
            LogEvent.Trace(1, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);
        }

        /// <summary>Handles the module logic for the post-file script event.</summary>
        /// <remarks>
        /// This method currently records trace logging for the post-file phase and accepts the active module
        /// configuration for future event-specific processing.
        /// </remarks>
        /// <param name="tngnWsvcSession">The current Tingen Web Service session being processed.</param>
        /// <param name="moduleConfig">The loaded module configuration for the current session.</param>
        /// <example>
        /// <code>
        /// DoseChangeEvaluationOtpEvent.PostFile(tngnWsvcSession, moduleConfig);
        /// </code>
        /// </example>
        internal static void PostFile(TngnWsvcSession tngnWsvcSession, DoseChangeEvaluationOtpConfig moduleConfig)
        {
            LogEvent.Trace(1, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);
        }

        /// <summary>Determines whether the current prescriber is the authorized provider for the order.</summary>
        /// <remarks>
        /// This method compares the current staff member identifier with the physician approver value stored on
        /// the option object and returns the appropriate authorization response.
        /// </remarks>
        /// <param name="tngnWsvcSession">The current Tingen Web Service session being processed.</param>
        /// <param name="moduleConfig">The loaded module configuration for the current session.</param>
        /// <example>
        /// <code>
        /// DoseChangeEvaluationOtpEvent.PrescriberIsAuthorizing(tngnWsvcSession, moduleConfig);
        /// </code>
        /// </example>
        internal static void PrescriberIsAuthorizing(TngnWsvcSession tngnWsvcSession, DoseChangeEvaluationOtpConfig moduleConfig)
        {
            //LogEvent.Debug();
            LogEvent.Trace(1, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);
            //LogEvent.Debug(moduleConfig.PhysicianApproverFieldId);

            var thing = tngnWsvcSession.OptObj.Original.GetFieldValue(moduleConfig.PhysicianApproverFieldId);

            LogEvent.Debug();

            if (string.IsNullOrEmpty(thing))
            {
                LogEvent.Debug("Physician Approver field is empty.");
            }
            else
            {
                LogEvent.Debug(thing);
            }

            var userIdQuery = QueryUserId.ToStaffMemberId(tngnWsvcSession);

            LogEvent.Debug($"userIdQuery: {userIdQuery}{Environment.NewLine}" +
                           $"physicianApprover: {thing}");

            var physicianApprover = tngnWsvcSession.OptObj.Original.GetFieldValue(moduleConfig.PhysicianApproverFieldId);

            SessionLog.AddToSessionLog(tngnWsvcSession, log_DoseChangeEval.PrescriberIsAuthorizing(LogComponents.GetCallerInfo(), userIdQuery, physicianApprover));

            LogEvent.Debug($"userIdQuery: {userIdQuery}{Environment.NewLine}" +
                           $"physicianApprover: {physicianApprover}");
            if (userIdQuery.Contains($"val=\"{physicianApprover}\""))
            {
                LogEvent.Trace(1, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                AvatarOptionObject.ToReturn(tngnWsvcSession, 0);
            }
            else
            {
                LogEvent.Trace(1, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                AvatarOptionObject.ToReturn(tngnWsvcSession, 1, moduleConfig.NotAllowedToAuthorizeOptObjMsg);
            }
        }
    }
}