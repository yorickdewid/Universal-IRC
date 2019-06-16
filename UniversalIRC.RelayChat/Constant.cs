using System;
using System.Reflection;

namespace UniversalIRC.RelayChat
{
    public static class Constant
    {
        /// <summary>
        /// Retrieve application version.
        /// </summary>
        public static Version ApplicationVersion => Assembly.GetExecutingAssembly().GetName().Version;

        /// <summary>
        /// Retrieve application name.
        /// </summary>
        public static string ApplicationName => Assembly.GetExecutingAssembly().GetName().Name;

        /// <summary>
        /// Get application version string.
        /// </summary>
        public static string ApplicationVersionString => $"{ApplicationName} {ApplicationVersion.ToString()}";
    }
}
