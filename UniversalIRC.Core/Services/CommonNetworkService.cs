using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using UniversalIRC.Core.Models;

namespace UniversalIRC.Core.Services
{
    public static class CommonNetworkService
    {
        private static IEnumerable<Network> AllNetworks()
        {
            return new Collection<Network>
            {
                new Network("FreeNode", "irc.freenode.net", "chat.freenode.net"),
                new Network("IRCnet", "open.ircnet.net"),
            };
        }

        public static async Task<IEnumerable<Network>> GetSampleModelDataAsync()
        {
            await Task.CompletedTask;

            return AllNetworks();
        }
    }
}
