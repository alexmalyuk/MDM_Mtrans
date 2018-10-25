using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mtrans_MDM.Startup))]
namespace Mtrans_MDM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
