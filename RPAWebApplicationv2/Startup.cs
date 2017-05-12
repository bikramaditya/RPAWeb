using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RPAWebApplicationv2.Startup))]
namespace RPAWebApplicationv2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
