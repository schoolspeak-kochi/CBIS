using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RegistrationProcess.Startup))]
namespace RegistrationProcess
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
