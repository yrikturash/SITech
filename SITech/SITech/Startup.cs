using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SITech.Startup))]
namespace SITech
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
