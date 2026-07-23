// 260601_code
// 260709_documentation

using System.IO;
using System.Reflection;
using ScriptLinkStandard.Objects;

namespace TingenWebService.Core.Avatar
{
    /// <summary> Avatar <see cref="OptionObject2015"> OptionObject</see> logic.</summary>
    /// <remarks><include file='AppData/XmlDoc/TngnWsvc.xml' path='TngnWsvc/Class[@name="Definition"]/OptionObject/*'/></remarks>
    public class AvatarOptionObject
    {
        /// <summary> The name of the executing assembly, used for logging purposes.</summary>
        public static string ExeAsm { get; set; } = Assembly.GetExecutingAssembly().GetName().Name;

        /// <summary>The original <see cref="OptionObject2015"/> received from Avatar.</summary>
        /// <remarks>This is <i>never</i> modified by the Tingen Web Service.</remarks>
        public OptionObject2015 Original { get; set; }

        /// <summary>The working copy of the <see cref="OptionObject2015"/> used during processing.</summary>
        /// <remarks>This is <i>potentially</i> modified during processing.</remarks>
        public OptionObject2015 Worker { get; set; }

        /// <summary>The completed <see cref="OptionObject2015"/> ready to be returned to Avatar.</summary>
        /// <remarks>This is the <i>final</i> version of the <see cref="OptionObject2015"/> that is returned to Avatar.</remarks>
        public OptionObject2015 Completed { get; set; }

        /// <summary>Verify whether an <see cref="OptionObject2015"/> was received from Avatar.</summary>
        /// <param name="origOptObj">The <see cref="OptionObject2015"/> to verify.</param>
        /// <returns>A message indicating whether an <see cref="OptionObject2015"/> was sent.</returns>
        public static string VerifyExistence(OptionObject2015 origOptObj)
        {
            return (origOptObj == null)
                ? "An OptionObject was not sent."
                : "An OptionObject was sent.";
        }

        /// <summary>Finalize the <see cref="OptionObject2015"/> so it can be returned to Avatar.</summary>
        /// <remarks>
        /// Two things are done here:
        /// <list type="number">
        /// <item>The <c>working</c> <see cref="OptionObject2015"/> is cloned into the <c>completed</c> object.</item>
        /// <item>The <c>completed</c> object is finalized by calling the <c>ToReturnOptionObject</c> ScriptLinkStandard method.</item>
        /// </list>
        /// </remarks>
        /// <param name="tngnWsvcSession">The current Tingen Web Service session.</param>
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
        public static void ToReturn(dynamic tngnWsvcSession, int errCode, string errMsg = "") //TODO rename this to something more descriptive
        {
            tngnWsvcSession.OptObj.Completed = tngnWsvcSession.OptObj.Worker.Clone();
            tngnWsvcSession.OptObj.Completed.ToReturnOptionObject(errCode, errMsg);
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
        public static void ExportOptObj(OptionObject2015 origOptObj, string exportPath) //TODO rename this to something more descriptive (e.g., ExportOptionObject)
        {
            var dateTime    = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var htmlVersion = origOptObj.ToHtmlString(true);

            File.WriteAllText($@"{exportPath}\\exported-optionobject-{dateTime}.html", htmlVersion);

            var jsonVersion = origOptObj.ToJson();

            File.WriteAllText($@"{exportPath}\\exported-optionobject-{dateTime}.json", jsonVersion);
        }
    }
}