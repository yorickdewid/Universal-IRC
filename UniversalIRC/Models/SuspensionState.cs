using System;

namespace UniversalIRC.Models
{
    public class SuspensionState
    {
        public object Data { get; set; }

        public DateTime SuspensionDate { get; set; } = DateTime.Now;
    }
}
