// 251111_code
// 260709_documentation.

using System;
using System.IO;
using System.Reflection;
using TingenWebService.Core.TingenWsvcSession;


namespace TingenWebService.Core.Avatar
{
    /// <summary>Avatar Script Parameter logic.</summary>
    /// <remarks><include file='AppData/XmlDoc/TngnWsvc.xml' path='TngnWsvc/Class[@name="Definition"]/ScriptParameter/*'/></remarks>
    public class AvatarScriptParameter
    {
        /// <summary>The name of the executing assembly, used for logging purposes.</summary>
        public static string ExeAsm { get; set; } = Assembly.GetExecutingAssembly().GetName().Name;

        /// <summary>The original script parameter sent by Avatar.</summary>
        /// <value>A string that tells the Tingen Web Service which action(s) to perform.</value>
        public string OriginalScriptParameter { get; set; }

        /// <summary>Verify whether a script parameter was sent from Avatar.</summary>
        /// <remarks>Pretty straight forward.</remarks>
        /// <param name="origScriptParam">The original script parameter to verify.</param>
        /// <returns>A message indicating whether the script parameter exists.</returns>
        /// <example>
        /// <code>
        /// var message = AvatarScriptParameter.VerifyExistence("_OpenIncident");
        /// Console.WriteLine(message);
        /// // Output: "The sent script parameter ('_OpenIncident') does exist."
        ///
        /// var missing = AvatarScriptParameter.VerifyExistence("");
        /// Console.WriteLine(missing);
        /// // Output: "The sent script parameter ('') does not exist."
        /// </code>
        /// </example>
        public static string VerifyExistence(string origScriptParam)
        {
            return string.IsNullOrWhiteSpace(origScriptParam)
                ? $"The sent script parameter ('{origScriptParam}') does not exist."
                : $"The sent script parameter ('{origScriptParam}') does exist.";
        }

        /// <summary>Route the request.</summary>
        /// <remarks>
        /// Routes to <see cref="SpecificFormRequest"/> when the original script parameter starts with an underscore;
        /// otherwise routes to <see cref="StandAloneRequest"/>.
        /// </remarks>
        /// <param name="tngnWsvcSession">The session object containing script parameter and request information.</param>
        /// <example>
        /// <code>
        /// // Routes to SpecificFormRequest when prefixed with "_":
        /// tngnWsvcSession.ScriptParameter.OriginalScriptParameter = "_OpenIncident";
        /// AvatarScriptParameter.Parse(tngnWsvcSession);
        ///
        /// // Routes to StandAloneRequest otherwise:
        /// tngnWsvcSession.ScriptParameter.OriginalScriptParameter = "catchoptionobject";
        /// AvatarScriptParameter.Parse(tngnWsvcSession);
        /// </code>
        /// </example>
        public static void Parse(TngnWsvcSession tngnWsvcSession)
        {
            if (tngnWsvcSession.ScriptParameter.OriginalScriptParameter.StartsWith("_", StringComparison.OrdinalIgnoreCase))
            {

                var specificFormName = GetFormName(tngnWsvcSession);

                SpecificFormRequest(specificFormName, tngnWsvcSession);
            }
            else
            {

                StandAloneRequest(tngnWsvcSession);

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
        /// <param name="tngnWsvcSession">The web service session object containing form data and module event parsers.</param>
        /// <example>
        /// <code>
        /// AvatarScriptParameter.SpecificFormRequest("OpenIncident", tngnWsvcSession);
        /// AvatarScriptParameter.SpecificFormRequest("DoseChangeEvaluationOtp", tngnWsvcSession);
        /// </code>
        /// </example>
        internal static void SpecificFormRequest(string specificFormName, TngnWsvcSession tngnWsvcSession)
        {
            if (specificFormName == "WSVC2491")
            {
                tngnWsvcSession.TngnWsvcSessionError.HardError(tngnWsvcSession, 1, $"[WSVC2491] The form ID '{tngnWsvcSession.OptObj.Original.OptionId}' was not found in the translation table.");
            }
            else
            {
                switch (specificFormName)
                {
                    case "OpenIncident":
                        tngnWsvcSession.Module.OpenIncident.OpenIncidentEvent.Parse(tngnWsvcSession);
                        break;

                    case "DoseChangeEvaluationOtp":
                        tngnWsvcSession.Module.DoseChangeEvaluationOtp.DoseChangeEvaluationOtpEvent.Parse(tngnWsvcSession);
                        break;

                        /* TODO
                         * Error catch should be here.
                         */
                }
            }
        }

        /// <summary>Handles stand-alone requests by routing to the appropriate module or generating an error.</summary>
        /// <remarks>
        /// Reserves script parameters that start with <c>tngnwsvc</c> for administrative requests, dispatches
        /// <c>catchoptionobject</c> to the option object utility, and otherwise emits a hard error indicating the
        /// script parameter was not found in the translation table.
        /// </remarks>
        /// <param name="tngnWsvcSession">The web service session object containing request information.</param>
        /// <example>
        /// <code>
        /// // Dispatches to OptObjUtility.CatchOptionObject:
        /// tngnWsvcSession.ScriptParameter.OriginalScriptParameter = "catchoptionobject";
        /// AvatarScriptParameter.StandAloneRequest(tngnWsvcSession);
        /// </code>
        /// </example>
        internal static void StandAloneRequest(TngnWsvcSession tngnWsvcSession)
        {
            if (tngnWsvcSession.ScriptParameter.OriginalScriptParameter.StartsWith("tngnwsvc"))
            {
                /* TODO
                 * Add admin static requests here.
                 */
            }
            else if (string.Equals(tngnWsvcSession.ScriptParameter.OriginalScriptParameter, "catchoptionobject", StringComparison.CurrentCultureIgnoreCase))
            {
                tngnWsvcSession.Module.TngnWsvc.OptObjUtility.CatchOptionObject(tngnWsvcSession);
            }
            else
            {
                tngnWsvcSession.TngnWsvcSessionError.HardError(tngnWsvcSession, 1, $"[WSVC9321] The Script Parameter request '{tngnWsvcSession.ScriptParameter.OriginalScriptParameter}' was not found in the translation table.");
            }
        }
    }
}