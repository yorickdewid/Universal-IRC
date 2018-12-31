using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalIRC.IRCCore
{
    public interface IUser : INotifyMessage
    {
        /// <summary>
        /// Network nickname.
        /// </summary>
        string NickName { get; }
        
        /// <summary>
        /// Users full name.
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Address/host of user.
        /// </summary>
        string Host { get; }
    }
}
