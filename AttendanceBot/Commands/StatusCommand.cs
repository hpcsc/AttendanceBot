using AttendanceBot.Models;
using LanguageExt;
using Microsoft.Bot.Connector;
using System.Linq;

namespace AttendanceBot.Commands
{
    public class StatusCommand : BotCommandBase
    {
        protected override string CommandName { get { return "status"; } }

        public override Option<string> Handle(string[] messageElements, Message originalMessage)
        {
            var result = 
                messageElements.Length > 1 ?
                AttendanceRegistry.FindByName(originalMessage.ConversationId, string.Join(" ", messageElements.Skip(1))) :
                AttendanceRegistry.FindCurrent(originalMessage.ConversationId);

            return result.Match(
                    e => e.ToString(),
                    () => "No event found"
                );
        }
    }
}