using System.Collections.Generic;

namespace AttendanceBot.Infrastructure.Repositories.PersistenceModel
{
    public class CurrentEventsPersistenceModel
    {
        public CurrentEventsPersistenceModel(Dictionary<string, string> values)
        {
            Values = values;
        }

        public Dictionary<string, string> Values { get; private set; }
    }
}