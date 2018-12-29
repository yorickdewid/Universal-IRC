using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalIRC.IRCCore
{
    /// <summary>
    /// Represents an IRC user.
    /// </summary>
    public class User
    {
        public string UserName { get; set; }
        public string RealName { get; set; }
        public string NickName { get; set; }
        public string NickNameSecond { get; set; }
        public string NickNameThird { get; set; }
        public string Password { get; set; }
        UserAuthenticationMethod LoginMethod { get; set; } = UserAuthenticationMethod.None;

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
            LoginMethod = LoginMethod;
        }

        public override string ToString()
        {
            return UserName;
        }
    }
}
