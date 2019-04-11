using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Caalinder.Startup))]
namespace Caalinder
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
