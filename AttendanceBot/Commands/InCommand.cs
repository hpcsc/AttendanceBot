using AttendanceBot.Models;
using LanguageExt;
using Microsoft.Bot.Connector;

namespace AttendanceBot.Commands
{
    public class InCommand : BotCommandBase
    {
        public override bool CanHandle(string[] messageElements)
        {
            return messageElements.Length > 0 && StringEquals(messageElements[0], "/in");            
        }

        public override Option<string> Handle(string[] messageElements, Message originalMessage)
        {
            var result = AttendanceRegistry.FindCurrent(originalMessage.ConversationId);
            return result.Match(
                e => {
                        e.In(originalMessage.From.Id, originalMessage.From.Name);
                        return $"{originalMessage.From.Name} in";
                    },
                    () => "No active event at the moment"
                );
        }
    }
}