using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GroupAPT.Startup))]
namespace GroupAPT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
