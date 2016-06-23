using System.Collections.Generic;
using LanguageExt;
using Microsoft.Bot.Connector;

namespace AttendanceBot.Commands
{
    public interface IBotCommand
    {
        IEnumerable<string> SupportedCommandNames { get; }
        bool CanHandle(string[] messageElements);
        Option<string> Handle(string[] messageElements, Message originalMessage);
    }
}