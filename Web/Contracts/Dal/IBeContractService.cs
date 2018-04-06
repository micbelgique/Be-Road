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
        BeContract FindBeContractById(string id);
    }
}
