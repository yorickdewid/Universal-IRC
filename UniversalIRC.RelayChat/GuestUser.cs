using System;

namespace UniversalIRC.RelayChat
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
