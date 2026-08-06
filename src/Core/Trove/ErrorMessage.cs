// 260806_code
// 260806_documentation

namespace TingenWebService.Core.Trove
{
    internal class ErrorMessage
    {
        /// <summary>Generic error with message.</summary>
        /// <param name="errorMessage">The error message.</param>
        /// <returns>The formatted error message.</returns>
        internal static string ERR1000(string errorMessage) => $"Error message: {errorMessage}";
    }
}