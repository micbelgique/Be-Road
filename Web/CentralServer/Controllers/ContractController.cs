using CentralServer.Dal;
using Contracts.Dal;
using System.Linq;
using System.Web.Mvc;

namespace CentralServer.Controllers
{
    public class ContractController : Controller
    {
        // GET: Contract
        public ActionResult Index()
        {
            return RedirectToAction("Create");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetOutput(string lookUpId, string contractId)
        {
            var ctx = new ContractContext();
            var total = ctx.Contracts.ToList();
            var contract = ctx.Contracts.FirstOrDefault(c => c.Id == contractId);
            var res = contract.Outputs.FindAll(o => o.LookupInputId == int.Parse(lookUpId) - 1);
            if (res != null)
                return Json(res, JsonRequestBehavior.AllowGet);
            else
                return Json("Null", JsonRequestBehavior.AllowGet);
        }
    }
}