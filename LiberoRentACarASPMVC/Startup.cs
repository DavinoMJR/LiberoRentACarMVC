using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LiberoRentACarASPMVC.Startup))]
namespace LiberoRentACarASPMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
            
        }
    }
}
