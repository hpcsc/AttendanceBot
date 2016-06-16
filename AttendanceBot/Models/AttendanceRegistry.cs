using AttendanceBot.Infrastructure.Repositories;
using LanguageExt;
using System.Collections.Generic;
using System.Linq;
using System;

namespace AttendanceBot.Models
{
    public class AttendanceRegistry
    {
        private static readonly Dictionary<string, Dictionary<string, EventAttendance>> _attendanceHistory = 
            new Dictionary<string, Dictionary<string, EventAttendance>>();
        private static readonly Dictionary<string, EventAttendance> _current = new Dictionary<string, EventAttendance>();

        public static void Initialize()
        {
            var repository = new EventAttendanceRepository();
            repository.FindAllEvents()
                .Match(
                events =>
                {
                    foreach (var e in events)
                    {
                        if (!_attendanceHistory.ContainsKey(e.ConversationId))
                        {
                            _attendanceHistory[e.ConversationId] = new Dictionary<string, EventAttendance>();
                        }

                        _attendanceHistory[e.ConversationId][e.Name] = e;
                    }
                },
                error =>
                {
                    //Log 
                });            
        }

        public static void SaveState()
        {
            var repository = new EventAttendanceRepository();
            foreach (var e in _attendanceHistory.Values.SelectMany(v => v.Values))
            {
                repository.Save(e);
            }
        }

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