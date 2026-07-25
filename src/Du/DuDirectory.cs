// 260725_code
// 260723_documentation

using System.IO;

namespace TingenWebService.Du
{
    public class DuDirectory
    {
        // [260723]
        /// <summary>Ensures that the specified directory exists. If it does not exist, it will be created.</summary>
        /// <param name="path">The path of the directory to check or create.</param>
        /// <example>
        /// <code>
        /// DuDirectory.EnsureDirectoryExists("C:\\Tingen_Data");
        /// </code>
        /// </example>
        public static void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}