using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MNPWebApplication.Startup))]
namespace MNPWebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
