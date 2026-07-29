// 260729_code
// 260729_documentation

using System;
using TingenWebService.Core.Logger;
using TingenWebService.Core.Trove;
using TingenWebService.Properties;


namespace TingenWebService.Core
{
    /// <summary>Runtime configuration/settings logic.</summary>
    /// <remarks>
    /// The RuntimeConfiguration is the first configuration component that is loaded when the Tingen Web Service starts.
    /// It contains information that the web service needs to know before anything else can be loaded.
    /// </remarks>
    internal class RuntimeConfig
    {
        /// <summary>The current date (yyMMdd).</summary>
        public string SessionStartDate { get; set; }

        /// <summary>The current time (HHmmss).</summary>
        public string SessionStartTime { get; set; }

        /// <summary>The current milliseconds (fffffff).</summary>
        public string SessionStartMilliseconds { get; set; }

        /// <summary>Release and build information.</summary>
        /// <remarks>
        /// These two pieces of information are combined to form a single string that is used to identify the version of
        /// the Tingen Web Service that is running.
        /// </remarks>
        public string ReleaseBuild { get; set; }

        /// <summary>The Avatar system that the Tingen Web Service will interface with.</summary>
        public string AvatarSystem { get; set; }

        /// <summary>The root directory for Tingen Web Service data.</summary>
        public string DataRoot { get; set; }

        internal static RuntimeConfig Load(string twsRelease)
        {
            try
            {
                return Build(twsRelease);
            }
            catch (Exception ex)
            {
                LogEvent.Primeval($"{DateTime.Now:yyMMdd-HHmmss-fffffff}-ERR1110-TwsConfigLoadFailed", SysMsg.ERR1110(ex.Message)[1]);

                throw;
            }
        }

        /// <summary>Build the runtime configuration.</summary>
        /// <returns>A <see cref="RuntimeConfig"/> instance with the current settings.</returns>
        internal static RuntimeConfig Build(string twsRelease)
        {
            /* For debugging prior to logging functionality being initialized.
             * Disable in production.
             */
            //Logger.LogEvent.Primeval($"{DateTime.Now:yyMMdd-HHmmss-fffffff}-DEBUG-RuntimeConfig.Build");

            return new RuntimeConfig()
            {
                SessionStartDate         = DateTime.Now.ToString("yyMMdd"),
                SessionStartTime         = DateTime.Now.ToString("HHmmss"),
                SessionStartMilliseconds = DateTime.Now.ToString("fffffff"),
                ReleaseBuild             = $"{twsRelease} (b{Settings.Default.TwsBuild})",
                AvatarSystem             = Settings.Default.AvatarSystem,
                DataRoot                 = Settings.Default.HostDataRoot,
            };
        }
    }
}