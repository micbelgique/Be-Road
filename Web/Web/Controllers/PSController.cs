using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Dal;
using Web.Models;

namespace Web.Controllers
{
    public class PSController : Controller
    {
        private PSContext context = new PSContext();

        public ActionResult Index()
        {
            Session["connected"] = true;
            EidCard eid = (EidCard)Session["eid"];
            if (eid != null)
                ViewBag.Name = $"{eid.FirstName} {eid.MiddleName} {eid.LastName}";
            else
                ViewBag.Name = "Incognito";
            //return View("Index", eid);
            return View("Index", context.PublicServices.ToList());
        }

        [HttpGet]
        public ActionResult Select(string id)
        {
            return RedirectToAction("Index", "PSData", new { id });
        }
    }
}