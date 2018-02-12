using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.App_Start;

namespace Web.Controllers
{
    public class PSDataController : Controller
    {
        [AuthorizationFilter]
        public ActionResult Index(string id)
        {
            ViewBag.id = id;
            return View();
        }
    }
}