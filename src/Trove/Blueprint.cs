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
            "================================================================================" +
            "Tingen Web Service Error Log" +
            "Date/Time: ~SESSION~DATE~TIME~" +
            "================================================================================" +
            Environment.NewLine +
            "[Error code] ~ERROR~CODE~" +
            Environment.NewLine +
            "~LOG~MESSAGE~" +
            Environment.NewLine +
            "--------------------------------------------------------------------------------" +
            Environment.NewLine +
            "[ASSEMBLY] ~ASSEMBLY~" +
            "[CLASS] ~CLASS~" +
            "[METHOD] ~METHOD~" +
            "[LINE] ~LINE~";

        internal static string CriticalErrorLogBlueprint() =>
            "================================================================================" +
            "Tingen Web Service Critical Error Log" +
            "Date/Time: ~SESSION~DATE~TIME~" +
            "================================================================================" +
            Environment.NewLine +
            "[Error code] ~ERROR~CODE~" +
            Environment.NewLine +
            "~LOG~MESSAGE~" +
            Environment.NewLine +
            "--------------------------------------------------------------------------------" +
            Environment.NewLine +
            "[ASSEMBLY] ~ASSEMBLY~" +
            "[CLASS] ~CLASS~" +
            "[METHOD] ~METHOD~" +
            "[LINE] ~LINE~";

        internal static string SessionLogBlueprint() =>
            "================================================================================" +
            "Tingen Web Service Session Log" +
            "Date: ~SESSION~DATE~" +
            "Time: ~SESSION~START~-~SESSION~END~" +
            "Duration: ~SESSION~DURATION~" +
            "Logged in as: ~OPTIONID~" +
            "Avatar system: ~AVATAR~SYSTEM~" +
            "Script parameter: ~SCRIPT~PARAMETER~" +
            "================================================================================" +
            Environment.NewLine +
            "Detail" +
            "--------" +
            "~RUNNING~LOG~";

        internal static string OptObjUnknownParameterBlueprint() =>
            "Unknown parameter: ~COMMAND~ [v~VERSION~ e~ERROR~CODE~]";

    }
}