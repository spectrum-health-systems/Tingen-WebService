// 260806_code
// 260811_documentation

using System;
using System.IO;
using ScriptLinkStandard.Objects;
using TingenWebService.Core.Logger;

namespace TingenWebService.Core.Avatar
{
    /// <summary>Data exchanged between Avatar and the Tingen Web Service.</summary>
    /// <remarks>
    /// <include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="AvatarData"]/AboutAvatarData/*'/>
    /// <include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="AvatarData"]/AboutOptionObjects/*'/>
    /// </remarks>
    internal class AvatarOptionObject
    {
        /// <summary>The original <see cref="OptionObject2015"/> sent from Avatar.</summary>
        /// <remarks>
        /// <include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="AvatarData"]/AboutOptionObjects/*'/>
        /// <include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="AvatarData"]/TypesOfOptionObjects/*'/>
        /// </remarks>
        public OptionObject2015 SentOptionObject { get; set; }

        /// <summary>The worker <see cref="OptionObject2015"/> used during processing.</summary>
        /// <remarks>
        /// <include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="AvatarData"]/AboutOptionObjects/*'/>
        /// <include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="AvatarData"]/TypesOfOptionObjects/*'/>
        /// </remarks>
        public OptionObject2015 WorkerOptionObject { get; set; }

        /// <summary>The complete <see cref="OptionObject2015"/> that is returned to Avatar.</summary>
        /// <remarks>
        /// <include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="AvatarData"]/AboutOptionObjects/*'/>
        /// <include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="AvatarData"]/TypesOfOptionObjects/*'/>
        /// </remarks>
        public OptionObject2015 CompleteOptionObject { get; set; }

        /// <summary>Initialize a new AvatarOptionObject object.</summary>
        /// <param name="sentOptionObject">The <see cref="OptionObject2015"/> sent from Avatar.</param>
        /// <remarks>TBD</remarks>
        /// <returns>A new instance of <see cref="AvatarOptionObject"/>.</returns>
        internal static AvatarOptionObject Build(OptionObject2015 sentOptionObject)
        {
            /* DEVNOTE: This method could be a simple expression-bodied member, for debugging purposes it is written as a full method so
             * that a debug log can created.
             *
             * Do not put logger functionality here, it will cause havoc!
             */

            LogEvent.Primeval("PRELOG-TRACE-AvatarOptionObject-Build");

            // TESTING BLOCK
            var ao = new AvatarOptionObject();
            LogEvent.Primeval("PRELOG-TRACE-AvatarOptionObject-Build1");
            ao.SentOptionObject = sentOptionObject;
            LogEvent.Primeval("PRELOG-TRACE-AvatarOptionObject-Build2");
            ao.WorkerOptionObject = sentOptionObject.Clone();
            LogEvent.Primeval("PRELOG-TRACE-AvatarOptionObject-Build3");
            ao.CompleteOptionObject = null;
            LogEvent.Primeval("PRELOG-TRACE-AvatarOptionObject-Build4");

            return ao;

            //return new AvatarOptionObject
            //{
            //    SentOptionObject      = sentOptionObject,
            //    WorkerOptionObject    = sentOptionObject.Clone(),
            //    CompleteOptionObject  = null
            //};
        }

        /// <summary>Verify whether an <see cref="OptionObject2015"/> was received from Avatar.</summary>
        /// <param name="sentOptionObject">The <see cref="OptionObject2015"/> to verify.</param>
        /// <remarks>TBD</remarks>
        /// <returns>True if an <see cref="OptionObject2015"/> was sent; otherwise, false.</returns>
        internal static bool WasSent(OptionObject2015 sentOptionObject)
        {
            /* DEVNOTE: This method could be a simple expression-bodied member, but it is written as a full method this
             * is a critical error and needs to be logged.
             *
             * Do not put logger functionality here, it will cause havoc!
             */

            LogEvent.Primeval("PRELOG-TRACE-WasSent-OptionObject");

            if (sentOptionObject == null)
            {
                LogEvent.Primeval("ERROR-MissingScriptParameter");
                // TODO - Potentially send an email in addition to the error logs.

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
        internal static void ExportOptObj(OptionObject2015 sentOptObj, string exportPath, int traceLimit, string sessionFolder)
        {
            LogEvent.Trace(1, traceLimit, sessionFolder);

            var dateTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            var htmlVersion = sentOptObj.ToHtmlString(true);
            File.WriteAllText(Path.Combine(exportPath, "OptionObject", $"{dateTime}.html"), htmlVersion);

            var jsonVersion = sentOptObj.ToJson();
            File.WriteAllText(Path.Combine(exportPath, "OptionObject", $"{dateTime}.json"), jsonVersion);
        }
    }
}