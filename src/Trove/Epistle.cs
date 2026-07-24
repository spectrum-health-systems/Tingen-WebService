// 260724_code
// 260724_documentation

using System;

namespace TingenWebService.Trove
{
    /// <summary>Provides predefined messages for the Tingen Web Service.</summary>
    /// <remarks>
    /// Preset messages.
    /// </remarks>
    internal class Epistle
    {
        internal static string TngnWsvcTestingComplete() => "Tingen Web Service testing complete!";

        internal static string HistoryStart() => $"> {DateTime.Now.ToString("yyyyMMdd-HH:mm:ss")}{Environment.NewLine}";

        internal static string FrameworkVerified() => $"> Framework verified.{Environment.NewLine}";

        internal static string BlueprintsExported() => $"> Blueprints exported.{Environment.NewLine}";
    }
}