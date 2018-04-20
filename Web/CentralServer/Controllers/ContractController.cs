using CentralServer.Dal;
using CentralServer.ViewModels;
using Contracts;
using Contracts.Dal;
using Contracts.Logic;
using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CentralServer.Controllers
{
    public class ContractController : Controller
    {
        ContractContext ctx = new ContractContext();
        Validators validators = new Validators();

        // GET: Contract
        public ActionResult Index()
        {
            var test = ctx.Contracts.FirstOrDefault(c => c.Id.Equals("GetAddressByDogIdTest"));
            if (test != null)
            {
                ctx.Contracts.Remove(test);
                ctx.SaveChanges();
            }

            return View("Create");
        }

        [HttpPost]
        public ActionResult Delete(string modalValue)
        {
            var contract = ctx.Contracts.FirstOrDefault(c => c.Id == modalValue);
            if (ctx.Queries.Count(q => q.Contract.Id == contract.Id) > 0)
            {
                TempData["Error"] = contract.Id;
                return RedirectToAction("GoToList");
            }
            ctx.Contracts.Remove(contract);
            ctx.SaveChanges();
            return RedirectToAction("GoToList");
        }

        public ActionResult Details(string id)
        {
            var contract = ctx.Contracts.FirstOrDefault(c => c.Id == id);
            var contractVM = new BeContractViewModel()
            {
                Id = contract.Id,
                Description = contract.Description,
                Version = contract.Version,
                Inputs = contract.Inputs,
                Queries = contract.Queries.Select(q =>
                new QueryViewModel()
                {
                    Contract = q.Contract.Id,
                    Mappings = q.Mappings
                }).ToList(),
                Outputs = contract.Outputs
            };

            return View("Details", contractVM);
        }

        public ActionResult GoToList()
        {
            var contracts = ctx.Contracts.Select(c =>
                new BeContractViewModel()
                {
                    Id = c.Id,
                    Description = c.Description,
                    Version = c.Version,
                    Inputs = c.Inputs,
                    Queries = c.Queries.Select(q =>
                    new QueryViewModel()
                    {
                        Contract = q.Contract.Id,
                        Mappings = q.Mappings
                    }).ToList(),
                    Outputs = c.Outputs
                }
            ).ToList();

            ViewBag.Error = TempData["Error"];
            return View("Display", contracts);
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> CreateAsync(BeContractViewModel contractVM)
        {
            var contract = new BeContract()
            {
                Id = contractVM.Id,
                Description = contractVM.Description,
                Version = contractVM.Version,
                Inputs = contractVM.Inputs,
                Queries = new List<Query>(),
                Outputs = contractVM.Outputs
            };

            if (contractVM.Queries != null)
            {
                contractVM.Queries.ForEach(q =>
                {
                    contract.Queries.Add(new Query()
                    {
                        Contract = ctx.Contracts.FirstOrDefault(c => c.Id.Equals(q.Contract)),
                        Mappings = q.Mappings
                    });
                });
            }

            try
            {
                await validators.ValidateBeContract(contract);
            }
            catch (BeContractException ex)
            {
                var validError = new
                {
                    Status = "2",
                    Error = ex.Message
                };
                return Json(validError);
            }

            try
            {
                ctx.Contracts.Add(contract);
                ctx.SaveChanges();
            }
            catch(Exception ex)
            {
                var dbError = new
                {
                    Status = "3",
                    Error = ex.Message
                };
                return Json(dbError);
            }

            var ret = new
            {
                Status = "1",
                Error = "None"
            };
            return Json(ret);

        }

        [HttpPost]
        public ActionResult GetOutput(string lookUpId, string contractId)
        {
            var ctx = new ContractContext();
            var contract = ctx.Contracts.FirstOrDefault(c => c.Id == contractId);
            var res = contract.Outputs.FindAll(o => o.LookupInputId == int.Parse(lookUpId) - 1);
            if (res != null)
                return Json(res, JsonRequestBehavior.AllowGet);
            else
                return Json("Null", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetContracts()
        {
            var ctx = new ContractContext();
            var contracts = ctx.Contracts.ToList();
            if (contracts != null)
                return Json(contracts, JsonRequestBehavior.AllowGet);
            else
                return Json("Null", JsonRequestBehavior.AllowGet);
        }
    }
}