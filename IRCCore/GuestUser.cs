using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalIRC.IRCCore
{
    public class GuestUser : User
    {
        private static string GenerateRandomName()
        {
            var counter = 4861;
            return $"Guest{counter}";
        }

        public GuestUser()
            : base(GenerateRandomName())
        {
        }
    }
}
