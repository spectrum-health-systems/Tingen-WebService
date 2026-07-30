// 260725_code
// 260730_documentation

using System.IO;

namespace TingenWebService.Du
{
    /// <summary>Utilities for file operations.</summary>
    public class DuFile
    {
        // [260723]
        /// <summary>Writes the specified content to a file at the given path.</summary>
        /// <param name="filePath">The path of the file to write to.</param>
        /// <param name="fileContent">The content to write to the file.</param>
        /// <remarks>If the file already exists, it will be overwritten.</remarks>
        /// <example>
        /// <code>
        /// DuFile.DeadDrop(@"C:\Path\to\file.txt", "Hello, World!");
        /// </code>
        /// </example>
        public static void DeadDrop(string filePath, string fileContent = "")
        {
            File.WriteAllText(filePath, fileContent);
        }

        // [260725]
        /// <summary>Writes the specified content to a file at the given path.</summary>
        /// <param name="filePath">The path of the file to write to.</param>
        /// <param name="fileContent">The content to write to the file.</param>
        /// <remarks>If the file already exists, the content will be appended to the existing file.</remarks>
        /// <example>
        /// <code>
        /// DuFile.DeadDrop(@"C:\Path\to\file.txt", "Hello, World!");
        /// </code>
        /// </example>
        public static void DeadDropAppend(string filePath, string fileContent = "")
        {
            File.AppendAllText(filePath, fileContent);
        }
    }
}