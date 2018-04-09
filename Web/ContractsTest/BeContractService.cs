using Contracts.Dal;
using Contracts.Models;
using Proxy.Dal.Mock;
using System.Linq;

namespace BeRoadTest
{
    public class BeContractService : IBeContractService
    {
        public BeContract FindBeContractById(string id)
        {
            return BeContractsMock.GetContracts().FirstOrDefault(c => c.Id.Equals(id));
        }
    }
}