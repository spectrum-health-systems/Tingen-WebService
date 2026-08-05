// 260805_code
// 260805_documentation

using System;
using TingenWebService.Core.Logger;
using TingenWebService.Core.Session;
using TingenWebService.Core.Trove;
using TingenWebService.Core.Utility;

namespace TingenWebService.Core.Avatar
{
    /// <summary>Data exchanged between Avatar and the Tingen Web Service.</summary>
    /// <remarks>
    /// <include file='AppData/XmlDoc/Topics.xml' path='Topics/Topic[@name="AvatarData"]/AboutAvatarData/*'/><br/>
    /// <include file='AppData/XmlDoc/Topics.xml' path='Topics/Topic[@name="AvatarData"]/AboutTheScriptParameter/*'/><br/>
    /// </remarks>
    internal class AvatarScriptParameter
    {
        /// <summary>The original script parameter sent from Avatar.</summary>
        /// <remarks>
        /// <include file='AppData/XmlDoc/Topics.xml' path='Topics/Topic[@name="AvatarData"]/AboutTheScriptParameter/*'/>
        /// <include file='AppData/XmlDoc/Topics.xml' path='Topics/Topic[@name="AvatarData"]/TypesOfScriptParameters/*'/>
        /// </remarks>
        public string SentScriptParameter { get; set; }

        ///////// <summary>Initialize a new AvatarData object.</summary>
        ///////// <param name="sentScriptParameter">The script parameter sent from Avatar.</param>
        ///////// <remarks>TBD</remarks>
        ///////// <returns>A new instance of <see cref="AvatarScriptParameter"/>.</returns>
        //////internal static AvatarScriptParameter Build(string sentScriptParameter)
        //////{
        //////    //LogEvent.Primeval("PRELOG-TRACE-AvatarScriptParameter-Build");

        //////    return new AvatarScriptParameter
        //////    {
        //////        SentScriptParameter = sentScriptParameter
        //////    };
        //////}

        /// <summary>Verify whether a script parameter was received from Avatar.</summary>
        /// <param name="sentScriptParameter">The script parameter to verify.</param>
        /// <remarks>TBD</remarks>
        /// <returns>True if a script parameter was sent; otherwise, false.</returns>
        internal static bool WasSent(string sentScriptParameter)
        {
            /* DEVNOTE - This method could be a simple expression-bodied member, but it is written as a full method this
             * is a critical error and needs to be logged.
             */

            //LogEvent.Primeval("PRELOG-TRACE-WasSent-ScriptParameter");

            if (string.IsNullOrWhiteSpace(sentScriptParameter))
            {
                LogEvent.Primeval("ERR1020_MissingScriptParameter");
                // TODO - Potentially send an email in addition to the error logs.

                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>Parses the incoming request for the given session.</summary>
        /// <param name="sess">The web service session object containing form data and module event parsers.</param>
        /// <remarks>
        /// TBD
        /// </remarks>
        internal static void Parse(Sess sess)
        {
            LogEvent.Trace(1, sess.TwsSetting.TraceLimit, sess.FrameworkSetting.SessionRoot);

            sess.RunningLog += RunningLog.ParseRequest(sess.SentScriptParameter);

            if (sess.SentScriptParameter.StartsWith("_", StringComparison.OrdinalIgnoreCase))
            {
                var formName = AvatarForm.GetFormName(sess.FrameworkSetting.TranslationRoot, sess.OptionObject.SentOptionObject.OptionId);
                sess.RunningLog += RunningLog.TranslateFormId(sess.OptionObject.SentOptionObject.OptionId, formName);
            }
            else
            {
                // SpecificFormRequest
            }
        }


        ///// <summary>Handles specific form requests by routing to the appropriate event parser or generating an error.</summary>
        ///// <remarks>
        ///// Generates a hard error when the form name is <c>WSVC2491</c>, indicating the form ID was not found in the
        ///// translation table.<br/>
        ///// <br/>
        ///// Otherwise, routes the known form names <c>OpenIncident</c> and <c>DoseChangeEvaluationOtp</c> to their
        ///// respective event parsers.
        ///// </remarks>
        ///// <param name="specificFormName">The name of the specific form to handle.</param>
        ///// <param name="sess">The web service session object containing form data and module event parsers.</param>
        ///// <example>
        ///// <code>
        ///// AvatarScriptParameter.SpecificFormRequest("OpenIncident", sess);
        ///// AvatarScriptParameter.SpecificFormRequest("DoseChangeEvaluationOtp", sess);
        ///// </code>
        ///// </example>
        //internal static void SpecificFormRequest(string specificFormName, Sess sess)
        //{
        //    if (specificFormName == "WSVC2491")
        //    {
        //        //sess.TngnWsvcSessionError.HardError(tngnWsvcSession, 1, $"[WSVC2491] The form ID '{tngnWsvcSession.OptObj.Original.OptionId}' was not found in the translation table.");
        //    }
        //    else
        //    {
        //        switch (specificFormName)
        //        {
        //            case "OpenIncident":
        //                tngnWsvcSession.Module.OpenIncident.OpenIncidentEvent.Parse(tngnWsvcSession);
        //                break;

        //            case "DoseChangeEvaluationOtp":
        //                tngnWsvcSession.Module.DoseChangeEvaluationOtp.DoseChangeEvaluationOtpEvent.Parse(tngnWsvcSession);
        //                break;

        //                /* TODO
        //                 * Error catch should be here.
        //                 */
        //        }
        //    }
        //}
    }
}