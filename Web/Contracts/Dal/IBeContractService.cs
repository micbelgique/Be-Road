using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dal
{
    public interface IBeContractService
    {
        Task<BeContract> FindBeContractByIdAsync(string id);
    }
}
