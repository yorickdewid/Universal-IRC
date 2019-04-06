using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using UniversalIRC.Core.Models;

namespace UniversalIRC.Core.Services
{
    /// <summary>
    /// Popular IRC networks, which can accelerate user interaction.
    /// </summary>
    public static class CommonNetworkService
    {
        private static IEnumerable<Network> AllNetworks()
        {
            return new Collection<Network>
            {
                new Network("FreeNode", "irc.freenode.net", null),
                new Network("IRCnet", "open.ircnet.net", null),
            };
        }

        public static async Task<IEnumerable<Network>> GetSampleModelDataAsync()
        {
            await Task.CompletedTask;

            return AllNetworks();
        }
    }
}
