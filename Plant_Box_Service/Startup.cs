using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Plant_Box_Service.Startup))]
namespace Plant_Box_Service
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
