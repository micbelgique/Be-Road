using Contracts.Dal;
using Contracts.Logic;
using Contracts.Models;
using Newtonsoft.Json;
using Proxy.Dal;
using Proxy.Dal.Mock;
using System.Threading.Tasks;
using System.Web.Http;

namespace Proxy.Controllers
{
    [RoutePrefix("api/contract")]
    public class ContractController : ApiController
    {
        private ContractManager cm;

        public ContractController()
        {
            cm = new ContractManager()
            {
                AsService = new AdapterServerServiceImpl(),
                BcService = new BeContractService()
            };
        }

        [HttpPost]
        [Route("call")]
        public async Task<string> CallContractAsync(BeContractCall call)
        {
            var res = await cm.CallAsync(call);

            return JsonConvert.SerializeObject(res);
        }
    }
}
