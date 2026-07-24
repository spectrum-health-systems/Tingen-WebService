// 260724_code
// 260724_documentation
using System;

namespace TingenWebService.Core
{
    internal class Blueprint
    {
        internal static string CriticalErrorMessage() =>
            "================================================================================" +
            "Tingen Web Service Critical Log" +
            "Date/Time: ~SESSION~DATE~TIME~" +
            "================================================================================" +
            Environment.NewLine +
            "~LOG~MESSAGE~" +
            Environment.NewLine +
            "--------------------------------------------------------------------------------" +
            Environment.NewLine +
            "[ASSEMBLY] ~ASSEMBLY~" +
            "[CLASS] ~CLASS~" +
            "[METHOD] ~METHOD~" +
            "[LINE] ~LINE~";
    }
}