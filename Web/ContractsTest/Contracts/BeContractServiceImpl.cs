using Contracts.Dal;
using Contracts.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BeRoadTest.Contracts
{
    public class BeContractServiceImpl : IBeContractService
    {
        public async Task<BeContract> FindBeContractByIdAsync(string id)
        {
            BeContract contract = null;
            await Task.Run(() => contract = BeContractsMock.GetContracts().FirstOrDefault(c => c.Id.Equals(id)));
            return contract;
        }
    }
}