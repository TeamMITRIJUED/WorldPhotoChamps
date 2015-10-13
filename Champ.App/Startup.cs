using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Champ.App.Startup))]
namespace Champ.App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
