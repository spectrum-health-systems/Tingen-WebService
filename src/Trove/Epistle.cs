// 260728_code
// 260728_documentation

using System;
using TingenWebService.Configuration;
using TingenWebService.Core;

namespace TingenWebService.Trove
{
    /// <summary>Preset messages and strings.</summary>
    /// <remarks>
    /// <note type="note" title="About epistles">
    /// Epistles are preset<b>messages</b> that cannot be modified by the user, and are used to format data in a
    /// consistent manner.
    /// </note>
    /// </remarks>
    internal class Epistle
    {

        /*
         * Logs
         */

        /// <summary>Build the debug start message.</summary>
        /// <param name="sentScriptParam">The script parameter sent to the debug start message.</param>
        /// <returns>The debug start message.</returns>
        internal static string DebugStartMessage(string sentScriptParam) => $"RunScript called with script parameter: {sentScriptParam}.";

        /// <summary>Build the daily maintenance system log start message.</summary>
        /// <returns>The daily maintenance system log start message.</returns>
        internal static string DailyStart(string releaseBuild) => $"[Release] {releaseBuild}{Environment.NewLine}";

        /*
         * Components
         */

        /// <summary>Build the message indicating that the framework has been verified.</summary>
        /// <returns>The framework verified message.</returns>
        internal static string FrameworkVerified() => $"[Framework verified]{Environment.NewLine}";

        internal static string FrameworkDetails(Framework framework)
        {
            // TODO - Do the same things for Blueprints

            return $"         Framework details{Environment.NewLine}" +
                   $"--------------------------{Environment.NewLine}" +
                   $"Avatar Generated Data Root: {framework.AvatarGeneratedDataRoot}{Environment.NewLine}" +
                   $"               Config Root: {framework.ConfigRoot}{Environment.NewLine}" +
                   $"               Export Root: {framework.ExportRoot}{Environment.NewLine}" +
                   $"               Import Root: {framework.ImportRoot}{Environment.NewLine}" +
                   $"               SysLog Root: {framework.SysLogRoot}{Environment.NewLine}" +
                   $"           Blueprints Root: {framework.BlueprintRoot}{Environment.NewLine}" +
                   $"              Session Root: {framework.SessionRoot}{Environment.NewLine}" +
                   $"    Translation Table Root: {framework.TranslationTableRoot}{Environment.NewLine}";
        }

        /// <summary>Build the message indicating that blueprints have been exported.</summary>
        /// <returns>The blueprints exported message.</returns>
        internal static string BlueprintsExported() => $"[Blueprints exported]{Environment.NewLine}"; // TODO - Do the same things we did with FrameworkDetails

        internal static string ConfigurationDetails(TwsConfig twsConfig)
        {
            return $"         Configuration details{Environment.NewLine}" +
                   $"------------------------------{Environment.NewLine}" +
                   $"                          Mode: {twsConfig.Mode}{Environment.NewLine}" +
                   $"             Trace Level Limit: {twsConfig.TraceLevelLimit}{Environment.NewLine}" +
                   $"                     Log Delay: {twsConfig.LogDelay}{Environment.NewLine}" +
                   $"               Session Timeout: {twsConfig.SessionTimeout}{Environment.NewLine}" +
                   $"            From Email Address: {twsConfig.FromEmailAddress}{Environment.NewLine}" +
                   $"           From Email Password: Please see TngnWsvc.config{Environment.NewLine}" +
                   $"              To Email Address: {string.Join(", ", twsConfig.ToEmailAddress)}{Environment.NewLine}" +
                   $"Netsmart web services username: {twsConfig.NtstWsvcUserName}{Environment.NewLine}" +
                   $"Netsmart web services password: Please see TngnWsvc.config{Environment.NewLine}";
        }

        internal static string RuntimeDetails(RuntimeConfig rtConfig)
        {
            return $"Runtime details{Environment.NewLine}" +
                   $"---------------{Environment.NewLine}" +
                   $"Release Build: {rtConfig.ReleaseBuild}{Environment.NewLine}" +
                   $"Avatar System: {rtConfig.AvatarSystem}{Environment.NewLine}" +
                   $"    Data Root: {rtConfig.DataRoot}{Environment.NewLine}";
        }

        /*
         * Error messages
         */

        /// <summary>Build the message missing Avatar data message.</summary>
        /// <returns>The missing Avatar data message.</returns>
        internal static string Error3876() =>
            $"[3876]{Environment.NewLine}" +
            $"The OptionOnject was not sent from Avatar";

        /// <summary>Build the message missing Avatar data message.</summary>
        /// <returns>The missing Avatar data message.</returns>
        internal static string Error5285() =>
            $"[5285]{Environment.NewLine}" +
            $"The Script Parameter was not sent from Avatar";

        /// <summary>Build the framework verification failed message.</summary>
        /// <param name="path">The path that could not be created.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns>The framework verification failed message.</returns>
        internal static string Error7622(string path, string errorMessage) =>
            $"[7622]{Environment.NewLine}" +
            $"Framework verification failed:{Environment.NewLine}" +
            $"  Path could not be created: {path}{Environment.NewLine}" +
            $"  Message:  {errorMessage}";

        /// <summary>Build the maintenance failure message.</summary>
        /// <param name="path">The path that could not be created.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns>The maintenance failure message.</returns>
        internal static string Error7516(string path, string errorMessage) =>
            $"[7516]{Environment.NewLine}" +
            $"Maintenance failure:{Environment.NewLine}" +
            $"  Path could not be created: {path}{Environment.NewLine}" +
            $"  Message:  {errorMessage}";

        /// <summary>Build the missing TngnWsvc.config message.</summary>
        /// <returns>The missing configuration file message.</returns>
        internal static string Error4694() =>
            $"[4694]{Environment.NewLine}" +
            $"Missing Tingen Web Service configuration file";

        internal static string Error7362(string timeoutSetting) =>
            $"Session exceeded timeout setting: {timeoutSetting}";
    }
}