using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UniversalIRC.RelayChat.Protocol;

namespace UniversalIRC.RelayChat.Models
{
    /// <summary>
    /// IRC User entity. This entity can be
    /// notified about incommng IRC messages.
    /// </summary>
    public class User : IUser
    {
        public string UserName { get; protected set; }
        public string NickName { get; protected set; }
        public string NickNameSecond { get; protected set; }
        public string NickNameThird { get; protected set; }
        public string Host { get; protected set; }

        public event MessageEventHandler<PrivMsgMessage> PrivMsg;
        public event MessageEventHandler<NoticeMessage> Notice;

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

        public void TriggerPrivMsg(MessageReceivedEventArgs<PrivMsgMessage> args) => PrivMsg?.Invoke(args);
        public void TriggerNotice(MessageReceivedEventArgs<NoticeMessage> args) => Notice?.Invoke(args);

        public override string ToString()
        {
            return UserName ?? NickName;
        }
    }
}
