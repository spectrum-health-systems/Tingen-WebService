// 260725_code
// 260725_documentation

using System;

namespace TingenWebService.Trove
{
    /// <summary>Provides blueprint templates for various logs in the Tingen Web Service.</summary>
    /// <remarks>Blueprints are external templates that can be customized.</remarks>
    internal class Blueprint
    {
        /// <summary>Gets the blueprint template for error logs.</summary>
        /// <returns>A string representing the error log blueprint.</returns>
        internal static string ErrorLogBlueprint() =>
            $"================================================================================{Environment.NewLine}" +
            $"Tingen Web Service Error Log{Environment.NewLine}" +
            $"Date/Time: ~SESSION~DATE~TIME~{Environment.NewLine}" +
            $"================================================================================{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"[Error code] ~ERROR~CODE~{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"~LOG~MESSAGE~{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"--------------------------------------------------------------------------------{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"[ASSEMBLY] ~ASSEMBLY~{Environment.NewLine}" +
            $"[CLASS] ~CLASS~{Environment.NewLine}" +
            $"[METHOD] ~METHOD~{Environment.NewLine}" +
            $"[LINE] ~LINE~{Environment.NewLine}";

        /// <summary>Gets the blueprint template for critical error logs.</summary>
        /// <returns>A string representing the critical error log blueprint.</returns>
        internal static string CriticalErrorLogBlueprint() =>
            $"================================================================================{Environment.NewLine}" +
            $"Tingen Web Service Critical Error Log{Environment.NewLine}" +
            $"Date/Time: ~SESSION~DATE~TIME~{Environment.NewLine}" +
            $"================================================================================{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"[Error code] ~ERROR~CODE~{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"~LOG~MESSAGE~{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"--------------------------------------------------------------------------------{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"[ASSEMBLY] ~ASSEMBLY~{Environment.NewLine}" +
            $"[CLASS] ~CLASS~{Environment.NewLine}" +
            $"[METHOD] ~METHOD~{Environment.NewLine}" +
            $"[LINE] ~LINE~{Environment.NewLine}";

        /// <summary>Gets the blueprint template for session logs.</summary>
        /// <returns>A string representing the session log blueprint.</returns>
        internal static string SessionLogBlueprint() =>
            $"================================================================================{Environment.NewLine}" +
            $"Tingen Web Service Session Log                               [~RELEASE~BUILD~]  {Environment.NewLine}" +
            $"--------------------------------------------------------------------------------{Environment.NewLine}" +
            $"            Date: ~SESSION~DATE~{Environment.NewLine}" +
            $"           Start: ~SESSION~START~{Environment.NewLine}" +
            $"             End: ~SESSION~END~{Environment.NewLine}" +
            $"        Duration: ~SESSION~DURATION~{Environment.NewLine}" +
            $"    Logged in as: ~AVATAR~USER~NAME~{Environment.NewLine}" +
            $"   Avatar system: ~AVATAR~SYSTEM~{Environment.NewLine}" +
            $"Script parameter: ~SCRIPT~PARAMETER~{Environment.NewLine}" +
            $"================================================================================{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"--------------{Environment.NewLine}" +
            $"Session Detail{Environment.NewLine}" +
            $"--------------{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"~SESSION~DETAILS~";

        /// <summary>Gets the blueprint template for unknown parameter logs.</summary>
        /// <returns>A string representing the unknown parameter log blueprint.</returns>
        internal static string OptObjUnknownParameterBlueprint() =>
            "Unknown parameter: ~COMMAND~ [v~VERSION~ e~ERROR~CODE~]";

    }
}