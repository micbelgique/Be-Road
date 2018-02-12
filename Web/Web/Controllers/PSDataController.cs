using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class PSDataController : Controller
    {
        public ActionResult Index(string id)
        {
            ViewBag.id = id;
            return View();
        }
    }
}