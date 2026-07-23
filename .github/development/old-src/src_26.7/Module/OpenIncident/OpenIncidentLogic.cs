// 251112_code
// 260520_documentation

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using TingenWebService.Core.Avatar;
using TingenWebService.Core.Logger;
using TingenWebService.Core.TingenWsvcSession;

namespace TingenWebService.Module.OpenIncident
{
    /// <summary>Contains business logic for OpenIncident module authorization and validation checks.</summary>
    /// <remarks>
    /// Provides helper methods used by event handlers to determine whether the current user may open or
    /// submit an incident and to build human-readable running log bodies used for diagnostics.
    /// </remarks>
    internal class OpenIncidentLogic
    {
        /// <include file='AppData/XmlDocumentation/TngnWsvc.xml' path='TngnWsvc/Class[@name="CommonDefinition"]/ExecutingAssembly/*'/>
        public static string ExeAsm { get; set; } = Assembly.GetExecutingAssembly().GetName().Name;

        /// <summary>Verifies access for the current session and returns an appropriate option object response.</summary>
        /// <remarks>
        /// Uses translation table and role membership checks to determine whether the current user may open
        /// the incident as-is, view it only, or is entirely unauthorized.
        /// </remarks>
        /// <param name="tngnWsvcSession">The active Tingen Web Service session.</param>
        /// <param name="openIncidentConfig">The OpenIncident module configuration.</param>
        /// <example>
        /// <code>
        /// OpenIncidentLogic.VerifyAccess(tngnWsvcSession, openIncidentConfig);
        /// </code>
        /// </example>
        internal static void VerifyAccess(TngnWsvcSession tngnWsvcSession, OpenIncidentConfig openIncidentConfig)
        {
            LogEvent.Debug();
            LogEvent.Trace(1, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

            bool isOriginalAuthor = IsOriginalAuthor(tngnWsvcSession, openIncidentConfig);
            bool isMemberOfAuthorizedUserRole = IsMemberOfAuthorizedUserRole(tngnWsvcSession, openIncidentConfig);

            var testPath         = Path.Combine(tngnWsvcSession.Framework.TngnWsvcDataFolder.Config, "test2.txt");

            File.WriteAllText(testPath, "Test2 content");

            if (isOriginalAuthor || (!isMemberOfAuthorizedUserRole && isOriginalAuthor))
            {
                LogEvent.Trace(4, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                AvatarOptionObject.ToReturn(tngnWsvcSession, 0);
            }
            else if (isMemberOfAuthorizedUserRole && !isOriginalAuthor)
            {
                LogEvent.Trace(4, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                AvatarOptionObject.ToReturn(tngnWsvcSession, openIncidentConfig.NotOriginalAuthorOpenErrCode, openIncidentConfig.NotOriginalAuthorOpenMsg);
            }
            else if (!isMemberOfAuthorizedUserRole && !isOriginalAuthor)
            {
                LogEvent.Trace(4, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                AvatarOptionObject.ToReturn(tngnWsvcSession, openIncidentConfig.NotMemberOfAuthorizedUserRoleErrCode, openIncidentConfig.NotMemberOfAuthorizedUserRoleMsg);
            }
        }

        /// <summary>Determines whether the current session user is the original author of the incident.</summary>
        /// <remarks>
        /// First attempts to match the original author via the translation table, then falls back to an Avatar
        /// query if the translation match fails.
        /// </remarks>
        /// <param name="tngnWsvcSession">The active Tingen Web Service session.</param>
        /// <param name="openIncidentConfig">The OpenIncident module configuration.</param>
        /// <returns><c>true</c> if the current user is the original author; otherwise <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// bool isAuthor = OpenIncidentLogic.IsOriginalAuthor(tngnWsvcSession, openIncidentConfig);
        /// </code>
        /// </example>
        internal static bool IsOriginalAuthor(TngnWsvcSession tngnWsvcSession, OpenIncidentConfig openIncidentConfig)
        {
            LogEvent.Debug();
            LogEvent.Trace(1, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

            var originalAuthorFullName = tngnWsvcSession.OptObj.Original.GetFieldValue(openIncidentConfig.PersonCompletingIncidentFormFieldId);

            if (MatchFoundInTranslationTable(tngnWsvcSession, openIncidentConfig, originalAuthorFullName))
            {
                LogEvent.Trace(2, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                return true;
            }
            else
            {
                if (MatchFoundInAvatarQuery(tngnWsvcSession, openIncidentConfig, originalAuthorFullName))
                {
                    LogEvent.Trace(2, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                    return true;
                }
                else
                {
                    LogEvent.Trace(2, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                    return false;
                }
            }
        }

        /// <summary>Determines whether the current user is a member of an authorized user role.</summary>
        /// <remarks>
        /// Compares the configured authorized roles with the current user's roles returned from the query.
        /// Adds a running log entry describing the result.
        /// </remarks>
        /// <param name="tngnWsvcSession">The active Tingen Web Service session.</param>
        /// <param name="openIncidentConfig">The OpenIncident module configuration.</param>
        /// <returns><c>true</c> if the user is a member of an authorized role; otherwise <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// bool isMember = OpenIncidentLogic.IsMemberOfAuthorizedUserRole(tngnWsvcSession, openIncidentConfig);
        /// </code>
        /// </example>
        internal static bool IsMemberOfAuthorizedUserRole(TngnWsvcSession tngnWsvcSession, OpenIncidentConfig openIncidentConfig)
        {
            LogEvent.Debug();
            LogEvent.Trace(1, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

            var currentUserRoles = OpenIncidentQuery.GetCurrentUserRoles(tngnWsvcSession);
            var authorizedUserRoles = GetAuthorizedUserRolesAsString(openIncidentConfig);

            if (openIncidentConfig.AuthorizedUserRoles.Any(userRole => currentUserRoles.Contains(userRole)))
            {
                LogEvent.Trace(2, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                var runningTitle = "Current user is a member of an authorized user role";
                var runningBody  = BodyUserRoles(authorizedUserRoles, currentUserRoles);

                SessionLog.AddToRunningLog(tngnWsvcSession, LogComponents.GetCallerInfo(), runningTitle, runningBody);

                return true;
            }
            else
            {
                LogEvent.Trace(2, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                var runningTitle = "Current user is not a member of an authorized user role";
                var runningBody  = BodyUserRoles(authorizedUserRoles, currentUserRoles);

                SessionLog.AddToRunningLog(tngnWsvcSession, LogComponents.GetCallerInfo(), runningTitle, runningBody);

                return false;
            }
        }

        /// <summary>Returns the configured authorized user roles as a comma-separated string.</summary>
        /// <remarks>
        /// Useful for logging; returns an empty string when no roles are configured.
        /// </remarks>
        /// <param name="openIncidentConfig">The OpenIncident module configuration.</param>
        /// <returns>A comma-separated list of authorized roles, or an empty string.</returns>
        /// <example>
        /// <code>
        /// string roles = OpenIncidentLogic.GetAuthorizedUserRolesAsString(openIncidentConfig);
        /// </code>
        /// </example>
        internal static string GetAuthorizedUserRolesAsString(OpenIncidentConfig openIncidentConfig)
        {
            return openIncidentConfig?.AuthorizedUserRoles == null || !openIncidentConfig.AuthorizedUserRoles.Any()
                ? string.Empty
                : string.Join(", ", openIncidentConfig.AuthorizedUserRoles);
        }

        /// <summary>Checks whether the original author matches the current user using the translation table.</summary>
        /// <remarks>
        /// Logs diagnostic running entries describing whether the translation table could be used to match the user.
        /// </remarks>
        /// <param name="tngnWsvcSession">The active Tingen Web Service session.</param>
        /// <param name="openIncidentConfig">The OpenIncident module configuration.</param>
        /// <param name="originalAuthorFullName">The original author full name read from the option object.</param>
        /// <returns><c>true</c> when a translation-table match is found; otherwise <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// bool found = OpenIncidentLogic.MatchFoundInTranslationTable(tngnWsvcSession, openIncidentConfig, originalAuthorFullName);
        /// </code>
        /// </example>
        internal static bool MatchFoundInTranslationTable(TngnWsvcSession tngnWsvcSession, OpenIncidentConfig openIncidentConfig, string originalAuthorFullName)
        {
            LogEvent.Trace(1, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

            var translatedUserDescription = Core.Translation.UserId.GetUserDescription(tngnWsvcSession.OptObj.Worker.OptionUserId, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, tngnWsvcSession.Framework.TngnWsvcDataFolder.TranslationTable);

            if (translatedUserDescription == null || string.IsNullOrWhiteSpace(translatedUserDescription) || translatedUserDescription == "WSVC4274")
            {
                LogEvent.Trace(2, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                var runningTitle = "Cannot find current user in the translation table";
                var runningBody  = BodyAuthorTranslation(tngnWsvcSession.Runtime.AvatarUserId, originalAuthorFullName, translatedUserDescription);

                SessionLog.AddToRunningLog(tngnWsvcSession, LogComponents.GetCallerInfo(), runningTitle, runningBody);

                return false;
            }
            else if (originalAuthorFullName == translatedUserDescription)
            {
                LogEvent.Trace(2, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                var runningTitle = "Original author/current user match in the translation table";
                var runningBody  = BodyAuthorTranslation(tngnWsvcSession.Runtime.AvatarUserId, originalAuthorFullName, translatedUserDescription);

                SessionLog.AddToRunningLog(tngnWsvcSession, LogComponents.GetCallerInfo(), runningTitle, runningBody);

                return true;
            }
            else if (originalAuthorFullName != translatedUserDescription)
            {
                LogEvent.Trace(2, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                var runningTitle = "Original author/current user do not match in the translation table";
                var runningBody  = BodyAuthorTranslation(tngnWsvcSession.Runtime.AvatarUserId, originalAuthorFullName, translatedUserDescription);

                SessionLog.AddToRunningLog(tngnWsvcSession, LogComponents.GetCallerInfo(), runningTitle, runningBody);

                return false;
            }
            else
            {
                LogEvent.Trace(2, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                var runningTitle = "Unknown error when using the translation table to match original author/current user";
                var runningBody  = BodyAuthorTranslation(tngnWsvcSession.Runtime.AvatarUserId, originalAuthorFullName, translatedUserDescription);

                SessionLog.AddToRunningLog(tngnWsvcSession, LogComponents.GetCallerInfo(), runningTitle, runningBody);

                return false;
            }
        }

        /// <summary>Checks whether the original author matches the current user via an Avatar user query.</summary>
        /// <remarks>
        /// Executes a query to obtain user descriptions and inspects the response for a matching original author
        /// value. Adds diagnostic running log entries describing the result.
        /// </remarks>
        /// <param name="tngnWsvcSession">The active Tingen Web Service session.</param>
        /// <param name="openIncidentConfig">The OpenIncident module configuration.</param>
        /// <param name="originalAuthorFullName">The original author full name read from the option object.</param>
        /// <returns><c>true</c> when the query contains a matching entry; otherwise <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// bool found = OpenIncidentLogic.MatchFoundInAvatarQuery(tngnWsvcSession, openIncidentConfig, originalAuthorFullName);
        /// </code>
        /// </example>
        internal static bool MatchFoundInAvatarQuery(TngnWsvcSession tngnWsvcSession, OpenIncidentConfig openIncidentConfig, string originalAuthorFullName)
        {
            LogEvent.Trace(1, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

            var queryiedUserDescription = OpenIncidentQuery.UserDescription(tngnWsvcSession);

            if (queryiedUserDescription.Contains($"val=\"{originalAuthorFullName}\""))
            {
                LogEvent.Trace(2, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                var runningTitle = $"Original author/current user match via query{Environment.NewLine}" +
                                   $"{Environment.NewLine}" +
                                   $"{LogComponents.GetCallerInfo()}";
                var runningBody  = BodyAuthorQuery(tngnWsvcSession.Runtime.AvatarUserId, originalAuthorFullName, queryiedUserDescription);

                SessionLog.AddToRunningLog(tngnWsvcSession, LogComponents.GetCallerInfo(), runningTitle, runningBody);

                return true;
            }
            else if (!queryiedUserDescription.Contains($"val=\"{originalAuthorFullName}\""))
            {
                LogEvent.Trace(2, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                LogEvent.Debug($"{queryiedUserDescription} - {originalAuthorFullName}");

                var runningTitle = $"Original author/current user do not match via query{Environment.NewLine}" +
                                   $"{Environment.NewLine}" +
                                   $"{LogComponents.GetCallerInfo()}";
                var runningBody  = BodyAuthorQuery(tngnWsvcSession.Runtime.AvatarUserId, originalAuthorFullName, queryiedUserDescription);

                SessionLog.AddToRunningLog(tngnWsvcSession, LogComponents.GetCallerInfo(), runningTitle, runningBody);

                return false;
            }
            else
            {
                LogEvent.Trace(2, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                var runningTitle = "Unknown error when attempting to match original author/current user via query";
                var runningBody  = BodyAuthorQuery(tngnWsvcSession.Runtime.AvatarUserId, originalAuthorFullName, queryiedUserDescription);

                SessionLog.AddToRunningLog(tngnWsvcSession, LogComponents.GetCallerInfo(), runningTitle, runningBody);

                return false;
            }
        }

        /// <summary>Validates that the submitting user is the original author and returns an option object result.</summary>
        /// <remarks>
        /// Compares the original author stored on the option object to the session user description obtained from
        /// the translation table and returns an appropriate Avatar option response.
        /// </remarks>
        /// <param name="tngnWsvcSession">The active Tingen Web Service session.</param>
        /// <param name="openIncidentConfig">The OpenIncident module configuration.</param>
        /// <example>
        /// <code>
        /// OpenIncidentLogic.IsOriginalAuthorSubmitting(tngnWsvcSession, openIncidentConfig);
        /// </code>
        /// </example>
        internal static void IsOriginalAuthorSubmitting(TngnWsvcSession tngnWsvcSession, OpenIncidentConfig openIncidentConfig)
        {
            LogEvent.Trace(1, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

            var originalAuthorFullName = tngnWsvcSession.OptObj.Original.GetFieldValue(openIncidentConfig.PersonCompletingIncidentFormFieldId);
            var fileCheck = tngnWsvcSession.Framework.TngnWsvcDataFolder.AvatarGeneratedData + $@"\USERID_User Description_{tngnWsvcSession.Runtime.AvatarSystem}.txt";
            var sessionUserFullName    = Core.Translation.UserId.GetUserDescription(tngnWsvcSession.OptObj.Worker.OptionUserId, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, tngnWsvcSession.Framework.TngnWsvcDataFolder.TranslationTable);

            if (originalAuthorFullName != sessionUserFullName)
            {
                LogEvent.Trace(2, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                AvatarOptionObject.ToReturn(tngnWsvcSession, openIncidentConfig.NotOriginalAuthorSubmitErrCode, openIncidentConfig.NotOriginalAuthorSubmitMsg);
            }
            else
            {
                LogEvent.Trace(2, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                AvatarOptionObject.ToReturn(tngnWsvcSession, 0);
            }
        }

        /// <summary>Validates that the Program of Incident field contains a value.</summary>
        /// <remarks>
        /// Returns <c>false</c> and logs a running entry when the Program of Incident value is empty or null;
        /// otherwise returns <c>true</c>.
        /// </remarks>
        /// <param name="tngnWsvcSession">The active Tingen Web Service session.</param>
        /// <param name="openIncidentConfig">The OpenIncident module configuration.</param>
        /// <returns><c>true</c> when the Program of Incident is present; otherwise <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// bool valid = OpenIncidentLogic.IsProgramOfIncidentValid(tngnWsvcSession, openIncidentConfig);
        /// </code>
        /// </example>
        internal static bool IsProgramOfIncidentValid(TngnWsvcSession tngnWsvcSession, OpenIncidentConfig openIncidentConfig)
        {
            LogEvent.Trace(1, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

            var programOfIncident = tngnWsvcSession.OptObj.Original.GetFieldValue(openIncidentConfig.ProgramOfIncidentFieldId);

            if (string.IsNullOrEmpty(programOfIncident))
            {
                LogEvent.Trace(2, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                var runningTitle = "Invalid Program of Incident";
                var runningBody  = $"Program of Incident = {programOfIncident}{Environment.NewLine}";

                SessionLog.AddToRunningLog(tngnWsvcSession, LogComponents.GetCallerInfo(), runningTitle, runningBody);

                return false;
            }
            else
            {
                LogEvent.Trace(2, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                return true;
            }
        }

        /// <summary>Builds a running log body describing authorized and current user roles.</summary>
        /// <remarks>Returns a formatted string suitable for inclusion in running log entries.</remarks>
        /// <param name="authorizedUserRoles">Comma-separated authorized user roles.</param>
        /// <param name="currentUserRoles">The raw query response of current user roles.</param>
        /// <returns>A formatted running log body string.</returns>
        /// <example>
        /// <code>
        /// string body = OpenIncidentLogic.BodyUserRoles(authorizedUserRoles, currentUserRoles);
        /// </code>
        /// </example>
        internal static string BodyUserRoles(string authorizedUserRoles, string currentUserRoles)
        {
            return $"{LogComponents.GetCallerInfo()}{Environment.NewLine}" +
                   $"Authorized user roles: {authorizedUserRoles}{Environment.NewLine}" +
                   $"Query User Role response: {currentUserRoles}{Environment.NewLine}";
        }

        /// <summary>Builds a running log body describing translation table author match diagnostics.</summary>
        /// <remarks>Returns a formatted string that includes the avatar user id, original author, and translation result.</remarks>
        /// <param name="avatarUserId">The Avatar user identifier for the current session.</param>
        /// <param name="originalAuthor">The original author full name read from the option object.</param>
        /// <param name="translatedUserDescription">The translation table user description value.</param>
        /// <returns>A formatted running log body string.</returns>
        /// <example>
        /// <code>
        /// string body = OpenIncidentLogic.BodyAuthorTranslation(avatarUserId, originalAuthor, translatedUserDescription);
        /// </code>
        /// </example>
        internal static string BodyAuthorTranslation(string avatarUserId, string originalAuthor, string translatedUserDescription)
        {
            return $"{LogComponents.GetCallerInfo()}{Environment.NewLine}" +
                   $"User ID: {avatarUserId}{Environment.NewLine}" +
                   $"Original author: {originalAuthor}{Environment.NewLine}" +
                   $"Translation table User Description value: {translatedUserDescription}";
        }

        /// <summary>Builds a running log body describing Avatar query author match diagnostics.</summary>
        /// <remarks>Returns a formatted string that includes the avatar user id, original author, and query response.</remarks>
        /// <param name="avatarUserId">The Avatar user identifier for the current session.</param>
        /// <param name="originalAuthor">The original author full name read from the option object.</param>
        /// <param name="queryiedUserDescription">The raw Avatar query response used to match authorship.</param>
        /// <returns>A formatted running log body string.</returns>
        /// <example>
        /// <code>
        /// string body = OpenIncidentLogic.BodyAuthorQuery(avatarUserId, originalAuthor, queryiedUserDescription);
        /// </code>
        /// </example>
        internal static string BodyAuthorQuery(string avatarUserId, string originalAuthor, string queryiedUserDescription)
        {
            return $"{LogComponents.GetCallerInfo()}{Environment.NewLine}" +
                   $"User ID: {avatarUserId}{Environment.NewLine}" +
                   $"Original author: {originalAuthor}{Environment.NewLine}" +
                   $"Query User Role response: {queryiedUserDescription}{Environment.NewLine}";
        }
    }
}