// 260807_code
// 260806_documentation

using System;
using TingenWebService.Core.Framework;
using TingenWebService.Module.OpenIncident;

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

        /// <summary>Build the configuration details message.</summary>
        /// <param name="appSetting">The Tingen Web Service configuration.</param>
        /// <returns>The configuration details message.</returns>
        internal static string ApplicationSettings(AppSetting appSetting) =>
            $"          Application settings{Environment.NewLine}" +
            $"------------------------------{Environment.NewLine}" +
            $"                          Mode: {appSetting.Mode}{Environment.NewLine}" +
            $"             Trace Level Limit: {appSetting.TraceLimit}{Environment.NewLine}" +
            $"                     Log Delay: {appSetting.LogDelay}{Environment.NewLine}" +
            $"      Session Log Detail Level: {appSetting.SessionLogDetailLevel}{Environment.NewLine}" +
            $"       Session Log text format: {appSetting.SessionLogTextFormat}{Environment.NewLine}" +
            $"   Session Log Markdown format: {appSetting.SessionLogMarkdownFormat}{Environment.NewLine}" +
            $"               Session Timeout: {appSetting.SessionTimeout}{Environment.NewLine}" +
            $"         Error Log text format: {appSetting.ErrorLogTextFormat}{Environment.NewLine}" +
            $"     Error Log Markdown format: {appSetting.ErrorLogMarkdownFormat}{Environment.NewLine}" +
            $"         Error Log HTML format: {appSetting.ErrorLogHtmlFormat}{Environment.NewLine}" +
            $"            From Email Address: {appSetting.FromEmailAddress}{Environment.NewLine}" +
            $"           From Email Password: Please see TngnWsvc.config{Environment.NewLine}" +
            $"              To Email Address: {string.Join(", ", appSetting.ToEmailAddress)}{Environment.NewLine}" +
            $"                  Email Format: {appSetting.EmailFormat}{Environment.NewLine}" +
            $"Netsmart web services username: {appSetting.NtstWsvcUserName}{Environment.NewLine}" +
            $"Netsmart web services password: Please see TngnWsvc.config{Environment.NewLine}";

        internal static string OpenIncidentConfig(OpenIncidentSetting openIncidentSetting) =>
            $"                  Open Incident Module Config{Environment.NewLine}" +
            $"---------------------------------------------{Environment.NewLine}" +
            $"                                         Mode: {openIncidentSetting.Mode}{Environment.NewLine}" +
            $"                                  Bypass List: {string.Join(", ", openIncidentSetting.BypassList)}{Environment.NewLine}" +
            $"                        Authorized User Roles: {string.Join(", ", openIncidentSetting.AuthorizedUserRoles)}{Environment.NewLine}" +
            $"          Brief Incident Description Field ID: {openIncidentSetting.BriefIncidentDescriptionFieldId}{Environment.NewLine}" +
            $"                 Program of Incident Field ID: {openIncidentSetting.ProgramOfIncidentFieldId}{Environment.NewLine}" +
            $"     Person Completing Incident Form Field ID: {openIncidentSetting.PersonCompletingIncidentFormFieldId}{Environment.NewLine}" +
            $"   Not Member of Authorized User Role Message: {openIncidentSetting.NotMemberOfAuthorizedUserRoleMsg}{Environment.NewLine}" +
            $"Not Member of Authorized User Role Error Code: {openIncidentSetting.NotMemberOfAuthorizedUserRoleErrCode}{Environment.NewLine}" +
            $"             Not Original Author Open Message: {openIncidentSetting.NotOriginalAuthorOpenMsg}{Environment.NewLine}" +
            $"          Not Original Author Open Error Code: {openIncidentSetting.NotOriginalAuthorOpenErrCode}{Environment.NewLine}" +
            $"           Not Original Author Submit Message: {openIncidentSetting.NotOriginalAuthorSubmitMsg}{Environment.NewLine}" +
            $"        Not Original Author Submit Error Code: {openIncidentSetting.NotOriginalAuthorSubmitErrCode}{Environment.NewLine}" +
            $"          Invalid Program of Incident Message: {openIncidentSetting.InvalidProgramOfIncidentMsg}{Environment.NewLine}" +
            $"       Invalid Program of Incident Error Code: {openIncidentSetting.InvalidProgramOfIncidentErrCode}{Environment.NewLine}";
    }
}