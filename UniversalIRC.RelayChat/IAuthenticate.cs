using System;

namespace UniversalIRC.RelayChat
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
