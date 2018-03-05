using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Web.Dal;
using Web.App_Start;
using Web.Models;

namespace Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalFilters.Filters.Add(new AuthorizationFilter());
        }

        protected void Session_Start()
        {
            string culture = "en-US";

            if (Request.UserLanguages != null)
                culture = Request.UserLanguages[0];

            if (culture.Equals("fr") || culture.Equals("fr-FR"))
                culture = "fr-BE";

            HttpCookie langCookie = new HttpCookie("Language");
            if (langCookie.Value == null)
                langCookie.Value = culture;
            langCookie.Expires = DateTime.Now.AddDays(1d);
            Response.Cookies.Add(langCookie);

            Session["lang"] = culture;

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(culture);
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            string before = (string)Session["lang"];
            string current = Request.Cookies.Get("Language").Value;
            string culture = null;

            if (before.Equals(current))
                culture = before;
            else
                culture = current;

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(culture);
        }
    }
}
