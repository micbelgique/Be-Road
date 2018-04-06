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
        public BeContractReturn Find(ASFindRequest req)
        {
            return CentralServerManager.FindMock(req.Ads, req.Call);
        }
    }
}
