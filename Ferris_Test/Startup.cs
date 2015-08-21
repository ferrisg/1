using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ferris_Test.Startup))]
namespace Ferris_Test
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
