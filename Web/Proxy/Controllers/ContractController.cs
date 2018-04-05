using Contracts.Dal;
using Contracts.Dal.Mock;
using Contracts.Logic;
using Contracts.Models;
using Newtonsoft.Json;
using System.Web.Http;

namespace Proxy.Controllers
{
    [RoutePrefix("api/contract")]
    public class ContractController : ApiController
    {
        [HttpPost]
        [Route("call")]
        public string CallContract(BeContractCall call)
        {
            AdapterServerService ass = null;
            var cm = new ContractManager(ass = new AdapterServerService());
            ass.SetADSList(ASSMock.Fill());

            var res = cm.Call(call);

            return JsonConvert.SerializeObject(res);
        }
    }
}
