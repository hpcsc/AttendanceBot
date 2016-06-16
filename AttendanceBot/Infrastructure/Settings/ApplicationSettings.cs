using System.Configuration;

namespace AttendanceBot.Infrastructure.Settings
{
    public class ApplicationSettings
    {
        public string ConnectionString => ConfigurationManager.AppSettings["ConnectionString"];
        public string DatabaseName => ConfigurationManager.AppSettings["DatabaseName"];
    }
}