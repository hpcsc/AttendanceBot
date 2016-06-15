using LanguageExt;
using Microsoft.Bot.Connector;
using System;

namespace AttendanceBot.Commands
{
    public abstract class BotCommandBase : IBotCommand
    {
        public abstract bool CanHandle(string[] messageElements);
        public abstract Option<string> Handle(string[] messageElements, Message originalMessage);        

        protected bool HasValidCommandFormat(string[] messageElements, string commandName, int numOfArguments)
        {
            return messageElements.Length == numOfArguments &&
                StringEquals(messageElements[0], commandName);
        }

        protected bool StringEquals(string first, string second)
        {
            return string.Equals(first, second, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}