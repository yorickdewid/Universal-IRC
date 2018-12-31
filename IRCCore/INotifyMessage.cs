using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalIRC.IRCCore
{
    public interface INotifyMessage
    {
        event EventHandler PrivMsg;
        event EventHandler Join;
        event EventHandler Part;
        event EventHandler Quit;
    }
}
