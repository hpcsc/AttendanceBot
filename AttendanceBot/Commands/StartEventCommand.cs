using AttendanceBot.Models;
using LanguageExt;
using Microsoft.Bot.Connector;
using System.Linq;

namespace AttendanceBot.Commands
{
    public class StartEventCommand : BotCommandBase
    {
        protected override string BaseCommandName { get; } = "start";

        public override Option<string> Handle(string[] messageElements, Message originalMessage)
        {
            if(messageElements.Length == 1)
            {
                return "Event name is required";
            }

            var result = AttendanceRegistry.Start(originalMessage.ConversationId, 
                string.Join(" ", messageElements.Skip(1)));

            return result.Match(e => "Event started", m => m);            
        }        
    }
}