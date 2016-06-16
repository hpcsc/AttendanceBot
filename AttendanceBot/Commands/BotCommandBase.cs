using LanguageExt;
using Microsoft.Bot.Connector;
using System;

namespace AttendanceBot.Commands
{
    public abstract class BotCommandBase : IBotCommand
    {
        public bool CanHandle(string[] messageElements)
        {
            return messageElements.Length > 0 && StringEquals(messageElements[0], "/" + CommandName);
        }

        protected abstract string CommandName { get; }
        public abstract Option<string> Handle(string[] messageElements, Message originalMessage);        

        protected bool StringEquals(string first, string second)
        {
            return string.Equals(first, second, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}