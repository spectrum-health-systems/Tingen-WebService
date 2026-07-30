// 260729_code
// 260730_documentation

using System;

namespace TingenWebService.Core.Trove
{
    /// <summary>Preset messages and strings.</summary>
    /// <remarks><include file='AppData/XmlDoc/Topics.xml' path='Topics/Topic[@name="RedPrint"]/About/*'/></remarks>
    internal class RedPrint
    {
        /// <summary>Build the framework details message.</summary>
        /// <param name="framework">The framework configuration.</param>
        /// <returns>The framework details message.</returns>
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

        /// <summary>Build the runtime details message.</summary>
        /// <param name="rtConfig">The runtime configuration.</param>
        /// <returns>The runtime details message.</returns>
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