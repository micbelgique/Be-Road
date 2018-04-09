using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ADSMock.Controllers
{
    [RoutePrefix("api/read")]
    public class ReadController : ApiController
    {
        /// <summary>
        /// Calls the IS to get it's info (mock)
        /// </summary>
        /// <returns>The contract return containing the IS info</returns>
        [HttpPost]
        [Route("GetServiceInfo")]
        public BeContractReturn GetServiceInfo(BeContractCall call)
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

        private BeContractReturn ReturnAnswer(string ownerId)
        {
            return new BeContractReturn()
            {
                Id = "GetOwnerIdByDogId",
                Outputs = new Dictionary<string, dynamic>()
                {
                    { "OwnerIDOfTheDog", ownerId}
                }
            };
        }

        [HttpPost]
        [Route("GetOwnerIdByDogId")]
        public BeContractReturn GetOwnerId(BeContractCall call)
        {
            switch (call?.Inputs["DogID"])
            {
                case "D-123": return ReturnAnswer("Wilson !");
                case "D-122": return ReturnAnswer("Mika !");
                case "D-124": return ReturnAnswer("Flo !");
                case "D-126": return ReturnAnswer("Pierre !");
                default: return ReturnAnswer("Incognito !");
            }
        }

        [HttpPost]
        [Route("GetSumFunction")]
        public static BeContractReturn GetSumFunction(BeContractCall call)
        {
            var a = Convert.ToInt32(call.Inputs["A"]);
            var b = Convert.ToInt32(call.Inputs["B"]);
            return new BeContractReturn()
            {
                Id = "GetMathemathicFunction",
                Outputs = new Dictionary<string, dynamic>()
                {
                    { "Total", a + b },
                    { "Formula", "Total = A + B"}
                }
            };
        }


        public BeContractReturn ReturnAnswer(string ownerId, int number, string country)
        {
            return new BeContractReturn()
            {
                Id = "GetAddressByOwnerId",
                Outputs = new Dictionary<string, dynamic>()
                {
                    { "Street", ownerId},
                    { "StreetNumber", number},
                    { "Country", country}
                }
            };
        }

        [HttpPost]
        [Route("GetAddressByOwnerId")]
        public BeContractReturn GetAddressByOwnerId(BeContractCall call)
        {
            switch (call.Inputs["OwnerID"])
            {
                case "Wilson !": return ReturnAnswer("Charleroi nord", 9999, "Belgique");
                case "Mika !": return ReturnAnswer("Bxl", 1080, "Belgique");
                case "Flo": return ReturnAnswer("Charleroi Centre", 1000, "Belgique");
                case "Pierre": return ReturnAnswer("Charleroi Central", 5000, "Belgique");
                default: return ReturnAnswer("SDF", 0, "SDF");
            }
        }
    }
}