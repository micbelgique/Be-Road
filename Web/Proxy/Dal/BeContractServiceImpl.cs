using Contracts.Dal;
using Contracts.Models;
using System.Linq;

namespace Proxy.Dal
{
    public class BeContractServiceImpl : IBeContractService
    {
        private ContractContext context;

        public BeContractServiceImpl()
        {
            context = new ContractContext();
        }

        public BeContract FindBeContractById(string id)
        {
            return context.Contracts.FirstOrDefault(c => c.Id.Equals(id));
        }
    }
}