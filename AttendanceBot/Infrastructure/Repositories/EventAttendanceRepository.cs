using AttendanceBot.Models;
using LanguageExt;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using AttendanceBot.Infrastructure.Repositories.PersistenceModel;

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

        public Either<string, CurrentEventsPersistenceModel> FindCurrentEvents()
        {
            return TryExecute(db => db.GetCollection<CurrentEventsPersistenceModel>("current-events")                                      
                                      .Find(FilterDefinition<CurrentEventsPersistenceModel>.Empty)                                      
                                      .Project<CurrentEventsPersistenceModel>(Builders<CurrentEventsPersistenceModel>.Projection.Exclude("_id"))
                                      .FirstOrDefault());
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

        public Option<string> SaveCurrentEvents(Dictionary<string, string> currentEvents)
        {            
            return TryExecute(db =>
            {
                db.GetCollection<CurrentEventsPersistenceModel>("current-events").UpdateOne(
                    e => true,
                    Builders<CurrentEventsPersistenceModel>.Update.Set(m => m.Values, currentEvents),
                    new UpdateOptions { IsUpsert = true });
            });
        }
    }
}