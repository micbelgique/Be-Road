using CentralServer.Dal;
using Contracts.Models;
using Newtonsoft.Json;
using System.Web.Http;

namespace CentralServer.Controllers
{
    [RoutePrefix("api/central")]
    public class CentralController : ApiController
    {
        [HttpPost]
        [Route("find")]
        public string Find(ASFindRequest req)
        {
            var res = CentralServerManager.FindMock(req.Ads, req.Call);
            return JsonConvert.SerializeObject(res);
        }
    }
}
