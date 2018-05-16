using CentralServer.Dal;
using CentralServer.Helpers;
using Contracts.Dal;
using Contracts.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CentralServer.Controllers
{
    public class ADSController : Controller
    {
        private ContractContext ctx = new ContractContext(ConfigHelper.GetConnectionString("ContractContext"));

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

        public async Task<ActionResult> Create(AdapterServer ads)
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
                await CallToMLAsync(new { UserName = ads.ISName, UserType = "TEMP" }, "api/AdapterServer/Add");
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
        public async Task<ActionResult> Save(AdapterServer ads)
        {
            var newAds = ctx.AdapterServers.FirstOrDefault(a => a.ISName == ads.ISName);
            if (newAds != null)
            {
                newAds.Url = ads.Url;
                newAds.Root = ads.Root;
                if (ads.ContractNames.Count > newAds.ContractNames.Count)
                    ads.ContractNames.ForEach(cn => {
                        newAds.ContractNames.Add(ctx.Contracts.FirstOrDefault(c => c.Id.Equals(cn.Id)));
                    });
                if(ads.ContractNames.Count < newAds.ContractNames.Count)
                {
                    newAds.ContractNames.Clear();
                    ads.ContractNames.ForEach(cn => {
                        var contract = ctx.Contracts.FirstOrDefault(c => c.Id.Equals(cn.Id));
                        newAds.ContractNames.Add(contract);
                    });
                }
                ctx.SaveChanges();
                await CallToMLAsync(new { UserName = newAds.ISName, UserType = "TEMP" }, "api/AdapterServer/Update");
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

        public async Task<ActionResult> Delete(string modalValue)
        {
            var ads = ctx.AdapterServers.FirstOrDefault(a => a.ISName == modalValue.Trim());
            var name = ads.ISName;
            if (ads != null)
            {
                ads.ContractNames.Clear();
                ctx.SaveChanges();
                ctx.AdapterServers.Remove(ads);
                ctx.SaveChanges();
                await CallToMLAsync(new { UserName = name }, "api/AdapterServer/Delete");
            }

            return RedirectToAction("Index");
        }

        private async Task CallToMLAsync(dynamic args, string route)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(ConfigHelper.GetServiceUrl("messagelog"));
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var httpContent = new StringContent(JsonConvert.SerializeObject(args), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(route, httpContent);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }
    }
}