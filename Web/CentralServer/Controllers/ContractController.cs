using CentralServer.Dal;
using Contracts.Dal;
using Contracts.Models;
using System.Linq;
using System.Web.Mvc;

namespace CentralServer.Controllers
{
    public class ContractController : Controller
    {
        // GET: Contract
        public ActionResult Index()
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create([Bind(Prefix = "c")]BeContract contract)
        {
            return View();
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