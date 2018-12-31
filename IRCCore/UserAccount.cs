using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalIRC.IRCCore
{
    /// <summary>
    /// IRC user account able to represent the client user.
    /// </summary>
    public class UserAccount : User, IAuthenticate
    {
        public string Password { get; }
        public AuthenticationMethod AuthenticationMethod { get; } = AuthenticationMethod.None;

        public UserAccount(string nickName)
            : base(nickName)
        {
        }

        public UserAccount(string nickName, string userName)
            : base(nickName, userName)
        {
        }

        public UserAccount(string nickName, string password, AuthenticationMethod loginMethod = AuthenticationMethod.ServerPassword, string userName = null)
            : base(nickName, userName)
        {
            Password = password;
            AuthenticationMethod = loginMethod;
        }
    }
}
