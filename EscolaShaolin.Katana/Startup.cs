using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(EscolaShaolin.Katana.Startup))]

namespace EscolaShaolin.Katana
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = GlobalConfiguration.Configuration;
            
            // Only serve files requested by name.
            //app.UseStaticFiles("/app");

            WebApiConfig.Register(config);
            app.UseWebApi(config); 


        }
    }
}