// 260729_code
// 260730_documentation

using System;
using System.IO;
using ScriptLinkStandard.Objects;
using TingenWebService.Core.Logger;
using TingenWebService.Core.Trove;

namespace TingenWebService.Core.Avatar
{
    /// <summary>Data exchanged between Avatar and the Tingen Web Service.</summary>
    /// <remarks>
    /// <include file='AppData/XmlDoc/Topics.xml' path='Topics/Topic[@name="AvatarData"]/About/*'/><br/>
    /// <include file='AppData/XmlDoc/Topics.xml' path='Topics/Topic[@name="AvatarData"]/AboutOptObj/*'/>
    /// AboutScriptParam Goes Here
    /// </remarks>
    internal class AvatarData
    {
        /// <summary>The original <see cref="OptionObject2015"/> sent from Avatar.</summary>
        /// <remarks>
        /// <include file='AppData/XmlDoc/Topics.xml' path='Topics/Topic[@name="AvatarData"]/AboutOptObj/*'/><br/>
        /// <include file='AppData/XmlDoc/Topics.xml' path='Topics/Topic[@name="AvatarData"]/TypesOfOptObj/*'/>
        /// </remarks>
        public OptionObject2015 SentOptObj { get; set; }

        /// <summary>The worker <see cref="OptionObject2015"/> used during processing.</summary>
        /// <remarks>
        /// <include file='AppData/XmlDoc/Topics.xml' path='Topics/Topic[@name="AvatarData"]/AboutOptObj/*'/><br/>
        /// <include file='AppData/XmlDoc/Topics.xml' path='Topics/Topic[@name="AvatarData"]/TypesOfOptObj/*'/>
        /// </remarks>
        public OptionObject2015 WorkerOptObj { get; set; }

        /// <summary>The complete <see cref="OptionObject2015"/> that is returned to Avatar.</summary>
        /// <remarks>
        /// <include file='AppData/XmlDoc/Topics.xml' path='Topics/Topic[@name="AvatarData"]/AboutOptObj/*'/><br/>
        /// <include file='AppData/XmlDoc/Topics.xml' path='Topics/Topic[@name="AvatarData"]/TypesOfOptObj/*'/>
        /// </remarks>
        public OptionObject2015 CompleteOptObj { get; set; }

        /// <summary>The original script parameter sent from Avatar.</summary>
        /// <remarks>TBD</remarks>
        public string SentScriptParam { get; set; }

        /// <summary>Initialize a new AvatarData object.</summary>
        /// <param name="sentOptObj">The <see cref="OptionObject2015"/> sent from Avatar.</param>
        /// <param name="sentScriptParam"></param>
        /// <remarks>TBD</remarks>
        /// <returns>A new instance of <see cref="AvatarData"/>.</returns>
        internal static AvatarData Build(OptionObject2015 sentOptObj, string sentScriptParam)
        {
            return new AvatarData
            {
                SentOptObj      = sentOptObj,
                WorkerOptObj    = sentOptObj.Clone(),
                CompleteOptObj  = null,
                SentScriptParam = sentScriptParam
            };
        }

        /// <summary>Verify whether an <see cref="OptionObject2015"/> was received from Avatar.</summary>
        /// <param name="sentOptObj">The <see cref="OptionObject2015"/> to verify.</param>
        /// <returns>True if an <see cref="OptionObject2015"/> was sent; otherwise, false.</returns>
        internal static bool WasSent(OptionObject2015 sentOptObj)
        {
            /* DEVNOTE
             * - Use primeval logs here to write error logs, since logging functionality has not been initialized yet.
             * - This can be simplified, but I'm leaving it for now to make it easier to add logging in the future.
             */

            // TODO - Potentially send an email in addition to the error logs.

            if (sentOptObj == null)
            {
                LogEvent.Primeval($"{DateTime.Now:yyMMdd-HHmmss-fffffff}-ERR1010-MissingOptObj", SysMsg.ERR1010()[1]);

                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>Verify whether a script parameter was received from Avatar.</summary>
        /// <param name="sentScriptParam">The script parameter to verify.</param>
        /// <returns>True if a script parameter was sent; otherwise, false.</returns>
        internal static bool WasSent(string sentScriptParam)
        {
            /* DEVNOTE
             * - Use primeval logs here to write error logs, since logging functionality has not been initialized yet.
             * - This can be simplified, but I'm leaving it for now to make it easier to add logging in the future.
             */

            // TODO - Potentially send an email in addition to the error logs.

            if (string.IsNullOrWhiteSpace(sentScriptParam))
            {
                LogEvent.Primeval($"{DateTime.Now:yyMMdd-HHmmss-fffffff}-ERR1020-MissingScriptParam", SysMsg.ERR1020()[1]);

                return false;
            }
            else
            {
                return true;
            }
        }

        // TODO - Don't use "dynamic`
        ///////// <summary>Finalize the <see cref="OptionObject2015"/> so it can be returned to Avatar.</summary>
        ///////// <remarks>
        ///////// Two things are done here:
        ///////// <list type="number">
        ///////// <item>The <c>working</c> <see cref="OptionObject2015"/> is cloned into the <c>complete</c> object.</item>
        ///////// <item>The <c>complete</c> object is finalized by calling the <c>ToReturnOptionObject</c> ScriptLinkStandard method.</item>
        ///////// </list>
        ///////// </remarks>
        ///////// <param name="twsSession">The current Tingen Web Service session.</param>
        ///////// <param name="errorCode">The error code that is returned with the <see cref="OptionObject2015"/>.</param>
        ///////// <param name="optionObjectErrorMessage">An optional error message that is returned with the <see cref="OptionObject2015"/>.</param>
        ///////// <example>
        ///////// <code>
        ///////// // Return successfully:
        ///////// AvatarOptionObject.ToReturn(tngnWsvcSession, 0);
        /////////
        ///////// // Return with an error:
        ///////// AvatarOptionObject.ToReturn(tngnWsvcSession, 1, "You shall not pass!");
        ///////// </code>
        ///////// </example>
        //////internal static void FinalizeOptionObject(dynamic twsSession, int optionObjectErrorCode, string optionObjectErrorMessage = "")
        //////{
        //////    twsSession.OptObj.Complete = twsSession.OptObj.Worker.Clone();
        //////    twsSession.OptObj.Complete.ToReturnOptionObject(optionObjectErrorCode, optionObjectErrorMessage);
        //////}

        // TODO - Test
        /// <summary>Export a <see cref="OptionObject2015"/> to HTML and JSON files.</summary>
        /// <remarks>
        /// Writes the <paramref name="sentOptObj"/> contents to HTML/JSON files.<br/> <br/> This is primarily used for
        /// troubleshooting.
        /// </remarks>
        /// <param name="sentOptObj">The <see cref="OptionObject2015"/> to export.</param>
        /// <param name="exportPath">The directory path where the exported files will be saved.</param>
        /// <example>
        /// <code>
        /// AvatarOptionObject.ExportOptObj(sentOptionObject, @"C:\Tingen_Data\WebService\%AvatarSystem%\Export\");
        /// // Produces:
        /// //   C:\Tingen_Data\WebService\%AvatarSystem%\Export\OptionObject\20260515_103045.html
        /// //   C:\Tingen_Data\WebService\%AvatarSystem%\Export\OptionObject\20260515_103045.json
        /// </code>
        /// </example>
        internal static void ExportOptObj(OptionObject2015 sentOptObj, string exportPath)
        {
            var dateTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            var htmlVersion = sentOptObj.ToHtmlString(true);
            File.WriteAllText(Path.Combine(exportPath, "OptionObject", $"{dateTime}.html"), htmlVersion);

            var jsonVersion = sentOptObj.ToJson();
            File.WriteAllText(Path.Combine(exportPath, "OptionObject", $"{dateTime}.json"), jsonVersion);
        }
    }
}