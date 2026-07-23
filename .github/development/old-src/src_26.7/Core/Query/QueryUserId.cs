// 251112_code
// 260515_documentation.

using System;
using System.Reflection;
using TingenWebService.Core.Logger;

namespace TingenWebService.Core.Query
{
    /// <summary>Resolves Avatar user identifiers by querying the appropriate Netsmart system database.</summary>
    /// <remarks>
    /// <para>
    /// <c>QueryUserId</c> translates an Avatar user id (the value supplied by the Avatar form at runtime) into a
    /// Netsmart <c>Staff_Member_ID</c> by issuing a <c>SELECT</c> against the <c>RADplus_users</c> table. The target
    /// system (LIVE, UAT, or SBOX) is chosen from the active session's runtime configuration so the same code path can
    /// be used in any deployment environment.
    /// </para>
    /// <para>
    /// This class also exposes a hook for generating Avatar-to-Tingen translation files, which is used by daily
    /// maintenance to keep cached lookups in sync with the source database.
    /// </para>
    /// </remarks>
    internal static class QueryUserId
    {
        /// <include file='AppData/XmlDocumentation/TngnWsvc.xml' path='TngnWsvc/Class[@name="CommonDefinition"]/ExecutingAssembly/*'/>
        public static string ExeAsm { get; set; } = Assembly.GetExecutingAssembly().GetName().Name;

        /// <summary>Returns the Netsmart <c>Staff_Member_ID</c> that corresponds to the session's Avatar user id.</summary>
        /// <param name="tngnWsvcSession">The active Tingen Web Service session object that exposes runtime credentials, the active Avatar system, and logging configuration.</param>
        /// <returns>The <c>Staff_Member_ID</c> returned by the target Avatar system, or the literal error code <c>"[WSVC4274]"</c> when the active system is not recognized.</returns>
        /// <remarks>
        /// <para>
        /// The Avatar user id is upper-cased before being embedded in the SQL statement so that lookups match the
        /// casing convention used by Netsmart. The query is executed against one of three systems based
        /// on <c>tngnWsvcSession.Runtime.AvatarSystem</c>:
        /// </para>
        /// <list type="bullet">
        ///   <item><description><c>LIVE</c> &#8212; uses <c>SuperLive.Query</c>.</description></item>
        ///   <item><description><c>UAT</c> &#8212; uses <c>NtstWsvcUatNxQuery.Query</c>.</description></item>
        ///   <item><description><c>SBOX</c> &#8212; uses <c>NtstWsvcSboxQuery.Query</c>.</description></item>
        /// </list>
        /// <para>
        /// Diagnostic information (the query, credentials, and result) is written to the debug log so that
        /// authentication or syntax issues can be reproduced from the captured session data.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// string staffMemberId = QueryUserId.ToStaffMemberId(tngnWsvcSession);
        /// </code>
        /// </example>
        internal static string ToStaffMemberId(dynamic tngnWsvcSession)
        {
            LogEvent.Trace(1, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

            var querySyntax = $"SELECT Staff_Member_ID FROM RADplus_users WHERE USERID = '{tngnWsvcSession.Runtime.AvatarUserId.ToUpper()}'";
            var queryUser   = tngnWsvcSession.Runtime.NtstWsvcUserName;
            var queryPass   = tngnWsvcSession.Runtime.NtstWsvcUserPass;
            var testr       = new SuperLive.Query().SubmitQuery("LIVE", queryUser, queryPass, querySyntax);

            LogEvent.Debug($"SLIVE!OpenIncidentQuery.UserRoles{Environment.NewLine}" +
                           $"queryUser: {queryUser}" +
                           $"queryPass: {queryPass}" +
                           $"querySyntax: {querySyntax}{Environment.NewLine}" +
                           $"Result: {testr}");

            if (tngnWsvcSession.Runtime.AvatarSystem.Equals("LIVE", StringComparison.OrdinalIgnoreCase))
            {
                LogEvent.Trace(2, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                return new SuperLive.Query().SubmitQuery("LIVE", queryUser, queryPass, querySyntax);
            }
            else if (tngnWsvcSession.Runtime.AvatarSystem.Equals("UAT", StringComparison.OrdinalIgnoreCase))
            {
                LogEvent.Trace(2, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                return new NtstWsvcUatNxQuery.Query().SubmitQuery("UAT", queryUser, queryPass, querySyntax);
            }
            else if (tngnWsvcSession.Runtime.AvatarSystem.Equals("SBOX", StringComparison.OrdinalIgnoreCase))
            {
                LogEvent.Trace(2, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);
                LogEvent.Debug($"{queryUser} - {queryPass} - {querySyntax}");

                return new NtstWsvcSboxQuery.Query().SubmitQuery("SBOX", queryUser, queryPass, querySyntax);
            }
            else
            {
                LogEvent.Trace(2, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                return "[WSVC4274]";
            }
        }

        /// <summary>Generates an Avatar-to-Tingen user id translation file from a source listing.</summary>
        /// <param name="generatedUserIdPath">The absolute path to the source file that lists generated Avatar user ids.</param>
        /// <param name="translationPath">The absolute path where the resulting translation file should be written.</param>
        /// <param name="traceLogLimit">The maximum number of trace log files retained for the active session.</param>
        /// <param name="sessionFolder">The absolute path to the current Tingen Web Service session folder.</param>
        /// <remarks>
        /// <para>
        /// In production this method parses the file at <paramref name="generatedUserIdPath"/> and writes a translation
        /// table to <paramref name="translationPath"/> so other Tingen components can resolve Avatar user ids without
        /// re-querying the source database. The current implementation is a stub that performs no work; it exists so
        /// callers (for example, <c>DailyMaintenance.RefreshTranslationTables</c>) can be wired up while the production
        /// logic is being ported.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// QueryUserId.CreateTranslationFile(
        ///     generatedUserIdPath: @"C:\Tingen_Data\Cache\GeneratedUserIds.txt",
        ///     translationPath:     @"C:\Tingen_Data\Translation\UserId.translation",
        ///     traceLogLimit:       traceLogLimit,
        ///     sessionFolder:       sessionFolder);
        /// </code>
        /// </example>
        internal static void CreateTranslationFile(string generatedUserIdPath, string translationPath, int traceLogLimit, string sessionFolder)
        {
            // Stub implementation for translation file creation
            // In production, this would parse the generatedUserIdPath and write to translationPath
        }
    }
}