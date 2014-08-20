using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AlethiCorp.Startup))]
namespace AlethiCorp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
