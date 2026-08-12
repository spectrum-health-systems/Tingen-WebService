// 260812_code
// 260812_documentation

using System;
using TingenWebService.Core.Logger;
using TingenWebService.Core.Session;
using TingenWebService.Core.Trove;
using TingenWebService.Core.Utility;
using TingenWebService.Module.OpenIncident;

namespace TingenWebService.Core.Avatar
{
    /// <summary>Objects and logic related to the Avatar <see cref="AvatarScriptParameter">Script Parameter</see></summary>
    /// <remarks>
    /// <include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="AvatarData"]/AboutAvatarData/*'/>
    /// This class focuses on the <see cref="AvatarScriptParameter">Script Parameter</see>.
    /// <include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="AvatarData"]/AboutTheScriptParameter/*'/>
    /// </remarks>
    internal class AvatarScriptParameter
    {
        /// <summary>The original <see cref="AvatarScriptParameter"> Script Parameter</see> sent from Avatar.</summary>
        public string SentScriptParameter { get; set; }

        /// <summary>
        /// Verify whether a <see cref="AvatarScriptParameter">Script Parameter</see> was sent from Avatar.
        /// </summary>
        /// <param name="sentScriptParameter">
        /// The <see cref="AvatarScriptParameter">Script Parameter</see> to verify.
        /// </param>
        /// <remarks>
        /// <para>The <see cref="AvatarScriptParameter">Script Parameter</see> is required, so if Avatar didn't sendone, that's a big
        /// mistake! <i>Big!</i> <i><b>Huge!</b></i><br/>
        /// <br/>
        /// And by "big, huge mistake", I mean that the Tingen Web Service will log an error return an unmodified
        /// <see cref="AvatarOptionObject">OptionObject</see> back to Avatar. </para>
        /// </remarks>
        /// <returns>
        /// <c>True</c> if a <see cref="AvatarScriptParameter">Script Parameter</see> was sent; otherwise, <c>false</c>.
        /// </returns>
        internal static bool WasSent(string sentScriptParameter)
        {
            /* DEVNOTE: This method could be a simplified but it is written as is because this is a critical error and needs to be logged.
             *
             * Do not put trace logs here, it will cause havoc!
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

        /// <summary>Parses the sent <see cref="AvatarScriptParameter">Script Parameter</see> to determine the appropriate action.</summary>
        /// <param name="sess">The Tingen Web Service <see cref="Session">session</see> data.</param>
        /// <remarks>
        /// <include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="AvatarData"]/TypesOfScriptParameters/*'/>
        /// <include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="Trove"]/AboutTranslations/*'/>
        /// </remarks>
        internal static void Parse(Sess sess)
        {
            LogEvent.Trace(1, sess.Trc.Lmt, sess.Trc.Fld);

            sess.RunningLog += RunningLog.ParseRequest(sess.SentScriptParameter);

            if (sess.SentScriptParameter.StartsWith("_", StringComparison.OrdinalIgnoreCase))
            {
                LogEvent.Trace(3, sess.Trc.Lmt, sess.Trc.Fld);

                var formName = AvatarForm.GetTranslatedName(sess.FrameworkSetting.TranslationRoot, sess.OptionObject.SentOptionObject.OptionId, sess.Trc);

                sess.RunningLog += RunningLog.TranslateFormId(sess.OptionObject.SentOptionObject.OptionId, formName);

                SpecificFormRequest(formName, sess);
            }
            else
            {
                LogEvent.Trace(3, sess.Trc.Lmt, sess.Trc.Fld);

                // StandAloneRequest
            }
        }

        /// <summary>Requests handling for a specific form.</summary>
        /// <param name="formName">The name of the specific form to handle.</param>
        /// <param name="sess">The web service session object.</param>
        /// <example>
        /// <code>
        /// AvatarScriptParameter.SpecificFormRequest("OpenIncident", sess);
        /// AvatarScriptParameter.SpecificFormRequest("DoseChangeEvaluationOtp", sess);
        /// </code>
        /// </example>
        internal static void SpecificFormRequest(string formName, Sess sess)
        {
            LogEvent.Trace(1, sess.Trc.Lmt, sess.Trc.Fld);

            switch (formName)
            {
                case "OpenIncident":
                    LogEvent.Trace(3, sess.Trc.Lmt, sess.Trc.Fld);
                    OpenIncidentRequest.Parse(sess);

                    break;

                //    case "DoseChangeEvaluationOtp":
                //        sess.Module.DoseChangeEvaluationOtp.DoseChangeEvaluationOtpEvent.Parse(sess);
                //        break;

                default:
                    //TODO: Implement this.
                    //sess.TngnWsvcSessionError.HardError(sess, 1, $"[WSVC9321] The form name '{formName}' was not found in the translation table.");

                    break;
            }
        }

        ///// <summary>Handles stand-alone requests by routing to the appropriate module or generating an error.</summary>
        ///// <remarks>
        ///// Reserves script parameters that start with <c>tngnwsvc</c> for administrative requests, dispatches
        ///// <c>catchoptionobject</c> to the option object utility, and otherwise emits a hard error indicating the
        ///// script parameter was not found in the translation table.
        ///// </remarks>
        ///// <param name="tngnWsvcSession">The web service session object containing request information.</param>
        ///// <example>
        ///// <code>
        ///// // Dispatches to OptObjUtility.CatchOptionObject:
        ///// tngnWsvcSession.ScriptParameter.OriginalScriptParameter = "catchoptionobject";
        ///// AvatarScriptParameter.StandAloneRequest(tngnWsvcSession);
        ///// </code>
        ///// </example>
        //internal static void StandAloneRequest(TngnWsvcSession tngnWsvcSession)
        //{
        //    if (tngnWsvcSession.ScriptParameter.OriginalScriptParameter.StartsWith("tngnwsvc"))
        //    {
        //        /* TODO
        //         * Add admin static requests here.
        //         */
        //    }
        //    else if (string.Equals(tngnWsvcSession.ScriptParameter.OriginalScriptParameter, "catchoptionobject", StringComparison.CurrentCultureIgnoreCase))
        //    {
        //        tngnWsvcSession.Module.TngnWsvc.OptObjUtility.CatchOptionObject(tngnWsvcSession);
        //    }
        //    else
        //    {
        //        tngnWsvcSession.TngnWsvcSessionError.HardError(tngnWsvcSession, 1, $"[WSVC9321] The Script Parameter request '{tngnWsvcSession.ScriptParameter.OriginalScriptParameter}' was not found in the translation table.");
        //    }
        //}
    }
}