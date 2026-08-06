// 260806_code
// 260805_documentation

namespace TingenWebService.Core.Trove
{
    internal class ErrorMessage
    {
        /* =====================================================================
         * 0000 - 0999: Miscellaneous
         * =====================================================================
         * Nothing here yet.
         */

        /* =====================================================================
         * 1000 - 1999: Tingen Web Service
         * =====================================================================
         * ERR1000 - Error message included in file content
         * ERR1010 -
         * ERR1020 -
         * ERR1110 - Failed to load runtime configuration
         * ERR1120 - Failed to load framework configuration
         * ERR1130 - Missing TngnWsvc.config file
         * ERR1140 - Failed to validate framework
         * ERR1210 - Session duration timeout exceeded
         */

        /// <summary>Build the runtime configuration load failure message.</summary>
        /// <param name="errorMessage">The error message.</param>
        /// <returns>The runtime configuration load failure message.</returns>
        internal static string ERR1000(string errorMessage) => $"Error message: {errorMessage}";
    }
}