// 260807_code
// 260806_documentation

using System;
using TingenWebService.Core.Framework;

namespace TingenWebService.Core.Trove
{
    /// <summary>Preset messages and strings.</summary>
    /// <remarks><include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="Trove"]/AboutRedprints/*'/></remarks>
    internal static class Redprint
    {
        internal static string DailyLog() =>
            $"v~VERSION~BUILD~{Environment.NewLine}" +
            $"~STARTIME~{Environment.NewLine}" +
            $"~RUNNING~LOG~" +
            $"~DURATION~";

        /// <summary>Build the runtime details message.</summary>
        /// <param name="runtimeSetting">The runtime configuration.</param>
        /// <returns>The runtime details message.</returns>
        internal static string RuntimeDetails(RuntimeSetting runtimeSetting) =>
            $"      Version: v{runtimeSetting.VersionBuild}{Environment.NewLine}" +
            $"Avatar System: {runtimeSetting.AvatarSystem}{Environment.NewLine}" +
            $"    Data Root: {runtimeSetting.DataRoot}{Environment.NewLine}";

        /// <summary>Build the framework details message.</summary>
        /// <param name="frameworkSetting">The framework configuration.</param>
        /// <returns>The framework details message.</returns>
        internal static string FrameworkSettings(FrameworkSetting frameworkSetting) =>
            $"Avatar Generated Data: {frameworkSetting.AvatarGeneratedDataRoot}{Environment.NewLine}" +
            $"  Configuration files: {frameworkSetting.ConfigRoot}{Environment.NewLine}" +
            $"              Exports: {frameworkSetting.ExportRoot}{Environment.NewLine}" +
            $"              Imports: {frameworkSetting.ImportRoot}{Environment.NewLine}" +
            $"          System Logs: {frameworkSetting.SysLogRoot}{Environment.NewLine}" +
            $"           Blueprints: {frameworkSetting.BlueprintRoot}{Environment.NewLine}" +
            $"         Session data: {frameworkSetting.SessionRoot}{Environment.NewLine}" +
            $"         Translations: {frameworkSetting.TranslationRoot}{Environment.NewLine}";


        /// <summary>Build the message indicating that blueprints have been exported.</summary>
        /// <returns>The blueprints exported message.</returns>
        internal static string BlueprintsExported() =>
            $"[Blueprints exported]{Environment.NewLine}"; // TODO - Do the same things we did with FrameworkDetails
    }
}