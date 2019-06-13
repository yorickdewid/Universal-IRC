using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalIRC.Core.Services
{
    public static class CommandParserService
    {
        public sealed class Command
        {
            public string Name { get; set; }
            public List<Action<string[]>> Actions { get; set; }
        }

        private static readonly Command[] commands = new Command[]
        {
            new Command{ Name = "join" },
            new Command{ Name = "part" },
            new Command{ Name = "quit" },
            new Command{ Name = "server" },
            new Command{ Name = "list" },
            new Command{ Name = "links" },
            new Command{ Name = "nick" },
            new Command{ Name = "names" },
            new Command{ Name = "msg" },
            new Command{ Name = "query" },
            new Command{ Name = "me" },
            new Command{ Name = "notice" },
            new Command{ Name = "whois" },
            new Command{ Name = "whowas" },
            new Command{ Name = "dns" },
            new Command{ Name = "ping" },
        };

        public static void SubscribeCommand(string _command, Action<string[]> actionDelegate)
        {
            foreach (var command in commands)
            {
                if (_command.ToLower() == command.Name)
                {
                    if (command.Actions == null)
                    {
                        command.Actions = new List<Action<string[]>>();
                    }

                    command.Actions.Add(actionDelegate);
                }
            }
        }

        public static bool Parse(string message)
        {
            if (message.Length < 3 || message[0] != '/')
            {
                return false;
            }

            var input = message.Substring(1).Split(' ');
            if (input.Length == 0) { return false; }
            foreach (var command in commands)
            {
                if (input[0].ToLower() == command.Name)
                {
                    foreach (var action in command.Actions)
                    {
                        action(input);
                    }

                    return true;
                }
            }

            return false;
        }
    }
}
