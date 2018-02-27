using PublicService.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PublicService.Controllers
{
    [Authorize]
    public class ServiceController : Controller
    {
        private PSContext db = new PSContext();

        [HttpGet]
        public ActionResult Index()
        {
            var users = db.Users.ToList();
            return View(users);
        }

        public ActionResult UploadToAzure()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(String name)
        {
            throw new NotImplementedException("Search users by name is not yet implemented");
        }


    }
}