using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InjectionAttacks.Startup))]
namespace InjectionAttacks
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
