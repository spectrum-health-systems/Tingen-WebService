// 260724_code
// 260724_documentation

using System;
using System.IO;
using TingenWebService.Du;
using TingenWebService.Trove;

namespace TingenWebService.Core
{
    internal class Framework
    {
        /// <summary>Avatar generated data.</summary>
        public string AvatarGeneratedDataRoot { get; set; }

        /// <summary>Application data.</summary>
        public string AppDataRoot { get; set; }

        /// <summary>Configuration data.</summary>
        public string ConfigRoot { get; set; }

        /// <summary>Export data.</summary>
        public string ExportRoot { get; set; }

        /// <summary>History data.</summary>
        public string HistoryRoot { get; set; }

        /// <summary>Import data.</summary>
        public string ImportRoot { get; set; }

        /// <summary>Log data.</summary>
        public string LogRoot { get; set; }

        /// <summary>Blueprint data.</summary>
        public string BlueprintRoot { get; set; }

        /// <summary>Epistle data.</summary>
        public string EpistleRoot { get; set; }

        /// <summary>Session data.</summary>
        public string SessionRoot { get; set; }

        /// <summary>Translation table data.</summary>
        public string TranslationTableRoot { get; set; }

        /// <summary>Loads the framework with the specified paths and avatar system.</summary>
        /// <param name="hostDataPath">The root path for data storage.</param>
        /// <param name="hostWwwPath">The root path for web resources.</param>
        /// <param name="avatarSystem">The avatar system identifier.</param>
        /// <returns>A <see cref="Framework"/> instance with the specified paths.</returns>
        internal static Framework Load(string hostDataPath, string hostWwwPath, string avatarSystem)
        {
            return new Framework()
            {
                AvatarGeneratedDataRoot = Path.Combine(hostDataPath, "WebService", "AvatarGeneratedData"),
                AppDataRoot             = Path.Combine(hostDataPath, "WebService", avatarSystem, "AppData"),
                ConfigRoot              = Path.Combine(hostDataPath, "WebService", avatarSystem, "Config"),
                ExportRoot              = Path.Combine(hostDataPath, "WebService", avatarSystem, "Export"),
                HistoryRoot             = Path.Combine(hostDataPath, "WebService", avatarSystem, "History"),
                ImportRoot              = Path.Combine(hostDataPath, "WebService", avatarSystem, "Import"),
                LogRoot                 = Path.Combine(hostDataPath, "WebService", avatarSystem, "Log"),
                BlueprintRoot           = Path.Combine(hostDataPath, "WebService", avatarSystem, "Blueprints"),
                EpistleRoot             = Path.Combine(hostDataPath, "WebService", avatarSystem, "Epistle"),
                SessionRoot             = Path.Combine(hostDataPath, "WebService", avatarSystem, "Session"),
                TranslationTableRoot    = Path.Combine(hostDataPath, "WebService", avatarSystem, "TranslationTables")
            };
        }

        /// <summary>Verifies that all directories in the framework exist, creating them if necessary.</summary>
        /// <param name="twsFramework">The framework instance containing the paths to verify.</param>
        internal static void Verify(Framework twsFramework)
        {
            foreach (var path in Catalog.FrameworkPathList(twsFramework))
            {
                try
                {
                    DuDirectory.EnsureDirectoryExists(path);
                }
                catch (Exception ex)
                {
                    /* Use a primeval log to log the error, since the logging functionality is not initialized yet.
                     */
                    Logger.LogEvent.Primeval($"ERROR-CreatingPath-{path}", $"[7622]: {ex.Message}");
                }
            }
        }
    }
}