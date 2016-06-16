using AttendanceBot.Models;
using MongoDB.Bson.Serialization;

namespace AttendanceBot.Infrastructure.Repositories
{
    public class DatabaseConfiguration
    {
        public static void ConfigureMapper()
        {
            BsonClassMap.RegisterClassMap<EventAttendance>(cm => {
                cm.MapField("_attendance");
                cm.MapProperty("ConversationId");
                cm.MapProperty("Name");
                cm.MapProperty("CreatedDate");
            });
        }
    }
}