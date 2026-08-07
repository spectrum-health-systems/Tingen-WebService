// 260807_code
// 260806_documentation

using System;
using TingenWebService.Core.Logger;
using TingenWebService.Core.Session;
using TingenWebService.Core.Trove;
using TingenWebService.Core.Utility;
using TingenWebService.Module.OpenIncident;

namespace TingenWebService.Core.Avatar
{
    /// <summary>Data exchanged between Avatar and the Tingen Web Service.</summary>
    /// <remarks>
    /// <include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="AvatarData"]/AboutAvatarData/*'/><br/>
    /// <include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="AvatarData"]/AboutTheScriptParameter/*'/><br/>
    /// </remarks>
    internal class AvatarScriptParameter
    {
        /// <summary>The original script parameter sent from Avatar.</summary>
        /// <remarks>
        /// <include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="AvatarData"]/AboutTheScriptParameter/*'/>
        /// <include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="AvatarData"]/TypesOfScriptParameters/*'/>
        /// </remarks>
        public string SentScriptParameter { get; set; }

        ///////// <summary>Initialize a new AvatarData object.</summary>
        ///////// <param name="sentScriptParameter">The script parameter sent from Avatar.</param>
        ///////// <remarks>TBD</remarks>
        ///////// <returns>A new instance of <see cref="AvatarScriptParameter"/>.</returns>
        //////internal static AvatarScriptParameter Build(string sentScriptParameter)
        //////{
        //////    LogEvent.Primeval("PRELOG-TRACE-AvatarScriptParameter-Build");

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

            LogEvent.Primeval("PRELOG-TRACE-WasSent-ScriptParameter");

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
            LogEvent.Trace(1, sess.Trc.Lmt, sess.Trc.Fld);

            sess.RunningLog += RunningLog.ParseRequest(sess.SentScriptParameter);

            if (sess.SentScriptParameter.StartsWith("_", StringComparison.OrdinalIgnoreCase))
            {
                LogEvent.Trace(4, sess.Trc.Lmt, sess.Trc.Fld);

                var formName = AvatarForm.GetFormName(sess.FrameworkSetting.TranslationRoot, sess.OptionObject.SentOptionObject.OptionId, sess.Trc);

                sess.RunningLog += RunningLog.TranslateFormId(sess.OptionObject.SentOptionObject.OptionId, formName);

                SpecificFormRequest(formName, sess);
            }
            else
            {
                LogEvent.Trace(4, sess.Trc.Lmt, sess.Trc.Fld);
                // StandAlongRequest
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
        /// <param name="formName">The name of the specific form to handle.</param>
        /// <param name="sess">The web service session object containing form data and module event parsers.</param>
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
                    LogEvent.Trace(4, sess.Trc.Lmt, sess.Trc.Fld);
                    OpenIncidentRequest.Parse(sess);

                    break;

                    //    case "DoseChangeEvaluationOtp":
                    //        sess.Module.DoseChangeEvaluationOtp.DoseChangeEvaluationOtpEvent.Parse(sess);
                    //        break;

                    //    default:
                    //        sess.TngnWsvcSessionError.HardError(sess, 1, $"[WSVC9321] The form name '{formName}' was not found in the translation table.");
                    //        break;
                    //
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


/*
         internal static void SpecificFormRequest(string formName, Sess sess)
        {
            LogEvent.Trace(1, sess.Trc.Lmt, sess.Trc.Fld);





            if (formName == "WSVC2491")
            {
                //sess.TngnWsvcSessionError.HardError(tngnWsvcSession, 1, $"[WSVC2491] The form ID '{tngnWsvcSession.OptObj.Original.OptionId}' was not found in the translation table.");
            }
            else
            {
                switch (formName)
                {
                    case "OpenIncident":
                        //sess.Module.OpenIncident.OpenIncidentEvent.Parse(sess);
                        break;

                        //case "DoseChangeEvaluationOtp":
                        //    sess.Module.DoseChangeEvaluationOtp.DoseChangeEvaluationOtpEvent.Parse(sess);
                        //    break;

                        /* TODO
                         * Error catch should be here.
                         */
