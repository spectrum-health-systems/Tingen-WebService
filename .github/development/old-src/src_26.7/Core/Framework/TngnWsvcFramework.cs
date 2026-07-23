// 251112_code
// 260515_documentation.

using System.IO;
using System.Reflection;

namespace TingenWebService.Core.Framework
{
    /// <summary>Provides core framework initialization and verification for the Tingen web service.</summary>
    public class TngnWsvcFramework
    {
        /// <include file='../../AppData/XmlDocumentation/TngnWsvc.xml' path='TngnWsvc/Class[@name="CommonDefinition"]/ExecutingAssembly/*'/>
        public static string ExeAsm { get; set; } = Assembly.GetExecutingAssembly().GetName().Name;

        /// <summary>The data folder paths for the Tingen web service.</summary>
        /// <value>A <see cref="TngnWsvcDataFolders"/> instance containing all relevant data folder paths.</value>
        public TngnWsvcDataFolders TngnWsvcDataFolder { get; set; }

        /// <summary>The WWW folder paths for the Tingen web service.</summary>
        /// <value>A <see cref="TngnWsvcWwwFolders"/> instance containing all relevant WWW folder paths.</value>
        public TngnWsvcWwwFolders TngnWsvcWwwFolder { get; set; }

        /// <summary>Creates and returns a fully initialized <see cref="TngnWsvcFramework"/> instance.</summary>
        /// <remarks>Loads both the data and WWW folder structures for the current session.</remarks>
        /// <param name="avatarSystem">The Avatar system identifier.</param>
        /// <param name="avatarUserId">The Avatar user ID for the current session.</param>
        /// <param name="serverDataPath">The root server path for data storage.</param>
        /// <param name="serverWwwPath">The root server path for WWW content.</param>
        /// <param name="sessionDate">The date of the current session.</param>
        /// <param name="sessionStartTime">The start time of the current session.</param>
        /// <returns>A new <see cref="TngnWsvcFramework"/> instance with all folder paths initialized.</returns>
        /// <example>
        /// <code>
        /// var framework = TngnWsvcFramework.Load(
        ///     avatarSystem:     "LIVE",
        ///     avatarUserId:     "jsmith",
        ///     serverDataPath:   @"C:\Tingen_Data",
        ///     serverWwwPath:    @"C:\inetpub\wwwroot",
        ///     sessionDate:      "2026-05-15",
        ///     sessionStartTime: "103045");
        ///
        /// Console.WriteLine(framework.TngnWsvcDataFolder.Log);
        /// // Output: C:\Tingen_Data\LIVE\AppData\Log
        ///
        /// Console.WriteLine(framework.TngnWsvcWwwFolder.WwwRoot);
        /// // Output: C:\inetpub\wwwroot\LIVE
        /// </code>
        /// </example>
        public static TngnWsvcFramework Load(string avatarSystem, string avatarUserId, string serverDataPath, string serverWwwPath, string sessionDate, string sessionStartTime) =>
            new TngnWsvcFramework
            {
                TngnWsvcDataFolder = TngnWsvcDataFolders.Load(avatarSystem, avatarUserId, serverDataPath, sessionDate, sessionStartTime),
                TngnWsvcWwwFolder  = TngnWsvcWwwFolders.Load(avatarSystem, serverWwwPath),
            };

        /// <summary>Verifies the framework state and ensures required data folders exist.</summary>
        /// <remarks>
        /// Iterates over all properties of <see cref="TngnWsvcDataFolders"/> and creates any missing directories whose
        /// paths contain the <paramref name="avatarSystem"/> identifier.
        /// </remarks>
        /// <param name="tngnWsvcFramework">The <see cref="TngnWsvcFramework"/> instance to verify.</param>
        /// <param name="traceLogLimit">The trace log limit used during verification.</param>
        /// <param name="avatarSystem">The Avatar system identifier used to filter relevant folder paths.</param>
        /// <param name="sessionDate">The date of the current session.</param>
        /// <example>
        /// <code>
        /// var framework = TngnWsvcFramework.Load(
        ///     "LIVE", "jsmith", @"C:\Tingen_Data", @"C:\inetpub\wwwroot",
        ///     "2026-05-15", "103045");
        ///
        /// // Ensure all "LIVE" data folders exist on disk:
        /// TngnWsvcFramework.Verify(framework, traceLogLimit: 3, avatarSystem: "LIVE", sessionDate: "2026-05-15");
        /// </code>
        /// </example>
        internal static void Verify(TngnWsvcFramework tngnWsvcFramework, int traceLogLimit, string avatarSystem, string sessionDate)
        {
            // Logging and folder creation logic omitted for brevity
            foreach (var property in tngnWsvcFramework.TngnWsvcDataFolder.GetType().GetProperties())
            {
                var folderName = property.GetValue(tngnWsvcFramework.TngnWsvcDataFolder).ToString();
                if (folderName.Contains(avatarSystem))
                {
                    if (!Directory.Exists(folderName))
                    {
                        Directory.CreateDirectory(folderName);
                    }
                }
            }
        }
    }
}