using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EFPractice.Startup))]
namespace EFPractice
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
