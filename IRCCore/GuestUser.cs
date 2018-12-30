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
            Random random = new Random();
            return $"Guest{random.Next(1000, 9999)}";
        }

        public GuestUser()
            : base(GenerateRandomName())
        {
        }
    }
}
