using CentralServer.Dal;
using Contracts.Models;
using Newtonsoft.Json;
using System.Linq;
using System.Web.Http;

namespace CentralServer.Controllers
{
    [RoutePrefix("api/central")]
    public class CentralController : ApiController
    {
        private ContractContext context = new ContractContext();

        [HttpGet]
        [Route("contracts")]
        public BeContract Contracts(string id)
        {
            return context.Contracts.FirstOrDefault(c => c.Id.Equals(id));
        }
    }
}
