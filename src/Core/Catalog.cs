

namespace TingenWebService.Core
{
    internal class Catalog
    {
        /// <summary>Gets all the framework paths.</summary>
        /// <param name="twsFramework">The framework instance containing the paths.</param>
        /// <returns>An array of all framework paths.</returns>
        internal static string[] FrameworkPaths(Framework twsFramework) =>
            new string[]
            {
                twsFramework.DataPath.AvatarGeneratedDataRoot,
                twsFramework.DataPath.AppDataRoot,
                twsFramework.DataPath.BlueprintRoot,
                twsFramework.DataPath.ConfigRoot,
                twsFramework.DataPath.ExportRoot,
                twsFramework.DataPath.HistoryRoot,
                twsFramework.DataPath.ImportRoot,
                twsFramework.DataPath.LogRoot,
                twsFramework.DataPath.OptObjErrorRoot,
                twsFramework.DataPath.TranslationTableRoot,
                twsFramework.DataPath.SessionRoot
            };
    }
}