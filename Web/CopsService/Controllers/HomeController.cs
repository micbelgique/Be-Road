using PublicService.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PublicService.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult AboutGDPR()
        {
            return View("AboutGDPR");
        }
        
    }
}