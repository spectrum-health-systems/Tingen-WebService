// 260727_code
// 260727_documentation

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

        /// <summary>Gets the message indicating the start of the daily log.</summary>
        /// <returns>A string representing the daily start message.</returns>
        internal static string DailyStart(string releaseBuild) => $"> Release {releaseBuild}{Environment.NewLine}";

        /// <summary>Gets the message indicating that the framework has been verified.</summary>
        /// <returns>A string representing the framework verified message.</returns>
        internal static string FrameworkVerified() => $"> Framework verified{Environment.NewLine}";

        /// <summary>Gets the message indicating that blueprints have been exported.</summary>
        /// <returns>A string representing the blueprints exported message.</returns>
        internal static string BlueprintsExported() => $"> Blueprints exported{Environment.NewLine}";

        internal static string DebugStartMessage(string sentScriptParam) => $"RunScript called with script parameter: {sentScriptParam}.";



        /*
         * Error messages
         */
        internal static string Error3876() =>
            $"[3876]{Environment.NewLine}" +
            $"Missing OptionObject and/or Script Parameter";

        internal static string Error7622(string path, string errorMessage) =>
            $"[7622]{Environment.NewLine}" +
            $"Framework verification failed:{Environment.NewLine}" +
            $"  Path could not be created: {path}{Environment.NewLine}" +
            $"  Message:  {errorMessage}";

        internal static string Error7516(string path, string errorMessage) =>
            $"[7516]{Environment.NewLine}" +
            $"Maintenance failure:{Environment.NewLine}" +
            $"  Path could not be created: {path}{Environment.NewLine}" +
            $"  Message:  {errorMessage}";

        internal static string Error4694() =>
            $"[4694]{Environment.NewLine}" +
            $"Missing Tingen Web Service configuration file";


    }
}