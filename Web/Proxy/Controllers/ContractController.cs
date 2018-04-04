using ContractsTest;
using Newtonsoft.Json;
using Proxy.Dal;
using Proxy.Logic;
using Proxy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            var cm = new ContractManager(ass = new AdapterServerService()
            {
                ADSList = ASSMock.Fill()
            });

            var res = cm.Call(call);

            return JsonConvert.SerializeObject(res);
        }
    }
}
