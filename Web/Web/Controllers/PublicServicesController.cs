using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class PublicServicesController : Controller
    {
        public ActionResult Index()
        {
            Session["connected"] = true;
            EidCard eid = (EidCard)Session["eid"];
            return View("Index", eid);
        }

        public ActionResult LogOut()
        {
            ViewBag.Extra = Resources.Global.Log_out;
            return View("Index");
        }
    }
}