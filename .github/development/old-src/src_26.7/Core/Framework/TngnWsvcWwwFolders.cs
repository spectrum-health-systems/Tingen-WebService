// 251112_code
// 2260515_documentation.

namespace TingenWebService.Core.Framework
{
    /// <summary>Represents the WWW folder paths used by the Tingen Web Service.</summary>
    /// <remarks>
    /// Each property holds an absolute path to a specific folder under the server's WWW root for a given
    /// Avatar system.<br/>
    /// <br/>
    /// Use <see cref="Load"/> to create a populated instance.
    /// </remarks>
    public class TngnWsvcWwwFolders
    {
        /// <summary>The root WWW folder for the Avatar system.</summary>
        /// <value>An absolute path in the form <c>{serverWwwPath}\{avatarSystem}</c>.</value>
        public string WwwRoot { get; set; }

        /// <summary>The application data root folder under the Avatar system's <c>bin</c> directory.</summary>
        /// <value>An absolute path in the form <c>{serverWwwPath}\{avatarSystem}\bin\AppData</c>.</value>
        public string AppDataRoot { get; set; }

        /// <summary>The Blueprint folder under <see cref="AppDataRoot"/>.</summary>
        /// <value>An absolute path in the form <c>{serverWwwPath}\{avatarSystem}\bin\AppData\Blueprint</c>.</value>
        public string Blueprint { get; set; }

        /// <summary>The OptObjErrorMessage folder under <see cref="AppDataRoot"/>.</summary>
        /// <value>An absolute path in the form <c>{serverWwwPath}\{avatarSystem}\bin\AppData\OptObjErrorMessage</c>.</value>
        public string OptObjErrorMessage { get; set; }

        /// <summary>The TranslationTable folder under <see cref="AppDataRoot"/>.</summary>
        /// <value>An absolute path in the form <c>{serverWwwPath}\{avatarSystem}\bin\AppData\TranslationTable</c>.</value>
        public string TranslationTable { get; set; }

        /// <summary>Creates a fully populated <see cref="TngnWsvcWwwFolders"/> instance for the given Avatar system.</summary>
        /// <remarks>
        /// All paths are constructed by combining <paramref name="serverWwwPath"/> and
        /// <paramref name="avatarSystem"/>.
        /// </remarks>
        /// <param name="avatarSystem">The name of the Avatar system subdirectory under the WWW root.</param>
        /// <param name="serverWwwPath">The absolute path to the server's WWW root directory.</param>
        /// <returns>A new <see cref="TngnWsvcWwwFolders"/> instance with all folder paths populated.</returns>
        /// <example>
        /// <code>
        /// var wwwFolders = TngnWsvcWwwFolders.Load("LIVE", @"C:\inetpub\wwwroot");
        ///
        /// Console.WriteLine(wwwFolders.WwwRoot);
        /// // Output: C:\inetpub\wwwroot\LIVE
        ///
        /// Console.WriteLine(wwwFolders.AppDataRoot);
        /// // Output: C:\inetpub\wwwroot\LIVE\bin\AppData
        ///
        /// Console.WriteLine(wwwFolders.Blueprint);
        /// // Output: C:\inetpub\wwwroot\LIVE\bin\AppData\Blueprint
        /// </code>
        /// </example>
        internal static TngnWsvcWwwFolders Load(string avatarSystem, string serverWwwPath)
        {
            return new TngnWsvcWwwFolders
            {
                WwwRoot            = $@"{serverWwwPath}\{avatarSystem}",
                AppDataRoot        = $@"{serverWwwPath}\{avatarSystem}\bin\AppData",
                Blueprint          = $@"{serverWwwPath}\{avatarSystem}\bin\AppData\Blueprint",
                OptObjErrorMessage = $@"{serverWwwPath}\{avatarSystem}\bin\AppData\OptObjErrorMessage",
                TranslationTable   = $@"{serverWwwPath}\{avatarSystem}\bin\AppData\TranslationTable",
            };
        }
    }
}