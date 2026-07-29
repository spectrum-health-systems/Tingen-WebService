// 260729_code
// 260729_documentation

using System;

namespace TingenWebService.Core.Trove
{
    /// <summary>Preset messages and strings.</summary>
    /// <remarks>
    /// <note type="note" title="About redprints">
    /// Redprints are preset<b>messages</b> that cannot be modified by the user, and are used to format data in a
    /// consistent manner.
    /// </note>
    /// </remarks>
    internal class RedPrint
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

        internal static string FrameworkDetails(Framework.FrwkConfig framework)
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
                   $"               Session Timeout: {twsConfig.SessTimeout}{Environment.NewLine}" +
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
    }
}