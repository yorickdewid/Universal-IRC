using System;
using System.Collections.Generic;
using System.Text;

using UniversalIRC.RelayChat.Protocol;

namespace UniversalIRC.RelayChat
{
    public interface IChannel
    {
        /// <summary>
        /// Channel name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Channel topic.
        /// </summary>
        string Topic { get; set; }

        event MessageEventHandler<PrivMsgMessage> PrivMsg;
        event MessageEventHandler<NoticeMessage> Notice;
        event MessageEventHandler<JoinMessage> Join;
        event MessageEventHandler<PartMessage> Part;
        event MessageEventHandler<QuitMessage> Quit;

        void TriggerPrivMsg(MessageReceivedEventArgs<PrivMsgMessage> args);
        void TriggerNotice(MessageReceivedEventArgs<NoticeMessage> args);
        void TriggerJoin(MessageReceivedEventArgs<JoinMessage> args);
        void TriggerPart(MessageReceivedEventArgs<PartMessage> args);
        void TriggerQuit(MessageReceivedEventArgs<QuitMessage> args);
    }
}
