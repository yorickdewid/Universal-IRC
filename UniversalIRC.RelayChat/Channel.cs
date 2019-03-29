using System;

using UniversalIRC.RelayChat.Protocol;

namespace UniversalIRC.RelayChat
{
    /// <summary>
    /// IRC channel.
    /// </summary>
    public class Channel : IChannel
    {
        public string Name { get; }

        public event MessageEventHandler<PrivMsgMessage> PrivMsg;
        public event MessageEventHandler<NoticeMessage> Notice;
        public event MessageEventHandler<JoinMessage> Join;
        public event MessageEventHandler<PartMessage> Part;
        public event MessageEventHandler<QuitMessage> Quit;

        public void TriggerPrivMsg(MessageReceivedEventArgs<PrivMsgMessage> args) => PrivMsg?.Invoke(args);
        public void TriggerNotice(MessageReceivedEventArgs<NoticeMessage> args) => Notice?.Invoke(args);
        public void TriggerJoin(MessageReceivedEventArgs<JoinMessage> args) => Join?.Invoke(args);
        public void TriggerPart(MessageReceivedEventArgs<PartMessage> args) => Part?.Invoke(args);
        public void TriggerQuit(MessageReceivedEventArgs<QuitMessage> args) => Quit?.Invoke(args);

        public Channel(string name)
        {
            Name = name;
        }
    }
}
