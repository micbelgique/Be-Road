using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADSMock.Controllers
{
    [RoutePrefix("api/read")]
    public class ReadController : Controller
    {
        /// <summary>
        /// Calls the IS to get it's info (mock)
        /// </summary>
        /// <returns>The contract return containing the IS info</returns>
        [HttpGet]
        [Route("GetServiceInfo")]
        public BeContractReturn GetServiceInfo()
        {
           // Calling the IS to get their info
           return new BeContractReturn()
            {
                Id = "GetServiceInfo",
                Outputs = new Dictionary<string, dynamic>()
                {
                    { "Name", "MockADS"},
                    { "Purpose", "Testing"},
                    { "CreationDate", DateTime.Now.ToShortDateString() }
                }
            };
        }
    }
}