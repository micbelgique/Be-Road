﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.App_Start;
using Web.Dal;
using Web.Helpers;
using Web.Models;

namespace Web.Controllers
{
    public class PSDataController : Controller
    {
        private PSContext context = new PSContext(ConfigHelper.GetAppSetting("PSContext"));
        private PSDataService service = new PSDataService();

        [AuthorizationFilter]
        public async Task<ActionResult> Index(int? id)
        {
            var ps = context.PublicServices.Where(model => model.ID == id).FirstOrDefault();
            EidCard eid = (EidCard)Session["eid"];
            if (ps == null || eid == null) 
                return RedirectToAction("Index", "Home");

            ViewBag.PS = ps;
            var psd = await service.GetDataOfAsync(ps, eid);
            return View("Index", psd);
        }
    }
}