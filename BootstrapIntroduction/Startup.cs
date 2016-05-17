using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BootstrapIntroduction.Startup))]
namespace BootstrapIntroduction
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
