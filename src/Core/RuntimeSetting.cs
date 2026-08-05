// 260805_code
// 260805_documentation

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
    internal class RuntimeSetting
    {
        /// <summary>The current date (yyMMdd).</summary>
        public string RuntimeStartDate { get; set; }

        /// <summary>The current time (HHmmss).</summary>
        public string RuntimeStartTime { get; set; }

        /// <summary>The current milliseconds (fffffff).</summary>
        public string RuntimeStartMs { get; set; }

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

        /// <summary>Load the runtime configuration.</summary>
        /// <param name="twsRelease">The release version of the Tingen Web Service.</param>
        /// <returns>The loaded <see cref="RuntimeSetting"/> instance.</returns>
        internal static RuntimeSetting Load(string twsRelease)
        {
            //LogEvent.Primeval("PRELOG-TRACE-RuntimeSetting-Load");

            try
            {
                return Build(twsRelease);
            }
            catch (Exception ex)
            {
                LogEvent.Primeval("ERR1110-FailedToLoadRuntimeSettings", ErrorMessage.ERR1110(ex.Message)[1]);

                throw;
            }
        }

        /// <summary>Build the runtime configuration.</summary>
        /// <param name="twsRelease">The release version of the Tingen Web Service.</param>
        /// <returns>A <see cref="RuntimeSetting"/> instance with the current settings.</returns>
        internal static RuntimeSetting Build(string twsRelease)
        {
            //LogEvent.Primeval("PRELOG-TRACE-RuntimeSetting-Build");

            return new RuntimeSetting()
            {
                RuntimeStartDate = DateTime.Now.ToString("yyMMdd"),
                RuntimeStartTime = DateTime.Now.ToString("HHmmss"),
                RuntimeStartMs   = DateTime.Now.ToString("fffffff"),
                ReleaseBuild     = $"{twsRelease} (b{Settings.Default.TwsBuild})",
                AvatarSystem     = Settings.Default.AvatarSystem,
                DataRoot         = Settings.Default.HostDataRoot,
            };
        }
    }
}