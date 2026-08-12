// 260812_code
// 260812_documentation

using System;
using System.IO;
using ScriptLinkStandard.Objects;
using TingenWebService.Core.Logger;
using TingenWebService.Core.Session;

namespace TingenWebService.Core.Avatar
{
    /// <summary>Objects and logic related to Avatar <see cref="OptionObject2015">OptionObjects</see></summary>
    /// <remarks>
    /// <include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="AvatarData"]/AboutAvatarData/*'/> <br/>
    /// This class focuses on <b>OptionObjects</b>.
    /// <include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="AvatarData"]/AboutOptionObjects/*'/>
    /// </remarks>
    internal class AvatarOptionObject
    {
        /// <summary>The <b>original</b> <see cref="AvatarOptionObject">OptionObject</see> <b>sent</b> from Avatar.</summary>
        /// <remarks>
        /// <include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="AvatarData"]/TypesOfOptionObjects/*'/>
        /// </remarks>
        public OptionObject2015 SentOptionObject { get; set; }

        /// <summary>The <b>worker</b> <see cref="AvatarOptionObject">OptionObject</see> used during processing.</summary>
        /// <remarks>
        /// <include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="AvatarData"]/TypesOfOptionObjects/*'/>
        /// </remarks>
        public OptionObject2015 WorkerOptionObject { get; set; }

        /// <summary>The <b>complete</b> <see cref="AvatarOptionObject">OptionObject</see>.</summary>
        /// <remarks>
        /// <include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="AvatarData"]/TypesOfOptionObjects/*'/>
        /// </remarks>
        public OptionObject2015 CompleteOptionObject { get; set; }

        /// <summary>Initialize a new <see cref="AvatarOptionObject"/>.</summary>
        /// <param name="sentOptionObject">The <see cref="SentOptionObject">OptionObject</see> sent from Avatar.</param>
        /// <remarks>
        /// The <see cref="AvatarOptionObject"/> instance bundles all of the necessary OptionObjects that the Tingen Web
        /// Service needs to process a request, all in a cute little package that laughs when tickled.
        /// </remarks>
        /// <returns>A new instance of <see cref="AvatarOptionObject"/>.</returns>
        internal static AvatarOptionObject Build(OptionObject2015 sentOptionObject)
        {
            /* DEVNOTE: This method could be a simple expression-bodied member, for debugging purposes it is written as a full method so
             * that a debug log can created.
             *
             * Do not put trace logs here, it will cause havoc!
             */

            //LogEvent.Primeval("PRELOG-TRACE-AvatarOptionObject-Build");

            return new AvatarOptionObject
            {
                SentOptionObject     = sentOptionObject,
                WorkerOptionObject   = sentOptionObject.Clone(),
                CompleteOptionObject = null
            };
        }

        /// <summary>Verify whether the <see cref="SentOptionObject">OptionObject</see> was sent from Avatar.</summary>
        /// <param name="sentOptionObject">The <see cref="SentOptionObject">OptionObject</see> to verify.</param>
        /// <remarks>
        /// <para>The <see cref="AvatarOptionObject">sent OptionObject</see> is required, so if Avatar didn't send one, it's game over,
        /// man! <i>It's game over!</i><br/>
        /// <br/>
        /// And by "game over", I mean that the Tingen Web Service will log an error and return an unmodified
        /// <see cref="AvatarOptionObject">OptionObject</see> back to Avatar.
        /// </para>
        /// </remarks>
        /// <returns><c>True</c> if an <see cref="AvatarOptionObject">OptionObject</see> was sent; otherwise, <c>false</c>.</returns>
        internal static bool WasSent(OptionObject2015 sentOptionObject)
        {
            /* DEVNOTE: This method could be a simplified but it is written as is because this is a critical error and needs to be logged.
             *
             * Do not put trace logs here, it will cause havoc!
             */

            //LogEvent.Primeval("PRELOG-TRACE-WasSent-OptionObject");

            if (sentOptionObject == null)
            {
                LogEvent.Primeval("ERROR-MissingScriptParameter");
                // TODO: Potentially send an email in addition to the error logs.

                return false;
            }
            else
            {
                return true;
            }
        }

        // TODO:Don't use "dynamic`
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

        /// <summary>Export a <see cref="OptionObject2015"/> as HTML and JSON formatted data.</summary>
        /// <remarks>This is primarily used for troubleshooting.</remarks>
        /// <param name="sentOptionObject">The <see cref="AvatarOptionObject">OptionObject</see> sent from Avatar. </param>
        /// <param name="exportPath">The directory path where the exported files will be saved.</param>
        /// <example>
        /// <code>
        /// AvatarOptionObject.ExportOptObj(sentOptionObject, @"C:\Tingen_Data\WebService\%AvatarSystem%\Export\");
        /// </code>
        /// Exports:
        /// C:\Tingen_Data\WebService\%AvatarSystem%\Export\OptionObject\20260515_103045.html
        /// C:\Tingen_Data\WebService\%AvatarSystem%\Export\OptionObject\20260515_103045.json
        /// </example>
        internal static void ExportOptionObject(OptionObject2015 sentOptionObject, string exportPath, Tracer trc)
        {
            //TODO: Test this functionality.

            LogEvent.Trace(1, trc.Lmt, trc.Fld);

            var todayDateTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            File.WriteAllText(Path.Combine(exportPath, "OptionObject", $"{todayDateTime}.html"), sentOptionObject.ToHtmlString(true));

            File.WriteAllText(Path.Combine(exportPath, "OptionObject", $"{todayDateTime}.json"), sentOptionObject.ToJson());
        }
    }
}