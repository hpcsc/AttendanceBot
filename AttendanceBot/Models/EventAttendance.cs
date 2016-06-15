using System;
using System.Collections.Generic;
using System.Linq;

namespace AttendanceBot.Models
{
    public class EventAttendance
    {
        private Dictionary<string, AttendanceEntry> _attendance;        
        private string _name;

        public EventAttendance(string name)
        {
            _name = name;
            _attendance = new Dictionary<string, AttendanceEntry>();
        }

        public void In(string userId, string name)
        {
            if(!_attendance.ContainsKey(userId))
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
            return $"## {_name}" +
                Environment.NewLine + Environment.NewLine + FormatAttendanceEntries(FindAttendance(a => a.Status == AttendanceStatus.Yes), "Yes") +
                Environment.NewLine + Environment.NewLine + FormatAttendanceEntries(FindAttendance(a => a.Status == AttendanceStatus.No), "No") +
                Environment.NewLine + Environment.NewLine + FormatAttendanceEntries(FindAttendance(a => a.Status == AttendanceStatus.Maybe), "Maybe");
        }

        private List<AttendanceEntry> FindAttendance(Func<AttendanceEntry, bool> condition)
        {
            return _attendance.Values.Where(condition).ToList();
        }

        private string FormatAttendanceEntries(List<AttendanceEntry> entries, string name)
        {
            return $"{name} ({entries.Count})" + Environment.NewLine +
                string.Join(Environment.NewLine, 
                        entries.Select(e => $"- {e.Name} " + 
                        (string.IsNullOrWhiteSpace(e.Message) ? string.Empty : $"({e.Message})")));
        }
    }
}