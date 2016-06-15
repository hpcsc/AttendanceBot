using LanguageExt;
using Microsoft.Bot.Connector;

namespace AttendanceBot.Commands
{
    public interface IBotCommand
    {
        bool CanHandle(string[] messageElements);
        Option<string> Handle(string[] messageElements, Message originalMessage);
    }
}