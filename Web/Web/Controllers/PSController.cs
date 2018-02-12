using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class PSController : Controller
    {
        public ActionResult Index()
        {
            Session["connected"] = true;
            EidCard eid = (EidCard)Session["eid"];
            return View("Index", eid);
        }

        [HttpGet]
        public ActionResult Select(string _id)
        {
            return RedirectToAction("Index", "PSData", new { id = _id });
        }
    }
}