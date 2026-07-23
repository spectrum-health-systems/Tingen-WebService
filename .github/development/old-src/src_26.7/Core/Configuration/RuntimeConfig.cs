// 251112_code
// 260603_documentation.

using System.Collections.Generic;
using TingenWebService.Properties;

namespace TingenWebService.Core.Configuration
{
    /// <summary>Runtime configuration logic.</summary>
    internal class RuntimeConfig
    {
        /* TODO
         * Verify this is correct, now that we aren't using Outpost31.
         */
        /// <summary>Load the runtime configuration values from the Web.config settings.</summary>
        /// <remarks>
        /// When a new setting is added to the Web.config file, it must also be added to this method.
        /// </remarks>
        /// <param name="webConfig">The strongly-typed <see cref="Settings"/> instance representing Web.config values.</param>
        /// <param name="tngnWsvcVersion">The current version string of the Tingen Web Service.</param>
        /// <returns>A dictionary containing the runtime configuration settings keyed by setting name.</returns>
        /// <example>
        /// <code>
        /// var runtimeConfig = RuntimeConfig.Load(Settings.Default, "1.2.3");
        /// var avatarSystem  = runtimeConfig["AvatarSystem"];
        /// var mode          = runtimeConfig["Mode"];
        ///
        /// Console.WriteLine($"Avatar system: {avatarSystem}, Mode: {mode}");
        /// </code>
        /// </example>
        internal static Dictionary<string, string> Load(Settings webConfig, string tngnWsvcVersion) =>
            new Dictionary<string, string>
            {
                { "Version",             tngnWsvcVersion },
                { "BuildNumber",         webConfig.BuildNumber },
                { "AvatarSystem",        webConfig.AvatarSystem },
                { "Mode",                webConfig.Mode.ToLower() },
                { "ServerWwwPath",       webConfig.ServerWwwPath},
                { "ServerDataPath",      webConfig.ServerDataPath},
                { "TraceLogLimit",       webConfig.TraceLogLimit},
                { "SessionLogLimit",     webConfig.SessionLogLimit },
                { "NtstWsvcUserName",    webConfig.NtstWsvcUserName },
                { "NtstWsvcUserPass",    webConfig.NtstWsvcUserPass }
            };
    }
}