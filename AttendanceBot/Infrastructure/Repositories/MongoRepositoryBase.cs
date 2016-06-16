using AttendanceBot.Infrastructure.Settings;
using LanguageExt;
using MongoDB.Driver;
using System;

namespace AttendanceBot.Infrastructure.Repositories
{
    public abstract class MongoRepositoryBase
    {
        protected IMongoDatabase Database;
        public MongoRepositoryBase()
        {
            var applicationSettings = new ApplicationSettings();
            var client = new MongoClient(applicationSettings.ConnectionString);
            Database = client.GetDatabase(applicationSettings.DatabaseName);
        }

        protected Option<string> TryExecute(Action<IMongoDatabase> action)
        {
            try
            {
                action(Database);

                return null;
            }
            catch (MongoException ex)
            {
                return ex.ToString();
            }
        }

        protected Either<string, T> TryExecute<T>(Func<IMongoDatabase, T> func)
        {
            try
            {
                var result = func(Database);

                return result;
            }
            catch (MongoException ex)
            {
                return ex.ToString();
            }
        }
    }
}