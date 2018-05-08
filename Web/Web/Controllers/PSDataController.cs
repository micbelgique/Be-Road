using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.App_Start;
using Web.Dal;
using Web.Helpers;
using Web.Models;
using Web.Models.ViewModel;

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
            var viewModelList = new List<PublicServiceDataViewModel>();
            psd.ForEach(data => 
            {
                var accessInfos = data.AccessInfos;
                var grouped = accessInfos.GroupBy(ai => new
                {
                    Date = ai.Date.ToShortDateString(),
                    ai.IsReliable,
                    ai.Justification,
                    ai.Name
                }, ai => new AccessInfoViewModel()
                {
                    Date = ai.Date.ToShortDateString(),
                    IsReliable = ai.IsReliable,
                    Justification = ai.Justification,
                    Name = ai.Name,
                    Total = accessInfos.Count(inside =>
                    {
                        return inside.Date.ToShortDateString() == ai.Date.ToShortDateString()
                            && inside.IsReliable == ai.IsReliable
                            && inside.Justification == ai.Justification
                            && inside.Name == ai.Name;
                    })
                })
                .Select(grp => grp.FirstOrDefault())
                .ToList();
                viewModelList.Add(new PublicServiceDataViewModel()
                {
                    Datas = data.Datas,
                    AccessInfos = grouped,
                    ContractName = data.ContractName,
                    NRID = data.NRID
                });
            });
            return View("Index", viewModelList);
        }
    }
}