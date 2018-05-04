using CentralServer.Dal;
using CentralServer.Helpers;
using Contracts.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CentralServer.Controllers
{
    [RoutePrefix("api/central")]
    public class CentralController : ApiController
    {
        ContractContext ctx = new ContractContext(ConfigHelper.GetAppSetting("ContractContext"));

        [HttpGet]
        [Route("contracts")]
        public BeContract Contracts(string id)
        {
            return ctx.Contracts.FirstOrDefault(c => c.Id.Equals(id));
        }

        [HttpGet]
        [Route("contract/ads")]
        public List<String> GetContractByISName(string isName)
        {
            return ctx.AdapterServers
                .Where(ads => ads.ISName.Equals(isName))
                .SelectMany(ads => ads.ContractNames)
                .Select(contract => contract.Id)
                .ToList();
        }

        [HttpGet]
        [Route("adapterserver")]
        public AdapterServer AdapterServer(string name)
        {
            return ctx.AdapterServers.FirstOrDefault(ads => ads.ContractNames.Any(c => c.Id.Equals(name)));
        }
    }
}
