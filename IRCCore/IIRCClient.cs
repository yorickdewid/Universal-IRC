using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalIRC.IRCCore.Connection;
using UniversalIRC.IRCCore.Protocol;

namespace UniversalIRC.IRCCore
{
    public delegate void MessageEventHandler<T>(MessageReceivedEventArgs<T> e)
        where T : AbstractMessage;

    public interface IIRCClient : IDisposable
    {
        IConnection Connection { get; }
        bool IsConnected { get; }
    }
}
