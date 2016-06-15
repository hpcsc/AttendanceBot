using AttendanceBot.Models;
using LanguageExt;
using Microsoft.Bot.Connector;
using System.Linq;

namespace AttendanceBot.Commands
{
    public class OutCommand : BotCommandBase
    {
        public override bool CanHandle(string[] messageElements)
        {
            return messageElements.Length > 0 && StringEquals(messageElements[0], "/out");
        }

        public override Option<string> Handle(string[] messageElements, Message originalMessage)
        {
            var result = AttendanceRegistry.FindCurrent(originalMessage.ConversationId);
            return result.Match(
                    e =>
                    {
                        e.Out(originalMessage.From.Id, originalMessage.From.Name, 
                            messageElements.Length > 1 ? string.Join(" ", messageElements.Skip(1)) : string.Empty);
                        return $"{originalMessage.From.Name} out";
                    },
                    () => "No active event at the moment"
                );
        }
    }
}