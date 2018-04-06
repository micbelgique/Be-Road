using Contracts.Dal;
using Contracts.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ContractsTest
{
    /// <summary>
    /// Class used to mock the Adapter Server Service
    /// </summary>
    public class ASSMock : AdapterServerService
    {
        public ASSMock()
        {
            ADSList = new List<AdapterServer>
            {
                new AdapterServer() { ContractNames = new List<string> { "GetOwnerIdByDogId" }, ISName = "Doggies", Url = "www.doggies.com/api/" },
                new AdapterServer() { ContractNames = new List<string>  { "GetMathemathicFunction" }, ISName = "MathLovers", Url = "www.mathlovers.com/api/" },
                new AdapterServer() { ContractNames = new List<string>  { "GetAddressByOwnerId" }, ISName = "CitizenDatabank", Url = "www.citizens.com/api/" },
            };
        }
        public async override Task<BeContractReturn> FindAsync(AdapterServer ads, BeContractCall call)
        {
            //Empty await for the method
            await Task.Run(() => { });
            switch (call.Id)
            {
                case "GetOwnerIdByDogId": return HandleGetOwnerIdByDogId(call);
                default: return new BeContractReturn();
            }
        }

        private BeContractReturn HandleGetOwnerIdByDogId(BeContractCall call)
        {
            var ret = new BeContractReturn()
            {
                Id = call.Id,
                Outputs = new Dictionary<string, dynamic>()
            };
            if ((call.Inputs["DogID"] as string).Equals("D-123"))
                ret.Outputs.Add("OwnerIDOfTheDog", "Wilson !");
            return ret;
        }
    }
}
