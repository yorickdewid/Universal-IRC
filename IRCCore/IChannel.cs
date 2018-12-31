using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalIRC.IRCCore
{
    public interface IChannel : INotifyMessage
    {
        string Name { get; }
    }
}
