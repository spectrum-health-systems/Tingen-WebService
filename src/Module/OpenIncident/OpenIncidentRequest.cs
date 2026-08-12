// 260812_code
// 260812_documentation

using System.Collections.Generic;
using System.IO;
using TingenWebService.Core.Logger;
using TingenWebService.Core.Session;

namespace TingenWebService.Module.OpenIncident
{
    internal class OpenIncidentRequest
    {
        /// <summary>Parses the OpenIncident request and sends it to the appropriate handler.</summary>
        /// <remarks>
        /// This method:
        /// <list type="number">
        /// <item>Loads the OpenIncident module configuration.</item>
        /// <item>Routes the request.</item>
        /// </list>
        /// </remarks>
        /// <param name="tngnWsvcSession">The current Tingen Web Service session being processed.</param>
        /// <example>
        /// <code>
        /// OpenIncidentEvent.Parse(tngnWsvcSession);
        /// </code>
        /// </example>
        internal static void Parse(Sess sess)
        {
            LogEvent.Trace(1, sess.Trc.Lmt, sess.Trc.Fld);

            var configPath = Path.Combine(sess.FrameworkSetting.ConfigRoot, "OpenIncident.config");

            var openIncidentConfig = OpenIncidentSetting.Load(configPath, sess.Trc);

            if (ModuleEnabled(openIncidentConfig.Mode, sess.Trc))
            {
                LogEvent.Trace(4, sess.Trc.Lmt, sess.Trc.Fld);
            }

            if (BypassUser(sess.OptionObject.SentOptionObject.OptionUserId, openIncidentConfig.BypassList, sess.Trc))
            {
                LogEvent.Trace(4, sess.Trc.Lmt, sess.Trc.Fld);

                sess.RunningLog += $"Bypass user {sess.OptionObject.SentOptionObject.OptionUserId} detected. OpenIncident module will not be processed.\n";

                return;
            }

            LogEvent.Trace(1, sess.Trc.Lmt, sess.Trc.Fld);
        }

        private static bool ModuleEnabled(string mode, Tracer trc)
        {
            LogEvent.Trace(1, trc.Lmt, trc.Fld);

            return !string.IsNullOrEmpty(mode) && mode.Equals("enabled", System.StringComparison.OrdinalIgnoreCase);
        }

        private static bool BypassUser(string userId, List<string> bypassList, Tracer trc)
        {
            LogEvent.Trace(1, trc.Lmt, trc.Fld);

            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }

            var bypassListNormalized = GetBypassList(bypassList, trc);

            foreach (var bypassUser in bypassListNormalized)
            {
                if (userId.Equals(bypassUser, System.StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
        private static List<string> GetBypassList(List<string> bypassList, Tracer trc)
        {
            LogEvent.Trace(1, trc.Lmt, trc.Fld);

            if (bypassList == null)
            {
                return new List<string>();
            }

            return bypassList;
        }
    }
}