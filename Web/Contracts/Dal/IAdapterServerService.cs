using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dal
{
    public interface IAdapterServerService
    {
        Task<AdapterServer> FindASAsync(string name);
        Task<BeContractReturn> CallAsync(AdapterServer ads, BeContractCall call);
    }
}
