using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalIRC.IRCCore
{
    /// <summary>
    /// IRC user.
    /// </summary>
    public class User : IUser, ICanAuthenticate
    {
        public string UserName { get; set; }
        public string RealName { get; set; }
        public string NickName { get; set; }
        public string NickNameSecond { get; set; }
        public string NickNameThird { get; set; }
        public string Password { get; }
        public UserAuthenticationMethod AuthenticationMethod { get; } = UserAuthenticationMethod.None;

        public string Name { get => NickName; set => NickName = value; }

        public User(string userName)
        {
            UserName = userName;
            RealName = userName;
            NickName = userName;
        }

        public User(string userName, string password, UserAuthenticationMethod loginMethod = UserAuthenticationMethod.ServerPassword)
        {
            UserName = userName;
            RealName = userName;
            NickName = userName;
            Password = password;
            AuthenticationMethod = loginMethod;
        }

        public override string ToString()
        {
            return UserName;
        }
    }
}
