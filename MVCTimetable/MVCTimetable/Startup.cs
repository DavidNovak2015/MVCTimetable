using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCTimetable.Startup))]
namespace MVCTimetable
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
