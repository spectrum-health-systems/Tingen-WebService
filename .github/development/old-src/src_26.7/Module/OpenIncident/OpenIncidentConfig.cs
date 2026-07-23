// 260722_code
// 260722_documentation

using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TingenWebService.Core.Du;
using TingenWebService.Core.Logger;

namespace TingenWebService.Module.OpenIncident
{
    /// <summary>OpenIncident Module configuration logic.</summary>
    internal class OpenIncidentConfig
    {
        /// <include file='AppData/XmlDocumentation/TngnWsvc.xml' path='TngnWsvc/Class[@name="CommonDefinition"]/ExecutingAssembly/*'/>
        public static string ExeAsm { get; set; } = Assembly.GetExecutingAssembly().GetName().Name;

        /// <summary>The OpenIncident Module operating mode.</summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>Enabled - The module is active and operational.</item>
        /// <item>Disabled - The module is inactive and non-operational.</item>
        /// </list>
        /// </remarks>
        /// <value>The configured mode value, such as <c>enabled</c>.</value>
        public string Mode { get; set; }

        //TODO Rename this to "BypassList"
        /// <summary>The list of Avatar usernames that bypass the OpenIncident module.</summary>
        /// <remarks>
        /// If an Avatar username (e.g., "JSMITH") is included in this list, they will bypass the OpenIncident module
        /// functionality, and the Open Incident form will behave normally.
        /// </remarks>
        public List<string> Whitelist { get; set; }

        //TODO Not sure if this is needed.
        /// <summary>The list of users requiring conditional handling.</summary>
        /// <value>A collection of user identifiers that should be treated as greylisted users.</value>
        public List<string> Greylist { get; set; }

        //TODO Rename this to "BannedList" - Also, not sure if this is needed.
        /// <summary>The list of Avatar usernames explicitly denied access to the module.</summary>
        public List<string> Blacklist { get; set; }

        /// <summary>The list of authorized Avatar User Roles that are permitted to view incidents.</summary>
        /// <remarks>
        /// If an Avatar User Role (e.g., "NXOPNURSE") is included in this list, it's members will be able to view Open
        /// Incident forms, regardless of whether they are the original author of the incident.
        /// </remarks>
        public List<string> AuthorizedUserRoles { get; set; }

        /// <summary>The Brief Incident Description field ID.</summary>
        /// <remarks>
        /// TBD - What this is used for.
        /// </remarks>
        public string BriefIncidentDescriptionFieldId { get; set; }

        /// <summary>The Program of Incident field ID.</summary>
        /// <remarks>
        /// TBD - What this is used for.
        /// </remarks>
        public string ProgramOfIncidentFieldId { get; set; }

        /// <summary>The Person Completing the Incident Form field ID.</summary>
        /// <remarks>
        /// TBD - What this is used for.
        /// </remarks>
        public string PersonCompletingIncidentFormFieldId { get; set; }

        /// <summary>The message returned when the user lacks an authorized role.</summary>
        /// <remarks>
        /// If the user is not a member of any of the roles specified in <see cref="AuthorizedUserRoles"/>, this message
        /// will be displayed to the user when they attempt to view an Open Incident form.
        /// </remarks>
        public string NotMemberOfAuthorizedUserRoleMsg { get; set; }

        /// <summary>The error code returned when the user lacks an authorized role.</summary>
        /// <remarks>
        /// If the user is not a member of any of the roles specified in <see cref="AuthorizedUserRoles"/>, this error code
        /// will be returned when they attempt to view an Open Incident form.
        /// </remarks>
        public int NotMemberOfAuthorizedUserRoleErrCode { get; set; }

        /// <summary>The message returned when a non-author opens an incident.</summary>
        /// <remarks>
        /// If the user is not the original author of the incident, this message will be displayed to the user when they
        /// attempt to open the incident.
        /// </remarks>
        public string NotOriginalAuthorOpenMsg { get; set; }

        /// <summary>The error code returned when a non-author opens an incident.</summary>
        /// <remarks>
        /// If the user is not the original author of the incident, this error code will be returned when they attempt to open the incident.
        /// </remarks>
        public int NotOriginalAuthorOpenErrCode { get; set; }

        /// <summary>The message returned when a non-author attempts to submit an incident.</summary>
        /// <remarks>
        /// If the user is not the original author of the incident, this message will be displayed to the user when they attempt to submit the incident.
        /// </remarks>
        public string NotOriginalAuthorSubmitMsg { get; set; }

        /// <summary>The error code returned when a non-author attempts to submit an incident.</summary>
        /// <remarks>
        /// If the user is not the original author of the incident, this error code will be returned when they attempt to submit the incident.
        /// </remarks>
        public int NotOriginalAuthorSubmitErrCode { get; set; }

        /// <summary>The message returned when the program of incident is invalid.</summary>
        /// <remarks>
        /// The message displayed to the user when the program of incident value is not valid and must be corrected
        /// before submitting the incident.
        /// </remarks>
        public string InvalidProgramOfIncidentMsg { get; set; }

        /// <summary>The error code returned when the program of incident is invalid.</summary>
        /// <remarks>
        /// The error code returned when the program of incident value is not valid and must be corrected before
        /// submitting the incident.
        /// </remarks>
        public int InvalidProgramOfIncidentErrCode { get; set; }

        /// <summary>The message returned when the user description cannot be resolved.</summary>
        /// <remarks>
        /// The message displayed to the user when the current username is missing from the translation table.
        /// </remarks>
        public string UnknownUserDescriptionMsg { get; set; }

        /// <summary>The error code returned when the user description cannot be resolved.</summary>
        /// <remarks>
        /// The numeric error code associated with an unknown user description result.
        /// </remarks>
        public int UnknownUserDescriptionErrCode { get; set; }

        /// <summary>Load the OpenIncident configuration.</summary>
        /// <remarks>
        /// If the configuration file does not exist, a new default configuration file is created before the file is
        /// loaded and returned.
        /// </remarks>
        /// <param name="configPath">The full path to the configuration file.</param>
        /// <param name="traceLogLimit">The maximum trace logging level to write.</param>
        /// <param name="sessionFolder">The session folder where trace output is written.</param>
        /// <returns>The loaded <see cref="OpenIncidentConfig"/> instance.</returns>
        /// <example>
        /// <code>
        /// OpenIncidentConfig config = OpenIncidentConfig.Load(configPath, traceLogLimit, sessionFolder);
        /// </code>
        /// </example>
        internal static OpenIncidentConfig Load(string configPath, int traceLogLimit, string sessionFolder)
        {
            LogEvent.Trace(1, traceLogLimit, sessionFolder, ExeAsm);

            if (!File.Exists(configPath))
            {
                LogEvent.Trace(2, traceLogLimit, sessionFolder, ExeAsm);

                CreateNew(configPath, traceLogLimit, sessionFolder);
            }

            return DuJson.ImportFromLocalFile<OpenIncidentConfig>(configPath);
        }

        /// <summary>Creates a new configuration file with default OpenIncident module values.</summary>
        /// <remarks>
        /// This method initializes a new <see cref="OpenIncidentConfig"/> instance and writes it to the path
        /// specified by <paramref name="configPath"/>.
        /// </remarks>
        /// <param name="configPath">The full path where the configuration file should be written.</param>
        /// <param name="traceLogLimit">The maximum trace logging level to write.</param>
        /// <param name="sessionFolder">The session folder where trace output is written.</param>
        /// <example>
        /// <code>
        /// OpenIncidentConfig.New(configPath, traceLogLimit, sessionFolder);
        /// </code>
        /// </example>
        internal static void CreateNew(string configPath, int traceLogLimit, string sessionFolder)
        {
            LogEvent.Trace(1, traceLogLimit, sessionFolder, ExeAsm);

            OpenIncidentConfig openIncident = new OpenIncidentConfig()
            {
                Mode                                = "enabled",
                Whitelist                           = new List<string>(),
                Blacklist                           = new List<string>(),
                Greylist                            = new List<string>(),
                AuthorizedUserRoles                 = new List<string>()
                {
                    "NXCOMPLIANCE",
                    "NXDOCCLINSUP",
                    "NXDOCMANAGEMENT",
                    "NXDOCNURSESUP",
                    "SuperUser"
                },
                BriefIncidentDescriptionFieldId     = "2",
                ProgramOfIncidentFieldId            = "20",
                PersonCompletingIncidentFormFieldId = "32",
                NotMemberOfAuthorizedUserRoleMsg    = "You are not authorized to view this incident.",
                NotMemberOfAuthorizedUserRoleErrCode= 1,
                NotOriginalAuthorOpenMsg            = "Since you are not the original author of this incident, you will only be able to view it and will not be able to submit modifications.",
                NotOriginalAuthorOpenErrCode        = 3,
                NotOriginalAuthorSubmitMsg          = "Since you are not the original author of this incident, you cannot submit modifications to this incident.",
                NotOriginalAuthorSubmitErrCode      = 1,
                UnknownUserDescriptionMsg           = "Your username was not found in the translation table. Please contact the IT Service Desk and give them this code: WSVC4274",
                UnknownUserDescriptionErrCode       = 1,
                InvalidProgramOfIncidentMsg         = "The Program of Incident specified is not valid. Please correct it before submitting the incident.",
                InvalidProgramOfIncidentErrCode     = 1
            };

            DuJson.ExportToLocalFile(openIncident, configPath, true);
        }
    }
}