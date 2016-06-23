using AttendanceBot.Helpers;
using LanguageExt;
using Microsoft.Bot.Connector;
using System.Collections.Generic;
using System.Linq;

namespace AttendanceBot.Commands
{
    public abstract class BotCommandBase : IBotCommand
    {
        public IEnumerable<string> SupportedCommandNames
        {
            get { return CommandPrefixes.Select(p => p + BaseCommandName); }
        }

        public bool CanHandle(string[] messageElements)
        {
            return messageElements.Length > 0 && SupportedCommandNames.Any(messageElements[0].EqualsIgnoreCase);
        }

        protected abstract string BaseCommandName { get; }
        public abstract Option<string> Handle(string[] messageElements, Message originalMessage);

        protected IEnumerable<string> CommandPrefixes
        {
            get
            {
                yield return "$";
                yield return "/";
            }
        }
    }
}