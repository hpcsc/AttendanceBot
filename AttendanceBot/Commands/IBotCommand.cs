using LanguageExt;
using Microsoft.Bot.Connector;

namespace AttendanceBot.Commands
{
    public interface IBotCommand
    {
        string CommandName { get; }
        bool CanHandle(string[] messageElements);
        Option<string> Handle(string[] messageElements, Message originalMessage);
    }
}