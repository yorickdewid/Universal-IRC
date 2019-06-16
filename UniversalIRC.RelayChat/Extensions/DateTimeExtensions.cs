using System;

namespace UniversalIRC.RelayChat.Extensions
{
    public static class DateTimeExtensions
    {
        public static string AsCTime(this DateTime dateTime)
        {
            return dateTime.ToString("ddd, dd MMM yyy HH:mm:ss 'GMT'");
        }
    }
}
