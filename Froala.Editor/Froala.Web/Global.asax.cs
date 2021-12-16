using log4net.Config;
using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Froala.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            CreateDirectories();
            XmlConfigurator.Configure();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error()
        {
            Exception ex = Server.GetLastError().GetBaseException();

            Logger.Log.Error(ex);
        }

        private void CreateDirectories()
        {
            string filesFolderPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Files");

            bool filesFolderExists = System.IO.Directory.Exists(filesFolderPath);

            if (!filesFolderExists)
            {
                System.IO.Directory.CreateDirectory(filesFolderPath);
            }

            string imagesFolderPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Images");

            bool imagesFolderExists = System.IO.Directory.Exists(imagesFolderPath);

            if (!imagesFolderExists)
            {
                System.IO.Directory.CreateDirectory(imagesFolderPath);
            }
        }
    }
}