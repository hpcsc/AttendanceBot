﻿using AttendanceBot.Models;
using LanguageExt;
using Microsoft.Bot.Connector;
using System;
using System.Linq;

namespace AttendanceBot.Commands
{
    public class ListEventsCommand : BotCommandBase
    {
        protected override string BaseCommandName { get; } = "list";

        public override Option<string> Handle(string[] messageElements, Message originalMessage)
        {
            var allEvents = AttendanceRegistry.FindAllEventNames(originalMessage.ConversationId);
            return allEvents.Any() ? 
                string.Join(Environment.NewLine, allEvents.Select(e => $"- {e}")) : 
                "No events available";
        }
    }
}