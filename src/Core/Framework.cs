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
        public string AvatarGeneratedDataRoot { get; set; }

        public string AppDataRoot { get; set; }

        public string ConfigRoot { get; set; }

        public string ExportRoot { get; set; }

        public string HistoryRoot { get; set; }

        public string ImportRoot { get; set; }

        public string LogRoot { get; set; }

        public string BlueprintRoot { get; set; }

        public string EpistleRoot { get; set; }

        public string SessionRoot { get; set; }

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

        ///// <summary>Gets all the framework paths.</summary>
        ///// <param name="twsFramework">The framework instance containing the paths.</param>
        ///// <returns>An array of all framework paths.</returns>
        //private static string[] FrameworkPaths(Framework twsFramework)
        //{
        //    return new string[]
        //    {
        //        twsFramework.DataPath.AvatarGeneratedDataRoot,
        //        twsFramework.DataPath.AppDataRoot,
        //        twsFramework.DataPath.BlueprintRoot,
        //        twsFramework.DataPath.ConfigRoot,
        //        twsFramework.DataPath.ExportRoot,
        //        twsFramework.DataPath.HistoryRoot,
        //        twsFramework.DataPath.ImportRoot,
        //        twsFramework.DataPath.LogRoot,
        //        twsFramework.DataPath.OptObjErrorRoot,
        //        twsFramework.DataPath.TranslationTableRoot,
        //        twsFramework.DataPath.SessionRoot
        //    };
        //}
    }
}