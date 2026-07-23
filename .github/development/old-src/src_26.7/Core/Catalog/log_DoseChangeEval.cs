// 260514_code
// 260617_documentation

using System;

namespace TingenWebService.Core.Catalog
{
    /// <summary>Log message catalog for Dose Change Evaluation OTP functionality.</summary>
    internal static class log_DoseChangeEval
    {
        /// <summary>Build the log message used when the prescriber is authorizing an order.</summary>
        /// <remarks>
        /// Helps identify whether the prescriber or a pharmacist is authorizing an order, which assists with
        /// troubleshooting issues in the order authorization process.
        /// </remarks>
        /// <param name="callerInfo">Information about the calling class, method, and line.</param>
        /// <param name="staffMemberIdQuery">The staff member ID query string used for the lookup.</param>
        /// <param name="physicianApprover">The physician or approver associated with the order.</param>
        /// <returns>An array of log message lines describing the prescriber authorization event.</returns>
        /// <example>
        /// <code>
        /// var callerInfo = LogComponents.GetCallerInfo();
        /// var logLines   = log_DoseChangeEval.PrescriberIsAuthorizing(callerInfo, "SELECT * FROM staff WHERE Id = '12345'", "Dr. Jane Smith");
        /// foreach (var line in logLines)
        /// {
        ///     Console.WriteLine(line);
        /// }
        /// </code>
        /// </example>
        internal static string[] PrescriberIsAuthorizing(string callerInfo, string staffMemberIdQuery, string physicianApprover) => new string[]
        {
            $"{callerInfo}",
            "Dose Change Evaluation OTP - Prescriber Is Authorizing Order Event Triggered",
            $"Query: {staffMemberIdQuery}{Environment.NewLine}" +
            $"Physician/Approver : {physicianApprover}"
        };
    }
}