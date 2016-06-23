using LanguageExt;
using Microsoft.Bot.Connector;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AttendanceBot.Commands
{
    public class CommandProcessor
    {
        private static readonly List<IBotCommand> _commands = new List<IBotCommand>
        {
            new StartEventCommand(),
            new StatusCommand(),
            new InCommand(),
            new OutCommand(),
            new MaybeCommand(),
            new ListEventsCommand(),
            new SelectEventCommand()
        };

        public static Option<string> Process(Message message)
        {
            var result = SplitMessage(message.Text);

            return result.Match(
                    splitted =>
                    {
                        foreach (var c in _commands)
                        {
                            if (c.CanHandle(splitted))
                            {
                                return c.Handle(splitted, message);
                            }
                        }

                        return null;
                    },
                    () => null
                );
        }
        
        public static List<string> ListAvailableCommands()
        {
            return _commands.Select(c => string.Join(" or ", c.SupportedCommandNames)).ToList();
        }

        private static Option<string[]> SplitMessage(string message)
        {
            if(string.IsNullOrWhiteSpace(message))
            {
                return null;
            }

            var splitted = Regex.Split(message, "\\s+");
            if(!splitted.Any())
            {
                return null;
            }

            return splitted;
        }
    }
}