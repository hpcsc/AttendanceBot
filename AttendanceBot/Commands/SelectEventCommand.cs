using AttendanceBot.Models;
using LanguageExt;
using Microsoft.Bot.Connector;
using System.Linq;

namespace AttendanceBot.Commands
{
    public class SelectEventCommand : BotCommandBase
    {
        protected override string BaseCommandName { get; } = "select";

        public override Option<string> Handle(string[] messageElements, Message originalMessage)
        {
            if(messageElements.Length == 1)
            {
                return "Event name is required";
            }

            var eventName = string.Join(" ", messageElements.Skip(1));
            return AttendanceRegistry.SetActive(originalMessage.ConversationId, eventName);
        }
    }
}