using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Physician_Directory.Startup))]
namespace Physician_Directory
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
