using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Dal;
using Web.Models;

namespace Web.Controllers
{
    public class PSDataController : Controller
    {
        private PSContext context = new PSContext();
        private PSDataService service = new PSDataService();

        public async Task<ActionResult> Index(int id)
        {
            var ps = context.PublicServices.Where(model => model.ID == id).FirstOrDefault();
            EidCard eid = (EidCard)Session["eid"];
            if (ps == null || eid == null) 
                return View("Index", "Home");

            ViewBag.PS = ps;
            var psd = await service.GetDataOfAsync(ps, eid);
            return View("Psd", psd);
        }
    }
}