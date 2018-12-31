using UniversalIRC.IRCCore.Protocol;

namespace UniversalIRC.IRCCore
{
    public interface IChannel
    {
        event MessageEventHandler<PrivMsgMessage> PrivMsg;
        event MessageEventHandler<NoticeMessage> Notice;
        event MessageEventHandler<JoinMessage> Join;
        event MessageEventHandler<PartMessage> Part;
        event MessageEventHandler<QuitMessage> Quit;

        string Name { get; }

        void TriggerPrivMsg(MessageReceivedEventArgs<PrivMsgMessage> args);
        void TriggerNotice(MessageReceivedEventArgs<NoticeMessage> args);
        void TriggerJoin(MessageReceivedEventArgs<JoinMessage> args);
        void TriggerPart(MessageReceivedEventArgs<PartMessage> args);
        void TriggerQuit(MessageReceivedEventArgs<QuitMessage> args);
    }
}
