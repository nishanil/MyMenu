using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MyMenuAppService.Startup))]

namespace MyMenuAppService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}