using CopsService.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CopsService.Controllers
{
    public class HomeController : Controller
    {
        private CopsContext db = new CopsContext();
        public ActionResult Index()
        {

            return View();
        }
        
    }
}