using Contracts.Dal;
using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CentralServer.Controllers
{
    public class ADSController : Controller
    {
        private ContractContext ctx = new ContractContext();

        // GET: ADS
        public ActionResult Index()
        {
            var adsList = ctx.AdapterServers.ToList();

            return View("Display", adsList);
        }

        public ActionResult Create(AdapterServer ads)
        {
            if (ctx.AdapterServers.Any(a => a.ISName == ads.ISName))
            {
                ViewBag.Error = "This Adapter Server already exists !";
                return View("Create");
            }
            else
            {
                ctx.AdapterServers.Add(ads);
                ctx.SaveChanges();
                ViewBag.Message = "Adapter Server successfully added !";
                return View("Create");
            }
        }

        public ActionResult Edit(int id)
        {
            var ads = ctx.AdapterServers.FirstOrDefault(a => a.Id == id);

            var usedContractNames = ctx.ContractNames.Select(cn => cn.Name);
            var allContracts = ctx.Contracts.Select(c => c.Id);
            var filteredContracts = allContracts.Where(c => !usedContractNames.Any(u => c.Equals(u)));
            ViewBag.contracts = filteredContracts.ToList();

            return View("Edit", ads);
        }

        public ActionResult Save(AdapterServer ads)
        {
            ctx.AdapterServers.Add(ads);
            ctx.SaveChanges();

            return View("Edit", ads);
        }

        public ActionResult Details(int id)
        {
            var ads = ctx.AdapterServers.FirstOrDefault(a => a.Id == id);

            return View("Details", ads);
        }

        public ActionResult Delete(int id)
        {
            var ads = ctx.AdapterServers.FirstOrDefault(a => a.Id == id);
            ctx.AdapterServers.Remove(ads);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}