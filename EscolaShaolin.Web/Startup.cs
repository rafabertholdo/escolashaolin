using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EscolaShaolin.Web.Startup))]
namespace EscolaShaolin.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
