using AttendanceBot.Infrastructure.Repositories;
using AttendanceBot.Models;
using Elmah.Contrib.WebApi;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace AttendanceBot
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configuration.Services.Add(typeof(IExceptionLogger), new ElmahExceptionLogger());

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
