using AttendanceBot.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AttendanceBot.Models
{
    public class EventAttendance
    {
        private Dictionary<string, AttendanceEntry> _attendance;
        public string ConversationId { get; private set; }
        public string Name { get; private set; }
        public DateTime CreatedDate { get; private set; }
        private static readonly string _messageTemplate =
            @"## {0}

Yes ( {1} )
{2}

No ( {3} )
{4}

Maybe ( {5} )
{6}";

        public EventAttendance(string conversationId, string name)
        {
            ConversationId = conversationId;
            Name = name;
            _attendance = new Dictionary<string, AttendanceEntry>();
            CreatedDate = DateTime.Now;
        }

        public void In(string userId, string name)
        {
            if (!_attendance.ContainsKey(userId))
            {
                _attendance[userId] = new AttendanceEntry(userId, name, AttendanceStatus.Yes);
            }
            else
            {
                _attendance[userId].In();
            }
        }

        public void Out(string userId, string name, string message = null)
        {
            if (!_attendance.ContainsKey(userId))
            {
                _attendance[userId] = new AttendanceEntry(userId, name, AttendanceStatus.No, message);
            }
            else
            {
                _attendance[userId].Out(message);
            }
        }

        public void Maybe(string userId, string name, string message = null)
        {
            if (!_attendance.ContainsKey(userId))
            {
                _attendance[userId] = new AttendanceEntry(userId, name, AttendanceStatus.Maybe, message);
            }
            else
            {
                _attendance[userId].Maybe(message);
            }
        }

        public override string ToString()
        {
            var yesEntries = FindAttendance(a => a.Status == AttendanceStatus.Yes);
            var noEntries = FindAttendance(a => a.Status == AttendanceStatus.No);
            var maybeEntries = FindAttendance(a => a.Status == AttendanceStatus.Maybe);

            return string.Format(_messageTemplate,
                                Name,
                                yesEntries.Count,
                                FormatAttendanceEntries(yesEntries),
                                noEntries.Count,
                                FormatAttendanceEntries(noEntries),
                                maybeEntries.Count,
                                FormatAttendanceEntries(maybeEntries));
        }

        private List<AttendanceEntry> FindAttendance(Func<AttendanceEntry, bool> condition)
        {
            return _attendance.Values.Where(condition).ToList();
        }

        private string FormatAttendanceEntries(List<AttendanceEntry> entries)
        {
            return string.Join(Environment.NewLine,
                        entries.Select(e => $"- {e.Name}" +
                        (string.IsNullOrWhiteSpace(e.Message) ? string.Empty : $" ({e.Message})")));
        }
    }
}