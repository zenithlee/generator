using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CATSE.Startup))]
namespace CATSE
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
