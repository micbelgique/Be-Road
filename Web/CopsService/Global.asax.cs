using PublicService.Dal;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;
using Hangfire;
using System.Diagnostics;
using Hangfire.SqlServer;

namespace PublicService
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private AzureUpload au = new AzureUpload();
        private PSContext db = new PSContext();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            System.Web.Http.GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            JobStorage.Current = new SqlServerStorage("PSContext");
            BackgroundJob.Schedule(
                () => au.UploadToAzureAsync(db),
                TimeSpan.FromDays(7));
        }
    }
}
