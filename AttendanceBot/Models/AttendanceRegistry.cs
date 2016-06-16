using LanguageExt;
using System.Collections.Generic;
using System.Linq;

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

        public static List<string> FindAllEventNames(string conversationId)
        {
            var eventNames = new List<string>();

            if(_attendanceHistory.ContainsKey(conversationId))
            {
                eventNames = _attendanceHistory[conversationId].Values.Select(v => v.Name).ToList();
            }

            return eventNames;
        }

        public static string SetActive(string conversationId, string eventName)
        {
            var result = FindByName(conversationId, eventName);
            return result.Match(
                    e =>
                    {
                        _current[conversationId] = e;
                        return $"**{eventName}** is set as active";
                    },
                    () => $"Event with name **{eventName}** not found"
                );
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
                var attendance = new EventAttendance(conversationId, eventName);
                _attendanceHistory[conversationId].Add(eventName, attendance);
                _current[conversationId] = attendance;
                return attendance;
            }
        }
    }
}