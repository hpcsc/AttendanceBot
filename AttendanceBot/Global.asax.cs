using AttendanceBot.Infrastructure.Repositories;
using AttendanceBot.Models;
using System.Web.Http;

namespace AttendanceBot
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            DatabaseConfiguration.ConfigureMapper();
            AttendanceRegistry.Initialize();
        }
        
        protected void Application_End()
        {
            AttendanceRegistry.SaveState();
        }
    }
}
