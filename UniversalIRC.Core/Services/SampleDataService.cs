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
        private static IEnumerable<ChatRoom> AllChannels()
        {
            // The following is chat room example data
            return new Collection<ChatRoom>
            {
                new ChatRoom
                {
                    Name = "#linux",
                    Symbol = (char)59158,
                    ChatHistory = new Collection<ChatMessage>
                    {
                        new ChatMessage{ Message = "New kernel looks awesome" },
                        new ChatMessage{ Message = "Yes, just made the last commit" },
                        new ChatMessage{ Message = "Its a lot faster too.." },
                    }
                },
                new ChatRoom
                {
                    Name = "#python-unregistered",
                    Symbol = (char)59158
                },
                new ChatRoom
                {
                    Name = "#postgres",
                    Symbol = (char)59158
                },
                new ChatRoom
                {
                    Name = "Torvalds",
                    Symbol = (char)57661,
                    ChatHistory = new Collection<ChatMessage>
                    {
                        new ChatMessage{ Message = "Hi there, how are you doing?" },
                        new ChatMessage{ Message = "I am testing" },
                    }
                },
                new ChatRoom
                {
                    Name = "#debian",
                    Symbol = (char)59158,
                    ChatHistory = new Collection<ChatMessage>
                    {
                        new ChatMessage{ Message = "When is the next debian release?" },
                        new ChatMessage{ Message = "Somwhere next month" },
                    }
                },
            };
        }

        public static async Task<IEnumerable<ChatRoom>> GetSampleModelDataAsync()
        {
            await Task.CompletedTask;

            return AllChannels();
        }
    }
}
