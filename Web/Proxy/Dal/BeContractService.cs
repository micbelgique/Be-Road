using Contracts.Dal;
using Contracts.Models;
using System.Linq;
using Proxy.Dal.Mock;

namespace Proxy.Dal
{
    public class BeContractService : IBeContractService
    {
        public BeContract FindBeContractById(string id)
        {
            return BeContractsMock.GetContracts().FirstOrDefault(c => c.Id.Equals(id));
        }
    }
}