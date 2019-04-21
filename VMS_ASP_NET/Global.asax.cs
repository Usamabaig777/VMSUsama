using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Configuration;

namespace VMS_ASP_NET
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            try
            {
                DataAccessSql.Initialize(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                Application["IsDatbaseConnectionValid"] = DataAccessSql.IsDatabaseConnected();
            }
            catch (Exception ex)
            {
                Application["IsDatbaseConnectionValid"] = false;
            }
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        //protected void Application_Error(object sender, EventArgs e)
        //{
        //    Exception ex = Server.GetLastError();

        //    if (ex != null)
        //    {
        //        Server.ClearError();
        //    }

        //    Response.Redirect("~/Error/InternalError");
        //}
    }
}
