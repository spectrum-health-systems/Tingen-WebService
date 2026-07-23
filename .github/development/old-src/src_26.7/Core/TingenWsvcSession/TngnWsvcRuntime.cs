// 251112_code
// 260515_documentation

using System;

namespace TingenWebService.Core.TingenWsvcSession
{
    /// <summary>Represents the per-session runtime state for the Tingen Web Service.</summary>
    /// <remarks>
    /// <para>
    /// <c>TngnWsvcRuntime</c> captures the values that vary from one Tingen Web Service request to the next: the
    /// session timestamp, the running version, the operating mode, the active Avatar system and user, and the Netsmar
    /// service credentials used to query that system. An instance is built once per session
    /// via <see cref="Load(string, string, string, string, string, string)"/> and then carried on the session object so
    /// downstream components can read consistent runtime information without
    /// re-evaluating <see cref="DateTime.Now"/> or re-parsing inputs.
    /// </para>
    /// </remarks>
    public class TngnWsvcRuntime
    {
        /// <summary>The session date in <c>yyMMdd</c> format.</summary>
        /// <value>The session date in <c>yyMMdd</c> format.</value>
        public string SessionDate { get; set; }

        /// <summary>The session time in <c>HHmmss</c> format.</summary>
        /// <value>The session time in <c>HHmmss</c> format.</value>
        public string SessionTime { get; set; }

        /// <summary>The Tingen Web Service version string for this session.</summary>
        /// <value>The Tingen Web Service version string for this session.</value>
        public string Version { get; set; }

        /// <summary>The operating mode (for example <c>"live"</c>, <c>"uat"</c>, or <c>"sbox"</c>), normalized to lowercase.</summary>
        /// <value>The operating mode (for example <c>"live"</c>, <c>"uat"</c>, or <c>"sbox"</c>), normalized to lowercase.</value>
        public string Mode { get; set; }

        /// <summary>The target Avatar system identifier (for example <c>"LIVE"</c>, <c>"UAT"</c>, or <c>"SBOX"</c>).</summary>
        /// <value>The target Avatar system identifier (for example <c>"LIVE"</c>, <c>"UAT"</c>, or <c>"SBOX"</c>).</value>
        public string AvatarSystem { get; set; }

        /// <summary>The Avatar user id associated with the session, normalized to lowercase.</summary>
        /// <value>The Avatar user id associated with the session, normalized to lowercase.</value>
        public string AvatarUserId { get; set; }

        /// <summary>The Netsmart service account user name used to issue queries against the Avatar system.</summary>
        /// <value>The Netsmart service account user name used to issue queries against the Avatar system.</value>
        public string NtstWsvcUserName { get; set; }

        /// <summary>The Netsmart service account password used to issue queries against the Avatar system.</summary>
        /// <value>The Netsmart service account password used to issue queries against the Avatar system.</value>
        public string NtstWsvcUserPass { get; set; }

        /// <summary>The in-memory running log accumulated over the lifetime of the session.</summary>
        /// <value>The in-memory running log accumulated over the lifetime of the session.</value>
        public string RunningLog { get; set; }

        /// <summary>Creates and initializes a <see cref="TngnWsvcRuntime"/> instance for the current session.</summary>
        /// <param name="version">The Tingen Web Service version string for this session.</param>
        /// <param name="mode">The operating mode for the session; the value is stored in lowercase.</param>
        /// <param name="avatarSystem">The target Avatar system identifier (for example <c>"LIVE"</c>, <c>"UAT"</c>, or <c>"SBOX"</c>).</param>
        /// <param name="avatarUserId">The Avatar user id for the session; the value is stored in lowercase.</param>
        /// <param name="ntstWsvcUserName">The Netsmart service account user name used for Avatar queries.</param>
        /// <param name="ntstWsvcUserPass">The Netsmart service account password used for Avatar queries.</param>
        /// <returns>A fully populated <see cref="TngnWsvcRuntime"/> with the session date and time captured from <see cref="DateTime.Now"/>.</returns>
        /// <remarks>
        /// <para>
        /// <see cref="SessionDate"/> and <see cref="SessionTime"/> are sampled at construction so every component that
        /// inspects the runtime sees the same timestamp for the duration of the session. <see cref="RunningLog"/> is
        /// initialized to an empty string and is intended to be appended to as the session progresses.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// var runtime = TngnWsvcRuntime.Load(
        ///     version:          "1.0.0",
        ///     mode:             "LIVE",
        ///     avatarSystem:     "LIVE",
        ///     avatarUserId:     "JDOE",
        ///     ntstWsvcUserName: "tngn_svc",
        ///     ntstWsvcUserPass: "********");
        /// </code>
        /// </example>
        internal static TngnWsvcRuntime Load(string version, string mode, string avatarSystem, string avatarUserId, string ntstWsvcUserName, string ntstWsvcUserPass) =>
            new TngnWsvcRuntime()
            {
                SessionDate         = DateTime.Now.ToString("yyMMdd"),
                SessionTime         = DateTime.Now.ToString("HHmmss"),
                Version             = version,
                Mode                = mode.ToLower(),
                AvatarSystem        = avatarSystem,
                AvatarUserId        = avatarUserId.ToLower(),
                RunningLog          = "",
                NtstWsvcUserName    = ntstWsvcUserName,
                NtstWsvcUserPass    = ntstWsvcUserPass
            };
    }
}