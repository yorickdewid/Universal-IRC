using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalIRC.IRCCore.Connection
{
    /// <summary>
    /// Provides the data received from a connection
    /// </summary>
    public class DataReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// Data received from the connection
        /// </summary>
        public string Data { get; }

        /// <summary>
        /// Initializes a new instance of DataReceivedEventArgs.
        /// </summary>
        /// <param name="data">Data received as string.</param>
        public DataReceivedEventArgs(string data)
        {
            Data = data;
        }
    }
}
