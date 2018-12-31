using UniversalIRC.IRCCore.Protocol;

namespace UniversalIRC.IRCCore
{
    public interface IUser
    {
        event MessageEventHandler<PrivMsgMessage> PrivMsg;
        event MessageEventHandler<NoticeMessage> Notice;

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

        void TriggerPrivMsg(MessageReceivedEventArgs<PrivMsgMessage> args);
        void TriggerNotice(MessageReceivedEventArgs<NoticeMessage> args);
    }
}
