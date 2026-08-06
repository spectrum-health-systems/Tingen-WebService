// 260806_code
// 260806_documentation

using System;
using TingenWebService.Core.Logger;
using TingenWebService.Core.Trove;
using TingenWebService.Properties;

namespace TingenWebService.Core
{
    /// <summary>Runtime settings logic.</summary>
    /// <remarks>
    /// <include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="RuntimeSetting"]/AboutRuntimeSetting/*'/>
    /// </remarks>
    internal class RuntimeSetting
    {
        /// <summary>The current date.</summary>
        public string CurentDate { get; set; }

        /// <summary>The current time.</summary>
        public string CurrentTime { get; set; }

        /// <summary>The current milliseconds.</summary>
        /// <remarks>This is used to calculate durations.</remarks>
        public string CurrentMs { get; set; }

        /// <summary>Release version and build information.</summary>
        /// <remarks>The release version and build information are combined to form a single string for readability.</remarks>
        public string VersionBuild { get; set; }

        /// <summary>The Avatar system that the Tingen Web Service will interface with.</summary>
        /// <value>e.g., "LIVE", "UAT"</value>
        public string AvatarSystem { get; set; }

        /// <summary>The root directory for Tingen Web Service data.</summary>
        /// <value>e.g., "C:\Tingen_Data"</value>
        public string DataRoot { get; set; }

        /// <summary>Loads the runtime settings.</summary>
        /// <param name="appVersion">The current version of the Tingen Web Service.</param>
        /// <returns>The runtime settings.</returns>
        internal static RuntimeSetting Load(string appVersion)
        {
            //LogEvent.Primeval("PRELOG-TRACE-RuntimeSetting-Load");

            try
            {
                return Build(appVersion);
            }
            catch (Exception ex)
            {
                LogEvent.Primeval("ERR1000-FailedToLoadRuntimeSettings", ErrorMessage.ERR1000(ex.Message));

                throw;
            }
        }

        /// <summary>Builds the runtime settings.</summary>
        /// <param name="appVersion">The release version of the Tingen Web Service.</param>
        /// <returns>A <see cref="RuntimeSetting"/> instance with the current settings.</returns>
        internal static RuntimeSetting Build(string appVersion)
        {
            //LogEvent.Primeval("PRELOG-TRACE-RuntimeSetting-Build");

            return new RuntimeSetting()
            {
                CurentDate   = DateTime.Now.ToString("yyMMdd"), // TODO - yy:MM:dd?
                CurrentTime  = DateTime.Now.ToString("HHmmss"), // TODO - HH:mm:ss?
                CurrentMs    = DateTime.Now.ToString("fffffff"),
                VersionBuild = $"{appVersion} (b{Settings.Default.Build})",
                AvatarSystem = Settings.Default.AvatarSystem,
                DataRoot     = Settings.Default.HostDataRoot,
            };
        }
    }
}