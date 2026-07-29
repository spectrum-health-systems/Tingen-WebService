// 260729_code
// 260729_documentation

using System.IO;
using ScriptLinkStandard.Objects;
using TingenWebService.Core.Logger;
using TingenWebService.Core.Trove;

namespace TingenWebService.Core.Avatar
{
    /// <summary>Represents the data sent from Avatar to the Tingen Web Service.</summary>
    /// <remarks>TBD</remarks>
    internal class AvatarData
    {
        #region Properties

        /// <summary>The original <see cref="OptionObject2015"/> sent from Avatar.</summary>
        /// <remarks>This is <i>never</i> modified by the Tingen Web Service.</remarks>
        public OptionObject2015 SentOptionObject { get; set; }

        /// <summary>The worker <see cref="OptionObject2015"/> used during processing.</summary>
        /// <remarks>This is <i>potentially</i> modified during processing.</remarks>
        public OptionObject2015 WorkerOptionObject { get; set; }

        /// <summary>The complete <see cref="OptionObject2015"/> ready to be returned to Avatar.</summary>
        /// <remarks>This is the <i>final</i> version of the <see cref="OptionObject2015"/> that is returned to Avatar.</remarks>
        public OptionObject2015 CompleteOptionObject { get; set; }

        /// <summary>The original script parameter sent from Avatar.</summary>
        /// <remarks>TBD</remarks>
        public string SentScriptParameter { get; set; }

        #endregion Properties

        /// <summary>Initialize a new AvatarData object.</summary>
        /// <param name="sentOptionObject">The <see cref="OptionObject2015"/> sent from Avatar.</param>
        /// <remarks>
        /// TBD
        /// </remarks>
        /// <returns>A new instance of <see cref="AvatarData"/>.</returns>
        internal static AvatarData Initialize(OptionObject2015 sentOptionObject, string sentScriptParameter)
        {
            return new AvatarData
            {
                SentOptionObject     = sentOptionObject,
                WorkerOptionObject   = sentOptionObject.Clone(),
                CompleteOptionObject = null,
                SentScriptParameter  = sentScriptParameter
            };
        }

        /*
         * Verifiers
         */

        /// <summary>Verify whether an <see cref="OptionObject2015"/> was received from Avatar.</summary>
        /// <param name="sentOptionObject">The <see cref="OptionObject2015"/> to verify.</param>
        /// <returns>True if an <see cref="OptionObject2015"/> was sent; otherwise, false.</returns>
        internal static bool WasSent(OptionObject2015 sentOptionObject)
        {
            /* DEVNOTE
             * - Use primeval logs here to write error logs, since logging functionality has not been initialized yet.
             * - This can be simplified, but I'm leaving it for now to make it easier to add logging in the future.
             */

            // TODO - Potentially send an email in addition to the error logs.

            if (sentOptionObject == null)
            {
                LogEvent.Primeval("ERR-MissingOptionObject", SysMsg.ERR1010()[1]);

                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>Verify whether a script parameter was received from Avatar.</summary>
        /// <param name="sentScriptParameter">The script parameter to verify.</param>
        /// <returns>True if a script parameter was sent; otherwise, false.</returns>
        public static bool WasSent(string sentScriptParameter)
        {
            /* DEVNOTE
             * - Use primeval logs here to write error logs, since logging functionality has not been initialized yet.
             * - This can be simplified, but I'm leaving it for now to make it easier to add logging in the future.
             */

            // TODO - Potentially send an email in addition to the error logs.

            if (string.IsNullOrWhiteSpace(sentScriptParameter))
            {
                LogEvent.Primeval("ERR-MissingScriptParameter", SysMsg.ERR1020()[1]);

                return false;
            }
            else
            {
                return true;
            }
        }



        /// <summary>Finalize the <see cref="OptionObject2015"/> so it can be returned to Avatar.</summary>
        /// <remarks>
        /// Two things are done here:
        /// <list type="number">
        /// <item>The <c>working</c> <see cref="OptionObject2015"/> is cloned into the <c>complete</c> object.</item>
        /// <item>The <c>complete</c> object is finalized by calling the <c>ToReturnOptionObject</c> ScriptLinkStandard method.</item>
        /// </list>
        /// </remarks>
        /// <param name="twsSession">The current Tingen Web Service session.</param>
        /// <param name="errCode">The error code that is returned with the <see cref="OptionObject2015"/>.</param>
        /// <param name="errMsg">An optional error message that is returned with the <see cref="OptionObject2015"/>.</param>
        /// <example>
        /// <code>
        /// // Return successfully:
        /// AvatarOptionObject.ToReturn(tngnWsvcSession, 0);
        ///
        /// // Return with an error:
        /// AvatarOptionObject.ToReturn(tngnWsvcSession, 1, "You shall not pass!");
        /// </code>
        /// </example>
        internal static void ToReturn(dynamic twsSession, int errCode, string errMsg = "") //TODO rename this to something more descriptive
        {
            twsSession.OptObj.Complete = twsSession.OptObj.Worker.Clone();
            twsSession.OptObj.Complete.ToReturnOptionObject(errCode, errMsg);
        }

        /// <summary>Export a <see cref="OptionObject2015"/> to HTML and JSON files.</summary>
        /// <remarks>
        /// Writes the <paramref name="origOptObj"/> contents to HTML/JSON files.<br/>
        /// <br/>
        /// This is primarily used for troubleshooting.
        /// </remarks>
        /// <param name="origOptObj">The <see cref="OptionObject2015"/> to export.</param>
        /// <param name="exportPath">The directory path where the exported files will be saved.</param>
        /// <example>
        /// <code>
        /// AvatarOptionObject.ExportOptObj(sentOptionObject, @"C:\Tingen_Data\exports");
        /// // Produces:
        /// //   C:\Tingen_Data\exports\exported-optionobject-20260515_103045.html
        /// //   C:\Tingen_Data\exports\exported-optionobject-20260515_103045.json
        /// </code>
        /// </example>
        internal static void Export(OptionObject2015 origOptObj, string exportPath) //TODO rename this to something more descriptive (e.g., ExportOptionObject)
        {
            var dateTime    = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var htmlVersion = origOptObj.ToHtmlString(true);

            File.WriteAllText($@"{exportPath}\\exported-optionobject-{dateTime}.html", htmlVersion);

            var jsonVersion = origOptObj.ToJson();

            File.WriteAllText($@"{exportPath}\\exported-optionobject-{dateTime}.json", jsonVersion);
        }
    }
}