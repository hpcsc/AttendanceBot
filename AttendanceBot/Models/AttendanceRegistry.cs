using LanguageExt;
using System.Collections.Generic;

namespace AttendanceBot.Models
{
    public class AttendanceRegistry
    {
        private static readonly Dictionary<string, Dictionary<string, EventAttendance>> _attendanceHistory = 
            new Dictionary<string, Dictionary<string, EventAttendance>>();
        private static readonly Dictionary<string, EventAttendance> _current = new Dictionary<string, EventAttendance>();

        public static Option<EventAttendance> FindCurrent(string conversationId)
        {
            if(_current.ContainsKey(conversationId))
            {
                return _current[conversationId];
            }

            return null;
        }

        public static Option<EventAttendance> FindByName(string conversationId, string eventName)
        {
            if (_attendanceHistory.ContainsKey(conversationId) && 
                _attendanceHistory[conversationId].ContainsKey(eventName))
            {
                return _attendanceHistory[conversationId][eventName];
            }

            return null;
        }

        public static Either<string, EventAttendance> Start(string conversationId, string eventName)
        {
            if(!_attendanceHistory.ContainsKey(conversationId))
            {
                _attendanceHistory[conversationId] = new Dictionary<string, EventAttendance>();                
            }

            if(_attendanceHistory[conversationId].ContainsKey(eventName))
            {
                return $"Event with name **{eventName}** exists";
            }
            else
            {
                var attendance = new EventAttendance(eventName);
                _attendanceHistory[conversationId].Add(eventName, attendance);
                _current[conversationId] = attendance;
                return attendance;
            }
        }
    }
}