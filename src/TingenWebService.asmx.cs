// 260804_code
// 260729_documentation

using System;
using System.IO;
using System.Reflection;
using System.Web.Services;
using ScriptLinkStandard.Objects;
using TingenWebService.Core;
using TingenWebService.Core.Avatar;
using TingenWebService.Core.Framework;
using TingenWebService.Core.Logger;
using TingenWebService.Core.Session;
using TingenWebService.Core.Trove;

namespace TingenWebService
{
    /// <summary>The Tingen Web Service entry class.</summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class TingenWebService : WebService
    {
        /// <summary>The current Tingen Web Service release.</summary>
        /// <remarks>
        /// Defined here because it's used in multiple places in this class, but I keep forgetting that, and wondering
        /// why I define it here, so that's why this comment exists. Hello future me (again).
        /// </remarks>
        /// <returns>A string representing the current release.</returns>
        private static string _releaseBuild { get; set; } = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        /// <summary>The Tingen Web Service session instance.</summary>
        /// <remarks>
        /// Defined here because it is initialized in <c>StartApp()</c>, but used in <c>RunScript()</c>, and I think
        /// this is easier to read/understand than <code>twsSession = StartApp()</code>
        /// </remarks>
        private Sess _sess { get; set; }

        /// <summary>Get the current version of the Tingen Web Service.</summary>
        /// <remarks>This method is required by Avatar.</remarks>
        /// <returns>A string representing the current version.</returns>
        [WebMethod]
        public string GetVersion() => $"VERSION {_releaseBuild}";

        /// <summary>The main entry method for the Tingen Web Service.</summary>
        /// <param name="sentOptObj">The OptionObject sent from Avatar.</param>
        /// <param name="sentScriptParam">The Script Parameter sent from Avatar.</param>
        /// <remarks>
        /// <include file='AppData/XmlDoc/Topics.xml' path='Topics/Topic[@name="RunScript"]/TheMagic/*'/>
        /// This method is required by Avatar.
        /// </remarks>
        /// <returns>A (potentially modified) completed OptionObject.</returns>
        [WebMethod]
        public OptionObject2015 RunScript(OptionObject2015 sentOptObj, string sentScriptParam)
        {
            LogEvent.Primeval("PRELOG-TRACE_TingenWebService-Runscript", SysMsg.PretraceStartMessage(sentScriptParam));

            if (IsMissingAvatarData(sentOptObj, sentScriptParam))
            {
                return sentOptObj.ToReturnOptionObject(0, "");
            }
            else
            {
                StartApp(sentOptObj, sentScriptParam);

                LogEvent.Trace(9, _sess.TwsSetting.TraceLimit, _sess.SessionFolder);

                // TODO - Route to the appropriate place.

                LogEvent.Session(_sess);

                return _sess.AvatarData.WorkerOptObj.ToReturnOptionObject(0, ""); // is this enough? Do we need CompleteOptObj?
            }
        }

        // TODO - Move to ns:Avatar.AvatarData?
        /// <summary>Determines if the Avatar data is missing.</summary>
        /// <param name="sentOptionObject">The OptionObject sent from Avatar.</param>
        /// <param name="sentScriptParameter">The Script Parameter sent from Avatar.</param>
        /// <remarks>
        /// <include file='AppData/XmlDoc/Topics.xml' path='Topics/Topic[@name="AvatarData"]/About/*'/>
        /// </remarks>
        /// <returns><c>True</c> if the avatar data is missing; otherwise, <c>false</c>.</returns>
        private static bool IsMissingAvatarData(OptionObject2015 sentOptionObject, string sentScriptParameter)
        {
            LogEvent.Primeval("PRELOG-TRACE_TingenWebService-IsMissingAvatarData");

            return !AvatarData.WasSent(sentOptionObject) || !AvatarData.WasSent(sentScriptParameter);
        }

        /// <summary>Start the Tingen Web Service.</summary>
        /// <param name="sentOptionObject">The <see cref="AvatarData.SentOptObj"/> sent from Avatar.</param>
        /// <param name="sentScriptParameter"> The <see cref="AvatarData.SentScriptParam"/> sent from Avatar.</param>
        /// <remarks>
        /// This method does the heavy-lifting of starting the Tingen Web Service by loading multiple configurations,
        /// validating requirements, and initializing the session state.
        /// </remarks>
        internal void StartApp(OptionObject2015 sentOptionObject, string sentScriptParameter)
        {
            LogEvent.Primeval("PRELOG-TRACE_TingenWebService-StartApp");

            RuntimeConfig runtimeConfig = RuntimeConfig.Load(_releaseBuild);

            FrameworkConfig frwkConfig = FrameworkConfig.Load(runtimeConfig.DataRoot, runtimeConfig.AvatarSystem);

            SessMaintenance.InitializeNewSession(runtimeConfig, frwkConfig); // TODO - is this really "initialize", or verify?

            _sess = Sess.StartSession(sentOptionObject, sentScriptParameter, runtimeConfig, frwkConfig);

            if (!File.Exists(Path.Combine(frwkConfig.SysLogRoot, "Configuration.current"))) // TODO - Move this somewhere else?
            {
                LogEvent.Trace(4, _sess.TwsSetting.TraceLimit, _sess.SessionFolder);

                LogMaintenance.ResetSystemLogs(_sess); // TODO - Test this.
            }
        }

        internal void ParseRequest(Sess sess)
        {
            LogEvent.Trace(1, sess.TwsSetting.TraceLimit, sess.FrwkSetting.SessionRoot);

            if (sess.AvatarData.SentScriptParam.StartsWith("_", StringComparison.OrdinalIgnoreCase))
            {

            }
            else
            {

            }


        }

        /// <summary>Handles specific form requests by routing to the appropriate event parser or generating an error.</summary>
        /// <remarks>
        /// Generates a hard error when the form name is <c>WSVC2491</c>, indicating the form ID was not found in the
        /// translation table.<br/>
        /// <br/>
        /// Otherwise, routes the known form names <c>OpenIncident</c> and <c>DoseChangeEvaluationOtp</c> to their
        /// respective event parsers.
        /// </remarks>
        /// <param name="specificFormName">The name of the specific form to handle.</param>
        /// <param name="sess">The web service session object containing form data and module event parsers.</param>
        /// <example>
        /// <code>
        /// AvatarScriptParameter.SpecificFormRequest("OpenIncident", sess);
        /// AvatarScriptParameter.SpecificFormRequest("DoseChangeEvaluationOtp", sess);
        /// </code>
        /// </example>
        internal static void SpecificFormRequest(string specificFormName, Sess sess)
        {
            //if (specificFormName == "WSVC2491")
            //{
            //    //sess.TngnWsvcSessionError.HardError(tngnWsvcSession, 1, $"[WSVC2491] The form ID '{tngnWsvcSession.OptObj.Original.OptionId}' was not found in the translation table.");
            //}
            //else
            //{
            //    switch (specificFormName)
            //    {
            //        case "OpenIncident":
            //            tngnWsvcSession.Module.OpenIncident.OpenIncidentEvent.Parse(tngnWsvcSession);
            //            break;

            //        case "DoseChangeEvaluationOtp":
            //            tngnWsvcSession.Module.DoseChangeEvaluationOtp.DoseChangeEvaluationOtpEvent.Parse(tngnWsvcSession);
            //            break;

            //            /* TODO
            //             * Error catch should be here.
            //             */
            //    }
            //}
        }

        /// <summary>
        /// Gets the form name from the translation table based on the original option ID sent by Avatar.
        /// </summary>
        /// <param name="tngnWsvcSession">
        /// The session object containing the original option ID and translation table.
        /// </param>
        /// <returns>The name of the form corresponding to the original option ID.</returns>
        /// <example>
        /// <code>
        /// var formName = AvatarScriptParameter.GetFormName(tngnWsvcSession);
        /// Console.WriteLine($"Resolved form name: {formName}");
        /// </code>
        /// </example>
        internal static string GetFormName(Sess sess)
        {

            //var t = sess.Translation.FormId.GetFormName(tngnWsvcSession.OptObj.Original.OptionId, tngnWsvcSession.Framework.TngnWsvcDataFolder.TranslationTable, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, tngnWsvcSession.LogSetting.TraceLogLimit);

            //return t;
            return "";
        }

    }
}