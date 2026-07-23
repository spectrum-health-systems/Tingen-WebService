// 251113_code
// 260520_documentation.

using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TingenWebService.Core.Du;
using TingenWebService.Core.Logger;

namespace TingenWebService.Module.DoseChangeEvaluationOtp
{
    /// <summary>Represents configuration settings for the dose change evaluation OTP module.</summary>
    /// <remarks>
    /// This type stores the allowlist, review list, denylist, field identifiers, and user-facing message values
    /// used when evaluating whether an order may be authorized within the module.
    /// </remarks>
    internal class DoseChangeEvaluationOtpConfig
    {
        /// <include file='AppData/XmlDocumentation/TngnWsvc.xml' path='TngnWsvc/Class[@name="CommonDefinition"]/ExecutingAssembly/*'/>
        public static string ExeAsm { get; set; } = Assembly.GetExecutingAssembly().GetName().Name;

        /// <summary>Gets or sets the module operating mode.</summary>
        /// <value>The configured mode value, such as <c>enabled</c>.</value>
        public string Mode { get; set; }

        /// <summary>Gets or sets the list of providers explicitly allowed to authorize orders.</summary>
        /// <value>A collection of identifiers that are allowed to authorize orders.</value>
        public List<string> Whitelist { get; set; }

        /// <summary>Gets or sets the list of providers that require additional review.</summary>
        /// <value>A collection of identifiers that should be handled as greylisted providers.</value>
        public List<string> Greylist { get; set; }

        /// <summary>Gets or sets the list of providers explicitly denied from authorizing orders.</summary>
        /// <value>A collection of identifiers that are not allowed to authorize orders.</value>
        public List<string> Blacklist { get; set; }

        /// <summary>Gets or sets the field identifier used to determine the authorizing provider.</summary>
        /// <value>The Avatar field identifier that indicates whether the provider is authorizing the order.</value>
        public string ProviderIsAuthorizingOrderFieldId { get; set; }

        /// <summary>Gets or sets the field identifier used for the physician approver.</summary>
        /// <value>The Avatar field identifier associated with the physician approver value.</value>
        public string PhysicianApproverFieldId { get; set; }

        /// <summary>Gets or sets the message returned when authorization is not allowed.</summary>
        /// <value>The user-facing message shown when the current user cannot authorize the order.</value>
        public string NotAllowedToAuthorizeOptObjMsg { get; set; }

        /// <summary>Gets or sets the error code returned when authorization is not allowed.</summary>
        /// <value>The numeric error code associated with a failed authorization check.</value>
        public int NotAllowedToAuthorizeOptObjErrCode { get; set; }

        /// <summary>Loads the module configuration from the specified path.</summary>
        /// <remarks>
        /// If the configuration file does not exist, a new default configuration file is created before the file is
        /// loaded and returned.
        /// </remarks>
        /// <param name="configPath">The full path to the configuration file.</param>
        /// <param name="traceLogLimit">The maximum trace logging level to write.</param>
        /// <param name="sessionFolder">The session folder where trace output is written.</param>
        /// <returns>The loaded <see cref="DoseChangeEvaluationOtpConfig"/> instance.</returns>
        /// <example>
        /// <code>
        /// DoseChangeEvaluationOtpConfig config =
        ///     DoseChangeEvaluationOtpConfig.Load(configPath, traceLogLimit, sessionFolder);
        /// </code>
        /// </example>
        internal static DoseChangeEvaluationOtpConfig Load(string configPath, int traceLogLimit, string sessionFolder)
        {
            LogEvent.Trace(1, traceLogLimit, sessionFolder, ExeAsm);

            if (!File.Exists(configPath))
            {
                LogEvent.Trace(2, traceLogLimit, sessionFolder, ExeAsm);

                New(configPath, traceLogLimit, sessionFolder);
            }

            return DuJson.ImportFromLocalFile<DoseChangeEvaluationOtpConfig>(configPath);
        }

        /// <summary>Creates a new configuration file with default module values.</summary>
        /// <remarks>
        /// This method initializes a new <see cref="DoseChangeEvaluationOtpConfig"/> instance and writes it to the
        /// path specified by <paramref name="configPath"/>.
        /// </remarks>
        /// <param name="configPath">The full path where the configuration file should be written.</param>
        /// <param name="traceLogLimit">The maximum trace logging level to write.</param>
        /// <param name="sessionFolder">The session folder where trace output is written.</param>
        /// <example>
        /// <code>
        /// DoseChangeEvaluationOtpConfig.New(configPath, traceLogLimit, sessionFolder);
        /// </code>
        /// </example>
        internal static void New(string configPath, int traceLogLimit, string sessionFolder)
        {
            LogEvent.Trace(1, traceLogLimit, sessionFolder, ExeAsm);

            DoseChangeEvaluationOtpConfig doseChangeEvaluationOtpConfig = new DoseChangeEvaluationOtpConfig()
            {
                Mode                               = "enabled",
                Whitelist                          = new List<string>(),
                Blacklist                          = new List<string>(),
                Greylist                           = new List<string>(),
                ProviderIsAuthorizingOrderFieldId  = "350.55",
                PhysicianApproverFieldId           = "339.75",
                NotAllowedToAuthorizeOptObjMsg     = "You are not logged in as the authorizing provider.",
                NotAllowedToAuthorizeOptObjErrCode = 1
            };

            DuJson.ExportToLocalFile(doseChangeEvaluationOtpConfig, configPath, true);
        }
    }
}