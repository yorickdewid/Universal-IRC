using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalIRC.RelayChat.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Add CR+LF if the string does not end on CR+LF.
        /// </summary>
        public static string EnsureCRLF(this string str)
        {
            const string crlf = "\r\n";

            if (!str.EndsWith(crlf))
            {
                str += crlf;
            }

            return str;
        }
    }
}
