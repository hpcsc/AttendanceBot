using AttendanceBot.Models;
using LanguageExt;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace AttendanceBot.Infrastructure.Repositories
{
    public class EventAttendanceRepository : MongoRepositoryBase
    {
        public Either<string, List<EventAttendance>> FindAllEvents()
        {
            return TryExecute(db => db.GetCollection<EventAttendance>("events")
                                      .Find(FilterDefinition<EventAttendance>.Empty)
                                      .Project<EventAttendance>(Builders<EventAttendance>.Projection.Exclude("_id"))
                                      .ToList());        
        }

        public Option<string> Save(EventAttendance incoming)
        {
            return TryExecute(db =>
            {
                db.GetCollection<EventAttendance>("events").ReplaceOne(
                    e => e.ConversationId == incoming.ConversationId && e.Name == incoming.Name,
                    incoming,
                    new UpdateOptions { IsUpsert = true });
            });
        }
    }
}