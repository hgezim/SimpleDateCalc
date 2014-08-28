using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SimpleDateCalcApp.Startup))]
namespace SimpleDateCalcApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
