using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalIRC.IRCCore
{
    /// <summary>
    /// IRC User entity. This entity can be
    /// notified about incommng IRC messages.
    /// </summary>
    public class User : IUser, INotifyMessage
    {
        public string UserName { get; protected set; }
        public string NickName { get; protected set; }
        public string NickNameSecond { get; protected set; }
        public string NickNameThird { get; protected set; }
        public string Host { get; protected set; }

        public event EventHandler PrivMsg;
        public event EventHandler Join;
        public event EventHandler Part;
        public event EventHandler Quit;

        public User(string nickName)
        {
            NickName = nickName;
            UserName = nickName;
        }

        public User(string nickName, string userName)
        {
            NickName = nickName;
            UserName = userName;
        }

        public override string ToString()
        {
            return UserName ?? NickName;
        }
    }
}
