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

        [HttpPost]
        public ActionResult Save(AdapterServer ads)
        {
            var newAds = ctx.AdapterServers.FirstOrDefault(a => a.ISName == ads.ISName);
            if(newAds != null)
            {
                newAds.Url = ads.Url;
                newAds.Root = ads.Root;
                if(ads.ContractNames != null)
                    ads.ContractNames.ForEach(cn => newAds.ContractNames.Add(cn));
                ctx.SaveChanges();
                ViewBag.Message = "Adapter Server successfully edited !";
            }
            else{
                ViewBag.Error = "This Adapter Server does not exist !";
            }

            return View("Edit", ads);
        }

        public ActionResult Details(int id)
        {
            var ads = ctx.AdapterServers.FirstOrDefault(a => a.Id == id);

            return View("Details", ads);
        }

        public ActionResult Search(string searchString)
        {
            var adsList = ctx.AdapterServers.Where(ads => ads.ISName == searchString).ToList();

            return View("Display", adsList);
        }

        public ActionResult Delete(string modalValue)
        {
            var ads = ctx.AdapterServers.FirstOrDefault(a => a.ISName == modalValue.Trim());
            if (ads != null)
            {
                ctx.AdapterServers.Remove(ads);
                ctx.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}