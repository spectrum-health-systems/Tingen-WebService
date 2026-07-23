// 251112_code
// 260515_documentation.

using ScriptLinkStandard.Objects;

/* TODO
 * A note explaining this class.
 */
namespace TingenWebService.Core.Catalog
{
    /// <summary>Provides Tingen Web Service log message catalogs.</summary>
    public class msg_TngnWscv
    {
        /// <summary>Builds the log message emitted when a required request component is missing.</summary>
        /// <param name="origOptObj">The original <see cref="OptionObject2015"/> received with the request.</param>
        /// <param name="origScriptParam">The original script parameter received with the request.</param>
        /// <returns>A formatted log message describing the missing component(s).</returns>
        /// <example>
        /// <code>
        /// var logMessage = msg_TngnWscv.MissingComponent(sentOptionObject, "_OpenIncident");
        /// Console.WriteLine(logMessage);
        /// // Output:
        /// // The OptionObject ("ScriptLinkStandard.Objects.OptionObject2015") and/or Script Parameter ("_OpenIncident") are missing.
        /// </code>
        /// </example>
        public static string MissingComponent(OptionObject2015 origOptObj, string origScriptParam) =>
            $"The OptionObject (\"{origOptObj}\") and/or Script Parameter (\"{origScriptParam}\") are missing.";

        /// <summary>Builds the log message emitted when the Tingen Web Service is disabled.</summary>
        /// <returns>A log message indicating that the Tingen Web Service is disabled.</returns>
        /// <example>
        /// <code>
        /// Console.WriteLine(msg_TngnWscv.DisabledMode());
        /// // Output: The Tingen Web Service is disabled
        /// </code>
        /// </example>
        public static string DisabledMode() =>
            "The Tingen Web Service is disabled";

        /// <summary>Builds the log message emitted when the Tingen Web Service is in an unknown mode.</summary>
        /// <returns>A log message indicating that the Tingen Web Service is in an unknown state.</returns>
        /// <example>
        /// <code>
        /// Console.WriteLine(msg_TngnWscv.UnknownMode());
        /// // Output: The Tingen Web Service is in an unknown state
        /// </code>
        /// </example>
        public static string UnknownMode() =>
            "The Tingen Web Service is in an unknown state";
    }
}