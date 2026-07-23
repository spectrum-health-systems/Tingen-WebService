// 251112_code
// 260520_documentation

using System;
using System.Reflection;
using System.Xml.Linq;
using SuperLive;
using NtstWsvcUatNxQuery;
using NtstWsvcSboxQuery;
using NtstWsvcLiveQuery;
using NtstWsvcUatQuery;
using TingenWebService.Core.Logger;
using TingenWebService.Core.TingenWsvcSession;

namespace TingenWebService.Module.OpenIncident
{
    /// <summary>Provides query helpers used by the OpenIncident module to obtain user role and description data.</summary>
    /// <remarks>
    /// This class encapsulates logic for submitting system-specific queries and formatting XML responses used
    /// by the OpenIncident authorization and validation workflows.
    /// </remarks>
    internal class OpenIncidentQuery
    {
        /// <include file='AppData/XmlDocumentation/TngnWsvc.xml' path='TngnWsvc/Class[@name="CommonDefinition"]/ExecutingAssembly/*'/>
        public static string ExeAsm { get; set; } = Assembly.GetExecutingAssembly().GetName().Name;

        /// <summary>Retrieves the current user's roles from the backend user store.</summary>
        /// <remarks>
        /// Builds and submits a role lookup query against the appropriate environment (LIVE, UAT, SBOX)
        /// based on the runtime Avatar system and returns the raw XML query result.
        /// </remarks>
        /// <param name="tngnWsvcSession">The active Tingen Web Service session containing runtime and credential data.</param>
        /// <returns>The raw XML response containing user role information or an error token when unsupported.</returns>
        internal static string GetCurrentUserRoles(TngnWsvcSession tngnWsvcSession)
        {
            LogEvent.Trace(1, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

            var querySyntax = $"SELECT USERROLE FROM RADplus_users WHERE '{tngnWsvcSession.Runtime.AvatarUserId.ToUpper()}' = USERID";
            var queryUser   = tngnWsvcSession.Runtime.NtstWsvcUserName;
            var queryPass   = tngnWsvcSession.Runtime.NtstWsvcUserPass;
            var testr = new SuperLive.Query().SubmitQuery("LIVE", queryUser, queryPass, querySyntax);

            LogEvent.Debug($"SuperLIVE! OpenIncidentQuery.UserRoles{Environment.NewLine}" +
                           $"queryUser: {queryUser}" +
                           $"queryPass: {queryPass}" +
                           $"querySyntax: {querySyntax}{Environment.NewLine}" +
                           $"Result: {FormatXmlQuery(testr)}");

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

                return new NtstWsvcSboxQuery.Query().SubmitQuery("SBOX", queryUser, queryPass, querySyntax);
            }
            else
            {
                LogEvent.Trace(2, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                return "[WSVC7582]";
            }
        }

        /// <summary>Formats a raw XML query response into a prettified XML string.</summary>
        /// <param name="xmlResult">The raw XML string returned by a query.</param>
        /// <returns>A formatted, indented XML string.</returns>
        internal static string FormatXmlQuery(string xmlResult)
        {
            return XDocument.Parse(xmlResult).ToString();
        }

        /// <summary>Retrieves the current user's description value from the backend user store.</summary>
        /// <remarks>
        /// Builds and submits a user description lookup against the appropriate environment (LIVE, UAT, SBOX)
        /// based on the runtime Avatar system and returns the raw XML or token result.
        /// </remarks>
        /// <param name="tngnWsvcSession">The active Tingen Web Service session containing runtime and credential data.</param>
        /// <returns>The raw XML response containing the user description or an error token when unsupported.</returns>
        internal static string UserDescription(TngnWsvcSession tngnWsvcSession)
        {
            LogEvent.Trace(1, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

            var querySyntax = $"SELECT user_description FROM RADplus_users WHERE USERID = '{tngnWsvcSession.Runtime.AvatarUserId.ToUpper()}'";
            var queryUser   = tngnWsvcSession.Runtime.NtstWsvcUserName;
            var queryPass   = tngnWsvcSession.Runtime.NtstWsvcUserPass;
            var testr = new NtstWsvcLiveQuery.Query().SubmitQuery("LIVE", queryUser, queryPass, querySyntax);

            LogEvent.Debug($"OpenIncidentQuery.UserRoles{Environment.NewLine}" +
                           $"queryUser: {queryUser}" +
                           $"queryPass: {queryPass}" +
                           $"querySyntax: {querySyntax}{Environment.NewLine}" +
                           $"Result: {testr}");

            if (tngnWsvcSession.Runtime.AvatarSystem.Equals("LIVE", StringComparison.OrdinalIgnoreCase))
            {
                LogEvent.Trace(2, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                return new NtstWsvcLiveQuery.Query().SubmitQuery("LIVE", queryUser, queryPass, querySyntax);
            }
            else if (tngnWsvcSession.Runtime.AvatarSystem.Equals("UAT", StringComparison.OrdinalIgnoreCase))
            {
                LogEvent.Trace(2, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                return new NtstWsvcUatQuery.Query().SubmitQuery("UAT", queryUser, queryPass, querySyntax);
            }
            else if (tngnWsvcSession.Runtime.AvatarSystem.Equals("SBOX", StringComparison.OrdinalIgnoreCase))
            {
                LogEvent.Trace(2, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                return new NtstWsvcSboxQuery.Query().SubmitQuery("SBOX", queryUser, queryPass, querySyntax);
            }
            else
            {
                LogEvent.Trace(2, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                return "[WSVC4274]";
            }
        }
    }
}