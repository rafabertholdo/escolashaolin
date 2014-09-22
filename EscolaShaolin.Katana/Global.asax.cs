using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace EscolaShaolin.Katana
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configuration.EnsureInitialized(); 
        }
    }
}
