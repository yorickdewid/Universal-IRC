using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using UniversalIRC.Core.Models;

namespace UniversalIRC.Core.Services
{
    // This class holds sample data used by some generated pages to show how they can be used.
    public static class SampleDataService
    {
        private static IEnumerable<Chat> AllChannels()
        {
            // The following is chat room example data
            return new Collection<Chat>
            {
                new Network("FreeNode", "irc.freenode.net")
                {
                    ChatHistory = new Collection<ChatMessage>
                    {
                        new ChatMessage{ Message = "tolkien.freenode.net Message of the Day - " },
                        new ChatMessage{ Message = "Welcome to tolkien.freenode.net in Sanford, NC, US. Thanks to" },
                        new ChatMessage{ Message = "https://travelingmailbox.com/ for sponsoring this server" },
                        new ChatMessage{ Message = "Welcome to freenode - supporting the free and open source" },
                        new ChatMessage{ Message = "software communities since 1998." },
                    }
                },
                new Channel("#linux")
                {
                    ChatHistory = new Collection<ChatMessage>
                    {
                        new ChatMessage{ Sender = "Someuser", Message = "New kernel looks awesome" },
                        new ChatMessage{ Sender = "User22", Message = "Yes, just made the last commit" },
                        new ChatMessage{ Sender = "Linux2User", Message = "Its a lot faster too.." },
                    }
                },
                new Channel("#python-unregistered"),
                new Channel("#postgres"),
                new Private("Torvalds")
                {
                    ChatHistory = new Collection<ChatMessage>
                    {
                        new ChatMessage{ Message = "Hi there, how are you doing?" },
                        new ChatMessage{ Message = "I am testing" },
                    }
                },
                new Channel("#debian")
                {
                    ChatHistory = new Collection<ChatMessage>
                    {
                        new ChatMessage{ Message = "When is the next debian release?" },
                        new ChatMessage{ Message = "Somwhere next month" },
                    }
                },
            };
        }

        public static async Task<IEnumerable<Chat>> GetSampleModelDataAsync()
        {
            await Task.CompletedTask;

            return AllChannels();
        }
    }
}
