using System;

using UniversalIRC.RelayChat.Models;

namespace UniversalIRC.Core.Models
{
    public class ChatUserAccount : UserAccount
    {
        public ChatUserAccount(string nickName)
            : base(nickName)
        {
        }

        public ChatUserAccount(string nickName, string userName)
            : base(nickName, userName)
        {
        }
    }
}
