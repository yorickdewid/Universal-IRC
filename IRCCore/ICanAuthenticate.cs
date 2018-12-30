using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalIRC.IRCCore
{
    public interface ICanAuthenticate
    {
        string Name { get; set; }
        string Password { get; }
        UserAuthenticationMethod AuthenticationMethod { get; }
    }
}
