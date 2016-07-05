using System.Configuration;

namespace AttendanceBot.Infrastructure.Settings
{
    public class ApplicationSettings
    {
        public string ConnectionString => ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString;
        public string DatabaseName => ConfigurationManager.AppSettings["DatabaseName"];
    }
}