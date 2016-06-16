using AttendanceBot.Models;
using LanguageExt;
using Microsoft.Bot.Connector;
using System;
using System.Linq;

namespace AttendanceBot.Commands
{
    public class ListEventsCommand : BotCommandBase
    {
        public override string CommandName { get { return CommandPrefix + "list"; } }

        public override Option<string> Handle(string[] messageElements, Message originalMessage)
        {
            var allEvents = AttendanceRegistry.FindAllEventNames(originalMessage.ConversationId);
            if(allEvents.Any())
            {
                return string.Join(Environment.NewLine, allEvents.Select(e => $"- {e}"));
            }

            return "No events available";
        }
    }
}