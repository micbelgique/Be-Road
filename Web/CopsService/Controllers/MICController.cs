using PublicService.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PublicService.Controllers
{
    public class MICController : Controller
    {
        private PSContext db = new PSContext();

        // GET: MIC
        public ActionResult Index()
        {
            var trainees = db.MicTrainees.ToList();
            return View(trainees);
        }

        [HttpPost]
        public ActionResult AddAccessInfo(int? dataId, string name, string reason, string ip)
        {
            var data = db.Datas.Find(dataId);
            if(data == null || String.IsNullOrWhiteSpace(name) || String.IsNullOrWhiteSpace(reason))
            {
                return HttpNotFound();
            }

            data.AccessInfos.Add(new Models.AccessInfo()
            {
                Date = DateTime.Now,
                Name = $"{name};{ip}",
                Reason = reason
            });
            db.SaveChanges();
            return Content("Successfully saved");
        }

        [HttpPost]
        public ActionResult Details(int? id, int? dataId, string name, string reason, string ip)
        {
            AddAccessInfo(dataId, name, reason, ip);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var mic = db.MicTrainees.Find(id);
            if (mic == null)
            {
                return HttpNotFound();
            }
            return View(mic);
        }
        
    }
}