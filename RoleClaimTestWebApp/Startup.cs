using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RoleClaimTestWebApp.Startup))]
namespace RoleClaimTestWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
