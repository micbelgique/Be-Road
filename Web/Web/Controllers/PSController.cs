using System.Web.Mvc;
using Web.App_Start;
using Web.Models;

namespace Web.Controllers
{
    public class PSController : Controller
    {
        [AuthorizationFilter]
        public ActionResult Index()
        {
            EidCard eid = (EidCard)Session["eid"];
            return View("Index", eid);
        }

        [AuthorizationFilter]
        [HttpGet]
        public ActionResult Select(string _id)
        {
            return RedirectToAction("Index", "PSData", new { id = _id });
        }
    }
}