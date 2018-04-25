using CentralServer.Dal;
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

        public ActionResult CreateIndex()
        {
            return View("Create");
        }

        public ActionResult Create(AdapterServer ads)
        {
            if (ads.ISName == null || ads.Root == null || ads.Url == null)
            {
                ViewBag.Error = "You must fill all inputs !";
                return View("Create");
            }
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

            var usedContractNames = ads.ContractNames.Select(cn => cn.Id);
            var allContracts = ctx.Contracts.Select(c => c.Id);
            var filteredContracts = allContracts.Where(c => !usedContractNames.Any(u => c.Equals(u)));
            ViewBag.contracts = filteredContracts.ToList();

            return View("Edit", ads);
        }

        [HttpPost]
        public ActionResult Save(AdapterServer ads)
        {
            var newAds = ctx.AdapterServers.FirstOrDefault(a => a.ISName == ads.ISName);
            if (newAds != null)
            {
                newAds.Url = ads.Url;
                newAds.Root = ads.Root;
                if (ads.ContractNames.Count > newAds.ContractNames.Count)
                    ads.ContractNames.ForEach(cn => {
                        newAds.ContractNames.Add(cn);
                    });
                ctx.SaveChanges();
                return Json("OK");
            }
            else
            {
                return Json("Cannot edit this Adapter Server !");

            }
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