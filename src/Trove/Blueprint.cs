// 260724_code
// 260724_documentation

using System;
using System.IO;
using TingenWebService.Du;

namespace TingenWebService.Trove
{
    /// <summary>Provides blueprint templates for various logs in the Tingen Web Service.</summary>
    /// <remarks>
    /// Blueprints are templates.
    /// </remarks>
    internal class Blueprint
    {
        internal static void ExportAllToHost(string blueprintRoot)
        {
            DuFile.DeadDrop(Path.Combine(blueprintRoot, "ErrorMessage.blueprint"), ErrorLogBlueprint());
            DuFile.DeadDrop(Path.Combine(blueprintRoot, "CriticalErrorMessage.blueprint"), CriticalErrorLogBlueprint());
            DuFile.DeadDrop(Path.Combine(blueprintRoot, "SessionLog.blueprint"), SessionLogBlueprint());
            DuFile.DeadDrop(Path.Combine(blueprintRoot, "UnknownParameter.blueprint"), OptObjUnknownParameterBlueprint());
        }

        internal static string ErrorLogBlueprint() =>
            $"================================================================================{Environment.NewLine}" +
            $"Tingen Web Service Error Log{Environment.NewLine}" +
            $"Date/Time: ~SESSION~DATE~TIME~{Environment.NewLine}`" +
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

        internal static string OptObjUnknownParameterBlueprint() =>
            "Unknown parameter: ~COMMAND~ [v~VERSION~ e~ERROR~CODE~]";

    }
}