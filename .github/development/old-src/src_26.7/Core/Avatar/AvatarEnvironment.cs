// 251112_code
// 260709_documentation

namespace TingenWebService.Core.Avatar
{
    /// <summary>Avatar environment logic.</summary>
    /// <remarks><include file='AppData/XmlDoc/TngnWsvc.xml' path='TngnWsvc/Class[@name="Definition"]/AvatarEnvironment/*'/></remarks>
    internal class AvatarEnvironment
    {
        /// <summary>The Avatar <i>System</i> that will interface with the Tingen Web Service.</summary>
        /// <value>The Avatar system name (example: <c>SBOX</c>, <c>UAT</c>, or <c>LIVE</c>).</value>
        public string AvatarSystem { get; set; }

        /// <summary>The <i>System Code</i> that identifies the Avatar system.</summary>
        /// <value>The Avatar system code (example: <c>LIVE</c>), that often mirrors <see cref="AvatarSystem"/>.</value>
        public string AvatarSystemCode { get; set; }
    }
}