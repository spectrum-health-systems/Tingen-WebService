// 260805_code
// 260805_documentation

using System;
using TingenWebService.Module.OpenIncident;

namespace TingenWebService.Core.Trove
{
    /// <summary>Preset messages and strings.</summary>
    /// <remarks><include file='AppData/XmlDoc/Topics.xml' path='Topics/Topic[@name="Trove"]/AboutRedprints/*'/></remarks>
    internal class Redprint
    {
        /// <summary>Build the framework details message.</summary>
        /// <param name="framework">The framework configuration.</param>
        /// <returns>The framework details message.</returns>
        internal static string FrameworkDetails(Framework.FrameworkConfig framework)
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
                   $"    Translation Table Root: {framework.TranslationRoot}{Environment.NewLine}";
        }

        /// <summary>Build the message indicating that blueprints have been exported.</summary>
        /// <returns>The blueprints exported message.</returns>
        internal static string BlueprintsExported() => $"[Blueprints exported]{Environment.NewLine}"; // TODO - Do the same things we did with FrameworkDetails

        /// <summary>Build the configuration details message.</summary>
        /// <param name="twsConfig">The Tingen Web Service configuration.</param>
        /// <returns>The configuration details message.</returns>
        internal static string ConfigurationDetails(TwsConfig twsConfig)
        {
            return $"         Configuration details{Environment.NewLine}" +
                   $"------------------------------{Environment.NewLine}" +
                   $"                          Mode: {twsConfig.Mode}{Environment.NewLine}" +
                   $"             Trace Level Limit: {twsConfig.TraceLimit}{Environment.NewLine}" +
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
        internal static string RuntimeDetails(RuntimeSetting rtConfig)
        {
            return $"Runtime details{Environment.NewLine}" +
                   $"---------------{Environment.NewLine}" +
                   $"Release Build: {rtConfig.ReleaseBuild}{Environment.NewLine}" +
                   $"Avatar System: {rtConfig.AvatarSystem}{Environment.NewLine}" +
                   $"    Data Root: {rtConfig.DataRoot}{Environment.NewLine}";
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

        internal static string DailyLogStart(string verifyStart, string verifyStartMs)
        {
            return $"[Start] {verifyStart}({verifyStartMs}){Environment.NewLine}";
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