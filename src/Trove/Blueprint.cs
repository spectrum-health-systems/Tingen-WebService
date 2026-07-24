// 260724_code
// 260724_documentation
using System;
using System.IO;
using TingenWebService.Du;

namespace TingenWebService.Core
{
    internal class Blueprint
    {
        internal static void ExportAllToHost(string blueprintRoot)
        {
            DuFile.DeadDrop(Path.Combine(blueprintRoot, "ErrorMessage.blueprint"), blpt_Error());
            DuFile.DeadDrop(Path.Combine(blueprintRoot, "CriticalErrorMessage.blueprint"), blpt_CriticalError());
            DuFile.DeadDrop(Path.Combine(blueprintRoot, "SessionLog.blueprint"), blpt_SessionLog());
        }

        internal static string blpt_Error() =>
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

        internal static string blpt_CriticalError() =>
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

        internal static string blpt_SessionLog() =>
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

        internal static string blpt_OptObjUnknownParameter() =>
            "Unknown parameter: ~COMMAND~ [v~VERSION~ e~ERROR~CODE~]";

    }
}