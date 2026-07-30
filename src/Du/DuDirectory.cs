// 260730_code
// 260730_documentation

using System.IO;

namespace TingenWebService.Du
{
    /// <summary>Utilities for directory operations.</summary>
    public class DuDirectory
    {
        // TODO - Verify this works with verifying a subdirectory of a non-existent directory creates both.
        // [260730]
        /// <summary>Ensures that the specified directory exists.</summary>
        /// <param name="dir">The directory to verify/create.</param>
        /// <remarks>If the directory does not exist, it will be created.</remarks>
        /// <example>
        /// <code>
        /// DuDirectory.ForceExist(@"C:\directory\name");
        /// </code>
        /// </example>
        public static void ForceExist(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
    }
}