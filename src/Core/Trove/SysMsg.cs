// 260729_code
// 260729_documentation

using System;

namespace TingenWebService.Core.Trove
{
    internal class SysMsg
    {
        /* =====================================================================
         * 0000 - 0999: Miscellaneous
         * =====================================================================
         * Nothing here yet.
         */

        /* =====================================================================
         * 1000 - 1999: Tingen Web Service
         * =====================================================================
         * ERR1010 - Missing sent OptionObject
         * ERR1020 - Missing sent ScriptParameter
         * ERR1110 - Failed to load runtime configuration
         * ERR1120 - Failed to load framework configuration
         * ERR1130 - Missing TngnWsvc.config file
         * ERR1140 - Failed to validate framework
         * ERR1210 - Session duration timeout exceeded
         */

        /// <summary>Build the missing OptionObject system message.</summary>
        /// <returns>The missing OptionObject system message.</returns>
        internal static string[] ERR1010() =>
            new string[]
            {
                "ERR1010",
                $"The OptionObject was not sent from Avatar."
            };

        /// <summary>Build the missing Script Parameter message.</summary>
        /// <returns>The missing Script Parameter message.</returns>
        internal static string[] ERR1020() =>
            new string[]
            {
                "ERR1020",
                $"The Script Parameter was not sent from Avatar."
            };

        /// <summary>Build the runtime configuration load failure message.</summary>
        /// <param name="errorMessage">The error message.</param>
        /// <returns>The runtime configuration load failure message.</returns>
        internal static string[] ERR1110(string errorMessage) =>
            new string[]
            {
                "ERR1110",
                $"Failed to load runtime configuration: {errorMessage}"
            };

        internal static string[] ERR1120(string errorMessage) =>
            new string[]
            {
                "ERR1120",
                $"Failed to load framework configuration: {errorMessage}"
            };

        /// <summary>Build the missing TngnWsvc.config message.</summary>
        /// <returns>The missing configuration file message.</returns>
        internal static string[] ERR1130() =>
            new string[]
            {
                "ERR1130",
                $"Missing Tingen Web Service configuration file."
            };


        /// <summary>Build the framework verification failed message.</summary>
        /// <param name="path">The path that could not be created.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns>The framework verification failed message.</returns>
        internal static string[] ERR1140(string path, string errorMessage) =>
            new string[]
            {
                "ERR1140",
                $"Framework verification failed:{Environment.NewLine}" +
                $"  Path could not be created: {path}{Environment.NewLine}" +
                $"  Message:  {errorMessage}"
            };

        internal static string[] ERR1210(string avatarUser, string sessionDurationMilliseconds) =>
            new string[]
            {
                $"ERR1210",
                $"User {avatarUser} session exceeded timeout setting: {sessionDurationMilliseconds}"
            };

        /* =====================================================================
         * 2000 - 2999: Tingen Web Service Modules
         * =====================================================================
         * Nothing here yet.
         */





        // TODO ??
        /// <summary>Build the maintenance failure message.</summary>
        /// <param name="path">The path that could not be created.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns>The maintenance failure message.</returns>
        internal static string Error7516(string path, string errorMessage) =>
            $"[7516]{Environment.NewLine}" +
            $"Maintenance failure:{Environment.NewLine}" +
            $"  Path could not be created: {path}{Environment.NewLine}" +
            $"  Message:  {errorMessage}";













    }
}


/*
 * System messages
 * 0000 - System
 * 1000 - TingenWebService
 * 2000 -
 * 4000 - TingenWebService.Du
 * 5000 -
 * 6000 -
 * 7000 - TingenWebService.Module
 * 8000 -
 * 9000 -
 *
 * 3876 - Missing sent OptionObject
 * 5285 - Missing sent ScriptParameter
 * 7622 - Error creating framework path
 * 7516 - Error creating dailyLogPath
 * 4694 - Missing TngnWsvc.config file
 * 7362 - Session timeout error occurred
 * 7540 - Cannot load Runtime configuration
 * 7322 - Cannot load Framework configuration
 */