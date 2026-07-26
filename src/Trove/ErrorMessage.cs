// 082056_code
// 082056_documentation

using System;

namespace TingenWebService.Trove
{
    internal class ErrorMessage
    {
        internal static string Error3876() =>
            $"[3876]{Environment.NewLine}" +
            $"Missing OptionObject and/or Script Parameter";

        internal static string Error7622(string path, string errorMessage) =>
            $"[7622]{Environment.NewLine}" +
            $"Framework verification failed:{Environment.NewLine}" +
            $"  Path could not be created: {path}{Environment.NewLine}" +
            $"  Message:  {errorMessage}";

        internal static string Error7516(string path, string errorMessage) =>
            $"[7516]{Environment.NewLine}" +
            $"Maintenance failure:{Environment.NewLine}" +
            $"  Path could not be created: {path}{Environment.NewLine}" +
            $"  Message:  {errorMessage}";

        internal static string Error4694() =>
            $"[4694]{Environment.NewLine}" +
            $"Missing Tingen Web Service configuration file";

    }
}