using AttendanceBot.Models;
using LanguageExt;
using Microsoft.Bot.Connector;
using System.Linq;

namespace AttendanceBot.Commands
{
    public class StartEventCommand : BotCommandBase
    {
        public override bool CanHandle(string[] messageElements)
        {
            return messageElements.Length > 1 && StringEquals(messageElements[0], "/start");
        }        

        public override Option<string> Handle(string[] messageElements, Message originalMessage)
        {
            var result = AttendanceRegistry.Start(originalMessage.ConversationId, 
                string.Join(" ", messageElements.Skip(1)));

            return result.Match(e => "Event started", m => m);            
        }        
    }
}