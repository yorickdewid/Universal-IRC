using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalIRC.IRCCore
{
    public interface IAuthenticate
    {
        /// <summary>
        /// Network nickname.
        /// </summary>
        string NickName { get; }

        /// <summary>
        /// Authentication password.
        /// </summary>
        string Password { get; }

        /// <summary>
        /// Authentication method.
        /// </summary>
        AuthenticationMethod AuthenticationMethod { get; }
    }
}
