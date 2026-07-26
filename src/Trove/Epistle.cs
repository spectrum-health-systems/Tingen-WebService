// 260725_code
// 260724_documentation

using System;

namespace TingenWebService.Trove
{
    /// <summary>Provides predefined messages for the Tingen Web Service.</summary>
    /// <remarks>Epistles are preset messages that cannot be modified.</remarks>
    internal class Epistle
    {
        /// <summary>Gets the message indicating that Tingen Web Service testing is complete.</summary>
        /// <returns>A string representing the testing complete message.</returns>
        internal static string TngnWsvcTestingComplete() => "Tingen Web Service testing complete!";

        /// <summary>Gets the message indicating the start of the history log.</summary>
        /// <returns>A string representing the history start message.</returns>
        internal static string HistoryStart(string twsRelease, string twsBuild) =>
            $"> {DateTime.Now.ToString("yyyyMMdd-HH:mm:ss")}{Environment.NewLine}" +
            $"> Release {twsRelease}{Environment.NewLine}" +
            $"> Build {twsBuild}{Environment.NewLine}";

        /// <summary>Gets the message indicating that the framework has been verified.</summary>
        /// <returns>A string representing the framework verified message.</returns>
        internal static string FrameworkVerified() => $"> Framework verified.{Environment.NewLine}";

        /// <summary>Gets the message indicating that blueprints have been exported.</summary>
        /// <returns>A string representing the blueprints exported message.</returns>
        internal static string BlueprintsExported() => $"> Blueprints exported.{Environment.NewLine}";

        // TODO - Error message / Critical error message
    }
}