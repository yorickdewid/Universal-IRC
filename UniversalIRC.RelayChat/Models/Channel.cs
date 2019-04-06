using System;
using System.Linq;
using System.Collections.Generic;

using UniversalIRC.RelayChat.Protocol;

namespace UniversalIRC.RelayChat.Models
{
    /// <summary>
    /// IRC channel.
    /// </summary>
    public class Channel : IChannel
    {
        private readonly List<IUser> users = new List<IUser>();

        /// <summary>
        /// Channel name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Channel topic.
        /// </summary>
        public string Topic { get; set; }

        public event MessageEventHandler<PrivMsgMessage> PrivMsg;
        public event MessageEventHandler<NoticeMessage> Notice;
        public event MessageEventHandler<JoinMessage> Join;
        public event MessageEventHandler<PartMessage> Part;
        public event MessageEventHandler<QuitMessage> Quit;

        public void TriggerPrivMsg(MessageReceivedEventArgs<PrivMsgMessage> args) => PrivMsg?.Invoke(args);
        public void TriggerNotice(MessageReceivedEventArgs<NoticeMessage> args) => Notice?.Invoke(args);

        public void TriggerJoin(MessageReceivedEventArgs<JoinMessage> args)
        {
            users.Add(new User(args.Source.Name));
            Join?.Invoke(args);
        }

        /// <summary>
        /// Trigger part event when user was found in this channel.
        /// </summary>
        public void TriggerPart(MessageReceivedEventArgs<PartMessage> args)
        {
            var user = users.FirstOrDefault(s => s.NickName == args.Source.Name);
            if (user != null)
            {
                users.Remove(user);
                Part?.Invoke(args);
            }
        }

        /// <summary>
        /// Trigger quit event when user was found in this channel.
        /// </summary>
        public void TriggerQuit(MessageReceivedEventArgs<QuitMessage> args)
        {
            var user = users.FirstOrDefault(s => s.NickName == args.Source.Name);
            if (user != null)
            {
                users.Remove(user);
                Quit?.Invoke(args);
            }
        }

        public Channel(string name)
        {
            Name = name;
        }
    }
}
