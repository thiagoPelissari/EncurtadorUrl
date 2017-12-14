using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EncurtadorUrl.Startup))]
namespace EncurtadorUrl
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
