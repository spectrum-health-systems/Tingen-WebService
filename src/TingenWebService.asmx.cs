// 260723_code
// 260723_documentation

using System.Reflection;
using System.Web.Services;
using ScriptLinkStandard.Objects;
using TingenWebService.Core;
using TingenWebService.Properties;

namespace TingenWebService
{
    /// <summary>The entry class for the Tingen Web Service.</summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class CustomAvatarWebService : System.Web.Services.WebService
    {
        /// <summary>Current Tingen Web Service release.</summary>
        /// <remarks>To update the version number, modify the AssemblyInfo.cs file.</remarks>
        public static string TwsRelease { get; set; } = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        /// <summary>Returns the current Tingen Web Service release.</summary>
        [WebMethod]
        public string GetVersion() => $"VERSION {TwsRelease}";

        [WebMethod]
        internal OptionObject2015 RunScript(OptionObject2015 sentOptObj, string sentScriptParam)
        {
            //TODO - Add a primeval log event here that is normally commented out.

            if (IsMissingAvatarData(sentOptObj, sentScriptParam))
            {
                return sentOptObj.ToReturnOptionObject(0, "");  //TODO - should there be an error message here?
            }
            else
            {
                Framework twsFramework = Framework.Load(Settings.Default.HostDataRoot, Settings.Default.HostWwwRoot);
                Framework.Verify(twsFramework);

                //TngnWsvcSession twsSession = TngnWsvcSession.Load(sentOptObj, sentScriptParam);

                return sentOptObj; //TODO - Placeholder
            }
        }

        /// <summary>Determine if the Avatar data is missing based.</summary>
        /// <param name="sentOptObj">The OptionObject sent from Avatar.</param>
        /// <param name="sentScriptParam">The Script Parameter sent from Avatar.</param>
        /// <returns><c>True</c> if the avatar data is missing; otherwise, <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// var result = IsMissingAvatarData(optionObject, scriptParam);
        /// </code>
        /// </example>
        private static bool IsMissingAvatarData(OptionObject2015 sentOptObj, string sentScriptParam)
        {
            if (sentOptObj == null || string.IsNullOrWhiteSpace(sentScriptParam))
            {
                //TODO - Log this, and potentially send an email.

                return true;
            }

            return false;
        }
    }
}