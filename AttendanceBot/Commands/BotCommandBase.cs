using AttendanceBot.Helpers;
using LanguageExt;
using Microsoft.Bot.Connector;
using System;

namespace AttendanceBot.Commands
{
    public abstract class BotCommandBase : IBotCommand
    {
        public bool CanHandle(string[] messageElements)
        {
            return messageElements.Length > 0 && messageElements[0].EqualsIgnoreCase(CommandName);
        }
        
        public abstract string CommandName { get; }        
        public abstract Option<string> Handle(string[] messageElements, Message originalMessage);        

        protected string CommandPrefix { get { return "$"; } }        
    }
}