// 260723_code
// 260723_documentation

using System;
using System.IO;
using TingenWebService.Du;

namespace TingenWebService.Core
{
    internal class Framework
    {
        public DataPaths DataPath { get; set; }

        public WwwPaths WwwPath { get; set; }

        public class DataPaths
        {
            public string AvatarGeneratedDataRoot { get; set; }
            public string AppDataRoot { get; set; }
            public string BlueprintRoot { get; set; }
            public string ConfigRoot { get; set; }
            public string ExportRoot { get; set; }
            public string HistoryRoot { get; set; }
            public string ImportRoot { get; set; }
            public string LogRoot { get; set; }
            public string OptObjErrorRoot { get; set; }
            public string TranslationTableRoot { get; set; }
            public string SessionRoot { get; set; }
        }

        internal class WwwPaths
        {
            public string AppDataRoot { get; set; }
            public string BlueprintRoot { get; set; }
            public string OptObjErrorRoot { get; set; }
            public string TranslationTableRoot { get; set; }
        }

        internal static Framework Load(string hostDataPath, string hostWwwPath, string avatarSystem)
        {
            return new Framework()
            {
                DataPath = new DataPaths()
                {
                    AvatarGeneratedDataRoot = Path.Combine(hostDataPath, "WebService", avatarSystem, "AvatarGeneratedData"),
                    AppDataRoot             = Path.Combine(hostDataPath, "WebService", avatarSystem, "AppData"),
                    BlueprintRoot           = Path.Combine(hostDataPath, "WebService", avatarSystem, "Blueprints"),
                    ConfigRoot              = Path.Combine(hostDataPath, "WebService", avatarSystem, "Config"),
                    ExportRoot              = Path.Combine(hostDataPath, "WebService", avatarSystem, "Export"),
                    HistoryRoot             = Path.Combine(hostDataPath, "WebService", avatarSystem, "History"),
                    ImportRoot              = Path.Combine(hostDataPath, "WebService", avatarSystem, "Import"),
                    LogRoot                 = Path.Combine(hostDataPath, "WebService", avatarSystem, "Log"),
                    OptObjErrorRoot         = Path.Combine(hostDataPath, "WebService", avatarSystem, "OptObjError"),
                    TranslationTableRoot    = Path.Combine(hostDataPath, "WebService", avatarSystem, "TranslationTables"),
                    SessionRoot             = Path.Combine(hostDataPath, "WebService", avatarSystem, "Session")
                },
                WwwPath = new WwwPaths()
                {
                    AppDataRoot          = Path.Combine(hostWwwPath, "WebService", avatarSystem, "App_Data"),
                    BlueprintRoot        = Path.Combine(hostWwwPath, "WebService", avatarSystem, "Blueprints"),
                    OptObjErrorRoot      = Path.Combine(hostWwwPath, "WebService", avatarSystem, "OptObjError"),
                    TranslationTableRoot = Path.Combine(hostWwwPath, "WebService", avatarSystem, "TranslationTables")
                }
            };
        }

        /// <summary>Verifies that all directories in the framework exist, creating them if necessary.</summary>
        /// <param name="twsFramework">The framework instance containing the paths to verify.</param>
        internal static void Verify(Framework twsFramework)
        {
            foreach (var path in DuConvert.ObjectToStringArray(twsFramework))
            {
                DuDirectory.EnsureDirectoryExists(path);
            }

            DuFile.DeadDrop(Path.Combine(twsFramework.DataPath.LogRoot, $"framework-verified.{DateTime.Now:yyyyMMddHHmmss}"), "Framework verified.");
        }
    }
}