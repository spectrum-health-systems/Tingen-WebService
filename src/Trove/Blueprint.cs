// 260724_code
// 260724_documentation

using System;
using System.IO;
using TingenWebService.Du;

namespace TingenWebService.Trove
{
    /// <summary>Provides blueprint templates for various logs in the Tingen Web Service.</summary>
    /// <remarks>Blueprints are external templates that can be customized.</remarks>
    internal class Blueprint
    {
        /// <summary>Exports all blueprint templates to the specified host directory.</summary>
        /// <param name="blueprintRoot">The root directory where the blueprints will be exported.</param>
        internal static void ExportAllToHost(string blueprintRoot)
        {
            DuFile.DeadDrop(Path.Combine(blueprintRoot, "ErrorMessage.blueprint"), ErrorLogBlueprint());
            DuFile.DeadDrop(Path.Combine(blueprintRoot, "CriticalErrorMessage.blueprint"), CriticalErrorLogBlueprint());
            DuFile.DeadDrop(Path.Combine(blueprintRoot, "SessionLog.blueprint"), SessionLogBlueprint());
            DuFile.DeadDrop(Path.Combine(blueprintRoot, "UnknownParameter.blueprint"), OptObjUnknownParameterBlueprint());
        }

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
            $"Tingen Web Service Session Log{Environment.NewLine}" +
            $"Date: ~SESSION~DATE~{Environment.NewLine}" +
            $"Time: ~SESSION~START~-~SESSION~END~{Environment.NewLine}" +
            $"Duration: ~SESSION~DURATION~{Environment.NewLine}" +
            $"Logged in as: ~OPTIONID~{Environment.NewLine}" +
            $"Avatar system: ~AVATAR~SYSTEM~{Environment.NewLine}" +
            $"Script parameter: ~SCRIPT~PARAMETER~{Environment.NewLine}" +
            $"================================================================================{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"Detail{Environment.NewLine}" +
            $"--------{Environment.NewLine}" +
            $"~RUNNING~LOG~";

        /// <summary>Gets the blueprint template for unknown parameter logs.</summary>
        /// <returns>A string representing the unknown parameter log blueprint.</returns>
        internal static string OptObjUnknownParameterBlueprint() =>
            "Unknown parameter: ~COMMAND~ [v~VERSION~ e~ERROR~CODE~]";

    }
}