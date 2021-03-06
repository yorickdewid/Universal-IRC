﻿using System;

using UniversalIRC.RelayChat.Protocol;

namespace UniversalIRC.RelayChat
{
    public interface INetwork
    {
        /// <summary>
        /// Network server host.
        /// </summary>
        string Host { get; }

        /// <summary>
        /// Network sever port.
        /// </summary>
        int Port { get; }

        bool HasUser { get; }

        IUser User { get; }

        event MessageEventHandler<NoticeMessage> Notice;

        void TriggerNotice(MessageReceivedEventArgs<NoticeMessage> args);
    }
}
