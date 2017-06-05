using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RealEstate.Administrator.Startup))]
namespace RealEstate.Administrator
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
