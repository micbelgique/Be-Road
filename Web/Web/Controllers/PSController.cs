using System.Web.Mvc;
using Web.Dal;
using Web.App_Start;
using Web.Models;
using System.Linq;

namespace Web.Controllers
{
    public class PSController : Controller
    {
        private PSContext context = new PSContext();

        [AuthorizationFilter]
        public ActionResult Index()
        {
            EidCard eid = (EidCard)Session["eid"];
            if (eid != null)
                ViewBag.Name = $"{eid.FirstName} {eid.MiddleName} {eid.LastName}";
            else
                ViewBag.Name = "Incognito";
            //return View("Index", eid);
            return View("Index", context.PublicServices.ToList());
        }

        [AuthorizationFilter]
        [HttpGet]
        public ActionResult Select(string id)
        {
            return RedirectToAction("Index", "PSData", new { id });
        }
    }
}