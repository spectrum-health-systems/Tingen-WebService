// 260806_code
// 260806_documentation

using System;
using TingenWebService.Core.Framework;
using TingenWebService.Module.OpenIncident;

namespace TingenWebService.Core.Trove
{
    /// <summary>Preset messages and strings.</summary>
    /// <remarks><include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="Trove"]/AboutRedprints/*'/></remarks>
    internal class Redprint
    {
        internal static string DailyLog()
        {
            return $"v~VERSION~BUILD~{Environment.NewLine}" +
                   $"~STARTIME~{Environment.NewLine}" +
                   $"~RUNNING~LOG~" +
                   $"~DURATION~";
        }

        /// <summary>Build the runtime details message.</summary>
        /// <param name="rtConfig">The runtime configuration.</param>
        /// <returns>The runtime details message.</returns>
        internal static string RuntimeDetails(RuntimeSetting rtConfig)
        {
            return $"Version (build): v{rtConfig.VersionBuild}{Environment.NewLine}" +
                   $"Avatar System: {rtConfig.AvatarSystem}{Environment.NewLine}" +
                   $"Data Root: {rtConfig.DataRoot}{Environment.NewLine}";
        }

        /// <summary>Build the framework details message.</summary>
        /// <param name="framework">The framework configuration.</param>
        /// <returns>The framework details message.</returns>
        internal static string FrameworkSettings(FrameworkSetting framework)
        {
            // TODO - Do the same things for Blueprints

            return
                   $"Avatar Generated Data: {framework.AvatarGeneratedDataRoot}{Environment.NewLine}" +
                   $"Configuration files: {framework.ConfigRoot}{Environment.NewLine}" +
                   $"Exports: {framework.ExportRoot}{Environment.NewLine}" +
                   $"Imports: {framework.ImportRoot}{Environment.NewLine}" +
                   $"System Logs: {framework.SysLogRoot}{Environment.NewLine}" +
                   $"Blueprints: {framework.BlueprintRoot}{Environment.NewLine}" +
                   $"Session data: {framework.SessionRoot}{Environment.NewLine}" +
                   $"Translations: {framework.TranslationRoot}{Environment.NewLine}";
        }

        /// <summary>Build the message indicating that blueprints have been exported.</summary>
        /// <returns>The blueprints exported message.</returns>
        internal static string BlueprintsExported()
            => $"[Blueprints exported]{Environment.NewLine}"; // TODO - Do the same things we did with FrameworkDetails

        /// <summary>Build the configuration details message.</summary>
        /// <param name="appSetting">The Tingen Web Service configuration.</param>
        /// <returns>The configuration details message.</returns>
        internal static string ApplicationSettings(AppSetting appSetting)
        {
            return $"          Application settings{Environment.NewLine}" +
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
        }

        internal static string OpenIncidentConfig(OpenIncidentConfig openIncidentConfig)
        {
            // TODO - See note below
            return $"                  Open Incident Module Config{Environment.NewLine}" +
                   $"---------------------------------------------{Environment.NewLine}" +
                   $"                                         Mode: {openIncidentConfig.Mode}{Environment.NewLine}" +
                   $"                                  Bypass List: {string.Join(", ", openIncidentConfig.BypassList)}{Environment.NewLine}" +
                   $"                        Authorized User Roles: {string.Join(", ", openIncidentConfig.AuthorizedUserRoles)}{Environment.NewLine}" +
                   $"          Brief Incident Description Field ID: {openIncidentConfig.BriefIncidentDescriptionFieldId}{Environment.NewLine}" +
                   $"                 Program of Incident Field ID: {openIncidentConfig.ProgramOfIncidentFieldId}{Environment.NewLine}" +
                   $"     Person Completing Incident Form Field ID: {openIncidentConfig.PersonCompletingIncidentFormFieldId}{Environment.NewLine}" +
                   $"   Not Member of Authorized User Role Message: {openIncidentConfig.NotMemberOfAuthorizedUserRoleMsg}{Environment.NewLine}" +
                   $"Not Member of Authorized User Role Error Code: {openIncidentConfig.NotMemberOfAuthorizedUserRoleErrCode}{Environment.NewLine}" +
                   $"             Not Original Author Open Message: {openIncidentConfig.NotOriginalAuthorOpenMsg}{Environment.NewLine}" +
                   $"          Not Original Author Open Error Code: {openIncidentConfig.NotOriginalAuthorOpenErrCode}{Environment.NewLine}" +
                   $"           Not Original Author Submit Message: {openIncidentConfig.NotOriginalAuthorSubmitMsg}{Environment.NewLine}" +
                   $"        Not Original Author Submit Error Code: {openIncidentConfig.NotOriginalAuthorSubmitErrCode}{Environment.NewLine}" +
                   $"          Invalid Program of Incident Message: {openIncidentConfig.InvalidProgramOfIncidentMsg}{Environment.NewLine}" +
                   $"       Invalid Program of Incident Error Code: {openIncidentConfig.InvalidProgramOfIncidentErrCode}{Environment.NewLine}";
        }
    }
}

/*

Originally in OpenIncidentConfig()

/// <summary>The message returned when the user description cannot be resolved.</summary>
/// <remarks>
/// The message displayed to the user when the current username is missing from the translation table.
/// </remarks>
public string UnknownUserDescriptionMsg { get; set; }

/// <summary>The error code returned when the user description cannot be resolved.</summary>
/// <remarks>
/// The numeric error code associated with an unknown user description result.
/// </remarks>
public int UnknownUserDescriptionErrCode { get; set; }
 */