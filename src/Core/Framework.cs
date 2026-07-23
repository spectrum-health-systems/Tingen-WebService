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

        internal static Framework Load(string hostDataPath, string hostWwwPath)
        {
            return new Framework()
            {
                DataPath = new DataPaths()
                {
                    AvatarGeneratedDataRoot = Path.Combine(hostDataPath, "AvatarGeneratedData"),
                    AppDataRoot             = Path.Combine(hostDataPath, "AppData"),
                    BlueprintRoot           = Path.Combine(hostDataPath, "Blueprints"),
                    ConfigRoot              = Path.Combine(hostDataPath, "Config"),
                    ExportRoot              = Path.Combine(hostDataPath, "Export"),
                    HistoryRoot             = Path.Combine(hostDataPath, "History"),
                    ImportRoot              = Path.Combine(hostDataPath, "Import"),
                    LogRoot                 = Path.Combine(hostDataPath, "Log"),
                    OptObjErrorRoot         = Path.Combine(hostDataPath, "OptObjError"),
                    TranslationTableRoot    = Path.Combine(hostDataPath, "TranslationTables"),
                    SessionRoot             = Path.Combine(hostDataPath, "Session")
                },
                WwwPath = new WwwPaths()
                {
                    AppDataRoot          = Path.Combine(hostWwwPath, "App_Data"),
                    BlueprintRoot        = Path.Combine(hostWwwPath, "Blueprints"),
                    OptObjErrorRoot      = Path.Combine(hostWwwPath, "OptObjError"),
                    TranslationTableRoot = Path.Combine(hostWwwPath, "TranslationTables")
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