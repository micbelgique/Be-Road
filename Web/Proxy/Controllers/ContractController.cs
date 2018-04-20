﻿using Contracts;
using Contracts.Dal;
using Contracts.Logic;
using Contracts.Models;
using Newtonsoft.Json;
using Proxy.Dal;
using Proxy.Dal.Mock;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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
                BcService = new BeContractServiceImpl()
            };
        }

        [HttpPost]
        [Route("call")]
        public async /*Task<Dictionary<int, BeContractReturn>>*/ Task<HttpResponseMessage> CallContractAsync(BeContractCall call)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, await cm.CallAsync(call));
            }
            catch(BeContractException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
