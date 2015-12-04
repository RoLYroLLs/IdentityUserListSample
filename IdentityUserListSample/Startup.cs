using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IdentityUserListSample.Startup))]
namespace IdentityUserListSample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
